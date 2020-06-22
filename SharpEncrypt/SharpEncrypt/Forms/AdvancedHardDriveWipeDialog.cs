using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Resources;
using System.Windows.Forms;
using SharpEncrypt.Controls;
using SharpEncrypt.Models;

namespace SharpEncrypt.Forms
{
    internal partial class AdvancedHardDriveWipeDialog : Form
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources.Resources));
        private readonly DriveSelectionControl DriveSelectionControl = new DriveSelectionControl { Dock = DockStyle.Fill };
        private readonly IDictionary<DriveInfo, IList<DriveWipeJob>> Jobs = new Dictionary<DriveInfo, IList<DriveWipeJob>>();

        public AdvancedHardDriveWipeDialog() => InitializeComponent();

        private void AdvancedHardDriveWipeDialog_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("HDDWipeDialog");
            OK.Text = ResourceManager.GetString("OK");
            Cancel.Text = ResourceManager.GetString("Cancel");
            Options.Text = ResourceManager.GetString("Options");
            ClearJobs.Text = ResourceManager.GetString("ClearJobs");
            ViewJobs.Text = ResourceManager.GetString("ViewJobs");
            ControlsPanel.Controls.Add(DriveSelectionControl);            
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Options_Click(object sender, EventArgs e)
        {
            var drives = DriveSelectionControl.GetSelectedDrives();
            if (!drives.Any())
            {
                MessageBox.Show("");
            }                
            else
            {
                using (var driveWipeOptionsDialog = new AdvancedHardDriveWipeOptionsDialog())
                {
                    if(driveWipeOptionsDialog.ShowDialog() == DialogResult.OK)
                    {
                        var job = driveWipeOptionsDialog.Job;
                        foreach (var drive in drives)
                        {
                            if (Jobs.ContainsKey(drive))
                                Jobs[drive].Add(job);
                            else
                                Jobs.Add(drive, new List<DriveWipeJob> { job });
                        }
                        MessageBox.Show("Added N jobs etc.");
                    }
                }
            }
        }

        private void ClearJobs_Click(object sender, EventArgs e)
        {
            Jobs.Clear();
        }

        private void ViewJobs_Click(object sender, EventArgs e)
        {
            var columns = new List<string> { ResourceManager.GetString("Drive") };

            var props = typeof(DriveWipeJob).GetProperties();
            foreach (var prop in props)
                columns.Add(ResourceManager.GetString(prop.Name));

            var rows = new List<List<object>>();
            foreach(var job in Jobs)
            {
                foreach (var value in job.Value)
                {
                    var list = new List<object> {
                        job.Key.Name
                    };

                    foreach(var prop in props)
                        list.Add(prop.GetValue(value));

                    rows.Add(list);
                }
            }

            using (var viewJobsDialog = new GenericGridForm(columns, rows))
            {
                viewJobsDialog.ShowDialog();
            }
        }
    }
}
