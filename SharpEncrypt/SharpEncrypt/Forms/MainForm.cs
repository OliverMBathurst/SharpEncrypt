using SharpEncrypt.AbstractClasses;
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
using System.Security.Cryptography;
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
        private readonly List<FileModel> ExcludedFiles = new List<FileModel>();
        private readonly List<FolderModel> ExcludedFolders = new List<FolderModel>();

        //Lists of models to display in their respective grids
        private readonly BindingList<FileModel> DisplaySecuredFiles = new BindingList<FileModel>();
        private readonly BindingList<FolderModel> DisplaySecuredFolders = new BindingList<FolderModel>();

        //Master lists containing all secured file and folder models
        private readonly List<FileModel> SecuredFiles = new List<FileModel>();
        private readonly List<FolderModel> SecuredFolders = new List<FolderModel>();
        #endregion

        #region Delegates and Events

        private delegate void SettingsChangeEventHandler(string settingsPropertyName, object value);
        private delegate void SettingsWriteEventHandler(SharpEncryptSettingsModel settings);
        private delegate void FileSecuredEventHandler(FileModel model);
        private delegate void FolderSecuredEventHandler(FolderModel model);
        private delegate void SecuredFileRenamedEventHandler(SecuredFileRenamedTaskResultModel resultModel);
        private delegate void ReadSecuredFileListEventHandler(IEnumerable<FileModel> models);
        private delegate void ReadSecuredFolderListEventHandler(IEnumerable<FolderModel> models);
        private delegate void ExcludedFilesListReadEventHandler(IEnumerable<FileModel> models);
        private delegate void ExcludedFoldersListReadEventHandler(IEnumerable<FolderModel> models);
        private delegate void UncontainerizedFilesListReadEventHandler(IEnumerable<FolderModel> paths);
        private delegate void LogFileReadEventHandler(IEnumerable<string> lines);
        private delegate void SettingsFileReadEventHandler(SharpEncryptSettingsModel settings);
        private delegate void TaskExceptionOccurredEventHandler(Exception exception);
        private delegate void OtpPasswordStoreKeyWrittenEventHandler(CreateOtpPasswordStoreKeyTaskResult result);
        private delegate void OtpPasswordStoreReadEventHandler(OpenOtpPasswordStoreTaskResult result);
        private delegate void AesPasswordStoreReadEventHandler(OpenAesPasswordStoreTaskResult result);
        private delegate void GenericTaskCompletedSuccessfullyEventHandler();
        private delegate void InactivityTaskCompletedEventHandler(int oldTimeout);
        private delegate void FileDecontainerizedEventHandler(DecontainerizeFileTaskResult result);
        private delegate void FolderDecontainerizedEventHandler(DecontainerizeFolderFilesTaskResult result);
        private delegate void TempFoldersEncryptedEventHandler(EncryptTempFoldersTaskResultModel resultModel);
        private delegate void TemporaryFoldersEncryptedEventHandler(List<FolderModel> paths);
        private delegate void BulkExportKeysTaskCompletedEventHandler(BulkExportKeysTaskResult result);

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
        private event TempFoldersEncryptedEventHandler TempFoldersEncrypted;
        private event UncontainerizedFilesListReadEventHandler UncontainerizedFilesListRead;
        private event TemporaryFoldersEncryptedEventHandler TemporaryFoldersEncrypted;
        private event BulkExportKeysTaskCompletedEventHandler BulkKeyExportCompleted;

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

        private bool ExitPending { get; set; }

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
            TempFoldersEncrypted += OnTempFoldersEncrypted;
            UncontainerizedFilesListRead += OnUncontainerizedFilesListRead;
            TemporaryFoldersEncrypted += FileSystemManager.OnTemporaryFoldersEncrypted;
            BulkKeyExportCompleted += OnBulkKeyExportCompleted; 

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
        private void ForEachSelectedSecuredFolder(Action<FolderModel> action)
        {
            foreach (DataGridViewRow row in SecuredFoldersGrid.SelectedRows)
            {
                if (row.DataBoundItem is FolderModel model)
                {
                    action(model);
                }
            }
        }

        private void ForEachSelectedSecuredFile(Action<FileModel> action)
        {
            foreach (DataGridViewRow row in SecuredFilesGrid.SelectedRows)
            {
                if (row.DataBoundItem is FileModel model)
                {
                    action(model);
                }
            }
        }

        private void ForAllSelectedFiles(Action<IEnumerable<FileModel>> action) =>
            action(SecuredFilesGrid.SelectedRows
                .Select(x => x)
                .Where(x => x is DataGridViewRow row && row.DataBoundItem is FileModel)
                .Select(z => (FileModel)((DataGridViewRow)z).DataBoundItem));

        private void EncryptTempFolders(bool silent = true, bool exitAfter = true)
        {
            var folders = FileSystemManager.FolderModels
                .Distinct()
                .ToList();

            if (folders.Any())
            {
                if (IsPasswordValid)
                {
                    TaskManager.AddTask(new EncryptTempFoldersTask(
                        folders, 
                        new ContainerizationSettings(SessionPassword, ResourceManager.GetString("EncryptedFileExtension")), 
                        AppSettings.IncludeSubfolders,
                        exitAfter, 
                        silent));
                }
                else
                {
                    if (!silent)
                    {
                        OnPasswordValidated(() => TaskManager.AddTask(new EncryptTempFoldersTask(
                            folders,
                            new ContainerizationSettings(SessionPassword, ResourceManager.GetString("EncryptedFileExtension")), 
                            AppSettings.IncludeSubfolders,
                            exitAfter, 
                            false)));
                    }
                    else
                    {
                        TaskManager.AddTask(new WriteUncontainerizedFoldersListTask(PathHelper.UncontainerizedFoldersLoggingFilePath, folders, exitAfter, true));
                    }
                }
            }
            else
            {
                if (exitAfter)
                {
                    OnExitRequested(silent);
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
                        TaskManager.AddTask(new SecureFolderTask(folder, new ContainerizationSettings(SessionPassword, ResourceManager.GetString("EncryptedFileExtension")), AppSettings.IncludeSubfolders));
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
                        TaskManager.AddTask(new ContainerizeFileTask(filePath, new ContainerizationSettings(SessionPassword, ResourceManager.GetString("EncryptedFileExtension"))));
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
                TaskManager.AddTask(model != null
                    ? new DecontainerizeFileTask(model, SessionPassword, openAfter: true)
                    : new DecontainerizeFileTask(dialog.FileName, SessionPassword, openAfter: true));
            }
        }

        private void LoadApplication()
        {
            SetToolTips();
            LoadExcludedFilesList();
            LoadExcludedFoldersList();
            LoadSecuredFilesList();
            LoadSecuredFoldersList();
            LoadUncontainerizedFilesList();
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
            => TaskManager.AddTask(new GenericDeserializationTask<SharpEncryptSettingsModel>(PathHelper.AppSettingsPath, TaskType.ReadSettingsFileTask,
                TaskType.ReadFileExclusionListTask,
                TaskType.ReadFolderExclusionListTask,
                TaskType.ReadSecuredFoldersListTask,
                TaskType.ReadSecuredFilesListTask,
                TaskType.ReadUncontainerizedFoldersListTask));

        private void LoadExcludedFilesList() => TaskManager.AddTask(new GenericDeserializationTask<IEnumerable<FileModel>>(PathHelper.ExcludedFilesFile, TaskType.ReadFileExclusionListTask));

        private void LoadExcludedFoldersList() => TaskManager.AddTask(new GenericDeserializationTask<List<FolderModel>>(PathHelper.ExcludedFoldersFile, TaskType.ReadFolderExclusionListTask));

        private void LoadSecuredFoldersList() => TaskManager.AddTask(new GenericDeserializationTask<List<FolderModel>>(PathHelper.SecuredFoldersListFile, TaskType.ReadSecuredFoldersListTask, new List<FolderModel>()));

        private void LoadSecuredFilesList() => TaskManager.AddTask(new GenericDeserializationTask<IEnumerable<FileModel>>(PathHelper.SecuredFilesListFile, TaskType.ReadSecuredFilesListTask));

        private void LoadUncontainerizedFilesList() 
            => TaskManager.AddTask(new GenericDeserializationTask<IEnumerable<FolderModel>>(PathHelper.UncontainerizedFoldersLoggingFilePath,
                    TaskType.ReadUncontainerizedFoldersListTask) { IsExclusive = true });

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
            OnPasswordValidated(() =>
            {
                using (var openFileDialog = GetAllFilesDialog())
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        TaskManager.AddTask(new RenameFileNameTask(openFileDialog.FileName, SessionPassword, anonymize));
                    }
                }
            });
        }

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
            => OnPasswordValidated(() => ForEachSelectedSecuredFile(m => TaskManager.AddTask(new RenameFileNameTask(m.Secured, SessionPassword, true))));

        private void DeanonymizeFileNameToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFile(m => TaskManager.AddTask(new RenameFileNameTask(m.Secured, SessionPassword, false))));

        private void AddSecuredFileToolStripMenuItem_Click(object sender, EventArgs e) => AddSecured();

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFile(m => TaskManager.AddTask(new DecontainerizeFileTask(m, SessionPassword, openAfter: true))));

        private void StopSecuringAndRemoveFromListToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFile(m => TaskManager.AddTask(new DecontainerizeFileTask(m, SessionPassword, true))));

        private void RenameToOriginalToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFile(m => TaskManager.AddTask(new RenameFileNameTask(m.Secured, SessionPassword, false))));

        private void ShareKeysToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForAllSelectedFiles(models =>
            {
                if (!models.Any()) return;

                OnPrivateKeyPasswordGiven(privateKey =>
                {
                    using (var folderDialog = new FolderBrowserDialog())
                    {
                        if (folderDialog.ShowDialog() != DialogResult.OK) return;

                        TaskManager.AddTask(new BulkExportKeysTask(
                            folderDialog.SelectedPath,
                            SessionPassword, 
                            models, 
                            privateKey, 
                            ResourceManager.GetString("ShareableKeyExtension")));
                    }
                });
            }));

        private void FileDecryptPermanently_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFile(model => TaskManager.AddTask(new DecontainerizeFileTask(model.Secured, SessionPassword, true))));

        private void OnPrivateKeyPasswordGiven(Action<RSAParameters> action)
        {
            if (!File.Exists(PathHelper.KeyPairPaths.PrivateKey))
            {
                MessageBox.Show(ResourceManager.GetString("UserPrivateKeyDoesNotExist"), string.Empty, MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(ResourceManager.GetString("ProvidePrivateKeyDecryptionKey"), string.Empty, MessageBoxButtons.OK);

                using (var passwordInput = new PasswordInputDialog(true, sessionPassword: SessionPassword))
                {
                    if (passwordInput.ShowDialog() != DialogResult.OK) return;

                    var privateKey = RsaKeyReaderHelper.GetParameters(PathHelper.KeyPairPaths.PrivateKey, passwordInput.Password);

                    if (privateKey.Exponent == null || privateKey.Modulus == null
                                                    || !privateKey.Exponent.Any()
                                                    || !privateKey.Modulus.Any())
                    {
                        MessageBox.Show(ResourceManager.GetString("PrivateKeyDecryptionKeyIncorrect"), string.Empty, MessageBoxButtons.OK);
                    }
                    else
                    {
                        action(privateKey);
                    }
                }
            }
        }

        #endregion

        #region Folder context menu items

        private void BulkAnonymousRenameToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFolder(m =>
                TaskManager.AddTask(new BulkRenameFolderTask(m.Uri, SessionPassword, AppSettings.IncludeSubfolders,
                    true))));

        private void BulkDeanonymizeFileNamesToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFolder(m => 
                TaskManager.AddTask(new BulkRenameFolderTask(m.Uri, SessionPassword, AppSettings.IncludeSubfolders, false))));

        private void ShowAllSecuredFolders_Click(object sender, EventArgs e)
            => AddModelsToSecuredFoldersDataGrid(ExcludedFolders.Where(x => Directory.Exists(x.Uri)));

        private void HideExcludedSecuredFoldersToolStripMenuItem_Click(object sender, EventArgs e)
            => RemoveModelsFromSecuredFoldersDataGrid(ExcludedFolders);

        private void DecryptPermanentlyToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFolder(model => 
                TaskManager.AddTask(new DecontainerizeFolderFilesTask(
                    model, 
                    new ContainerizationSettings(SessionPassword, ResourceManager.GetString("EncryptedFileExtension")), 
                    AppSettings.IncludeSubfolders, 
                    true, 
                    false))));

        private void DecryptTemporarilyToolStripMenuItem_Click(object sender, EventArgs e)
            => OnPasswordValidated(() => ForEachSelectedSecuredFolder(model =>
                TaskManager.AddTask(new DecontainerizeFolderFilesTask(
                    model, 
                    new ContainerizationSettings(SessionPassword, ResourceManager.GetString("EncryptedFileExtension")), 
                    AppSettings.IncludeSubfolders,
                    true,
                    true))));

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

        private void OpenPasswordManagerToolStripMenuItem_Click(object sender, EventArgs e) => OpenPasswordStore();

        private void ClearSessionPassword_Click(object sender, EventArgs e) => SessionPassword = null;

        private void ShowAllSecuredFiles_Click(object sender, EventArgs e) => AddModelsToSecuredFilesDataGrid(ExcludedFiles.Where(x => File.Exists(x.Secured)));

        private void HideExcludedSecuredFilesToolStripMenuItem_Click(object sender, EventArgs e) => RemoveModelsFromSecuredFilesDataGrid(ExcludedFiles);

        private void OpenSecuredToolStripMenuItem_Click(object sender, EventArgs e) => OpenSecured();

        private void SecureToolStripMenuItem_Click(object sender, EventArgs e) => AddSecured();

        private void StopSecuringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPasswordValidated(() =>
            {
                using (var fileDialog = GetSeefFilesDialog())
                {
                    if (fileDialog.ShowDialog() != DialogResult.OK) return;

                    var fileName = fileDialog.FileName;
                    var model = SecuredFiles.FirstOrDefault(x => x.Secured.Equals(fileName));
                    if (model == null)
                    {
                        TaskManager.AddTask(new DecontainerizeOtherFileTask(fileName, SessionPassword));
                    }
                    else
                    {
                        TaskManager.AddTask(new DecontainerizeFileTask(model, SessionPassword, true));
                    }
                }
            });
        }

        private void SecuredFoldersToolStripMenuItem_Click(object sender, EventArgs e) => Tabs.SelectedTab = Tabs.TabPages[1];

        private void AnonymousRenameToolStripMenuItem_Click(object sender, EventArgs e) => TransformFileName(true);

        private void RenameToOriginalToolStripMenuItem1_Click(object sender, EventArgs e) => TransformFileName(false);

        private void AddSecuredFolderToolStripMenuItem1_Click(object sender, EventArgs e) => AddSecuredFolder();

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

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ExitPending)
                OnExitRequested(false);
        }

        private void SecureFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AppSettings.OtpDisclaimerHide)
            {
                using (var showOnceDialog = new ShowOnceDialog(ResourceManager.GetString("OTPDisclaimer")))
                {
                    if (showOnceDialog.ShowDialog() != DialogResult.OK)
                        return;

                    if (showOnceDialog.IsChecked)
                        SettingsChangeRequired?.Invoke("OtpDisclaimerHide", true);
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
                    TaskManager.AddTask(new ImportPublicRsaKeyTask(PathHelper.OtherUsersPubKeyFile, importPubKeyForm.Result));
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
            using (var openFileDialog = GetSeefFilesDialog())
            {
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                using (var filePasswordInput = new PasswordInputDialog(true, sessionPassword: SessionPassword))
                {
                    if (filePasswordInput.ShowDialog() != DialogResult.OK) return;

                    if (!ContainerHelper.ValidateContainer(openFileDialog.FileName, filePasswordInput.Password))
                    {
                        MessageBox.Show(ResourceManager.GetString("ProvidedPasswordIsInvalid"));
                    }
                    else
                    {
                        if (!File.Exists(PathHelper.KeyPairPaths.PrivateKey))
                        {
                            MessageBox.Show(ResourceManager.GetString("UserPrivateKeyDoesNotExist"), string.Empty,
                                MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show(ResourceManager.GetString("ProvidePrivateKeyDecryptionKey"), string.Empty,
                                MessageBoxButtons.OK);

                            using (var passwordInput = new PasswordInputDialog(true, sessionPassword: SessionPassword))
                            {
                                if (passwordInput.ShowDialog() != DialogResult.OK) return;

                                var privateKey = RsaKeyReaderHelper.GetParameters(PathHelper.KeyPairPaths.PrivateKey, passwordInput.Password);

                                if (privateKey.Exponent != null && privateKey.Modulus != null
                                                                && privateKey.Exponent.Any() &&
                                                                privateKey.Modulus.Any())
                                {
                                    using (var saveDialog = new SaveFileDialog())
                                    {
                                        saveDialog.Filter = ResourceManager.GetString("ShareableKeyFilter");
                                        if (saveDialog.ShowDialog() != DialogResult.OK) return;

                                        TaskManager.AddTask(new CreateShareableKeyTask(
                                            privateKey, 
                                            saveDialog.FileName,
                                            filePasswordInput.Password));
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(ResourceManager.GetString("PrivateKeyDecryptionKeyIncorrect"),
                                        string.Empty, MessageBoxButtons.OK);
                                }
                            }
                        }
                    }
                }
            }
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
            var rows = TaskManager.CompletedTasks.Select(x => new RowModel 
                {
                    Cells = new List<CellModel>
                    {
                        new CellModel
                        {
                            CellType = CellType.TextBox,
                            Value = x.Task.TaskType
                        },
                        new CellModel
                        {
                            Value = x.Time.ToString(CultureInfo.CurrentCulture)
                        }
                    }
                }).ToList();

            using (var completedTasksDialog = new GenericGridForm(
                GridHelper.GetCompletedJobsColumnDefinitions(ResourceManager), 
                rows, 
                ResourceManager.GetString("CompletedTasks")))
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

        private void OnBulkKeyExportCompleted(BulkExportKeysTaskResult result)
            => MessageBox.Show(string.Format(ResourceManager.GetString("TheFollowingKeyFilesWereNotExported") ?? "{0}",
                    string.Join("\n", result.NotCreated)));

        private void OnUncontainerizedFilesListRead(IEnumerable<FolderModel> paths)
            => MessageBox.Show(string.Format(ResourceManager.GetString("TheFollowingWereNotReEncrypted") ?? "{0}",
                string.Join("\n", paths)));

        private void OnTempFoldersEncrypted(EncryptTempFoldersTaskResultModel model)
        {
            if (model.ContainerizedFolders.Any())
            {
                TemporaryFoldersEncrypted?.Invoke(model.ContainerizedFolders);
                TaskManager.AddTask(new WriteSecuredFoldersListTask(
                    PathHelper.SecuredFoldersListFile, 
                    true, 
                    model.ContainerizedFolders.Select(x => new FolderModel { Time = DateTime.Now, Uri = x.Uri }).ToArray()));
            }

            if (model.UncontainerizedFolders.Any())
            {
                if (!model.Silent)
                {
                    MessageBox.Show(string.Format(ResourceManager.GetString("TheFollowingWereNotReEncrypted") ?? "{0}", string.Join("\n", model.UncontainerizedFolders)));
                }
                else
                {
                    TaskManager.AddTask(new WriteUncontainerizedFoldersListTask(PathHelper.UncontainerizedFoldersLoggingFilePath, model.UncontainerizedFolders, model.ExitAfter, model.Silent));
                }
            }
            else
            {
                if (model.ExitAfter)
                {
                    OnExitRequested(model.Silent);
                }
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
                        EncryptTempFolders(exitAfter: false);
                    }
                    break;
                }
                case SessionSwitchReason.SessionLogon:
                {
                    if (AppSettings.ReencryptOnLock)
                    {
                        var task = new GenericDeserializationTask<IEnumerable<FolderModel>>(
                            PathHelper.UncontainerizedFoldersLoggingFilePath,
                            TaskType.ReadUncontainerizedFoldersListTask) {IsExclusive = true};
                        TaskManager.AddTask(task);
                    }
                    break;
                }
            }
        }

        private void OnFolderDecontainerized(DecontainerizeFolderFilesTaskResult result)
        {
            if (result.Temporary)
            {
                FileSystemManager.AddTempFolder(result.Model);
            }

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
                EncryptTempFolders();
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
                    EncryptTempFolders();
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

        private static void ApplicationLabelDoubleClicked(object sender, MouseEventArgs e) => Process.Start(Constants.ProjectUrl);

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e) => OnException(e.ExceptionObject as Exception);

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

        private static void OnLogFileRead(IEnumerable<string> lines)
        {
            using (var textViewer = new GenericTextViewer(lines))
            {
                textViewer.ShowDialog();
            }
        }

        private void OnExcludedFoldersListRead(IEnumerable<FolderModel> exclusions)
        {
            var folderModels = exclusions.ToList();
            var removed = folderModels.Where(x => !Directory.Exists(x.Uri)).ToList();
            ExcludedFolders.AddRange(folderModels.Where(x => Directory.Exists(x.Uri)));
            if (removed.Any())
                TaskManager.AddTask(new WriteFolderExclusionListTask(PathHelper.ExcludedFoldersFile, false, removed));
        }

        private void OnExcludedFilesListRead(IEnumerable<FileModel> exclusions)
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

        private void OnSecuredFolderListRead(IEnumerable<FolderModel> folders)
        {
            var folderModels = folders.ToList();
            var existingFolders = folderModels.Where(x => Directory.Exists(x.Uri)).ToList();
            FileSystemManager.AddPaths(existingFolders.Select(x => x.Uri));

            SecuredFolders.AddRange(existingFolders);
            AddModelsToSecuredFoldersDataGrid(SecuredFolders.Where(x => !ExcludedFolders.Any(z => z.Uri.Equals(x.Uri, StringComparison.Ordinal))));

            var removedFolders = folderModels.Where(x => !Directory.Exists(x.Uri)).ToArray();

            if (removedFolders.Any())
                TaskManager.AddTask(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFile, false, removedFolders));
        }

        private void OnSecuredFileListRead(IEnumerable<FileModel> models)
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

        private void OnFolderSecured(FolderModel folderModel)
        {
            TaskManager.AddTask(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFile, true, folderModel));
            FileSystemManager.AddPaths(folderModel.Uri);
            SecuredFolders.Add(folderModel);
            AddModelsToSecuredFoldersDataGrid(folderModel);
        }

        private void OnFileSecured(FileModel model)
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

        private void FormClosingHandler(object sender, FormClosingEventArgs e)
        {
            if (!ExitPending)
                OnExitRequested(e.CloseReason != CloseReason.UserClosing);
        }

        private void OnExitRequested(bool silent)
        {
            if (!ExitPending)
                ExitPending = true;

            if (!FileSystemManager.HasTempDecryptedFolders)
            {
                if (!TaskManager.HasCompletedBlockingTasks)
                {
                    if (!silent)
                    {
                        InvokeOnControl(new MethodInvoker(() =>
                        {
                            MessageBox.Show(
                                ResourceManager.GetString("ThereAreActiveTasks"),
                                ResourceManager.GetString("ActiveTasksWarning"),
                                MessageBoxButtons.OK);

                            using (var activeTasksForm = new ActiveTasksForm(TaskManager))
                            {
                                activeTasksForm.AllTasksCompleted += CloseApplication;
                                activeTasksForm.ShowDialog();
                            }

                            ExitPending = false;
                        }));
                    }
                    else
                    {
                        TaskManager.BlockingTasksCompleted += CloseApplication;
                        if (TaskManager.HasCompletedBlockingTasks)
                            CloseApplication();
                    }
                }
                else
                {
                    CloseApplication();
                }
            }
            else
            {
                EncryptTempFolders(silent);
            }
        }

        private void OnSecuredFileRenamed(SecuredFileRenamedTaskResultModel resultModel)
        {
            var fileModel = SecuredFiles.FirstOrDefault(x => x.Secured.Equals(resultModel.OldPath, StringComparison.Ordinal));
            if (fileModel != null)
            {
                fileModel.Secured = resultModel.NewPath;             
            }

            var excludedFileModel = ExcludedFiles.FirstOrDefault(x => x.Secured.Equals(resultModel.OldPath, StringComparison.Ordinal));
            if (excludedFileModel != null)
            {
                excludedFileModel.Secured = resultModel.NewPath;           
            }

            InvokeOnControl(new MethodInvoker(() =>
            {
                var displayedFileModel = DisplaySecuredFiles.FirstOrDefault(x => x.Secured.Equals(resultModel.OldPath, StringComparison.Ordinal));
                if (displayedFileModel != null)
                {
                    displayedFileModel.Secured = resultModel.NewPath;
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

        private void AddModelsToSecuredFoldersDataGrid(IEnumerable<FolderModel> models) =>
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFolders.AddRange(models);
            }));

        private void AddModelsToSecuredFoldersDataGrid(params FolderModel[] models) =>
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFolders.AddRange(models);
            }));

        private void RemoveModelsFromSecuredFoldersDataGrid(IEnumerable<FolderModel> models) =>
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFolders.RemoveAll(models);
            }));

        private void RemoveModelsFromSecuredFilesDataGrid(IEnumerable<FileModel> models) =>
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFiles.RemoveAll(models);
            }));

        private void AddModelsToSecuredFilesDataGrid(params FileModel[] models) =>
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFiles.AddRange(models);
            }));

        private void AddModelsToSecuredFilesDataGrid(IEnumerable<FileModel> models) =>
            InvokeOnControl(new MethodInvoker(() => {
                DisplaySecuredFiles.AddRange(models);
            }));

        private void ClearSecuredFilesListToolStripMenuItem_Click(object sender, EventArgs e) =>
            InvokeOnControl(new MethodInvoker(delegate
            {
                DisplaySecuredFiles.Clear();
                SecuredFilesGrid.Refresh();
            }));

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
                var dir = DirectoryHelper.GetDirectoryPath(path);
                if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
                {
                    Process.Start(dir);
                }
            }
        }

        private void ClearSecuredFoldersGridToolStripMenuItem_Click(object sender, EventArgs e) =>
            InvokeOnControl(new MethodInvoker(delegate
            {
                DisplaySecuredFolders.Clear();
                SecuredFoldersGrid.Refresh();
            }));

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

        private static FileModel GetFileModelFromRow(object rowObject)
        {
            if (!(rowObject is DataGridViewRow row) || row.DataBoundItem == null) return null;
            if (row.DataBoundItem is FileModel model)
            {
                return model;
            }
            return null;
        }

        private static FolderModel GetFolderModelFromRow(object rowObject)
        {
            if (!(rowObject is DataGridViewRow row) || row.DataBoundItem == null) return null;
            if (row.DataBoundItem is FolderModel model)
            {
                return model;
            }
            return null;
        }

        #endregion

        #region Directory events

        private void ItemRenamed(string newPath, string oldPath, bool inSubFolder) => TaskManager.AddTask(new SecuredFileRenamedTask(PathHelper.SecuredFilesListFile, newPath, oldPath));

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
            TaskManager.AddTask(new ContainerizeFileTask(path, new ContainerizationSettings(SessionPassword, ResourceManager.GetString("EncryptedFileExtension"))));
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
                    case TaskType.ReadSecuredFilesListTask when task.Result.Value is IEnumerable<FileModel> models:
                        SecuredFileListRead?.Invoke(models);
                        break;
                    case TaskType.ReadSecuredFoldersListTask when task.Result.Value is IEnumerable<FolderModel> models:
                        SecuredFolderListRead?.Invoke(models);
                        break;
                    case TaskType.ReadFileExclusionListTask when task.Result.Value is IEnumerable<FileModel> models:
                        ExcludedFilesListRead?.Invoke(models);
                        break;
                    case TaskType.ReadFolderExclusionListTask when task.Result.Value is IEnumerable<FolderModel> models:
                        ExcludedFoldersListRead?.Invoke(models);
                        break;
                    case TaskType.ReadLogFileTask when task.Result.Value is IEnumerable<string> lines:
                        LogFileRead?.Invoke(lines);
                        break;
                    case TaskType.ReadUncontainerizedFoldersListTask when task.Result.Value is IEnumerable<FolderModel> uncontainerizedFiles:
                        UncontainerizedFilesListRead?.Invoke(uncontainerizedFiles);
                        break;
                    case TaskType.SecureFolderTask when task.Result.Value is FolderModel model:
                        FolderSecured?.Invoke(model);
                        break;
                    case TaskType.ReadSettingsFileTask when task.Result.Value is SharpEncryptSettingsModel settings:
                        SettingsFileRead?.Invoke(settings);
                        break;
                    case TaskType.ContainerizeFileTask when task.Result.Value is FileModel model:
                        FileSecured?.Invoke(model);
                        break;
                    case TaskType.SecuredFileRenamedTask when task.Result.Value is SecuredFileRenamedTaskResultModel result:
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
                    case TaskType.DecontainerizeFolderFilesTask when task.Result.Value is DecontainerizeFolderFilesTaskResult result:
                        FolderDecontainerized?.Invoke(result);
                        break;
                    case TaskType.EncryptTempFoldersTask when task.Result.Value is EncryptTempFoldersTaskResultModel result:
                        TempFoldersEncrypted?.Invoke(result);
                        break;                    
                    case TaskType.WriteUncontainerizedFoldersListTask when task.Result.Value is FinalizableTaskResultModel model:
                        if (model.ExitAfter)
                            OnExitRequested(model.Silent);
                        break;
                    case TaskType.BulkExportKeysTask when task.Result.Value is BulkExportKeysTaskResult result:
                        BulkKeyExportCompleted?.Invoke(result);
                        break;
                }
            }
        }

        #endregion

        #region Test methods

        private void RunFailTaskToolStripMenuItem_Click(object sender, EventArgs e) => TaskManager.AddTask(new FailingTask());

        private void TaskCountToolStripMenuItem_Click_1(object sender, EventArgs e) => MessageBox.Show(TaskManager.ActiveTaskHandlersCount.ToString());

        private void WaitingTaskCountToolStripMenuItem_Click(object sender, EventArgs e) => MessageBox.Show(TaskManager.WaitingListTaskCount.ToString());

        private void RunLongRunningTaskToolStripMenuItem_Click(object sender, EventArgs e) => TaskManager.AddTask(new LongRunningTask(false));

        private void RunLongRunningBlockingTaskToolStripMenuItem_Click(object sender, EventArgs e) => TaskManager.AddTask(new LongRunningTask(true));

        #endregion

        private void OpenSomeonesFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //todo
        }
    }
}