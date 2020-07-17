using System;

namespace SharpEncrypt.Forms
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
            this.Tabs = new System.Windows.Forms.TabControl();
            this.SecuredFilesTab = new System.Windows.Forms.TabPage();
            this.SecuredFilesGrid = new System.Windows.Forms.DataGridView();
            this.FileColumnHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeColumnHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SecuredColumnHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlgorithmColumnHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveFileFromListButKeepSecuredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopSecuringAndRemoveFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShareKeysToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowInFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameToOriginalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearSecuredFilesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddSecuredFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowAllSecuredFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.HideExcludedSecuredFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnonymizeFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeanonymizeFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SecuredFoldersTab = new System.Windows.Forms.TabPage();
            this.SecuredFoldersGrid = new System.Windows.Forms.DataGridView();
            this.Folder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FolderMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShareKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DecryptPermanentlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DecryptTemporarilyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveFolderFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenExplorerHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearSecuredFoldersGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddSecuredFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowAllSecuredFolders = new System.Windows.Forms.ToolStripMenuItem();
            this.HideExcludedSecuredFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BulkAnonymousRenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BulkDeanonymizeFileNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenSecuredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SecureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopSecuringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SecuredFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnonymousRenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeanonymiseFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddSecuredFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SecureDeleteFile = new System.Windows.Forms.ToolStripMenuItem();
            this.SecureDeleteFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Advanced = new System.Windows.Forms.ToolStripMenuItem();
            this.OneTimePadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SecureFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DecryptFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GenerateKeyForFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EnglishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeutschGermanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NetherlandsDutchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FrancaisFrenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItalianoItalianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KoreanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PolskiPolishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PortugueseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RussianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SwedishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TurkishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KeyManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportPublicKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportMyPublicKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GenerateNewKeyPairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasswordManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenPasswordManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ManagementStoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AESPasswordBasedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OTPKeyBasedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeSessionPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearSessionPassword = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetAllSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Debug = new System.Windows.Forms.ToolStripMenuItem();
            this.IncludeSubfolders = new System.Windows.Forms.ToolStripMenuItem();
            this.DoNotPromptForPasswordOnStartup = new System.Windows.Forms.ToolStripMenuItem();
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UseADifferentPasswordForEachFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebugMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.ValidateContainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewActiveJobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewCompletedJobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CancelAllFutureTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteGridExclusionLists = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DiskToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WipeFreeDiskSpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdvancedMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.ForceExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IndividualSettingsResetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AppLabel = new System.Windows.Forms.Label();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.AddSecuredGUIButton = new System.Windows.Forms.Button();
            this.OpenSecuredGUIButton = new System.Windows.Forms.Button();
            this.ShareButton = new System.Windows.Forms.Button();
            this.PasswordManagement = new System.Windows.Forms.Button();
            this.OpenSecuredToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.AddSecuredFileToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ShareToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PasswordManagementToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.OpenHomeFolder = new System.Windows.Forms.Button();
            this.OpenHomeFolderToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.NotifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Tabs.SuspendLayout();
            this.SecuredFilesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SecuredFilesGrid)).BeginInit();
            this.FileMenuStrip.SuspendLayout();
            this.SecuredFoldersTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SecuredFoldersGrid)).BeginInit();
            this.FolderMenuStrip.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.NotifyIconContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.AllowDrop = true;
            this.Tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tabs.Controls.Add(this.SecuredFilesTab);
            this.Tabs.Controls.Add(this.SecuredFoldersTab);
            this.Tabs.Location = new System.Drawing.Point(12, 165);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(1068, 405);
            this.Tabs.TabIndex = 0;
            // 
            // SecuredFilesTab
            // 
            this.SecuredFilesTab.Controls.Add(this.SecuredFilesGrid);
            this.SecuredFilesTab.Location = new System.Drawing.Point(4, 29);
            this.SecuredFilesTab.Name = "SecuredFilesTab";
            this.SecuredFilesTab.Padding = new System.Windows.Forms.Padding(3);
            this.SecuredFilesTab.Size = new System.Drawing.Size(1060, 372);
            this.SecuredFilesTab.TabIndex = 0;
            this.SecuredFilesTab.Text = "Secured Files";
            this.SecuredFilesTab.UseVisualStyleBackColor = true;
            // 
            // SecuredFilesGrid
            // 
            this.SecuredFilesGrid.AllowDrop = true;
            this.SecuredFilesGrid.AllowUserToAddRows = false;
            this.SecuredFilesGrid.AllowUserToDeleteRows = false;
            this.SecuredFilesGrid.AllowUserToOrderColumns = true;
            this.SecuredFilesGrid.AllowUserToResizeRows = false;
            this.SecuredFilesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SecuredFilesGrid.BackgroundColor = System.Drawing.Color.White;
            this.SecuredFilesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SecuredFilesGrid.ColumnHeadersHeight = 34;
            this.SecuredFilesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileColumnHeader,
            this.TimeColumnHeader,
            this.SecuredColumnHeader,
            this.AlgorithmColumnHeader});
            this.SecuredFilesGrid.ContextMenuStrip = this.FileMenuStrip;
            this.SecuredFilesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SecuredFilesGrid.GridColor = System.Drawing.Color.White;
            this.SecuredFilesGrid.Location = new System.Drawing.Point(3, 3);
            this.SecuredFilesGrid.Name = "SecuredFilesGrid";
            this.SecuredFilesGrid.ReadOnly = true;
            this.SecuredFilesGrid.RowHeadersVisible = false;
            this.SecuredFilesGrid.RowHeadersWidth = 62;
            this.SecuredFilesGrid.RowTemplate.ContextMenuStrip = this.FileMenuStrip;
            this.SecuredFilesGrid.RowTemplate.Height = 28;
            this.SecuredFilesGrid.RowTemplate.ReadOnly = true;
            this.SecuredFilesGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SecuredFilesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SecuredFilesGrid.Size = new System.Drawing.Size(1054, 366);
            this.SecuredFilesGrid.TabIndex = 1;
            // 
            // FileColumnHeader
            // 
            this.FileColumnHeader.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FileColumnHeader.DataPropertyName = "File";
            this.FileColumnHeader.HeaderText = "File";
            this.FileColumnHeader.MinimumWidth = 8;
            this.FileColumnHeader.Name = "FileColumnHeader";
            this.FileColumnHeader.ReadOnly = true;
            // 
            // TimeColumnHeader
            // 
            this.TimeColumnHeader.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TimeColumnHeader.DataPropertyName = "Time";
            this.TimeColumnHeader.HeaderText = "Time";
            this.TimeColumnHeader.MinimumWidth = 8;
            this.TimeColumnHeader.Name = "TimeColumnHeader";
            this.TimeColumnHeader.ReadOnly = true;
            // 
            // SecuredColumnHeader
            // 
            this.SecuredColumnHeader.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SecuredColumnHeader.DataPropertyName = "Secured";
            this.SecuredColumnHeader.HeaderText = "Secured";
            this.SecuredColumnHeader.MinimumWidth = 8;
            this.SecuredColumnHeader.Name = "SecuredColumnHeader";
            this.SecuredColumnHeader.ReadOnly = true;
            // 
            // AlgorithmColumnHeader
            // 
            this.AlgorithmColumnHeader.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AlgorithmColumnHeader.DataPropertyName = "Algorithm";
            this.AlgorithmColumnHeader.HeaderText = "Algorithm";
            this.AlgorithmColumnHeader.MinimumWidth = 8;
            this.AlgorithmColumnHeader.Name = "AlgorithmColumnHeader";
            this.AlgorithmColumnHeader.ReadOnly = true;
            // 
            // FileMenuStrip
            // 
            this.FileMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.FileMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.RemoveFileFromListButKeepSecuredToolStripMenuItem,
            this.StopSecuringAndRemoveFromListToolStripMenuItem,
            this.ShareKeysToolStripMenuItem1,
            this.ShowInFolderToolStripMenuItem,
            this.RenameToOriginalToolStripMenuItem,
            this.ClearSecuredFilesListToolStripMenuItem,
            this.AddSecuredFileToolStripMenuItem,
            this.ShowAllSecuredFiles,
            this.HideExcludedSecuredFilesToolStripMenuItem,
            this.AnonymizeFileNameToolStripMenuItem,
            this.DeanonymizeFileNameToolStripMenuItem});
            this.FileMenuStrip.Name = "contextMenuStrip1";
            this.FileMenuStrip.Size = new System.Drawing.Size(401, 388);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(400, 32);
            this.OpenToolStripMenuItem.Text = "Open";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // RemoveFileFromListButKeepSecuredToolStripMenuItem
            // 
            this.RemoveFileFromListButKeepSecuredToolStripMenuItem.Name = "RemoveFileFromListButKeepSecuredToolStripMenuItem";
            this.RemoveFileFromListButKeepSecuredToolStripMenuItem.Size = new System.Drawing.Size(400, 32);
            this.RemoveFileFromListButKeepSecuredToolStripMenuItem.Text = "Remove File From List But Keep Secured";
            this.RemoveFileFromListButKeepSecuredToolStripMenuItem.Click += new System.EventHandler(this.RemoveFileFromListButKeepSecuredToolStripMenuItem_Click);
            // 
            // StopSecuringAndRemoveFromListToolStripMenuItem
            // 
            this.StopSecuringAndRemoveFromListToolStripMenuItem.Name = "StopSecuringAndRemoveFromListToolStripMenuItem";
            this.StopSecuringAndRemoveFromListToolStripMenuItem.Size = new System.Drawing.Size(400, 32);
            this.StopSecuringAndRemoveFromListToolStripMenuItem.Text = "Stop Securing And Remove From List";
            this.StopSecuringAndRemoveFromListToolStripMenuItem.Click += new System.EventHandler(this.StopSecuringAndRemoveFromListToolStripMenuItem_Click);
            // 
            // ShareKeysToolStripMenuItem1
            // 
            this.ShareKeysToolStripMenuItem1.Name = "ShareKeysToolStripMenuItem1";
            this.ShareKeysToolStripMenuItem1.Size = new System.Drawing.Size(400, 32);
            this.ShareKeysToolStripMenuItem1.Text = "Share Keys";
            this.ShareKeysToolStripMenuItem1.Click += new System.EventHandler(this.ShareKeysToolStripMenuItem1_Click);
            // 
            // ShowInFolderToolStripMenuItem
            // 
            this.ShowInFolderToolStripMenuItem.Name = "ShowInFolderToolStripMenuItem";
            this.ShowInFolderToolStripMenuItem.Size = new System.Drawing.Size(400, 32);
            this.ShowInFolderToolStripMenuItem.Text = "Show In Folder";
            this.ShowInFolderToolStripMenuItem.Click += new System.EventHandler(this.ShowInFolderToolStripMenuItem_Click);
            // 
            // RenameToOriginalToolStripMenuItem
            // 
            this.RenameToOriginalToolStripMenuItem.Name = "RenameToOriginalToolStripMenuItem";
            this.RenameToOriginalToolStripMenuItem.Size = new System.Drawing.Size(400, 32);
            this.RenameToOriginalToolStripMenuItem.Text = "Rename To Original";
            this.RenameToOriginalToolStripMenuItem.Click += new System.EventHandler(this.RenameToOriginalToolStripMenuItem_Click);
            // 
            // ClearSecuredFilesListToolStripMenuItem
            // 
            this.ClearSecuredFilesListToolStripMenuItem.Name = "ClearSecuredFilesListToolStripMenuItem";
            this.ClearSecuredFilesListToolStripMenuItem.Size = new System.Drawing.Size(400, 32);
            this.ClearSecuredFilesListToolStripMenuItem.Text = "Clear Secured Files Grid";
            this.ClearSecuredFilesListToolStripMenuItem.Click += new System.EventHandler(this.ClearSecuredFilesListToolStripMenuItem_Click);
            // 
            // AddSecuredFileToolStripMenuItem
            // 
            this.AddSecuredFileToolStripMenuItem.Name = "AddSecuredFileToolStripMenuItem";
            this.AddSecuredFileToolStripMenuItem.Size = new System.Drawing.Size(400, 32);
            this.AddSecuredFileToolStripMenuItem.Text = "Add Secured File";
            this.AddSecuredFileToolStripMenuItem.Click += new System.EventHandler(this.AddSecuredFileToolStripMenuItem_Click);
            // 
            // ShowAllSecuredFiles
            // 
            this.ShowAllSecuredFiles.Name = "ShowAllSecuredFiles";
            this.ShowAllSecuredFiles.Size = new System.Drawing.Size(400, 32);
            this.ShowAllSecuredFiles.Text = "Show Excluded Secured Files";
            this.ShowAllSecuredFiles.Click += new System.EventHandler(this.ShowAllSecuredFiles_Click);
            // 
            // HideExcludedSecuredFilesToolStripMenuItem
            // 
            this.HideExcludedSecuredFilesToolStripMenuItem.Name = "HideExcludedSecuredFilesToolStripMenuItem";
            this.HideExcludedSecuredFilesToolStripMenuItem.Size = new System.Drawing.Size(400, 32);
            this.HideExcludedSecuredFilesToolStripMenuItem.Text = "Hide Excluded Secured Files";
            this.HideExcludedSecuredFilesToolStripMenuItem.Click += new System.EventHandler(this.HideExcludedSecuredFilesToolStripMenuItem_Click);
            // 
            // AnonymizeFileNameToolStripMenuItem
            // 
            this.AnonymizeFileNameToolStripMenuItem.Name = "AnonymizeFileNameToolStripMenuItem";
            this.AnonymizeFileNameToolStripMenuItem.Size = new System.Drawing.Size(400, 32);
            this.AnonymizeFileNameToolStripMenuItem.Text = "Anonymize File Name";
            this.AnonymizeFileNameToolStripMenuItem.Click += new System.EventHandler(this.AnonymizeFileNameToolStripMenuItem_Click);
            // 
            // DeanonymizeFileNameToolStripMenuItem
            // 
            this.DeanonymizeFileNameToolStripMenuItem.Name = "DeanonymizeFileNameToolStripMenuItem";
            this.DeanonymizeFileNameToolStripMenuItem.Size = new System.Drawing.Size(400, 32);
            this.DeanonymizeFileNameToolStripMenuItem.Text = "Deanonymize File Name";
            this.DeanonymizeFileNameToolStripMenuItem.Click += new System.EventHandler(this.DeanonymizeFileNameToolStripMenuItem_Click);
            // 
            // SecuredFoldersTab
            // 
            this.SecuredFoldersTab.Controls.Add(this.SecuredFoldersGrid);
            this.SecuredFoldersTab.Location = new System.Drawing.Point(4, 29);
            this.SecuredFoldersTab.Name = "SecuredFoldersTab";
            this.SecuredFoldersTab.Padding = new System.Windows.Forms.Padding(3);
            this.SecuredFoldersTab.Size = new System.Drawing.Size(1060, 372);
            this.SecuredFoldersTab.TabIndex = 1;
            this.SecuredFoldersTab.Text = "Secured Folders";
            this.SecuredFoldersTab.UseVisualStyleBackColor = true;
            // 
            // SecuredFoldersGrid
            // 
            this.SecuredFoldersGrid.AllowDrop = true;
            this.SecuredFoldersGrid.AllowUserToAddRows = false;
            this.SecuredFoldersGrid.AllowUserToDeleteRows = false;
            this.SecuredFoldersGrid.AllowUserToOrderColumns = true;
            this.SecuredFoldersGrid.AllowUserToResizeRows = false;
            this.SecuredFoldersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SecuredFoldersGrid.BackgroundColor = System.Drawing.Color.White;
            this.SecuredFoldersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SecuredFoldersGrid.ColumnHeadersHeight = 34;
            this.SecuredFoldersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Folder,
            this.Time});
            this.SecuredFoldersGrid.ContextMenuStrip = this.FolderMenuStrip;
            this.SecuredFoldersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SecuredFoldersGrid.GridColor = System.Drawing.Color.White;
            this.SecuredFoldersGrid.Location = new System.Drawing.Point(3, 3);
            this.SecuredFoldersGrid.Name = "SecuredFoldersGrid";
            this.SecuredFoldersGrid.ReadOnly = true;
            this.SecuredFoldersGrid.RowHeadersVisible = false;
            this.SecuredFoldersGrid.RowHeadersWidth = 62;
            this.SecuredFoldersGrid.RowTemplate.ContextMenuStrip = this.FolderMenuStrip;
            this.SecuredFoldersGrid.RowTemplate.Height = 28;
            this.SecuredFoldersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SecuredFoldersGrid.Size = new System.Drawing.Size(1054, 366);
            this.SecuredFoldersGrid.TabIndex = 0;
            // 
            // Folder
            // 
            this.Folder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Folder.DataPropertyName = "URI";
            this.Folder.HeaderText = "Folder";
            this.Folder.MinimumWidth = 8;
            this.Folder.Name = "Folder";
            this.Folder.ReadOnly = true;
            // 
            // Time
            // 
            this.Time.DataPropertyName = "Time";
            this.Time.HeaderText = "Time";
            this.Time.MinimumWidth = 8;
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // FolderMenuStrip
            // 
            this.FolderMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.FolderMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShareKeysToolStripMenuItem,
            this.DecryptPermanentlyToolStripMenuItem,
            this.DecryptTemporarilyToolStripMenuItem,
            this.RemoveFolderFromListToolStripMenuItem,
            this.OpenExplorerHereToolStripMenuItem,
            this.ClearSecuredFoldersGridToolStripMenuItem,
            this.AddSecuredFolderToolStripMenuItem,
            this.ShowAllSecuredFolders,
            this.HideExcludedSecuredFoldersToolStripMenuItem,
            this.BulkAnonymousRenameToolStripMenuItem,
            this.BulkDeanonymizeFileNamesToolStripMenuItem});
            this.FolderMenuStrip.Name = "folderMenuStrip";
            this.FolderMenuStrip.Size = new System.Drawing.Size(425, 356);
            // 
            // ShareKeysToolStripMenuItem
            // 
            this.ShareKeysToolStripMenuItem.Name = "ShareKeysToolStripMenuItem";
            this.ShareKeysToolStripMenuItem.Size = new System.Drawing.Size(424, 32);
            this.ShareKeysToolStripMenuItem.Text = "Share Keys";
            this.ShareKeysToolStripMenuItem.Click += new System.EventHandler(this.ShareKeysToolStripMenuItem_Click);
            // 
            // DecryptPermanentlyToolStripMenuItem
            // 
            this.DecryptPermanentlyToolStripMenuItem.Name = "DecryptPermanentlyToolStripMenuItem";
            this.DecryptPermanentlyToolStripMenuItem.Size = new System.Drawing.Size(424, 32);
            this.DecryptPermanentlyToolStripMenuItem.Text = "Decrypt Permanently";
            this.DecryptPermanentlyToolStripMenuItem.Click += new System.EventHandler(this.DecryptPermanentlyToolStripMenuItem_Click);
            // 
            // DecryptTemporarilyToolStripMenuItem
            // 
            this.DecryptTemporarilyToolStripMenuItem.Name = "DecryptTemporarilyToolStripMenuItem";
            this.DecryptTemporarilyToolStripMenuItem.Size = new System.Drawing.Size(424, 32);
            this.DecryptTemporarilyToolStripMenuItem.Text = "Decrypt Temporarily";
            this.DecryptTemporarilyToolStripMenuItem.Click += new System.EventHandler(this.DecryptTemporarilyToolStripMenuItem_Click);
            // 
            // RemoveFolderFromListToolStripMenuItem
            // 
            this.RemoveFolderFromListToolStripMenuItem.Name = "RemoveFolderFromListToolStripMenuItem";
            this.RemoveFolderFromListToolStripMenuItem.Size = new System.Drawing.Size(424, 32);
            this.RemoveFolderFromListToolStripMenuItem.Text = "Remove Folder From List But Keep Secured";
            this.RemoveFolderFromListToolStripMenuItem.Click += new System.EventHandler(this.RemoveFolderFromListToolStripMenuItem_Click);
            // 
            // OpenExplorerHereToolStripMenuItem
            // 
            this.OpenExplorerHereToolStripMenuItem.Name = "OpenExplorerHereToolStripMenuItem";
            this.OpenExplorerHereToolStripMenuItem.Size = new System.Drawing.Size(424, 32);
            this.OpenExplorerHereToolStripMenuItem.Text = "Open Explorer Here";
            this.OpenExplorerHereToolStripMenuItem.Click += new System.EventHandler(this.OpenExplorerHereToolStripMenuItem_Click);
            // 
            // ClearSecuredFoldersGridToolStripMenuItem
            // 
            this.ClearSecuredFoldersGridToolStripMenuItem.Name = "ClearSecuredFoldersGridToolStripMenuItem";
            this.ClearSecuredFoldersGridToolStripMenuItem.Size = new System.Drawing.Size(424, 32);
            this.ClearSecuredFoldersGridToolStripMenuItem.Text = "Clear Secured Folders Grid";
            this.ClearSecuredFoldersGridToolStripMenuItem.Click += new System.EventHandler(this.ClearSecuredFoldersGridToolStripMenuItem_Click);
            // 
            // AddSecuredFolderToolStripMenuItem
            // 
            this.AddSecuredFolderToolStripMenuItem.Name = "AddSecuredFolderToolStripMenuItem";
            this.AddSecuredFolderToolStripMenuItem.Size = new System.Drawing.Size(424, 32);
            this.AddSecuredFolderToolStripMenuItem.Text = "Add Secured Folder";
            this.AddSecuredFolderToolStripMenuItem.Click += new System.EventHandler(this.AddSecuredFolderToolStripMenuItem_Click);
            // 
            // ShowAllSecuredFolders
            // 
            this.ShowAllSecuredFolders.Name = "ShowAllSecuredFolders";
            this.ShowAllSecuredFolders.Size = new System.Drawing.Size(424, 32);
            this.ShowAllSecuredFolders.Text = "Show Excluded Secured Folders";
            this.ShowAllSecuredFolders.Click += new System.EventHandler(this.ShowAllSecuredFolders_Click);
            // 
            // HideExcludedSecuredFoldersToolStripMenuItem
            // 
            this.HideExcludedSecuredFoldersToolStripMenuItem.Name = "HideExcludedSecuredFoldersToolStripMenuItem";
            this.HideExcludedSecuredFoldersToolStripMenuItem.Size = new System.Drawing.Size(424, 32);
            this.HideExcludedSecuredFoldersToolStripMenuItem.Text = "Hide Excluded Secured Folders";
            this.HideExcludedSecuredFoldersToolStripMenuItem.Click += new System.EventHandler(this.HideExcludedSecuredFoldersToolStripMenuItem_Click);
            // 
            // BulkAnonymousRenameToolStripMenuItem
            // 
            this.BulkAnonymousRenameToolStripMenuItem.Name = "BulkAnonymousRenameToolStripMenuItem";
            this.BulkAnonymousRenameToolStripMenuItem.Size = new System.Drawing.Size(424, 32);
            this.BulkAnonymousRenameToolStripMenuItem.Text = "Bulk Anonymize File Names";
            this.BulkAnonymousRenameToolStripMenuItem.Click += new System.EventHandler(this.BulkAnonymousRenameToolStripMenuItem_Click);
            // 
            // BulkDeanonymizeFileNamesToolStripMenuItem
            // 
            this.BulkDeanonymizeFileNamesToolStripMenuItem.Name = "BulkDeanonymizeFileNamesToolStripMenuItem";
            this.BulkDeanonymizeFileNamesToolStripMenuItem.Size = new System.Drawing.Size(424, 32);
            this.BulkDeanonymizeFileNamesToolStripMenuItem.Text = "Bulk Deanonymize File Names";
            this.BulkDeanonymizeFileNamesToolStripMenuItem.Click += new System.EventHandler(this.BulkDeanonymizeFileNamesToolStripMenuItem_Click);
            // 
            // MenuStrip
            // 
            this.MenuStrip.BackColor = System.Drawing.Color.Transparent;
            this.MenuStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.MenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuStripItem,
            this.DebugMenuStrip,
            this.DiskToolsToolStripMenuItem,
            this.AdvancedMenuStrip,
            this.HelpMenuStrip});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(1092, 33);
            this.MenuStrip.TabIndex = 2;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // FileMenuStripItem
            // 
            this.FileMenuStripItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenSecuredToolStripMenuItem,
            this.SecureToolStripMenuItem,
            this.StopSecuringToolStripMenuItem,
            this.SecuredFoldersToolStripMenuItem,
            this.AnonymousRenameToolStripMenuItem,
            this.DeanonymiseFileToolStripMenuItem,
            this.AddSecuredFolderToolStripMenuItem1,
            this.toolStripSeparator1,
            this.SecureDeleteFile,
            this.SecureDeleteFolder,
            this.toolStripSeparator2,
            this.Advanced,
            this.OptionsToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.FileMenuStripItem.Name = "FileMenuStripItem";
            this.FileMenuStripItem.Size = new System.Drawing.Size(54, 29);
            this.FileMenuStripItem.Text = "File";
            // 
            // OpenSecuredToolStripMenuItem
            // 
            this.OpenSecuredToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OpenSecuredToolStripMenuItem.Image")));
            this.OpenSecuredToolStripMenuItem.Name = "OpenSecuredToolStripMenuItem";
            this.OpenSecuredToolStripMenuItem.Size = new System.Drawing.Size(305, 34);
            this.OpenSecuredToolStripMenuItem.Text = "Open Secured";
            this.OpenSecuredToolStripMenuItem.Click += new System.EventHandler(this.OpenSecuredToolStripMenuItem_Click);
            // 
            // SecureToolStripMenuItem
            // 
            this.SecureToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SecureToolStripMenuItem.Image")));
            this.SecureToolStripMenuItem.Name = "SecureToolStripMenuItem";
            this.SecureToolStripMenuItem.Size = new System.Drawing.Size(305, 34);
            this.SecureToolStripMenuItem.Text = "Secure";
            this.SecureToolStripMenuItem.Click += new System.EventHandler(this.SecureToolStripMenuItem_Click);
            // 
            // StopSecuringToolStripMenuItem
            // 
            this.StopSecuringToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("StopSecuringToolStripMenuItem.Image")));
            this.StopSecuringToolStripMenuItem.Name = "StopSecuringToolStripMenuItem";
            this.StopSecuringToolStripMenuItem.Size = new System.Drawing.Size(305, 34);
            this.StopSecuringToolStripMenuItem.Text = "Stop Securing";
            this.StopSecuringToolStripMenuItem.Click += new System.EventHandler(this.StopSecuringToolStripMenuItem_Click);
            // 
            // SecuredFoldersToolStripMenuItem
            // 
            this.SecuredFoldersToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SecuredFoldersToolStripMenuItem.Image")));
            this.SecuredFoldersToolStripMenuItem.Name = "SecuredFoldersToolStripMenuItem";
            this.SecuredFoldersToolStripMenuItem.Size = new System.Drawing.Size(305, 34);
            this.SecuredFoldersToolStripMenuItem.Text = "Secured Folders";
            this.SecuredFoldersToolStripMenuItem.Click += new System.EventHandler(this.SecuredFoldersToolStripMenuItem_Click);
            // 
            // AnonymousRenameToolStripMenuItem
            // 
            this.AnonymousRenameToolStripMenuItem.Name = "AnonymousRenameToolStripMenuItem";
            this.AnonymousRenameToolStripMenuItem.Size = new System.Drawing.Size(305, 34);
            this.AnonymousRenameToolStripMenuItem.Text = "Anonymize File Name";
            this.AnonymousRenameToolStripMenuItem.Click += new System.EventHandler(this.AnonymousRenameToolStripMenuItem_Click);
            // 
            // DeanonymiseFileToolStripMenuItem
            // 
            this.DeanonymiseFileToolStripMenuItem.Name = "DeanonymiseFileToolStripMenuItem";
            this.DeanonymiseFileToolStripMenuItem.Size = new System.Drawing.Size(305, 34);
            this.DeanonymiseFileToolStripMenuItem.Text = "Deanonymize File Name";
            this.DeanonymiseFileToolStripMenuItem.Click += new System.EventHandler(this.RenameToOriginalToolStripMenuItem1_Click);
            // 
            // AddSecuredFolderToolStripMenuItem1
            // 
            this.AddSecuredFolderToolStripMenuItem1.Name = "AddSecuredFolderToolStripMenuItem1";
            this.AddSecuredFolderToolStripMenuItem1.Size = new System.Drawing.Size(305, 34);
            this.AddSecuredFolderToolStripMenuItem1.Text = "Add Secured Folder";
            this.AddSecuredFolderToolStripMenuItem1.Click += new System.EventHandler(this.AddSecuredFolderToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(302, 6);
            // 
            // SecureDeleteFile
            // 
            this.SecureDeleteFile.Image = ((System.Drawing.Image)(resources.GetObject("SecureDeleteFile.Image")));
            this.SecureDeleteFile.Name = "SecureDeleteFile";
            this.SecureDeleteFile.Size = new System.Drawing.Size(305, 34);
            this.SecureDeleteFile.Text = "Secure Delete File";
            this.SecureDeleteFile.Click += new System.EventHandler(this.SecureDeleteToolStripMenuItem_Click);
            // 
            // SecureDeleteFolder
            // 
            this.SecureDeleteFolder.Image = ((System.Drawing.Image)(resources.GetObject("SecureDeleteFolder.Image")));
            this.SecureDeleteFolder.Name = "SecureDeleteFolder";
            this.SecureDeleteFolder.Size = new System.Drawing.Size(305, 34);
            this.SecureDeleteFolder.Text = "Secure Delete Folder";
            this.SecureDeleteFolder.Click += new System.EventHandler(this.SecureDeleteFolder_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(302, 6);
            // 
            // Advanced
            // 
            this.Advanced.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OneTimePadToolStripMenuItem});
            this.Advanced.Name = "Advanced";
            this.Advanced.Size = new System.Drawing.Size(305, 34);
            this.Advanced.Text = "Advanced";
            // 
            // OneTimePadToolStripMenuItem
            // 
            this.OneTimePadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SecureFileToolStripMenuItem,
            this.DecryptFileToolStripMenuItem,
            this.GenerateKeyForFileToolStripMenuItem});
            this.OneTimePadToolStripMenuItem.Name = "OneTimePadToolStripMenuItem";
            this.OneTimePadToolStripMenuItem.Size = new System.Drawing.Size(224, 34);
            this.OneTimePadToolStripMenuItem.Text = "One Time Pad";
            // 
            // SecureFileToolStripMenuItem
            // 
            this.SecureFileToolStripMenuItem.Name = "SecureFileToolStripMenuItem";
            this.SecureFileToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.SecureFileToolStripMenuItem.Text = "Secure File";
            this.SecureFileToolStripMenuItem.Click += new System.EventHandler(this.SecureFileToolStripMenuItem_Click);
            // 
            // DecryptFileToolStripMenuItem
            // 
            this.DecryptFileToolStripMenuItem.Name = "DecryptFileToolStripMenuItem";
            this.DecryptFileToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.DecryptFileToolStripMenuItem.Text = "Decrypt File";
            this.DecryptFileToolStripMenuItem.Click += new System.EventHandler(this.DecryptFileToolStripMenuItem_Click);
            // 
            // GenerateKeyForFileToolStripMenuItem
            // 
            this.GenerateKeyForFileToolStripMenuItem.Name = "GenerateKeyForFileToolStripMenuItem";
            this.GenerateKeyForFileToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.GenerateKeyForFileToolStripMenuItem.Text = "Generate Key For File";
            this.GenerateKeyForFileToolStripMenuItem.Click += new System.EventHandler(this.GenerateKeyForFileToolStripMenuItem_Click);
            // 
            // OptionsToolStripMenuItem
            // 
            this.OptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LanguageToolStripMenuItem,
            this.KeyManagementToolStripMenuItem,
            this.PasswordManagementToolStripMenuItem,
            this.ChangeSessionPasswordToolStripMenuItem,
            this.ClearSessionPassword,
            this.ResetAllSettingsToolStripMenuItem,
            this.toolStripSeparator3,
            this.Debug,
            this.IncludeSubfolders,
            this.DoNotPromptForPasswordOnStartup,
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem,
            this.UseADifferentPasswordForEachFile,
            this.ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem});
            this.OptionsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OptionsToolStripMenuItem.Image")));
            this.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
            this.OptionsToolStripMenuItem.Size = new System.Drawing.Size(305, 34);
            this.OptionsToolStripMenuItem.Text = "Options";
            // 
            // LanguageToolStripMenuItem
            // 
            this.LanguageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnglishToolStripMenuItem,
            this.DeutschGermanToolStripMenuItem,
            this.NetherlandsDutchToolStripMenuItem,
            this.FrancaisFrenchToolStripMenuItem,
            this.ItalianoItalianToolStripMenuItem,
            this.KoreanToolStripMenuItem,
            this.PolskiPolishToolStripMenuItem,
            this.PortugueseToolStripMenuItem,
            this.RussianToolStripMenuItem,
            this.SwedishToolStripMenuItem,
            this.TurkishToolStripMenuItem});
            this.LanguageToolStripMenuItem.Name = "LanguageToolStripMenuItem";
            this.LanguageToolStripMenuItem.Size = new System.Drawing.Size(559, 34);
            this.LanguageToolStripMenuItem.Text = "Language";
            // 
            // EnglishToolStripMenuItem
            // 
            this.EnglishToolStripMenuItem.Name = "EnglishToolStripMenuItem";
            this.EnglishToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.EnglishToolStripMenuItem.Text = "English";
            this.EnglishToolStripMenuItem.Click += new System.EventHandler(this.EnglishToolStripMenuItem_Click);
            // 
            // DeutschGermanToolStripMenuItem
            // 
            this.DeutschGermanToolStripMenuItem.Name = "DeutschGermanToolStripMenuItem";
            this.DeutschGermanToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.DeutschGermanToolStripMenuItem.Text = "Deutsch (German)";
            this.DeutschGermanToolStripMenuItem.Click += new System.EventHandler(this.DeutschGermanToolStripMenuItem_Click);
            // 
            // NetherlandsDutchToolStripMenuItem
            // 
            this.NetherlandsDutchToolStripMenuItem.Name = "NetherlandsDutchToolStripMenuItem";
            this.NetherlandsDutchToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.NetherlandsDutchToolStripMenuItem.Text = "Netherlands (Dutch)";
            this.NetherlandsDutchToolStripMenuItem.Click += new System.EventHandler(this.NetherlandsDutchToolStripMenuItem_Click);
            // 
            // FrancaisFrenchToolStripMenuItem
            // 
            this.FrancaisFrenchToolStripMenuItem.Name = "FrancaisFrenchToolStripMenuItem";
            this.FrancaisFrenchToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.FrancaisFrenchToolStripMenuItem.Text = "Français (French)";
            this.FrancaisFrenchToolStripMenuItem.Click += new System.EventHandler(this.FrancaisFrenchToolStripMenuItem_Click);
            // 
            // ItalianoItalianToolStripMenuItem
            // 
            this.ItalianoItalianToolStripMenuItem.Name = "ItalianoItalianToolStripMenuItem";
            this.ItalianoItalianToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.ItalianoItalianToolStripMenuItem.Text = "Italiano (Italian)";
            this.ItalianoItalianToolStripMenuItem.Click += new System.EventHandler(this.ItalianoItalianToolStripMenuItem_Click);
            // 
            // KoreanToolStripMenuItem
            // 
            this.KoreanToolStripMenuItem.Name = "KoreanToolStripMenuItem";
            this.KoreanToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.KoreanToolStripMenuItem.Text = "한국어 (Korean)";
            this.KoreanToolStripMenuItem.Click += new System.EventHandler(this.KoreanToolStripMenuItem_Click);
            // 
            // PolskiPolishToolStripMenuItem
            // 
            this.PolskiPolishToolStripMenuItem.Name = "PolskiPolishToolStripMenuItem";
            this.PolskiPolishToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.PolskiPolishToolStripMenuItem.Text = "Polski (Polish)";
            this.PolskiPolishToolStripMenuItem.Click += new System.EventHandler(this.PolskiPolishToolStripMenuItem_Click);
            // 
            // PortugueseToolStripMenuItem
            // 
            this.PortugueseToolStripMenuItem.Name = "PortugueseToolStripMenuItem";
            this.PortugueseToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.PortugueseToolStripMenuItem.Text = "Português (Portuguese)";
            this.PortugueseToolStripMenuItem.Click += new System.EventHandler(this.PortugueseToolStripMenuItem_Click);
            // 
            // RussianToolStripMenuItem
            // 
            this.RussianToolStripMenuItem.Name = "RussianToolStripMenuItem";
            this.RussianToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.RussianToolStripMenuItem.Text = "русский (Russian)";
            this.RussianToolStripMenuItem.Click += new System.EventHandler(this.RussianToolStripMenuItem_Click);
            // 
            // SwedishToolStripMenuItem
            // 
            this.SwedishToolStripMenuItem.Name = "SwedishToolStripMenuItem";
            this.SwedishToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.SwedishToolStripMenuItem.Text = "Svenska (Swedish)";
            this.SwedishToolStripMenuItem.Click += new System.EventHandler(this.SwedishToolStripMenuItem_Click);
            // 
            // TurkishToolStripMenuItem
            // 
            this.TurkishToolStripMenuItem.Name = "TurkishToolStripMenuItem";
            this.TurkishToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.TurkishToolStripMenuItem.Text = "Türkçe (Turkish)";
            this.TurkishToolStripMenuItem.Click += new System.EventHandler(this.TurkishToolStripMenuItem_Click);
            // 
            // KeyManagementToolStripMenuItem
            // 
            this.KeyManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportPublicKeyToolStripMenuItem,
            this.ExportMyPublicKeyToolStripMenuItem,
            this.GenerateNewKeyPairToolStripMenuItem});
            this.KeyManagementToolStripMenuItem.Name = "KeyManagementToolStripMenuItem";
            this.KeyManagementToolStripMenuItem.Size = new System.Drawing.Size(559, 34);
            this.KeyManagementToolStripMenuItem.Text = "Key Management";
            // 
            // ImportPublicKeyToolStripMenuItem
            // 
            this.ImportPublicKeyToolStripMenuItem.Name = "ImportPublicKeyToolStripMenuItem";
            this.ImportPublicKeyToolStripMenuItem.Size = new System.Drawing.Size(347, 34);
            this.ImportPublicKeyToolStripMenuItem.Text = "Import Someone\'s Public Key";
            this.ImportPublicKeyToolStripMenuItem.Click += new System.EventHandler(this.ImportPublicKeyToolStripMenuItem_Click);
            // 
            // ExportMyPublicKeyToolStripMenuItem
            // 
            this.ExportMyPublicKeyToolStripMenuItem.Name = "ExportMyPublicKeyToolStripMenuItem";
            this.ExportMyPublicKeyToolStripMenuItem.Size = new System.Drawing.Size(347, 34);
            this.ExportMyPublicKeyToolStripMenuItem.Text = "Export My Public Key";
            this.ExportMyPublicKeyToolStripMenuItem.Click += new System.EventHandler(this.ExportMyPublicKeyToolStripMenuItem_Click);
            // 
            // GenerateNewKeyPairToolStripMenuItem
            // 
            this.GenerateNewKeyPairToolStripMenuItem.Name = "GenerateNewKeyPairToolStripMenuItem";
            this.GenerateNewKeyPairToolStripMenuItem.Size = new System.Drawing.Size(347, 34);
            this.GenerateNewKeyPairToolStripMenuItem.Text = "Generate New Key Pair";
            this.GenerateNewKeyPairToolStripMenuItem.Click += new System.EventHandler(this.GenerateNewKeyPairToolStripMenuItem_Click);
            // 
            // PasswordManagementToolStripMenuItem
            // 
            this.PasswordManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenPasswordManagerToolStripMenuItem,
            this.ManagementStoreToolStripMenuItem});
            this.PasswordManagementToolStripMenuItem.Name = "PasswordManagementToolStripMenuItem";
            this.PasswordManagementToolStripMenuItem.Size = new System.Drawing.Size(559, 34);
            this.PasswordManagementToolStripMenuItem.Text = "Password Management";
            // 
            // OpenPasswordManagerToolStripMenuItem
            // 
            this.OpenPasswordManagerToolStripMenuItem.Name = "OpenPasswordManagerToolStripMenuItem";
            this.OpenPasswordManagerToolStripMenuItem.Size = new System.Drawing.Size(345, 34);
            this.OpenPasswordManagerToolStripMenuItem.Text = "Open Password Manager";
            this.OpenPasswordManagerToolStripMenuItem.Click += new System.EventHandler(this.OpenPasswordManagerToolStripMenuItem_Click);
            // 
            // ManagementStoreToolStripMenuItem
            // 
            this.ManagementStoreToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AESPasswordBasedToolStripMenuItem,
            this.OTPKeyBasedToolStripMenuItem});
            this.ManagementStoreToolStripMenuItem.Name = "ManagementStoreToolStripMenuItem";
            this.ManagementStoreToolStripMenuItem.Size = new System.Drawing.Size(345, 34);
            this.ManagementStoreToolStripMenuItem.Text = "Password Management Store";
            // 
            // AESPasswordBasedToolStripMenuItem
            // 
            this.AESPasswordBasedToolStripMenuItem.CheckOnClick = true;
            this.AESPasswordBasedToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.AESPasswordBasedToolStripMenuItem.Name = "AESPasswordBasedToolStripMenuItem";
            this.AESPasswordBasedToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.AESPasswordBasedToolStripMenuItem.Text = "AES Password-Based";
            this.AESPasswordBasedToolStripMenuItem.Click += new System.EventHandler(this.AESPasswordBasedToolStripMenuItem_Click);
            // 
            // OTPKeyBasedToolStripMenuItem
            // 
            this.OTPKeyBasedToolStripMenuItem.CheckOnClick = true;
            this.OTPKeyBasedToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.OTPKeyBasedToolStripMenuItem.Name = "OTPKeyBasedToolStripMenuItem";
            this.OTPKeyBasedToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.OTPKeyBasedToolStripMenuItem.Text = "OTP Key-Based";
            this.OTPKeyBasedToolStripMenuItem.Click += new System.EventHandler(this.OTPKeyBasedToolStripMenuItem_Click);
            // 
            // ChangeSessionPasswordToolStripMenuItem
            // 
            this.ChangeSessionPasswordToolStripMenuItem.Name = "ChangeSessionPasswordToolStripMenuItem";
            this.ChangeSessionPasswordToolStripMenuItem.Size = new System.Drawing.Size(559, 34);
            this.ChangeSessionPasswordToolStripMenuItem.Text = "Change Session Password";
            this.ChangeSessionPasswordToolStripMenuItem.Click += new System.EventHandler(this.ChangeSessionPasswordToolStripMenuItem_Click);
            // 
            // ClearSessionPassword
            // 
            this.ClearSessionPassword.Name = "ClearSessionPassword";
            this.ClearSessionPassword.Size = new System.Drawing.Size(559, 34);
            this.ClearSessionPassword.Text = "Clear Session Password";
            this.ClearSessionPassword.Click += new System.EventHandler(this.ClearSessionPassword_Click);
            // 
            // ResetAllSettingsToolStripMenuItem
            // 
            this.ResetAllSettingsToolStripMenuItem.Name = "ResetAllSettingsToolStripMenuItem";
            this.ResetAllSettingsToolStripMenuItem.Size = new System.Drawing.Size(559, 34);
            this.ResetAllSettingsToolStripMenuItem.Text = "Reset All Settings";
            this.ResetAllSettingsToolStripMenuItem.Click += new System.EventHandler(this.ResetAllSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(556, 6);
            // 
            // Debug
            // 
            this.Debug.CheckOnClick = true;
            this.Debug.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Debug.Name = "Debug";
            this.Debug.Size = new System.Drawing.Size(559, 34);
            this.Debug.Text = "Debug";
            this.Debug.Click += new System.EventHandler(this.DebugToolStripMenuItem_Click);
            // 
            // IncludeSubfolders
            // 
            this.IncludeSubfolders.CheckOnClick = true;
            this.IncludeSubfolders.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.IncludeSubfolders.Name = "IncludeSubfolders";
            this.IncludeSubfolders.Size = new System.Drawing.Size(559, 34);
            this.IncludeSubfolders.Text = "Include Subfolders";
            this.IncludeSubfolders.Click += new System.EventHandler(this.IncludeToolStripMenuItem_Click);
            // 
            // DoNotPromptForPasswordOnStartup
            // 
            this.DoNotPromptForPasswordOnStartup.CheckOnClick = true;
            this.DoNotPromptForPasswordOnStartup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DoNotPromptForPasswordOnStartup.Name = "DoNotPromptForPasswordOnStartup";
            this.DoNotPromptForPasswordOnStartup.Size = new System.Drawing.Size(559, 34);
            this.DoNotPromptForPasswordOnStartup.Text = "Do Not Prompt For Password On Startup";
            this.DoNotPromptForPasswordOnStartup.Click += new System.EventHandler(this.DoNotPromptForPasswordOnStartup_Click);
            // 
            // WipeDiskSpaceAfterSecureDeleteToolStripMenuItem
            // 
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.CheckOnClick = true;
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Name = "WipeDiskSpaceAfterSecureDeleteToolStripMenuItem";
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Size = new System.Drawing.Size(559, 34);
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Text = "Wipe Free Disk Space After A Secure Delete";
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Click += new System.EventHandler(this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem_Click);
            // 
            // UseADifferentPasswordForEachFile
            // 
            this.UseADifferentPasswordForEachFile.CheckOnClick = true;
            this.UseADifferentPasswordForEachFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.UseADifferentPasswordForEachFile.Name = "UseADifferentPasswordForEachFile";
            this.UseADifferentPasswordForEachFile.Size = new System.Drawing.Size(559, 34);
            this.UseADifferentPasswordForEachFile.Text = "Use A Different Password For Each File";
            this.UseADifferentPasswordForEachFile.Click += new System.EventHandler(this.UseADifferentPasswordForEachFileToolStripMenuItem_Click);
            // 
            // ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem
            // 
            this.ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem.CheckOnClick = true;
            this.ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem.Name = "ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem";
            this.ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem.Size = new System.Drawing.Size(559, 34);
            this.ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem.Text = "Re-encrypt Temporarily Decrypted Files On Lock/Log-off";
            this.ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem.Click += new System.EventHandler(this.ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ExitToolStripMenuItem.Image")));
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(305, 34);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // DebugMenuStrip
            // 
            this.DebugMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ValidateContainerToolStripMenuItem,
            this.ViewActiveJobsToolStripMenuItem,
            this.ViewCompletedJobsToolStripMenuItem,
            this.LoggingToolStripMenuItem,
            this.CancelAllFutureTasksToolStripMenuItem,
            this.DeleteGridExclusionLists,
            this.ViewLogToolStripMenuItem});
            this.DebugMenuStrip.Enabled = false;
            this.DebugMenuStrip.Name = "DebugMenuStrip";
            this.DebugMenuStrip.Size = new System.Drawing.Size(82, 29);
            this.DebugMenuStrip.Text = "Debug";
            // 
            // ValidateContainerToolStripMenuItem
            // 
            this.ValidateContainerToolStripMenuItem.Name = "ValidateContainerToolStripMenuItem";
            this.ValidateContainerToolStripMenuItem.Size = new System.Drawing.Size(318, 34);
            this.ValidateContainerToolStripMenuItem.Text = "Validate Container";
            this.ValidateContainerToolStripMenuItem.Click += new System.EventHandler(this.ValidateContainerToolStripMenuItem_Click);
            // 
            // ViewActiveJobsToolStripMenuItem
            // 
            this.ViewActiveJobsToolStripMenuItem.Name = "ViewActiveJobsToolStripMenuItem";
            this.ViewActiveJobsToolStripMenuItem.Size = new System.Drawing.Size(318, 34);
            this.ViewActiveJobsToolStripMenuItem.Text = "View Active Jobs";
            this.ViewActiveJobsToolStripMenuItem.Click += new System.EventHandler(this.ViewActiveJobsToolStripMenuItem_Click);
            // 
            // ViewCompletedJobsToolStripMenuItem
            // 
            this.ViewCompletedJobsToolStripMenuItem.Name = "ViewCompletedJobsToolStripMenuItem";
            this.ViewCompletedJobsToolStripMenuItem.Size = new System.Drawing.Size(318, 34);
            this.ViewCompletedJobsToolStripMenuItem.Text = "View Completed Jobs";
            this.ViewCompletedJobsToolStripMenuItem.Click += new System.EventHandler(this.ViewCompletedJobsToolStripMenuItem_Click);
            // 
            // LoggingToolStripMenuItem
            // 
            this.LoggingToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LoggingToolStripMenuItem.Name = "LoggingToolStripMenuItem";
            this.LoggingToolStripMenuItem.Size = new System.Drawing.Size(318, 34);
            this.LoggingToolStripMenuItem.Text = "Logging";
            this.LoggingToolStripMenuItem.Click += new System.EventHandler(this.LoggingToolStripMenuItem_Click);
            // 
            // CancelAllFutureTasksToolStripMenuItem
            // 
            this.CancelAllFutureTasksToolStripMenuItem.Name = "CancelAllFutureTasksToolStripMenuItem";
            this.CancelAllFutureTasksToolStripMenuItem.Size = new System.Drawing.Size(318, 34);
            this.CancelAllFutureTasksToolStripMenuItem.Text = "Cancel All Future Tasks";
            this.CancelAllFutureTasksToolStripMenuItem.Click += new System.EventHandler(this.CancelAllFutureTasksToolStripMenuItem_Click);
            // 
            // DeleteGridExclusionLists
            // 
            this.DeleteGridExclusionLists.Name = "DeleteGridExclusionLists";
            this.DeleteGridExclusionLists.Size = new System.Drawing.Size(318, 34);
            this.DeleteGridExclusionLists.Text = "Delete Grid Exclusion Lists";
            this.DeleteGridExclusionLists.Click += new System.EventHandler(this.DeleteGridExclusionList_Click);
            // 
            // ViewLogToolStripMenuItem
            // 
            this.ViewLogToolStripMenuItem.Name = "ViewLogToolStripMenuItem";
            this.ViewLogToolStripMenuItem.Size = new System.Drawing.Size(318, 34);
            this.ViewLogToolStripMenuItem.Text = "View Log";
            this.ViewLogToolStripMenuItem.Click += new System.EventHandler(this.ViewLogToolStripMenuItem_Click);
            // 
            // DiskToolsToolStripMenuItem
            // 
            this.DiskToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WipeFreeDiskSpaceToolStripMenuItem});
            this.DiskToolsToolStripMenuItem.Name = "DiskToolsToolStripMenuItem";
            this.DiskToolsToolStripMenuItem.Size = new System.Drawing.Size(108, 29);
            this.DiskToolsToolStripMenuItem.Text = "Disk Tools";
            // 
            // WipeFreeDiskSpaceToolStripMenuItem
            // 
            this.WipeFreeDiskSpaceToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("WipeFreeDiskSpaceToolStripMenuItem.Image")));
            this.WipeFreeDiskSpaceToolStripMenuItem.Name = "WipeFreeDiskSpaceToolStripMenuItem";
            this.WipeFreeDiskSpaceToolStripMenuItem.Size = new System.Drawing.Size(284, 34);
            this.WipeFreeDiskSpaceToolStripMenuItem.Text = "Wipe Free Disk Space";
            this.WipeFreeDiskSpaceToolStripMenuItem.Click += new System.EventHandler(this.WipeFreeDiskSpaceToolStripMenuItem_Click);
            // 
            // AdvancedMenuStrip
            // 
            this.AdvancedMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ForceExitToolStripMenuItem,
            this.IndividualSettingsResetToolStripMenuItem});
            this.AdvancedMenuStrip.Name = "AdvancedMenuStrip";
            this.AdvancedMenuStrip.Size = new System.Drawing.Size(107, 29);
            this.AdvancedMenuStrip.Text = "Advanced";
            // 
            // ForceExitToolStripMenuItem
            // 
            this.ForceExitToolStripMenuItem.Name = "ForceExitToolStripMenuItem";
            this.ForceExitToolStripMenuItem.Size = new System.Drawing.Size(307, 34);
            this.ForceExitToolStripMenuItem.Text = "Force Exit";
            this.ForceExitToolStripMenuItem.Click += new System.EventHandler(this.ForceExitToolStripMenuItem_Click);
            // 
            // IndividualSettingsResetToolStripMenuItem
            // 
            this.IndividualSettingsResetToolStripMenuItem.Name = "IndividualSettingsResetToolStripMenuItem";
            this.IndividualSettingsResetToolStripMenuItem.Size = new System.Drawing.Size(307, 34);
            this.IndividualSettingsResetToolStripMenuItem.Text = "Individual Settings Reset";
            this.IndividualSettingsResetToolStripMenuItem.Click += new System.EventHandler(this.IndividualSettingsResetToolStripMenuItem_Click);
            // 
            // HelpMenuStrip
            // 
            this.HelpMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowHelpToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.HelpMenuStrip.Name = "HelpMenuStrip";
            this.HelpMenuStrip.Size = new System.Drawing.Size(65, 29);
            this.HelpMenuStrip.Text = "Help";
            // 
            // ShowHelpToolStripMenuItem
            // 
            this.ShowHelpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ShowHelpToolStripMenuItem.Image")));
            this.ShowHelpToolStripMenuItem.Name = "ShowHelpToolStripMenuItem";
            this.ShowHelpToolStripMenuItem.Size = new System.Drawing.Size(200, 34);
            this.ShowHelpToolStripMenuItem.Text = "Show Help";
            this.ShowHelpToolStripMenuItem.Click += new System.EventHandler(this.ShowHelpToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AboutToolStripMenuItem.Image")));
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(200, 34);
            this.AboutToolStripMenuItem.Text = "About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // AppLabel
            // 
            this.AppLabel.AutoSize = true;
            this.AppLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppLabel.Location = new System.Drawing.Point(130, 59);
            this.AppLabel.Name = "AppLabel";
            this.AppLabel.Size = new System.Drawing.Size(357, 71);
            this.AppLabel.TabIndex = 3;
            this.AppLabel.Text = "SharpEncrypt";
            this.AppLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Logo
            // 
            this.Logo.Image = ((System.Drawing.Image)(resources.GetObject("Logo.Image")));
            this.Logo.Location = new System.Drawing.Point(22, 43);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(100, 116);
            this.Logo.TabIndex = 4;
            this.Logo.TabStop = false;
            // 
            // AddSecuredGUIButton
            // 
            this.AddSecuredGUIButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AddSecuredGUIButton.BackColor = System.Drawing.Color.Transparent;
            this.AddSecuredGUIButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddSecuredGUIButton.BackgroundImage")));
            this.AddSecuredGUIButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddSecuredGUIButton.FlatAppearance.BorderSize = 0;
            this.AddSecuredGUIButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddSecuredGUIButton.ForeColor = System.Drawing.Color.Transparent;
            this.AddSecuredGUIButton.Location = new System.Drawing.Point(575, 70);
            this.AddSecuredGUIButton.Name = "AddSecuredGUIButton";
            this.AddSecuredGUIButton.Size = new System.Drawing.Size(64, 60);
            this.AddSecuredGUIButton.TabIndex = 6;
            this.AddSecuredGUIButton.UseVisualStyleBackColor = false;
            this.AddSecuredGUIButton.Click += new System.EventHandler(this.AddSecured_Click);
            // 
            // OpenSecuredGUIButton
            // 
            this.OpenSecuredGUIButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OpenSecuredGUIButton.BackColor = System.Drawing.Color.Transparent;
            this.OpenSecuredGUIButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OpenSecuredGUIButton.BackgroundImage")));
            this.OpenSecuredGUIButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OpenSecuredGUIButton.FlatAppearance.BorderSize = 0;
            this.OpenSecuredGUIButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenSecuredGUIButton.ForeColor = System.Drawing.Color.Transparent;
            this.OpenSecuredGUIButton.Location = new System.Drawing.Point(505, 70);
            this.OpenSecuredGUIButton.Name = "OpenSecuredGUIButton";
            this.OpenSecuredGUIButton.Size = new System.Drawing.Size(64, 60);
            this.OpenSecuredGUIButton.TabIndex = 7;
            this.OpenSecuredGUIButton.UseVisualStyleBackColor = false;
            this.OpenSecuredGUIButton.Click += new System.EventHandler(this.OpenSecured_Click);
            // 
            // ShareButton
            // 
            this.ShareButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ShareButton.BackColor = System.Drawing.Color.Transparent;
            this.ShareButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ShareButton.BackgroundImage")));
            this.ShareButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ShareButton.FlatAppearance.BorderSize = 0;
            this.ShareButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShareButton.ForeColor = System.Drawing.Color.Transparent;
            this.ShareButton.Location = new System.Drawing.Point(645, 70);
            this.ShareButton.Name = "ShareButton";
            this.ShareButton.Size = new System.Drawing.Size(64, 60);
            this.ShareButton.TabIndex = 8;
            this.ShareButton.UseVisualStyleBackColor = false;
            this.ShareButton.Click += new System.EventHandler(this.ShareButton_Click);
            // 
            // PasswordManagement
            // 
            this.PasswordManagement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PasswordManagement.BackColor = System.Drawing.Color.Transparent;
            this.PasswordManagement.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PasswordManagement.BackgroundImage")));
            this.PasswordManagement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PasswordManagement.FlatAppearance.BorderSize = 0;
            this.PasswordManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PasswordManagement.ForeColor = System.Drawing.Color.Transparent;
            this.PasswordManagement.Location = new System.Drawing.Point(715, 70);
            this.PasswordManagement.Name = "PasswordManagement";
            this.PasswordManagement.Size = new System.Drawing.Size(64, 60);
            this.PasswordManagement.TabIndex = 9;
            this.PasswordManagement.UseVisualStyleBackColor = false;
            this.PasswordManagement.Click += new System.EventHandler(this.PasswordManagement_Click);
            // 
            // OpenHomeFolder
            // 
            this.OpenHomeFolder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OpenHomeFolder.BackColor = System.Drawing.Color.Transparent;
            this.OpenHomeFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OpenHomeFolder.BackgroundImage")));
            this.OpenHomeFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OpenHomeFolder.FlatAppearance.BorderSize = 0;
            this.OpenHomeFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenHomeFolder.ForeColor = System.Drawing.Color.Transparent;
            this.OpenHomeFolder.Location = new System.Drawing.Point(785, 70);
            this.OpenHomeFolder.Name = "OpenHomeFolder";
            this.OpenHomeFolder.Size = new System.Drawing.Size(64, 60);
            this.OpenHomeFolder.TabIndex = 10;
            this.OpenHomeFolder.UseVisualStyleBackColor = false;
            this.OpenHomeFolder.Click += new System.EventHandler(this.OpenHomeFolder_Click);
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.ContextMenuStrip = this.NotifyIconContextMenu;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "SharpEncrypt";
            // 
            // NotifyIconContextMenu
            // 
            this.NotifyIconContextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.NotifyIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowToolStripMenuItem,
            this.ExitToolStripMenuItem1});
            this.NotifyIconContextMenu.Name = "NotifyIconContextMenu";
            this.NotifyIconContextMenu.Size = new System.Drawing.Size(129, 68);
            // 
            // ShowToolStripMenuItem
            // 
            this.ShowToolStripMenuItem.Name = "ShowToolStripMenuItem";
            this.ShowToolStripMenuItem.Size = new System.Drawing.Size(128, 32);
            this.ShowToolStripMenuItem.Text = "Show";
            this.ShowToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem1
            // 
            this.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1";
            this.ExitToolStripMenuItem1.Size = new System.Drawing.Size(128, 32);
            this.ExitToolStripMenuItem1.Text = "Exit";
            this.ExitToolStripMenuItem1.Click += new System.EventHandler(this.ExitToolStripMenuItem1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 582);
            this.Controls.Add(this.OpenHomeFolder);
            this.Controls.Add(this.PasswordManagement);
            this.Controls.Add(this.ShareButton);
            this.Controls.Add(this.OpenSecuredGUIButton);
            this.Controls.Add(this.AddSecuredGUIButton);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.AppLabel);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.Tabs);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(1018, 638);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SharpEncrypt";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Tabs.ResumeLayout(false);
            this.SecuredFilesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SecuredFilesGrid)).EndInit();
            this.FileMenuStrip.ResumeLayout(false);
            this.SecuredFoldersTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SecuredFoldersGrid)).EndInit();
            this.FolderMenuStrip.ResumeLayout(false);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.NotifyIconContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage SecuredFilesTab;
        private System.Windows.Forms.TabPage SecuredFoldersTab;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileMenuStripItem;
        private System.Windows.Forms.ToolStripMenuItem OpenSecuredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SecureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StopSecuringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SecuredFoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AnonymousRenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeanonymiseFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddSecuredFolderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem SecureDeleteFile;
        private System.Windows.Forms.ToolStripMenuItem OptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LanguageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IncludeSubfolders;
        private System.Windows.Forms.ToolStripMenuItem Debug;
        private System.Windows.Forms.ToolStripMenuItem KeyManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportPublicKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportMyPublicKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem DebugMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ShowHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DiskToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WipeFreeDiskSpaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WipeDiskSpaceAfterSecureDeleteToolStripMenuItem;
        private System.Windows.Forms.Label AppLabel;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Button AddSecuredGUIButton;
        private System.Windows.Forms.DataGridView SecuredFilesGrid;
        private System.Windows.Forms.Button OpenSecuredGUIButton;
        private System.Windows.Forms.Button ShareButton;
        private System.Windows.Forms.ContextMenuStrip FolderMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ShareKeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DecryptPermanentlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DecryptTemporarilyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveFolderFromListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenExplorerHereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddSecuredFolderToolStripMenuItem;
        private System.Windows.Forms.DataGridView SecuredFoldersGrid;
        private System.Windows.Forms.ContextMenuStrip FileMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveFileFromListButKeepSecuredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StopSecuringAndRemoveFromListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShareKeysToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ShowInFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RenameToOriginalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearSecuredFilesListToolStripMenuItem;
        private System.Windows.Forms.Button PasswordManagement;
        private System.Windows.Forms.ToolTip OpenSecuredToolTip;
        private System.Windows.Forms.ToolTip AddSecuredFileToolTip;
        private System.Windows.Forms.ToolTip ShareToolTip;
        private System.Windows.Forms.ToolTip PasswordManagementToolTip;
        private System.Windows.Forms.ToolStripMenuItem EnglishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeutschGermanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NetherlandsDutchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FrancaisFrenchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ItalianoItalianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem KoreanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PolskiPolishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PortugueseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RussianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SwedishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TurkishToolStripMenuItem;
        private System.Windows.Forms.Button OpenHomeFolder;
        private System.Windows.Forms.ToolTip OpenHomeFolderToolTip;
        private System.Windows.Forms.ToolStripMenuItem UseADifferentPasswordForEachFile;
        private System.Windows.Forms.ToolStripMenuItem ChangeSessionPasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GenerateNewKeyPairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Advanced;
        private System.Windows.Forms.ToolStripMenuItem OneTimePadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SecureFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DecryptFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResetAllSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ValidateContainerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewCompletedJobsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GenerateKeyForFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewActiveJobsToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem AdvancedMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ForceExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CancelAllFutureTasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IndividualSettingsResetToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip NotifyIconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ShowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem SecureDeleteFolder;
        private System.Windows.Forms.ToolStripMenuItem DeleteGridExclusionLists;
        private System.Windows.Forms.ToolStripMenuItem ShowAllSecuredFolders;
        private System.Windows.Forms.ToolStripMenuItem ShowAllSecuredFiles;
        private System.Windows.Forms.ToolStripMenuItem HideExcludedSecuredFoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HideExcludedSecuredFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearSecuredFoldersGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewLogToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileColumnHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeColumnHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn SecuredColumnHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlgorithmColumnHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn Folder;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.ToolStripMenuItem AddSecuredFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DoNotPromptForPasswordOnStartup;
        private System.Windows.Forms.ToolStripMenuItem PasswordManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenPasswordManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ManagementStoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AESPasswordBasedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OTPKeyBasedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ClearSessionPassword;
        private System.Windows.Forms.ToolStripMenuItem BulkAnonymousRenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BulkDeanonymizeFileNamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AnonymizeFileNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeanonymizeFileNameToolStripMenuItem;
    }
}

