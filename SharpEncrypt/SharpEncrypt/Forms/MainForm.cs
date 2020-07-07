﻿using SecureEraseLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Exceptions;
using SharpEncrypt.ExtensionClasses;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;
using SharpEncrypt.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    internal partial class MainForm : Form
    {
        private readonly ComponentResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly List<ExcludedResource> ExcludedResources = new List<ExcludedResource>();
        private readonly FileSystemManager FileSystemManager = new FileSystemManager();
        private readonly TaskManager TaskManager = new TaskManager();
        private readonly PathHelper PathHelper = new PathHelper();
        private SharpEncryptSettings _settings = new SharpEncryptSettings();

        #region Delegates and Events

        private delegate void SettingsChangeEventHandler(string settingsPropertyName, object value);
        private delegate void SettingsWriteEventHandler(SharpEncryptSettings settings);
        private delegate void FileSecuredEventHandler(string filePath, string newFilePath);
        private delegate void FolderSecuredEventHandler(string dirPath);
        private delegate void SecuredFilesAddedEventHandler(IEnumerable<FileDataGridItemModel> model);
        private delegate void ReadSecuredFileListEventHandler(IEnumerable<FileDataGridItemModel> models);
        private delegate void ReadSecuredFolderListEventHandler(IEnumerable<string> folders);
        private delegate void ExcludedResourceListReadEventHandler(IEnumerable<ExcludedResource> exclusions);
        private delegate void SettingsFileReadEventHandler(SharpEncryptSettings settings);
        private delegate void TaskExceptionOccurredEventHandler(Exception exception);

        private event SettingsWriteEventHandler SettingsWriteRequired;
        private event SettingsChangeEventHandler SettingsChangeRequired;
        private event FileSecuredEventHandler FileSecured;
        private event FolderSecuredEventHandler FolderSecured;
        private event SecuredFilesAddedEventHandler SecuredFileAdded;
        private event ReadSecuredFileListEventHandler SecureFileListRead;
        private event ReadSecuredFolderListEventHandler SecuredFolderListRead;
        private event SettingsFileReadEventHandler SettingsFileRead;
        private event TaskExceptionOccurredEventHandler TaskException;
        private event ExcludedResourceListReadEventHandler ExcludedResourcesListRead;
        #endregion

        private string Password { get; set; }

        public SharpEncryptSettings Settings {
            get => _settings;
            private set {
                _settings = value;
                SetUIOptions();
            }
        }

        public MainForm() 
        {
            InitializeComponent();
            SettingsWriteRequired += SettingsWriterHandler;
            SettingsChangeRequired += SettingsChangeHandler;

            FormClosing += FormClosingHandler;
            Resize += FormResize;
            NotifyIcon.DoubleClick += NotifyIcon_DoubleClick;

            FileSecured += OnFileSecured;
            FolderSecured += OnFolderSecured;
            SecuredFileAdded += SecureFilesAdded;
            SecureFileListRead += OnSecureFileListRead;
            SecuredFolderListRead += OnSecuredFolderListRead;
            SettingsFileRead += OnSettingsFileRead;
            TaskException += ExceptionHandler;

            RecentFilesGrid.DragDrop += RecentFilesGrid_DragDrop;
            RecentFilesGrid.DragEnter += RecentFilesGrid_DragEnter;

            SecuredFoldersGrid.DragDrop += SecuredFoldersGrid_DragDrop;
            SecuredFoldersGrid.DragEnter += SecuredFoldersGrid_DragEnter;

            TaskManager.TaskCompleted += TaskCompleted;
            TaskManager.ExceptionOccurred += ExceptionHandler;

            ExcludedResourcesListRead += OnExcludedResourcesListRead;
        }

        private void MainForm_Load(object sender, EventArgs e) => LoadApplication();

        #region Misc methods

        private static void SecureFolders(params string[] folders)
        {
            if (folders != null)
            {
                foreach (var folder in folders)
                {
                    if (Directory.Exists(folder))
                    {
                        //logic here
                    }
                }
            }
        }

        private static void SecureFiles(params string[] filePaths)
        {
            if (filePaths != null)
            {
                foreach (var filePath in filePaths)
                {
                    if (File.Exists(filePath))
                    {
                        //logic here
                    }
                }
            }
        }

        private void AddSecured()
        {
            using (var dialog = GetAllFilesDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var fileToSecure = dialog.FileName;
                    //secure file
                    //add it to datagridview
                    FileSecured?.Invoke(fileToSecure, fileToSecure);
                }
            }
        }

        private void OpenSecured()
        {
            using (var dialog = GetSEEFFilesDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var securedFilePath = dialog.FileName;
                    //open secured file
                }
            }
        }

        private void LoadApplication()
        {
            SetApplicationSettings();
            LoadExclusionList();
            LoadRecentFilesList();
            LoadSecuredFoldersList();     
        }

        private void ReloadApplication(bool changeLanguage)
        {
            if (changeLanguage)
                ChangeLanguage(Settings.LanguageCode);
        }

        private void SetApplicationSettings()
        {
            var settingsFilePath = PathHelper.AppSettingsPath;            

            if (!File.Exists(settingsFilePath))
            {
                SettingsWriteRequired?.Invoke(Settings);
            }
            else
            {
                TaskManager.AddTask(new ReadSettingsFileTask(settingsFilePath));
            }            
        }

        private void SetUIOptions()
        {
            InvokeOnControl(new MethodInvoker(delegate ()
            {
                Debug.Checked = Settings.DebugEnabled;
                IncludeSubfolders.Checked = Settings.IncludeSubfolders;
                UseADifferentPasswordForEachFile.Checked = Settings.UseADifferentPasswordForEachFile;
                WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Checked = Settings.WipeFreeSpaceAfterSecureDelete;
                LoggingToolStripMenuItem.Checked = Settings.Logging;

                if (Debug.Checked)
                {
                    DebugMenuStrip.Enabled = true;
                }
                else
                {
                    DebugMenuStrip.Enabled = false;
                }
            }));
        }

        private void LoadExclusionList()
        {
            TaskManager.AddTask(new ReadResourceExclusionListTask(PathHelper.ExclusionFile));
        }

        private void LoadSecuredFoldersList()
        {
            TaskManager.AddTask(new ReadSecuredFoldersListTask(PathHelper.SecuredFoldersListFileName));
        }

        private void LoadRecentFilesList()
        {
            TaskManager.AddTask(new ReadSecuredFilesListTask(PathHelper.SecuredFilesListFile));
        }

        private static void CloseApplication()
        {
            Application.Exit();
        }

        private static string[] ExtractPaths(DragEventArgs args)
        {
            var paths = args.Data.GetData(DataFormats.FileDrop, false);
            if (paths is string[] filePaths)
            {
                return filePaths;
            }
            return null;
        }

        #endregion

        #region File context menu items

        private void ClearRecentFilesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeOnControl(new MethodInvoker(delegate ()
            {
                RecentFilesGrid.Rows.Clear();
                RecentFilesGrid.Refresh();
            }));        
        }

        private void RemoveFileFromListButKeepSecuredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var removals = new List<string>();

            foreach (DataGridViewRow selectedRow in RecentFilesGrid.SelectedRows)
            {
                if (selectedRow.Cells[2].Value is string securedFilePath)
                {
                    removals.Add(securedFilePath);
                }
            }

            InvokeOnControl(new MethodInvoker(delegate ()
            {
                foreach (DataGridViewRow row in RecentFilesGrid.SelectedRows)
                    RecentFilesGrid.Rows.Remove(row);

                RecentFilesGrid.Refresh();
            }));

            TaskManager.AddTask(new WriteResourceExclusionListTask(PathHelper.ExclusionFile, true, removals.Select(x => new ExcludedResource { ResourceType = ResourceType.File, URI = x })));
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void StopSecuringAndRemoveFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ShareKeysToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void ShowInFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in RecentFilesGrid.SelectedRows)
            {
                if(row.Cells[2].Value is string securedFilePath)
                {
                    if (File.Exists(securedFilePath))
                    {
                        var dir = Path.GetDirectoryName(securedFilePath);
                        if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
                        {
                            Process.Start(dir);
                        }
                    }
                }
            }
        }

        private void RenameToOriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Folder context menu items

        private void RemoveFolderFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var directoryURIs = new List<string>();
            foreach (DataGridViewRow row in SecuredFoldersGrid.SelectedRows)
            {
                if(row.Cells[0].Value is string dir)
                {
                    directoryURIs.Add(dir);
                }                
            }

            InvokeOnControl(new MethodInvoker(delegate ()
            {
                foreach (DataGridViewRow row in SecuredFoldersGrid.SelectedRows)
                    SecuredFoldersGrid.Rows.Remove(row);

                SecuredFoldersGrid.Refresh();
            }));

            TaskManager.AddTask(new WriteResourceExclusionListTask(PathHelper.ExclusionFile, true, directoryURIs.Select(x => new ExcludedResource { ResourceType = ResourceType.Folder, URI = x })));
        }

        private void ShareKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DecryptPermanentlyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DecryptTemporarilyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void OpenExplorerHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in SecuredFoldersGrid.SelectedRows)
            {
                if(row.Cells[0].Value is string folderPath)
                {
                    if (Directory.Exists(folderPath))
                    {
                        Process.Start(folderPath);
                    }
                }
            }
        }

        private void AddSecuredFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSecuredFolder();
        }

        #endregion

        #region File menu items

        private void OpenSecuredToolStripMenuItem_Click(object sender, EventArgs e) => OpenSecured();

        private void SecureToolStripMenuItem_Click(object sender, EventArgs e) => AddSecured();

        private void StopSecuringToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SecuredFoldersToolStripMenuItem_Click(object sender, EventArgs e) => Tabs.SelectedTab = Tabs.TabPages[1];

        private void AnonymousRenameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void RenameToOriginalToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void AddSecuredFolderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddSecuredFolder();
        }

        private void SecureDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var allFilesDialog = GetAllFilesDialog())
            {
                if(allFilesDialog.ShowDialog() == DialogResult.OK)
                {
                    TaskManager.AddTask(new SecureDeleteFileTask(allFilesDialog.FileName));
                }
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExitRequested();
        }

        private void SecureFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Settings.OTPDisclaimerHide)
            {
                using (var showOnceDialog = new ShowOnceDialog(ResourceManager.GetString("OTPDisclaimer")))
                {
                    if (showOnceDialog.ShowDialog() != DialogResult.OK)
                    {
                        if (showOnceDialog.IsChecked)
                            SettingsChangeRequired?.Invoke("OTPDisclaimerHide", true);
                        return;
                    }
                }
            }

            using (var openFileDialog = GetAllFilesDialog())
            {
                openFileDialog.Title = ResourceManager.GetString("SelectFile");
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var keyFileOpenFileDialog = new OpenFileDialog())
                    {
                        keyFileOpenFileDialog.Title = ResourceManager.GetString("SelectKeyFile");
                        keyFileOpenFileDialog.Filter = ResourceManager.GetString("SharpEncryptOTPKeyFilter");
                        if(keyFileOpenFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            TaskManager.AddTask(new OneTimePadTransformTask(openFileDialog.FileName, keyFileOpenFileDialog.FileName));
                        }
                    }
                }
            }
        }

        private void DecryptFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = ResourceManager.GetString("SelectFile");
                openFileDialog.Filter = ResourceManager.GetString("SharpEncryptOTPEncryptedFile");
                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var openKeyFileDialog = new OpenFileDialog())
                    {
                        openKeyFileDialog.Title = ResourceManager.GetString("SelectKeyFile");
                        openKeyFileDialog.Filter = ResourceManager.GetString("SharpEncryptOTPKeyFilter");
                        if(openKeyFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            TaskManager.AddTask(new OneTimePadTransformTask(openFileDialog.FileName, openKeyFileDialog.FileName));
                        }
                    }
                }
            }
        }

        private void ChangeSessionPasswordToolStripMenuItem_Click(object sender, EventArgs e) => SetSessionPassword();

        private void SetSessionPassword()
        {
            using (var dialog = new PasswordInputDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Password = dialog.Password;
                }
            }
        }

        private void ResetAllSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                ResourceManager.GetString("AreYouSure?"),
                string.Empty,
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var newSettingsObj = new SharpEncryptSettings();
                var changeLang = Settings.LanguageCode != newSettingsObj.LanguageCode;
                SettingsWriteRequired?.Invoke(newSettingsObj);

                Settings = newSettingsObj;

                ReloadApplication(changeLang);
            }
        }

        private void ImportPublicKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var importPubKeyForm = new ImportPublicKeyForm())
            {
                importPubKeyForm.Show();
            }
        }

        private void ExportMyPublicKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var (@public, _) = PathHelper.KeyPairPaths;

            if (!File.Exists(@public))
            {
                MessageBox.Show(ResourceManager.GetString("PublicKeyDoesNotExist"));
            }
            else
            {
                try
                {
                    var pubKey = RSAKeyReaderHelper.GetParameters(@public);
                    using (var dialog = new SaveFileDialog())
                    {
                        dialog.Filter = ResourceManager.GetString("RSAKeyFilter");
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            RSAKeyWriterHelper.Write(dialog.FileName, pubKey);
                        }
                    }
                }
                catch (InvalidKeyException)
                {
                    MessageBox.Show(ResourceManager.GetString("InvalidKey"));
                }
            }
        }

        private void GenerateNewKeyPairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var (@public, @private) = PathHelper.KeyPairPaths;

            if (MessageBox.Show(
                ResourceManager.GetString("KeyPairDisclaimer"),
                string.Empty,
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                var (publicKey, privateKey) = RSAKeyPairHelper.GetNewKeyPair();
                RSAKeyWriterHelper.Write(@public, publicKey);
                RSAKeyWriterHelper.Write(@private, privateKey);
                //encrypt the rsa priv key (IMPORTANT)
            }
        }

        private void GenerateKeyForFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = GetAllFilesDialog())
            {
                openFileDialog.Title = ResourceManager.GetString("SelectReferenceFile");
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Title = ResourceManager.GetString("SaveKeyFile");
                        saveFileDialog.Filter = ResourceManager.GetString("SharpEncryptOTPKeyFilter");
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            TaskManager.AddTask(new OTPSaveKeyOfFileTask(saveFileDialog.FileName, openFileDialog.FileName));
                        }
                    }
                }
            }
        }

        #endregion

        #region GUI buttons

        private void OpenSecured_Click(object sender, EventArgs e) => OpenSecured();

        private void AddSecured_Click(object sender, EventArgs e) => AddSecured();

        private void ShareButton_Click(object sender, EventArgs e)
        {

        }

        private void PasswordManagement_Click(object sender, EventArgs e)
        {

        }

        private void OpenHomeFolder_Click(object sender, EventArgs e) => Process.Start(PathHelper.AppDirectory);

        #endregion

        #region Disk tools menu items

        private void WipeFreeDiskSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var hardDriveWipeDialog = new HardDriveWipeDialog())
            {
                hardDriveWipeDialog.ShowDialog();
            }
        }

        private void AdvancedDiskWipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var hardDriveWipeDialog = new AdvancedHardDriveWipeDialog())
            {
                hardDriveWipeDialog.ShowDialog();
            }
        }

        #endregion

        #region Debug menu items
        private void ValidateContainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(var openFileDialog = GetSEEFFilesDialog())
            {
                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(ContainerHelper.ValidateContainer(openFileDialog.FileName) 
                        ? string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("ValidContainer"), openFileDialog.FileName)
                        : string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("NotAValidContainer"), openFileDialog.FileName));
                }
            }
        }

        private void ViewActiveJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var activeTasksForm = new ActiveTasksForm(TaskManager))
            {
                activeTasksForm.ShowDialog();
            }
        }

        private void ViewCompletedJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var columns = new[] { ResourceManager.GetString("TaskType"), ResourceManager.GetString("Completed") };
            var rows = TaskManager.CompletedTasks.Select(x => new List<object> { x.Task.TaskType, x.Time.ToString(CultureInfo.CurrentCulture) }).ToList();
            using (var completedJobsDialog = new GenericGridForm(columns, rows, ResourceManager.GetString("CompletedJobs")))
            {
                completedJobsDialog.ShowDialog();
            }
        }

        private void LoggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsChangeRequired?.Invoke("Logging", ToggleChecked(LoggingToolStripMenuItem));
        }

        private void CancelAllFutureTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskManager.SetCancellationFlag();
        }
        #endregion

        #region Flag Menu Items
        private void DebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleEnablement(DebugMenuStrip);
            SettingsChangeRequired?.Invoke("DebugEnabled", ToggleChecked(Debug));
        }

        private void UseADifferentPasswordForEachFileToolStripMenuItem_Click(object sender, EventArgs e)
            => SettingsChangeRequired?.Invoke("UseADifferentPasswordForEachFile", ToggleChecked(UseADifferentPasswordForEachFile));

        private void IncludeToolStripMenuItem_Click(object sender, EventArgs e)
            => SettingsChangeRequired?.Invoke("IncludeSubfolders", ToggleChecked(IncludeSubfolders));

        private void WipeDiskSpaceAfterSecureDeleteToolStripMenuItem_Click(object sender, EventArgs e)
            => SettingsChangeRequired?.Invoke("WipeFreeSpaceAfterSecureDelete", ToggleChecked(WipeDiskSpaceAfterSecureDeleteToolStripMenuItem));

        private static bool ToggleChecked(ToolStripMenuItem item)
        {
            if (item.Checked)
                item.Checked = false;
            else
                item.Checked = true;

            return item.Checked;
        }

        private static bool ToggleEnablement(ToolStripMenuItem item)
        {
            if (item.Enabled)
                item.Enabled = false;
            else
                item.Enabled = true;

            return item.Enabled;
        }

        #endregion

        #region Handlers

        private void OnExcludedResourcesListRead(IEnumerable<ExcludedResource> exclusions)
        {
            var removed = exclusions.Where(x => x.ResourceType == ResourceType.File ? !File.Exists(x.URI) : !Directory.Exists(x.URI));
            ExcludedResources.AddRange(exclusions.Where(x => !removed.Any(z => z.URI.Equals(x.URI, StringComparison.InvariantCulture))));
            if(removed.Any())
                TaskManager.AddTask(new WriteResourceExclusionListTask(PathHelper.ExclusionFile, false, removed));
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowApplication();
        }

        private void FormResize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                NotifyIcon.Visible = true;
                NotifyIcon.Text = ResourceManager.GetString("AppName");
                NotifyIcon.BalloonTipText = ResourceManager.GetString("AppName");
                NotifyIcon.ShowBalloonTip(500);
                Hide();
            }
        }

        private void SecuredFoldersGrid_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void SecuredFoldersGrid_DragDrop(object sender, DragEventArgs e)
        {
            SecureFolders(ExtractPaths(e));
        }

        private void RecentFilesGrid_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void RecentFilesGrid_DragDrop(object sender, DragEventArgs e)
        {
            SecureFiles(ExtractPaths(e));
        }

        private void TaskCompleted(SharpEncryptTask task)
        {
            if (task.Result.Exception != null)
            {
                TaskException?.Invoke(task.Result.Exception);
            }
            else
            {
                switch (task.TaskType)
                {
                    case TaskType.ReadSecuredFilesListTask when task.Result.Value is IEnumerable<FileDataGridItemModel> models:
                        SecureFileListRead?.Invoke(models);
                        break;
                    case TaskType.ReadSecuredFoldersListTask when task.Result.Value is IEnumerable<string> folders:
                        SecuredFolderListRead?.Invoke(folders);
                        break;
                    case TaskType.ReadSettingsFileTask when task.Result.Value is SharpEncryptSettings settings:
                        SettingsFileRead?.Invoke(settings);
                        break;
                    case TaskType.WriteSecuredFoldersListTask when task.Result.Value is IEnumerable<string> addedDirs:
                        AddFoldersToSecuredFoldersDataGrid(addedDirs.ToArray());
                        break;
                    case TaskType.ReadResourceExclusionListTask when task.Result.Value is IEnumerable<ExcludedResource> exclusions:
                        ExcludedResourcesListRead?.Invoke(exclusions);
                        break;
                    case TaskType.SecureFolderTask when task.Result.Value is string folderPath:
                        FolderSecured?.Invoke(folderPath);
                        break;
                }
            }
        }

        private void ExceptionHandler(Exception exception)
        {
            if (Settings.Logging)
                TaskManager.AddTask(new LoggingTask(PathHelper.LoggingFilePath, exception.StackTrace));

            InvokeOnControl(new MethodInvoker(() =>
            {
                MessageBox.Show(
                    string.Format(CultureInfo.CurrentCulture,
                    ResourceManager.GetString("AnExceptionHasOccurred"),
                    exception.Message),
                    ResourceManager.GetString("Error"),
                    MessageBoxButtons.OK);
            }));
        }

        private void OnSettingsFileRead(SharpEncryptSettings settings)
        {
            Settings = settings;
            if (Settings.LanguageCode != Constants.DefaultLanguage)
                ChangeLanguage(Settings.LanguageCode);
        }

        private void OnSecuredFolderListRead(IEnumerable<string> folders)
        {
            var existingFolders = folders.Where(x => Directory.Exists(x)).ToArray();
            FileSystemManager.AddFolders(existingFolders);

            AddFoldersToSecuredFoldersDataGrid(existingFolders);

            var removedFolders = folders.Where(x => !Directory.Exists(x)).ToArray();

            if (removedFolders.Any())
                TaskManager.AddTask(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFileName, false, removedFolders));
        }

        private void OnSecureFileListRead(IEnumerable<FileDataGridItemModel> models)
        {
            var existing = models.Where(x => File.Exists(x.Secured)).ToList();
            FileSystemManager.AddFiles(existing.Select(x => x.Secured));

            AddFilesToRecentFilesDataGrid(existing);

            var removedFiles = models.Where(x => !File.Exists(x.Secured)).ToList();
            if (removedFiles.Any())
                TaskManager.AddTask(new WriteSecuredFileListTask(PathHelper.SecuredFilesListFile, removedFiles, false));
        }

        private void SecureFilesAdded(IEnumerable<FileDataGridItemModel> models)
        {
            TaskManager.AddTask(new WriteSecuredFileListTask(PathHelper.SecuredFilesListFile, models, true));
        }

        private void OnFolderSecured(string folderPath)
        {
            TaskManager.AddTask(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFileName, true, folderPath));
        }

        private void OnFileSecured(string filePath, string newFilePath)
        {
            var listOfModel = new []
            {
                new FileDataGridItemModel
                {
                    File = Path.GetFileName(filePath),
                    Time = DateTime.Now,
                    Secured = newFilePath,
                    Algorithm = CipherType.AES
                }
            };           

            AddFilesToRecentFilesDataGrid(listOfModel);
            SecuredFileAdded?.Invoke(listOfModel);
        }

        private void SettingsChangeHandler(string settingsPropertyName, object value)
        {
            var prop = typeof(SharpEncryptSettings).GetProperty(settingsPropertyName);
            if (prop == null)
            {
                MessageBox.Show(ResourceManager.GetString("ACriticalErrorHasOccurred"));
            }
            else
            {
                if (!value.GetType().IsAssignableFrom(prop.PropertyType))
                {
                    MessageBox.Show(ResourceManager.GetString("ACriticalErrorHasOccurred"));
                }
                else
                {
                    prop.SetValue(Settings, value);
                    SettingsWriteRequired?.Invoke(Settings);
                }
            }
        }

        private void SettingsWriterHandler(SharpEncryptSettings settings)
            => TaskManager.AddTask(new WriteSettingsFileTask(PathHelper.AppSettingsPath, settings));

        private void FormClosingHandler(object sender, FormClosingEventArgs e)
        {
            OnExitRequested();
        }

        private void OnExitRequested()
        {
            if (!TaskManager.HasCompletedJobs)
            {
                MessageBox.Show(
                    ResourceManager.GetString("ThereAreActiveTasks"),
                    ResourceManager.GetString("ActiveJobsWarning"),
                    MessageBoxButtons.OK);
            }
            else
            {
                CloseApplication();
            }
        }

        #endregion

        #region Language change methods

        private void ChangeLanguage(string lang)
        {
            var culture = CultureInfo.CreateSpecificCulture(lang);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            foreach (var control in this.AllControls())
            {
                if (control is ToolStrip strip)
                    foreach (var item in strip.AllItems())
                        ResourceManager.ApplyResources(item, item.Name);

                ResourceManager.ApplyResources(control, control.Name);
            }

            SetControlText(ResourceManager);
        }

        private void SetControlText(ResourceManager rm)
        {
            Text = rm.GetString("AppName");
            AppLabel.Text = rm.GetString("AppName");
            Tabs.TabPages[0].Text = rm.GetString("RecentFiles");
            Tabs.TabPages[1].Text = rm.GetString("SecuredFolders");

            RecentFilesGrid.Columns[0].HeaderText = rm.GetString("File");
            RecentFilesGrid.Columns[1].HeaderText = rm.GetString("Time");
            RecentFilesGrid.Columns[2].HeaderText = rm.GetString("Secured");
            RecentFilesGrid.Columns[3].HeaderText = rm.GetString("Algorithm");

            SecuredFoldersGrid.Columns[0].HeaderText = rm.GetString("Folder");

            OpenSecuredToolTip.SetToolTip(OpenSecuredGUIButton, rm.GetString("SelectSecuredFile"));
            AddSecuredFileToolTip.SetToolTip(AddSecuredGUIButton, rm.GetString("AddSecuredFile"));
            ShareToolTip.SetToolTip(ShareButton, rm.GetString("ClickToShare"));
            PasswordManagementToolTip.SetToolTip(PasswordManagement, rm.GetString("GoToPasswordManagement"));
            OpenHomeFolderToolTip.SetToolTip(OpenHomeFolder, rm.GetString("OpenHomeFolder"));
        }

        #endregion

        #region Help menu items
        private void ShowHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e) => MessageBox.Show(ResourceManager.GetString("CreatedByCredits"));

        #endregion

        #region Misc

        private void AddSecuredFolder()
        {
            using (var secureFolderDialog = new FolderBrowserDialog())
            {
                if (secureFolderDialog.ShowDialog() == DialogResult.OK 
                    && !string.IsNullOrWhiteSpace(secureFolderDialog.SelectedPath))
                {
                    TaskManager.AddTask(new SecureFolderTask(secureFolderDialog.SelectedPath));
                }
            }
        }

        private void ShowApplication()
        {
            Show();
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            NotifyIcon.Visible = false;
        }

        private void InvokeOnControl(Delegate method) => Invoke(method);

        private void AddFoldersToSecuredFoldersDataGrid(params string[] paths)
        {
            InvokeOnControl(new MethodInvoker(delegate ()
            {
                foreach (var path in paths)
                {
                    if (!ExcludedResources.Any(x => x.URI.Equals(path, StringComparison.InvariantCulture)))
                    {
                        var newRow = new DataGridViewRow();
                        newRow.Cells.Add(new DataGridViewTextBoxCell { Value = path });
                        SecuredFoldersGrid.Rows.Add(newRow);
                    }
                }
            }));
        }

        private void AddFilesToRecentFilesDataGrid(IList<FileDataGridItemModel> models)
        {
            var filteredModels = models.Where(x => !ExcludedResources.Any(z => z.URI.Equals(x.Secured, StringComparison.InvariantCulture))).ToList();
            var rows = new DataGridViewRow[filteredModels.Count];
            var props = typeof(FileDataGridItemModel).GetProperties();

            for (var i = 0; i < filteredModels.Count; i++)
            {
                var row = new DataGridViewRow();
                foreach (var prop in props)
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = prop.GetValue(filteredModels[i]) });
                
                rows[i] = row;
            }

            InvokeOnControl(new MethodInvoker(delegate() { RecentFilesGrid.Rows.AddRange(rows); }));
        }

        private OpenFileDialog GetAllFilesDialog()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = ResourceManager.GetString("AllFilesFilter");
                return dialog;
            }
        }

        private OpenFileDialog GetSEEFFilesDialog()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = ResourceManager.GetString("EncryptedFileFilter");
                return dialog;
            }
        }

        #endregion

        #region Language Menu Items

        private void EnglishToolStripMenuItem_Click(object sender, EventArgs e) => ChangeLanguage("en-GB");

        private void DeutschGermanToolStripMenuItem_Click(object sender, EventArgs e) => ChangeLanguage("de-DE");

        private void NetherlandsDutchToolStripMenuItem_Click(object sender, EventArgs e) => ChangeLanguage("nl-NL");

        private void FrancaisFrenchToolStripMenuItem_Click(object sender, EventArgs e) => ChangeLanguage("fr-FR");

        private void ItalianoItalianToolStripMenuItem_Click(object sender, EventArgs e) => ChangeLanguage("it-IT");

        private void KoreanToolStripMenuItem_Click(object sender, EventArgs e) => ChangeLanguage("ko-KR");

        private void PolskiPolishToolStripMenuItem_Click(object sender, EventArgs e) => ChangeLanguage("pl-PL");

        private void PortugueseToolStripMenuItem_Click(object sender, EventArgs e) => ChangeLanguage("pt-BR");

        private void RussianToolStripMenuItem_Click(object sender, EventArgs e) => ChangeLanguage("ru-RU");

        private void SwedishToolStripMenuItem_Click(object sender, EventArgs e) => ChangeLanguage("sv-SE");

        private void TurkishToolStripMenuItem_Click(object sender, EventArgs e) => ChangeLanguage("tr-TR");

        #endregion

        #region Advanced menu items

        private void ForceExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Settings.ForceExitDisclaimerHide)
            {
                using (var showOnceDialog = new ShowOnceDialog(ResourceManager.GetString("AreYouSure?")))
                {
                    if(showOnceDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (showOnceDialog.IsChecked)
                            SettingsChangeRequired?.Invoke("ForceExitDisclaimerHide", true);
                        CloseApplication();
                    }
                }
            }
            else
            {
                CloseApplication();
            }
        }

        private void IndividualSettingsResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var individualSettingsResetDialog = new IndividualSettingsResetDialog<SharpEncryptSettings>(Settings, new SharpEncryptSettings()))
            {
                if(individualSettingsResetDialog.ShowDialog() == DialogResult.OK)
                {
                    Settings = individualSettingsResetDialog.Result;
                }
            }
        }

        #endregion

        #region Notify icon methods
        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowApplication();
            OnExitRequested();
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowApplication();
        }
        #endregion
    }
}