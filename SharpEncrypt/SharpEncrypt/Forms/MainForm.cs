﻿using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Exceptions;
using SharpEncrypt.ExtensionClasses;
using SharpEncrypt.Managers;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using SharpEncrypt.Tasks.Aes_Tasks;
using SharpEncrypt.Tasks.File_Tasks;
using SharpEncrypt.Tasks.Folder_Tasks;
using SharpEncrypt.Tasks.Logging_Tasks;
using SharpEncrypt.Tasks.Misc_Tasks;
using SharpEncrypt.Tasks.Otp_Tasks;
using SharpEncrypt.Tasks.Rsa_Tasks;
using SharpEncrypt.Tasks.Test_Tasks;

namespace SharpEncrypt.Forms
{
    internal partial class MainForm : Form
    {
        private readonly ComponentResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly FileSystemManager FileSystemManager = new FileSystemManager();
        private readonly TaskManager TaskManager = new TaskManager();
        private readonly PathHelper PathHelper = new PathHelper();
        private SharpEncryptSettingsModel Settings = new SharpEncryptSettingsModel();

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
        private delegate void SettingsWriteEventHandler(SharpEncryptSettingsModel settings);
        private delegate void FileSecuredEventHandler(FileDataGridItemModel model);
        private delegate void FolderSecuredEventHandler(FolderDataGridItemModel model);
        private delegate void SecuredFileRenamedEventHandler(OnSecuredFileRenamedTaskResult result);
        private delegate void ReadSecuredFileListEventHandler(IEnumerable<FileDataGridItemModel> models);
        private delegate void ReadSecuredFolderListEventHandler(IEnumerable<FolderDataGridItemModel> models);
        private delegate void ExcludedFilesListReadEventHandler(IEnumerable<FileDataGridItemModel> models);
        private delegate void ExcludedFoldersListReadEventHandler(IEnumerable<FolderDataGridItemModel> models);
        private delegate void SettingsFileReadEventHandler(SharpEncryptSettingsModel settings);
        private delegate void TaskExceptionOccurredEventHandler(Exception exception);
        private delegate void OtpPasswordStoreKeyWrittenEventHandler(CreateOtpPasswordStoreKeyTaskResult result);
        private delegate void OtpPasswordStoreReadEventHandler(OpenOtpPasswordStoreTaskResult result);
        private delegate void AesPasswordStoreReadEventHandler(OpenAesPasswordStoreTaskResult result);
        private delegate void LogFileReadEventHandler(string[] lines);
        private delegate void GenericTaskCompletedSuccessfullyEventHandler();
        private delegate void InactivityTaskCompletedEventHandler(int oldTimeout);
        private delegate void FileDecontainerizedEventHandler(DecontainerizeFileTaskResult result);
        private delegate void FolderDecontainerizedEventHandler(DecontainerizeFolderTaskResult result);
        private delegate void TempFilesEncryptedEventHandler(ReencryptTempFilesTaskResult result);

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
        private event OtpPasswordStoreKeyWrittenEventHandler OtpPasswordStoreKeyWritten;
        private event OtpPasswordStoreReadEventHandler OtpPasswordStoreRead;
        private event AesPasswordStoreReadEventHandler AesPasswordStoreRead;
        private event LogFileReadEventHandler LogFileRead;
        private event GenericTaskCompletedSuccessfullyEventHandler GenericTaskCompletedSuccessfully;
        private event FileDecontainerizedEventHandler FileDecontainerized;
        private event InactivityTaskCompletedEventHandler InactivityTaskCompleted;
        private event FolderDecontainerizedEventHandler FolderDecontainerized;
        private event TempFilesEncryptedEventHandler TempFilesEncrypted;

        #endregion

        #region Structs

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        internal struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        #endregion

        #region DLL imports

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO dummy);

        #endregion

        private string SessionPassword { get; set; }

        private bool IsPasswordValid => !string.IsNullOrEmpty(SessionPassword);

        public SharpEncryptSettingsModel AppSettings {
            get => Settings;
            private set {
                Settings = value;
                SetOptions();
            }
        }

        public MainForm() 
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
            SystemEvents.SessionSwitch += OnSessionSwitch;

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
            OtpPasswordStoreKeyWritten += OnOTPPasswordStoreKeyWritten;
            OtpPasswordStoreRead += OnOTPPasswordStoreRead;
            AesPasswordStoreRead += OnAESPasswordStoreRead;
            GenericTaskCompletedSuccessfully += OnGenericTaskCompletedSuccessfully;
            FileDecontainerized += OnFileDecontainerized;
            InactivityTaskCompleted += OnInactivityTaskCompleted;
            FolderDecontainerized += OnFolderDecontainerized;
            TempFilesEncrypted += OnTempFilesEncrypted;

            SecuredFilesGrid.DragDrop += OnSecuredFilesGridDragDrop;
            SecuredFilesGrid.DragEnter += OnSecuredFilesGridDragEnter;

            SecuredFoldersGrid.DragDrop += SecuredFoldersGrid_DragDrop;
            SecuredFoldersGrid.DragEnter += SecuredFoldersGrid_DragEnter;

            TaskManager.TaskCompleted += OnTaskCompleted;
            TaskManager.ExceptionOccurred += OnException;
            TaskManager.DuplicateExclusiveTask += OnDuplicateExclusiveTaskDetected;

            ExcludedFilesListRead += OnExcludedFilesListRead;
            ExcludedFoldersListRead += OnExcludedFoldersListRead;

            LogFileRead += OnLogFileRead;

            SecuredFilesGrid.DataSource = DisplaySecuredFiles;
            SecuredFoldersGrid.DataSource = DisplaySecuredFolders;

            AppLabel.MouseDoubleClick += ApplicationLabelDoubleClicked;

