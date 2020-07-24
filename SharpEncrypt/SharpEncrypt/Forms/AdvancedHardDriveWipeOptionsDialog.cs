using SharpEncrypt.Helpers;
using SharpEncrypt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    public partial class AdvancedHardDriveWipeOptionsDialog : Form
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources.Resources));
        private readonly IList<DriveWipeTaskSettingsModel> Options = DriveWipeTaskSettingsHelper.GetOptions().ToList();

        public DriveWipeTaskModel Task { get; private set; }

        public AdvancedHardDriveWipeOptionsDialog()
        {
            InitializeComponent();
        }

        private void AdvancedHardDriveWipeOptionsDialog_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("AddTask");
            AddTask.Text = ResourceManager.GetString("AddTask");
            Cancel.Text = ResourceManager.GetString("Cancel");
            WipeTypeGroupBox.Text = ResourceManager.GetString("WipeType");
            WipeOptions.Text = ResourceManager.GetString("WipeOptions");

            WipeNumber.Text = ResourceManager.GetString("WipeNumber");
            NameObfuscationRounds.Text = ResourceManager.GetString("NameObfuscationRounds");
            PropertyObfuscationRounds.Text = ResourceManager.GetString("PropertyObfuscationRounds");

            WipeTypeComboBox.DataSource = Options;
            WipeTypeComboBox.SelectedIndexChanged += WipeTypeComboBox_SelectedIndexChanged;
            OnIndexChanged(0);
        }

        private void WipeTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnIndexChanged(WipeTypeComboBox.SelectedIndex);
        }

        private void OnIndexChanged(int index)
        {
            ItemSelected(Options[index]);
        }

        private void ItemSelected(DriveWipeTaskSettingsModel settingsModel)
        {
            NameObfuscationPicker.Value = 0;
            NameObfuscationPicker.Enabled = settingsModel.NameObfuscation;

            PropertyObfuscationPicker.Value = 0;
            PropertyObfuscationPicker.Enabled = settingsModel.PropertyObfuscation;

            WipeRoundsPicker.Value = 1;
            WipeRoundsPicker.Enabled = settingsModel.WipeRounds;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            Task = new DriveWipeTaskModel
            {
                WipeType = Options[WipeTypeComboBox.SelectedIndex].Type,
                WipeRounds = (int)WipeRoundsPicker.Value,
                NameObfuscationRounds = (int)NameObfuscationPicker.Value,
                PropertyObfuscationRounds = (int)PropertyObfuscationPicker.Value,
            };
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
