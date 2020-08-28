namespace SharpEncrypt.Forms
{
    partial class ImportPublicKeyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportPublicKeyForm));
            this.IdentityField = new System.Windows.Forms.TextBox();
            this.Identity = new System.Windows.Forms.GroupBox();
            this.PublicKeyGroupBox = new System.Windows.Forms.GroupBox();
            this.PublicKeyFilePathField = new System.Windows.Forms.TextBox();
            this.Browse = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Identity.SuspendLayout();
            this.PublicKeyGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // IdentityField
            // 
            this.IdentityField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IdentityField.Location = new System.Drawing.Point(6, 41);
            this.IdentityField.Name = "IdentityField";
            this.IdentityField.Size = new System.Drawing.Size(751, 26);
            this.IdentityField.TabIndex = 0;
            // 
            // Identity
            // 
            this.Identity.Controls.Add(this.IdentityField);
            this.Identity.Location = new System.Drawing.Point(25, 25);
            this.Identity.Name = "Identity";
            this.Identity.Size = new System.Drawing.Size(763, 104);
            this.Identity.TabIndex = 1;
            this.Identity.TabStop = false;
            this.Identity.Text = "Identity";
            // 
            // PublicKeyGroupBox
            // 
            this.PublicKeyGroupBox.Controls.Add(this.PublicKeyFilePathField);
            this.PublicKeyGroupBox.Controls.Add(this.Browse);
            this.PublicKeyGroupBox.Location = new System.Drawing.Point(25, 156);
            this.PublicKeyGroupBox.Name = "PublicKeyGroupBox";
            this.PublicKeyGroupBox.Size = new System.Drawing.Size(763, 158);
            this.PublicKeyGroupBox.TabIndex = 2;
            this.PublicKeyGroupBox.TabStop = false;
            this.PublicKeyGroupBox.Text = "Public Key";
            // 
            // PublicKeyFilePathField
            // 
            this.PublicKeyFilePathField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PublicKeyFilePathField.Location = new System.Drawing.Point(6, 55);
            this.PublicKeyFilePathField.Name = "PublicKeyFilePathField";
            this.PublicKeyFilePathField.ReadOnly = true;
            this.PublicKeyFilePathField.Size = new System.Drawing.Size(751, 26);
            this.PublicKeyFilePathField.TabIndex = 1;
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(663, 113);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(94, 39);
            this.Browse.TabIndex = 0;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // OK
            // 
            this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OK.Location = new System.Drawing.Point(588, 398);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(86, 40);
            this.OK.TabIndex = 3;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.Location = new System.Drawing.Point(696, 398);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(86, 40);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // ImportPublicKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.PublicKeyGroupBox);
            this.Controls.Add(this.Identity);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImportPublicKeyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImportPublicKeyForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ImportPublicKeyForm_Load);
            this.Identity.ResumeLayout(false);
            this.Identity.PerformLayout();
            this.PublicKeyGroupBox.ResumeLayout(false);
            this.PublicKeyGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox IdentityField;
        private System.Windows.Forms.GroupBox Identity;
        private System.Windows.Forms.GroupBox PublicKeyGroupBox;
        private System.Windows.Forms.TextBox PublicKeyFilePathField;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
    }
}