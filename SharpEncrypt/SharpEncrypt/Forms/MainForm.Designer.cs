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
            this.recentFiles = new System.Windows.Forms.TabPage();
            this.RecentFilesGrid = new System.Windows.Forms.DataGridView();
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
            this.ClearRecentFilesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securedFolders = new System.Windows.Forms.TabPage();
            this.SecuredFoldersGrid = new System.Windows.Forms.DataGridView();
            this.Folder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FolderMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShareKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DecryptPermanentlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DecryptTemporarilyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveFolderFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenExplorerHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddSecuredFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.IncludeSubfolders = new System.Windows.Forms.ToolStripMenuItem();
            this.Debug = new System.Windows.Forms.ToolStripMenuItem();
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KeyManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportPublicKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportMyPublicKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GenerateNewKeyPairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UseADifferentPasswordForEachFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeSessionPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetAllSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebugMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.ValidateContainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewCompletedJobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DiskToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WipeFreeDiskSpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdvancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdvancedDiskWipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.HomeFolderToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.OpenHomeFolder = new System.Windows.Forms.Button();
            this.OpenHomeFolderToolTip = new System.Windows.Forms.ToolTip(this.components);
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
            this.RecentFilesGrid.AllowUserToResizeRows = false;
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
            this.RecentFilesGrid.RowTemplate.ContextMenuStrip = this.FileMenuStrip;
            this.RecentFilesGrid.RowTemplate.Height = 28;
            this.RecentFilesGrid.RowTemplate.ReadOnly = true;
            this.RecentFilesGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.RecentFilesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RecentFilesGrid.Size = new System.Drawing.Size(1054, 366);
            this.RecentFilesGrid.TabIndex = 1;
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
            this.ClearRecentFilesListToolStripMenuItem});
            this.FileMenuStrip.Name = "contextMenuStrip1";
            this.FileMenuStrip.Size = new System.Drawing.Size(389, 228);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(388, 32);
            this.OpenToolStripMenuItem.Text = "Open";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // RemoveFileFromListButKeepSecuredToolStripMenuItem
            // 
            this.RemoveFileFromListButKeepSecuredToolStripMenuItem.Name = "RemoveFileFromListButKeepSecuredToolStripMenuItem";
            this.RemoveFileFromListButKeepSecuredToolStripMenuItem.Size = new System.Drawing.Size(388, 32);
            this.RemoveFileFromListButKeepSecuredToolStripMenuItem.Text = "Remove file from list but keep secured";
            this.RemoveFileFromListButKeepSecuredToolStripMenuItem.Click += new System.EventHandler(this.RemoveFileFromListButKeepSecuredToolStripMenuItem_Click);
            // 
            // StopSecuringAndRemoveFromListToolStripMenuItem
            // 
            this.StopSecuringAndRemoveFromListToolStripMenuItem.Name = "StopSecuringAndRemoveFromListToolStripMenuItem";
            this.StopSecuringAndRemoveFromListToolStripMenuItem.Size = new System.Drawing.Size(388, 32);
            this.StopSecuringAndRemoveFromListToolStripMenuItem.Text = "Stop securing and remove from list";
            this.StopSecuringAndRemoveFromListToolStripMenuItem.Click += new System.EventHandler(this.StopSecuringAndRemoveFromListToolStripMenuItem_Click);
            // 
            // ShareKeysToolStripMenuItem1
            // 
            this.ShareKeysToolStripMenuItem1.Name = "ShareKeysToolStripMenuItem1";
            this.ShareKeysToolStripMenuItem1.Size = new System.Drawing.Size(388, 32);
            this.ShareKeysToolStripMenuItem1.Text = "Share keys";
            this.ShareKeysToolStripMenuItem1.Click += new System.EventHandler(this.ShareKeysToolStripMenuItem1_Click);
            // 
            // ShowInFolderToolStripMenuItem
            // 
            this.ShowInFolderToolStripMenuItem.Name = "ShowInFolderToolStripMenuItem";
            this.ShowInFolderToolStripMenuItem.Size = new System.Drawing.Size(388, 32);
            this.ShowInFolderToolStripMenuItem.Text = "Show in folder";
            this.ShowInFolderToolStripMenuItem.Click += new System.EventHandler(this.ShowInFolderToolStripMenuItem_Click);
            // 
            // RenameToOriginalToolStripMenuItem
            // 
            this.RenameToOriginalToolStripMenuItem.Name = "RenameToOriginalToolStripMenuItem";
            this.RenameToOriginalToolStripMenuItem.Size = new System.Drawing.Size(388, 32);
            this.RenameToOriginalToolStripMenuItem.Text = "Rename to original";
            this.RenameToOriginalToolStripMenuItem.Click += new System.EventHandler(this.RenameToOriginalToolStripMenuItem_Click);
            // 
            // ClearRecentFilesListToolStripMenuItem
            // 
            this.ClearRecentFilesListToolStripMenuItem.Name = "ClearRecentFilesListToolStripMenuItem";
            this.ClearRecentFilesListToolStripMenuItem.Size = new System.Drawing.Size(388, 32);
            this.ClearRecentFilesListToolStripMenuItem.Text = "Clear Recent Files list";
            this.ClearRecentFilesListToolStripMenuItem.Click += new System.EventHandler(this.ClearRecentFilesListToolStripMenuItem_Click);
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
            this.SecuredFoldersGrid.AllowUserToResizeRows = false;
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
            this.SecuredFoldersGrid.RowTemplate.ContextMenuStrip = this.FolderMenuStrip;
            this.SecuredFoldersGrid.RowTemplate.Height = 28;
            this.SecuredFoldersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
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
            this.ShareKeysToolStripMenuItem,
            this.DecryptPermanentlyToolStripMenuItem,
            this.DecryptTemporarilyToolStripMenuItem,
            this.RemoveFolderFromListToolStripMenuItem,
            this.OpenExplorerHereToolStripMenuItem,
            this.AddSecuredFolderToolStripMenuItem});
            this.FolderMenuStrip.Name = "folderMenuStrip";
            this.FolderMenuStrip.Size = new System.Drawing.Size(413, 196);
            // 
            // ShareKeysToolStripMenuItem
            // 
            this.ShareKeysToolStripMenuItem.Name = "ShareKeysToolStripMenuItem";
            this.ShareKeysToolStripMenuItem.Size = new System.Drawing.Size(412, 32);
            this.ShareKeysToolStripMenuItem.Text = "Share keys";
            this.ShareKeysToolStripMenuItem.Click += new System.EventHandler(this.ShareKeysToolStripMenuItem_Click);
            // 
            // DecryptPermanentlyToolStripMenuItem
            // 
            this.DecryptPermanentlyToolStripMenuItem.Name = "DecryptPermanentlyToolStripMenuItem";
            this.DecryptPermanentlyToolStripMenuItem.Size = new System.Drawing.Size(412, 32);
            this.DecryptPermanentlyToolStripMenuItem.Text = "Decrypt permanently";
            this.DecryptPermanentlyToolStripMenuItem.Click += new System.EventHandler(this.DecryptPermanentlyToolStripMenuItem_Click);
            // 
            // DecryptTemporarilyToolStripMenuItem
            // 
            this.DecryptTemporarilyToolStripMenuItem.Name = "DecryptTemporarilyToolStripMenuItem";
            this.DecryptTemporarilyToolStripMenuItem.Size = new System.Drawing.Size(412, 32);
            this.DecryptTemporarilyToolStripMenuItem.Text = "Decrypt temporarily";
            this.DecryptTemporarilyToolStripMenuItem.Click += new System.EventHandler(this.DecryptTemporarilyToolStripMenuItem_Click);
            // 
            // RemoveFolderFromListToolStripMenuItem
            // 
            this.RemoveFolderFromListToolStripMenuItem.Name = "RemoveFolderFromListToolStripMenuItem";
            this.RemoveFolderFromListToolStripMenuItem.Size = new System.Drawing.Size(412, 32);
            this.RemoveFolderFromListToolStripMenuItem.Text = "Remove folder from list but keep secured";
            this.RemoveFolderFromListToolStripMenuItem.Click += new System.EventHandler(this.RemoveFolderFromListToolStripMenuItem_Click);
            // 
            // OpenExplorerHereToolStripMenuItem
            // 
            this.OpenExplorerHereToolStripMenuItem.Name = "OpenExplorerHereToolStripMenuItem";
            this.OpenExplorerHereToolStripMenuItem.Size = new System.Drawing.Size(412, 32);
            this.OpenExplorerHereToolStripMenuItem.Text = "Open Explorer here";
            this.OpenExplorerHereToolStripMenuItem.Click += new System.EventHandler(this.OpenExplorerHereToolStripMenuItem_Click);
            // 
            // AddSecuredFolderToolStripMenuItem
            // 
            this.AddSecuredFolderToolStripMenuItem.Name = "AddSecuredFolderToolStripMenuItem";
            this.AddSecuredFolderToolStripMenuItem.Size = new System.Drawing.Size(412, 32);
            this.AddSecuredFolderToolStripMenuItem.Text = "Add Secured Folder";
            this.AddSecuredFolderToolStripMenuItem.Click += new System.EventHandler(this.AddSecuredFolderToolStripMenuItem_Click);
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
            this.OpenSecuredToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.OpenSecuredToolStripMenuItem.Text = "Open Secured";
            this.OpenSecuredToolStripMenuItem.Click += new System.EventHandler(this.OpenSecuredToolStripMenuItem_Click);
            // 
            // SecureToolStripMenuItem
            // 
            this.SecureToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SecureToolStripMenuItem.Image")));
            this.SecureToolStripMenuItem.Name = "SecureToolStripMenuItem";
            this.SecureToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.SecureToolStripMenuItem.Text = "Secure";
            this.SecureToolStripMenuItem.Click += new System.EventHandler(this.SecureToolStripMenuItem_Click);
            // 
            // StopSecuringToolStripMenuItem
            // 
            this.StopSecuringToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("StopSecuringToolStripMenuItem.Image")));
            this.StopSecuringToolStripMenuItem.Name = "StopSecuringToolStripMenuItem";
            this.StopSecuringToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.StopSecuringToolStripMenuItem.Text = "Stop Securing";
            this.StopSecuringToolStripMenuItem.Click += new System.EventHandler(this.StopSecuringToolStripMenuItem_Click);
            // 
            // SecuredFoldersToolStripMenuItem
            // 
            this.SecuredFoldersToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SecuredFoldersToolStripMenuItem.Image")));
            this.SecuredFoldersToolStripMenuItem.Name = "SecuredFoldersToolStripMenuItem";
            this.SecuredFoldersToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.SecuredFoldersToolStripMenuItem.Text = "Secured Folders";
            this.SecuredFoldersToolStripMenuItem.Click += new System.EventHandler(this.SecuredFoldersToolStripMenuItem_Click);
            // 
            // AnonymousRenameToolStripMenuItem
            // 
            this.AnonymousRenameToolStripMenuItem.Name = "AnonymousRenameToolStripMenuItem";
            this.AnonymousRenameToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.AnonymousRenameToolStripMenuItem.Text = "Anonymous Rename";
            this.AnonymousRenameToolStripMenuItem.Click += new System.EventHandler(this.AnonymousRenameToolStripMenuItem_Click);
            // 
            // RenameToOriginalToolStripMenuItem1
            // 
            this.RenameToOriginalToolStripMenuItem1.Name = "RenameToOriginalToolStripMenuItem1";
            this.RenameToOriginalToolStripMenuItem1.Size = new System.Drawing.Size(279, 34);
            this.RenameToOriginalToolStripMenuItem1.Text = "Rename to original";
            this.RenameToOriginalToolStripMenuItem1.Click += new System.EventHandler(this.RenameToOriginalToolStripMenuItem1_Click);
            // 
            // AddSecuredFolderToolStripMenuItem1
            // 
            this.AddSecuredFolderToolStripMenuItem1.Name = "AddSecuredFolderToolStripMenuItem1";
            this.AddSecuredFolderToolStripMenuItem1.Size = new System.Drawing.Size(279, 34);
            this.AddSecuredFolderToolStripMenuItem1.Text = "Add Secured Folder";
            this.AddSecuredFolderToolStripMenuItem1.Click += new System.EventHandler(this.AddSecuredFolderToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(276, 6);
            // 
            // SecureDeleteToolStripMenuItem
            // 
            this.SecureDeleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SecureDeleteToolStripMenuItem.Image")));
            this.SecureDeleteToolStripMenuItem.Name = "SecureDeleteToolStripMenuItem";
            this.SecureDeleteToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.SecureDeleteToolStripMenuItem.Text = "Secure Delete";
            this.SecureDeleteToolStripMenuItem.Click += new System.EventHandler(this.SecureDeleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(276, 6);
            // 
            // Advanced
            // 
            this.Advanced.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OneTimePadToolStripMenuItem});
            this.Advanced.Name = "Advanced";
            this.Advanced.Size = new System.Drawing.Size(279, 34);
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
            this.IncludeSubfolders,
            this.Debug,
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem,
            this.KeyManagementToolStripMenuItem,
            this.UseADifferentPasswordForEachFile,
            this.ChangeSessionPasswordToolStripMenuItem,
            this.ResetAllSettingsToolStripMenuItem});
            this.OptionsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OptionsToolStripMenuItem.Image")));
            this.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
            this.OptionsToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
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
            this.LanguageToolStripMenuItem.Size = new System.Drawing.Size(426, 34);
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
            // IncludeSubfolders
            // 
            this.IncludeSubfolders.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.IncludeSubfolders.Name = "IncludeSubfolders";
            this.IncludeSubfolders.Size = new System.Drawing.Size(426, 34);
            this.IncludeSubfolders.Text = "Include subfolders";
            this.IncludeSubfolders.Click += new System.EventHandler(this.IncludeToolStripMenuItem_Click);
            // 
            // Debug
            // 
            this.Debug.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Debug.Name = "Debug";
            this.Debug.Size = new System.Drawing.Size(426, 34);
            this.Debug.Text = "Debug";
            this.Debug.Click += new System.EventHandler(this.DebugToolStripMenuItem_Click);
            // 
            // WipeDiskSpaceAfterSecureDeleteToolStripMenuItem
            // 
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Name = "WipeDiskSpaceAfterSecureDeleteToolStripMenuItem";
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Size = new System.Drawing.Size(426, 34);
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Text = "Wipe free disk space after secure delete";
            this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Click += new System.EventHandler(this.WipeDiskSpaceAfterSecureDeleteToolStripMenuItem_Click);
            // 
            // KeyManagementToolStripMenuItem
            // 
            this.KeyManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportPublicKeyToolStripMenuItem,
            this.ExportMyPublicKeyToolStripMenuItem,
            this.GenerateNewKeyPairToolStripMenuItem});
            this.KeyManagementToolStripMenuItem.Name = "KeyManagementToolStripMenuItem";
            this.KeyManagementToolStripMenuItem.Size = new System.Drawing.Size(426, 34);
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
            // UseADifferentPasswordForEachFile
            // 
            this.UseADifferentPasswordForEachFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.UseADifferentPasswordForEachFile.Name = "UseADifferentPasswordForEachFile";
            this.UseADifferentPasswordForEachFile.Size = new System.Drawing.Size(426, 34);
            this.UseADifferentPasswordForEachFile.Text = "Use a different password for each file";
            this.UseADifferentPasswordForEachFile.Click += new System.EventHandler(this.UseADifferentPasswordForEachFileToolStripMenuItem_Click);
            // 
            // ChangeSessionPasswordToolStripMenuItem
            // 
            this.ChangeSessionPasswordToolStripMenuItem.Name = "ChangeSessionPasswordToolStripMenuItem";
            this.ChangeSessionPasswordToolStripMenuItem.Size = new System.Drawing.Size(426, 34);
            this.ChangeSessionPasswordToolStripMenuItem.Text = "Change session password";
            this.ChangeSessionPasswordToolStripMenuItem.Click += new System.EventHandler(this.ChangeSessionPasswordToolStripMenuItem_Click);
            // 
            // ResetAllSettingsToolStripMenuItem
            // 
            this.ResetAllSettingsToolStripMenuItem.Name = "ResetAllSettingsToolStripMenuItem";
            this.ResetAllSettingsToolStripMenuItem.Size = new System.Drawing.Size(426, 34);
            this.ResetAllSettingsToolStripMenuItem.Text = "Reset all settings";
            this.ResetAllSettingsToolStripMenuItem.Click += new System.EventHandler(this.ResetAllSettingsToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ExitToolStripMenuItem.Image")));
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(279, 34);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // DebugMenuStrip
            // 
            this.DebugMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ValidateContainerToolStripMenuItem,
            this.ViewCompletedJobsToolStripMenuItem,
            this.LoggingToolStripMenuItem});
            this.DebugMenuStrip.Enabled = false;
            this.DebugMenuStrip.Name = "DebugMenuStrip";
            this.DebugMenuStrip.Size = new System.Drawing.Size(82, 29);
            this.DebugMenuStrip.Text = "Debug";
            // 
            // ValidateContainerToolStripMenuItem
            // 
            this.ValidateContainerToolStripMenuItem.Name = "ValidateContainerToolStripMenuItem";
            this.ValidateContainerToolStripMenuItem.Size = new System.Drawing.Size(285, 34);
            this.ValidateContainerToolStripMenuItem.Text = "Validate Container";
            this.ValidateContainerToolStripMenuItem.Click += new System.EventHandler(this.ValidateContainerToolStripMenuItem_Click);
            // 
            // ViewCompletedJobsToolStripMenuItem
            // 
            this.ViewCompletedJobsToolStripMenuItem.Name = "ViewCompletedJobsToolStripMenuItem";
            this.ViewCompletedJobsToolStripMenuItem.Size = new System.Drawing.Size(285, 34);
            this.ViewCompletedJobsToolStripMenuItem.Text = "View Completed Jobs";
            this.ViewCompletedJobsToolStripMenuItem.Click += new System.EventHandler(this.ViewJobsToolStripMenuItem_Click);
            // 
            // LoggingToolStripMenuItem
            // 
            this.LoggingToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LoggingToolStripMenuItem.Name = "LoggingToolStripMenuItem";
            this.LoggingToolStripMenuItem.Size = new System.Drawing.Size(285, 34);
            this.LoggingToolStripMenuItem.Text = "Logging";
            this.LoggingToolStripMenuItem.Click += new System.EventHandler(this.LoggingToolStripMenuItem_Click);
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
            this.WipeFreeDiskSpaceToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("WipeFreeDiskSpaceToolStripMenuItem.Image")));
            this.WipeFreeDiskSpaceToolStripMenuItem.Name = "WipeFreeDiskSpaceToolStripMenuItem";
            this.WipeFreeDiskSpaceToolStripMenuItem.Size = new System.Drawing.Size(284, 34);
            this.WipeFreeDiskSpaceToolStripMenuItem.Text = "Wipe Free Disk Space";
            this.WipeFreeDiskSpaceToolStripMenuItem.Click += new System.EventHandler(this.WipeFreeDiskSpaceToolStripMenuItem_Click);
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
            this.AdvancedDiskWipeToolStripMenuItem.Click += new System.EventHandler(this.AdvancedDiskWipeToolStripMenuItem_Click);
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
        private System.Windows.Forms.ToolStripMenuItem AdvancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AdvancedDiskWipeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WipeDiskSpaceAfterSecureDeleteToolStripMenuItem;
        private System.Windows.Forms.Label AppLabel;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Button AddSecuredGUIButton;
        private System.Windows.Forms.DataGridView RecentFilesGrid;
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
        private System.Windows.Forms.ToolStripMenuItem ClearRecentFilesListToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Folder;
        private System.Windows.Forms.Button PasswordManagement;
        private System.Windows.Forms.ToolTip OpenSecuredToolTip;
        private System.Windows.Forms.ToolTip AddSecuredFileToolTip;
        private System.Windows.Forms.ToolTip ShareToolTip;
        private System.Windows.Forms.ToolTip PasswordManagementToolTip;
        private System.Windows.Forms.ToolTip HomeFolderToolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileColumnHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeColumnHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn SecuredColumnHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlgorithmColumnHeader;
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
    }
}

