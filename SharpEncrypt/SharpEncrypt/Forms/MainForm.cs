using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Exceptions;
using SharpEncrypt.ExtensionClasses;
using SharpEncrypt.Managers;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;
using SharpEncrypt.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    internal partial class MainForm : Form
    {
        private readonly ComponentResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly FileSystemManager FileSystemManager = new FileSystemManager();
        private readonly TaskManager TaskManager = new TaskManager();
        private readonly PathHelper PathHelper = new PathHelper();
        private SharpEncryptSettings _settings;

        #region Collections
        //Lists of excluded file and folder model objects the user has requested not to see
        private readonly List<FileDataGridItemModel> ExcludedFiles = new List<FileDataGridItemModel>();
        private readonly List<FolderDataGridItemModel> ExcludedFolders = new List<FolderDataGridItemModel>();

        //Lists of models to display in their respective grids
        private readonly BindingList<FileDataGridItemModel> DisplaySecuredFiles = new BindingList<FileDataGridItemModel>();
        private readonly BindingList<FolderDataGridItemModel> DisplaySecuredFolders = new BindingList<FolderDataGridItemModel>();

        //Master lists containing all secured file and folder models
        private readonly List<FileDataGridItemModel> SecuredFiles = new List<FileDataGridItemModel>();
        private readonly List<FolderDataGridItemModel> SecuredFolders = new List<FolderDataGridItemModel>();
        #endregion

        #region Delegates and Events

        private delegate void SettingsChangeEventHandler(string settingsPropertyName, object value);
        private delegate void SettingsWriteEventHandler(SharpEncryptSettings settings);
        private delegate void FileSecuredEventHandler(FileDataGridItemModel model);
        private delegate void FolderSecuredEventHandler(FolderDataGridItemModel model);
        private delegate void SecuredFileRenamedEventHandler(OnSecuredFileRenamedTaskResult result);
        private delegate void ReadSecuredFileListEventHandler(IEnumerable<FileDataGridItemModel> models);
        private delegate void ReadSecuredFolderListEventHandler(IEnumerable<FolderDataGridItemModel> models);
        private delegate void ExcludedFilesListReadEventHandler(IEnumerable<FileDataGridItemModel> models);
        private delegate void ExcludedFoldersListReadEventHandler(IEnumerable<FolderDataGridItemModel> models);
        private delegate void SettingsFileReadEventHandler(SharpEncryptSettings settings);
        private delegate void TaskExceptionOccurredEventHandler(Exception exception);
        private delegate void OTPPasswordStoreKeyWrittenEventHandler(CreateOTPPasswordStoreKeyTaskResult result);
        private delegate void OTPPasswordStoreReadEventHandler(OpenOTPPasswordStoreTaskResult result);
        private delegate void LogFileReadEventHandler(string[] lines);

        private event SettingsWriteEventHandler SettingsWriteRequired;
        private event SettingsChangeEventHandler SettingsChangeRequired;
        private event FileSecuredEventHandler FileSecured;
        private event FolderSecuredEventHandler FolderSecured;
        private event ReadSecuredFileListEventHandler SecuredFileListRead;
        private event ReadSecuredFolderListEventHandler SecuredFolderListRead;
        private event SettingsFileReadEventHandler SettingsFileRead;
        private event TaskExceptionOccurredEventHandler TaskException;
        private event ExcludedFilesListReadEventHandler ExcludedFilesListRead;
        private event ExcludedFoldersListReadEventHandler ExcludedFoldersListRead;
        private event SecuredFileRenamedEventHandler SecuredFileRenamed;
        private event OTPPasswordStoreKeyWrittenEventHandler OTPPasswordStoreKeyWritten;
        private event OTPPasswordStoreReadEventHandler OTPPasswordStoreRead;
        private event LogFileReadEventHandler LogFileRead;

        #endregion

        private string SessionPassword { get; set; }

        private bool IsPasswordValid => SessionPassword != null && SessionPassword.Length > 0;

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

            AppDomain.CurrentDomain.UnhandledException += UnhandledException;

            SettingsWriteRequired += SettingsWriteHandler;
            SettingsChangeRequired += SettingsChangeHandler;

            FormClosing += FormClosingHandler;
            Resize += FormResize;
            NotifyIcon.DoubleClick += OnNotifyIconDoubleClicked;

            FileSecured += OnFileSecured;
            FolderSecured += OnFolderSecured;
            SecuredFileListRead += OnSecuredFileListRead;
            SecuredFolderListRead += OnSecuredFolderListRead;
            SettingsFileRead += OnSettingsFileRead;
            TaskException += OnException;
            SecuredFileRenamed += OnSecuredFileRenamed;
            OTPPasswordStoreKeyWritten += OnOTPPasswordStoreKeyWritten;
            OTPPasswordStoreRead += OnOTPPasswordStoreRead;

            RecentFilesGrid.DragDrop += OnRecentFilesGridDragDrop;
            RecentFilesGrid.DragEnter += OnRecentFilesGridDragEnter;

            SecuredFoldersGrid.DragDrop += SecuredFoldersGrid_DragDrop;
            SecuredFoldersGrid.DragEnter += SecuredFoldersGrid_DragEnter;

            TaskManager.TaskCompleted += OnTaskCompleted;
            TaskManager.ExceptionOccurred += OnException;
            TaskManager.DuplicateExclusiveTask += OnDuplicateExclusiveTaskDetected;

            ExcludedFilesListRead += OnExcludedFilesListRead;
            ExcludedFoldersListRead += OnExcludedFoldersListRead;

            LogFileRead += OnLogFileRead;

            RecentFilesGrid.DataSource = DisplaySecuredFiles;
            SecuredFoldersGrid.DataSource = DisplaySecuredFolders;

            AppLabel.MouseDoubleClick += ApplicationLabelDoubleClicked;

            FileSystemManager.Exception += OnException;
            FileSystemManager.ItemDeleted += ItemDeleted;
            FileSystemManager.ItemRenamed += ItemRenamed;
            FileSystemManager.ItemCreated += ItemCreated;
        }

        private void MainForm_Load(object sender, EventArgs e) => LoadApplication();

        #region Misc methods

        private void SecureFolders(params string[] folders)
        {
            if (OnPasswordRequired())
            {
                if (folders != null)
                {
                    foreach (var folder in folders)
                    {
                        if (Directory.Exists(folder))
                        {
                            TaskManager.AddTask(new SecureFolderTask(folder, Settings.IncludeSubfolders));
                        }
                    }
                }
            }
        }

        private void SecureFiles(params string[] filePaths)
        {
            if (OnPasswordRequired())
            {
                if (filePaths != null)
                {
                    foreach (var filePath in filePaths)
                    {
                        if (File.Exists(filePath))
                        {
                            TaskManager.AddTask(new SecureFileTask(filePath));
                        }
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
                    SecureFiles(dialog.FileName);
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
            SetToolTips();
            SetApplicationSettings();
            LoadExcludedFilesList();
            LoadExcludedFoldersList();
            LoadSecuredFilesList();
            LoadSecuredFoldersList();
        }

        private void SetApplicationSettings()
        {
            TaskManager.AddTask(new ReadSettingsFileTask(PathHelper.AppSettingsPath));
        }

        private void ReloadApplication(bool changeLanguage)
        {
            if (changeLanguage)
                ChangeLanguage(Settings.LanguageCode);
        }

        private void SetUIOptions()
        {
            InvokeOnControl(new MethodInvoker(delegate ()
            {
                DoNotPromptForPasswordOnStartup.Checked = Settings.PasswordStartupPromptHide;
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

                switch (Settings.StoreType) 
                {
                    case StoreType.AES:
                        AESPasswordBasedToolStripMenuItem.Checked = true;
                        OTPKeyBasedToolStripMenuItem.Checked = false;
                        break;
                    case StoreType.OTP:
                        OTPKeyBasedToolStripMenuItem.Checked = true;
                        AESPasswordBasedToolStripMenuItem.Checked = false;
                        break;
                }

            }));
        }

        private void LoadExcludedFilesList()
        {
            TaskManager.AddTask(new ReadFileExclusionListTask(PathHelper.ExcludedFilesFile));
        }

        private void LoadExcludedFoldersList()
        {
            TaskManager.AddTask(new ReadFolderExclusionListTask(PathHelper.ExcludedFoldersFile));
        }

        private void LoadSecuredFoldersList()
        {
            TaskManager.AddTask(new ReadSecuredFoldersListTask(PathHelper.SecuredFoldersListFile));
        }

        private void LoadSecuredFilesList()
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

        private void AddSecuredFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSecured();
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

        private void RenameToOriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Folder context menu items

        private void ShowAllSecuredFolders_Click(object sender, EventArgs e)
        {
            AddModelsToSecuredFoldersDataGrid_NoCheck(ExcludedFolders.Where(x => Directory.Exists(x.URI)));
        }

        private void HideExcludedSecuredFoldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveModelsFromSecuredFoldersDataGrid(ExcludedFolders);
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

        private void AddSecuredFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSecuredFolder();
        }

        #endregion

        #region File menu items

        private void AESPasswordBasedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AESPasswordBasedToolStripMenuItem.Checked)
            {
                OTPKeyBasedToolStripMenuItem.Checked = false;
            }
            else
            {
                OTPKeyBasedToolStripMenuItem.Checked = true;
            }

            SettingsChangeRequired?.Invoke("StoreType", OTPKeyBasedToolStripMenuItem.Checked ? StoreType.OTP : StoreType.AES);
        }

        private void OTPKeyBasedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OTPKeyBasedToolStripMenuItem.Checked)
            {
                AESPasswordBasedToolStripMenuItem.Checked = false;
            }
            else
            {
                AESPasswordBasedToolStripMenuItem.Checked = true;
            }

            SettingsChangeRequired?.Invoke("StoreType", OTPKeyBasedToolStripMenuItem.Checked ? StoreType.OTP : StoreType.AES);
        }

        private void OpenPasswordManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPasswordStore();
        }

        private void ClearSessionPassword_Click(object sender, EventArgs e)
        {
            SessionPassword = null;
        }

        private void ShowAllSecuredFiles_Click(object sender, EventArgs e)
        {
            AddModelsToRecentFilesDataGrid_NoCheck(ExcludedFiles.Where(x => File.Exists(x.Secured)));
        }

        private void HideExcludedSecuredFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveModelsFromSecuredFilesDataGrid(ExcludedFiles);
        }

        private void OpenSecuredToolStripMenuItem_Click(object sender, EventArgs e) => OpenSecured();

        private void SecureToolStripMenuItem_Click(object sender, EventArgs e) => AddSecured();

        private void StopSecuringToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SecuredFoldersToolStripMenuItem_Click(object sender, EventArgs e) => Tabs.SelectedTab = Tabs.TabPages[1];

        private void AnonymousRenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransformFileName(true);
        }

        private void RenameToOriginalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TransformFileName(false);
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

        private void SecureDeleteFolder_Click(object sender, EventArgs e)
        {
            using (var folderBrowser = new FolderBrowserDialog())
            {
                if(folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(folderBrowser.SelectedPath))
                    {
                        TaskManager.AddTask(new ShredDirectoryTask(folderBrowser.SelectedPath, Settings.IncludeSubfolders));
                    }
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
                        return;
                    }
                    else
                    {
                        if (showOnceDialog.IsChecked)
                            SettingsChangeRequired?.Invoke("OTPDisclaimerHide", true);
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
                            TaskManager.AddTask(new OneTimePadTransformTask(openFileDialog.FileName, openKeyFileDialog.FileName, false));
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
                    SessionPassword = dialog.Password.Hash();
                }
            }
        }

        private bool OnPasswordRequired()
        {
            if (IsPasswordValid)
                return true;
            SetSessionPassword();
            return IsPasswordValid;
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
            OpenPasswordStore();
        }

        private void OpenHomeFolder_Click(object sender, EventArgs e) => Process.Start(PathHelper.AppDirectory);

        #endregion

        #region Disk tools menu items

        private void WipeFreeDiskSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var hardDriveWipeDialog = new AdvancedHardDriveWipeDialog())
            {
                hardDriveWipeDialog.ShowDialog();
            }
        }

        #endregion

        #region Debug menu items

        private void ViewLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskManager.AddTask(new ReadLogFileTask(PathHelper.LoggingFilePath));
        }

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
            SettingsChangeRequired?.Invoke("Logging", LoggingToolStripMenuItem.Checked);
        }

        private void CancelAllFutureTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskManager.SetCancellationFlag();
        }

        private void DeleteGridExclusionList_Click(object sender, EventArgs e)
        {
            TaskManager.AddTask(new GenericDeleteFileTask(PathHelper.ExcludedFilesFile));
            TaskManager.AddTask(new GenericDeleteFileTask(PathHelper.ExcludedFoldersFile));
        }
        #endregion

        #region Flag Menu Items

        private void DoNotPromptForPasswordOnStartup_Click(object sender, EventArgs e)
            => SettingsChangeRequired?.Invoke("PasswordStartupPromptHide", DoNotPromptForPasswordOnStartup.Checked);

        private void DebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebugMenuStrip.Enabled = Debug.Checked;
            SettingsChangeRequired?.Invoke("DebugEnabled", Debug.Checked);
        }

        private void UseADifferentPasswordForEachFileToolStripMenuItem_Click(object sender, EventArgs e)
            => SettingsChangeRequired?.Invoke("UseADifferentPasswordForEachFile", UseADifferentPasswordForEachFile.Checked);

        private void IncludeToolStripMenuItem_Click(object sender, EventArgs e)
            => SettingsChangeRequired?.Invoke("IncludeSubfolders", IncludeSubfolders.Checked);

        private void WipeDiskSpaceAfterSecureDeleteToolStripMenuItem_Click(object sender, EventArgs e)
            => SettingsChangeRequired?.Invoke("WipeFreeSpaceAfterSecureDelete", WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Checked);

        #endregion

        #region Handlers

        private void ApplicationLabelDoubleClicked(object sender, MouseEventArgs e)
        {
            Process.Start(Constants.ProjectURL);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OnException(e.ExceptionObject as Exception);
        }

        private void OnDuplicateExclusiveTaskDetected(SharpEncryptTask task)
        {
            InvokeOnControl(new MethodInvoker(() =>
            {
                MessageBox.Show(
                    ResourceManager.GetString("ADuplicateTaskHasBeenDetected"),
                    ResourceManager.GetString("Error"),
                    MessageBoxButtons.OK);
            }));
        }

        private void OnLogFileRead(string[] lines)
        {
            using (var textViewer = new GenericTextViewer(lines))
            {
                textViewer.ShowDialog();
            }
        }

        private void OnExcludedFoldersListRead(IEnumerable<FolderDataGridItemModel> exclusions)
        {
            var removed = exclusions.Where(x => !Directory.Exists(x.URI));
            ExcludedFolders.AddRange(exclusions.Where(x => Directory.Exists(x.URI)));
            if (removed.Any())
                TaskManager.AddTask(new WriteFolderExclusionListTask(PathHelper.ExcludedFoldersFile, false, removed));
        }

        private void OnExcludedFilesListRead(IEnumerable<FileDataGridItemModel> exclusions)
        {
            var removed = exclusions.Where(x => !File.Exists(x.Secured));
            ExcludedFiles.AddRange(exclusions.Where(x => File.Exists(x.Secured)));
            if(removed.Any())
                TaskManager.AddTask(new WriteFileExclusionListTask(PathHelper.ExcludedFilesFile, false, removed));
        }

        private void OnNotifyIconDoubleClicked(object sender, EventArgs e)
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

        private void OnRecentFilesGridDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void OnRecentFilesGridDragDrop(object sender, DragEventArgs e)
        {
            SecureFiles(ExtractPaths(e));
        }

        private void OnException(Exception exception)
        {
            if (Settings.Logging)
                TaskManager.AddTask(new LoggingTask(PathHelper.LoggingFilePath, exception));

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
            if (!Settings.PasswordStartupPromptHide)
                SetSessionPassword();
        }

        private void OnSecuredFolderListRead(IEnumerable<FolderDataGridItemModel> folders)
        {
            var existingFolders = folders.Where(x => Directory.Exists(x.URI)).ToArray();
            FileSystemManager.AddPaths(existingFolders.Select(x => x.URI));

            SecuredFolders.AddRange(existingFolders);
            AddModelsToSecuredFoldersDataGrid_NoCheck(SecuredFolders.Where(x => !ExcludedFolders.Any(z => z.URI.Equals(x.URI, StringComparison.Ordinal))));

            var removedFolders = folders.Where(x => !Directory.Exists(x.URI)).ToArray();

            if (removedFolders.Any())
                TaskManager.AddTask(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFile, false, removedFolders));
        }

        private void OnSecuredFileListRead(IEnumerable<FileDataGridItemModel> models)
        {
            var existing = models.Where(x => File.Exists(x.Secured)).ToArray();
            FileSystemManager.AddPaths(existing.Select(x => x.Secured));

            SecuredFiles.AddRange(existing);
            AddModelsToRecentFilesDataGrid_NoCheck(SecuredFiles.Where(x => !ExcludedFiles.Any(z => z.Equals(x))));

            var removedFiles = models.Where(x => !File.Exists(x.Secured)).ToArray();
            if (removedFiles.Any())
                TaskManager.AddTask(new WriteSecuredFileListTask(PathHelper.SecuredFilesListFile, false, removedFiles));
        }

        private void OnFolderSecured(FolderDataGridItemModel folderModel)
        {
            TaskManager.AddTask(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFile, true, folderModel));
            FileSystemManager.AddPaths(new[] { folderModel.URI });
            SecuredFolders.Add(folderModel);
            AddModelsToSecuredFoldersDataGrid_NoCheck(folderModel);
        }

        private void OnFileSecured(FileDataGridItemModel model)
        {
            TaskManager.AddTask(new WriteSecuredFileListTask(PathHelper.SecuredFilesListFile, true, model));
            FileSystemManager.AddPaths(new[] { model.Secured });
            SecuredFiles.Add(model);
            AddModelsToRecentFilesDataGrid_NoCheck(model);
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

        private void SettingsWriteHandler(SharpEncryptSettings settings)
            => TaskManager.AddTask(new WriteSettingsFileTask(PathHelper.AppSettingsPath, settings));

        private void FormClosingHandler(object sender, FormClosingEventArgs e)
        {
            OnExitRequested();
        }

        private void OnExitRequested()
        {
            if (!TaskManager.HasCompletedJobs)
            {
                InvokeOnControl(new MethodInvoker(() =>
                {
                    MessageBox.Show(
                        ResourceManager.GetString("ThereAreActiveTasks"),
                        ResourceManager.GetString("ActiveJobsWarning"),
                        MessageBoxButtons.OK);
                }));
            }
            else
            {
                CloseApplication();
            }
        }

        private void OnSecuredFileRenamed(OnSecuredFileRenamedTaskResult result)
        {
            var fileModel = SecuredFiles.FirstOrDefault(x => x.Secured.Equals(result.OldPath, StringComparison.Ordinal));
            if (fileModel != null)
            {
                fileModel.Secured = result.NewPath;
            }

            var excludedFileModel = ExcludedFiles.FirstOrDefault(x => x.Secured.Equals(result.OldPath, StringComparison.Ordinal));
            if(excludedFileModel != null)
            {
                excludedFileModel.Secured = result.NewPath;
            }

            InvokeOnControl(new MethodInvoker(() =>
            {
                var displayedFileModel = DisplaySecuredFiles.FirstOrDefault(x => x.Secured.Equals(result.OldPath, StringComparison.Ordinal));
                if(displayedFileModel != null)
                {
                    displayedFileModel.Secured = result.NewPath;
                }
            }));
        }

        private void OnOTPPasswordStoreKeyWritten(CreateOTPPasswordStoreKeyTaskResult result)
        {
            SettingsChangeRequired?.Invoke("OTPStoreKeyFilePath", result.KeyPath);
            if (result.OpenAfter)
            {
                TaskManager.AddTask(new OpenOTPPasswordStoreTask(result.StorePath, result.KeyPath));
            }
        }

        private void OnOTPPasswordStoreRead(OpenOTPPasswordStoreTaskResult result)
        {
            InvokeOnControl(new MethodInvoker(() =>
            {
                using (var passwordManager = new PasswordManagerForm(result.Models))
                {
                    if(passwordManager.ShowDialog() == DialogResult.OK)
                    {
                        TaskManager.AddTask(new SavePasswordsTask(result.StorePath, result.KeyPath, passwordManager.PasswordModels));
                    }
                }
            }));            
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

            SetControlText();
            SetToolTips();
        }

        private void SetControlText()
        {
            Text = ResourceManager.GetString("AppName");
            AppLabel.Text = ResourceManager.GetString("AppName");
            Tabs.TabPages[0].Text = ResourceManager.GetString("RecentFiles");
            Tabs.TabPages[1].Text = ResourceManager.GetString("SecuredFolders");

            RecentFilesGrid.Columns[0].HeaderText = ResourceManager.GetString("File");
            RecentFilesGrid.Columns[1].HeaderText = ResourceManager.GetString("Time");
            RecentFilesGrid.Columns[2].HeaderText = ResourceManager.GetString("Secured");
            RecentFilesGrid.Columns[3].HeaderText = ResourceManager.GetString("Algorithm");

            SecuredFoldersGrid.Columns[0].HeaderText = ResourceManager.GetString("Folder");
            SecuredFoldersGrid.Columns[1].HeaderText = ResourceManager.GetString("Time");
        }

        private void SetToolTips()
        {
            OpenSecuredToolTip.SetToolTip(OpenSecuredGUIButton, ResourceManager.GetString("SelectSecuredFile"));
            AddSecuredFileToolTip.SetToolTip(AddSecuredGUIButton, ResourceManager.GetString("AddSecuredFile"));
            ShareToolTip.SetToolTip(ShareButton, ResourceManager.GetString("ClickToShare"));
            PasswordManagementToolTip.SetToolTip(PasswordManagement, ResourceManager.GetString("GoToPasswordManagement"));
            OpenHomeFolderToolTip.SetToolTip(OpenHomeFolder, ResourceManager.GetString("OpenHomeFolder"));
        }

        #endregion

        #region Help menu items
        private void ShowHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e) => MessageBox.Show(ResourceManager.GetString("CreatedByCredits"));

        #endregion

        #region Misc

        private void TransformFileName(bool anonymise)
        {
            if (!IsPasswordValid)
            {
                ShowErrorDialog(ResourceManager.GetString("PasswordInvalid"));
            }
            else
            {
                using (var openFileDialog = GetAllFilesDialog())
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (anonymise)
                        {
                            AnonymousRenameHelper.AnonymiseFile(openFileDialog.FileName, SessionPassword);
                        }
                        else
                        {
                            AnonymousRenameHelper.DeanonymiseFile(openFileDialog.FileName, SessionPassword);
                        }
                    }
                }
            }
        }

        private void ShowErrorDialog(string message)
        {
            MessageBox.Show(message, ResourceManager.GetString("Error"), MessageBoxButtons.OK);
        }

        private void OpenPasswordStore()
        {
            if (Settings.StoreType == StoreType.OTP)
            {
                TaskManager.AddTask(new OpenOTPPasswordStoreTask(PathHelper.OTPPasswordStoreFile, Settings.OTPStoreKeyFilePath));
            }
            else if (Settings.StoreType == StoreType.AES)
            {
                TaskManager.AddTask(new OpenAESPasswordStoreTask(PathHelper.AESPasswordStoreFile, SessionPassword));
            }
        }

        private void OTPPasswordStoreKeyNotFound(KeyFileStoreFileTupleModel tuple)
        {
            if(!DriveInfo.GetDrives().Any(x => x.Name[0].Equals(tuple.KeyFile[0])))
            {
                InvokeOnControl(new MethodInvoker(() =>
                {
                    using (var waitingForDriveForm = new WaitingForDriveForm(tuple))
                    {
                        if(waitingForDriveForm.ShowDialog() == DialogResult.OK)
                        {
                            TaskManager.AddTask(new OpenOTPPasswordStoreTask(tuple.StoreFile, tuple.KeyFile));
                        }
                    }
                }));
            }
            else
            {
                OnException(new FileNotFoundException(tuple.KeyFile));
            }
        }

        private void OTPPasswordStoreFirstUse()
        {
            if(MessageBox.Show(ResourceManager.GetString("AKeyMustBeSavedForThisOTPStore"), 
                ResourceManager.GetString("FirstUseDialogTitle"),
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                InvokeOnControl(new MethodInvoker(() =>
                {
                    using (var saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = ResourceManager.GetString("SharpEncryptPasswordStoreFileKey");
                        var result = saveFileDialog.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            TaskManager.AddTask(new CreateOTPPasswordStoreKeyTask(PathHelper.OTPPasswordStoreFile, saveFileDialog.FileName, true));
                        }
                    }
                }));
            }
        }

        private void AddSecuredFolder()
        {
            using (var secureFolderDialog = new FolderBrowserDialog())
            {
                if (secureFolderDialog.ShowDialog() == DialogResult.OK 
                    && !string.IsNullOrWhiteSpace(secureFolderDialog.SelectedPath))
                {
                    SecureFolders(secureFolderDialog.SelectedPath);
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
                    SettingsWriteRequired?.Invoke(Settings);
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

        #region DataGrid methods

        private void AddModelsToSecuredFoldersDataGrid_NoCheck(IEnumerable<FolderDataGridItemModel> models)
        {
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFolders.AddRange(models);
            }));
        }

        private void AddModelsToSecuredFoldersDataGrid_NoCheck(params FolderDataGridItemModel[] models)
        {
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFolders.AddRange(models);
            }));
        }

        private void RemoveModelsFromSecuredFoldersDataGrid(IEnumerable<FolderDataGridItemModel> models)
        {
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFolders.RemoveAll(models);
            }));
        }

        private void RemoveModelsFromSecuredFilesDataGrid(IEnumerable<FileDataGridItemModel> models)
        {
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFiles.RemoveAll(models);
            }));
        }

        private void RemoveModelsFromSecuredFilesDataGrid(params FileDataGridItemModel[] models)
        {
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFiles.RemoveAll(models);
            }));
        }

        private void AddModelsToRecentFilesDataGrid_NoCheck(params FileDataGridItemModel[] models)
        {
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFiles.AddRange(models);
            }));
        }

        private void AddModelsToRecentFilesDataGrid_NoCheck(IEnumerable<FileDataGridItemModel> models)
        {
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFiles.AddRange(models);
            }));
        }

        private void ClearRecentFilesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeOnControl(new MethodInvoker(delegate ()
            {
                DisplaySecuredFiles.Clear();
                RecentFilesGrid.Refresh();
            }));
        }

        private void RemoveFileFromListButKeepSecuredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var removals = RecentFilesGrid.SelectedRows.Select(x => GetFileModelFromRow(x))
                .Where(z => z != null);

            InvokeOnControl(new MethodInvoker(delegate ()
            {
                ExcludedFiles.AddRange(removals);
                DisplaySecuredFiles.RemoveAll(removals);
                RecentFilesGrid.Refresh();
            }));

            TaskManager.AddTask(new WriteFileExclusionListTask(PathHelper.ExcludedFilesFile, true, removals));
        }

        private void ShowInFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var paths = RecentFilesGrid.SelectedRows.Select(x => GetFileModelFromRow(x))
                .Where(z => z != null)
                .Select(k => k.Secured);

            foreach(var path in paths)
            {
                var dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
                {
                    Process.Start(dir);
                }
            }
        }

        private void ClearSecuredFoldersGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeOnControl(new MethodInvoker(delegate ()
            {
                DisplaySecuredFolders.Clear();
                SecuredFoldersGrid.Refresh();
            }));
        }

        private void OpenExplorerHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var models = SecuredFoldersGrid.SelectedRows.Select(x => GetFolderModelFromRow(x));
            foreach(var model in models)
            {
                if (Directory.Exists(model.URI))
                {
                    Process.Start(model.URI);
                }
            }
        }

        private void RemoveFolderFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var directoryModels = SecuredFoldersGrid.SelectedRows.Select(x => GetFolderModelFromRow(x))
                .Where(z => z != null);

            InvokeOnControl(new MethodInvoker(delegate ()
            {
                ExcludedFolders.AddRange(directoryModels);
                DisplaySecuredFolders.RemoveAll(directoryModels);
                SecuredFoldersGrid.Refresh();
            }));

            TaskManager.AddTask(new WriteFolderExclusionListTask(PathHelper.ExcludedFoldersFile, true, directoryModels));
        }

        private static FileDataGridItemModel GetFileModelFromRow(object rowObject)
        {
            if(rowObject is DataGridViewRow row && row.DataBoundItem != null)
            {
                if(row.DataBoundItem is FileDataGridItemModel model)
                {
                    return model;
                }
            }
            return null;
        }

        private static FolderDataGridItemModel GetFolderModelFromRow(object rowObject)
        {
            if (rowObject is DataGridViewRow row && row.DataBoundItem != null)
            {
                if (row.DataBoundItem is FolderDataGridItemModel model)
                {
                    return model;
                }
            }
            return null;
        }

        #endregion

        #region Directory events

        private void ItemRenamed(string newPath, string oldPath, bool inSubFolder)
        {
            TaskManager.AddTask(new OnSecuredFileRenamedTask(PathHelper.SecuredFilesListFile, newPath, oldPath));
        }

        private void ItemDeleted(string path)
        {
            var fileModel = SecuredFiles.FirstOrDefault(x => x.Secured.Equals(path, StringComparison.Ordinal));
            if (fileModel != null)
            {
                InvokeOnControl(new MethodInvoker(() =>
                {
                    DisplaySecuredFiles.Remove(fileModel);
                    SecuredFiles.Remove(fileModel);
                    ExcludedFiles.Remove(fileModel);
                }));

                TaskManager.AddTask(new WriteSecuredFileListTask(PathHelper.SecuredFilesListFile, false, fileModel));
            }
            else
            {
                var folderModel = SecuredFolders.FirstOrDefault(x => x.URI.Equals(path, StringComparison.Ordinal));
                if (folderModel != null)
                {
                    InvokeOnControl(new MethodInvoker(() =>
                    {
                        DisplaySecuredFolders.Remove(folderModel);
                        SecuredFolders.Remove(folderModel);
                        ExcludedFolders.Remove(folderModel);
                    }));
                    TaskManager.AddTask(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFile, false, folderModel));
                }
            }
        }

        private void ItemCreated(string path, bool inSubfolder)
        {
            if (inSubfolder && !Settings.IncludeSubfolders)
                return;
            TaskManager.AddTask(new SecureFileTask(path));
        }

        #endregion

        #region OnTaskCompleted

        private void OnTaskCompleted(SharpEncryptTask task)
        {
            if (task.Result.Exception != null)
            {
                if (task.Result.Exception is OTPPasswordStoreFirstUseException)
                {
                    OTPPasswordStoreFirstUse();
                }
                else if (task.Result.Exception is OTPKeyFileNotAvailableException
                    && task.Result.Value is KeyFileStoreFileTupleModel tuple)
                {
                    OTPPasswordStoreKeyNotFound(tuple);
                }
                else
                {
                    TaskException?.Invoke(task.Result.Exception);
                }
            }
            else
            {
                switch (task.TaskType)
                {
                    case TaskType.ReadSecuredFilesListTask when task.Result.Value is IEnumerable<FileDataGridItemModel> models:
                        SecuredFileListRead?.Invoke(models);
                        break;
                    case TaskType.ReadSecuredFoldersListTask when task.Result.Value is IEnumerable<FolderDataGridItemModel> models:
                        SecuredFolderListRead?.Invoke(models);
                        break;
                    case TaskType.ReadSettingsFileTask when task.Result.Value is SharpEncryptSettings settings:
                        SettingsFileRead?.Invoke(settings);
                        break;
                    case TaskType.ReadFileExclusionListTask when task.Result.Value is IEnumerable<FileDataGridItemModel> models:
                        ExcludedFilesListRead?.Invoke(models);
                        break;
                    case TaskType.ReadFolderExclusionListTask when task.Result.Value is IEnumerable<FolderDataGridItemModel> models:
                        ExcludedFoldersListRead?.Invoke(models);
                        break;
                    case TaskType.SecureFolderTask when task.Result.Value is FolderDataGridItemModel model:
                        FolderSecured?.Invoke(model);
                        break;
                    case TaskType.ReadLogFileTask when task.Result.Value is string[] lines:
                        LogFileRead?.Invoke(lines);
                        break;
                    case TaskType.SecureFileTask when task.Result.Value is FileDataGridItemModel model:
                        FileSecured?.Invoke(model);
                        break;
                    case TaskType.OnSecuredFileRenamedTask when task.Result.Value is OnSecuredFileRenamedTaskResult result:
                        SecuredFileRenamed?.Invoke(result);
                        break;
                    case TaskType.CreateOTPPasswordStoreKeyTask when task.Result.Value is CreateOTPPasswordStoreKeyTaskResult result:
                        OTPPasswordStoreKeyWritten?.Invoke(result);
                        break;
                    case TaskType.OpenOTPPasswordStoreTask when task.Result.Value is OpenOTPPasswordStoreTaskResult result:
                        OTPPasswordStoreRead?.Invoke(result);
                        break;
                }
            }
        }

        #endregion
    }
}