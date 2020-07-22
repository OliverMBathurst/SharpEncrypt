using SharpEncrypt.Helpers;
using System;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    internal partial class PasswordInputDialog : Form
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources.Resources));
        private readonly PasswordHelper PasswordHelper = new PasswordHelper();
        private readonly bool CanUseSessionPassword, CanHaveRestrictedChars, IsDisplayOnly;
        private readonly string SessionPassword;

        public string Password { get; private set; }

        public PasswordInputDialog(bool canUseSessionPassword = false, bool canHaveRestrictedChars = false, bool isDisplayOnly = false, string sessionPassword = "")
        {
            CanUseSessionPassword = canUseSessionPassword;
            CanHaveRestrictedChars = canHaveRestrictedChars;
            IsDisplayOnly = isDisplayOnly;
            SessionPassword = sessionPassword;
            InitializeComponent();
        }

        private void PasswordInput_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("PasswordInputDialogTitle");
            PasswordGroupBox.Text = ResourceManager.GetString("Password");
            OK.Text = ResourceManager.GetString("OK");
            Cancel.Text = ResourceManager.GetString("Cancel");
            ShowPassword.Text = ResourceManager.GetString("Show");
            Copy.Text = ResourceManager.GetString("Copy");
            Generator.Text = ResourceManager.GetString("Generator");
            New.Text = ResourceManager.GetString("New");
            CopyGenerated.Text = ResourceManager.GetString("Copy");
            StrengthLabel.Text = ResourceManager.GetString("Strength");
            UseSessionPassword.Text = ResourceManager.GetString("UseSessionPassword");

            if (CanUseSessionPassword)
                UseSessionPassword.Enabled = true;

            PasswordInputBox.TextChanged += PasswordInputBox_TextChanged;
        }

        private void PasswordInputBox_TextChanged(object sender, EventArgs e)
        {
            PasswordStrengthProgressBar.Value = PasswordHelper.GetPasswordStrength(PasswordInputBox.Text);
        }

        private void ShowPassword_Click(object sender, EventArgs e)
        {
            PasswordInputBox.UseSystemPasswordChar = !PasswordInputBox.UseSystemPasswordChar;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (IsDisplayOnly)
            {
                Password = PasswordInputBox.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                if (UseSessionPassword.Enabled && UseSessionPassword.Checked)
                {
                    if (string.IsNullOrEmpty(SessionPassword))
                    {
                        MessageBox.Show(ResourceManager.GetString("PasswordIsEmpty"));
                    }
                    else
                    {
                        Password = SessionPassword;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(PasswordInputBox.Text))
                    {
                        MessageBox.Show(ResourceManager.GetString("PasswordIsEmpty"));
                    }
                    else
                    {
                        if (CanHaveRestrictedChars || PasswordHelper.IsValid(PasswordInputBox.Text))
                        {
                            Password = PasswordInputBox.Text;
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show(string.Format(CultureInfo.CurrentCulture,
                                ResourceManager.GetString("PasswordRestrictedChars") ?? string.Empty,
                                string.Join(", ", PasswordHelper.GetRestrictedChars())));
                        }
                    }
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e) 
        {
            DialogResult = DialogResult.Cancel;
            Close();
        } 

        private void Copy_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(PasswordInputBox.Text))
                Clipboard.SetText(PasswordInputBox.Text);
        }

        private void New_Click(object sender, EventArgs e)
        {
            PasswordGeneratorField.Text = PasswordHelper.GeneratePassword();
        }

        private void CopyGenerated_Click(object sender, EventArgs e) { 
            if(!string.IsNullOrEmpty(PasswordGeneratorField.Text))
                Clipboard.SetText(PasswordGeneratorField.Text);
        } 
    }
}