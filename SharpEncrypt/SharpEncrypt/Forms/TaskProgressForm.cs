using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    public partial class TaskProgressForm : Form
    {
        private readonly ComponentResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly BackgroundTaskHandler BackgroundTaskHandler;
        private readonly int InitialTaskCount;


        public TaskProgressForm(BackgroundTaskHandler handler)
        {
            BackgroundTaskHandler = handler ?? throw new ArgumentNullException(nameof(handler));
            BackgroundTaskHandler.TaskDequeuedEvent += BackgroundTaskHandler_TaskDequeuedEvent;
            BackgroundTaskHandler.TasksCompleted += BackgroundTaskHandler_TasksCompleted;
            var taskCount = BackgroundTaskHandler.TaskCount;
            InitialTaskCount = taskCount == 0 ? 1 : taskCount;
            InitializeComponent();
        }

        private void BackgroundTaskHandler_TasksCompleted()
        {
            BackgroundTaskHandler.CancelWorker();
            while (BackgroundTaskHandler.IsBusy) { }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BackgroundTaskHandler_TaskDequeuedEvent(System.Threading.Tasks.Task task)
        {
            var tskCount = BackgroundTaskHandler.TaskCount;
            var taskCount = tskCount == 0 ? 1 : tskCount;

            var percentage = (taskCount / InitialTaskCount) * 100;
            TaskProgressBar.Value = percentage;
            
            TaskProgressTextBox.Text = string.Format(CultureInfo.CurrentCulture, ResourceManager.GetString("TasksRemaining"), taskCount);
        }

        private void TaskProgressForm_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("TaskProgressForm");
        }
    }
}