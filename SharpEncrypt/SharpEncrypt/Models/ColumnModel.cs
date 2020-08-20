using System;
using System.Windows.Forms;
using SharpEncrypt.Enums;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class ColumnModel
    {
        public DataGridViewColumnType ColumnType { get; set; } = DataGridViewColumnType.Default;

        public string ColumnName { get; set; } = string.Empty;

        public string HeaderText { get; set; } = string.Empty;

        public DataGridViewColumn ToDataGridViewColumn()
        {
            DataGridViewColumn column;
            switch (ColumnType)
            {
                case DataGridViewColumnType.Button:
                    column = new DataGridViewButtonColumn();
                    break;
                case DataGridViewColumnType.CheckBox:
                    column = new DataGridViewCheckBoxColumn();
                    break;
                case DataGridViewColumnType.ComboBox:
                    column = new DataGridViewComboBoxColumn();
                    break;
                case DataGridViewColumnType.Image:
                    column = new DataGridViewImageColumn();
                    break;
                case DataGridViewColumnType.Link:
                    column = new DataGridViewLinkColumn();
                    break;
                case DataGridViewColumnType.TextBox:
                case DataGridViewColumnType.Default:
                    column = new DataGridViewTextBoxColumn();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            column.Name = ColumnName;
            column.HeaderText = HeaderText;

            return column;
        }
    }
}