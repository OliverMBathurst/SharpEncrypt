namespace SharpEncrypt.Controls
{
    partial class DriveSelectionControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DriveDataGrid = new System.Windows.Forms.DataGridView();
            this.SelectedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Drive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AvailableSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DriveType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DriveFormat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DriveDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // DriveDataGrid
            // 
            this.DriveDataGrid.AllowUserToAddRows = false;
            this.DriveDataGrid.AllowUserToDeleteRows = false;
            this.DriveDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DriveDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.DriveDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DriveDataGrid.ColumnHeadersHeight = 34;
            this.DriveDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectedColumn,
            this.Drive,
            this.TotalSize,
            this.AvailableSize,
            this.DriveType,
            this.DriveFormat});
            this.DriveDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DriveDataGrid.Location = new System.Drawing.Point(0, 0);
            this.DriveDataGrid.Name = "DriveDataGrid";
            this.DriveDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DriveDataGrid.RowHeadersVisible = false;
            this.DriveDataGrid.RowHeadersWidth = 62;
            this.DriveDataGrid.RowTemplate.Height = 28;
            this.DriveDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DriveDataGrid.Size = new System.Drawing.Size(897, 566);
            this.DriveDataGrid.TabIndex = 1;
            // 
            // SelectedColumn
            // 
            this.SelectedColumn.FillWeight = 20F;
            this.SelectedColumn.HeaderText = "";
            this.SelectedColumn.MinimumWidth = 8;
            this.SelectedColumn.Name = "SelectedColumn";
            this.SelectedColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SelectedColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Drive
            // 
            this.Drive.FillWeight = 85.9091F;
            this.Drive.HeaderText = "Drive";
            this.Drive.MinimumWidth = 8;
            this.Drive.Name = "Drive";
            // 
            // TotalSize
            // 
            this.TotalSize.FillWeight = 85.9091F;
            this.TotalSize.HeaderText = "Total Size";
            this.TotalSize.MinimumWidth = 8;
            this.TotalSize.Name = "TotalSize";
            // 
            // AvailableSize
            // 
            this.AvailableSize.FillWeight = 85.9091F;
            this.AvailableSize.HeaderText = "Available Space";
            this.AvailableSize.MinimumWidth = 8;
            this.AvailableSize.Name = "AvailableSize";
            // 
            // DriveType
            // 
            this.DriveType.FillWeight = 85.9091F;
            this.DriveType.HeaderText = "Drive Type";
            this.DriveType.MinimumWidth = 8;
            this.DriveType.Name = "DriveType";
            // 
            // DriveFormat
            // 
            this.DriveFormat.FillWeight = 85.9091F;
            this.DriveFormat.HeaderText = "Drive Format";
            this.DriveFormat.MinimumWidth = 8;
            this.DriveFormat.Name = "DriveFormat";
            // 
            // DriveSelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.DriveDataGrid);
            this.Name = "DriveSelectionControl";
            this.Size = new System.Drawing.Size(897, 566);
            this.Load += new System.EventHandler(this.DriveSelectionControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DriveDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DriveDataGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Drive;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn AvailableSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriveType;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriveFormat;
    }
}
