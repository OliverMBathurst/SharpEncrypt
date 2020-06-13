using System;
using System.Linq;
using System.Windows.Forms;

namespace SharpEncrypt
{
    public partial class PasswordInputDialog : Form
    {
        private readonly char[] SpecialChars = new[] { '<', '>', '?', '!', '£', '$', '%', '^', '&', '*', '(', ')' };
        private readonly char[] Alphabet = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private readonly char[] RestrictedChars = new[] { '<', '>', '\'', '\\' };
        private readonly Random Random = new Random();

        public string Password { get; private set; }

        public PasswordInputDialog() => InitializeComponent();

        private void PasswordInput_Load(object sender, EventArgs e)
        {
            Name = Resources.PasswordInputDialogTitle;
            PasswordGroupBox.Text = Resources.Password;
            OK.Text = Resources.OK;
            Cancel.Text = Resources.Cancel;
            ShowPassword.Text = Resources.Show;
            Copy.Text = Resources.Copy;
            Generator.Text = Resources.Generator;
            New.Text = Resources.New;
            CopyGenerated.Text = Resources.Copy;
        }

        private void ShowPassword_Click(object sender, EventArgs e)
        {
            if (PasswordInputBox.UseSystemPasswordChar)
                PasswordInputBox.UseSystemPasswordChar = false;
            else
                PasswordInputBox.UseSystemPasswordChar = true;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (ValidatePasswordBox())
            {
                Password = PasswordInputBox.Text;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(string.Format(Resources.PasswordRestrictedChars, string.Join(", ", RestrictedChars)));
            }
        }

        private void Cancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;

        private bool ValidatePasswordBox()
        {
            if (PasswordInputBox.Text.Any(x => RestrictedChars.Contains(x)))
                return false;
            return true;
        }

        private void Copy_Click(object sender, EventArgs e) => Clipboard.SetText(PasswordInputBox.Text);

        private void New_Click(object sender, EventArgs e)
        {
            var passwordString = string.Empty;
            while(passwordString.Length < 12)
            {
                if (Random.Next(2) == 1)
                    passwordString += Alphabet[Random.Next(Alphabet.Length)];
                else
                    passwordString += SpecialChars[Random.Next(SpecialChars.Length)];
            }

            PasswordGeneratorField.Text = passwordString;
        }

        private void CopyGenerated_Click(object sender, EventArgs e) => Clipboard.SetText(PasswordGeneratorField.Text);
    }
}