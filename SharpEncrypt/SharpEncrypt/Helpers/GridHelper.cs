using System.Collections.Generic;
using System.Linq;
using System.Resources;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Helpers
{
    internal static class GridHelper
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
    }
}
