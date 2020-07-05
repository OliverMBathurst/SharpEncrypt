using SharpEncrypt.AbstractClasses;
using System;
using System.ComponentModel;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    public partial class ActiveTasksForm : Form
    {
        private readonly ResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly TaskManager TaskManager;

        public ActiveTasksForm(TaskManager taskManager)
        {
            InitializeComponent();
            TaskManager = taskManager ?? throw new ArgumentNullException(nameof(taskManager));
            LoadTasks();
            TaskManager.GenericTaskCompleted += TaskManager_GenericTaskCompleted;
        }

        private void TaskManager_GenericTaskCompleted(SharpEncryptTask task)
        {
            foreach(DataGridViewRow row in ActiveJobsGridView.Rows)
            {
                if(row.Cells[0].Value is Guid guid && guid == task.Identifier)
                {
                    ActiveJobsGridView.Rows.Remove(row);
                }
            }
        }

        private void ActiveTasksForm_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("ActiveTasks");
        }

        private void LoadTasks()
        {
            foreach(var task in TaskManager.Tasks)
            {
                ActiveJobsGridView.Rows.Add(task.Identifier, task.TaskType, task.IsLongRunning);
            }
        }
    }
}
