using SharpEncrypt.AbstractClasses;
using SharpEncrypt.ExtensionClasses;
using SharpEncrypt.Managers;
using System;
using System.ComponentModel;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    public partial class ActiveTasksForm : Form
    {
        private readonly ResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly BindingList<SharpEncryptTask> ActiveTasks = new BindingList<SharpEncryptTask>();
        private readonly TaskManager TaskManager;

        public ActiveTasksForm(TaskManager taskManager)
        {
            InitializeComponent();
            TaskManager = taskManager ?? throw new ArgumentNullException(nameof(taskManager));
            ActiveTasks.AddRange(TaskManager.Tasks);
            TaskManager.TaskCompleted += TaskCompleted;
        }

        private void TaskCompleted(SharpEncryptTask task)
        {
            ActiveTasks.Remove(task);
        }

        private void ActiveTasksForm_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("ActiveTasks");
        }
    }
}
