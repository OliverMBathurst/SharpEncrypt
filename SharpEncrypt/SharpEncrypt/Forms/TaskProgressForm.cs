using SharpEncrypt.AbstractClasses;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    public partial class TaskProgressForm : Form
    {
        private readonly ComponentResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly TaskManager TaskManager;
        private readonly int InitialTaskCount;

        public TaskProgressForm(TaskManager taskManager)
        {
            InitializeComponent();

            TaskManager = taskManager ?? throw new ArgumentNullException(nameof(taskManager));
            TaskManager.TaskDequeued += TaskManager_TaskDequeuedEvent;
            TaskManager.TaskManagerCompleted += TaskManager_TaskManagerCompletedEvent;

            TaskManager.SetCancellationFlag();
            InitialTaskCount = TaskManager.TaskCount;
        }

        private void TaskManager_TaskManagerCompletedEvent()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void TaskManager_TaskDequeuedEvent(SharpEncryptTask task)
        {
            var count = TaskManager.TaskCount;
            TaskProgressBar.Value = (count / InitialTaskCount) * 100;
            TaskProgressTextBox.Text = string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("TasksRemaining"), count);
        }

        private void TaskProgressForm_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("TaskProgressForm");
        }
    }
}