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
        }

        private void importPublicKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (debugMenuStrip.Checked)
            {
                debugToolStripMenuItem.Checked = false;
                debugMenuStrip.Enabled = false;                
            }
            else
            {
                debugToolStripMenuItem.Checked = true;
                debugMenuStrip.Enabled = true;                
            }            
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
