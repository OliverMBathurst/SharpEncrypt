using OTPLibrary;
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
        private readonly List<FileDataGridItemModel> SecuredFiles = new List<FileDataGridItemModel>();

        #region Delegates and Events
        private delegate void SettingsChangeDelegate(string settingsPropertyName, object value);
        private delegate void SettingsWriterDelegate(SharpEncryptSettings settings);
        private delegate void FileSecured(string filePath, string newFilePath);
        private delegate void FolderSecured(string dirPath);
        private delegate void SecuredFilesAdded(IEnumerable<FileDataGridItemModel> model);
        private delegate void ReadSecuredFileList(IEnumerable<FileDataGridItemModel> models);
        private delegate void ReadSecuredFolderList(IEnumerable<string> folders);
        private delegate void SettingsFileRead(SharpEncryptSettings settings);
        private delegate void TaskExceptionOccurred(Exception exception);

        private readonly SettingsWriterDelegate DefaultSettingsWriterDelegate;
        private readonly SettingsChangeDelegate DefaultSettingsChangeDelegate;
        private readonly BackgroundTaskHandler TaskHandler = new BackgroundTaskHandler();
        private readonly PathHelper PathHelper = new PathHelper();

        private event FileSecured OnFileSecured;
        private event FolderSecured OnFolderSecured;
        private event SecuredFilesAdded OnSecuredFileAdded;
        private event ReadSecuredFileList OnSecureFileListRead;
        private event ReadSecuredFolderList OnSecuredFolderListRead;
        private event SettingsFileRead OnSettingsFileRead;
        private event TaskExceptionOccurred OnTaskException;

        #endregion

        private string Password { get; set; }

        private SharpEncryptSettings Settings { get; set; }

        public MainForm() 
        {
            InitializeComponent();
            DefaultSettingsWriterDelegate = new SettingsWriterDelegate(SettingsWriterHandler);
            DefaultSettingsChangeDelegate = new SettingsChangeDelegate(SettingsChangeHandler);
            FormClosing += FormClosingHandler;
            OnFileSecured += MainForm_OnFileSecured;
            OnFolderSecured += MainForm_OnFolderSecured;
            OnSecuredFileAdded += MainForm_OnSecureFilesAdded;
            OnSecureFileListRead += MainForm_OnSecureFileListRead;
            OnSecuredFolderListRead += MainForm_OnSecuredFolderListRead;
            OnSettingsFileRead += MainForm_OnSettingsFileRead;
            OnTaskException += MainForm_OnTaskException;

            TaskHandler.TaskCompletedEvent += TaskHandler_TaskCompletedEvent;

            TaskHandler.Run();                        
        }

        private void MainForm_Load(object sender, EventArgs e) => LoadApplication();

        #region Misc methods

        private void AddSecured()
        {
            using (var dialog = GetAllFilesDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var fileToSecure = dialog.FileName;
                    //secure file
                    //add it to datagridview
                    OnFileSecured?.Invoke(fileToSecure, fileToSecure);
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
                DefaultSettingsWriterDelegate.DynamicInvoke(new SharpEncryptSettings());
                SetUIOptions();
            }
            else
            {
                TaskHandler.AddJob(new ReadSettingsFileTask(settingsFilePath));
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
                    DebugMenuStrip.Enabled = true;
            }));
        }

        private void LoadSecuredFoldersList()
        {
            TaskHandler.AddJob(new ReadSecuredFoldersListTask(PathHelper.SecuredFoldersListFileName));
        }

        private void LoadRecentFilesList()
        {
            TaskHandler.AddJob(new ReadSecuredFilesListTask(PathHelper.SecuredFilesListFile));
        }

        private static void CloseApplication()
        {
            Application.Exit();
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
            var selectedRows = RecentFilesGrid.SelectedRows;
            var removals = new List<FileDataGridItemModel>();

            foreach (DataGridViewRow selectedRow in selectedRows)
            {
                if (selectedRow.Cells[2].Value is string securedFilePath)
                {
                    var files = SecuredFiles.Where(x => x.Secured == securedFilePath);
                    if (files.Any())
                    {
                        var file = files.First();
                        removals.Add(file);
                        SecuredFiles.Remove(file);
                    }
                }
            }

            TaskHandler.AddJob(new WriteSecuredFileReferenceTask(PathHelper.SecuredFilesListFile, removals, false));

            InvokeOnControl(new MethodInvoker(delegate ()
            {
                foreach (DataGridViewRow row in RecentFilesGrid.SelectedRows)
                    RecentFilesGrid.Rows.Remove(row);

                RecentFilesGrid.Refresh();
            }));         
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

        }

        private void RenameToOriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Folder context menu items

        private void RemoveFolderFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var DirectoryURIs = new List<string>();
            foreach(DataGridViewRow row in SecuredFoldersGrid.SelectedRows)
                DirectoryURIs.Add(row.Cells[0].Value as string);

            InvokeOnControl(new MethodInvoker(delegate ()
            {
                foreach (DataGridViewRow row in SecuredFoldersGrid.SelectedRows)
                    SecuredFoldersGrid.Rows.Remove(row);

                SecuredFoldersGrid.Refresh();
            }));

            TaskHandler.AddJob(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFileName, DirectoryURIs, false));
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

        }

        private void AddSecuredFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            using (var secureFolderDialog = new FolderBrowserDialog())
            {
                var result = secureFolderDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(secureFolderDialog.SelectedPath))
                {
                    TaskHandler.AddJob(new SecureFolderTask(secureFolderDialog.SelectedPath));
                }
            }
        }

        private void SecureDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExitRequested();
        }

        private void SecureFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Settings.OTPDisclaimerHide)
            {
                using (var showOnceDialog = new ShowOnceDialog(
                        ResourceManager.GetString("OTPDisclaimer"),
                        "OTPDisclaimerHide",
                        DefaultSettingsChangeDelegate))
                {
                    if (showOnceDialog.ShowDialog() != DialogResult.OK)
                    {
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
                            TaskHandler.AddJob(new OneTimePadEncryptTask(openFileDialog.FileName, keyFileOpenFileDialog.FileName));
                        }
                    }
                }
            }
        }

        private void DecryptFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO
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
                DefaultSettingsWriterDelegate.DynamicInvoke(newSettingsObj);

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
                            OTPHelper.GenerateKey(saveFileDialog.FileName, openFileDialog.FileName);
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

        private void ViewJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var columns = new[] { ResourceManager.GetString("TaskType"), ResourceManager.GetString("Completed") };
            var rows = TaskHandler.CompletedTasks.Select(x => new List<object> { x.Task.TaskType, x.Time.ToString(CultureInfo.CurrentCulture) }).ToList();
            using (var completedJobsDialog = new GenericGridForm(columns, rows))
            {
                completedJobsDialog.ShowDialog();
            }
        }

        private void LoggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DefaultSettingsChangeDelegate.DynamicInvoke("Logging", ToggleChecked(LoggingToolStripMenuItem));
        }
        #endregion

        #region Flag Menu Items
        private void DebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleEnablement(DebugMenuStrip);
            DefaultSettingsChangeDelegate.DynamicInvoke("DebugEnabled", ToggleChecked(Debug));
        }

        private void UseADifferentPasswordForEachFileToolStripMenuItem_Click(object sender, EventArgs e)
            => DefaultSettingsChangeDelegate.DynamicInvoke("UseADifferentPasswordForEachFile", ToggleChecked(UseADifferentPasswordForEachFile));

        private void IncludeToolStripMenuItem_Click(object sender, EventArgs e)
            => DefaultSettingsChangeDelegate.DynamicInvoke("IncludeSubfolders", ToggleChecked(IncludeSubfolders));

        private void WipeDiskSpaceAfterSecureDeleteToolStripMenuItem_Click(object sender, EventArgs e)
            => DefaultSettingsChangeDelegate.DynamicInvoke("WipeFreeSpaceAfterSecureDelete", ToggleChecked(WipeDiskSpaceAfterSecureDeleteToolStripMenuItem));

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

        private void TaskHandler_TaskCompletedEvent(SharpEncryptTask task)
        {
            if(task.Exception != null)
            {
                OnTaskException?.Invoke(task.Exception);
            }
            else
            {
                switch (task.TaskType)
                {
                    case TaskType.SecureFolderTask when task.Value is string folderPath:
                        OnFolderSecured?.Invoke(folderPath);
                        break;
                    case TaskType.ReadSecuredFileListTask when task.Value is IEnumerable<FileDataGridItemModel> models:
                        OnSecureFileListRead?.Invoke(models);
                        break;
                    case TaskType.ReadSecuredFoldersListTask when task.Value is IEnumerable<string> folders:
                        OnSecuredFolderListRead?.Invoke(folders);
                        break;
                    case TaskType.ReadSettingsFileTask when task.Value is SharpEncryptSettings settings:
                        OnSettingsFileRead?.Invoke(settings);
                        break;
                }
            }
        }

        private void MainForm_OnTaskException(Exception exception)
        {
            if (Settings.Logging)
                TaskHandler.AddJob(new LoggingTask(PathHelper.LoggingFilePath, exception.StackTrace));

            InvokeOnControl(new MethodInvoker(() =>
            {
                MessageBox.Show(
                    exception.StackTrace, 
                    ResourceManager.GetString("AnErrorHasOccurred"), 
                    MessageBoxButtons.OK);
            }));
        }

        private void MainForm_OnSettingsFileRead(SharpEncryptSettings settings)
        {
            Settings = settings;
            if (Settings.LanguageCode != Constants.DefaultLanguage)
                ChangeLanguage(Settings.LanguageCode);
            SetUIOptions();
        }

        private void MainForm_OnSecuredFolderListRead(IEnumerable<string> folders)
        {
            foreach(var existingFolder in folders.Where(x => Directory.Exists(x)))
                AddSecuredFolderRow(existingFolder);

            var removedFolders = folders.Where(x => !Directory.Exists(x));

            if (removedFolders.Any())
                TaskHandler.AddJob(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFileName, removedFolders, false));
        }

        private void MainForm_OnSecureFileListRead(IEnumerable<FileDataGridItemModel> models)
        {
            var existing = models.Where(x => File.Exists(x.Secured)).ToList();
            AddFilesToRecentFilesDataGrid(existing);

            var removedFiles = models.Where(x => !File.Exists(x.Secured)).ToList();
            if (removedFiles.Any())
                TaskHandler.AddJob(new WriteSecuredFileReferenceTask(PathHelper.SecuredFilesListFile, removedFiles, false));
        }

        private void MainForm_OnSecureFilesAdded(IEnumerable<FileDataGridItemModel> models)
        {
            TaskHandler.AddJob(new WriteSecuredFileReferenceTask(PathHelper.SecuredFilesListFile, models));
        }

        private void MainForm_OnFolderSecured(string folderPath)
        {
            TaskHandler.AddJob(new WriteSecuredFoldersListTask(PathHelper.SecuredFoldersListFileName, new List<string> { folderPath }));
            AddSecuredFolderRow(folderPath);
        }

        private void MainForm_OnFileSecured(string filePath, string newFilePath)
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
            OnSecuredFileAdded?.Invoke(listOfModel);
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
                    DefaultSettingsWriterDelegate.DynamicInvoke(Settings);
                }
            }
        }

        private void SettingsWriterHandler(SharpEncryptSettings settings)
            => TaskHandler.AddJob(new WriteSettingsFileTask(PathHelper.AppSettingsPath, settings));

        private void CloseApplicationHandler(object sender, FormClosedEventArgs e)
        {
            CloseApplication();
        }

        private void FormClosingHandler(object sender, FormClosingEventArgs e)
        {
            OnExitRequested();
        }

        private void OnExitRequested()
        {
            if (!TaskHandler.HasCompletedJobs)
            {
                using (var taskHandlerForm = new TaskProgressForm(TaskHandler))
                {
                    taskHandlerForm.FormClosed += CloseApplicationHandler;
                    taskHandlerForm.Show();
                }
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

        private void InvokeOnControl(Delegate method) => Invoke(method);

        private void AddSecuredFolderRow(string folderPath)
        {
            InvokeOnControl(new MethodInvoker(delegate ()
            {
                var newRow = new DataGridViewRow();
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = folderPath });
                SecuredFoldersGrid.Rows.Add(newRow);
            }));
        }

        private void AddFilesToRecentFilesDataGrid(IList<FileDataGridItemModel> models)
        {
            var rows = new DataGridViewRow[models.Count];
            var props = typeof(FileDataGridItemModel).GetProperties();

            for (var i = 0; i < models.Count; i++)
            {
                var row = new DataGridViewRow();
                foreach (var prop in props)
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = prop.GetValue(models[i]) });
                
                rows[i] = row;
            }

            SecuredFiles.AddRange(models);

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
    }
}