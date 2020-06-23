using SharpEncrypt.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    internal partial class ImportPublicKeyForm : Form
    {
        private readonly PathHelper PathService = new PathHelper();
        private readonly ResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));

        public ImportPublicKeyForm() => InitializeComponent();

        private void ImportPublicKeyForm_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("ImportPublicKeyDialogTitle");
            Identity.Text = ResourceManager.GetString("Identity");
            PublicKeyGroupBox.Text = ResourceManager.GetString("PublicKey");
            OK.Text = ResourceManager.GetString("OK");
            Cancel.Text = ResourceManager.GetString("Cancel");
        }

        private void Cancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = ResourceManager.GetString("RSAKeyFilter");
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    PublicKeyFilePathField.Text = openFileDialog.FileName;
                }
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Identity.Text))
            {
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("FieldCannotBeEmpty"), ResourceManager.GetString("Identity")));
                return;
            }

            if (string.IsNullOrEmpty(PublicKeyFilePathField.Text))
            {
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("FieldCannotBeEmpty"), ResourceManager.GetString("PublicKey")));
                return;
            }

            if(ImportPublicKey(Identity.Text))
                DialogResult = DialogResult.OK;
        }

        private bool ImportPublicKey(string identity)
        {
            var keyFilePath = PathService.PubKeyFile;
            IDictionary<string, RSAParameters> pubKeyList = new Dictionary<string, RSAParameters>();
            if (File.Exists(keyFilePath))
            {
                pubKeyList = RSAKeyReaderHelper.GetPublicKeys(keyFilePath);

                if (pubKeyList.ContainsKey(identity))
                {
                    MessageBox.Show(ResourceManager.GetString("DuplicateIdentity"));
                    return false;
                }
            }
            
            pubKeyList.Add(identity, RSAKeyReaderHelper.GetParameters(PublicKeyFilePathField.Text));

            using (var fs = new FileStream(keyFilePath, FileMode.Create))
            {
                new BinaryFormatter().Serialize(fs, pubKeyList);
            }

            return true;
        }
    }
}