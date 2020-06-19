using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt.Controls
{
    internal partial class DriveSelectionControl : UserControl
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources.Resources));
        private readonly DriveInfo[] Drives = DriveInfo.GetDrives();

        public DriveSelectionControl() => InitializeComponent();

        public IEnumerable<DriveInfo> GetSelectedDrives()
        {
            for(var i = 0; i < DriveDataGrid.Rows.Count; i++)
            {
                if(DriveDataGrid.Rows[i].Cells[0].Value is bool value && value)
                {
                    yield return Drives[i];
                }
            }
        }

        private void DriveSelectionControl_Load(object sender, EventArgs e) => InitializeListBox();

        private void InitializeListBox()
        {
            DriveDataGrid.Columns[1].HeaderText = ResourceManager.GetString("Drive");
            DriveDataGrid.Columns[2].HeaderText = ResourceManager.GetString("TotalSize");
            DriveDataGrid.Columns[3].HeaderText = ResourceManager.GetString("AvailableSize");
            DriveDataGrid.Columns[4].HeaderText = ResourceManager.GetString("DriveType");
            DriveDataGrid.Columns[5].HeaderText = ResourceManager.GetString("DriveFormat");

            foreach (var drive in Drives)
                DriveDataGrid.Rows.Add(false, drive.Name, drive.TotalSize/1024/1024/1024, drive.AvailableFreeSpace/1024/1024/1024, drive.DriveType, drive.DriveFormat);
        }
    }
}
