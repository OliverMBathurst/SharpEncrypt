namespace SharpEncrypt.Forms
{
    partial class ActiveTasksForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActiveTasksForm));
            this.ActiveTasksGridView = new System.Windows.Forms.DataGridView();
            this.Guid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ActiveTasksGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ActiveTasksGridView
            // 
            this.ActiveTasksGridView.AllowUserToAddRows = false;
            this.ActiveTasksGridView.AllowUserToDeleteRows = false;
            this.ActiveTasksGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ActiveTasksGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ActiveTasksGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Guid,
            this.TaskType});
            this.ActiveTasksGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActiveTasksGridView.Location = new System.Drawing.Point(0, 0);
            this.ActiveTasksGridView.Name = "ActiveTasksGridView";
            this.ActiveTasksGridView.ReadOnly = true;
            this.ActiveTasksGridView.RowHeadersVisible = false;
            this.ActiveTasksGridView.RowHeadersWidth = 62;
            this.ActiveTasksGridView.RowTemplate.Height = 28;
            this.ActiveTasksGridView.Size = new System.Drawing.Size(830, 459);
            this.ActiveTasksGridView.TabIndex = 1;
            // 
            // Guid
            // 
            this.Guid.DataPropertyName = "Identifier";
            this.Guid.HeaderText = "Guid";
            this.Guid.MinimumWidth = 8;
            this.Guid.Name = "Guid";
            this.Guid.ReadOnly = true;
            // 
            // TaskType
            // 
            this.TaskType.DataPropertyName = "TaskType";
            this.TaskType.HeaderText = "Task Type";
            this.TaskType.MinimumWidth = 8;
            this.TaskType.Name = "TaskType";
            this.TaskType.ReadOnly = true;
            // 
            // ActiveTasksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 459);
            this.Controls.Add(this.ActiveTasksGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ActiveTasksForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ActiveTasksForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ActiveTasksForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ActiveTasksGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ActiveTasksGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Guid;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskType;
    }
}