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
    public partial class MainForm : Form
    {
        private string Password { get; set; }

        public MainForm() => InitializeComponent();

        private void MainForm_Load(object sender, EventArgs e) => LoadApplication();

        private void LoadApplication()
        {
            var settingsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Resources.AppName);
            if (!Directory.Exists(settingsDirectory))
                Directory.CreateDirectory(settingsDirectory);

            var settingsFilePath = Path.Combine(settingsDirectory, Resources.SharpEncryptSettingsFileName);

            var settings = new SharpEncryptSettings();
            if (!File.Exists(settingsFilePath))
            {
                new SettingsWriter().WriteSettingsFile(settingsFilePath, settings);
            }
            else
            {
                var readSettings = new SettingsReader().ReadSettingsFile(settingsFilePath);
                if (readSettings == null) //it's corrupt
                {
                    File.Delete(settingsFilePath);//delete and re-write
                    new SettingsWriter().WriteSettingsFile(settingsFilePath, settings);
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

            SetControlText(rm);
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

            SetControlText(rm);
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

        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e) => Application.Exit();

        private void OpenHomeFolder_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Resources.AppName);
            Process.Start(Directory.Exists(path) ? path : Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        }

        private void ClearRecentFilesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecentFilesGrid.Rows.Clear();
            RecentFilesGrid.Refresh();
        }

        private void RemoveFromListButKeepSecuredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in RecentFilesGrid.SelectedRows)
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

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
            => MessageBox.Show(Resources.CreatedByCredits);

        private void EnglishToolStripMenuItem_Click(object sender, EventArgs e)
            => ChangeLanguage("en-GB");

        private void DeutschGermanToolStripMenuItem_Click(object sender, EventArgs e)
            => ChangeLanguage("de-DE");

        private void NetherlandsDutchToolStripMenuItem_Click(object sender, EventArgs e)
            => ChangeLanguage("nl-NL");

        private void FrancaisFrenchToolStripMenuItem_Click(object sender, EventArgs e)
            => ChangeLanguage("fr-FR");

        private void ItalianoItalianToolStripMenuItem_Click(object sender, EventArgs e)
            => ChangeLanguage("it-IT");

        private void KoreanToolStripMenuItem_Click(object sender, EventArgs e)
            => ChangeLanguage("ko-KR");

        private void PolskiPolishToolStripMenuItem_Click(object sender, EventArgs e)
            => ChangeLanguage("pl-PL");

        private void PortugueseToolStripMenuItem_Click(object sender, EventArgs e)
            => ChangeLanguage("pt-BR");

        private void RussianToolStripMenuItem_Click(object sender, EventArgs e)
            => ChangeLanguage("ru-RU");

        private void SwedishToolStripMenuItem_Click(object sender, EventArgs e)
            => ChangeLanguage("sv-SE");

        private void TurkishToolStripMenuItem_Click(object sender, EventArgs e)
            => ChangeLanguage("tr-TR");

        private void OpenHomeFolder_Click_1(object sender, EventArgs e)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Resources.AppName);
            Process.Start(Directory.Exists(path) ? path : Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        }

        private void ChangeSessionPasswordToolStripMenuItem_Click(object sender, EventArgs e)
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
                dialog.Filter = "SharpEncrypt Encrypted File (*.seef)|*.seef";
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
                dialog.Filter = "All Files (*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var fileToSecure = dialog.FileName;
                    //secure file
                    //add it to datagridview
                }
            }            
        }

        private void UseADifferentPasswordForEachFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UseADifferentPasswordForEachFileToolStripMenuItem.Checked)
                UseADifferentPasswordForEachFileToolStripMenuItem.Checked = false;
            else
                UseADifferentPasswordForEachFileToolStripMenuItem.Checked = true;
        }
    }
}