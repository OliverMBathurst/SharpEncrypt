using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class RowModel
    {
        public object DataSource { get; set; }

        public List<CellModel> Cells { get; set; }

        public bool ReadOnly { get; set; } = false;

        public DataGridViewRow ToDataGridViewRow()
        {
            var row = new DataGridViewRow();
            foreach (var cell in Cells)
            {
                row.Cells.Add(cell.ToDataGridViewCell());
            }
            row.ReadOnly = ReadOnly;
            return row;
        }
    }
}
