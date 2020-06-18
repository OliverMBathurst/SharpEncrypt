using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SharpEncrypt
{
    internal partial class ImportPublicKeyForm : Form
    {
        private readonly ResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));

        public string PubKeyPath { get; private set; }

        public string PublicKeyIdentity { get; private set; }

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
                    PubKeyPath = openFileDialog.FileName;
                    PublicKeyFilePathField.Text = PubKeyPath;
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

            PublicKeyIdentity = Identity.Text;
            if(ImportPublicKey())
                DialogResult = DialogResult.OK;
        }

        private bool ImportPublicKey()
        {
            var keysDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ResourceManager.GetString("ImportedKeysDir"));
            var keyFilePath = Path.Combine(keysDir, ResourceManager.GetString("PubKeysFile"));

            IDictionary<string, RSAParameters> pubKeyList = new Dictionary<string, RSAParameters>();
            if (File.Exists(keyFilePath))
                pubKeyList = RSAKeyReader.GetPublicKeys(keyFilePath);

            if (pubKeyList.ContainsKey(PublicKeyIdentity))
            {
                MessageBox.Show(ResourceManager.GetString("DuplicateIdentity"));
                return false;
            }
            
            pubKeyList.Add(PublicKeyIdentity, RSAKeyReader.GetParameters(PubKeyPath));           

            using (var fs = new FileStream(keyFilePath, FileMode.Create))
            {
                new BinaryFormatter().Serialize(fs, pubKeyList);
            }

            return true;
        }
    }
}