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
        private readonly ResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly BindingList<SharpEncryptTask> ActiveTasks = new BindingList<SharpEncryptTask>();
        private readonly bool ExitOnCompletion;

        public ActiveTasksForm(TaskManager taskManager, bool exitOnCompletion = false)
        {
            InitializeComponent();
            var taskManager1 = taskManager ?? throw new ArgumentNullException(nameof(taskManager));
            ActiveTasks.AddRange(taskManager1.Tasks);
            taskManager1.TaskCompleted += TaskCompleted;
            ExitOnCompletion = exitOnCompletion;
        }

        private void TaskCompleted(SharpEncryptTask task)
        {
            ActiveTasks.Remove(task);
            if (ActiveTasks.Any() || !ExitOnCompletion) return;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ActiveTasksForm_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("ActiveTasks");
        }
    }
}
