using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharpEncrypt.Enums;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class RowModel
    {
        public IEnumerable<CellModel> Cells { get; set; }

        public DataGridViewRow ToDataGridViewRow()
        {
            var row = new DataGridViewRow();
            foreach (var cell in Cells)
            {
                DataGridViewCell newCell;

                switch (cell.CellType)
                {
                    case CellType.TextBox:
                        newCell = new DataGridViewTextBoxCell();
                        break;
                    default:
                        newCell = new DataGridViewTextBoxCell();
                        break;
                }

                newCell.Value = cell.Value;

                row.Cells.Add(newCell);
            }
            return row;
        }
    }
}
