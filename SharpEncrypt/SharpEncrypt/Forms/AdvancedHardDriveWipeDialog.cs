using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using SharpEncrypt.Controls;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;

namespace SharpEncrypt.Forms
{
    internal partial class AdvancedHardDriveWipeDialog : Form
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources.Resources));
        private readonly DriveSelectionControl DriveSelectionControl = new DriveSelectionControl { Dock = DockStyle.Fill };

        public IDictionary<DriveInfo, IList<DriveWipeTaskModel>> Tasks { get; } = new Dictionary<DriveInfo, IList<DriveWipeTaskModel>>();

        public AdvancedHardDriveWipeDialog() => InitializeComponent();

        private void AdvancedHardDriveWipeDialog_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("HDDWipeDialog");
            OK.Text = ResourceManager.GetString("OK");
            Cancel.Text = ResourceManager.GetString("Cancel");
            AddTask.Text = ResourceManager.GetString("AddTask");
            ClearTasks.Text = ResourceManager.GetString("ClearTasks");
            ViewTasks.Text = ResourceManager.GetString("ViewTasks");
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
                    var task = driveWipeOptionsDialog.Task;
                    foreach (var drive in drives)
                    {
                        if (Tasks.ContainsKey(drive))
                            Tasks[drive].Add(task);
                        else
                            Tasks.Add(drive, new List<DriveWipeTaskModel> { task });
                    }
                    MessageBox.Show(string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("AddedNTasks") ?? string.Empty, drives.Count));
                }
            }
        }

        private void ClearTasks_Click(object sender, EventArgs e) => Tasks.Clear();

        private void ViewTasks_Click(object sender, EventArgs e)
        {
            var props = typeof(DriveWipeTaskModel).GetProperties();
            var rows = new List<RowModel>();
            foreach (var kvp in Tasks)
            {
                foreach (var driveWipeTaskModel in kvp.Value)
                {
                    var cells = new List<CellModel> 
                    {
                        new CellModel
                        {
                            Value = kvp.Key.Name
                        }
                    };

                    cells.AddRange(props.Select(prop => new CellModel { Value = prop.GetValue(driveWipeTaskModel) }));
                    rows.Add(new RowModel { Cells = cells });
                }
            }

            using (var viewTasksDialog = new GenericGridForm(
                GridHelper.GetDriveWipeColumnDefinitions(ResourceManager), 
                rows, 
                ResourceManager.GetString("Tasks")))
            {
                viewTasksDialog.ShowDialog();
            }
        }
    }
}