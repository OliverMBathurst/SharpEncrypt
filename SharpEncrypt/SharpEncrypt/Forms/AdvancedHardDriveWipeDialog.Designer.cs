namespace SharpEncrypt.Forms
{
    partial class AdvancedHardDriveWipeDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedHardDriveWipeDialog));
            this.ControlsPanel = new System.Windows.Forms.Panel();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.AddJob = new System.Windows.Forms.Button();
            this.ClearJobs = new System.Windows.Forms.Button();
            this.ViewJobs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ControlsPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ControlsPanel.Location = new System.Drawing.Point(11, 12);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Size = new System.Drawing.Size(1084, 442);
            this.ControlsPanel.TabIndex = 0;
            // 
            // OK
            // 
            this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OK.Location = new System.Drawing.Point(851, 470);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(119, 62);
            this.OK.TabIndex = 1;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.Location = new System.Drawing.Point(976, 470);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(119, 62);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // AddJob
            // 
            this.AddJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddJob.Location = new System.Drawing.Point(726, 470);
            this.AddJob.Name = "AddJob";
            this.AddJob.Size = new System.Drawing.Size(119, 62);
            this.AddJob.TabIndex = 3;
            this.AddJob.Text = "Add Job";
            this.AddJob.UseVisualStyleBackColor = true;
            this.AddJob.Click += new System.EventHandler(this.Options_Click);
            // 
            // ClearJobs
            // 
            this.ClearJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearJobs.Location = new System.Drawing.Point(601, 470);
            this.ClearJobs.Name = "ClearJobs";
            this.ClearJobs.Size = new System.Drawing.Size(119, 62);
            this.ClearJobs.TabIndex = 4;
            this.ClearJobs.Text = "Clear Jobs";
            this.ClearJobs.UseVisualStyleBackColor = true;
            this.ClearJobs.Click += new System.EventHandler(this.ClearJobs_Click);
            // 
            // ViewJobs
            // 
            this.ViewJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewJobs.Location = new System.Drawing.Point(476, 470);
            this.ViewJobs.Name = "ViewJobs";
            this.ViewJobs.Size = new System.Drawing.Size(119, 62);
            this.ViewJobs.TabIndex = 5;
            this.ViewJobs.Text = "View Jobs";
            this.ViewJobs.UseVisualStyleBackColor = true;
            this.ViewJobs.Click += new System.EventHandler(this.ViewJobs_Click);
            // 
            // AdvancedHardDriveWipeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 544);
            this.Controls.Add(this.ViewJobs);
            this.Controls.Add(this.ClearJobs);
            this.Controls.Add(this.AddJob);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.ControlsPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdvancedHardDriveWipeDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HardDriveWipeDialog";
            this.Load += new System.EventHandler(this.AdvancedHardDriveWipeDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ControlsPanel;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button AddJob;
        private System.Windows.Forms.Button ClearJobs;
        private System.Windows.Forms.Button ViewJobs;
    }
}