namespace SharpEncrypt
{
    partial class PasswordInputDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordInputDialog));
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.PasswordGroupBox = new System.Windows.Forms.GroupBox();
            this.Copy = new System.Windows.Forms.Button();
            this.ShowPassword = new System.Windows.Forms.Button();
            this.PasswordInputBox = new System.Windows.Forms.TextBox();
            this.Generator = new System.Windows.Forms.GroupBox();
            this.New = new System.Windows.Forms.Button();
            this.PasswordGeneratorField = new System.Windows.Forms.TextBox();
            this.CopyGenerated = new System.Windows.Forms.Button();
            this.StrengthLabel = new System.Windows.Forms.Label();
            this.PasswordStrengthProgressBar = new System.Windows.Forms.ProgressBar();
            this.PasswordGroupBox.SuspendLayout();
            this.Generator.SuspendLayout();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OK.Location = new System.Drawing.Point(495, 408);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(138, 66);
            this.OK.TabIndex = 0;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Cancel.Location = new System.Drawing.Point(653, 408);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(138, 66);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // PasswordGroupBox
            // 
            this.PasswordGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordGroupBox.Controls.Add(this.PasswordStrengthProgressBar);
            this.PasswordGroupBox.Controls.Add(this.Copy);
            this.PasswordGroupBox.Controls.Add(this.StrengthLabel);
            this.PasswordGroupBox.Controls.Add(this.ShowPassword);
            this.PasswordGroupBox.Controls.Add(this.PasswordInputBox);
            this.PasswordGroupBox.Location = new System.Drawing.Point(13, 13);
            this.PasswordGroupBox.Name = "PasswordGroupBox";
            this.PasswordGroupBox.Size = new System.Drawing.Size(778, 182);
            this.PasswordGroupBox.TabIndex = 2;
            this.PasswordGroupBox.TabStop = false;
            this.PasswordGroupBox.Text = "Password";
            // 
            // Copy
            // 
            this.Copy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Copy.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Copy.Location = new System.Drawing.Point(616, 66);
            this.Copy.Name = "Copy";
            this.Copy.Size = new System.Drawing.Size(75, 41);
            this.Copy.TabIndex = 5;
            this.Copy.Text = "Copy";
            this.Copy.UseVisualStyleBackColor = true;
            this.Copy.Click += new System.EventHandler(this.Copy_Click);
            // 
            // ShowPassword
            // 
            this.ShowPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowPassword.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ShowPassword.Location = new System.Drawing.Point(697, 66);
            this.ShowPassword.Name = "ShowPassword";
            this.ShowPassword.Size = new System.Drawing.Size(75, 41);
            this.ShowPassword.TabIndex = 4;
            this.ShowPassword.Text = "Show";
            this.ShowPassword.UseVisualStyleBackColor = true;
            this.ShowPassword.Click += new System.EventHandler(this.ShowPassword_Click);
            // 
            // PasswordInputBox
            // 
            this.PasswordInputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordInputBox.Location = new System.Drawing.Point(11, 34);
            this.PasswordInputBox.Name = "PasswordInputBox";
            this.PasswordInputBox.Size = new System.Drawing.Size(761, 26);
            this.PasswordInputBox.TabIndex = 3;
            this.PasswordInputBox.UseSystemPasswordChar = true;
            // 
            // Generator
            // 
            this.Generator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Generator.Controls.Add(this.CopyGenerated);
            this.Generator.Controls.Add(this.New);
            this.Generator.Controls.Add(this.PasswordGeneratorField);
            this.Generator.Location = new System.Drawing.Point(13, 270);
            this.Generator.Name = "Generator";
            this.Generator.Size = new System.Drawing.Size(778, 115);
            this.Generator.TabIndex = 3;
            this.Generator.TabStop = false;
            this.Generator.Text = "Generator";
            // 
            // New
            // 
            this.New.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.New.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.New.Location = new System.Drawing.Point(697, 66);
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(75, 41);
            this.New.TabIndex = 4;
            this.New.Text = "New";
            this.New.UseVisualStyleBackColor = true;
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // PasswordGeneratorField
            // 
            this.PasswordGeneratorField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordGeneratorField.Location = new System.Drawing.Point(11, 34);
            this.PasswordGeneratorField.Name = "PasswordGeneratorField";
            this.PasswordGeneratorField.ReadOnly = true;
            this.PasswordGeneratorField.Size = new System.Drawing.Size(761, 26);
            this.PasswordGeneratorField.TabIndex = 3;
            // 
            // CopyGenerated
            // 
            this.CopyGenerated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyGenerated.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CopyGenerated.Location = new System.Drawing.Point(616, 66);
            this.CopyGenerated.Name = "CopyGenerated";
            this.CopyGenerated.Size = new System.Drawing.Size(75, 41);
            this.CopyGenerated.TabIndex = 5;
            this.CopyGenerated.Text = "Copy";
            this.CopyGenerated.UseVisualStyleBackColor = true;
            this.CopyGenerated.Click += new System.EventHandler(this.CopyGenerated_Click);
            // 
            // StrengthLabel
            // 
            this.StrengthLabel.AutoSize = true;
            this.StrengthLabel.Location = new System.Drawing.Point(7, 110);
            this.StrengthLabel.Name = "StrengthLabel";
            this.StrengthLabel.Size = new System.Drawing.Size(71, 20);
            this.StrengthLabel.TabIndex = 4;
            this.StrengthLabel.Text = "Strength";
            // 
            // PasswordStrengthProgressBar
            // 
            this.PasswordStrengthProgressBar.ForeColor = System.Drawing.Color.Transparent;
            this.PasswordStrengthProgressBar.Location = new System.Drawing.Point(11, 133);
            this.PasswordStrengthProgressBar.Name = "PasswordStrengthProgressBar";
            this.PasswordStrengthProgressBar.Size = new System.Drawing.Size(761, 33);
            this.PasswordStrengthProgressBar.TabIndex = 5;
            // 
            // PasswordInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 486);
            this.Controls.Add(this.Generator);
            this.Controls.Add(this.PasswordGroupBox);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "PasswordInputDialog";
            this.Text = "PasswordInput";
            this.Load += new System.EventHandler(this.PasswordInput_Load);
            this.PasswordGroupBox.ResumeLayout(false);
            this.PasswordGroupBox.PerformLayout();
            this.Generator.ResumeLayout(false);
            this.Generator.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.GroupBox PasswordGroupBox;
        private System.Windows.Forms.TextBox PasswordInputBox;
        private System.Windows.Forms.Button ShowPassword;
        private System.Windows.Forms.Button Copy;
        private System.Windows.Forms.GroupBox Generator;
        private System.Windows.Forms.Button New;
        private System.Windows.Forms.TextBox PasswordGeneratorField;
        private System.Windows.Forms.Button CopyGenerated;
        private System.Windows.Forms.Label StrengthLabel;
        private System.Windows.Forms.ProgressBar PasswordStrengthProgressBar;
    }
}