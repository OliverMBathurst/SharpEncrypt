using SharpEncrypt.ExtensionClasses;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    public partial class PasswordManagerForm : Form
    {
        private readonly ResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly BindingList<PasswordModel> Models = new BindingList<PasswordModel>();
        private readonly PasswordHelper PasswordHelper = new PasswordHelper();
        private readonly Stack<Operation> UndoStack = new Stack<Operation>();

        public PasswordManagerForm(List<PasswordModel> models)
        {
            InitializeComponent();

            if (models == null)
                throw new ArgumentNullException(nameof(models));

            Models.AddRange(models);
            GridView.DataSource = Models;
            GridView.CellClick += GridView_CellClick;
        }

        private void GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1) 
            {
                if (GridView.Rows[e.RowIndex].DataBoundItem is PasswordModel model)
                {
                    switch (GridView.Columns[e.ColumnIndex].Name)
                    {
                        case "Generate":
                            model.Password = PasswordHelper.GeneratePassword();
                            GridView.Refresh();
                            break;
                        case "Delete":
                            Models.Remove(model);
                            break;
                    }
                }
            }
        }

        public List<PasswordModel> PasswordModels => Models.ToList();

        private void PasswordManager_Load(object sender, EventArgs e)
        {
            SearchGroupBox.Text = ResourceManager.GetString("Search");
            OK.Text = ResourceManager.GetString("OK");
            Text = ResourceManager.GetString("PasswordManager");
            PasswordGenerator.Text = ResourceManager.GetString("PasswordGenerator");
            AddButton.Text = ResourceManager.GetString("Add");
            RemoveButton.Text = ResourceManager.GetString("Remove");
            UndoButton.Text = ResourceManager.GetString("Undo");

            GridView.AutoGenerateColumns = false;
            GridView.RowsAdded += GridView_RowsAdded;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void PasswordGenerator_Click(object sender, EventArgs e)
        {
            using (var passwordInputDialog = new PasswordInputDialog(isDisplayOnly: true))
            {
                passwordInputDialog.ShowDialog();
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            var model = new PasswordModel();
            Models.Add(model);
            UndoStack.Push(new Operation { OperationType = OperationType.Add, Models = new[] { model } });
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            var models = GridView.Rows.Where(x => x.Selected)
                .Select(x => x.DataBoundItem is PasswordModel model ? model : null)
                .Where(x => x != null)
                .ToList();
            Models.RemoveAll(models);
            UndoStack.Push(new Operation { OperationType = OperationType.Remove, Models = models });
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            if(UndoStack.Count != 0)
                Undo(UndoStack.Pop());
        }

        private void Undo(Operation operation)
        {
            switch (operation.OperationType)
            {
                case OperationType.Add:
                    foreach(var model in operation.Models)
                    {
                        Models.Remove(model);
                    }
                    break;
                case OperationType.Remove:
                    foreach(var model in operation.Models)
                    {
                        Models.Add(model);
                    }
                    break;
            }
        }

        private void GridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in GridView.Rows)
            {
                foreach (var cell in row.Cells)
                {
                    if (cell is DataGridViewButtonCell buttonCell)
                    {
                        switch (buttonCell.OwningColumn.Name)
                        {
                            case "Generate":
                                buttonCell.Value = ResourceManager.GetString("Generate");
                                break;
                            case "Delete":
                                buttonCell.Value = ResourceManager.GetString("Delete");
                                break;
                        }
                    }
                }
            }
        }

        private class Operation
        {
            public IEnumerable<PasswordModel> Models { get; set; }

            public OperationType OperationType { get; set; }
        }

        private enum OperationType 
        { 
            Add,
            Remove
        }
    }
}