using SecureEraseLibrary;
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
        private readonly List<FileDataGridItemModel> ExcludedFiles = new List<FileDataGridItemModel>();
        private readonly FileSystemManager FileSystemManager = new FileSystemManager();
        private readonly List<string> ExcludedFolders = new List<string>();        
        private readonly TaskManager TaskManager = new TaskManager();
        private readonly PathHelper PathHelper = new PathHelper();
        private SharpEncryptSettings _settings = new SharpEncryptSettings();

        #region Delegates and Events

        private delegate void SettingsChangeEventHandler(string settingsPropertyName, object value);
        private delegate void SettingsWriteEventHandler(SharpEncryptSettings settings);
        private delegate void FileSecuredEventHandler(string filePath, string newFilePath);
        private delegate void FolderSecuredEventHandler(string dirPath);
        private delegate void ReadSecuredFileListEventHandler(IEnumerable<FileDataGridItemModel> models);
        private delegate void ReadSecuredFolderListEventHandler(IEnumerable<string> folders);
        private delegate void ExcludedFilesListReadEventHandler(IEnumerable<FileDataGridItemModel> exclusions);
        private delegate void ExcludedFoldersListReadEventHandler(IEnumerable<string> folderUris);
        private delegate void SettingsFileReadEventHandler(SharpEncryptSettings settings);
        private delegate void TaskExceptionOccurredEventHandler(Exception exception);
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
        private event LogFileReadEventHandler LogFileRead;
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
            LoadExcludedFilesList();
            LoadExcludedFoldersList();
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
            var removals = new List<FileDataGridItemModel>();

            var props = typeof(FileDataGridItemModel).GetProperties();
            foreach (DataGridViewRow selectedRow in RecentFilesGrid.SelectedRows)
            {
                var removal = new FileDataGridItemModel();
                for(var i = 0; i < selectedRow.Cells.Count; i++)
                {
                    props[i].SetValue(removal, selectedRow.Cells[i].Value);
                }
                removals.Add(removal);
            }

            InvokeOnControl(new MethodInvoker(delegate ()
            {
                RecentFilesGrid.Rows.RemoveAll(RecentFilesGrid.SelectedRows);
                RecentFilesGrid.Refresh();
            }));

            var pruned = removals.Where(x => !ExcludedFiles.Any(z => z.Secured.Equals(x.Secured, StringComparison.Ordinal))).ToArray();
            TaskManager.AddTask(new WriteFileExclusionListTask(PathHelper.ExcludedFilesFile, true, pruned));
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
                    var dir = Path.GetDirectoryName(securedFilePath);
                    if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
                    {
                        Process.Start(dir);
                    }
                }
            }
        }

        private void RenameToOriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Folder context menu items

        private void ClearSecuredFoldersGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeOnControl(new MethodInvoker(delegate ()
            {
                SecuredFoldersGrid.Rows.Clear();
                SecuredFoldersGrid.Refresh();
            }));
        }

        private void ShowAllSecuredFolders_Click(object sender, EventArgs e)
        {
            AddFoldersToSecuredFoldersDataGrid(ExcludedFolders.ToArray());
        }

        private void HideExcludedSecuredFoldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideFoldersFromSecuredFoldersDataGrid(ExcludedFolders);
        }

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
                SecuredFoldersGrid.Rows.RemoveAll(SecuredFoldersGrid.SelectedRows);
                SecuredFoldersGrid.Refresh();
            }));

            var pruned = directoryURIs.Where(x => !ExcludedFolders.Any(z => z.Equals(x, StringComparison.Ordinal)));
            TaskManager.AddTask(new WriteFolderExclusionListTask(PathHelper.ExcludedFoldersFile, true, pruned));
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

        private void ShowAllSecuredFiles_Click(object sender, EventArgs e)
        {
            AddNewFileModelsToRecentFilesDataGrid(ExcludedFiles.ToArray());
        }

        private void HideExcludedSecuredFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideFilesFromSecuredFilesDataGrid(ExcludedFiles);
        }

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
            SettingsChangeRequired?.Invoke("Logging", ToggleChecked(LoggingToolStripMenuItem));
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

        private void OnExcludedFoldersListRead(IEnumerable<string> exclusions)
        {
            var removed = exclusions.Where(x => !Directory.Exists(x));
            ExcludedFolders.AddRange(exclusions.Where(x => Directory.Exists(x)));
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

        private void OnTaskCompleted(SharpEncryptTask task)
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
                        SecuredFileListRead?.Invoke(models);
                        break;
                    case TaskType.ReadSecuredFoldersListTask when task.Result.Value is IEnumerable<string> folders:
                        SecuredFolderListRead?.Invoke(folders);
                        break;
                    case TaskType.ReadSettingsFileTask when task.Result.Value is SharpEncryptSettings settings:
                        SettingsFileRead?.Invoke(settings);
                        break;
                    case TaskType.ReadFileExclusionListTask when task.Result.Value is IEnumerable<FileDataGridItemModel> exclusions:
                        ExcludedFilesListRead?.Invoke(exclusions);
                        break;
                    case TaskType.ReadFolderExclusionListTask when task.Result.Value is IEnumerable<string> folderUris:
                        ExcludedFoldersListRead?.Invoke(folderUris);
                        break;
                    case TaskType.SecureFolderTask when task.Result.Value is string folderPath:
                        FolderSecured?.Invoke(folderPath);
                        break;
                    case TaskType.ReadLogFileTask when task.Result.Value is string[] lines:
                        LogFileRead?.Invoke(lines);
                        break;
                }
            }
        }

        private void OnException(Exception exception)
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

        private void OnSecuredFileListRead(IEnumerable<FileDataGridItemModel> models)
        {
            var existing = models.Where(x => File.Exists(x.Secured)).ToArray();
            FileSystemManager.AddFiles(existing.Select(x => x.Secured));

            AddFileModelsToRecentFilesDataGrid(existing);

            var removedFiles = models.Where(x => !File.Exists(x.Secured)).ToArray();
            if (removedFiles.Any())
                TaskManager.AddTask(new WriteSecuredFileListTask(PathHelper.SecuredFilesListFile, false, removedFiles));
        }

        private void OnFolderSecured(string folderPath)
        {
            TaskManager.AddTask(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFileName, true, folderPath));
            AddFoldersToSecuredFoldersDataGrid(folderPath);
        }

        private void OnFileSecured(string filePath, string newFilePath)
        {
            var model = new FileDataGridItemModel
            {
                File = Path.GetFileName(filePath),
                Time = DateTime.Now,
                Secured = newFilePath,
                Algorithm = CipherType.AES
            };
            TaskManager.AddTask(new WriteSecuredFileListTask(PathHelper.SecuredFilesListFile, true, model));
            AddNewFileModelsToRecentFilesDataGrid(model);            
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
                    if (!ExcludedFolders.Any(x => x.Equals(path, StringComparison.Ordinal)))
                    {
                        var newRow = new DataGridViewRow();
                        newRow.Cells.Add(new DataGridViewTextBoxCell { Value = path });
                        SecuredFoldersGrid.Rows.Add(newRow);
                    }
                }
            }));
        }

        private void HideFoldersFromSecuredFoldersDataGrid(IEnumerable<string> folders)
        {
            if (folders.Any()) 
            {
                var rows = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in SecuredFoldersGrid.Rows)
                {
                    if (row.Cells[0].Value is string folderPath)
                    {
                        if(folders.Any(x => x.Equals(folderPath, StringComparison.Ordinal)))
                        {
                            rows.Add(row);
                        }
                    }
                }

                SecuredFoldersGrid.Rows.RemoveAll(rows);
            }
        }

        private void HideFilesFromSecuredFilesDataGrid(IEnumerable<FileDataGridItemModel> models)
        {
            if (models.Any()) 
            {
                var rows = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in RecentFilesGrid.Rows)
                {
                    if (row.Cells[2].Value is string securedFilePath)
                    {
                        if (models.Any(x => x.Secured.Equals(securedFilePath, StringComparison.Ordinal)))
                        {
                            rows.Add(row);
                        }
                    }
                }

                RecentFilesGrid.Rows.RemoveAll(rows);
            }
        }

        private void AddNewFileModelsToRecentFilesDataGrid(params FileDataGridItemModel[] models)
        {
            AddFileModelsToGrid(models);
        }

        private void AddFileModelsToRecentFilesDataGrid(params FileDataGridItemModel[] models)
        {
            AddFileModelsToGrid(models.Where(x => !ExcludedFiles.Any(z => z.Secured.Equals(x.Secured, StringComparison.Ordinal))).ToArray());
        }

        private void AddFileModelsToGrid(params FileDataGridItemModel[] models)
        {
            var rows = new DataGridViewRow[models.Length];
            var props = typeof(FileDataGridItemModel).GetProperties();

            for (var i = 0; i < models.Length; i++)
            {
                var row = new DataGridViewRow();
                foreach (var prop in props)
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = prop.GetValue(models[i]) });

                rows[i] = row;
            }

            InvokeOnControl(new MethodInvoker(delegate () { RecentFilesGrid.Rows.AddRange(rows); }));
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