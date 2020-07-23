using SharpEncrypt.AbstractClasses;
using SharpEncrypt.ExtensionClasses;
using SharpEncrypt.Managers;
using System;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    public partial class ActiveTasksForm : Form
    {
        private readonly BindingList<SharpEncryptTask> ActiveTasks = new BindingList<SharpEncryptTask>();
        private readonly ResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly TaskManager TaskManager;
        private readonly bool ExitOnCompletion;

        public delegate void AllTasksCompletedEventHandler();
        public event AllTasksCompletedEventHandler AllTasksCompleted;


        public ActiveTasksForm(TaskManager taskManager, bool exitOnCompletion = false)
        {
            InitializeComponent();
            TaskManager = taskManager ?? throw new ArgumentNullException(nameof(taskManager));
            ExitOnCompletion = exitOnCompletion;
        }

        private void TaskCompleted(SharpEncryptTask task)
        {
            ActiveTasks.Remove(task);
            ActiveTasksGridView.Refresh();
            if (!ActiveTasks.Any() && ExitOnCompletion)
                AllTasksCompleted?.Invoke();
        }

        private void TaskManagerCompleted(bool hasRemainingTasks) => AllTasksCompleted?.Invoke();

        private void ActiveTasksForm_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("ActiveTasks");
            ActiveTasks.AddRange(TaskManager.Tasks);
            TaskManager.TaskCompleted += TaskCompleted;
            TaskManager.TaskManagerCompleted += TaskManagerCompleted;
            if (!ActiveTasks.Any() || ActiveTasks.All(x => x.InnerTask.IsCompleted))
                AllTasksCompleted?.Invoke();

            ActiveTasksGridView.AutoGenerateColumns = false;
            ActiveTasksGridView.DataSource = ActiveTasks;
            ActiveTasksGridView.Refresh();
        }
    }
}
