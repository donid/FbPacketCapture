using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AdysTech.CredentialManager;

using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

using DnblCore;

using FboxLanDevicesMonitor.Properties;

using FboxSharp;

using PS.FritzBox.API;
using PS.FritzBox.API.LANDevice;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FboxLanDevicesMonitor
{

	// todo: does the api provide ipv6 addresses?
	// autofilter and filter dropdown on ipaddress-column do not work

	public partial class MainForm : XtraForm
	{
		private readonly string _credManTarget = string.Empty;
		public string CredManTarget
		{
			get => _credManTarget;
		}

		private static MainForm? s_mainFormInstance;
		public static MainForm Instance
		{
			get
			{
				if (s_mainFormInstance == null) // suppress nullable warning
				{
					throw new Exception();
				}
				return s_mainFormInstance;
			}
		}

		private FboxConnectionSettings? _connectionSettings;
		private FboxClient? _fboxClient = null;
		private List<HostEntryVM>? _currentDevicesList;
		private DateTime? _latestLogEntryTimestamp = null;
		private IReadOnlyList<FboxLogEntry>? _logLines;
		private DiffResults<HostEntryVM, string[]>? _devicesDiffResult;


		public MainForm()
		{
			InitializeComponent();
			s_mainFormInstance = this;
			_credManTarget = ConfigurationManager.AppSettings["CredentialManagerTarget"];
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			base.Text += " " + Application.ProductVersion;
		}

		private async void Form1_Load(object sender, EventArgs e)
		{
			CreateConnectionSettingsAndFboxClient();
			await LoadAndDevicesShowData();
		}

		private void CreateConnectionSettingsAndFboxClient()
		{
			NetworkCredential savedCredential = CredentialManager.GetCredentials(_credManTarget);
			string hostName = ConfigurationManager.AppSettings["FbHostName"];
			string userName = string.Empty;
			string password = string.Empty;
			if (savedCredential != null)
			{
				userName = savedCredential.UserName;
				password = savedCredential.Password;
			}
			_connectionSettings = new FboxConnectionSettings(userName, password) { Host = hostName };
			_fboxClient = new FboxClient(_connectionSettings);
		}

		internal void ShowMessageBox(string message)
		{
			XtraMessageBox.Show(this, message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private async Task LoadAndDevicesShowData()
		{
			if (_connectionSettings == null)
			{
				return;
			}

			// works with empty UserName and Password - even on a FritzBox that is configured for user-based credentials !
			ConnectionSettings psSettings = Helpers.ConvertToPsapiConnectionSettings(_connectionSettings);

			gridViewDevices.LoadingPanelVisible = true;

			List<HostEntry> tmp = await Helpers.GetAllHosts(psSettings);
			_currentDevicesList = tmp.Select(i => new HostEntryVM(i)).ToList();
			gridControlDevices.DataSource = _currentDevicesList;

			// this is not sufficient!, OptionsColumn.AllowSort has to be changed from Default to true !!!!
			//colIPAddress.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;

			colActive.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
			colActive.BestFit();
			colAddressSource.BestFit();
			colInterfaceType.BestFit();

			gridViewDevices.LoadingPanelVisible = false;
			if (_currentDevicesList.Any())
			{
				gridViewDevices.FocusedRowHandle = 0;
			}

		}

		// not sure why, but a duplicate MAC-Address occurred once after attaching a new device to the network
		private static List<HostEntryVM> FilterUniqueMacAddresses(List<HostEntryVM> devices, out string messages)
		{
			List<HostEntryVM> result = new List<HostEntryVM>();
			StringBuilder stringBuilder = new StringBuilder();
			var dict = new Dictionary<string, HostEntryVM>();
			foreach (HostEntryVM hostEntry in devices)
			{
				if (dict.TryGetValue(hostEntry.MACAddress, out var found))
				{
					string msg = $"Ignoring {hostEntry.MACAddress} {hostEntry.HostName} {hostEntry.IPAddress} because {hostEntry.MACAddress} {hostEntry.HostName} {hostEntry.IPAddress} has the same MAC-Address";
					stringBuilder.AppendLine(msg);
				}
				else
				{
					dict.Add(hostEntry.MACAddress, hostEntry);
				}
			}
			messages = stringBuilder.ToString();
			return result;
		}

		private async void simpleButtonRefreshDevices_Click(object sender, EventArgs e)
		{
			if (_connectionSettings == null)
			{
				return;
			}

			gridViewDevices.LoadingPanelVisible = true;
			//Application.UseWaitCursor = true;
			try
			{
				//Cursor.Current = Cursors.WaitCursor;
				ConnectionSettings psSettings = Helpers.ConvertToPsapiConnectionSettings(_connectionSettings);
				List<HostEntry> newList = await Helpers.GetAllHosts(psSettings);
				List<HostEntryVM> newDevicesList = newList.Select(i => new HostEntryVM(i)).ToList();
				List<HostEntryVM> currentDevicesList = _currentDevicesList ?? new List<HostEntryVM>();
				List<HostEntryVM> filteredCurrentDevices = FilterUniqueMacAddresses(currentDevicesList, out string messages);
				DiffResults<HostEntryVM, string[]> diffResult =
					ListDiffer.ListsDiff(currentDevicesList, newDevicesList, (HostEntryVM he) => he.MACAddress, HostEntryVM.ListDiffComparer);
				labelControl1.Text = $"{DateTime.Now.ToLongTimeString()} - {diffResult.Added.Count()} added - {diffResult.Removed.Count()} removed - {diffResult.Changed.Count()} changed";
				labelControl1.ToolTip = Helpers.GetTooltipText(diffResult);

				if (messages != "")
				{
					ShowMessageBox(messages);
				}

				_devicesDiffResult = diffResult;
				_currentDevicesList = newDevicesList;
				gridControlDevices.DataSource = _currentDevicesList;
			}
			finally
			{
				gridViewDevices.LoadingPanelVisible = false;
				// WaitCursor is not removed until the mouse is moved!?!?
				//Application.UseWaitCursor = false;
			}
		}

		private async void simpleButtonRefreshLog_Click(object sender, EventArgs e)
		{
			await RefreshLogAsync();
			simpleButtonRefreshLog.Text = "Refresh From Box";
		}

		// return value: success
		private async Task<bool> RefreshLogAsync()
		{
			if (_fboxClient == null)
			{
				return false;
			}

			bool showLoadingPanel = true;
			if (_logLines != null && _logLines.Any())
			{
				showLoadingPanel = false;
				_latestLogEntryTimestamp = _logLines.OrderByDescending(i => i.Timestamp).First().Timestamp.AddSeconds(1);
			}

			if (showLoadingPanel)
			{
				gridViewLog.LoadingPanelVisible = true;
			}

			bool success = true;
			try
			{
				_logLines = await _fboxClient.GetAllLogEntriesAsync();
				gridControlLog.DataSource = _logLines;
			}
			catch (Exception ex)
			{
				success = false;
				ShowMessageBox(ex.Message);
			}

			if (showLoadingPanel)
			{
				gridViewLog.LoadingPanelVisible = false;
			}
			return success;
		}

		private void gridViewDevices_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
		{
			try
			{
				if (e.Column == colIPAddress)
				{
					//object val1 = gridViewDevices.GetListSourceRowCellValue(e.ListSourceRowIndex1, "IsEmptyRow");
					//object val2 = gridViewDevices.GetListSourceRowCellValue(e.ListSourceRowIndex2, "IsEmptyRow");
					HostEntryVM? entry1 = e.RowObject1 as HostEntryVM;
					HostEntryVM? entry2 = e.RowObject2 as HostEntryVM;
					if (entry1 == null || entry2 == null)
					{
						return;
					}

					e.Handled = true;
					//e.Result = System.Collections.Comparer.Default.Compare(entry1.IPAddress, entry2.IPAddress);
					e.Result = Helpers.CompareIPAddress(entry1.IPAddress, entry2.IPAddress);
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine("gridViewDevices_CustomColumnSortexception: " + ex.ToString());
			}
		}

		private void simpleButtonFromFile_Click(object sender, EventArgs e)
		{
			string? csvFilePath = null;

			OpenFileDialog openFileDialog1 = new OpenFileDialog
			{
				//InitialDirectory = @"D:\",
				Title = "Select FritzBox-EventLog file",

				CheckFileExists = true,
				CheckPathExists = true,

				DefaultExt = "csv",
				Filter = "csv files (*.csv)|*.csv",
				FilterIndex = 2,
				RestoreDirectory = true,

				ReadOnlyChecked = true,
				ShowReadOnly = true
			};

			if (openFileDialog1.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			csvFilePath = openFileDialog1.FileName;

			_latestLogEntryTimestamp = null;
			gridViewLog.LoadingPanelVisible = true;
			try
			{
				_logLines = LogEntriesCsvHelper.ReadAllEntries(csvFilePath).ToList();
				gridControlLog.DataSource = _logLines;
			}
			catch (CsvHelper.ReaderException ex)
			{
				ShowMessageBox("File has an unexpected format!: " + ex.Message);
			}
			catch (CsvHelper.TypeConversion.TypeConverterException ex)
			{
				ShowMessageBox("File has an unexpected format!: " + ex.Message);
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
			gridViewLog.LoadingPanelVisible = false;

		}

		private void gridViewLog_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
		{
			if (_latestLogEntryTimestamp == null)
			{
				return;
			}
			FboxLogEntry? entry = gridViewLog.GetRow(e.RowHandle) as FboxLogEntry;
			if (entry == null || entry.Timestamp < _latestLogEntryTimestamp)
			{
				return;
			}
			e.Appearance.BackColor = Color.GreenYellow;
		}

		private void simpleButtonManageSavedCredentials_Click(object sender, EventArgs e)
		{
			ManageSavedCredentialsForm form = new ManageSavedCredentialsForm();
			form.ShowDialog(this);
			CreateConnectionSettingsAndFboxClient();
		}

		private void checkEditAutoRefresh_CheckedChanged(object sender, EventArgs e)
		{
			timer1.Enabled = checkEditAutoRefresh.Checked;
		}

		private async void timer1_Tick(object sender, EventArgs e)
		{
			bool success = await RefreshLogAsync();
			if (!success)
			{
				checkEditAutoRefresh.Checked = false;
			}
		}

		private void gridViewDevices_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
		{
			if (_devicesDiffResult == null)
			{
				return;
			}
			HostEntryVM row = (HostEntryVM)gridViewDevices.GetRow(e.RowHandle);
			if (row == null)
			{
				return;
			}
			if (_devicesDiffResult.Added.Any(i => i.MACAddress == row.MACAddress))
			{
				e.Appearance.BackColor = Color.GreenYellow;
				return;
			}
			// relies on the mac-address not changing
			DiffResultChangedItem<HostEntryVM, string[]>? found = _devicesDiffResult.Changed.FirstOrDefault(i => i.Item1.MACAddress == row.MACAddress);
			if (found != null)
			{
				string currentPropertyName = e.Column.FieldName;
				if (found.DiffInfo.Contains(currentPropertyName))
				{
					e.Appearance.BackColor = Color.Yellow;
				}
			}
		}

		private void gridViewDevices_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
		{
			if (e.HitInfo.InRow)
			{
				AddDevicesRowMenuItem(e, "Copy Name as HTTP-Url", CopyNameUrlHandler);
				AddDevicesRowMenuItem(e, "Copy IP as HTTP-Url", CopyIpUrlHandler);
				AddDevicesRowMenuItem(e, "Mac Vendor Lookup", MacLookupHandler);
				AddDevicesRowMenuItem(e, "Start diagnosis shell", DiagnosisShellHandler);
			}
		}
		private void DiagnosisShellHandler(object sender, EventArgs e)
		{
			DXMenuItem? menuItem = sender as DXMenuItem;
			if (menuItem == null)
			{
				return;
			}

			HostEntryVM row = (HostEntryVM)menuItem.Tag;
			string command = $"-noexit -command \"& {{ Resolve-DnsName {row.HostName} -LlmnrFallback -NetbiosFallback ; ping {row.IPAddress} }}\" ";
			try
			{
				Process.Start("powershell.exe", command);
			}
			catch (Exception ex)
			{
				ShowMessageBox("Cannot start powershell: " + ex.Message);
			}
		}

		private void CopyNameUrlHandler(object sender, EventArgs e)
		{
			CopyUrlHandler(sender, e, true);
		}
		private void CopyIpUrlHandler(object sender, EventArgs e)
		{
			CopyUrlHandler(sender, e, false);
		}

		private void CopyUrlHandler(object sender, EventArgs e, bool useHostName)
		{
			DXMenuItem? menuItem = sender as DXMenuItem;
			if (menuItem == null)
			{
				return;
			}

			HostEntryVM row = (HostEntryVM)menuItem.Tag;
			string nameOrIp = useHostName ? row.HostName : row.IPAddress.ToString();
			string text = "http://" + nameOrIp;
			Clipboard.SetText(text);
		}

		private void AddDevicesRowMenuItem(PopupMenuShowingEventArgs e, string caption, EventHandler handler)
		{
			DXMenuItem menuItem = new DXMenuItem(caption, handler);
			HostEntryVM row = (HostEntryVM)gridViewDevices.GetRow(e.HitInfo.RowHandle);
			menuItem.Tag = row;
			e.Menu.Items.Add(menuItem);
		}

		private void MacLookupHandler(object sender, EventArgs e)
		{
			WebClient client = new WebClient();
			DXMenuItem? menuItem = sender as DXMenuItem;
			if (menuItem == null)
			{
				return;
			}

			HostEntryVM row = (HostEntryVM)menuItem.Tag;
			string macAddress = row.MACAddress.Replace(":", "");
			if (string.IsNullOrWhiteSpace(macAddress))
			{
				ShowMessageBox("Invalid MAC-Address");
				return;
			}

			string uri = "https://api.macvendors.com/" + macAddress;
			string result;
			try
			{
				result = client.DownloadString(uri);
			}
			catch (WebException ex)
			{
				if (ex.Response is HttpWebResponse resp && resp.StatusCode == HttpStatusCode.NotFound)
				{
					result = "No entry found in the vendor database!";
				}
				else
				{
					result = ex.Message;
				}
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			ShowMessageBox(result);
		}
	}
}
