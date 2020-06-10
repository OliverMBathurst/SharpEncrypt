using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

[assembly: NeutralResourcesLanguage("en-GB")]

namespace SharpEncrypt
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();

        private void MainForm_Load(object sender, EventArgs e) => LoadApplication();

        private void LoadApplication()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var settingsDirectory = Path.Combine(documentsPath, Resources.AppName);
            if (!Directory.Exists(settingsDirectory))
                Directory.CreateDirectory(settingsDirectory);

            var settingsFilePath = Path.Combine(settingsDirectory, Resources.SharpEncryptSettingsFileName);

            var settings = new SharpEncryptSettings();
            if (!File.Exists(settingsFilePath))
            {
                new SettingsWriter().Write(settingsFilePath, settings);
            }
            else
            {
                var readSettings = new SettingsReader().Read(settingsFilePath);
                if (readSettings == null) //it's corrupt
                {
                    File.Delete(settingsFilePath);//delete and rewrite
                    new SettingsWriter().Write(settingsFilePath, settings);
                }
                else
                {
                    settings = readSettings;
                }
            }

            var rm = new ResourceManager(typeof(Resources));
            var culture = CultureInfo.CreateSpecificCulture(settings.LanguageCode);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            Text = rm.GetString("AppName");
            AppLabel.Text = rm.GetString("AppName");

            OpenSecuredToolTip.SetToolTip(OpenSecured, rm.GetString("SelectSecuredFile"));
            AddSecuredFileToolTip.SetToolTip(AddSecured, rm.GetString("AddSecuredFile"));
            ShareToolTip.SetToolTip(ShareButton, rm.GetString("ClickToShare"));
            PasswordManagementToolTip.SetToolTip(PasswordManagement, rm.GetString("GoToPasswordManagement"));
            HomeFolderToolTip.SetToolTip(OpenHomeFolder, rm.GetString("OpenHomeFolder"));
            HomeFolderToolTip.SetToolTip(OpenCloudFolder, rm.GetString("OpenHomeFolder"));
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

        private void ChangeLanguage(string lang)
        {
            var rm = new ComponentResourceManager(typeof(Resources));
            var culture = CultureInfo.CreateSpecificCulture(lang);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            foreach (var control in this.AllControls())
            {
                if (control is ToolStrip strip)
                    foreach (var item in strip.AllItems())
                        rm.ApplyResources(item, item.Name);

                rm.ApplyResources(control, control.Name);
            }

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
            HomeFolderToolTip.SetToolTip(OpenHomeFolder, rm.GetString("OpenHomeFolder"));
            HomeFolderToolTip.SetToolTip(OpenCloudFolder, rm.GetString("OpenHomeFolder"));
        }

        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e) => Application.Exit();
    }
}
