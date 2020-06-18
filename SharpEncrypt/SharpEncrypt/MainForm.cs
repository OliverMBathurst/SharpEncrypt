using SharpEncrypt.Exceptions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

[assembly: NeutralResourcesLanguage("en-GB")]

namespace SharpEncrypt
{
    internal partial class MainForm : Form
    {
        private readonly ComponentResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources));
        private readonly PathService PathService = new PathService();
        private delegate void SettingsChangeDelegate(string settingsPropertyName, object value);
        private delegate void SettingsWriterDelegate(SharpEncryptSettings settings, bool synchronous);

        private readonly SettingsWriterDelegate DefaultSettingsWriterDelegate;
        private readonly SettingsChangeDelegate DefaultSettingsChangeDelegate;

        private string Password { get; set; }

        private SharpEncryptSettings Settings { get; set; }

        public MainForm() 
        {
            InitializeComponent();
            DefaultSettingsWriterDelegate = new SettingsWriterDelegate(SettingsWriterHandler);
            DefaultSettingsChangeDelegate = new SettingsChangeDelegate(SettingsChangeHandler);
        }

        private void MainForm_Load(object sender, EventArgs e) => LoadApplication();

        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e) => Application.Exit();

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e) => MessageBox.Show(Resources.CreatedByCredits);

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

        private void OpenHomeFolder_Click(object sender, EventArgs e) => Process.Start(PathService.AppDirectory);

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

        private void RemoveFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in SecuredFoldersGrid.SelectedRows)
                SecuredFoldersGrid.Rows.Remove(row);

            SecuredFoldersGrid.Refresh();
        }

        private void LoadApplication()
        {
            SetApplicationSettings();
            if(Settings.LanguageCode != Constants.DefaultLanguage)
                ChangeLanguage(Settings.LanguageCode);

            SetSessionPassword();
        }

        private void ReloadApplication(bool changeLanguage)
        {
            if (changeLanguage)
                ChangeLanguage(Settings.LanguageCode);
        }

        private void ChangeSessionPasswordToolStripMenuItem_Click(object sender, EventArgs e)
            => SetSessionPassword();

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

        private void OpenSecured_Click(object sender, EventArgs e)
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

        private void AddSecured_Click(object sender, EventArgs e)
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

        #region Flag Menu Items

        private void UseADifferentPasswordForEachFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UseADifferentPasswordForEachFileToolStripMenuItem.Checked)
                UseADifferentPasswordForEachFileToolStripMenuItem.Checked = false;
            else
                UseADifferentPasswordForEachFileToolStripMenuItem.Checked = true;
        }

        private void DebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DebugMenuStrip.Checked)
            {
                DebugToolStripMenuItem.Checked = false;
                DebugMenuStrip.Enabled = false;
            }
            else
            {
                DebugToolStripMenuItem.Checked = true;
                DebugMenuStrip.Enabled = true;
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

            OpenSecuredToolTip.SetToolTip(OpenSecured, rm.GetString("SelectSecuredFile"));
            AddSecuredFileToolTip.SetToolTip(AddSecured, rm.GetString("AddSecuredFile"));
            ShareToolTip.SetToolTip(ShareButton, rm.GetString("ClickToShare"));
            PasswordManagementToolTip.SetToolTip(PasswordManagement, rm.GetString("GoToPasswordManagement"));
            OpenHomeFolderToolTip.SetToolTip(OpenHomeFolder, rm.GetString("OpenHomeFolder"));
        }

        #endregion

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
                var readSettings = SettingsReader.ReadSettingsFile(settingsFilePath, true);
                if (readSettings == null) //it's corrupt
                {
                    DefaultSettingsWriterDelegate.DynamicInvoke(settings, false);
                }
                else
                {
                    settings = readSettings.Result;
                }
            }

            Settings = settings;
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

        private void ImportPublicKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var importPubKeyForm = new ImportPublicKeyForm())
            {
                importPubKeyForm.Show();
            }                
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
                    if (showOnceDialog.ShowDialog()
                        != DialogResult.OK)
                    {
                        return;
                    }
                }                    
            }

            //rest of logic
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
                    DefaultSettingsWriterDelegate.DynamicInvoke(Settings, false);
                }                
            }
        }

        private void SettingsWriterHandler(SharpEncryptSettings settings, bool synchronous)
            => SettingsWriter.WriteSettingsFile(PathService.AppSettingsPath, settings, synchronous);

        private void ResetAllSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show(
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

        private void WipeFreeDiskSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var hardDriveWipeDialog = new HardDriveWipeDialog())
            {
                hardDriveWipeDialog.Show();
            }
        }
    }
}