namespace FamilyBudget.AddIn.UI
{
    partial class frmItem
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
            this.dtpTxDate = new System.Windows.Forms.DateTimePicker();
            this.lblTxDate = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblSubcategory = new System.Windows.Forms.Label();
            this.lblTxAmount = new System.Windows.Forms.Label();
            this.lblTxType = new System.Windows.Forms.Label();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.categoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbSubcategory = new System.Windows.Forms.ComboBox();
            this.subcategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbTxType = new System.Windows.Forms.ComboBox();
            this.typeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.paymentMethodBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.statusBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtTxAmount = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblTaxDeductible = new System.Windows.Forms.Label();
            this.chkTaxDeductible = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.categoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subcategoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paymentMethodBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpTxDate
            // 
            this.dtpTxDate.Location = new System.Drawing.Point(182, 7);
            this.dtpTxDate.Name = "dtpTxDate";
            this.dtpTxDate.Size = new System.Drawing.Size(200, 20);
            this.dtpTxDate.TabIndex = 0;
            // 
            // lblTxDate
            // 
            this.lblTxDate.AutoSize = true;
            this.lblTxDate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTxDate.Location = new System.Drawing.Point(12, 9);
            this.lblTxDate.Name = "lblTxDate";
            this.lblTxDate.Size = new System.Drawing.Size(123, 19);
            this.lblTxDate.TabIndex = 10;
            this.lblTxDate.Text = "Transaction Date:";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(12, 43);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(72, 19);
            this.lblCategory.TabIndex = 11;
            this.lblCategory.Text = "Category:";
            // 
            // lblSubcategory
            // 
            this.lblSubcategory.AutoSize = true;
            this.lblSubcategory.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubcategory.Location = new System.Drawing.Point(12, 80);
            this.lblSubcategory.Name = "lblSubcategory";
            this.lblSubcategory.Size = new System.Drawing.Size(93, 19);
            this.lblSubcategory.TabIndex = 12;
            this.lblSubcategory.Text = "Subcategory:";
            // 
            // lblTxAmount
            // 
            this.lblTxAmount.AutoSize = true;
            this.lblTxAmount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTxAmount.Location = new System.Drawing.Point(12, 147);
            this.lblTxAmount.Name = "lblTxAmount";
            this.lblTxAmount.Size = new System.Drawing.Size(143, 19);
            this.lblTxAmount.TabIndex = 13;
            this.lblTxAmount.Text = "Transaction Amount:";
            // 
            // lblTxType
            // 
            this.lblTxType.AutoSize = true;
            this.lblTxType.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTxType.Location = new System.Drawing.Point(12, 183);
            this.lblTxType.Name = "lblTxType";
            this.lblTxType.Size = new System.Drawing.Size(122, 19);
            this.lblTxType.TabIndex = 14;
            this.lblTxType.Text = "Transaction Type:";
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentMethod.Location = new System.Drawing.Point(12, 219);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(125, 19);
            this.lblPaymentMethod.TabIndex = 15;
            this.lblPaymentMethod.Text = "Payment Method:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(12, 254);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 19);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "Status:";
            // 
            // cbCategory
            // 
            this.cbCategory.DataSource = this.categoryBindingSource;
            this.cbCategory.DisplayMember = "CategoryName";
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(182, 41);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(200, 21);
            this.cbCategory.TabIndex = 1;
            this.cbCategory.ValueMember = "Key";
            this.cbCategory.SelectedIndexChanged += new System.EventHandler(this.cbCategory_SelectedIndexChanged);
            // 
            // categoryBindingSource
            // 
            this.categoryBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.Category);
            // 
            // cbSubcategory
            // 
            this.cbSubcategory.DataSource = this.subcategoryBindingSource;
            this.cbSubcategory.DisplayMember = "Name";
            this.cbSubcategory.FormattingEnabled = true;
            this.cbSubcategory.Location = new System.Drawing.Point(182, 78);
            this.cbSubcategory.Name = "cbSubcategory";
            this.cbSubcategory.Size = new System.Drawing.Size(200, 21);
            this.cbSubcategory.TabIndex = 2;
            this.cbSubcategory.ValueMember = "Key";
            // 
            // subcategoryBindingSource
            // 
            this.subcategoryBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.Subcategory);
            // 
            // cbTxType
            // 
            this.cbTxType.DataSource = this.typeBindingSource;
            this.cbTxType.DisplayMember = "DisplayValue";
            this.cbTxType.FormattingEnabled = true;
            this.cbTxType.Location = new System.Drawing.Point(182, 181);
            this.cbTxType.Name = "cbTxType";
            this.cbTxType.Size = new System.Drawing.Size(200, 21);
            this.cbTxType.TabIndex = 4;
            this.cbTxType.ValueMember = "ActualValue";
            // 
            // typeBindingSource
            // 
            this.typeBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.EnumListMember);
            // 
            // cbPaymentMethod
            // 
            this.cbPaymentMethod.DataSource = this.paymentMethodBindingSource;
            this.cbPaymentMethod.DisplayMember = "PaymentMethodName";
            this.cbPaymentMethod.FormattingEnabled = true;
            this.cbPaymentMethod.Location = new System.Drawing.Point(182, 219);
            this.cbPaymentMethod.Name = "cbPaymentMethod";
            this.cbPaymentMethod.Size = new System.Drawing.Size(200, 21);
            this.cbPaymentMethod.TabIndex = 5;
            this.cbPaymentMethod.ValueMember = "Key";
            // 
            // paymentMethodBindingSource
            // 
            this.paymentMethodBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.PaymentMethod);
            // 
            // cbStatus
            // 
            this.cbStatus.DataSource = this.statusBindingSource;
            this.cbStatus.DisplayMember = "DisplayValue";
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(182, 252);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(200, 21);
            this.cbStatus.TabIndex = 6;
            this.cbStatus.ValueMember = "ActualValue";
            // 
            // statusBindingSource
            // 
            this.statusBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.EnumListMember);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(16, 328);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(98, 328);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(307, 328);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtTxAmount
            // 
            this.txtTxAmount.Location = new System.Drawing.Point(182, 147);
            this.txtTxAmount.Name = "txtTxAmount";
            this.txtTxAmount.Size = new System.Drawing.Size(200, 20);
            this.txtTxAmount.TabIndex = 17;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(182, 113);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(200, 20);
            this.txtDescription.TabIndex = 18;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(12, 114);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(87, 19);
            this.lblDescription.TabIndex = 19;
            this.lblDescription.Text = "Description:";
            // 
            // lblTaxDeductible
            // 
            this.lblTaxDeductible.AutoSize = true;
            this.lblTaxDeductible.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxDeductible.Location = new System.Drawing.Point(12, 286);
            this.lblTaxDeductible.Name = "lblTaxDeductible";
            this.lblTaxDeductible.Size = new System.Drawing.Size(112, 19);
            this.lblTaxDeductible.TabIndex = 20;
            this.lblTaxDeductible.Text = "Tax Deductible?";
            // 
            // chkTaxDeductible
            // 
            this.chkTaxDeductible.AutoSize = true;
            this.chkTaxDeductible.Location = new System.Drawing.Point(182, 287);
            this.chkTaxDeductible.Name = "chkTaxDeductible";
            this.chkTaxDeductible.Size = new System.Drawing.Size(15, 14);
            this.chkTaxDeductible.TabIndex = 21;
            this.chkTaxDeductible.UseVisualStyleBackColor = true;
            // 
            // frmItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 363);
            this.Controls.Add(this.chkTaxDeductible);
            this.Controls.Add(this.lblTaxDeductible);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtTxAmount);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.cbPaymentMethod);
            this.Controls.Add(this.cbTxType);
            this.Controls.Add(this.cbSubcategory);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblPaymentMethod);
            this.Controls.Add(this.lblTxType);
            this.Controls.Add(this.lblTxAmount);
            this.Controls.Add(this.lblSubcategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblTxDate);
            this.Controls.Add(this.dtpTxDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Line Item";
            this.Load += new System.EventHandler(this.frmItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.categoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subcategoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paymentMethodBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpTxDate;
        private System.Windows.Forms.Label lblTxDate;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblSubcategory;
        private System.Windows.Forms.Label lblTxAmount;
        private System.Windows.Forms.Label lblTxType;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.ComboBox cbSubcategory;
        private System.Windows.Forms.ComboBox cbTxType;
        private System.Windows.Forms.ComboBox cbPaymentMethod;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.BindingSource categoryBindingSource;
        private System.Windows.Forms.BindingSource subcategoryBindingSource;
        private System.Windows.Forms.BindingSource paymentMethodBindingSource;
        private System.Windows.Forms.TextBox txtTxAmount;
        private System.Windows.Forms.BindingSource typeBindingSource;
        private System.Windows.Forms.BindingSource statusBindingSource;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblTaxDeductible;
        private System.Windows.Forms.CheckBox chkTaxDeductible;
    }
}