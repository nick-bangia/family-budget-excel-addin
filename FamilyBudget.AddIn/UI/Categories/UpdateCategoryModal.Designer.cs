﻿namespace FamilyBudget.AddIn.UI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.categoryDataGrid = new System.Windows.Forms.DataGridView();
            this.dataGridCategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridAccountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.subCategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.categoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblUpdateCategories = new System.Windows.Forms.Label();
            this.cboCategories = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.categoryKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SubcategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubcategoryPrefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Account = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.IsAllocatable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isActiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.categoryDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCategoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAccountBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subCategoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // categoryDataGrid
            // 
            this.categoryDataGrid.AllowUserToAddRows = false;
            this.categoryDataGrid.AllowUserToDeleteRows = false;
            this.categoryDataGrid.AutoGenerateColumns = false;
            this.categoryDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.categoryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.categoryDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.categoryDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.categoryKeyDataGridViewTextBoxColumn,
            this.SubcategoryName,
            this.SubcategoryPrefix,
            this.Account,
            this.IsAllocatable,
            this.isActiveDataGridViewCheckBoxColumn});
            this.categoryDataGrid.DataSource = this.subCategoryBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.categoryDataGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.categoryDataGrid.Location = new System.Drawing.Point(12, 102);
            this.categoryDataGrid.Name = "categoryDataGrid";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.categoryDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.categoryDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.categoryDataGrid.Size = new System.Drawing.Size(578, 176);
            this.categoryDataGrid.TabIndex = 0;
            // 
            // dataGridCategoryBindingSource
            // 
            this.dataGridCategoryBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.Category);
            // 
            // dataGridAccountBindingSource
            // 
            this.dataGridAccountBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.Account);
            // 
            // subCategoryBindingSource
            // 
            this.subCategoryBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.Subcategory);
            // 
            // categoryBindingSource
            // 
            this.categoryBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.Category);
            // 
            // lblUpdateCategories
            // 
            this.lblUpdateCategories.AutoSize = true;
            this.lblUpdateCategories.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateCategories.Location = new System.Drawing.Point(13, 13);
            this.lblUpdateCategories.Name = "lblUpdateCategories";
            this.lblUpdateCategories.Size = new System.Drawing.Size(381, 19);
            this.lblUpdateCategories.TabIndex = 1;
            this.lblUpdateCategories.Text = "Choose a category for which to update subcategories for:";
            // 
            // cboCategories
            // 
            this.cboCategories.DataSource = this.categoryBindingSource;
            this.cboCategories.DisplayMember = "CategoryName";
            this.cboCategories.FormattingEnabled = true;
            this.cboCategories.Location = new System.Drawing.Point(17, 35);
            this.cboCategories.Name = "cboCategories";
            this.cboCategories.Size = new System.Drawing.Size(173, 21);
            this.cboCategories.TabIndex = 2;
            this.cboCategories.ValueMember = "Key";
            this.cboCategories.SelectedIndexChanged += new System.EventHandler(this.cboCategories_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImage = global::FamilyBudget.AddIn.Properties.Resources.arrow_refresh;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRefresh.Location = new System.Drawing.Point(566, 72);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(24, 24);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // categoryKeyDataGridViewTextBoxColumn
            // 
            this.categoryKeyDataGridViewTextBoxColumn.DataPropertyName = "CategoryKey";
            this.categoryKeyDataGridViewTextBoxColumn.DataSource = this.dataGridCategoryBindingSource;
            this.categoryKeyDataGridViewTextBoxColumn.DisplayMember = "CategoryName";
            this.categoryKeyDataGridViewTextBoxColumn.HeaderText = "Category";
            this.categoryKeyDataGridViewTextBoxColumn.Name = "categoryKeyDataGridViewTextBoxColumn";
            this.categoryKeyDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.categoryKeyDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.categoryKeyDataGridViewTextBoxColumn.ValueMember = "Key";
            // 
            // SubcategoryName
            // 
            this.SubcategoryName.DataPropertyName = "Name";
            this.SubcategoryName.HeaderText = "Name";
            this.SubcategoryName.Name = "SubcategoryName";
            this.SubcategoryName.Width = 150;
            // 
            // SubcategoryPrefix
            // 
            this.SubcategoryPrefix.DataPropertyName = "Prefix";
            this.SubcategoryPrefix.HeaderText = "Prefix";
            this.SubcategoryPrefix.Name = "SubcategoryPrefix";
            this.SubcategoryPrefix.Width = 65;
            // 
            // Account
            // 
            this.Account.DataPropertyName = "AccountKey";
            this.Account.DataSource = this.dataGridAccountBindingSource;
            this.Account.DisplayMember = "AccountName";
            this.Account.HeaderText = "Account";
            this.Account.Name = "Account";
            this.Account.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Account.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Account.ValueMember = "Key";
            this.Account.Width = 85;
            // 
            // IsAllocatable
            // 
            this.IsAllocatable.DataPropertyName = "IsAllocatable";
            this.IsAllocatable.HeaderText = "Allocatable?";
            this.IsAllocatable.Name = "IsAllocatable";
            this.IsAllocatable.Width = 75;
            // 
            // isActiveDataGridViewCheckBoxColumn
            // 
            this.isActiveDataGridViewCheckBoxColumn.DataPropertyName = "IsActive";
            this.isActiveDataGridViewCheckBoxColumn.HeaderText = "Enabled?";
            this.isActiveDataGridViewCheckBoxColumn.Name = "isActiveDataGridViewCheckBoxColumn";
            this.isActiveDataGridViewCheckBoxColumn.Width = 80;
            // 
            // frmUpdateCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 291);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cboCategories);
            this.Controls.Add(this.lblUpdateCategories);
            this.Controls.Add(this.categoryDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmUpdateCategories";
            this.Text = "Update Categories";
            this.Load += new System.EventHandler(this.frmUpdateCategories_Load);
            ((System.ComponentModel.ISupportInitialize)(this.categoryDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCategoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAccountBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subCategoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView categoryDataGrid;
        private System.Windows.Forms.BindingSource subCategoryBindingSource;
        private System.Windows.Forms.Label lblUpdateCategories;
        private System.Windows.Forms.BindingSource categoryBindingSource;
        private System.Windows.Forms.ComboBox cboCategories;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.BindingSource dataGridCategoryBindingSource;
        private System.Windows.Forms.BindingSource dataGridAccountBindingSource;
        private System.Windows.Forms.DataGridViewComboBoxColumn categoryKeyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubcategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubcategoryPrefix;
        private System.Windows.Forms.DataGridViewComboBoxColumn Account;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsAllocatable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActiveDataGridViewCheckBoxColumn;
    }
}