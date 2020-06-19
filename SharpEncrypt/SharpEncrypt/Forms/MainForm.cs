using SharpEncrypt.Exceptions;
using SharpEncrypt.ExtensionClasses;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    internal partial class MainForm : Form
    {
        private readonly ComponentResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        
        private delegate void SettingsChangeDelegate(string settingsPropertyName, object value);
        private delegate void SettingsWriterDelegate(SharpEncryptSettings settings, bool synchronous);

        private readonly SettingsWriterDelegate DefaultSettingsWriterDelegate;
        private readonly SettingsChangeDelegate DefaultSettingsChangeDelegate;
        private readonly BackgroundTaskHandler TaskHandler = new BackgroundTaskHandler();
        private readonly PathService PathService = new PathService();

        private string Password { get; set; }

        private SharpEncryptSettings Settings { get; set; }

        public MainForm() 
        {
            InitializeComponent();
            DefaultSettingsWriterDelegate = new SettingsWriterDelegate(SettingsWriterHandler);
            DefaultSettingsChangeDelegate = new SettingsChangeDelegate(SettingsChangeHandler);
        }

        private void MainForm_Load(object sender, EventArgs e) => LoadApplication();

        #region Misc methods

        private void AddSecured()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = ResourceManager.GetString("AllFilesFilter");
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var fileToSecure = dialog.FileName;
                    //secure file
                    //add it to datagridview
                }
            }
        }

        private void OpenSecured()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = ResourceManager.GetString("EncryptedFileFilter");
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var securedFilePath = dialog.FileName;
                    //open secured file
                }
            }
        }

        private void LoadApplication()
        {
            TaskHandler.Run();
            SetApplicationSettings();
            if (Settings.LanguageCode != Constants.DefaultLanguage)
                ChangeLanguage(Settings.LanguageCode);

            SetSessionPassword();            
        }

        private void ReloadApplication(bool changeLanguage)
        {
            if (changeLanguage)
                ChangeLanguage(Settings.LanguageCode);
        }

        private void SetApplicationSettings()
        {
            var settingsFilePath = PathService.AppSettingsPath;
            var settings = new SharpEncryptSettings();

            if (!File.Exists(settingsFilePath))
            {
                DefaultSettingsWriterDelegate.DynamicInvoke(settings, false);
            }
            else
            {
                var task = SettingsReader.ReadSettingsFileTask(settingsFilePath);
                TaskHandler.AddJob(task, true);
                if (task.Result == null) //it's corrupt
                {
                    DefaultSettingsWriterDelegate.DynamicInvoke(settings, false);
                }
                else
                {
                    settings = task.Result;
                }
            }

            Settings = settings;
        }

        #endregion

        #region File context menu items

        private void ClearRecentFilesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecentFilesGrid.Rows.Clear();
            RecentFilesGrid.Refresh();
        }

        private void RemoveFromListButKeepSecuredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in RecentFilesGrid.SelectedRows)
                RecentFilesGrid.Rows.Remove(row);

            RecentFilesGrid.Refresh();
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

        private void RemoveFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in SecuredFoldersGrid.SelectedRows)
                SecuredFoldersGrid.Rows.Remove(row);

            SecuredFoldersGrid.Refresh();
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

        #region Mouse events

        private void RecentFilesGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RecentFilesGrid.CurrentCell = RecentFilesGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                RecentFilesGrid.Rows[e.RowIndex].Selected = true;
                RecentFilesGrid.Focus();
            }
        }

        private void SecuredFoldersGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                SecuredFoldersGrid.CurrentCell = SecuredFoldersGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                SecuredFoldersGrid.Rows[e.RowIndex].Selected = true;
                SecuredFoldersGrid.Focus();
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

        private void OpenHomeFolder_Click(object sender, EventArgs e) => Process.Start(PathService.AppDirectory);

        #endregion

        #region Disk tools menu items

        private void WipeFreeDiskSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var hardDriveWipeDialog = new HardDriveWipeDialog())
            {
                hardDriveWipeDialog.Show();
            }
        }

        private void AdvancedDiskWipeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Debug menu items

        private void ValidateContainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(var openFileDialog = new OpenFileDialog())
            {
                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(ContainerHelper.ValidateContainer(openFileDialog.FileName) 
                        ? string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("ValidContainer"), openFileDialog.FileName)
                        : string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("NotAValidContainer"), openFileDialog.FileName));
                }
            }
        }

        #endregion

        #region Flag Menu Items
        private void DebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Toggle(DebugMenuStrip);
            DefaultSettingsChangeDelegate.DynamicInvoke("DebugEnabled", Toggle(Debug));
        }

        private void UseADifferentPasswordForEachFileToolStripMenuItem_Click(object sender, EventArgs e)
            => DefaultSettingsChangeDelegate.DynamicInvoke("UseADifferentPasswordForEachFile", Toggle(UseADifferentPasswordForEachFile));

        private void IncludeToolStripMenuItem_Click(object sender, EventArgs e)
            => DefaultSettingsChangeDelegate.DynamicInvoke("IncludeSubfolders", Toggle(IncludeSubfolders));

        private void WipeDiskSpaceAfterSecureDeleteToolStripMenuItem_Click(object sender, EventArgs e)
            => DefaultSettingsChangeDelegate.DynamicInvoke("WipeFreeSpaceAfterSecureDelete", Toggle(WipeDiskSpaceAfterSecureDeleteToolStripMenuItem));

        private static bool Toggle(ToolStripMenuItem item)
        {
            if (item.Checked)
                item.Checked = false;
            else
                item.Checked = true;

            return item.Checked;
        }

        #endregion

        #region Handlers

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
                    DefaultSettingsWriterDelegate.DynamicInvoke(Settings, false);
                }
            }
        }

        private void SettingsWriterHandler(SharpEncryptSettings settings, bool synchronous)
            => TaskHandler.AddJob(SettingsWriter.WriteSettingsFileTask(PathService.AppSettingsPath, settings), synchronous);

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

        }

        private void SecureDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void SecureFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Settings.OTPDisclaimerHide)
            {
                using (var showOnceDialog = new ShowOnceDialog(
                        ResourceManager.GetString("OTPDisclaimer"),
                        "OTPDisclaimerHide",
                        DefaultSettingsChangeDelegate))
                {
                    if (showOnceDialog.ShowDialog()
                        != DialogResult.OK)
                    {
                        return;
                    }
                }
            }

            //rest of logic
        }

        private void DecryptFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                DefaultSettingsWriterDelegate.DynamicInvoke(newSettingsObj, false);

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
            var (@public, _) = PathService.KeyPairPaths;

            if (!File.Exists(@public))
            {
                MessageBox.Show(ResourceManager.GetString("PublicKeyDoesNotExist"));
            }
            else
            {
                try
                {
                    var pubKey = RSAKeyReader.GetParameters(@public);
                    using (var dialog = new SaveFileDialog())
                    {
                        dialog.Filter = ResourceManager.GetString("RSAKeyFilter");
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            RSAKeyWriter.Write(dialog.FileName, pubKey);
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
            var (@public, @private) = PathService.KeyPairPaths;

            if (MessageBox.Show(
                ResourceManager.GetString("KeyPairDisclaimer"),
                string.Empty,
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                var (publicKey, privateKey) = RSAHelper.GetNewKeyPair();
                RSAKeyWriter.Write(@public, publicKey);
                RSAKeyWriter.Write(@private, privateKey);
                //encrypt the rsa priv key (IMPORTANT)
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