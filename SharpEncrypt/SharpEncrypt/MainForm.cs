using System;
using System.Windows.Forms;

namespace SharpEncrypt
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = Resources.AppName;
            SharpEncryptLabel.Text = Resources.AppName;
            Tabs.TabPages[0].Text = Resources.RecentFiles;
            Tabs.TabPages[1].Text = Resources.SecuredFolders;


            openSecuredToolTip.SetToolTip(OpenSecured, Resources.SelectSecuredFile);
            addSecuredFileToolTip.SetToolTip(AddSecured, Resources.AddSecuredFile);
            shareToolTip.SetToolTip(ShareButton, Resources.ClickToShare);
            passwordManagementToolTip.SetToolTip(PasswordManagement, Resources.GoToPasswordManagement);



        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
