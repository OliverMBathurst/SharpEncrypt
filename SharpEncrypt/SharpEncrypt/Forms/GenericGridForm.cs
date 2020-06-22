using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    public partial class GenericGridForm : Form
    {
        private readonly IEnumerable<string> Columns;
        private readonly List<List<object>> Rows;

        public GenericGridForm(IEnumerable<string> columns, List<List<object>> rows)
        {
            Columns = columns;
            Rows = rows;
            InitializeComponent();
        }

        private void GenericGridForm_Load(object sender, EventArgs e)
        {
            foreach(var column in Columns)
                GridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = column });

            foreach(var row in Rows)
            {
                var newRow = new DataGridViewRow();
                for (var i = 0; i < row.Count; i++)
                    newRow.Cells.Add(new DataGridViewTextBoxCell { Value = row[i] });
                GridView.Rows.Add(newRow);
            }
        }
    }
}
