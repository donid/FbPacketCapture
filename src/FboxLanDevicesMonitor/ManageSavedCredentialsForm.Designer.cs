namespace FboxLanDevicesMonitor
{
	partial class ManageSavedCredentialsForm
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
			this.simpleButtonDeleteSavedCredentials = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonTestSavedCredentials = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonCreateSavedCredentials = new DevExpress.XtraEditors.SimpleButton();
			this.SuspendLayout();
			// 
			// simpleButtonDeleteSavedCredentials
			// 
			this.simpleButtonDeleteSavedCredentials.Location = new System.Drawing.Point(39, 70);
			this.simpleButtonDeleteSavedCredentials.Name = "simpleButtonDeleteSavedCredentials";
			this.simpleButtonDeleteSavedCredentials.Size = new System.Drawing.Size(142, 22);
			this.simpleButtonDeleteSavedCredentials.TabIndex = 10;
			this.simpleButtonDeleteSavedCredentials.Text = "Delete Saved Credentials";
			this.simpleButtonDeleteSavedCredentials.Click += new System.EventHandler(this.simpleButtonDeleteSavedCredentials_Click);
			// 
			// simpleButtonTestSavedCredentials
			// 
			this.simpleButtonTestSavedCredentials.Location = new System.Drawing.Point(39, 12);
			this.simpleButtonTestSavedCredentials.Name = "simpleButtonTestSavedCredentials";
			this.simpleButtonTestSavedCredentials.Size = new System.Drawing.Size(142, 23);
			this.simpleButtonTestSavedCredentials.TabIndex = 11;
			this.simpleButtonTestSavedCredentials.Text = "Test Saved Credentials";
			this.simpleButtonTestSavedCredentials.Click += new System.EventHandler(this.simpleButtonTestSavedCredentials_Click);
			// 
			// simpleButtonCreateSavedCredentials
			// 
			this.simpleButtonCreateSavedCredentials.Location = new System.Drawing.Point(39, 41);
			this.simpleButtonCreateSavedCredentials.Name = "simpleButtonCreateSavedCredentials";
			this.simpleButtonCreateSavedCredentials.Size = new System.Drawing.Size(142, 23);
			this.simpleButtonCreateSavedCredentials.TabIndex = 12;
			this.simpleButtonCreateSavedCredentials.Text = "Create Saved Credentials";
			this.simpleButtonCreateSavedCredentials.Click += new System.EventHandler(this.simpleButtonCreateSavedCredentials_Click);
			// 
			// ManageSavedCredentialsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(215, 101);
			this.Controls.Add(this.simpleButtonCreateSavedCredentials);
			this.Controls.Add(this.simpleButtonTestSavedCredentials);
			this.Controls.Add(this.simpleButtonDeleteSavedCredentials);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ManageSavedCredentialsForm";
			this.ShowInTaskbar = false;
			this.Text = "Manage Saved Credentials";
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton simpleButtonDeleteSavedCredentials;
		private DevExpress.XtraEditors.SimpleButton simpleButtonTestSavedCredentials;
		private DevExpress.XtraEditors.SimpleButton simpleButtonCreateSavedCredentials;
	}
}
