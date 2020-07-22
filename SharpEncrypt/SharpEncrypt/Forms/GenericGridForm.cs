using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    public partial class GenericGridForm : Form
    {
        private readonly ResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly IEnumerable<string> Columns;
        private readonly List<List<object>> Rows;

        public GenericGridForm(IEnumerable<string> columns, List<List<object>> rows, string title = "")
        {
            InitializeComponent();
            Columns = columns;
            Rows = rows;

            Text = string.IsNullOrEmpty(title) ? ResourceManager.GetString("GridView") : title;
        }

        private void GenericGridForm_Load(object sender, EventArgs e)
        {

            foreach (var column in Columns)
                GridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = column });

            foreach (var row in Rows)
            {
                var newRow = new DataGridViewRow();
                for (var i = 0; i < row.Count; i++)
                    newRow.Cells.Add(new DataGridViewTextBoxCell { Value = row[i] });
                GridView.Rows.Add(newRow);
            }
        }
    }
}
