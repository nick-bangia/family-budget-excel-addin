namespace FamilyBudget.AddIn.UI
{
    partial class frmNewSubcategory
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
            this.lblSubCategoryName = new System.Windows.Forms.Label();
            this.lblSubCategoryPrefix = new System.Windows.Forms.Label();
            this.txtSubCategoryName = new System.Windows.Forms.TextBox();
            this.txtSubCategoryPrefix = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.cbParentCategory = new System.Windows.Forms.ComboBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAccount = new System.Windows.Forms.ComboBox();
            this.chkAllocatable = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblSubCategoryName
            // 
            this.lblSubCategoryName.AutoSize = true;
            this.lblSubCategoryName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubCategoryName.Location = new System.Drawing.Point(12, 70);
            this.lblSubCategoryName.Name = "lblSubCategoryName";
            this.lblSubCategoryName.Size = new System.Drawing.Size(136, 19);
            this.lblSubCategoryName.TabIndex = 0;
            this.lblSubCategoryName.Text = "Subcategory Name:";
            // 
            // lblSubCategoryPrefix
            // 
            this.lblSubCategoryPrefix.AutoSize = true;
            this.lblSubCategoryPrefix.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubCategoryPrefix.Location = new System.Drawing.Point(12, 121);
            this.lblSubCategoryPrefix.Name = "lblSubCategoryPrefix";
            this.lblSubCategoryPrefix.Size = new System.Drawing.Size(134, 19);
            this.lblSubCategoryPrefix.TabIndex = 1;
            this.lblSubCategoryPrefix.Text = "Subcategory Prefix:";
            // 
            // txtSubCategoryName
            // 
            this.txtSubCategoryName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubCategoryName.Location = new System.Drawing.Point(16, 92);
            this.txtSubCategoryName.Name = "txtSubCategoryName";
            this.txtSubCategoryName.Size = new System.Drawing.Size(211, 26);
            this.txtSubCategoryName.TabIndex = 2;
            // 
            // txtSubCategoryPrefix
            // 
            this.txtSubCategoryPrefix.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubCategoryPrefix.Location = new System.Drawing.Point(16, 143);
            this.txtSubCategoryPrefix.Name = "txtSubCategoryPrefix";
            this.txtSubCategoryPrefix.Size = new System.Drawing.Size(211, 26);
            this.txtSubCategoryPrefix.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(146, 282);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 34);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryName.Location = new System.Drawing.Point(12, 18);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(180, 19);
            this.lblCategoryName.TabIndex = 6;
            this.lblCategoryName.Text = "Choose a parent category:";
            // 
            // cbParentCategory
            // 
            this.cbParentCategory.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbParentCategory.FormattingEnabled = true;
            this.cbParentCategory.Location = new System.Drawing.Point(16, 41);
            this.cbParentCategory.Name = "cbParentCategory";
            this.cbParentCategory.Size = new System.Drawing.Size(211, 26);
            this.cbParentCategory.TabIndex = 7;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnabled.Location = new System.Drawing.Point(16, 239);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(82, 22);
            this.chkEnabled.TabIndex = 8;
            this.chkEnabled.Text = "Enabled?";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "Account:";
            // 
            // cbAccount
            // 
            this.cbAccount.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAccount.FormattingEnabled = true;
            this.cbAccount.Location = new System.Drawing.Point(16, 194);
            this.cbAccount.Name = "cbAccount";
            this.cbAccount.Size = new System.Drawing.Size(211, 26);
            this.cbAccount.TabIndex = 10;
            // 
            // chkAllocatable
            // 
            this.chkAllocatable.AutoSize = true;
            this.chkAllocatable.Checked = true;
            this.chkAllocatable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllocatable.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllocatable.Location = new System.Drawing.Point(127, 239);
            this.chkAllocatable.Name = "chkAllocatable";
            this.chkAllocatable.Size = new System.Drawing.Size(100, 22);
            this.chkAllocatable.TabIndex = 11;
            this.chkAllocatable.Text = "Allocatable?";
            this.chkAllocatable.UseVisualStyleBackColor = true;
            // 
            // frmNewSubcategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 327);
            this.Controls.Add(this.chkAllocatable);
            this.Controls.Add(this.cbAccount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.cbParentCategory);
            this.Controls.Add(this.lblCategoryName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtSubCategoryPrefix);
            this.Controls.Add(this.txtSubCategoryName);
            this.Controls.Add(this.lblSubCategoryPrefix);
            this.Controls.Add(this.lblSubCategoryName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmNewSubcategory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add a New Budget Subcategory";
            this.Load += new System.EventHandler(this.frmNewSubcategory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSubCategoryName;
        private System.Windows.Forms.Label lblSubCategoryPrefix;
        private System.Windows.Forms.TextBox txtSubCategoryName;
        private System.Windows.Forms.TextBox txtSubCategoryPrefix;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblCategoryName;
        private System.Windows.Forms.ComboBox cbParentCategory;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAccount;
        private System.Windows.Forms.CheckBox chkAllocatable;
    }
}