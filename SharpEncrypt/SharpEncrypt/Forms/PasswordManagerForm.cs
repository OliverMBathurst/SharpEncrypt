using SharpEncrypt.ExtensionClasses;
using SharpEncrypt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    public partial class PasswordManagerForm : Form
    {
        private readonly ResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly BindingList<PasswordModel> Models = new BindingList<PasswordModel>();

        public PasswordManagerForm(List<PasswordModel> models)
        {
            InitializeComponent();

            if (models == null)
                throw new ArgumentNullException(nameof(models));

            Models.AddRange(models);
            GridView.DataSource = Models;
        }

        public List<PasswordModel> PasswordModels => Models.ToList();

        private void PasswordManager_Load(object sender, EventArgs e)
        {
            OK.Text = ResourceManager.GetString("OK");
            Cancel.Text = ResourceManager.GetString("Cancel");
            Text = ResourceManager.GetString("PasswordManager");
            PasswordGenerator.Text = ResourceManager.GetString("PasswordGenerator");
        }

        private void OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void PasswordGenerator_Click(object sender, EventArgs e)
        {
            //todo
        }
    }
}
