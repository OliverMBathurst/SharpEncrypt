using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt
{
    internal partial class HardDriveWipeDialog : Form
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources));
        private readonly DriveSelectionControl DriveSelectionControl = new DriveSelectionControl { Dock = DockStyle.Fill };

        public HardDriveWipeDialog() => InitializeComponent();

        public IEnumerable<DriveInfo> SelectedDrives { get; private set; }

        private void HardDriveWipeDialog_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("HDDWipeDialog");
            OK.Text = ResourceManager.GetString("OK");
            Cancel.Text = ResourceManager.GetString("Cancel");
            ControlsPanel.Controls.Add(DriveSelectionControl);
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            SelectedDrives = DriveSelectionControl.GetSelectedDrives();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
