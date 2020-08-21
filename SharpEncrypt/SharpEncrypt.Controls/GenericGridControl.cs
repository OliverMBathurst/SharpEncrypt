using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SharpEncrypt.ExtensionClasses;
using SharpEncrypt.Models;

namespace SharpEncrypt.Controls
{
    public partial class GenericGridControl : UserControl
    {
        private readonly List<PropertyInfo> GridProperties = typeof(DataGridView).GetProperties().ToList();
        private readonly List<ColumnModel> Columns = new List<ColumnModel>();
        private readonly List<RowModel> Rows = new List<RowModel>();

        public GenericGridControl() => InitializeComponent();

        public void AddColumns(IEnumerable<ColumnModel> columns) => AddColumnsInternal(columns);

        public void AddRows(IEnumerable<RowModel> rows) => AddRowsInternal(rows);

        public void SetProperties(IEnumerable<PropertyModel> props)
        {
            foreach (var prop in props)
            {
                var gridProp = GridProperties.FirstOrDefault(x => x.Name.Equals(prop.PropertyName));
                if (gridProp != null)
                {
                    gridProp.SetValue(DataGridView, prop.PropertyValue);
                }
            }
        }

        private void AddColumnsInternal(IEnumerable<ColumnModel> columns)
        {
            Columns.AddRange(columns);
            DataGridView.Columns.AddRange(columns.Select(x => x.ToDataGridViewColumn()));
        }

        private void AddRowsInternal(IEnumerable<RowModel> rows)
        {
            Rows.AddRange(rows);
            DataGridView.Rows.AddRange(rows.Select(x => x.ToDataGridViewRow()));
        }

        public void RefreshGrid() => DataGridView.Refresh();

        public IEnumerable<RowModel> SelectedRows
        {
            get
            {
                if (!Columns.Any(x => x.OverrideSelectedRows))
                    return from DataGridViewRow row in DataGridView.SelectedRows select Rows[row.Index];

                var index = Columns.FindIndex(x => x.OverrideSelectedRows);

                var selected = new List<RowModel>();
                foreach (DataGridViewRow row in DataGridView.Rows)
                {
                    if (row.Cells[index].Value is bool booleanValue && booleanValue)
                    {
                        selected.Add(Rows[row.Index]);
                    }
                }

                return selected;
            }
        }
    }
}
