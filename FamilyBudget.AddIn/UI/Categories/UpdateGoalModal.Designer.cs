namespace FamilyBudget.AddIn.UI
{
    partial class frmUpdateGoals
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
            this.dataGridCategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridAccountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.goalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.categoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblUpdateCategories = new System.Windows.Forms.Label();
            this.cboCategories = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.categoryKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.GoalName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Account = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.GoalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstimatedCompletionDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsAllocatable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isActiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.categoryDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCategoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAccountBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.goalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // categoryDataGrid
            // 
            this.categoryDataGrid.AllowUserToAddRows = false;
            this.categoryDataGrid.AllowUserToDeleteRows = false;
            this.categoryDataGrid.AutoGenerateColumns = false;
            this.categoryDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
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
            this.categoryKeyDataGridViewTextBoxColumn,
            this.GoalName,
            this.Prefix,
            this.Account,
            this.GoalAmount,
            this.EstimatedCompletionDate,
            this.IsAllocatable,
            this.isActiveDataGridViewCheckBoxColumn});
            this.categoryDataGrid.DataSource = this.goalBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.categoryDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.categoryDataGrid.Location = new System.Drawing.Point(12, 102);
            this.categoryDataGrid.Name = "categoryDataGrid";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.categoryDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.categoryDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.categoryDataGrid.Size = new System.Drawing.Size(774, 176);
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
            // goalBindingSource
            // 
            this.goalBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.Goal);
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
            this.lblUpdateCategories.Size = new System.Drawing.Size(325, 19);
            this.lblUpdateCategories.TabIndex = 1;
            this.lblUpdateCategories.Text = "Choose a category for which to update goals for:";
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
            this.btnRefresh.Location = new System.Drawing.Point(762, 72);
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
            // GoalName
            // 
            this.GoalName.DataPropertyName = "Name";
            this.GoalName.HeaderText = "Name";
            this.GoalName.Name = "GoalName";
            this.GoalName.Width = 150;
            // 
            // Prefix
            // 
            this.Prefix.DataPropertyName = "Prefix";
            this.Prefix.HeaderText = "Prefix";
            this.Prefix.Name = "Prefix";
            this.Prefix.Width = 65;
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
            // GoalAmount
            // 
            this.GoalAmount.DataPropertyName = "GoalAmount";
            this.GoalAmount.HeaderText = "Amount";
            this.GoalAmount.Name = "GoalAmount";
            this.GoalAmount.Width = 75;
            // 
            // EstimatedCompletionDate
            // 
            this.EstimatedCompletionDate.DataPropertyName = "EstimatedCompletionDate";
            this.EstimatedCompletionDate.HeaderText = "Target Date";
            this.EstimatedCompletionDate.Name = "EstimatedCompletionDate";
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
            // frmUpdateGoals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 291);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cboCategories);
            this.Controls.Add(this.lblUpdateCategories);
            this.Controls.Add(this.categoryDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmUpdateGoals";
            this.Text = "Update Goals";
            this.Load += new System.EventHandler(this.frmUpdateGoals_Load);
            ((System.ComponentModel.ISupportInitialize)(this.categoryDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCategoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAccountBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.goalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView categoryDataGrid;
        private System.Windows.Forms.BindingSource goalBindingSource;
        private System.Windows.Forms.Label lblUpdateCategories;
        private System.Windows.Forms.BindingSource categoryBindingSource;
        private System.Windows.Forms.ComboBox cboCategories;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.BindingSource dataGridCategoryBindingSource;
        private System.Windows.Forms.BindingSource dataGridAccountBindingSource;
        private System.Windows.Forms.DataGridViewComboBoxColumn categoryKeyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoalName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prefix;
        private System.Windows.Forms.DataGridViewComboBoxColumn Account;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstimatedCompletionDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsAllocatable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActiveDataGridViewCheckBoxColumn;
    }
}