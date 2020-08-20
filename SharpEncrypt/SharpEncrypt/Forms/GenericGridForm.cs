using System.Collections.Generic;
using System.ComponentModel;
using System.Resources;
using System.Windows.Forms;
using SharpEncrypt.Models;

namespace SharpEncrypt.Forms
{
    internal partial class GenericGridForm : Form
    {
        private readonly ResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly IEnumerable<ColumnModel> Columns;
        private readonly IEnumerable<RowModel> Rows;

        public delegate void ExitEventHandler();
        public event ExitEventHandler ExitRequested;

        public GenericGridForm(
            IEnumerable<ColumnModel> columns,
            IEnumerable<RowModel> rows,
            string title = "")
        {
            InitializeComponent();
            Columns = columns;
            Rows = rows;

            Text = string.IsNullOrEmpty(title) ? ResourceManager.GetString("GridView") ?? string.Empty : title;
            OK.Text = ResourceManager.GetString("OK");
        }

        private void GenericGridForm_Load(object sender, System.EventArgs e)
        {
            GridControl.AddColumns(Columns);
            GridControl.AddRows(Rows);
            GridControl.RefreshGrid();
        }

        private void OK_Click(object sender, System.EventArgs e) => ExitRequested?.Invoke();
    }
}
