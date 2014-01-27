namespace HouseholdBudget.UI
{
    partial class frmUpdateCategories
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.categoryDataGrid = new System.Windows.Forms.DataGridView();
            this.categoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblUpdateCategories = new System.Windows.Forms.Label();
            this.categoryNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subCategoryNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subCategoryPrefixDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.categoryDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // categoryDataGrid
            // 
            this.categoryDataGrid.AllowUserToAddRows = false;
            this.categoryDataGrid.AllowUserToDeleteRows = false;
            this.categoryDataGrid.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.categoryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.categoryDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.categoryDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.categoryNameDataGridViewTextBoxColumn,
            this.subCategoryNameDataGridViewTextBoxColumn,
            this.subCategoryPrefixDataGridViewTextBoxColumn,
            this.isActiveDataGridViewCheckBoxColumn});
            this.categoryDataGrid.DataSource = this.categoryBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.categoryDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.categoryDataGrid.Location = new System.Drawing.Point(12, 47);
            this.categoryDataGrid.Name = "categoryDataGrid";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.categoryDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.categoryDataGrid.Size = new System.Drawing.Size(536, 320);
            this.categoryDataGrid.TabIndex = 0;
            this.categoryDataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.categoryDataGrid_CellEndEdit);
            // 
            // categoryBindingSource
            // 
            this.categoryBindingSource.DataSource = typeof(HouseholdBudget.Data.Domain.Category);
            // 
            // lblUpdateCategories
            // 
            this.lblUpdateCategories.AutoSize = true;
            this.lblUpdateCategories.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateCategories.Location = new System.Drawing.Point(13, 13);
            this.lblUpdateCategories.Name = "lblUpdateCategories";
            this.lblUpdateCategories.Size = new System.Drawing.Size(284, 19);
            this.lblUpdateCategories.TabIndex = 1;
            this.lblUpdateCategories.Text = "Update Category Details in the grid below.";
            // 
            // categoryNameDataGridViewTextBoxColumn
            // 
            this.categoryNameDataGridViewTextBoxColumn.DataPropertyName = "CategoryName";
            this.categoryNameDataGridViewTextBoxColumn.HeaderText = "Category";
            this.categoryNameDataGridViewTextBoxColumn.Name = "categoryNameDataGridViewTextBoxColumn";
            this.categoryNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // subCategoryNameDataGridViewTextBoxColumn
            // 
            this.subCategoryNameDataGridViewTextBoxColumn.DataPropertyName = "SubCategoryName";
            this.subCategoryNameDataGridViewTextBoxColumn.HeaderText = "SubCategory";
            this.subCategoryNameDataGridViewTextBoxColumn.Name = "subCategoryNameDataGridViewTextBoxColumn";
            this.subCategoryNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // subCategoryPrefixDataGridViewTextBoxColumn
            // 
            this.subCategoryPrefixDataGridViewTextBoxColumn.DataPropertyName = "SubCategoryPrefix";
            this.subCategoryPrefixDataGridViewTextBoxColumn.HeaderText = "Prefix";
            this.subCategoryPrefixDataGridViewTextBoxColumn.Name = "subCategoryPrefixDataGridViewTextBoxColumn";
            this.subCategoryPrefixDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // isActiveDataGridViewCheckBoxColumn
            // 
            this.isActiveDataGridViewCheckBoxColumn.DataPropertyName = "IsActive";
            this.isActiveDataGridViewCheckBoxColumn.HeaderText = "Enabled?";
            this.isActiveDataGridViewCheckBoxColumn.Name = "isActiveDataGridViewCheckBoxColumn";
            this.isActiveDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.isActiveDataGridViewCheckBoxColumn.Width = 75;
            // 
            // frmUpdateCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 379);
            this.Controls.Add(this.lblUpdateCategories);
            this.Controls.Add(this.categoryDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmUpdateCategories";
            this.Text = "Update Categories";
            this.Load += new System.EventHandler(this.frmUpdateCategories_Load);
            ((System.ComponentModel.ISupportInitialize)(this.categoryDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView categoryDataGrid;
        private System.Windows.Forms.BindingSource categoryBindingSource;
        private System.Windows.Forms.Label lblUpdateCategories;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn subCategoryNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn subCategoryPrefixDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActiveDataGridViewCheckBoxColumn;
    }
}