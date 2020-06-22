using SharpEncrypt.Enums;
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
        private readonly IList<DriveWipeJobSettings> Options = DriveWipeJobSettingsHelper.GetOptions().ToList();

        public DriveWipeJob Job { get; private set; }

        public AdvancedHardDriveWipeOptionsDialog()
        {
            InitializeComponent();
        }

        private void AdvancedHardDriveWipeOptionsDialog_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("AddJob");
            AddJob.Text = ResourceManager.GetString("AddJob");
            Cancel.Text = ResourceManager.GetString("Cancel");
            WipeTypeGroupBox.Text = ResourceManager.GetString("WipeType");
            WipeOptions.Text = ResourceManager.GetString("WipeOptions");

            WipeNumber.Text = ResourceManager.GetString("WipeNumber");
            NameObfuscationRounds.Text = ResourceManager.GetString("NameObfuscationRounds");
            PropertyObfuscationRounds.Text = ResourceManager.GetString("PropertyObfuscationRounds");

            WipeTypeComboBox.DataSource = Options;
            WipeTypeComboBox.SelectedIndexChanged += WipeTypeComboBox_SelectedIndexChanged;
        }

        private void WipeTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemSelected(Options[WipeTypeComboBox.SelectedIndex]);
        }

        private void ItemSelected(DriveWipeJobSettings settings)
        {
            NameObfuscationPicker.Value = 0;
            NameObfuscationPicker.Enabled = settings.NameObfuscation;

            PropertyObfuscationPicker.Value = 0;
            PropertyObfuscationPicker.Enabled = settings.PropertyObfuscation;

            WipeRoundsPicker.Value = 1;
            WipeRoundsPicker.Enabled = settings.WipeRounds;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            Job = new DriveWipeJob
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
