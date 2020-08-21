using System;
using System.Windows.Forms;
using SharpEncrypt.Enums;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class CellModel
    {
        public CellType CellType { get; set; } = CellType.TextBox;

        public object InitialValue { get; set; }

        public bool ReadOnly { get; set; } = false;

        public DataGridViewCell ToDataGridViewCell()
        {
            DataGridViewCell cell;
            switch (CellType)
            {
                case CellType.TextBox:
                    cell = new DataGridViewTextBoxCell();
                    break;
                case CellType.CheckBox:
                    cell = new DataGridViewCheckBoxCell();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            cell.Value = InitialValue;
            cell.ReadOnly = ReadOnly;
            return cell;
        }
    }
}
