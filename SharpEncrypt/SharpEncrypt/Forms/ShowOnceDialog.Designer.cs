namespace SharpEncrypt.Forms
{
    partial class ShowOnceDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowOnceDialog));
            this.DialogTextBox = new System.Windows.Forms.RichTextBox();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.NeverShowAgain = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // DialogTextBox
            // 
            this.DialogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DialogTextBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.DialogTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DialogTextBox.Location = new System.Drawing.Point(12, 12);
            this.DialogTextBox.Name = "DialogTextBox";
            this.DialogTextBox.ReadOnly = true;
            this.DialogTextBox.Size = new System.Drawing.Size(775, 344);
            this.DialogTextBox.TabIndex = 0;
            this.DialogTextBox.Text = "";
            // 
            // OK
            // 
            this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OK.Location = new System.Drawing.Point(533, 411);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(119, 51);
            this.OK.TabIndex = 1;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.Location = new System.Drawing.Point(668, 411);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(119, 51);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // NeverShowAgain
            // 
            this.NeverShowAgain.AutoSize = true;
            this.NeverShowAgain.Location = new System.Drawing.Point(12, 373);
            this.NeverShowAgain.Name = "NeverShowAgain";
            this.NeverShowAgain.Size = new System.Drawing.Size(160, 24);
            this.NeverShowAgain.TabIndex = 3;
            this.NeverShowAgain.Text = "Never show again";
            this.NeverShowAgain.UseVisualStyleBackColor = true;
            // 
            // ShowOnceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 474);
            this.Controls.Add(this.NeverShowAgain);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.DialogTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowOnceDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShowOnceDialog";
            this.Load += new System.EventHandler(this.ShowOnceDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox DialogTextBox;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.CheckBox NeverShowAgain;
    }
}