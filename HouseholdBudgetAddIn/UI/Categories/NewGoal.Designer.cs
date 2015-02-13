﻿namespace HouseholdBudget.UI
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.cbParentCategory = new System.Windows.Forms.ComboBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.lblAccountName = new System.Windows.Forms.Label();
            this.txtGoalAmount = new System.Windows.Forms.TextBox();
            this.lblGoalAmount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblGoalName
            // 
            this.lblGoalName.AutoSize = true;
            this.lblGoalName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoalName.Location = new System.Drawing.Point(12, 70);
            this.lblGoalName.Name = "lblGoalName";
            this.lblGoalName.Size = new System.Drawing.Size(85, 19);
            this.lblGoalName.TabIndex = 0;
            this.lblGoalName.Text = "Goal Name:";
            // 
            // lblGoalPrefix
            // 
            this.lblGoalPrefix.AutoSize = true;
            this.lblGoalPrefix.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoalPrefix.Location = new System.Drawing.Point(12, 121);
            this.lblGoalPrefix.Name = "lblGoalPrefix";
            this.lblGoalPrefix.Size = new System.Drawing.Size(83, 19);
            this.lblGoalPrefix.TabIndex = 1;
            this.lblGoalPrefix.Text = "Goal Prefix:";
            // 
            // txtGoalName
            // 
            this.txtGoalName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGoalName.Location = new System.Drawing.Point(16, 92);
            this.txtGoalName.Name = "txtGoalName";
            this.txtGoalName.Size = new System.Drawing.Size(211, 26);
            this.txtGoalName.TabIndex = 2;
            // 
            // txtGoalPrefix
            // 
            this.txtGoalPrefix.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGoalPrefix.Location = new System.Drawing.Point(16, 143);
            this.txtGoalPrefix.Name = "txtGoalPrefix";
            this.txtGoalPrefix.Size = new System.Drawing.Size(211, 26);
            this.txtGoalPrefix.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(71, 330);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 34);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(152, 330);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 35);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryName.Location = new System.Drawing.Point(12, 18);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(179, 19);
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
            this.cbParentCategory.TabIndex = 1;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnabled.Location = new System.Drawing.Point(16, 292);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(84, 22);
            this.chkEnabled.TabIndex = 6;
            this.chkEnabled.Text = "Enabled?";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // txtAccountName
            // 
            this.txtAccountName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountName.Location = new System.Drawing.Point(18, 194);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(211, 26);
            this.txtAccountName.TabIndex = 4;
            // 
            // lblAccountName
            // 
            this.lblAccountName.AutoSize = true;
            this.lblAccountName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountName.Location = new System.Drawing.Point(14, 172);
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Size = new System.Drawing.Size(107, 19);
            this.lblAccountName.TabIndex = 9;
            this.lblAccountName.Text = "Account Name:";
            // 
            // txtGoalAmount
            // 
            this.txtGoalAmount.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGoalAmount.Location = new System.Drawing.Point(28, 249);
            this.txtGoalAmount.Name = "txtGoalAmount";
            this.txtGoalAmount.Size = new System.Drawing.Size(199, 26);
            this.txtGoalAmount.TabIndex = 5;
            // 
            // lblGoalAmount
            // 
            this.lblGoalAmount.AutoSize = true;
            this.lblGoalAmount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoalAmount.Location = new System.Drawing.Point(14, 224);
            this.lblGoalAmount.Name = "lblGoalAmount";
            this.lblGoalAmount.Size = new System.Drawing.Size(97, 19);
            this.lblGoalAmount.TabIndex = 12;
            this.lblGoalAmount.Text = "Goal Amount:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "$";
            // 
            // frmNewGoal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 375);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblGoalAmount);
            this.Controls.Add(this.txtGoalAmount);
            this.Controls.Add(this.txtAccountName);
            this.Controls.Add(this.lblAccountName);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.cbParentCategory);
            this.Controls.Add(this.lblCategoryName);
            this.Controls.Add(this.btnCancel);
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
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCategoryName;
        private System.Windows.Forms.ComboBox cbParentCategory;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.TextBox txtAccountName;
        private System.Windows.Forms.Label lblAccountName;
        private System.Windows.Forms.TextBox txtGoalAmount;
        private System.Windows.Forms.Label lblGoalAmount;
        private System.Windows.Forms.Label label1;
    }
}