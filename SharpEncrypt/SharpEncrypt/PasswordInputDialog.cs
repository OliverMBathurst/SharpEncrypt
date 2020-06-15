﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SharpEncrypt
{
    public partial class PasswordInputDialog : Form
    {
        private readonly char[] SpecialChars = new[] { '<', '>', '?', '!', '£', '$', '%', '^', '&', '*', '(', ')' };
        private readonly char[] Alphabet = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private readonly char[] Numerics = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private readonly char[] RestrictedChars = new[] { '<', '>', '\'', '\\' };
        private readonly Random Random = new Random();
        private readonly bool CanUseSessionPassword;
        private readonly string SessionPassword;

        public string Password { get; private set; }

        public PasswordInputDialog(bool canUseSessionPassword = false, string sessionPassword = "")
        {
            CanUseSessionPassword = canUseSessionPassword;
            SessionPassword = sessionPassword;
            InitializeComponent();
        }

        private void PasswordInput_Load(object sender, EventArgs e)
        {
            var rm = new ComponentResourceManager(typeof(Resources));
            Text = rm.GetString("PasswordInputDialogTitle");
            PasswordGroupBox.Text = rm.GetString("Password");
            OK.Text = rm.GetString("OK");
            Cancel.Text = rm.GetString("Cancel");
            ShowPassword.Text = rm.GetString("Show");
            Copy.Text = rm.GetString("Copy");
            Generator.Text = rm.GetString("Generator");
            New.Text = rm.GetString("New");
            CopyGenerated.Text = rm.GetString("Copy");
            StrengthLabel.Text = rm.GetString("Strength");
            UseSessionPassword.Text = rm.GetString("UseSessionPassword");

            if (CanUseSessionPassword)
                UseSessionPassword.Enabled = true;

            PasswordInputBox.TextChanged += PasswordInputBox_TextChanged;
        }

        private void PasswordInputBox_TextChanged(object sender, EventArgs e)
        {            
            var value = 0;

            var regularCharCount = PasswordInputBox.Text.Where(x => Alphabet.Contains(x)).Count();
            if (regularCharCount >= 8)
                value = 40;
            else
                value = regularCharCount * 5;

            var specialCharCount = PasswordInputBox.Text.Where(x => SpecialChars.Contains(x)).Count();
            value += specialCharCount * 10;

            var numericsCount = PasswordInputBox.Text.Where(x => Numerics.Contains(x)).Count();
            value += numericsCount * 8;

            if (value > 100)
                value = 100;

            PasswordStrengthProgressBar.Value = value;
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
            if (UseSessionPassword.Enabled && UseSessionPassword.Checked)
            {
                if (string.IsNullOrEmpty(SessionPassword))
                {
                    MessageBox.Show(Resources.PasswordIsEmpty);
                }
                else
                {
                    Password = SessionPassword;
                    DialogResult = DialogResult.OK;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(PasswordInputBox.Text))
                {
                    MessageBox.Show(Resources.PasswordIsEmpty);
                }
                else
                {
                    if (!PasswordInputBox.Text.Any(x => RestrictedChars.Contains(x)))
                    {
                        Password = PasswordInputBox.Text;
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show(string.Format(Resources.PasswordRestrictedChars, string.Join(", ", RestrictedChars)));
                    }
                }
            }            
        }

        private void Cancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;

        private void Copy_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(PasswordInputBox.Text))
                Clipboard.SetText(PasswordInputBox.Text);
        }

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