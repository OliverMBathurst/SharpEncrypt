using SharpEncrypt.Models;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    internal partial class WaitingForDriveForm : Form
    {
        private readonly ResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly KeyFileStoreFileTupleModel _model;

        public WaitingForDriveForm(KeyFileStoreFileTupleModel model)
        {
            InitializeComponent();
            _model = model;
        }

        private void WaitingForDriveForm_Load(object sender, EventArgs e)
        {
            var waitingFor = string.Format(CultureInfo.InvariantCulture, ResourceManager.GetString("WaitingForDrive"), $"{_model.KeyFile[0]}\\");
            Text = waitingFor;
            Label.Text = waitingFor;
            OK.Text = ResourceManager.GetString("OK");
            Retry.Text = ResourceManager.GetString("Retry");
        }

        private void OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void Retry_Click(object sender, EventArgs e)
        {
            if(DriveInfo.GetDrives().Any(x => x.Name[0].Equals(_model.KeyFile[0])))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}