using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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

        public IDictionary<DriveInfo, IList<DriveWipeJob>> Jobs { get; } = new Dictionary<DriveInfo, IList<DriveWipeJob>>();

        public AdvancedHardDriveWipeDialog() => InitializeComponent();

        private void AdvancedHardDriveWipeDialog_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("HDDWipeDialog");
            OK.Text = ResourceManager.GetString("OK");
            Cancel.Text = ResourceManager.GetString("Cancel");
            AddJob.Text = ResourceManager.GetString("AddJob");
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
            var drives = DriveSelectionControl.GetSelectedDrives().ToList();
            if (!drives.Any())
            {
                MessageBox.Show(ResourceManager.GetString("NoDrivesSelected"));
            }                
            else
            {
                using (var driveWipeOptionsDialog = new AdvancedHardDriveWipeOptionsDialog())
                {
                    if (driveWipeOptionsDialog.ShowDialog() != DialogResult.OK) return;
                    var job = driveWipeOptionsDialog.Job;
                    foreach (var drive in drives)
                    {
                        if (Jobs.ContainsKey(drive))
                            Jobs[drive].Add(job);
                        else
                            Jobs.Add(drive, new List<DriveWipeJob> { job });
                    }
                    MessageBox.Show(string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("AddedNJobs") ?? string.Empty, drives.Count));
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
            columns.AddRange(props.Select(prop => ResourceManager.GetString(prop.Name)));

            var rows = new List<List<object>>();
            foreach (var kvp in Jobs)
            {
                foreach (var value in kvp.Value)
                {
                    var row = new List<object> {
                        kvp.Key.Name
                    };
                    row.AddRange(props.Select(prop => prop.GetValue(value)));

                    rows.Add(row);
                }
            }

            using (var viewJobsDialog = new GenericGridForm(columns, rows, ResourceManager.GetString("Jobs")))
            {
                viewJobsDialog.ShowDialog();
            }
        }
    }
}