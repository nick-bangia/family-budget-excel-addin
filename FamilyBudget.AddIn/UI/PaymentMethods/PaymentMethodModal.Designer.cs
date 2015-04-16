namespace FamilyBudget.AddIn.UI
{
    partial class frmPaymentMethods
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
            this.PaymentMethodDataGrid = new System.Windows.Forms.DataGridView();
            this.paymentMethodNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PaymentMethodBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtPaymentMethod = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddNewPaymentMethod = new System.Windows.Forms.Button();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentMethodDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentMethodBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // PaymentMethodDataGrid
            // 
            this.PaymentMethodDataGrid.AllowUserToAddRows = false;
            this.PaymentMethodDataGrid.AllowUserToDeleteRows = false;
            this.PaymentMethodDataGrid.AutoGenerateColumns = false;
            this.PaymentMethodDataGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.PaymentMethodDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PaymentMethodDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PaymentMethodDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.paymentMethodNameDataGridViewTextBoxColumn,
            this.isActiveDataGridViewCheckBoxColumn});
            this.PaymentMethodDataGrid.DataSource = this.PaymentMethodBindingSource;
            this.PaymentMethodDataGrid.Location = new System.Drawing.Point(12, 43);
            this.PaymentMethodDataGrid.Name = "PaymentMethodDataGrid";
            this.PaymentMethodDataGrid.Size = new System.Drawing.Size(272, 155);
            this.PaymentMethodDataGrid.TabIndex = 0;
            // 
            // paymentMethodNameDataGridViewTextBoxColumn
            // 
            this.paymentMethodNameDataGridViewTextBoxColumn.DataPropertyName = "PaymentMethodName";
            this.paymentMethodNameDataGridViewTextBoxColumn.HeaderText = "Payment Method";
            this.paymentMethodNameDataGridViewTextBoxColumn.Name = "paymentMethodNameDataGridViewTextBoxColumn";
            this.paymentMethodNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // isActiveDataGridViewCheckBoxColumn
            // 
            this.isActiveDataGridViewCheckBoxColumn.DataPropertyName = "IsActive";
            this.isActiveDataGridViewCheckBoxColumn.HeaderText = "Enabled?";
            this.isActiveDataGridViewCheckBoxColumn.Name = "isActiveDataGridViewCheckBoxColumn";
            this.isActiveDataGridViewCheckBoxColumn.Width = 75;
            // 
            // PaymentMethodBindingSource
            // 
            this.PaymentMethodBindingSource.DataSource = typeof(FamilyBudget.Data.Domain.PaymentMethod);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Update current Payment Methods:";
            // 
            // txtPaymentMethod
            // 
            this.txtPaymentMethod.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaymentMethod.Location = new System.Drawing.Point(12, 245);
            this.txtPaymentMethod.Name = "txtPaymentMethod";
            this.txtPaymentMethod.Size = new System.Drawing.Size(187, 26);
            this.txtPaymentMethod.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Or, add a new method:";
            // 
            // btnAddNewPaymentMethod
            // 
            this.btnAddNewPaymentMethod.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewPaymentMethod.Location = new System.Drawing.Point(205, 245);
            this.btnAddNewPaymentMethod.Name = "btnAddNewPaymentMethod";
            this.btnAddNewPaymentMethod.Size = new System.Drawing.Size(75, 29);
            this.btnAddNewPaymentMethod.TabIndex = 4;
            this.btnAddNewPaymentMethod.Text = "Add";
            this.btnAddNewPaymentMethod.UseVisualStyleBackColor = true;
            this.btnAddNewPaymentMethod.Click += new System.EventHandler(this.btnAddNewPaymentMethod_Click);
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnabled.Location = new System.Drawing.Point(15, 277);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(84, 22);
            this.chkEnabled.TabIndex = 5;
            this.chkEnabled.Text = "Enabled?";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // frmPaymentMethods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 310);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.btnAddNewPaymentMethod);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPaymentMethod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PaymentMethodDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPaymentMethods";
            this.Text = "Payment Methods";
            this.Load += new System.EventHandler(this.PaymentMethodModal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PaymentMethodDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentMethodBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView PaymentMethodDataGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource PaymentMethodBindingSource;
        private System.Windows.Forms.TextBox txtPaymentMethod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddNewPaymentMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn paymentMethodNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActiveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.CheckBox chkEnabled;
    }
}