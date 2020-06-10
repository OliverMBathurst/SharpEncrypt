namespace SharpEncrypt
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
            this.recentFiles = new System.Windows.Forms.TabPage();
            this.RecentFilesGrid = new System.Windows.Forms.DataGridView();
            this.FileMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromListButKeepSecuredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopSecuringAndRemoveFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shareKeysToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showInFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToOriginalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRecentFilesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securedFolders = new System.Windows.Forms.TabPage();
            this.SecuredFoldersGrid = new System.Windows.Forms.DataGridView();
            this.Folder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FolderMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.shareKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decryptPermanentlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decryptTemporarilyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openExplorerHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSecuredFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenSecuredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SecureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopSecuringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SecuredFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnonymousRenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameToOriginalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.AddSecuredFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SecureDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.OptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IncludeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KeyManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportPublicKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportMyPublicKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebugMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.DiskToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WipeFreeDiskSpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdvancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdvancedDiskWipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AppLabel = new System.Windows.Forms.Label();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.AddSecured = new System.Windows.Forms.Button();
            this.OpenSecured = new System.Windows.Forms.Button();
            this.ShareButton = new System.Windows.Forms.Button();
            this.PasswordManagement = new System.Windows.Forms.Button();
            this.OpenHomeFolder = new System.Windows.Forms.Button();
            this.OpenCloudFolder = new System.Windows.Forms.Button();
            this.OpenSecuredToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.AddSecuredFileToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ShareToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PasswordManagementToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.HomeFolderToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.FileColumnHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeColumnHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SecuredColumnHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlgorithmColumnHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tabs.SuspendLayout();
            this.recentFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecentFilesGrid)).BeginInit();
            this.FileMenuStrip.SuspendLayout();
            this.securedFolders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SecuredFoldersGrid)).BeginInit();
            this.FolderMenuStrip.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tabs.Controls.Add(this.recentFiles);
            this.Tabs.Controls.Add(this.securedFolders);
            this.Tabs.Location = new System.Drawing.Point(12, 165);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(1068, 405);
            this.Tabs.TabIndex = 0;
            // 
            // recentFiles
            // 
            this.recentFiles.Controls.Add(this.RecentFilesGrid);
            this.recentFiles.Location = new System.Drawing.Point(4, 29);
            this.recentFiles.Name = "recentFiles";
            this.recentFiles.Padding = new System.Windows.Forms.Padding(3);
            this.recentFiles.Size = new System.Drawing.Size(1060, 372);
            this.recentFiles.TabIndex = 0;
            this.recentFiles.Text = "Recent Files";
            this.recentFiles.UseVisualStyleBackColor = true;
            // 
            // RecentFilesGrid
            // 
            this.RecentFilesGrid.AllowUserToAddRows = false;
            this.RecentFilesGrid.AllowUserToDeleteRows = false;
            this.RecentFilesGrid.AllowUserToOrderColumns = true;
            this.RecentFilesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.RecentFilesGrid.BackgroundColor = System.Drawing.Color.White;
            this.RecentFilesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RecentFilesGrid.ColumnHeadersHeight = 34;
            this.RecentFilesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileColumnHeader,
            this.TimeColumnHeader,
            this.SecuredColumnHeader,
            this.AlgorithmColumnHeader});
            this.RecentFilesGrid.ContextMenuStrip = this.FileMenuStrip;
            this.RecentFilesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RecentFilesGrid.GridColor = System.Drawing.Color.White;
            this.RecentFilesGrid.Location = new System.Drawing.Point(3, 3);
            this.RecentFilesGrid.Name = "RecentFilesGrid";
            this.RecentFilesGrid.ReadOnly = true;
            this.RecentFilesGrid.RowHeadersVisible = false;
            this.RecentFilesGrid.RowHeadersWidth = 62;
            this.RecentFilesGrid.RowTemplate.Height = 28;
            this.RecentFilesGrid.Size = new System.Drawing.Size(1054, 366);
            this.RecentFilesGrid.TabIndex = 1;
            // 
            // FileMenuStrip
            // 
            this.FileMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.FileMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.removeFromListButKeepSecuredToolStripMenuItem,
            this.stopSecuringAndRemoveFromListToolStripMenuItem,
            this.shareKeysToolStripMenuItem1,
            this.showInFolderToolStripMenuItem,
            this.renameToOriginalToolStripMenuItem,
            this.clearRecentFilesListToolStripMenuItem});
            this.FileMenuStrip.Name = "contextMenuStrip1";
            this.FileMenuStrip.Size = new System.Drawing.Size(364, 228);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(363, 32);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // removeFromListButKeepSecuredToolStripMenuItem
            // 
            this.removeFromListButKeepSecuredToolStripMenuItem.Name = "removeFromListButKeepSecuredToolStripMenuItem";
            this.removeFromListButKeepSecuredToolStripMenuItem.Size = new System.Drawing.Size(363, 32);
            this.removeFromListButKeepSecuredToolStripMenuItem.Text = "Remove from list but keep secured";
            // 
            // stopSecuringAndRemoveFromListToolStripMenuItem
            // 
            this.stopSecuringAndRemoveFromListToolStripMenuItem.Name = "stopSecuringAndRemoveFromListToolStripMenuItem";
            this.stopSecuringAndRemoveFromListToolStripMenuItem.Size = new System.Drawing.Size(363, 32);
            this.stopSecuringAndRemoveFromListToolStripMenuItem.Text = "Stop securing and remove from list";
            // 
            // shareKeysToolStripMenuItem1
            // 
            this.shareKeysToolStripMenuItem1.Name = "shareKeysToolStripMenuItem1";
            this.shareKeysToolStripMenuItem1.Size = new System.Drawing.Size(363, 32);
            this.shareKeysToolStripMenuItem1.Text = "Share keys";
            // 
            // showInFolderToolStripMenuItem
            // 
            this.showInFolderToolStripMenuItem.Name = "showInFolderToolStripMenuItem";
            this.showInFolderToolStripMenuItem.Size = new System.Drawing.Size(363, 32);
            this.showInFolderToolStripMenuItem.Text = "Show in folder";
            // 
            // renameToOriginalToolStripMenuItem
            // 
            this.renameToOriginalToolStripMenuItem.Name = "renameToOriginalToolStripMenuItem";
            this.renameToOriginalToolStripMenuItem.Size = new System.Drawing.Size(363, 32);
            this.renameToOriginalToolStripMenuItem.Text = "Rename to original";
            // 
            // clearRecentFilesListToolStripMenuItem
            // 
            this.clearRecentFilesListToolStripMenuItem.Name = "clearRecentFilesListToolStripMenuItem";
            this.clearRecentFilesListToolStripMenuItem.Size = new System.Drawing.Size(363, 32);
            this.clearRecentFilesListToolStripMenuItem.Text = "Clear Recent Files list";
            // 
            // securedFolders
            // 
            this.securedFolders.Controls.Add(this.SecuredFoldersGrid);
            this.securedFolders.Location = new System.Drawing.Point(4, 29);
            this.securedFolders.Name = "securedFolders";
            this.securedFolders.Padding = new System.Windows.Forms.Padding(3);
            this.securedFolders.Size = new System.Drawing.Size(1060, 372);
            this.securedFolders.TabIndex = 1;
            this.securedFolders.Text = "Secured Folders";
            this.securedFolders.UseVisualStyleBackColor = true;
            // 
            // SecuredFoldersGrid
            // 
            this.SecuredFoldersGrid.AllowUserToAddRows = false;
            this.SecuredFoldersGrid.AllowUserToDeleteRows = false;
            this.SecuredFoldersGrid.AllowUserToOrderColumns = true;
            this.SecuredFoldersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SecuredFoldersGrid.BackgroundColor = System.Drawing.Color.White;
            this.SecuredFoldersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SecuredFoldersGrid.ColumnHeadersHeight = 34;
            this.SecuredFoldersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Folder});
            this.SecuredFoldersGrid.ContextMenuStrip = this.FolderMenuStrip;
            this.SecuredFoldersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SecuredFoldersGrid.GridColor = System.Drawing.Color.White;
            this.SecuredFoldersGrid.Location = new System.Drawing.Point(3, 3);
            this.SecuredFoldersGrid.Name = "SecuredFoldersGrid";
            this.SecuredFoldersGrid.ReadOnly = true;
            this.SecuredFoldersGrid.RowHeadersVisible = false;
            this.SecuredFoldersGrid.RowHeadersWidth = 62;
            this.SecuredFoldersGrid.RowTemplate.Height = 28;
            this.SecuredFoldersGrid.Size = new System.Drawing.Size(1054, 366);
            this.SecuredFoldersGrid.TabIndex = 0;
            // 
            // Folder
            // 
            this.Folder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Folder.HeaderText = "Folder";
            this.Folder.MinimumWidth = 8;
            this.Folder.Name = "Folder";
            this.Folder.ReadOnly = true;
            // 
            // FolderMenuStrip
            // 
            this.FolderMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.FolderMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shareKeysToolStripMenuItem,
            this.decryptPermanentlyToolStripMenuItem,
            this.decryptTemporarilyToolStripMenuItem,
            this.removeFromListToolStripMenuItem,
            this.openExplorerHereToolStripMenuItem,
            this.addSecuredFolderToolStripMenuItem});
            this.FolderMenuStrip.Name = "folderMenuStrip";
            this.FolderMenuStrip.Size = new System.Drawing.Size(361, 196);
            // 
            // shareKeysToolStripMenuItem
            // 
            this.shareKeysToolStripMenuItem.Name = "shareKeysToolStripMenuItem";
            this.shareKeysToolStripMenuItem.Size = new System.Drawing.Size(360, 32);
            this.shareKeysToolStripMenuItem.Text = "Share Keys";
            // 
            // decryptPermanentlyToolStripMenuItem
            // 
            this.decryptPermanentlyToolStripMenuItem.Name = "decryptPermanentlyToolStripMenuItem";
            this.decryptPermanentlyToolStripMenuItem.Size = new System.Drawing.Size(360, 32);
            this.decryptPermanentlyToolStripMenuItem.Text = "Decrypt Permanently";
            // 
            // decryptTemporarilyToolStripMenuItem
            // 
            this.decryptTemporarilyToolStripMenuItem.Name = "decryptTemporarilyToolStripMenuItem";
            this.decryptTemporarilyToolStripMenuItem.Size = new System.Drawing.Size(360, 32);
            this.decryptTemporarilyToolStripMenuItem.Text = "Decrypt Temporarily";
            // 
            // removeFromListToolStripMenuItem
            // 
            this.removeFromListToolStripMenuItem.Name = "removeFromListToolStripMenuItem";
            this.removeFromListToolStripMenuItem.Size = new System.Drawing.Size(360, 32);
            this.removeFromListToolStripMenuItem.Text = "Remove from list but keep secured";
            // 
            // openExplorerHereToolStripMenuItem
            // 
            this.openExplorerHereToolStripMenuItem.Name = "openExplorerHereToolStripMenuItem";
            this.openExplorerHereToolStripMenuItem.Size = new System.Drawing.Size(360, 32);
            this.openExplorerHereToolStripMenuItem.Text = "Open Explorer here";
            // 
            // addSecuredFolderToolStripMenuItem
            // 
            this.addSecuredFolderToolStripMenuItem.Name = "addSecuredFolderToolStripMenuItem";
            this.addSecuredFolderToolStripMenuItem.Size = new System.Drawing.Size(360, 32);
            this.addSecuredFolderToolStripMenuItem.Text = "Add Secured Folder";
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
            this.RenameToOriginalToolStripMenuItem1,
            this.AddSecuredFolderToolStripMenuItem1,
            this.toolStripSeparator1,
            this.SecureDeleteToolStripMenuItem,
            this.toolStripSeparator2,
            this.OptionsToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.FileMenuStripItem.Name = "FileMenuStripItem";
            this.FileMenuStripItem.Size = new System.Drawing.Size(54, 29);
            this.FileMenuStripItem.Text = "File";
            // 
            // OpenSecuredToolStripMenuItem
            // 
            this.OpenSecuredToolStripMenuItem.Name = "OpenSecuredToolStripMenuItem";
            this.OpenSecuredToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.OpenSecuredToolStripMenuItem.Text = "Open Secured";
            // 
            // SecureToolStripMenuItem
            // 
            this.SecureToolStripMenuItem.Name = "SecureToolStripMenuItem";
            this.SecureToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.SecureToolStripMenuItem.Text = "Secure";
            // 
            // StopSecuringToolStripMenuItem
            // 
            this.StopSecuringToolStripMenuItem.Name = "StopSecuringToolStripMenuItem";
            this.StopSecuringToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.StopSecuringToolStripMenuItem.Text = "Stop Securing";
            // 
            // SecuredFoldersToolStripMenuItem
            // 
            this.SecuredFoldersToolStripMenuItem.Name = "SecuredFoldersToolStripMenuItem";
            this.SecuredFoldersToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.SecuredFoldersToolStripMenuItem.Text = "Secured Folders";
            // 
            // AnonymousRenameToolStripMenuItem
            // 
            this.AnonymousRenameToolStripMenuItem.Name = "AnonymousRenameToolStripMenuItem";
            this.AnonymousRenameToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.AnonymousRenameToolStripMenuItem.Text = "Anonymous Rename";
            // 
            // RenameToOriginalToolStripMenuItem1
            // 
            this.RenameToOriginalToolStripMenuItem1.Name = "RenameToOriginalToolStripMenuItem1";
            this.RenameToOriginalToolStripMenuItem1.Size = new System.Drawing.Size(279, 34);
            this.RenameToOriginalToolStripMenuItem1.Text = "Rename to original";
            // 
            // AddSecuredFolderToolStripMenuItem1
            // 
            this.AddSecuredFolderToolStripMenuItem1.Name = "AddSecuredFolderToolStripMenuItem1";
            this.AddSecuredFolderToolStripMenuItem1.Size = new System.Drawing.Size(279, 34);
            this.AddSecuredFolderToolStripMenuItem1.Text = "Add Secured Folder";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(276, 6);
            // 
            // SecureDeleteToolStripMenuItem
            // 
            this.SecureDeleteToolStripMenuItem.Name = "SecureDeleteToolStripMenuItem";
            this.SecureDeleteToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.SecureDeleteToolStripMenuItem.Text = "Secure Delete";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(276, 6);
            // 
            // OptionsToolStripMenuItem
            // 
            this.OptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LanguageToolStripMenuItem,
            this.IncludeToolStripMenuItem,
            this.DebugToolStripMenuItem,
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem,
            this.KeyManagementToolStripMenuItem});
            this.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
            this.OptionsToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.OptionsToolStripMenuItem.Text = "Options";
            // 
            // LanguageToolStripMenuItem
            // 
            this.LanguageToolStripMenuItem.Name = "LanguageToolStripMenuItem";
            this.LanguageToolStripMenuItem.Size = new System.Drawing.Size(402, 34);
            this.LanguageToolStripMenuItem.Text = "Language";
            // 
            // IncludeToolStripMenuItem
            // 
            this.IncludeToolStripMenuItem.Name = "IncludeToolStripMenuItem";
            this.IncludeToolStripMenuItem.Size = new System.Drawing.Size(402, 34);
            this.IncludeToolStripMenuItem.Text = "Include Subfolders";
            // 
            // DebugToolStripMenuItem
            // 
            this.DebugToolStripMenuItem.Name = "DebugToolStripMenuItem";
            this.DebugToolStripMenuItem.Size = new System.Drawing.Size(402, 34);
            this.DebugToolStripMenuItem.Text = "Debug";
            this.DebugToolStripMenuItem.Click += new System.EventHandler(this.DebugToolStripMenuItem_Click);
            // 
            // WipeDiskSpaceAfterSecureDeleteToolStripMenuItem
            // 
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Name = "WipeDiskSpaceAfterSecureDeleteToolStripMenuItem";
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Size = new System.Drawing.Size(402, 34);
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Text = "Wipe Disk Space After Secure Delete";
            // 
            // KeyManagementToolStripMenuItem
            // 
            this.KeyManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportPublicKeyToolStripMenuItem,
            this.ExportMyPublicKeyToolStripMenuItem});
            this.KeyManagementToolStripMenuItem.Name = "KeyManagementToolStripMenuItem";
            this.KeyManagementToolStripMenuItem.Size = new System.Drawing.Size(402, 34);
            this.KeyManagementToolStripMenuItem.Text = "Key Management";
            // 
            // ImportPublicKeyToolStripMenuItem
            // 
            this.ImportPublicKeyToolStripMenuItem.Name = "ImportPublicKeyToolStripMenuItem";
            this.ImportPublicKeyToolStripMenuItem.Size = new System.Drawing.Size(347, 34);
            this.ImportPublicKeyToolStripMenuItem.Text = "Import Someone\'s Public Key";
            // 
            // ExportMyPublicKeyToolStripMenuItem
            // 
            this.ExportMyPublicKeyToolStripMenuItem.Name = "ExportMyPublicKeyToolStripMenuItem";
            this.ExportMyPublicKeyToolStripMenuItem.Size = new System.Drawing.Size(347, 34);
            this.ExportMyPublicKeyToolStripMenuItem.Text = "Export My Public Key";
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem1_Click);
            // 
            // DebugMenuStrip
            // 
            this.DebugMenuStrip.Enabled = false;
            this.DebugMenuStrip.Name = "DebugMenuStrip";
            this.DebugMenuStrip.Size = new System.Drawing.Size(82, 29);
            this.DebugMenuStrip.Text = "Debug";
            // 
            // DiskToolsToolStripMenuItem
            // 
            this.DiskToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WipeFreeDiskSpaceToolStripMenuItem,
            this.AdvancedToolStripMenuItem});
            this.DiskToolsToolStripMenuItem.Name = "DiskToolsToolStripMenuItem";
            this.DiskToolsToolStripMenuItem.Size = new System.Drawing.Size(108, 29);
            this.DiskToolsToolStripMenuItem.Text = "Disk Tools";
            // 
            // WipeFreeDiskSpaceToolStripMenuItem
            // 
            this.WipeFreeDiskSpaceToolStripMenuItem.Name = "WipeFreeDiskSpaceToolStripMenuItem";
            this.WipeFreeDiskSpaceToolStripMenuItem.Size = new System.Drawing.Size(284, 34);
            this.WipeFreeDiskSpaceToolStripMenuItem.Text = "Wipe Free Disk Space";
            // 
            // AdvancedToolStripMenuItem
            // 
            this.AdvancedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AdvancedDiskWipeToolStripMenuItem});
            this.AdvancedToolStripMenuItem.Name = "AdvancedToolStripMenuItem";
            this.AdvancedToolStripMenuItem.Size = new System.Drawing.Size(284, 34);
            this.AdvancedToolStripMenuItem.Text = "Advanced";
            // 
            // AdvancedDiskWipeToolStripMenuItem
            // 
            this.AdvancedDiskWipeToolStripMenuItem.Name = "AdvancedDiskWipeToolStripMenuItem";
            this.AdvancedDiskWipeToolStripMenuItem.Size = new System.Drawing.Size(278, 34);
            this.AdvancedDiskWipeToolStripMenuItem.Text = "Advanced Disk Wipe";
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
            this.ShowHelpToolStripMenuItem.Name = "ShowHelpToolStripMenuItem";
            this.ShowHelpToolStripMenuItem.Size = new System.Drawing.Size(200, 34);
            this.ShowHelpToolStripMenuItem.Text = "Show Help";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(200, 34);
            this.AboutToolStripMenuItem.Text = "About";
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
            // AddSecured
            // 
            this.AddSecured.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AddSecured.BackColor = System.Drawing.Color.Transparent;
            this.AddSecured.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddSecured.BackgroundImage")));
            this.AddSecured.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddSecured.FlatAppearance.BorderSize = 0;
            this.AddSecured.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddSecured.ForeColor = System.Drawing.Color.Transparent;
            this.AddSecured.Location = new System.Drawing.Point(575, 70);
            this.AddSecured.Name = "AddSecured";
            this.AddSecured.Size = new System.Drawing.Size(64, 60);
            this.AddSecured.TabIndex = 6;
            this.AddSecured.UseVisualStyleBackColor = false;
            // 
            // OpenSecured
            // 
            this.OpenSecured.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OpenSecured.BackColor = System.Drawing.Color.Transparent;
            this.OpenSecured.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OpenSecured.BackgroundImage")));
            this.OpenSecured.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OpenSecured.FlatAppearance.BorderSize = 0;
            this.OpenSecured.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenSecured.ForeColor = System.Drawing.Color.Transparent;
            this.OpenSecured.Location = new System.Drawing.Point(505, 70);
            this.OpenSecured.Name = "OpenSecured";
            this.OpenSecured.Size = new System.Drawing.Size(64, 60);
            this.OpenSecured.TabIndex = 7;
            this.OpenSecured.UseVisualStyleBackColor = false;
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
            // 
            // OpenCloudFolder
            // 
            this.OpenCloudFolder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OpenCloudFolder.BackColor = System.Drawing.Color.Transparent;
            this.OpenCloudFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OpenCloudFolder.BackgroundImage")));
            this.OpenCloudFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OpenCloudFolder.FlatAppearance.BorderSize = 0;
            this.OpenCloudFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenCloudFolder.ForeColor = System.Drawing.Color.Transparent;
            this.OpenCloudFolder.Location = new System.Drawing.Point(855, 70);
            this.OpenCloudFolder.Name = "OpenCloudFolder";
            this.OpenCloudFolder.Size = new System.Drawing.Size(64, 60);
            this.OpenCloudFolder.TabIndex = 11;
            this.OpenCloudFolder.UseVisualStyleBackColor = false;
            // 
            // FileColumnHeader
            // 
            this.FileColumnHeader.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FileColumnHeader.HeaderText = "File";
            this.FileColumnHeader.MinimumWidth = 8;
            this.FileColumnHeader.Name = "FileColumnHeader";
            this.FileColumnHeader.ReadOnly = true;
            // 
            // TimeColumnHeader
            // 
            this.TimeColumnHeader.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TimeColumnHeader.HeaderText = "Time";
            this.TimeColumnHeader.MinimumWidth = 8;
            this.TimeColumnHeader.Name = "TimeColumnHeader";
            this.TimeColumnHeader.ReadOnly = true;
            // 
            // SecuredColumnHeader
            // 
            this.SecuredColumnHeader.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SecuredColumnHeader.HeaderText = "Secured";
            this.SecuredColumnHeader.MinimumWidth = 8;
            this.SecuredColumnHeader.Name = "SecuredColumnHeader";
            this.SecuredColumnHeader.ReadOnly = true;
            // 
            // AlgorithmColumnHeader
            // 
            this.AlgorithmColumnHeader.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AlgorithmColumnHeader.HeaderText = "Algorithm";
            this.AlgorithmColumnHeader.MinimumWidth = 8;
            this.AlgorithmColumnHeader.Name = "AlgorithmColumnHeader";
            this.AlgorithmColumnHeader.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 582);
            this.Controls.Add(this.OpenCloudFolder);
            this.Controls.Add(this.OpenHomeFolder);
            this.Controls.Add(this.PasswordManagement);
            this.Controls.Add(this.ShareButton);
            this.Controls.Add(this.OpenSecured);
            this.Controls.Add(this.AddSecured);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.AppLabel);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.Tabs);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(1018, 638);
            this.Name = "MainForm";
            this.Text = "SharpEncrypt";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Tabs.ResumeLayout(false);
            this.recentFiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RecentFilesGrid)).EndInit();
            this.FileMenuStrip.ResumeLayout(false);
            this.securedFolders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SecuredFoldersGrid)).EndInit();
            this.FolderMenuStrip.ResumeLayout(false);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage recentFiles;
        private System.Windows.Forms.TabPage securedFolders;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileMenuStripItem;
        private System.Windows.Forms.ToolStripMenuItem OpenSecuredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SecureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StopSecuringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SecuredFoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AnonymousRenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RenameToOriginalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem AddSecuredFolderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem SecureDeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LanguageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IncludeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DebugToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem AdvancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AdvancedDiskWipeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WipeDiskSpaceAfterSecureDeleteToolStripMenuItem;
        private System.Windows.Forms.Label AppLabel;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Button AddSecured;
        private System.Windows.Forms.DataGridView RecentFilesGrid;
        private System.Windows.Forms.Button OpenSecured;
        private System.Windows.Forms.Button ShareButton;
        private System.Windows.Forms.ContextMenuStrip FolderMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem shareKeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decryptPermanentlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decryptTemporarilyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFromListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openExplorerHereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSecuredFolderToolStripMenuItem;
        private System.Windows.Forms.DataGridView SecuredFoldersGrid;
        private System.Windows.Forms.ContextMenuStrip FileMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFromListButKeepSecuredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopSecuringAndRemoveFromListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shareKeysToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showInFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToOriginalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearRecentFilesListToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Folder;
        private System.Windows.Forms.Button PasswordManagement;
        private System.Windows.Forms.Button OpenHomeFolder;
        private System.Windows.Forms.Button OpenCloudFolder;
        private System.Windows.Forms.ToolTip OpenSecuredToolTip;
        private System.Windows.Forms.ToolTip AddSecuredFileToolTip;
        private System.Windows.Forms.ToolTip ShareToolTip;
        private System.Windows.Forms.ToolTip PasswordManagementToolTip;
        private System.Windows.Forms.ToolTip HomeFolderToolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileColumnHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeColumnHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn SecuredColumnHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlgorithmColumnHeader;
    }
}

