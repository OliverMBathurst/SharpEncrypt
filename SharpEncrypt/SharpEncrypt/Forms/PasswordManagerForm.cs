using SharpEncrypt.ExtensionClasses;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
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
            GridView.AutoGenerateColumns = false;
            GridView.DataSource = Models;
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
            GridContextMenuStrip.Items[0].Text = ResourceManager.GetString("GenerateNewPassword");
            GridContextMenuStrip.Items[1].Text = ResourceManager.GetString("Remove");

            TotalLabel.Text = $"{ResourceManager.GetString("Total")}:";
            SelectedLabel.Text = string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("0OutOfNSelected"), GridView.SelectedRows.Count, GridView.Rows.Count);

            GridView.SelectionChanged += GridView_SelectionChanged;
        }

        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            SelectedLabel.Text = string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("0OutOfNSelected"), GridView.SelectedRows.Count, GridView.Rows.Count);
            SelectedRowDetailsBox.Text = GetSelectSelectedRowDetailsBoxText();
        }

        private void FullRefresh()
        {
            GridView.Refresh();
            TotalLabel.Text = $"{ResourceManager.GetString("Total")}: {Models.Count}";
            SelectedLabel.Text = string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("0OutOfNSelected"), GridView.SelectedRows.Count, GridView.Rows.Count);
        }

        #region Button handlers
        private void OK_Click(object sender, EventArgs e)
        {
            foreach (var model in Models)
            {
                model.LastAccess = DateTime.Now;
                model.OldPasswords.Clear();
            }
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
            var model = new PasswordModel { PasswordName = ResourceManager.GetString("Name")};            
            Models.Add(model);
            UndoStack.Push(new Operation { OperationType = OperationType.Add, Models = new[] { model } });
            FullRefresh();
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            var models = GridView.Rows.Where(x => x.Selected)
                .Select(x => x.DataBoundItem is PasswordModel model ? model : null)
                .Where(x => x != null)
                .ToList();
            
            Models.RemoveAll(models);
            UndoStack.Push(new Operation { OperationType = OperationType.Remove, Models = models });
            FullRefresh();
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            if(UndoStack.Count != 0)
                Undo(UndoStack.Pop());
        }

        #endregion

        private string GetSelectSelectedRowDetailsBoxText()
        {
            var sb = new StringBuilder();
            foreach(DataGridViewRow row in GridView.SelectedRows)
            {
                if(row.DataBoundItem is PasswordModel model)
                {
                    sb.Append(
                        $"{ResourceManager.GetString("Name")}: {model.PasswordName ?? string.Empty}, " +
                        $"{ResourceManager.GetString("UserName")}: {model.UserName ?? string.Empty}, " +
                        $"{ResourceManager.GetString("Password")}: {'*'.Repeat(model.Password != null ? model.Password.Length : 0)}, " +
                        $"{ResourceManager.GetString("Address")}: {model.Address ?? string.Empty}, " +
                        $"{ResourceManager.GetString("Notes")}: {model.Notes ?? string.Empty}, " +
                        $"{ResourceManager.GetString("CreationTime")}: {model.Created}, " +
                        $"{ResourceManager.GetString("LastModified")}: {model.Modified}, " +
                        $"{ResourceManager.GetString("LastAccess")}: {model.LastAccess}\n");
                }
            }
            return sb.ToString();
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
                case OperationType.PasswordChange:
                    foreach(var model in operation.Models)
                    {
                        if(model.OldPasswords.Count > 0)
                            model.Password = model.OldPasswords.Pop();
                    }
                    break;
            }

            FullRefresh();
        }

        #region Classes
        private class Operation
        {
            public IEnumerable<PasswordModel> Models { get; set; }

            public OperationType OperationType { get; set; }
        }

        private enum OperationType 
        { 
            Add,
            Remove,
            PasswordChange
        }
        #endregion

        #region Context menu items
        private void GenerateNewPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in GridView.SelectedRows)
            {
                if(row.DataBoundItem is PasswordModel model)
                {
                    model.OldPasswords.Push(model.Password);
                    model.Password = PasswordHelper.GeneratePassword();
                    UndoStack.Push(new Operation { OperationType = OperationType.PasswordChange, Models = new[] { model } });
                }
            }

            FullRefresh();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in GridView.SelectedRows)
            {
                if(row.DataBoundItem is PasswordModel model)
                {
                    Models.Remove(model);
                    UndoStack.Push(new Operation { OperationType = OperationType.Remove, Models = new[] { model } });
                }
            }

            FullRefresh();
        }

        #endregion
    }
}