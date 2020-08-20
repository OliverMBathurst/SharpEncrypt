namespace SharpEncrypt.Forms
{
    partial class GenericGridForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenericGridForm));
            this.OK = new System.Windows.Forms.Button();
            this.GridControl = new SharpEncrypt.Controls.GenericGridControl();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OK.Location = new System.Drawing.Point(699, 467);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(104, 61);
            this.OK.TabIndex = 1;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // GridControl
            // 
            this.GridControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridControl.BackColor = System.Drawing.Color.White;
            this.GridControl.ForeColor = System.Drawing.Color.Black;
            this.GridControl.Location = new System.Drawing.Point(12, 12);
            this.GridControl.Name = "GridControl";
            this.GridControl.Size = new System.Drawing.Size(791, 446);
            this.GridControl.TabIndex = 2;
            // 
            // GenericGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 530);
            this.Controls.Add(this.GridControl);
            this.Controls.Add(this.OK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(827, 586);
            this.Name = "GenericGridForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.GenericGridForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button OK;
        private Controls.GenericGridControl GridControl;
    }
}