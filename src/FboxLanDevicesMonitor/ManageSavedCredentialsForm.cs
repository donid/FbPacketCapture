using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdysTech.CredentialManager;
using DevExpress.XtraEditors;
using FboxSharp;
using PS.FritzBox.API;

namespace FboxLanDevicesMonitor
{
	public partial class ManageSavedCredentialsForm : Form
	{
		public ManageSavedCredentialsForm()
		{
			InitializeComponent();
		}

		private void simpleButtonDeleteSavedCredentials_Click(object sender, EventArgs e)
		{
			string credManTarget = MainForm.Instance.CredManTarget;
			ICredential found = CredentialManager.GetICredential(credManTarget);
			if (found == null)
			{
				MainForm.Instance.ShowMessageBox($"There are no saved credentials '{credManTarget}'");
				return;
			}

			string message = $"Do you really want to delete the saved credentials '{credManTarget}' ?";
			DialogResult result = XtraMessageBox.Show(this, message, Application.ProductName,
						MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (result != DialogResult.OK)
			{
				return;
			}

			try
			{
				CredentialManager.RemoveCredentials(credManTarget);
			}
			catch (Exception ex)
			{
				MainForm.Instance.ShowMessageBox(ex.Message);
			}

		}

		private void simpleButtonTestSavedCredentials_Click(object sender, EventArgs e)
		{
			// todo cleanup duplicated code
			string credManTarget = MainForm.Instance.CredManTarget;
			ICredential found = CredentialManager.GetICredential(credManTarget);
			if (found == null)
			{
				MainForm.Instance.ShowMessageBox($"There are no saved credentials '{credManTarget}'");
				return;
			}
			NetworkCredential savedCredential = found.ToNetworkCredential();

			string hostName = ConfigurationManager.AppSettings["FbHostName"];
			var _connectionSettings = new FboxConnectionSettings(savedCredential.UserName, savedCredential.Password) { Host = hostName };
			var _fboxClient = new FboxClient(_connectionSettings);
			try
			{
				string sid=_fboxClient.CreateNewSession();
				MainForm.Instance.ShowMessageBox("Success! SessionId: "+sid);
			}
			catch (FboxSharpException ex)
			{
				MainForm.Instance.ShowMessageBox("Error: "+ex.Message);
			}
		}


		private void simpleButtonCreateSavedCredentials_Click(object sender, EventArgs e)
		{
			string credManTarget = MainForm.Instance.CredManTarget;

			string dialogCaption = Application.ProductName;
			string message = "Enter credentials of a FritzBox user account" + Environment.NewLine + "with permission to change FritzBox settings";

			bool credentialsShouldBeSaved = true;// false: 'save cred' checkbox will be hidden!
			NetworkCredential enteredCredentials = CredentialManager.PromptForCredentials(credManTarget, ref credentialsShouldBeSaved, message, dialogCaption);
			if (enteredCredentials == null)
			{
				// user clicked 'cancel'
				MainForm.Instance.ShowMessageBox($"Credentials input was cancelled.");
				return;
			}
			if (credentialsShouldBeSaved)
			{
				try
				{
					CredentialManager.SaveCredentials(credManTarget, enteredCredentials);
					MainForm.Instance.ShowMessageBox($"Credentials have been saved.");
				}
				catch (ArgumentNullException ex)
				{
					// user did not enter anything
					// message:CredentialBlob can't be Null or Empty
					MainForm.Instance.ShowMessageBox($"Error saving credentials: {ex.Message}");
				}
			}

		}


	}
}
