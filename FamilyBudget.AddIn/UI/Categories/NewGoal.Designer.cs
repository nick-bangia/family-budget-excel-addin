namespace FamilyBudget.AddIn.UI
{
    partial class frmNewGoal
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
            this.lblGoalName = new System.Windows.Forms.Label();
            this.lblGoalPrefix = new System.Windows.Forms.Label();
            this.txtGoalName = new System.Windows.Forms.TextBox();
            this.txtGoalPrefix = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.lblAccount = new System.Windows.Forms.Label();
            this.txtGoalAmount = new System.Windows.Forms.TextBox();
            this.lblGoalAmount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAccounts = new System.Windows.Forms.ComboBox();
            this.dtEstimatedCompletionDate = new System.Windows.Forms.DateTimePicker();
            this.lblEstimatedCompletionDate = new System.Windows.Forms.Label();
            this.cbParentCategory = new System.Windows.Forms.ComboBox();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.chkAllocatable = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblGoalName
            // 
            this.lblGoalName.AutoSize = true;
            this.lblGoalName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoalName.Location = new System.Drawing.Point(19, 61);
            this.lblGoalName.Name = "lblGoalName";
            this.lblGoalName.Size = new System.Drawing.Size(86, 19);
            this.lblGoalName.TabIndex = 0;
            this.lblGoalName.Text = "Goal Name:";
            // 
            // lblGoalPrefix
            // 
            this.lblGoalPrefix.AutoSize = true;
            this.lblGoalPrefix.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoalPrefix.Location = new System.Drawing.Point(19, 112);
            this.lblGoalPrefix.Name = "lblGoalPrefix";
            this.lblGoalPrefix.Size = new System.Drawing.Size(84, 19);
            this.lblGoalPrefix.TabIndex = 1;
            this.lblGoalPrefix.Text = "Goal Prefix:";
            // 
            // txtGoalName
            // 
            this.txtGoalName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGoalName.Location = new System.Drawing.Point(23, 83);
            this.txtGoalName.Name = "txtGoalName";
            this.txtGoalName.Size = new System.Drawing.Size(242, 26);
            this.txtGoalName.TabIndex = 4;
            // 
            // txtGoalPrefix
            // 
            this.txtGoalPrefix.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGoalPrefix.Location = new System.Drawing.Point(23, 134);
            this.txtGoalPrefix.Name = "txtGoalPrefix";
            this.txtGoalPrefix.Size = new System.Drawing.Size(242, 26);
            this.txtGoalPrefix.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(190, 372);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 34);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnabled.Location = new System.Drawing.Point(23, 328);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(82, 22);
            this.chkEnabled.TabIndex = 8;
            this.chkEnabled.Text = "Enabled?";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccount.Location = new System.Drawing.Point(21, 163);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(65, 19);
            this.lblAccount.TabIndex = 9;
            this.lblAccount.Text = "Account:";
            // 
            // txtGoalAmount
            // 
            this.txtGoalAmount.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGoalAmount.Location = new System.Drawing.Point(35, 240);
            this.txtGoalAmount.Name = "txtGoalAmount";
            this.txtGoalAmount.Size = new System.Drawing.Size(230, 26);
            this.txtGoalAmount.TabIndex = 7;
            // 
            // lblGoalAmount
            // 
            this.lblGoalAmount.AutoSize = true;
            this.lblGoalAmount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoalAmount.Location = new System.Drawing.Point(21, 215);
            this.lblGoalAmount.Name = "lblGoalAmount";
            this.lblGoalAmount.Size = new System.Drawing.Size(98, 19);
            this.lblGoalAmount.TabIndex = 12;
            this.lblGoalAmount.Text = "Goal Amount:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "$";
            // 
            // cbAccounts
            // 
            this.cbAccounts.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAccounts.FormattingEnabled = true;
            this.cbAccounts.Location = new System.Drawing.Point(23, 185);
            this.cbAccounts.Name = "cbAccounts";
            this.cbAccounts.Size = new System.Drawing.Size(242, 26);
            this.cbAccounts.TabIndex = 14;
            // 
            // dtEstimatedCompletionDate
            // 
            this.dtEstimatedCompletionDate.Location = new System.Drawing.Point(25, 297);
            this.dtEstimatedCompletionDate.Name = "dtEstimatedCompletionDate";
            this.dtEstimatedCompletionDate.Size = new System.Drawing.Size(240, 20);
            this.dtEstimatedCompletionDate.TabIndex = 15;
            // 
            // lblEstimatedCompletionDate
            // 
            this.lblEstimatedCompletionDate.AutoSize = true;
            this.lblEstimatedCompletionDate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstimatedCompletionDate.Location = new System.Drawing.Point(19, 272);
            this.lblEstimatedCompletionDate.Name = "lblEstimatedCompletionDate";
            this.lblEstimatedCompletionDate.Size = new System.Drawing.Size(157, 19);
            this.lblEstimatedCompletionDate.TabIndex = 16;
            this.lblEstimatedCompletionDate.Text = "Estimated Completion:";
            // 
            // cbParentCategory
            // 
            this.cbParentCategory.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbParentCategory.FormattingEnabled = true;
            this.cbParentCategory.Location = new System.Drawing.Point(23, 32);
            this.cbParentCategory.Name = "cbParentCategory";
            this.cbParentCategory.Size = new System.Drawing.Size(242, 26);
            this.cbParentCategory.TabIndex = 18;
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryName.Location = new System.Drawing.Point(19, 9);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(180, 19);
            this.lblCategoryName.TabIndex = 17;
            this.lblCategoryName.Text = "Choose a parent category:";
            // 
            // chkAllocatable
            // 
            this.chkAllocatable.AutoSize = true;
            this.chkAllocatable.Checked = true;
            this.chkAllocatable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllocatable.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllocatable.Location = new System.Drawing.Point(165, 328);
            this.chkAllocatable.Name = "chkAllocatable";
            this.chkAllocatable.Size = new System.Drawing.Size(100, 22);
            this.chkAllocatable.TabIndex = 19;
            this.chkAllocatable.Text = "Allocatable?";
            this.chkAllocatable.UseVisualStyleBackColor = true;
            // 
            // frmNewGoal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 419);
            this.Controls.Add(this.chkAllocatable);
            this.Controls.Add(this.cbParentCategory);
            this.Controls.Add(this.lblCategoryName);
            this.Controls.Add(this.lblEstimatedCompletionDate);
            this.Controls.Add(this.dtEstimatedCompletionDate);
            this.Controls.Add(this.cbAccounts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblGoalAmount);
            this.Controls.Add(this.txtGoalAmount);
            this.Controls.Add(this.lblAccount);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtGoalPrefix);
            this.Controls.Add(this.txtGoalName);
            this.Controls.Add(this.lblGoalPrefix);
            this.Controls.Add(this.lblGoalName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmNewGoal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add a New Savings Goal";
            this.Load += new System.EventHandler(this.frmNewGoal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGoalName;
        private System.Windows.Forms.Label lblGoalPrefix;
        private System.Windows.Forms.TextBox txtGoalName;
        private System.Windows.Forms.TextBox txtGoalPrefix;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.TextBox txtGoalAmount;
        private System.Windows.Forms.Label lblGoalAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAccounts;
        private System.Windows.Forms.DateTimePicker dtEstimatedCompletionDate;
        private System.Windows.Forms.Label lblEstimatedCompletionDate;
        private System.Windows.Forms.ComboBox cbParentCategory;
        private System.Windows.Forms.Label lblCategoryName;
        private System.Windows.Forms.CheckBox chkAllocatable;
    }
}