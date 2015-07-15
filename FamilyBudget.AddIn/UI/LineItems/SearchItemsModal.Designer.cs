namespace FamilyBudget.AddIn.UI
{
    partial class frmSearchItems
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.listSearchCriteria = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSearchField = new System.Windows.Forms.ComboBox();
            this.SearchFieldBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbComparators = new System.Windows.Forms.ComboBox();
            this.CompareOperatorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbQuarter = new System.Windows.Forms.ComboBox();
            this.QuarterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.lblOperator = new System.Windows.Forms.Label();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.MonthBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbDay = new System.Windows.Forms.ComboBox();
            this.DayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbDayOfWeek = new System.Windows.Forms.ComboBox();
            this.DayOfWeekBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.CategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbSubCategory = new System.Windows.Forms.ComboBox();
            this.SubCategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbType = new System.Windows.Forms.ComboBox();
            this.TypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbSubType = new System.Windows.Forms.ComboBox();
            this.SubTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.PaymentMethodBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.StatusBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtTextValue = new System.Windows.Forms.TextBox();
            this.txtMinAmount = new System.Windows.Forms.TextBox();
            this.txtMaxAmount = new System.Windows.Forms.TextBox();
            this.lblAmountAnd = new System.Windows.Forms.Label();
            this.dtpMinDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateAnd = new System.Windows.Forms.Label();
            this.dtpMaxDate = new System.Windows.Forms.DateTimePicker();
            this.panelDates = new System.Windows.Forms.Panel();
            this.panelAmount = new System.Windows.Forms.Panel();
            this.btnAddCriteria = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRemoveSelectedCriteria = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchFieldBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompareOperatorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuarterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DayBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DayOfWeekBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubCategoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentMethodBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBindingSource)).BeginInit();
            this.panelDates.SuspendLayout();
            this.panelAmount.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.listSearchCriteria);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 98);
            this.panel1.TabIndex = 0;
            // 
            // listSearchCriteria
            // 
            this.listSearchCriteria.FormattingEnabled = true;
            this.listSearchCriteria.Location = new System.Drawing.Point(7, 26);
            this.listSearchCriteria.Name = "listSearchCriteria";
            this.listSearchCriteria.Size = new System.Drawing.Size(531, 69);
            this.listSearchCriteria.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Criteria:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Add new criteria:";
            // 
            // cbSearchField
            // 
            this.cbSearchField.DataSource = this.SearchFieldBindingSource;
            this.cbSearchField.DisplayMember = "DisplayValue";
            this.cbSearchField.FormattingEnabled = true;
            this.cbSearchField.Location = new System.Drawing.Point(13, 195);
            this.cbSearchField.Name = "cbSearchField";
            this.cbSearchField.Size = new System.Drawing.Size(132, 21);
            this.cbSearchField.TabIndex = 3;
            this.cbSearchField.ValueMember = "ActualValue";
            this.cbSearchField.SelectedIndexChanged += new System.EventHandler(this.cbSearchField_SelectedIndexChanged);
            // 
            // SearchFieldBindingSource
            // 
            this.SearchFieldBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.EnumListMember);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Field:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(148, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Operator:";
            // 
            // cbComparators
            // 
            this.cbComparators.DataSource = this.CompareOperatorBindingSource;
            this.cbComparators.DisplayMember = "DisplayValue";
            this.cbComparators.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbComparators.FormattingEnabled = true;
            this.cbComparators.Location = new System.Drawing.Point(151, 195);
            this.cbComparators.Name = "cbComparators";
            this.cbComparators.Size = new System.Drawing.Size(96, 21);
            this.cbComparators.TabIndex = 6;
            this.cbComparators.ValueMember = "ActualValue";
            this.cbComparators.Visible = false;
            this.cbComparators.SelectedIndexChanged += new System.EventHandler(this.cbComparators_SelectedIndexChanged);
            // 
            // CompareOperatorBindingSource
            // 
            this.CompareOperatorBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.EnumListMember);
            // 
            // cbQuarter
            // 
            this.cbQuarter.DataSource = this.QuarterBindingSource;
            this.cbQuarter.DisplayMember = "DisplayValue";
            this.cbQuarter.FormattingEnabled = true;
            this.cbQuarter.Location = new System.Drawing.Point(253, 194);
            this.cbQuarter.Name = "cbQuarter";
            this.cbQuarter.Size = new System.Drawing.Size(201, 21);
            this.cbQuarter.TabIndex = 7;
            this.cbQuarter.ValueMember = "ActualValue";
            this.cbQuarter.Visible = false;
            // 
            // QuarterBindingSource
            // 
            this.QuarterBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.EnumListMember);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(250, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Value:";
            // 
            // lblOperator
            // 
            this.lblOperator.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperator.Location = new System.Drawing.Point(150, 195);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(97, 20);
            this.lblOperator.TabIndex = 9;
            this.lblOperator.Text = "=";
            this.lblOperator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbMonth
            // 
            this.cbMonth.DataSource = this.MonthBindingSource;
            this.cbMonth.DisplayMember = "DisplayValue";
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Location = new System.Drawing.Point(253, 221);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(201, 21);
            this.cbMonth.TabIndex = 10;
            this.cbMonth.ValueMember = "ActualValue";
            this.cbMonth.Visible = false;
            // 
            // MonthBindingSource
            // 
            this.MonthBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.EnumListMember);
            // 
            // cbDay
            // 
            this.cbDay.DataSource = this.DayBindingSource;
            this.cbDay.DisplayMember = "DisplayValue";
            this.cbDay.FormattingEnabled = true;
            this.cbDay.Location = new System.Drawing.Point(253, 248);
            this.cbDay.Name = "cbDay";
            this.cbDay.Size = new System.Drawing.Size(201, 21);
            this.cbDay.TabIndex = 11;
            this.cbDay.ValueMember = "ActualValue";
            this.cbDay.Visible = false;
            // 
            // DayBindingSource
            // 
            this.DayBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.EnumListMember);
            // 
            // cbDayOfWeek
            // 
            this.cbDayOfWeek.DataSource = this.DayOfWeekBindingSource;
            this.cbDayOfWeek.DisplayMember = "DisplayValue";
            this.cbDayOfWeek.FormattingEnabled = true;
            this.cbDayOfWeek.Location = new System.Drawing.Point(253, 275);
            this.cbDayOfWeek.Name = "cbDayOfWeek";
            this.cbDayOfWeek.Size = new System.Drawing.Size(201, 21);
            this.cbDayOfWeek.TabIndex = 12;
            this.cbDayOfWeek.ValueMember = "ActualValue";
            this.cbDayOfWeek.Visible = false;
            // 
            // DayOfWeekBindingSource
            // 
            this.DayOfWeekBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.EnumListMember);
            // 
            // cbCategory
            // 
            this.cbCategory.DataSource = this.CategoryBindingSource;
            this.cbCategory.DisplayMember = "CategoryName";
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(253, 302);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(201, 21);
            this.cbCategory.TabIndex = 13;
            this.cbCategory.ValueMember = "CategoryKey";
            this.cbCategory.Visible = false;
            // 
            // CategoryBindingSource
            // 
            this.CategoryBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.Category);
            // 
            // cbSubCategory
            // 
            this.cbSubCategory.DataSource = this.SubCategoryBindingSource;
            this.cbSubCategory.DisplayMember = "SubCategoryName";
            this.cbSubCategory.FormattingEnabled = true;
            this.cbSubCategory.Location = new System.Drawing.Point(253, 329);
            this.cbSubCategory.Name = "cbSubCategory";
            this.cbSubCategory.Size = new System.Drawing.Size(201, 21);
            this.cbSubCategory.TabIndex = 14;
            this.cbSubCategory.ValueMember = "SubCategoryKey";
            this.cbSubCategory.Visible = false;
            // 
            // SubCategoryBindingSource
            // 
            this.SubCategoryBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.Subcategory);
            // 
            // cbType
            // 
            this.cbType.DataSource = this.TypeBindingSource;
            this.cbType.DisplayMember = "DisplayValue";
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(273, 204);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(201, 21);
            this.cbType.TabIndex = 15;
            this.cbType.ValueMember = "ActualValue";
            this.cbType.Visible = false;
            // 
            // TypeBindingSource
            // 
            this.TypeBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.EnumListMember);
            // 
            // cbSubType
            // 
            this.cbSubType.DataSource = this.SubTypeBindingSource;
            this.cbSubType.DisplayMember = "DisplayValue";
            this.cbSubType.FormattingEnabled = true;
            this.cbSubType.Location = new System.Drawing.Point(273, 231);
            this.cbSubType.Name = "cbSubType";
            this.cbSubType.Size = new System.Drawing.Size(201, 21);
            this.cbSubType.TabIndex = 16;
            this.cbSubType.ValueMember = "ActualValue";
            this.cbSubType.Visible = false;
            // 
            // SubTypeBindingSource
            // 
            this.SubTypeBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.EnumListMember);
            // 
            // cbPaymentMethod
            // 
            this.cbPaymentMethod.DataSource = this.PaymentMethodBindingSource;
            this.cbPaymentMethod.DisplayMember = "PaymentMethodName";
            this.cbPaymentMethod.FormattingEnabled = true;
            this.cbPaymentMethod.Location = new System.Drawing.Point(273, 258);
            this.cbPaymentMethod.Name = "cbPaymentMethod";
            this.cbPaymentMethod.Size = new System.Drawing.Size(201, 21);
            this.cbPaymentMethod.TabIndex = 17;
            this.cbPaymentMethod.ValueMember = "PaymentMethodKey";
            this.cbPaymentMethod.Visible = false;
            // 
            // PaymentMethodBindingSource
            // 
            this.PaymentMethodBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.PaymentMethod);
            // 
            // cbStatus
            // 
            this.cbStatus.DataSource = this.StatusBindingSource;
            this.cbStatus.DisplayMember = "DisplayValue";
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(273, 285);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(201, 21);
            this.cbStatus.TabIndex = 18;
            this.cbStatus.ValueMember = "ActualValue";
            this.cbStatus.Visible = false;
            // 
            // StatusBindingSource
            // 
            this.StatusBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.EnumListMember);
            // 
            // txtTextValue
            // 
            this.txtTextValue.Location = new System.Drawing.Point(273, 312);
            this.txtTextValue.Name = "txtTextValue";
            this.txtTextValue.Size = new System.Drawing.Size(201, 20);
            this.txtTextValue.TabIndex = 19;
            this.txtTextValue.Visible = false;
            // 
            // txtMinAmount
            // 
            this.txtMinAmount.Location = new System.Drawing.Point(0, 0);
            this.txtMinAmount.Name = "txtMinAmount";
            this.txtMinAmount.Size = new System.Drawing.Size(67, 20);
            this.txtMinAmount.TabIndex = 20;
            // 
            // txtMaxAmount
            // 
            this.txtMaxAmount.Location = new System.Drawing.Point(135, 0);
            this.txtMaxAmount.Name = "txtMaxAmount";
            this.txtMaxAmount.Size = new System.Drawing.Size(67, 20);
            this.txtMaxAmount.TabIndex = 21;
            // 
            // lblAmountAnd
            // 
            this.lblAmountAnd.Location = new System.Drawing.Point(68, 0);
            this.lblAmountAnd.Name = "lblAmountAnd";
            this.lblAmountAnd.Size = new System.Drawing.Size(67, 16);
            this.lblAmountAnd.TabIndex = 22;
            this.lblAmountAnd.Text = "and";
            this.lblAmountAnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpMinDate
            // 
            this.dtpMinDate.Location = new System.Drawing.Point(0, 0);
            this.dtpMinDate.Name = "dtpMinDate";
            this.dtpMinDate.Size = new System.Drawing.Size(200, 20);
            this.dtpMinDate.TabIndex = 23;
            // 
            // lblDateAnd
            // 
            this.lblDateAnd.AutoSize = true;
            this.lblDateAnd.Location = new System.Drawing.Point(86, 25);
            this.lblDateAnd.Name = "lblDateAnd";
            this.lblDateAnd.Size = new System.Drawing.Size(25, 13);
            this.lblDateAnd.TabIndex = 24;
            this.lblDateAnd.Text = "and";
            // 
            // dtpMaxDate
            // 
            this.dtpMaxDate.Location = new System.Drawing.Point(0, 44);
            this.dtpMaxDate.Name = "dtpMaxDate";
            this.dtpMaxDate.Size = new System.Drawing.Size(200, 20);
            this.dtpMaxDate.TabIndex = 25;
            // 
            // panelDates
            // 
            this.panelDates.Controls.Add(this.dtpMaxDate);
            this.panelDates.Controls.Add(this.dtpMinDate);
            this.panelDates.Controls.Add(this.lblDateAnd);
            this.panelDates.Location = new System.Drawing.Point(289, 239);
            this.panelDates.Name = "panelDates";
            this.panelDates.Size = new System.Drawing.Size(201, 67);
            this.panelDates.TabIndex = 26;
            this.panelDates.Visible = false;
            // 
            // panelAmount
            // 
            this.panelAmount.Controls.Add(this.txtMaxAmount);
            this.panelAmount.Controls.Add(this.txtMinAmount);
            this.panelAmount.Controls.Add(this.lblAmountAnd);
            this.panelAmount.Location = new System.Drawing.Point(274, 339);
            this.panelAmount.Name = "panelAmount";
            this.panelAmount.Size = new System.Drawing.Size(201, 21);
            this.panelAmount.TabIndex = 27;
            this.panelAmount.Visible = false;
            // 
            // btnAddCriteria
            // 
            this.btnAddCriteria.Location = new System.Drawing.Point(460, 192);
            this.btnAddCriteria.Name = "btnAddCriteria";
            this.btnAddCriteria.Size = new System.Drawing.Size(88, 23);
            this.btnAddCriteria.TabIndex = 28;
            this.btnAddCriteria.Text = "Add Criteria";
            this.btnAddCriteria.UseVisualStyleBackColor = true;
            this.btnAddCriteria.Click += new System.EventHandler(this.btnAddCriteria_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(6, 248);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 29;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(87, 248);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 30;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRemoveSelectedCriteria
            // 
            this.btnRemoveSelectedCriteria.Location = new System.Drawing.Point(13, 114);
            this.btnRemoveSelectedCriteria.Name = "btnRemoveSelectedCriteria";
            this.btnRemoveSelectedCriteria.Size = new System.Drawing.Size(132, 23);
            this.btnRemoveSelectedCriteria.TabIndex = 31;
            this.btnRemoveSelectedCriteria.Text = "Remove Selected";
            this.btnRemoveSelectedCriteria.UseVisualStyleBackColor = true;
            this.btnRemoveSelectedCriteria.Click += new System.EventHandler(this.btnRemoveSelectedCriteria_Click);
            // 
            // frmSearchItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 280);
            this.Controls.Add(this.btnRemoveSelectedCriteria);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnAddCriteria);
            this.Controls.Add(this.panelAmount);
            this.Controls.Add(this.panelDates);
            this.Controls.Add(this.txtTextValue);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.cbPaymentMethod);
            this.Controls.Add(this.cbSubType);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.cbSubCategory);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.cbDayOfWeek);
            this.Controls.Add(this.cbDay);
            this.Controls.Add(this.cbMonth);
            this.Controls.Add(this.lblOperator);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbQuarter);
            this.Controls.Add(this.cbComparators);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbSearchField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSearchItems";
            this.Text = "Search";
            this.Load += new System.EventHandler(this.frmSearchItems_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchFieldBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompareOperatorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuarterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DayBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DayOfWeekBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubCategoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentMethodBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBindingSource)).EndInit();
            this.panelDates.ResumeLayout(false);
            this.panelDates.PerformLayout();
            this.panelAmount.ResumeLayout(false);
            this.panelAmount.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSearchField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbComparators;
        private System.Windows.Forms.ComboBox cbQuarter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.ComboBox cbDay;
        private System.Windows.Forms.ComboBox cbDayOfWeek;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.ComboBox cbSubCategory;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.ComboBox cbSubType;
        private System.Windows.Forms.ComboBox cbPaymentMethod;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.TextBox txtTextValue;
        private System.Windows.Forms.TextBox txtMinAmount;
        private System.Windows.Forms.TextBox txtMaxAmount;
        private System.Windows.Forms.Label lblAmountAnd;
        private System.Windows.Forms.DateTimePicker dtpMinDate;
        private System.Windows.Forms.Label lblDateAnd;
        private System.Windows.Forms.DateTimePicker dtpMaxDate;
        private System.Windows.Forms.Panel panelDates;
        private System.Windows.Forms.Panel panelAmount;
        private System.Windows.Forms.Button btnAddCriteria;
        private System.Windows.Forms.BindingSource SearchFieldBindingSource;
        private System.Windows.Forms.BindingSource QuarterBindingSource;
        private System.Windows.Forms.BindingSource MonthBindingSource;
        private System.Windows.Forms.BindingSource DayBindingSource;
        private System.Windows.Forms.BindingSource DayOfWeekBindingSource;
        private System.Windows.Forms.BindingSource CompareOperatorBindingSource;
        private System.Windows.Forms.BindingSource CategoryBindingSource;
        private System.Windows.Forms.BindingSource SubCategoryBindingSource;
        private System.Windows.Forms.BindingSource TypeBindingSource;
        private System.Windows.Forms.BindingSource SubTypeBindingSource;
        private System.Windows.Forms.BindingSource PaymentMethodBindingSource;
        private System.Windows.Forms.BindingSource StatusBindingSource;
        private System.Windows.Forms.ListBox listSearchCriteria;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRemoveSelectedCriteria;
    }
}