namespace SharpEncrypt.Forms
{
    partial class AdvancedHardDriveWipeOptionsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedHardDriveWipeOptionsDialog));
            this.WipeTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.WipeTypeComboBox = new System.Windows.Forms.ComboBox();
            this.WipeOptions = new System.Windows.Forms.GroupBox();
            this.NameObfuscationRounds = new System.Windows.Forms.Label();
            this.PropertyObfuscationRounds = new System.Windows.Forms.Label();
            this.WipeNumber = new System.Windows.Forms.Label();
            this.PropertyObfuscationPicker = new System.Windows.Forms.NumericUpDown();
            this.NameObfuscationPicker = new System.Windows.Forms.NumericUpDown();
            this.WipeRoundsPicker = new System.Windows.Forms.NumericUpDown();
            this.AddJob = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.WipeTypeGroupBox.SuspendLayout();
            this.WipeOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PropertyObfuscationPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NameObfuscationPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WipeRoundsPicker)).BeginInit();
            this.SuspendLayout();
            // 
            // WipeTypeGroupBox
            // 
            this.WipeTypeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WipeTypeGroupBox.Controls.Add(this.WipeTypeComboBox);
            this.WipeTypeGroupBox.Location = new System.Drawing.Point(16, 53);
            this.WipeTypeGroupBox.Name = "WipeTypeGroupBox";
            this.WipeTypeGroupBox.Size = new System.Drawing.Size(772, 79);
            this.WipeTypeGroupBox.TabIndex = 1;
            this.WipeTypeGroupBox.TabStop = false;
            this.WipeTypeGroupBox.Text = "Wipe Type";
            // 
            // WipeTypeComboBox
            // 
            this.WipeTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WipeTypeComboBox.FormattingEnabled = true;
            this.WipeTypeComboBox.Location = new System.Drawing.Point(6, 36);
            this.WipeTypeComboBox.Name = "WipeTypeComboBox";
            this.WipeTypeComboBox.Size = new System.Drawing.Size(760, 28);
            this.WipeTypeComboBox.TabIndex = 0;
            // 
            // WipeOptions
            // 
            this.WipeOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WipeOptions.Controls.Add(this.NameObfuscationRounds);
            this.WipeOptions.Controls.Add(this.PropertyObfuscationRounds);
            this.WipeOptions.Controls.Add(this.WipeNumber);
            this.WipeOptions.Controls.Add(this.PropertyObfuscationPicker);
            this.WipeOptions.Controls.Add(this.NameObfuscationPicker);
            this.WipeOptions.Controls.Add(this.WipeRoundsPicker);
            this.WipeOptions.Location = new System.Drawing.Point(16, 139);
            this.WipeOptions.Name = "WipeOptions";
            this.WipeOptions.Size = new System.Drawing.Size(772, 179);
            this.WipeOptions.TabIndex = 2;
            this.WipeOptions.TabStop = false;
            this.WipeOptions.Text = "Wipe Options";
            // 
            // NameObfuscationRounds
            // 
            this.NameObfuscationRounds.AutoSize = true;
            this.NameObfuscationRounds.Location = new System.Drawing.Point(154, 36);
            this.NameObfuscationRounds.Name = "NameObfuscationRounds";
            this.NameObfuscationRounds.Size = new System.Drawing.Size(201, 20);
            this.NameObfuscationRounds.TabIndex = 5;
            this.NameObfuscationRounds.Text = "Name Obfuscation Rounds";
            // 
            // PropertyObfuscationRounds
            // 
            this.PropertyObfuscationRounds.AutoSize = true;
            this.PropertyObfuscationRounds.Location = new System.Drawing.Point(2, 113);
            this.PropertyObfuscationRounds.Name = "PropertyObfuscationRounds";
            this.PropertyObfuscationRounds.Size = new System.Drawing.Size(218, 20);
            this.PropertyObfuscationRounds.TabIndex = 4;
            this.PropertyObfuscationRounds.Text = "Property Obfuscation Rounds";
            // 
            // WipeNumber
            // 
            this.WipeNumber.AutoSize = true;
            this.WipeNumber.Location = new System.Drawing.Point(2, 37);
            this.WipeNumber.Name = "WipeNumber";
            this.WipeNumber.Size = new System.Drawing.Size(105, 20);
            this.WipeNumber.TabIndex = 3;
            this.WipeNumber.Text = "Wipe Rounds";
            // 
            // PropertyObfuscationPicker
            // 
            this.PropertyObfuscationPicker.Location = new System.Drawing.Point(6, 136);
            this.PropertyObfuscationPicker.Name = "PropertyObfuscationPicker";
            this.PropertyObfuscationPicker.Size = new System.Drawing.Size(214, 26);
            this.PropertyObfuscationPicker.TabIndex = 2;
            // 
            // NameObfuscationPicker
            // 
            this.NameObfuscationPicker.Location = new System.Drawing.Point(154, 60);
            this.NameObfuscationPicker.Name = "NameObfuscationPicker";
            this.NameObfuscationPicker.Size = new System.Drawing.Size(201, 26);
            this.NameObfuscationPicker.TabIndex = 1;
            // 
            // WipeRoundsPicker
            // 
            this.WipeRoundsPicker.Location = new System.Drawing.Point(6, 60);
            this.WipeRoundsPicker.Name = "WipeRoundsPicker";
            this.WipeRoundsPicker.Size = new System.Drawing.Size(101, 26);
            this.WipeRoundsPicker.TabIndex = 0;
            this.WipeRoundsPicker.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // AddJob
            // 
            this.AddJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddJob.Location = new System.Drawing.Point(546, 378);
            this.AddJob.Name = "AddJob";
            this.AddJob.Size = new System.Drawing.Size(118, 60);
            this.AddJob.TabIndex = 3;
            this.AddJob.Text = "Add Job";
            this.AddJob.UseVisualStyleBackColor = true;
            this.AddJob.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.Location = new System.Drawing.Point(670, 378);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(118, 60);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // AdvancedHardDriveWipeOptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.AddJob);
            this.Controls.Add(this.WipeOptions);
            this.Controls.Add(this.WipeTypeGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdvancedHardDriveWipeOptionsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdvancedHardDriveWipeOptionsDialog";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AdvancedHardDriveWipeOptionsDialog_Load);
            this.WipeTypeGroupBox.ResumeLayout(false);
            this.WipeOptions.ResumeLayout(false);
            this.WipeOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PropertyObfuscationPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NameObfuscationPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WipeRoundsPicker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox WipeTypeGroupBox;
        private System.Windows.Forms.ComboBox WipeTypeComboBox;
        private System.Windows.Forms.GroupBox WipeOptions;
        private System.Windows.Forms.NumericUpDown PropertyObfuscationPicker;
        private System.Windows.Forms.NumericUpDown NameObfuscationPicker;
        private System.Windows.Forms.NumericUpDown WipeRoundsPicker;
        private System.Windows.Forms.Label NameObfuscationRounds;
        private System.Windows.Forms.Label PropertyObfuscationRounds;
        private System.Windows.Forms.Label WipeNumber;
        private System.Windows.Forms.Button AddJob;
        private System.Windows.Forms.Button Cancel;
    }
}