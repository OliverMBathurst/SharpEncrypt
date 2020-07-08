﻿using SharpEncrypt.AbstractClasses;
using SharpEncrypt.ExtensionClasses;
using System;
using System.Collections.Generic;
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
            TaskManager.TaskCompleted += TaskCompleted;
        }

        private void TaskCompleted(SharpEncryptTask task)
        {
            var removals = new List<DataGridViewRow>();
            foreach(DataGridViewRow row in ActiveJobsGridView.Rows)
            {
                if(row.Cells[0].Value is Guid guid && guid.Equals(task.Identifier))
                {
                    removals.Add(row);
                }
            }

            ActiveJobsGridView.Rows.RemoveAll(removals);
        }

        private void ActiveTasksForm_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("ActiveTasks");
        }

        private void LoadTasks()
        {
            foreach(var task in TaskManager.Tasks)
            {
                ActiveJobsGridView.Rows.Add(task.Identifier, task.TaskType, task.IsSpecial);
            }
        }
    }
}
