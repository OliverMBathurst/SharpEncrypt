namespace SharpEncrypt.Forms
{
    partial class TaskProgressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskProgressForm));
            this.TaskProgressBar = new System.Windows.Forms.ProgressBar();
            this.TaskProgressTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TaskProgressBar
            // 
            this.TaskProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TaskProgressBar.Location = new System.Drawing.Point(12, 73);
            this.TaskProgressBar.Name = "TaskProgressBar";
            this.TaskProgressBar.Size = new System.Drawing.Size(371, 55);
            this.TaskProgressBar.TabIndex = 0;
            // 
            // TaskProgressTextBox
            // 
            this.TaskProgressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TaskProgressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TaskProgressTextBox.Location = new System.Drawing.Point(12, 31);
            this.TaskProgressTextBox.Name = "TaskProgressTextBox";
            this.TaskProgressTextBox.ReadOnly = true;
            this.TaskProgressTextBox.Size = new System.Drawing.Size(371, 19);
            this.TaskProgressTextBox.TabIndex = 1;
            // 
            // TaskProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 140);
            this.Controls.Add(this.TaskProgressTextBox);
            this.Controls.Add(this.TaskProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TaskProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task Progress";
            this.Load += new System.EventHandler(this.TaskProgressForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar TaskProgressBar;
        private System.Windows.Forms.TextBox TaskProgressTextBox;
    }
}