using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    public partial class IndividualSettingsResetDialog<T> : Form
    {
        private readonly ComponentResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly PropertyInfo[] Properties;
        private readonly object[] CurrentPropertyValues, DefaultPropertyValues;

        public T Result { get; private set; }

        public IndividualSettingsResetDialog(T currentObject, T defaultObject)
        {
            InitializeComponent();
            Properties = typeof(T).GetProperties();
            CurrentPropertyValues = Properties.Select(x => x.GetValue(currentObject)).ToArray();
            DefaultPropertyValues = Properties.Select(x => x.GetValue(defaultObject)).ToArray();            

            PropertyValuesGrid.CellClick += CellClick;
        }

        private void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == PropertyValuesGrid.Columns.Count - 1)
            {
                Reset(e.RowIndex);
                RefreshGrid();
                PropertyValuesGrid.Rows[e.RowIndex].Selected = true;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            SetResultObject();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ResetAll_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < PropertyValuesGrid.Rows.Count; i++)
            {
                Reset(i);
            }
            RefreshGrid();
        }

        private void IndividualSettingsResetDialog_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("IndividualSettingsResetDialog");
            OK.Text = ResourceManager.GetString("OK");
            Cancel.Text = ResourceManager.GetString("Cancel");
            ResetAll.Text = ResourceManager.GetString("ResetAll");
            PropertyValuesGrid.Columns[0].HeaderText = ResourceManager.GetString("Property");
            PropertyValuesGrid.Columns[1].HeaderText = ResourceManager.GetString("CurrentValue");
            PropertyValuesGrid.Columns[2].HeaderText = ResourceManager.GetString("DefaultValue");

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            var resetText = ResourceManager.GetString("Reset");
            PropertyValuesGrid.Rows.Clear();
            for (var i = 0; i < Properties.Length; i++)
            {
                var rowIndex = PropertyValuesGrid.Rows.Add(ResourceManager.GetString(Properties[i].Name), CurrentPropertyValues[i], DefaultPropertyValues[i]);
                var row = PropertyValuesGrid.Rows[rowIndex];
                row.Cells[row.Cells.Count - 1].Value = resetText;
            }

            PropertyValuesGrid.Refresh();
        }

        private void SetResultObject()
        {
            var newObj = Activator.CreateInstance<T>();
            for (var i = 0; i < Properties.Length; i++)
            {
                Properties[i].SetValue(newObj, CurrentPropertyValues[i]);
            }

            Result = newObj;
        }

        private void Reset(int index)
        {
            if (index > -1)
            {
                CurrentPropertyValues[index] = DefaultPropertyValues[index];
            }            
        }
    }
}
