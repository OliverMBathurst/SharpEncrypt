using System.Collections.Generic;
using System.Linq;
using System.Resources;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Helpers
{
    public static class GridHelper
    {
        public static IEnumerable<ColumnModel> GetPublicKeyGridColumnDefinitions(ResourceManager resourceManager)
            => new []
            {
                new ColumnModel
                {
                    ColumnType = DataGridViewColumnType.TextBox,
                    ColumnName = resourceManager.GetString("Identity"),
                    HeaderText = resourceManager.GetString("Identity")
                }
            };

        public static IEnumerable<ColumnModel> GetCompletedJobsColumnDefinitions(ResourceManager resourceManager)
            => new[]
            {
                new ColumnModel
                {
                    ColumnType = DataGridViewColumnType.TextBox,
                    ColumnName = resourceManager.GetString("TaskType"),
                    HeaderText = resourceManager.GetString("TaskType")
                },
                new ColumnModel
                {
                    ColumnType = DataGridViewColumnType.TextBox,
                    ColumnName = resourceManager.GetString("Completed"),
                    HeaderText = resourceManager.GetString("Completed")
                }
            };

        public static IEnumerable<ColumnModel> GetDriveWipeColumnDefinitions(ResourceManager resourceManager)
        {
            var columns = new List<ColumnModel>
            {
                new ColumnModel
                {
                    ColumnType = DataGridViewColumnType.TextBox,
                    ColumnName = resourceManager.GetString("Drive"),
                    HeaderText = resourceManager.GetString("Drive")
                }
            };

            columns.AddRange(typeof(DriveWipeTaskModel).GetProperties()
                .Select(x => resourceManager.GetString(x.Name))
                .Select(x => new ColumnModel
                {
                    ColumnName = x, 
                    HeaderText = x, 
                    ColumnType = DataGridViewColumnType.TextBox
                }));

            return columns;
        }

        public static IEnumerable<ColumnModel> GetDriveSelectionColumnDefinitions(ResourceManager resourceManager)
        {
            return new[]
            {
                new ColumnModel
                {
                    ColumnType = DataGridViewColumnType.CheckBox,
                    OverrideSelectedRows = true,
                    ColumnName = "DriveCheckBoxColumn"
                },
                new ColumnModel
                {
                    ColumnName = resourceManager.GetString("Drive"),
                    HeaderText = resourceManager.GetString("Drive")
                },
                new ColumnModel
                {
                    ColumnName = resourceManager.GetString("TotalSize"),
                    HeaderText = resourceManager.GetString("TotalSize")
                },
                new ColumnModel
                {
                    ColumnName = resourceManager.GetString("AvailableSize"),
                    HeaderText = resourceManager.GetString("AvailableSize")
                },
                new ColumnModel
                {
                    ColumnName = resourceManager.GetString("DriveType"),
                    HeaderText = resourceManager.GetString("DriveType")
                },
                new ColumnModel
                {
                    ColumnName = resourceManager.GetString("DriveFormat"),
                    HeaderText = resourceManager.GetString("DriveFormat")
                }
            };
        }
    }
}
