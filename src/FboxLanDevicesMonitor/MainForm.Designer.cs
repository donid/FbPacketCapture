
namespace FboxLanDevicesMonitor
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.gridControlDevices = new DevExpress.XtraGrid.GridControl();
			this.gridViewDevices = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colActive = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colHostName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colInterfaceType = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colIPAddress = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colAddressSource = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colLeaseTimeRemaining = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colMACAddress = new DevExpress.XtraGrid.Columns.GridColumn();
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.barStaticItemLoggingTo = new DevExpress.XtraBars.BarStaticItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.checkEditAutoRefresh = new DevExpress.XtraEditors.CheckEdit();
			this.simpleButtonManageSavedCredentials = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonFromFile = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonRefreshLog = new DevExpress.XtraEditors.SimpleButton();
			this.gridControlLog = new DevExpress.XtraGrid.GridControl();
			this.gridViewLog = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colTimestamp = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colEventTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colLogMessage = new DevExpress.XtraGrid.Columns.GridColumn();
			this.simpleButtonRefreshDevices = new DevExpress.XtraEditors.SimpleButton();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemGridDevices = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemRefreshLog = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemGridLog = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.barStaticItemVersion = new DevExpress.XtraBars.BarStaticItem();
			((System.ComponentModel.ISupportInitialize)(this.gridControlDevices)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewDevices)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAutoRefresh.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlLog)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewLog)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGridDevices)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRefreshLog)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGridLog)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlDevices
			// 
			this.gridControlDevices.DataSource = typeof(FboxLanDevicesMonitor.HostEntryVM);
			this.gridControlDevices.Location = new System.Drawing.Point(12, 38);
			this.gridControlDevices.MainView = this.gridViewDevices;
			this.gridControlDevices.MenuManager = this.barManager1;
			this.gridControlDevices.Name = "gridControlDevices";
			this.gridControlDevices.Size = new System.Drawing.Size(798, 344);
			this.gridControlDevices.TabIndex = 0;
			this.gridControlDevices.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDevices});
			// 
			// gridViewDevices
			// 
			this.gridViewDevices.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colActive,
            this.colHostName,
            this.colInterfaceType,
            this.colIPAddress,
            this.colAddressSource,
            this.colLeaseTimeRemaining,
            this.colMACAddress});
			this.gridViewDevices.GridControl = this.gridControlDevices;
			this.gridViewDevices.Name = "gridViewDevices";
			this.gridViewDevices.OptionsView.ShowAutoFilterRow = true;
			this.gridViewDevices.OptionsView.ShowGroupPanel = false;
			this.gridViewDevices.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewDevices_RowCellStyle);
			this.gridViewDevices.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridViewDevices_PopupMenuShowing);
			this.gridViewDevices.CustomColumnSort += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(this.gridViewDevices_CustomColumnSort);
			// 
			// colActive
			// 
			this.colActive.FieldName = "Active";
			this.colActive.Name = "colActive";
			this.colActive.Visible = true;
			this.colActive.VisibleIndex = 0;
			// 
			// colHostName
			// 
			this.colHostName.FieldName = "HostName";
			this.colHostName.Name = "colHostName";
			this.colHostName.Visible = true;
			this.colHostName.VisibleIndex = 1;
			// 
			// colInterfaceType
			// 
			this.colInterfaceType.FieldName = "InterfaceType";
			this.colInterfaceType.Name = "colInterfaceType";
			this.colInterfaceType.Visible = true;
			this.colInterfaceType.VisibleIndex = 5;
			// 
			// colIPAddress
			// 
			this.colIPAddress.FieldName = "IPAddress";
			this.colIPAddress.Name = "colIPAddress";
			this.colIPAddress.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
			this.colIPAddress.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
			this.colIPAddress.Visible = true;
			this.colIPAddress.VisibleIndex = 2;
			// 
			// colAddressSource
			// 
			this.colAddressSource.FieldName = "AddressSource";
			this.colAddressSource.Name = "colAddressSource";
			this.colAddressSource.Visible = true;
			this.colAddressSource.VisibleIndex = 4;
			// 
			// colLeaseTimeRemaining
			// 
			this.colLeaseTimeRemaining.FieldName = "LeaseTimeRemaining";
			this.colLeaseTimeRemaining.Name = "colLeaseTimeRemaining";
			this.colLeaseTimeRemaining.Visible = true;
			this.colLeaseTimeRemaining.VisibleIndex = 6;
			// 
			// colMACAddress
			// 
			this.colMACAddress.FieldName = "MACAddress";
			this.colMACAddress.Name = "colMACAddress";
			this.colMACAddress.Visible = true;
			this.colMACAddress.VisibleIndex = 3;
			// 
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItemLoggingTo,
            this.barStaticItemVersion});
			this.barManager1.MaxItemId = 2;
			this.barManager1.StatusBar = this.bar3;
			// 
			// bar3
			// 
			this.bar3.BarName = "Status bar";
			this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.bar3.DockCol = 0;
			this.bar3.DockRow = 0;
			this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemLoggingTo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemVersion)});
			this.bar3.OptionsBar.AllowQuickCustomization = false;
			this.bar3.OptionsBar.DrawDragBorder = false;
			this.bar3.OptionsBar.UseWholeRow = true;
			this.bar3.Text = "Status bar";
			// 
			// barStaticItemLoggingTo
			// 
			this.barStaticItemLoggingTo.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.barStaticItemLoggingTo.Caption = "Logging To:";
			this.barStaticItemLoggingTo.Id = 0;
			this.barStaticItemLoggingTo.Name = "barStaticItemLoggingTo";
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Manager = this.barManager1;
			this.barDockControlTop.Size = new System.Drawing.Size(822, 0);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 652);
			this.barDockControlBottom.Manager = this.barManager1;
			this.barDockControlBottom.Size = new System.Drawing.Size(822, 27);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
			this.barDockControlLeft.Manager = this.barManager1;
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 652);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(822, 0);
			this.barDockControlRight.Manager = this.barManager1;
			this.barDockControlRight.Size = new System.Drawing.Size(0, 652);
			// 
			// toolTipController1
			// 
			this.toolTipController1.AllowHtmlText = true;
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.labelControl1);
			this.layoutControl1.Controls.Add(this.checkEditAutoRefresh);
			this.layoutControl1.Controls.Add(this.simpleButtonManageSavedCredentials);
			this.layoutControl1.Controls.Add(this.simpleButtonFromFile);
			this.layoutControl1.Controls.Add(this.simpleButtonRefreshLog);
			this.layoutControl1.Controls.Add(this.gridControlLog);
			this.layoutControl1.Controls.Add(this.simpleButtonRefreshDevices);
			this.layoutControl1.Controls.Add(this.gridControlDevices);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(950, 408, 650, 400);
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(822, 652);
			this.layoutControl1.TabIndex = 1;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(310, 12);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(500, 22);
			this.labelControl1.StyleController = this.layoutControl1;
			this.labelControl1.TabIndex = 11;
			this.labelControl1.Text = "Click \'Refresh\' to see a summary of the changes here (and in the tooltip)";
			this.labelControl1.ToolTipController = this.toolTipController1;
			// 
			// checkEditAutoRefresh
			// 
			this.checkEditAutoRefresh.Location = new System.Drawing.Point(424, 412);
			this.checkEditAutoRefresh.Name = "checkEditAutoRefresh";
			this.checkEditAutoRefresh.Properties.Caption = "AutoRefresh";
			this.checkEditAutoRefresh.Size = new System.Drawing.Size(196, 19);
			this.checkEditAutoRefresh.StyleController = this.layoutControl1;
			this.checkEditAutoRefresh.TabIndex = 10;
			this.checkEditAutoRefresh.CheckedChanged += new System.EventHandler(this.checkEditAutoRefresh_CheckedChanged);
			// 
			// simpleButtonManageSavedCredentials
			// 
			this.simpleButtonManageSavedCredentials.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButtonManageSavedCredentials.ImageOptions.SvgImage")));
			this.simpleButtonManageSavedCredentials.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
			this.simpleButtonManageSavedCredentials.Location = new System.Drawing.Point(624, 412);
			this.simpleButtonManageSavedCredentials.Name = "simpleButtonManageSavedCredentials";
			this.simpleButtonManageSavedCredentials.Size = new System.Drawing.Size(183, 22);
			this.simpleButtonManageSavedCredentials.StyleController = this.layoutControl1;
			this.simpleButtonManageSavedCredentials.TabIndex = 9;
			this.simpleButtonManageSavedCredentials.Text = "Manage Saved Credentials";
			this.simpleButtonManageSavedCredentials.Click += new System.EventHandler(this.simpleButtonManageSavedCredentials_Click);
			// 
			// simpleButtonFromFile
			// 
			this.simpleButtonFromFile.Location = new System.Drawing.Point(15, 412);
			this.simpleButtonFromFile.Name = "simpleButtonFromFile";
			this.simpleButtonFromFile.Size = new System.Drawing.Size(194, 22);
			this.simpleButtonFromFile.StyleController = this.layoutControl1;
			this.simpleButtonFromFile.TabIndex = 8;
			this.simpleButtonFromFile.Text = "From File";
			this.simpleButtonFromFile.Click += new System.EventHandler(this.simpleButtonFromFile_Click);
			// 
			// simpleButtonRefreshLog
			// 
			this.simpleButtonRefreshLog.Location = new System.Drawing.Point(213, 412);
			this.simpleButtonRefreshLog.Name = "simpleButtonRefreshLog";
			this.simpleButtonRefreshLog.Size = new System.Drawing.Size(207, 22);
			this.simpleButtonRefreshLog.StyleController = this.layoutControl1;
			this.simpleButtonRefreshLog.TabIndex = 7;
			this.simpleButtonRefreshLog.Text = "From Box";
			this.simpleButtonRefreshLog.Click += new System.EventHandler(this.simpleButtonRefreshLog_Click);
			// 
			// gridControlLog
			// 
			this.gridControlLog.DataSource = typeof(FboxSharp.FboxLogEntry);
			this.gridControlLog.Location = new System.Drawing.Point(15, 438);
			this.gridControlLog.MainView = this.gridViewLog;
			this.gridControlLog.Name = "gridControlLog";
			this.gridControlLog.Size = new System.Drawing.Size(792, 199);
			this.gridControlLog.TabIndex = 6;
			this.gridControlLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLog});
			// 
			// gridViewLog
			// 
			this.gridViewLog.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTimestamp,
            this.colCategory,
            this.colEventTypeId,
            this.colLogMessage});
			this.gridViewLog.GridControl = this.gridControlLog;
			this.gridViewLog.Name = "gridViewLog";
			this.gridViewLog.OptionsSelection.InvertSelection = true;
			this.gridViewLog.OptionsView.ShowGroupPanel = false;
			this.gridViewLog.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewLog_RowStyle);
			// 
			// colTimestamp
			// 
			this.colTimestamp.DisplayFormat.FormatString = "G";
			this.colTimestamp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.colTimestamp.FieldName = "Timestamp";
			this.colTimestamp.Name = "colTimestamp";
			this.colTimestamp.OptionsColumn.FixedWidth = true;
			this.colTimestamp.Visible = true;
			this.colTimestamp.VisibleIndex = 0;
			this.colTimestamp.Width = 130;
			// 
			// colCategory
			// 
			this.colCategory.FieldName = "Category";
			this.colCategory.Name = "colCategory";
			this.colCategory.OptionsColumn.FixedWidth = true;
			this.colCategory.Visible = true;
			this.colCategory.VisibleIndex = 1;
			this.colCategory.Width = 80;
			// 
			// colEventTypeId
			// 
			this.colEventTypeId.Caption = "Type ID";
			this.colEventTypeId.FieldName = "EventTypeId";
			this.colEventTypeId.Name = "colEventTypeId";
			this.colEventTypeId.OptionsColumn.FixedWidth = true;
			this.colEventTypeId.Visible = true;
			this.colEventTypeId.VisibleIndex = 2;
			this.colEventTypeId.Width = 70;
			// 
			// colLogMessage
			// 
			this.colLogMessage.FieldName = "LogMessage";
			this.colLogMessage.Name = "colLogMessage";
			this.colLogMessage.Visible = true;
			this.colLogMessage.VisibleIndex = 3;
			this.colLogMessage.Width = 494;
			// 
			// simpleButtonRefreshDevices
			// 
			this.simpleButtonRefreshDevices.Location = new System.Drawing.Point(12, 12);
			this.simpleButtonRefreshDevices.Name = "simpleButtonRefreshDevices";
			this.simpleButtonRefreshDevices.Size = new System.Drawing.Size(294, 22);
			this.simpleButtonRefreshDevices.StyleController = this.layoutControl1;
			this.simpleButtonRefreshDevices.TabIndex = 4;
			this.simpleButtonRefreshDevices.Text = "Refresh";
			this.simpleButtonRefreshDevices.Click += new System.EventHandler(this.simpleButtonRefreshDevices_Click);
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemGridDevices,
            this.layoutControlItem2,
            this.layoutControlGroup1,
            this.splitterItem1,
            this.layoutControlItem5});
			this.Root.Name = "Root";
			this.Root.Size = new System.Drawing.Size(822, 652);
			this.Root.TextVisible = false;
			// 
			// layoutControlItemGridDevices
			// 
			this.layoutControlItemGridDevices.Control = this.gridControlDevices;
			this.layoutControlItemGridDevices.Location = new System.Drawing.Point(0, 26);
			this.layoutControlItemGridDevices.MinSize = new System.Drawing.Size(104, 24);
			this.layoutControlItemGridDevices.Name = "layoutControlItemGridDevices";
			this.layoutControlItemGridDevices.Size = new System.Drawing.Size(802, 348);
			this.layoutControlItemGridDevices.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemGridDevices.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemGridDevices.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.simpleButtonRefreshDevices;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(298, 26);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemRefreshLog,
            this.layoutControlItemGridLog,
            this.layoutControlItem4,
            this.layoutControlItem1,
            this.layoutControlItem3});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 379);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(802, 253);
			this.layoutControlGroup1.Text = "Log Entries";
			this.layoutControlGroup1.TextLocation = DevExpress.Utils.Locations.Default;
			// 
			// layoutControlItemRefreshLog
			// 
			this.layoutControlItemRefreshLog.Control = this.simpleButtonRefreshLog;
			this.layoutControlItemRefreshLog.Location = new System.Drawing.Point(198, 0);
			this.layoutControlItemRefreshLog.Name = "layoutControlItemRefreshLog";
			this.layoutControlItemRefreshLog.Size = new System.Drawing.Size(211, 26);
			this.layoutControlItemRefreshLog.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemRefreshLog.TextVisible = false;
			// 
			// layoutControlItemGridLog
			// 
			this.layoutControlItemGridLog.Control = this.gridControlLog;
			this.layoutControlItemGridLog.Location = new System.Drawing.Point(0, 26);
			this.layoutControlItemGridLog.MinSize = new System.Drawing.Size(104, 24);
			this.layoutControlItemGridLog.Name = "layoutControlItemGridLog";
			this.layoutControlItemGridLog.Size = new System.Drawing.Size(796, 203);
			this.layoutControlItemGridLog.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemGridLog.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemGridLog.TextVisible = false;
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.checkEditAutoRefresh;
			this.layoutControlItem4.Location = new System.Drawing.Point(409, 0);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(200, 26);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.simpleButtonFromFile;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(198, 26);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.simpleButtonManageSavedCredentials;
			this.layoutControlItem3.Location = new System.Drawing.Point(609, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(187, 26);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// splitterItem1
			// 
			this.splitterItem1.Location = new System.Drawing.Point(0, 374);
			this.splitterItem1.Name = "splitterItem1";
			this.splitterItem1.Size = new System.Drawing.Size(802, 5);
			// 
			// layoutControlItem5
			// 
			this.layoutControlItem5.Control = this.labelControl1;
			this.layoutControlItem5.Location = new System.Drawing.Point(298, 0);
			this.layoutControlItem5.MinSize = new System.Drawing.Size(67, 17);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Size = new System.Drawing.Size(504, 26);
			this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			// 
			// timer1
			// 
			this.timer1.Interval = 10000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// barStaticItemVersion
			// 
			this.barStaticItemVersion.Caption = "Version";
			this.barStaticItemVersion.Id = 1;
			this.barStaticItemVersion.Name = "barStaticItemVersion";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(822, 679);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("MainForm.IconOptions.SvgImage")));
			this.Name = "MainForm";
			this.Text = "Fbox Lan-Devices Monitor";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridControlDevices)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewDevices)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkEditAutoRefresh.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlLog)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewLog)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGridDevices)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRefreshLog)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGridLog)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControlDevices;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewDevices;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGridDevices;
		private DevExpress.XtraEditors.SimpleButton simpleButtonRefreshDevices;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRefreshLog;
        private DevExpress.XtraGrid.GridControl gridControlLog;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLog;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGridLog;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRefreshLog;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colActive;
        private DevExpress.XtraGrid.Columns.GridColumn colHostName;
        private DevExpress.XtraGrid.Columns.GridColumn colInterfaceType;
        private DevExpress.XtraGrid.Columns.GridColumn colIPAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colAddressSource;
        private DevExpress.XtraGrid.Columns.GridColumn colLeaseTimeRemaining;
        private DevExpress.XtraGrid.Columns.GridColumn colMACAddress;
		private DevExpress.XtraGrid.Columns.GridColumn colTimestamp;
		private DevExpress.XtraGrid.Columns.GridColumn colCategory;
		private DevExpress.XtraGrid.Columns.GridColumn colEventTypeId;
		private DevExpress.XtraGrid.Columns.GridColumn colLogMessage;
		private DevExpress.XtraEditors.SimpleButton simpleButtonFromFile;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraEditors.SimpleButton simpleButtonManageSavedCredentials;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraEditors.CheckEdit checkEditAutoRefresh;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
		private System.Windows.Forms.Timer timer1;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
		private DevExpress.Utils.ToolTipController toolTipController1;
		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarStaticItem barStaticItemLoggingTo;
		private DevExpress.XtraBars.BarStaticItem barStaticItemVersion;
	}
}