            FileSystemManager.Exception += OnException;
            FileSystemManager.ItemDeleted += ItemDeleted;
            FileSystemManager.ItemRenamed += ItemRenamed;
            FileSystemManager.ItemCreated += ItemCreated;
        }

        private void MainForm_Load(object sender, EventArgs e) => LoadApplication();

        #region Misc methods
        private void ForEachSelectedSecuredFolder(Action<FolderDataGridItemModel> action)
        {
            foreach (DataGridViewRow row in SecuredFoldersGrid.SelectedRows)
            {
                if (row.DataBoundItem is FolderDataGridItemModel model)
                {
                    action(model);
                }
            }
        }

        private void ForEachSelectedSecuredFile(Action<FileDataGridItemModel> action)
        {
            foreach (DataGridViewRow row in SecuredFilesGrid.SelectedRows)
            {
                if (row.DataBoundItem is FileDataGridItemModel model)
                {
                    action(model);
                }
            }
        }

        private void ReencryptTempFiles(bool silent = false, bool exitAfter = false)
        {
            var files = FileSystemManager.TempFiles;

            if (!files.Any() && exitAfter)
            {
                OnExitRequested(true);
            }
            else
            {
                if (IsPasswordValid)
                {
                    TaskManager.AddTask(new ReencryptTempFilesTask(files, SessionPassword, exitAfter));
                }
                else
                {
                    if (!silent)
                    {
                        OnPasswordValidated(() => TaskManager.AddTask(new ReencryptTempFilesTask(files, SessionPassword, exitAfter)));
                    }
                    else
                    {
                        //todo log errors in a temp error file (lasterror.txt or something)
                    }
                }
            }
        }

        private void SecureFolders(params string[] folders)
            => OnPasswordValidated(() =>
            {
                if (folders == null) return;
                foreach (var folder in folders)
                {
                    if (Directory.Exists(folder))
                    {
                        TaskManager.AddTask(new SecureFolderTask(folder, SessionPassword, AppSettings.IncludeSubfolders));
                    }
                }
            });

        private void SecureFiles(params string[] filePaths) 
            => OnPasswordValidated(() =>
            {
                if (filePaths == null) return;
                foreach (var filePath in filePaths)
                {
                    if (File.Exists(filePath))
                    {
                        TaskManager.AddTask(new ContainerizeFileTask(filePath, SessionPassword, ResourceManager.GetString("EncryptedFileExtension")));
                    }
                }
            });

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
            using (var dialog = GetSeefFilesDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                var model = SecuredFiles.FirstOrDefault(x => x.Secured.Equals(dialog.FileName, StringComparison.Ordinal));
                if (model != null)
                {
                    TaskManager.AddTask(new DecontainerizeFileTask(model, SessionPassword, openAfter: true));
                }
                else
                {
                    OnException(new FileNotSecuredException());
                }
            }
        }

        private void LoadApplication()
        {
            SetToolTips();
            LoadExcludedFilesList();
            LoadExcludedFoldersList();
            LoadSecuredFilesList();
            LoadSecuredFoldersList();

            LoadApplicationSettings();
        }

        private void ReloadApplication(bool changeLanguage)
        {
            if (changeLanguage)
                ChangeLanguage(AppSettings.LanguageCode);
        }

        private void OnSettingsFileRead(SharpEncryptSettingsModel settings)
        {
            AppSettings = settings;
            if (AppSettings.LanguageCode != Constants.DefaultLanguage)
                ChangeLanguage(AppSettings.LanguageCode);
            if (AppSettings.InactivityTimeout != Constants.DefaultInactivityTimeout)
                AddTimeoutTask(AppSettings.InactivityTimeout);
            if (!AppSettings.PasswordStartupPromptHide)
                SetSessionPassword();
        }

        [SuppressMessage("ReSharper", "SwitchStatementHandlesSomeKnownEnumValuesWithDefault")]
        private void SetOptions()
        {
            InvokeOnControl(new MethodInvoker(delegate
            {
                DoNotPromptForPasswordOnStartup.Checked = AppSettings.PasswordStartupPromptHide;
                Debug.Checked = AppSettings.DebugEnabled;
                IncludeSubfolders.Checked = AppSettings.IncludeSubfolders;
                UseADifferentPasswordForEachFile.Checked = AppSettings.UseADifferentPasswordForEachFile;
                WipeDiskSpaceAfterSecureDeleteToolStripMenuItem.Checked = AppSettings.WipeFreeSpaceAfterSecureDelete;
                LoggingToolStripMenuItem.Checked = AppSettings.Logging;
                ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem.Checked = AppSettings.ReencryptOnLock;

                if (AppSettings.InactivityTimeout == Constants.DefaultInactivityTimeout)
                {
                    TaskManager.CancelAllExisting(TaskType.InactivityTimeoutTask);
                }

                DebugMenuStrip.Enabled = Debug.Checked;

                switch (AppSettings.StoreType) 
                {
                    case StoreType.Aes:
                        AESPasswordBasedToolStripMenuItem.Checked = true;
                        OTPKeyBasedToolStripMenuItem.Checked = false;
                        break;
                    case StoreType.Otp:
                        OTPKeyBasedToolStripMenuItem.Checked = true;
                        AESPasswordBasedToolStripMenuItem.Checked = false;
                        break;
                }

                TestMenuShow();
            }));
        }

        private void LoadApplicationSettings()
            => TaskManager.AddTask(new ReadSettingsFileTask(PathHelper.AppSettingsPath, 
                TaskType.ReadFileExclusionListTask, 
                TaskType.ReadFolderExclusionListTask,
                TaskType.ReadSecuredFoldersListTask,
                TaskType.ReadSecuredFilesListTask));

        private void LoadExcludedFilesList() => TaskManager.AddTask(new ReadFileExclusionListTask(PathHelper.ExcludedFilesFile));

        private void LoadExcludedFoldersList() => TaskManager.AddTask(new ReadFolderExclusionListTask(PathHelper.ExcludedFoldersFile));

        private void LoadSecuredFoldersList() => TaskManager.AddTask(new ReadSecuredFoldersListTask(PathHelper.SecuredFoldersListFile));

        private void LoadSecuredFilesList() => TaskManager.AddTask(new ReadSecuredFilesListTask(PathHelper.SecuredFilesListFile));

        private static void CloseApplication() => Application.Exit();

        private static string[] ExtractPaths(DragEventArgs args)
        {
            var paths = args.Data.GetData(DataFormats.FileDrop, false);
            if (paths is string[] filePaths)
            {
                return filePaths;
            }
            return null;
        }

        public static uint GetIdleTime()
        {
            var lastUserAction = new LASTINPUTINFO();
            lastUserAction.cbSize = (uint)Marshal.SizeOf(lastUserAction);
            GetLastInputInfo(ref lastUserAction);
            return ((uint)TickCount - lastUserAction.dwTime);
        }

        public static long TickCount => Environment.TickCount;

        private void AddTimeoutTask(int timeout) => TaskManager.AddTask(new InactivityTimeoutTask(timeout));

        private void TransformFileName(bool anonymize)
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
                        TaskManager.AddTask(new RenameFileNameTask(openFileDialog.FileName, SessionPassword, anonymize));
                    }
                }
            }
        }

        private void ShowErrorDialog(string message) => MessageBox.Show(message, ResourceManager.GetString("Error"), MessageBoxButtons.OK);

        private void OpenPasswordStore()
        {
            switch (AppSettings.StoreType)
            {
                case StoreType.Otp:
                    TaskManager.AddTask(new OpenOtpPasswordStoreTask(PathHelper.OtpPasswordStoreFile, AppSettings.OtpStoreKeyFilePath));
                    break;
                case StoreType.Aes:
                {
                    OnPasswordValidated(() =>
                        TaskManager.AddTask(new OpenAesPasswordStoreTask(PathHelper.AesPasswordStoreFile,
                            SessionPassword)));
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OtpPasswordStoreKeyNotFound(KeyFileStoreFileTupleModel tuple)
        {
            if (!DriveInfo.GetDrives().Any(x => x.Name[0].Equals(tuple.KeyFile[0])))
            {
                InvokeOnControl(new MethodInvoker(() =>
                {
                    using (var waitingForDriveForm = new WaitingForDriveForm(tuple))
                    {
                        if (waitingForDriveForm.ShowDialog() == DialogResult.OK)
                        {
                            TaskManager.AddTask(new OpenOtpPasswordStoreTask(tuple.StoreFile, tuple.KeyFile));
                        }
                    }
                }));
            }
            else
            {
                OnException(new FileNotFoundException(tuple.KeyFile));
            }
        }

        private void OtpPasswordStoreFirstUse()
        {
            if (MessageBox.Show(ResourceManager.GetString("AKeyMustBeSavedForThisOTPStore"),
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
                            TaskManager.AddTask(new CreateOtpPasswordStoreKeyTask(PathHelper.OtpPasswordStoreFile, saveFileDialog.FileName, true));
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

        [Conditional("DEBUG")]
        private void TestMenuShow()
        {
            InvokeOnControl(new MethodInvoker(() =>
            {
                TestToolStripMenuItem.Visible = true;
                TestToolStripMenuItem.Enabled = true;
            }));
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

        private OpenFileDialog GetSeefFilesDialog()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = ResourceManager.GetString("EncryptedFileFilter");
                return dialog;
            }
        }

        #endregion

        #region File context menu items

        private void AnonymizeFileNameToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFile((m) => TaskManager.AddTask(new RenameFileNameTask(m.Secured, SessionPassword, true))));

        private void DeanonymizeFileNameToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFile((m) => TaskManager.AddTask(new RenameFileNameTask(m.Secured, SessionPassword, false))));

        private void AddSecuredFileToolStripMenuItem_Click(object sender, EventArgs e) => AddSecured();

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFile((m) => TaskManager.AddTask(new DecontainerizeFileTask(m, SessionPassword, openAfter: true))));

        private void StopSecuringAndRemoveFromListToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFile((m) => TaskManager.AddTask(new DecontainerizeFileTask(m, SessionPassword, true))));

        private void RenameToOriginalToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFile((m) => TaskManager.AddTask(new RenameFileNameTask(m.Secured, SessionPassword, false))));

        private void ShareKeysToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Folder context menu items

        private void BulkAnonymousRenameToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFolder((m) =>
                TaskManager.AddTask(new BulkRenameFolderTask(m.Uri, SessionPassword, AppSettings.IncludeSubfolders,
                    true))));

        private void BulkDeanonymizeFileNamesToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFolder((m) => 
                TaskManager.AddTask(new BulkRenameFolderTask(m.Uri, SessionPassword, AppSettings.IncludeSubfolders, false))));

        private void ShowAllSecuredFolders_Click(object sender, EventArgs e)
            => AddModelsToSecuredFoldersDataGrid(ExcludedFolders.Where(x => Directory.Exists(x.Uri)));

        private void HideExcludedSecuredFoldersToolStripMenuItem_Click(object sender, EventArgs e)
            => RemoveModelsFromSecuredFoldersDataGrid(ExcludedFolders);

        private void ShareKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DecryptPermanentlyToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFolder((m) => TaskManager.AddTask(new DecontainerizeFolderTask(m, SessionPassword, AppSettings.IncludeSubfolders, true))));

        private void DecryptTemporarilyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void AddSecuredFolderToolStripMenuItem_Click(object sender, EventArgs e) => AddSecuredFolder();

        #endregion

        #region File menu items

        private void SetInactivityTimeoutStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var genericTextInput = new GenericTextInput<int>(ResourceManager.GetString("EnterTimeout"), -1))
            {
                if (genericTextInput.ShowDialog() != DialogResult.OK) return;

                TaskManager.CancelAllExisting(TaskType.InactivityTimeoutTask);
                SettingsChangeRequired?.Invoke("InactivityTimeout", genericTextInput.Result);
                if (genericTextInput.Result != Constants.DefaultInactivityTimeout)
                {
                    AddTimeoutTask(genericTextInput.Result);
                }
            }
        }

        private void AESPasswordBasedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OTPKeyBasedToolStripMenuItem.Checked = !AESPasswordBasedToolStripMenuItem.Checked;
            SettingsChangeRequired?.Invoke("StoreType", OTPKeyBasedToolStripMenuItem.Checked ? StoreType.Otp : StoreType.Aes);
        }

        private void OTPKeyBasedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AESPasswordBasedToolStripMenuItem.Checked = !OTPKeyBasedToolStripMenuItem.Checked;
            SettingsChangeRequired?.Invoke("StoreType", OTPKeyBasedToolStripMenuItem.Checked ? StoreType.Otp : StoreType.Aes);
        }

        private void OpenPasswordManagerToolStripMenuItem_Click(object sender, EventArgs e)
            => OpenPasswordStore();

        private void ClearSessionPassword_Click(object sender, EventArgs e)
            => SessionPassword = null;

        private void ShowAllSecuredFiles_Click(object sender, EventArgs e)
            => AddModelsToSecuredFilesDataGrid(ExcludedFiles.Where(x => File.Exists(x.Secured)));

        private void HideExcludedSecuredFilesToolStripMenuItem_Click(object sender, EventArgs e)
            => RemoveModelsFromSecuredFilesDataGrid(ExcludedFiles);

        private void OpenSecuredToolStripMenuItem_Click(object sender, EventArgs e) => OpenSecured();

        private void SecureToolStripMenuItem_Click(object sender, EventArgs e) => AddSecured();

        private void StopSecuringToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SecuredFoldersToolStripMenuItem_Click(object sender, EventArgs e) => Tabs.SelectedTab = Tabs.TabPages[1];

        private void AnonymousRenameToolStripMenuItem_Click(object sender, EventArgs e)
            => TransformFileName(true);

        private void RenameToOriginalToolStripMenuItem1_Click(object sender, EventArgs e)
            => TransformFileName(false);

        private void AddSecuredFolderToolStripMenuItem1_Click(object sender, EventArgs e)
            => AddSecuredFolder();

        private void SecureDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var allFilesDialog = GetAllFilesDialog())
            {
                if (allFilesDialog.ShowDialog() == DialogResult.OK)
                {
                    TaskManager.AddTask(new SecureDeleteFileTask(allFilesDialog.FileName));
                }
            }
        }

        private void SecureDeleteFolder_Click(object sender, EventArgs e)
        {
            using (var folderBrowser = new FolderBrowserDialog())
            {
                if (folderBrowser.ShowDialog() != DialogResult.OK) return;

                if (!string.IsNullOrEmpty(folderBrowser.SelectedPath))
                {
                    TaskManager.AddTask(new ShredDirectoryTask(folderBrowser.SelectedPath, AppSettings.IncludeSubfolders));
                }
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => OnExitRequested(false);

        private void SecureFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AppSettings.OtpDisclaimerHide)
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
                            SettingsChangeRequired?.Invoke("OtpDisclaimerHide", true);
                    }
                }
            }

            using (var openFileDialog = GetAllFilesDialog())
            {
                openFileDialog.Title = ResourceManager.GetString("SelectFile");
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                using (var keyFileOpenFileDialog = new OpenFileDialog())
                {
                    keyFileOpenFileDialog.Title = ResourceManager.GetString("SelectKeyFile");
                    keyFileOpenFileDialog.Filter = ResourceManager.GetString("SharpEncryptOTPKeyFilter");
                    if (keyFileOpenFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        TaskManager.AddTask(new OtpTransformTask(openFileDialog.FileName, ResourceManager.GetString("SharpEncryptOTPEncryptedFileExtension"), keyFileOpenFileDialog.FileName));
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

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                using (var openKeyFileDialog = new OpenFileDialog())
                {
                    openKeyFileDialog.Title = ResourceManager.GetString("SelectKeyFile");
                    openKeyFileDialog.Filter = ResourceManager.GetString("SharpEncryptOTPKeyFilter");
                    if (openKeyFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        TaskManager.AddTask(new OtpTransformTask(openFileDialog.FileName, ResourceManager.GetString("SharpEncryptOTPEncryptedFileExtension"), openKeyFileDialog.FileName, false));
                    }
                }
            }
        }

        private void ChangeSessionPasswordToolStripMenuItem_Click(object sender, EventArgs e) => SetSessionPassword();

        private bool SetSessionPassword()
        {
            using (var dialog = new PasswordInputDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return false;
                {
                    SessionPassword = dialog.Password.Hash();
                    return true;
                }
            }
        }

        private void OnPasswordValidated(Action action)
        {
            if (IsPasswordValid)
            {
                action();
            }
            else
            {
                if (!SetSessionPassword()) return;
                {
                    if (IsPasswordValid)
                    {
                        action();
                    }
                }
            }
        }

        private void ResetAllSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(ResourceManager.GetString("AreYouSure?"), string.Empty, MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            var newSettingsObj = new SharpEncryptSettingsModel();
            var changeLang = AppSettings.LanguageCode != newSettingsObj.LanguageCode;
            SettingsWriteRequired?.Invoke(newSettingsObj);

            AppSettings = newSettingsObj;

            ReloadApplication(changeLang);
        }

        private void ImportPublicKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var importPubKeyForm = new ImportPublicKeyForm())
            {
                if (importPubKeyForm.ShowDialog() == DialogResult.OK)
                {
                    TaskManager.AddTask(new ImportPublicRsaKeyTask(PathHelper.PubKeyFile, importPubKeyForm.Result));
                }
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
                    var pubKey = RsaKeyReaderHelper.GetParameters(@public);
                    using (var dialog = new SaveFileDialog())
                    {
                        dialog.Filter = ResourceManager.GetString("RSAKeyFilter");
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            RsaKeyWriterHelper.Write(dialog.FileName, pubKey);
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
            if (MessageBox.Show(ResourceManager.GetString("KeyPairDisclaimer"), string.Empty, MessageBoxButtons.OKCancel) != DialogResult.OK) return;

            OnPasswordValidated(() => TaskManager.AddTask(new GenerateNewRsaKeyPairTask(PathHelper.KeyPairPaths, SessionPassword)));
        }

        private void GenerateKeyForFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = GetAllFilesDialog())
            {
                openFileDialog.Title = ResourceManager.GetString("SelectReferenceFile");

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = ResourceManager.GetString("SaveKeyFile");
                    saveFileDialog.Filter = ResourceManager.GetString("SharpEncryptOTPKeyFilter");
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        TaskManager.AddTask(new OtpSaveKeyOfFileTask(saveFileDialog.FileName, openFileDialog.FileName));
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

        private void PasswordManagement_Click(object sender, EventArgs e) => OpenPasswordStore();

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

        private void ViewLogToolStripMenuItem_Click(object sender, EventArgs e) => TaskManager.AddTask(new ReadLogFileTask(PathHelper.LoggingFilePath));

        private void ValidateContainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPasswordValidated(() =>
            {
                using (var openFileDialog = GetAllFilesDialog())
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show(ContainerHelper.ValidateContainer(openFileDialog.FileName, SessionPassword)
                            ? string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("ValidContainer") ?? string.Empty, openFileDialog.FileName)
                            : string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("NotAValidContainer") ?? string.Empty, openFileDialog.FileName));
                    }
                }
            });
        }

        private void ViewActiveTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var activeTasksForm = new ActiveTasksForm(TaskManager))
            {
                activeTasksForm.ShowDialog();
            }
        }

        private void ViewCompletedTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var columns = new[] { ResourceManager.GetString("TaskType"), ResourceManager.GetString("Completed") };
            var rows = TaskManager.CompletedTasks.Select(x => new List<object> { x.Task.TaskType, x.Time.ToString(CultureInfo.CurrentCulture) }).ToList();
            using (var completedTasksDialog = new GenericGridForm(columns, rows, ResourceManager.GetString("CompletedTasks")))
            {
                completedTasksDialog.ShowDialog();
            }
        }

        private void LoggingToolStripMenuItem_Click(object sender, EventArgs e) => SettingsChangeRequired?.Invoke("Logging", LoggingToolStripMenuItem.Checked);

        private void CancelAllFutureTasksToolStripMenuItem_Click(object sender, EventArgs e) => TaskManager.SetCancellationFlag();

        private void DeleteGridExclusionList_Click(object sender, EventArgs e)
            => TaskManager.AddTask(new GenericDeleteFilesTask(PathHelper.ExcludedFilesFile, PathHelper.ExcludedFoldersFile));
       
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

        private void ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem_Click(object sender, EventArgs e)
            => SettingsChangeRequired?.Invoke("ReencryptOnLock", ReencryptTemporarilyDecryptedFileOnLockLogoffToolStripMenuItem.Checked);

        #endregion

        #region Handlers

        private void OnTempFilesEncrypted(ReencryptTempFilesTaskResult result)
        {
            if (result.UncontainerizedFiles.Any())
            {
                //todo write to log
            }

            if (result.ExitAfter)
            {
                OnExitRequested(true);
            }
        }

        [SuppressMessage("ReSharper", "SwitchStatementMissingSomeEnumCasesNoDefault")]
        private void OnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLogoff:
                case SessionSwitchReason.SessionLock:
                {
                    if (AppSettings.ReencryptOnLock)
                    {
                        ReencryptTempFiles(true);
                    }

                    break;
                }
                case SessionSwitchReason.SessionLogon:
                {
                    if (AppSettings.ReencryptOnLock)
                    {
                        //todo check for re-encryption log and present user with any errors
                    }

                    break;
                }
            }
        }

        private void OnFolderDecontainerized(DecontainerizeFolderTaskResult result)
        {
            if (!result.RemoveAfter) return;

            InvokeOnControl(new MethodInvoker(() =>
            {
                DisplaySecuredFolders.Remove(result.Model);
                SecuredFolders.Remove(result.Model);
                ExcludedFolders.Remove(result.Model);
            }));

            TaskManager.AddTask(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFile, false, result.Model));
        }

        private void OnInactivityTaskCompleted(int oldTimeout)
        {
            var idle = GetIdleTime();
            if (idle >= oldTimeout)
            {
                ReencryptTempFiles(true, true);
            }
            else
            {
                if (AppSettings.InactivityTimeout == Constants.DefaultInactivityTimeout) return;

                var wait = AppSettings.InactivityTimeout - idle;
                if (wait > 0)
                {
                    if (wait < int.MaxValue && wait > int.MinValue)
                    {
                        TaskManager.AddTask(new InactivityTimeoutTask((int)wait));
                    }
                }
                else
                {
                    ReencryptTempFiles(true, true);
                }
            }
        }

        private void OnGenericTaskCompletedSuccessfully()
        {
            InvokeOnControl(new MethodInvoker(() =>
            {
                MessageBox.Show(ResourceManager.GetString("TaskCompletedSuccessfully"), ResourceManager.GetString("Success"), MessageBoxButtons.OK);
            }));
        }

        private static void ApplicationLabelDoubleClicked(object sender, MouseEventArgs e)
        {
            Process.Start(Constants.ProjectUrl);
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

        private static void OnLogFileRead(string[] lines)
        {
            using (var textViewer = new GenericTextViewer(lines))
            {
                textViewer.ShowDialog();
            }
        }

        private void OnExcludedFoldersListRead(IEnumerable<FolderDataGridItemModel> exclusions)
        {
            var folderDataGridItemModels = exclusions.ToList();
            var removed = folderDataGridItemModels.Where(x => !Directory.Exists(x.Uri)).ToList();
            ExcludedFolders.AddRange(folderDataGridItemModels.Where(x => Directory.Exists(x.Uri)));
            if (removed.Any())
                TaskManager.AddTask(new WriteFolderExclusionListTask(PathHelper.ExcludedFoldersFile, false, removed));
        }

        private void OnExcludedFilesListRead(IEnumerable<FileDataGridItemModel> exclusions)
        {
            var fileDataGridItemModels = exclusions.ToList();
            var removed = fileDataGridItemModels.Where(x => !File.Exists(x.Secured)).ToList();
            ExcludedFiles.AddRange(fileDataGridItemModels.Where(x => File.Exists(x.Secured)));
            if (removed.Any())
                TaskManager.AddTask(new WriteFileExclusionListTask(PathHelper.ExcludedFilesFile, false, removed));
        }

        private void OnNotifyIconDoubleClicked(object sender, EventArgs e) => ShowApplication();

        private void FormResize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized) return;
            ShowInTaskbar = false;
            NotifyIcon.Visible = true;
            NotifyIcon.Text = ResourceManager.GetString("AppName");
            NotifyIcon.BalloonTipText = ResourceManager.GetString("AppName");
            NotifyIcon.ShowBalloonTip(500);
            Hide();
        }

        private void SecuredFoldersGrid_DragDrop(object sender, DragEventArgs e) => SecureFolders(ExtractPaths(e));

        private static void SecuredFoldersGrid_DragEnter(object sender, DragEventArgs e) => e.Effect = DragDropEffects.Copy;

        private static void OnSecuredFilesGridDragEnter(object sender, DragEventArgs e) => e.Effect = DragDropEffects.Copy;

        private void OnSecuredFilesGridDragDrop(object sender, DragEventArgs e) => SecureFiles(ExtractPaths(e));

        private void OnException(Exception exception)
        {
            if (AppSettings.Logging)
                TaskManager.AddTask(new LoggingTask(PathHelper.LoggingFilePath, exception));

            InvokeOnControl(new MethodInvoker(() =>
            {
                var message = exception.Message;
                message += exception.InnerException != null
                    ? $"\n{exception.InnerException.Message}"
                    : $"\n{exception.StackTrace}";

                var exMessage = ResourceManager.GetString("AnExceptionHasOccurred");
                if (exMessage != null)
                {
                    MessageBox.Show(string.Format(CultureInfo.CurrentCulture, exMessage, message),
                    ResourceManager.GetString("Error"),
                    MessageBoxButtons.OK);
                }
            }));
        }

        private void OnSecuredFolderListRead(IEnumerable<FolderDataGridItemModel> folders)
        {
            var folderDataGridItemModels = folders.ToList();
            var existingFolders = folderDataGridItemModels.Where(x => Directory.Exists(x.Uri)).ToList();
            FileSystemManager.AddPaths(existingFolders.Select(x => x.Uri));

            SecuredFolders.AddRange(existingFolders);
            AddModelsToSecuredFoldersDataGrid(SecuredFolders.Where(x => !ExcludedFolders.Any(z => z.Uri.Equals(x.Uri, StringComparison.Ordinal))));

            var removedFolders = folderDataGridItemModels.Where(x => !Directory.Exists(x.Uri)).ToArray();

            if (removedFolders.Any())
                TaskManager.AddTask(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFile, false, removedFolders));
        }

        private void OnSecuredFileListRead(IEnumerable<FileDataGridItemModel> models)
        {
            var fileDataGridItemModels = models.ToList();
            var existing = fileDataGridItemModels.Where(x => File.Exists(x.Secured)).ToArray();
            FileSystemManager.AddPaths(existing.Select(x => x.Secured));

            SecuredFiles.AddRange(existing);
            AddModelsToSecuredFilesDataGrid(SecuredFiles.Where(x => !ExcludedFiles.Any(z => z.Equals(x))));

            var removedFiles = fileDataGridItemModels.Where(x => !File.Exists(x.Secured)).ToArray();
            if (removedFiles.Any())
                TaskManager.AddTask(new WriteSecuredFileListTask(PathHelper.SecuredFilesListFile, false, removedFiles));
        }

        private void OnFolderSecured(FolderDataGridItemModel folderModel)
        {
            TaskManager.AddTask(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFile, true, folderModel));
            FileSystemManager.AddPaths(folderModel.Uri);
            SecuredFolders.Add(folderModel);
            AddModelsToSecuredFoldersDataGrid(folderModel);
        }

        private void OnFileSecured(FileDataGridItemModel model)
        {
            TaskManager.AddTask(new WriteSecuredFileListTask(PathHelper.SecuredFilesListFile, true, model));
            FileSystemManager.AddPaths(model.Secured);
            SecuredFiles.Add(model);
            AddModelsToSecuredFilesDataGrid(model);
        }

        private void SettingsChangeHandler(string settingsPropertyName, object value)
        {
            var prop = typeof(SharpEncryptSettingsModel).GetProperty(settingsPropertyName);
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
                    prop.SetValue(AppSettings, value);
                    SettingsWriteRequired?.Invoke(AppSettings);
                }
            }
        }

        private void SettingsWriteHandler(SharpEncryptSettingsModel settings)
            => TaskManager.AddTask(new WriteSettingsFileTask(PathHelper.AppSettingsPath, settings));

        private void FormClosingHandler(object sender, FormClosingEventArgs e) => OnExitRequested(false);

        private void OnExitRequested(bool silent)
        {
            if (!FileSystemManager.HasTempDecryptedFiles)
            {
                if (!TaskManager.HasCompletedBlockingTasks)
                {
                    InvokeOnControl(new MethodInvoker(() =>
                    {
                        if (!silent)
                        {
                            MessageBox.Show(
                                ResourceManager.GetString("ThereAreActiveTasks"),
                                ResourceManager.GetString("ActiveTasksWarning"),
                                MessageBoxButtons.OK);
                        }

                        using (var activeTasksForm = new ActiveTasksForm(TaskManager, true))
                        {
                            if (activeTasksForm.ShowDialog() == DialogResult.OK)
                            {
                                CloseApplication();
                            }
                        }
                    }));
                }
                else
                {
                    CloseApplication();
                }
            }
            else
            {
                ReencryptTempFiles(true, true);
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
            if (excludedFileModel != null)
            {
                excludedFileModel.Secured = result.NewPath;           
            }

            InvokeOnControl(new MethodInvoker(() =>
            {
                var displayedFileModel = DisplaySecuredFiles.FirstOrDefault(x => x.Secured.Equals(result.OldPath, StringComparison.Ordinal));
                if (displayedFileModel != null)
                {
                    displayedFileModel.Secured = result.NewPath;
                }
            }));
        }

        private void OnOTPPasswordStoreKeyWritten(CreateOtpPasswordStoreKeyTaskResult result)
        {
            SettingsChangeRequired?.Invoke("OtpStoreKeyFilePath", result.KeyPath);
            if (result.OpenAfter)
            {
                TaskManager.AddTask(new OpenOtpPasswordStoreTask(result.StorePath, result.KeyPath));
            }
        }

        private void OnAESPasswordStoreRead(OpenAesPasswordStoreTaskResult result)
            =>  OnPasswordValidated(() => 
            {
                InvokeOnControl(new MethodInvoker(() =>
                {
                    using (var passwordManager = new PasswordManagerForm(result.Models))
                    {
                        if (passwordManager.ShowDialog() == DialogResult.OK)
                        {
                            TaskManager.AddTask(new AesSavePasswordsTask(PathHelper.AesPasswordStoreFile,
                                SessionPassword, passwordManager.PasswordModels));
                        }
                    }
                }));
            });

        private void OnOTPPasswordStoreRead(OpenOtpPasswordStoreTaskResult result)
        {
            InvokeOnControl(new MethodInvoker(() =>
            {
                using (var passwordManager = new PasswordManagerForm(result.Models))
                {
                    if (passwordManager.ShowDialog() == DialogResult.OK)
                    {
                        TaskManager.AddTask(new OtpSavePasswordsTask(result.StorePath, result.KeyPath, passwordManager.PasswordModels));
                    }
                }
            }));
        }

        private void OnFileDecontainerized(DecontainerizeFileTaskResult result)
        {
            if (result.DeleteAfter)
            {
                var fileModel = SecuredFiles.FirstOrDefault(x => x.File.Equals(result.Model.File, StringComparison.Ordinal) && x.Time.Equals(result.Model.Time));
                if (fileModel == null) return;
                InvokeOnControl(new MethodInvoker(() =>
                {
                    DisplaySecuredFiles.Remove(fileModel);
                    SecuredFiles.Remove(fileModel);
                    ExcludedFiles.Remove(fileModel);
                }));

                TaskManager.AddTask(new WriteSecuredFileListTask(PathHelper.SecuredFilesListFile, result.NewPath));
            }
            else if (result.OpenAfter)
            {
                Process.Start(result.NewPath);
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

            SetControlText();
            SetToolTips();
        }

        private void SetControlText()
        {
            Text = ResourceManager.GetString("AppName");
            AppLabel.Text = ResourceManager.GetString("AppName");
            Tabs.TabPages[0].Text = ResourceManager.GetString("SecuredFiles");
            Tabs.TabPages[1].Text = ResourceManager.GetString("SecuredFolders");

            SecuredFilesGrid.Columns[0].HeaderText = ResourceManager.GetString("File");
            SecuredFilesGrid.Columns[1].HeaderText = ResourceManager.GetString("Time");
            SecuredFilesGrid.Columns[2].HeaderText = ResourceManager.GetString("Secured");
            SecuredFilesGrid.Columns[3].HeaderText = ResourceManager.GetString("Algorithm");

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
            if (!AppSettings.ForceExitDisclaimerHide)
            {
                using (var showOnceDialog = new ShowOnceDialog(ResourceManager.GetString("AreYouSure?")))
                {
                    if (showOnceDialog.ShowDialog() != DialogResult.OK) return;
                    if (showOnceDialog.IsChecked)
                        SettingsChangeRequired?.Invoke("ForceExitDisclaimerHide", true);
                    CloseApplication();
                }
            }
            else
            {
                CloseApplication();
            }
        }

        private void IndividualSettingsResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var individualSettingsResetDialog = new IndividualSettingsResetDialog<SharpEncryptSettingsModel>(AppSettings, new SharpEncryptSettingsModel()))
            {
                if (individualSettingsResetDialog.ShowDialog() != DialogResult.OK) return;
                AppSettings = individualSettingsResetDialog.Result;
                SettingsWriteRequired?.Invoke(AppSettings);
            }
        }

        #endregion

        #region Notify icon methods
        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowApplication();
            OnExitRequested(false);
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e) => ShowApplication();

        #endregion

        #region DataGrid methods

        private void AddModelsToSecuredFoldersDataGrid(IEnumerable<FolderDataGridItemModel> models)
        {
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFolders.AddRange(models);
            }));
        }

        private void AddModelsToSecuredFoldersDataGrid(params FolderDataGridItemModel[] models)
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

        private void AddModelsToSecuredFilesDataGrid(params FileDataGridItemModel[] models)
        {
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFiles.AddRange(models);
            }));
        }

        private void AddModelsToSecuredFilesDataGrid(IEnumerable<FileDataGridItemModel> models)
        {
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFiles.AddRange(models);
            }));
        }

        private void ClearSecuredFilesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeOnControl(new MethodInvoker(delegate
            {
                DisplaySecuredFiles.Clear();
                SecuredFilesGrid.Refresh();
            }));
        }

        private void RemoveFileFromListButKeepSecuredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var removals = SecuredFilesGrid.SelectedRows.Select(GetFileModelFromRow)
                .Where(z => z != null)
                .ToList();

            InvokeOnControl(new MethodInvoker(delegate
            {
                ExcludedFiles.AddRange(removals);
                DisplaySecuredFiles.RemoveAll(removals);
                SecuredFilesGrid.Refresh();
            }));

            TaskManager.AddTask(new WriteFileExclusionListTask(PathHelper.ExcludedFilesFile, true, removals));
        }

        private void ShowInFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var paths = SecuredFilesGrid.SelectedRows.Select(GetFileModelFromRow)
                .Where(z => z != null)
                .Select(k => k.Secured);

            foreach (var path in paths)
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
            InvokeOnControl(new MethodInvoker(delegate
            {
                DisplaySecuredFolders.Clear();
                SecuredFoldersGrid.Refresh();
            }));
        }

        private void OpenExplorerHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var models = SecuredFoldersGrid.SelectedRows.Select(GetFolderModelFromRow)
                .Where(z => z != null);

            foreach (var model in models)
            {
                if (Directory.Exists(model.Uri))
                {
                    Process.Start(model.Uri);
                }
            }
        }

        private void RemoveFolderFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var directoryModels = SecuredFoldersGrid.SelectedRows.Select(GetFolderModelFromRow)
                .Where(z => z != null)
                .ToList();

            InvokeOnControl(new MethodInvoker(delegate
            {
                ExcludedFolders.AddRange(directoryModels);
                DisplaySecuredFolders.RemoveAll(directoryModels);
                SecuredFoldersGrid.Refresh();
            }));

            TaskManager.AddTask(new WriteFolderExclusionListTask(PathHelper.ExcludedFoldersFile, true, directoryModels));
        }

        private static FileDataGridItemModel GetFileModelFromRow(object rowObject)
        {
            if (!(rowObject is DataGridViewRow row) || row.DataBoundItem == null) return null;
            if (row.DataBoundItem is FileDataGridItemModel model)
            {
                return model;
            }
            return null;
        }

        private static FolderDataGridItemModel GetFolderModelFromRow(object rowObject)
        {
            if (!(rowObject is DataGridViewRow row) || row.DataBoundItem == null) return null;
            if (row.DataBoundItem is FolderDataGridItemModel model)
            {
                return model;
            }
            return null;
        }

        #endregion

        #region Directory events

        private void ItemRenamed(string newPath, string oldPath, bool inSubFolder) => TaskManager.AddTask(new OnSecuredFileRenamedTask(PathHelper.SecuredFilesListFile, newPath, oldPath));

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
                var folderModel = SecuredFolders.FirstOrDefault(x => x.Uri.Equals(path, StringComparison.Ordinal));
                if (folderModel == null) return;

                InvokeOnControl(new MethodInvoker(() =>
                {
                    DisplaySecuredFolders.Remove(folderModel);
                    SecuredFolders.Remove(folderModel);
                    ExcludedFolders.Remove(folderModel);
                }));
                TaskManager.AddTask(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFile, false, folderModel));
            }
        }

        private void ItemCreated(string path, bool inSubfolder)
        {
            if (inSubfolder && !AppSettings.IncludeSubfolders)
                return;
            TaskManager.AddTask(new ContainerizeFileTask(path, SessionPassword, ResourceManager.GetString("EncryptedFileExtension")));
        }

        #endregion

        #region OnTaskCompleted

        [SuppressMessage("ReSharper", "SwitchStatementMissingSomeEnumCasesNoDefault")]
        private void OnTaskCompleted(SharpEncryptTask task)
        {
            if (task.Result.Exception != null && !(task.Result.Exception is OperationCanceledException))
            {
                switch (task.Result.Exception)
                {
                    case OtpPasswordStoreFirstUseException _:
                        OtpPasswordStoreFirstUse();
                        break;
                    case OtpKeyFileNotAvailableException _ when task.Result.Value is KeyFileStoreFileTupleModel tuple:
                        OtpPasswordStoreKeyNotFound(tuple);
                        break;
                    default:
                        TaskException?.Invoke(task.Result.Exception);
                        break;
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
                    case TaskType.ReadSettingsFileTask when task.Result.Value is SharpEncryptSettingsModel settings:
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
                    case TaskType.ContainerizeFileTask when task.Result.Value is FileDataGridItemModel model:
                        FileSecured?.Invoke(model);
                        break;
                    case TaskType.OnSecuredFileRenamedTask when task.Result.Value is OnSecuredFileRenamedTaskResult result:
                        SecuredFileRenamed?.Invoke(result);
                        break;
                    case TaskType.CreateOtpPasswordStoreKeyTask when task.Result.Value is CreateOtpPasswordStoreKeyTaskResult result:
                        OtpPasswordStoreKeyWritten?.Invoke(result);
                        break;
                    case TaskType.OpenOtpPasswordStoreTask when task.Result.Value is OpenOtpPasswordStoreTaskResult result:
                        OtpPasswordStoreRead?.Invoke(result);
                        break;
                    case TaskType.OpenAesPasswordStoreTask when task.Result.Value is OpenAesPasswordStoreTaskResult result:
                        AesPasswordStoreRead?.Invoke(result);
                        break;
                    case TaskType.BulkRenameFolderTask when task.Result.Exception == null:
                        GenericTaskCompletedSuccessfully?.Invoke();
                        break;
                    case TaskType.DecontainerizeFileTask when task.Result.Value is DecontainerizeFileTaskResult result:
                        FileDecontainerized?.Invoke(result);
                        break;
                    case TaskType.InactivityTimeoutTask when task.Result.Value is int result:
                        InactivityTaskCompleted?.Invoke(result);
                        break;
                    case TaskType.DecontainerizeFolderTask when task.Result.Value is DecontainerizeFolderTaskResult result:
                        FolderDecontainerized?.Invoke(result);
                        break;
                    case TaskType.ReencryptTempFilesTask when task.Result.Value is ReencryptTempFilesTaskResult result:
                        TempFilesEncrypted?.Invoke(result);
                        break;
                }
            }
        }

        #endregion

        #region Test methods

        private void RunFailTaskToolStripMenuItem_Click(object sender, EventArgs e) => TaskManager.AddTask(new FailingTask());

        private void TaskCountToolStripMenuItem_Click_1(object sender, EventArgs e) => MessageBox.Show(TaskManager.ActiveTaskHandlersCount.ToString());

        private void WaitingTaskCountToolStripMenuItem_Click(object sender, EventArgs e) => MessageBox.Show(TaskManager.WaitingListTaskCount.ToString());

        #endregion
    }
}