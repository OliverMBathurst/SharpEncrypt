using System;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt
{
    internal partial class ShowOnceDialog : Form
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources.Resources));
        private readonly string SettingsName;
        private readonly Delegate SettingChangedDelegate;

        public ShowOnceDialog(
            string text, 
            string settingsName, 
            Delegate settingChangeDelegate)
        {
            InitializeComponent();
            DialogTextBox.Text = text;
            SettingsName = settingsName;
            SettingChangedDelegate = settingChangeDelegate;
        }

        private void ShowOnceDialog_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("Dialog");
            OK.Text = ResourceManager.GetString("OK");
            Cancel.Text = ResourceManager.GetString("Cancel");
            NeverShowAgain.Text = ResourceManager.GetString("NeverShowAgain");
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (NeverShowAgain.Checked)
                SettingChangedDelegate.DynamicInvoke(SettingsName, true);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}