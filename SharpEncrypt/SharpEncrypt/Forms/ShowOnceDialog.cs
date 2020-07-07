using System;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    internal partial class ShowOnceDialog : Form
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources.Resources));

        public bool IsChecked { get; private set; }

        public ShowOnceDialog(string text)
        {
            InitializeComponent();
            DialogTextBox.Text = text;
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
            IsChecked = NeverShowAgain.Checked;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}