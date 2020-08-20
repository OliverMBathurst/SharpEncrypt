using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SharpEncrypt.ExtensionClasses;
using SharpEncrypt.Models;

namespace SharpEncrypt.Controls
{
    internal partial class GenericGridControl : UserControl
    {
        private readonly IEnumerable<PropertyInfo> GridProperties = typeof(DataGridView).GetProperties();
        private readonly IEnumerable<ColumnModel> Columns = new List<ColumnModel>();
        private readonly IEnumerable<RowModel> Rows = new List<RowModel>();
        private readonly IEnumerable<PropertyModel> Properties = new List<PropertyModel>();

        public GenericGridControl() => InitializeComponent();

        public GenericGridControl(
            IEnumerable<ColumnModel> columns,
            IEnumerable<RowModel> rows,
            params PropertyModel[] props)
        {
            Columns = columns;
            Rows = rows;
            Properties = props;

            InitializeComponent();
        }

        private void GenericGridControl_Load(object sender, EventArgs e)
        {
            foreach (var prop in Properties)
            {
                var gridProp = GridProperties.FirstOrDefault(x => x.Name.Equals(prop.PropertyName));
                if (gridProp != null)
                {
                    gridProp.SetValue(DataGridView, prop.PropertyValue);
                }
            }
        
            foreach (var column in Columns)
                DataGridView.Columns.Add(column.ToDataGridViewColumn());
         
            foreach (var row in Rows)
                DataGridView.Rows.Add(row);

            DataGridView.Refresh();
        }

        public void AddColumns(IEnumerable<ColumnModel> columns)
            => DataGridView.Columns.AddRange(columns.Select(x => x.ToDataGridViewColumn()));

        public void AddRows(IEnumerable<RowModel> rows)
            => DataGridView.Rows.AddRange(rows.Select(x => x.ToDataGridViewRow()));

        public void RefreshGrid() => DataGridView.Refresh();

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
