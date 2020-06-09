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
            this.tabs = new System.Windows.Forms.TabControl();
            this.recentFiles = new System.Windows.Forms.TabPage();
            this.recentFilesGrid = new System.Windows.Forms.DataGridView();
            this.securedFolders = new System.Windows.Forms.TabPage();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSecuredToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.secureToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stopSecuringToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.securedFoldersToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.anonymousRenameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToOriginalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addSecuredFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.secureDeleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.includeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wipeDiskSpaceAfterSecureDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importPublicKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMyPublicKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.debugMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.diskToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wipeFreeDiskSpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedDiskWipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.showHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sharpEncryptLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.folderMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.shareKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decryptPermanentlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decryptTemporarilyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openExplorerHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSecuredFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securedFoldersGrid = new System.Windows.Forms.DataGridView();
            this.fileMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromListButKeepSecuredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopSecuringAndRemoveFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shareKeysToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showInFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToOriginalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRecentFilesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Folder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.File = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Secured = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Algorithm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabs.SuspendLayout();
            this.recentFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recentFilesGrid)).BeginInit();
            this.securedFolders.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.folderMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.securedFoldersGrid)).BeginInit();
            this.fileMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.recentFiles);
            this.tabs.Controls.Add(this.securedFolders);
            this.tabs.Location = new System.Drawing.Point(12, 165);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(972, 405);
            this.tabs.TabIndex = 0;
            // 
            // recentFiles
            // 
            this.recentFiles.Controls.Add(this.recentFilesGrid);
            this.recentFiles.Location = new System.Drawing.Point(4, 29);
            this.recentFiles.Name = "recentFiles";
            this.recentFiles.Padding = new System.Windows.Forms.Padding(3);
            this.recentFiles.Size = new System.Drawing.Size(964, 372);
            this.recentFiles.TabIndex = 0;
            this.recentFiles.Text = "Recent Files";
            this.recentFiles.UseVisualStyleBackColor = true;
            // 
            // recentFilesGrid
            // 
            this.recentFilesGrid.AllowUserToAddRows = false;
            this.recentFilesGrid.AllowUserToDeleteRows = false;
            this.recentFilesGrid.AllowUserToOrderColumns = true;
            this.recentFilesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.recentFilesGrid.BackgroundColor = System.Drawing.Color.White;
            this.recentFilesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.recentFilesGrid.ColumnHeadersHeight = 34;
            this.recentFilesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.File,
            this.Time,
            this.Secured,
            this.Algorithm});
            this.recentFilesGrid.ContextMenuStrip = this.fileMenuStrip;
            this.recentFilesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recentFilesGrid.GridColor = System.Drawing.Color.White;
            this.recentFilesGrid.Location = new System.Drawing.Point(3, 3);
            this.recentFilesGrid.Name = "recentFilesGrid";
            this.recentFilesGrid.ReadOnly = true;
            this.recentFilesGrid.RowHeadersVisible = false;
            this.recentFilesGrid.RowHeadersWidth = 62;
            this.recentFilesGrid.RowTemplate.Height = 28;
            this.recentFilesGrid.Size = new System.Drawing.Size(958, 366);
            this.recentFilesGrid.TabIndex = 1;
            // 
            // securedFolders
            // 
            this.securedFolders.Controls.Add(this.securedFoldersGrid);
            this.securedFolders.Location = new System.Drawing.Point(4, 29);
            this.securedFolders.Name = "securedFolders";
            this.securedFolders.Padding = new System.Windows.Forms.Padding(3);
            this.securedFolders.Size = new System.Drawing.Size(964, 372);
            this.securedFolders.TabIndex = 1;
            this.securedFolders.Text = "Secured Folders";
            this.securedFolders.UseVisualStyleBackColor = true;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuStripItem,
            this.debugMenuStrip,
            this.diskToolsToolStripMenuItem,
            this.helpMenuStrip});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(996, 33);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenuStripItem
            // 
            this.fileMenuStripItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSecuredToolStripMenuItem1,
            this.secureToolStripMenuItem1,
            this.stopSecuringToolStripMenuItem1,
            this.securedFoldersToolStripMenuItem1,
            this.anonymousRenameToolStripMenuItem1,
            this.renameToOriginalToolStripMenuItem1,
            this.addSecuredFolderToolStripMenuItem1,
            this.toolStripSeparator1,
            this.secureDeleteToolStripMenuItem1,
            this.toolStripSeparator2,
            this.optionsToolStripMenuItem1,
            this.exitToolStripMenuItem1});
            this.fileMenuStripItem.Name = "fileMenuStripItem";
            this.fileMenuStripItem.Size = new System.Drawing.Size(54, 29);
            this.fileMenuStripItem.Text = "File";
            this.fileMenuStripItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openSecuredToolStripMenuItem1
            // 
            this.openSecuredToolStripMenuItem1.Name = "openSecuredToolStripMenuItem1";
            this.openSecuredToolStripMenuItem1.Size = new System.Drawing.Size(279, 34);
            this.openSecuredToolStripMenuItem1.Text = "Open Secured";
            // 
            // secureToolStripMenuItem1
            // 
            this.secureToolStripMenuItem1.Name = "secureToolStripMenuItem1";
            this.secureToolStripMenuItem1.Size = new System.Drawing.Size(279, 34);
            this.secureToolStripMenuItem1.Text = "Secure";
            // 
            // stopSecuringToolStripMenuItem1
            // 
            this.stopSecuringToolStripMenuItem1.Name = "stopSecuringToolStripMenuItem1";
            this.stopSecuringToolStripMenuItem1.Size = new System.Drawing.Size(279, 34);
            this.stopSecuringToolStripMenuItem1.Text = "Stop Securing";
            // 
            // securedFoldersToolStripMenuItem1
            // 
            this.securedFoldersToolStripMenuItem1.Name = "securedFoldersToolStripMenuItem1";
            this.securedFoldersToolStripMenuItem1.Size = new System.Drawing.Size(279, 34);
            this.securedFoldersToolStripMenuItem1.Text = "Secured Folders";
            // 
            // anonymousRenameToolStripMenuItem1
            // 
            this.anonymousRenameToolStripMenuItem1.Name = "anonymousRenameToolStripMenuItem1";
            this.anonymousRenameToolStripMenuItem1.Size = new System.Drawing.Size(279, 34);
            this.anonymousRenameToolStripMenuItem1.Text = "Anonymous Rename";
            // 
            // renameToOriginalToolStripMenuItem1
            // 
            this.renameToOriginalToolStripMenuItem1.Name = "renameToOriginalToolStripMenuItem1";
            this.renameToOriginalToolStripMenuItem1.Size = new System.Drawing.Size(279, 34);
            this.renameToOriginalToolStripMenuItem1.Text = "Rename to original";
            // 
            // addSecuredFolderToolStripMenuItem1
            // 
            this.addSecuredFolderToolStripMenuItem1.Name = "addSecuredFolderToolStripMenuItem1";
            this.addSecuredFolderToolStripMenuItem1.Size = new System.Drawing.Size(279, 34);
            this.addSecuredFolderToolStripMenuItem1.Text = "Add Secured Folder";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(276, 6);
            // 
            // secureDeleteToolStripMenuItem1
            // 
            this.secureDeleteToolStripMenuItem1.Name = "secureDeleteToolStripMenuItem1";
            this.secureDeleteToolStripMenuItem1.Size = new System.Drawing.Size(279, 34);
            this.secureDeleteToolStripMenuItem1.Text = "Secure Delete";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(276, 6);
            // 
            // optionsToolStripMenuItem1
            // 
            this.optionsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem1,
            this.includeToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.wipeDiskSpaceAfterSecureDeleteToolStripMenuItem,
            this.keyManagementToolStripMenuItem});
            this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
            this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(279, 34);
            this.optionsToolStripMenuItem1.Text = "Options";
            // 
            // languageToolStripMenuItem1
            // 
            this.languageToolStripMenuItem1.Name = "languageToolStripMenuItem1";
            this.languageToolStripMenuItem1.Size = new System.Drawing.Size(402, 34);
            this.languageToolStripMenuItem1.Text = "Language";
            // 
            // includeToolStripMenuItem
            // 
            this.includeToolStripMenuItem.Name = "includeToolStripMenuItem";
            this.includeToolStripMenuItem.Size = new System.Drawing.Size(402, 34);
            this.includeToolStripMenuItem.Text = "Include Subfolders";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(402, 34);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // wipeDiskSpaceAfterSecureDeleteToolStripMenuItem
            // 
            this.wipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Name = "wipeDiskSpaceAfterSecureDeleteToolStripMenuItem";
            this.wipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Size = new System.Drawing.Size(402, 34);
            this.wipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Text = "Wipe Disk Space After Secure Delete";
            // 
            // keyManagementToolStripMenuItem
            // 
            this.keyManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importPublicKeyToolStripMenuItem,
            this.exportMyPublicKeyToolStripMenuItem});
            this.keyManagementToolStripMenuItem.Name = "keyManagementToolStripMenuItem";
            this.keyManagementToolStripMenuItem.Size = new System.Drawing.Size(402, 34);
            this.keyManagementToolStripMenuItem.Text = "Key Management";
            // 
            // importPublicKeyToolStripMenuItem
            // 
            this.importPublicKeyToolStripMenuItem.Name = "importPublicKeyToolStripMenuItem";
            this.importPublicKeyToolStripMenuItem.Size = new System.Drawing.Size(347, 34);
            this.importPublicKeyToolStripMenuItem.Text = "Import Someone\'s Public Key";
            this.importPublicKeyToolStripMenuItem.Click += new System.EventHandler(this.importPublicKeyToolStripMenuItem_Click);
            // 
            // exportMyPublicKeyToolStripMenuItem
            // 
            this.exportMyPublicKeyToolStripMenuItem.Name = "exportMyPublicKeyToolStripMenuItem";
            this.exportMyPublicKeyToolStripMenuItem.Size = new System.Drawing.Size(347, 34);
            this.exportMyPublicKeyToolStripMenuItem.Text = "Export My Public Key";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(279, 34);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // debugMenuStrip
            // 
            this.debugMenuStrip.Enabled = false;
            this.debugMenuStrip.Name = "debugMenuStrip";
            this.debugMenuStrip.Size = new System.Drawing.Size(82, 29);
            this.debugMenuStrip.Text = "Debug";
            // 
            // diskToolsToolStripMenuItem
            // 
            this.diskToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wipeFreeDiskSpaceToolStripMenuItem,
            this.advancedToolStripMenuItem});
            this.diskToolsToolStripMenuItem.Name = "diskToolsToolStripMenuItem";
            this.diskToolsToolStripMenuItem.Size = new System.Drawing.Size(108, 29);
            this.diskToolsToolStripMenuItem.Text = "Disk Tools";
            // 
            // wipeFreeDiskSpaceToolStripMenuItem
            // 
            this.wipeFreeDiskSpaceToolStripMenuItem.Name = "wipeFreeDiskSpaceToolStripMenuItem";
            this.wipeFreeDiskSpaceToolStripMenuItem.Size = new System.Drawing.Size(284, 34);
            this.wipeFreeDiskSpaceToolStripMenuItem.Text = "Wipe Free Disk Space";
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.advancedDiskWipeToolStripMenuItem});
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(284, 34);
            this.advancedToolStripMenuItem.Text = "Advanced";
            // 
            // advancedDiskWipeToolStripMenuItem
            // 
            this.advancedDiskWipeToolStripMenuItem.Name = "advancedDiskWipeToolStripMenuItem";
            this.advancedDiskWipeToolStripMenuItem.Size = new System.Drawing.Size(278, 34);
            this.advancedDiskWipeToolStripMenuItem.Text = "Advanced Disk Wipe";
            // 
            // helpMenuStrip
            // 
            this.helpMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpMenuStrip.Name = "helpMenuStrip";
            this.helpMenuStrip.Size = new System.Drawing.Size(65, 29);
            this.helpMenuStrip.Text = "Help";
            // 
            // showHelpToolStripMenuItem
            // 
            this.showHelpToolStripMenuItem.Name = "showHelpToolStripMenuItem";
            this.showHelpToolStripMenuItem.Size = new System.Drawing.Size(200, 34);
            this.showHelpToolStripMenuItem.Text = "Show Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(200, 34);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // sharpEncryptLabel
            // 
            this.sharpEncryptLabel.AutoSize = true;
            this.sharpEncryptLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sharpEncryptLabel.Location = new System.Drawing.Point(128, 59);
            this.sharpEncryptLabel.Name = "sharpEncryptLabel";
            this.sharpEncryptLabel.Size = new System.Drawing.Size(357, 71);
            this.sharpEncryptLabel.TabIndex = 3;
            this.sharpEncryptLabel.Text = "SharpEncrypt";
            this.sharpEncryptLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(22, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 116);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button2.Location = new System.Drawing.Point(572, 59);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 71);
            this.button2.TabIndex = 6;
            this.button2.Text = "+";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.Location = new System.Drawing.Point(491, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 71);
            this.button1.TabIndex = 7;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button3.Location = new System.Drawing.Point(653, 59);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 71);
            this.button3.TabIndex = 8;
            this.button3.Text = "+";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // folderMenuStrip
            // 
            this.folderMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.folderMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shareKeysToolStripMenuItem,
            this.decryptPermanentlyToolStripMenuItem,
            this.decryptTemporarilyToolStripMenuItem,
            this.removeFromListToolStripMenuItem,
            this.openExplorerHereToolStripMenuItem,
            this.addSecuredFolderToolStripMenuItem});
            this.folderMenuStrip.Name = "folderMenuStrip";
            this.folderMenuStrip.Size = new System.Drawing.Size(361, 196);
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
            this.openExplorerHereToolStripMenuItem.Click += new System.EventHandler(this.openExplorerHereToolStripMenuItem_Click);
            // 
            // addSecuredFolderToolStripMenuItem
            // 
            this.addSecuredFolderToolStripMenuItem.Name = "addSecuredFolderToolStripMenuItem";
            this.addSecuredFolderToolStripMenuItem.Size = new System.Drawing.Size(360, 32);
            this.addSecuredFolderToolStripMenuItem.Text = "Add Secured Folder";
            // 
            // securedFoldersGrid
            // 
            this.securedFoldersGrid.AllowUserToAddRows = false;
            this.securedFoldersGrid.AllowUserToDeleteRows = false;
            this.securedFoldersGrid.AllowUserToOrderColumns = true;
            this.securedFoldersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.securedFoldersGrid.BackgroundColor = System.Drawing.Color.White;
            this.securedFoldersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.securedFoldersGrid.ColumnHeadersHeight = 34;
            this.securedFoldersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Folder});
            this.securedFoldersGrid.ContextMenuStrip = this.folderMenuStrip;
            this.securedFoldersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.securedFoldersGrid.GridColor = System.Drawing.Color.White;
            this.securedFoldersGrid.Location = new System.Drawing.Point(3, 3);
            this.securedFoldersGrid.Name = "securedFoldersGrid";
            this.securedFoldersGrid.ReadOnly = true;
            this.securedFoldersGrid.RowHeadersVisible = false;
            this.securedFoldersGrid.RowHeadersWidth = 62;
            this.securedFoldersGrid.RowTemplate.Height = 28;
            this.securedFoldersGrid.Size = new System.Drawing.Size(958, 366);
            this.securedFoldersGrid.TabIndex = 0;
            // 
            // fileMenuStrip
            // 
            this.fileMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.fileMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.removeFromListButKeepSecuredToolStripMenuItem,
            this.stopSecuringAndRemoveFromListToolStripMenuItem,
            this.shareKeysToolStripMenuItem1,
            this.showInFolderToolStripMenuItem,
            this.renameToOriginalToolStripMenuItem,
            this.clearRecentFilesListToolStripMenuItem});
            this.fileMenuStrip.Name = "contextMenuStrip1";
            this.fileMenuStrip.Size = new System.Drawing.Size(364, 228);
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
            // Folder
            // 
            this.Folder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Folder.HeaderText = "Folder";
            this.Folder.MinimumWidth = 8;
            this.Folder.Name = "Folder";
            this.Folder.ReadOnly = true;
            // 
            // File
            // 
            this.File.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.File.HeaderText = "File";
            this.File.MinimumWidth = 8;
            this.File.Name = "File";
            this.File.ReadOnly = true;
            // 
            // Time
            // 
            this.Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Time.HeaderText = "Time";
            this.Time.MinimumWidth = 8;
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // Secured
            // 
            this.Secured.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Secured.HeaderText = "Secured";
            this.Secured.MinimumWidth = 8;
            this.Secured.Name = "Secured";
            this.Secured.ReadOnly = true;
            // 
            // Algorithm
            // 
            this.Algorithm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Algorithm.HeaderText = "Algorithm";
            this.Algorithm.MinimumWidth = 8;
            this.Algorithm.Name = "Algorithm";
            this.Algorithm.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 582);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.sharpEncryptLabel);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.tabs);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(1018, 638);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabs.ResumeLayout(false);
            this.recentFiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.recentFilesGrid)).EndInit();
            this.securedFolders.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.folderMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.securedFoldersGrid)).EndInit();
            this.fileMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage recentFiles;
        private System.Windows.Forms.TabPage securedFolders;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuStripItem;
        private System.Windows.Forms.ToolStripMenuItem openSecuredToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem secureToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem stopSecuringToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem securedFoldersToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem anonymousRenameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem renameToOriginalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addSecuredFolderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem secureDeleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem includeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importPublicKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportMyPublicKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem debugMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem helpMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diskToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wipeFreeDiskSpaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advancedDiskWipeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wipeDiskSpaceAfterSecureDeleteToolStripMenuItem;
        private System.Windows.Forms.Label sharpEncryptLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView recentFilesGrid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ContextMenuStrip folderMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem shareKeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decryptPermanentlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decryptTemporarilyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFromListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openExplorerHereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSecuredFolderToolStripMenuItem;
        private System.Windows.Forms.DataGridView securedFoldersGrid;
        private System.Windows.Forms.ContextMenuStrip fileMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFromListButKeepSecuredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopSecuringAndRemoveFromListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shareKeysToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showInFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToOriginalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearRecentFilesListToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn File;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Secured;
        private System.Windows.Forms.DataGridViewTextBoxColumn Algorithm;
        private System.Windows.Forms.DataGridViewTextBoxColumn Folder;
    }
}

