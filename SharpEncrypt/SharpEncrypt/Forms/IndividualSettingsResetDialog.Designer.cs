namespace SharpEncrypt.Forms
{
    partial class IndividualSettingsResetDialog<T>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources.Resources));
            this.Cancel = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.PropertyValuesGrid = new System.Windows.Forms.DataGridView();
            this.ResetAll = new System.Windows.Forms.Button();
            this.Property = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResetColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.PropertyValuesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.Location = new System.Drawing.Point(825, 462);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(112, 60);
            this.Cancel.TabIndex = 0;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // OK
            // 
            this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OK.Location = new System.Drawing.Point(707, 462);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(112, 60);
            this.OK.TabIndex = 1;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // PropertyValuesGrid
            // 
            this.PropertyValuesGrid.AllowUserToAddRows = false;
            this.PropertyValuesGrid.AllowUserToDeleteRows = false;
            this.PropertyValuesGrid.AllowUserToOrderColumns = true;
            this.PropertyValuesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PropertyValuesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PropertyValuesGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.PropertyValuesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PropertyValuesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Property,
            this.CurrentValue,
            this.DefaultValue,
            this.ResetColumn});
            this.PropertyValuesGrid.Location = new System.Drawing.Point(12, 12);
            this.PropertyValuesGrid.MultiSelect = false;
            this.PropertyValuesGrid.Name = "PropertyValuesGrid";
            this.PropertyValuesGrid.RowHeadersVisible = false;
            this.PropertyValuesGrid.RowHeadersWidth = 62;
            this.PropertyValuesGrid.RowTemplate.Height = 28;
            this.PropertyValuesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PropertyValuesGrid.Size = new System.Drawing.Size(925, 444);
            this.PropertyValuesGrid.TabIndex = 2;
            // 
            // ResetAll
            // 
            this.ResetAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResetAll.Location = new System.Drawing.Point(12, 462);
            this.ResetAll.Name = "ResetAll";
            this.ResetAll.Size = new System.Drawing.Size(112, 60);
            this.ResetAll.TabIndex = 3;
            this.ResetAll.Text = "Reset All";
            this.ResetAll.UseVisualStyleBackColor = true;
            this.ResetAll.Click += new System.EventHandler(this.ResetAll_Click);
            // 
            // Property
            // 
            this.Property.HeaderText = "Property";
            this.Property.MinimumWidth = 8;
            this.Property.Name = "Property";
            this.Property.ReadOnly = true;
            this.Property.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CurrentValue
            // 
            this.CurrentValue.HeaderText = "Current Value";
            this.CurrentValue.MinimumWidth = 8;
            this.CurrentValue.Name = "CurrentValue";
            this.CurrentValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DefaultValue
            // 
            this.DefaultValue.HeaderText = "Default Value";
            this.DefaultValue.MinimumWidth = 8;
            this.DefaultValue.Name = "DefaultValue";
            this.DefaultValue.ReadOnly = true;
            this.DefaultValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ResetColumn
            // 
            this.ResetColumn.HeaderText = "";
            this.ResetColumn.MinimumWidth = 8;
            this.ResetColumn.Name = "ResetColumn";
            this.ResetColumn.ReadOnly = true;
            this.ResetColumn.Text = "Result";
            // 
            // IndividualSettingsResetDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 534);
            this.Controls.Add(this.ResetAll);
            this.Controls.Add(this.PropertyValuesGrid);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IndividualSettingsResetDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Individual Settings Reset Dialog";
            this.Load += new System.EventHandler(this.IndividualSettingsResetDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PropertyValuesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.DataGridView PropertyValuesGrid;
        private System.Windows.Forms.Button ResetAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn Property;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefaultValue;
        private System.Windows.Forms.DataGridViewButtonColumn ResetColumn;
    }
}