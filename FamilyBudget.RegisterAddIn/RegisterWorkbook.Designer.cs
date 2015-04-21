namespace FamilyBudget.RegisterAddIn
{
    partial class frmRegisterWorkbook
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
            this.lblRegistrationText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtApiRootUrl = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblApiRootUrl = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnCompleteRegistration = new System.Windows.Forms.Button();
            this.lblValidationText = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblWorkbookPath = new System.Windows.Forms.Label();
            this.txtWorkbookPath = new System.Windows.Forms.TextBox();
            this.dialogSelectWorkbookPath = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // lblRegistrationText
            // 
            this.lblRegistrationText.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistrationText.Location = new System.Drawing.Point(13, 7);
            this.lblRegistrationText.Name = "lblRegistrationText";
            this.lblRegistrationText.Size = new System.Drawing.Size(327, 54);
            this.lblRegistrationText.TabIndex = 0;
            this.lblRegistrationText.Text = "Welcome to the Family Budget Registration Wizard!";
            this.lblRegistrationText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(326, 42);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please fill in the following fields to complete registration.";
            // 
            // txtApiRootUrl
            // 
            this.txtApiRootUrl.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApiRootUrl.Location = new System.Drawing.Point(109, 169);
            this.txtApiRootUrl.Name = "txtApiRootUrl";
            this.txtApiRootUrl.Size = new System.Drawing.Size(231, 26);
            this.txtApiRootUrl.TabIndex = 2;
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(109, 201);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(231, 26);
            this.txtUsername.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(109, 233);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(231, 26);
            this.txtPassword.TabIndex = 4;
            // 
            // lblApiRootUrl
            // 
            this.lblApiRootUrl.AutoSize = true;
            this.lblApiRootUrl.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApiRootUrl.Location = new System.Drawing.Point(12, 172);
            this.lblApiRootUrl.Name = "lblApiRootUrl";
            this.lblApiRootUrl.Size = new System.Drawing.Size(91, 18);
            this.lblApiRootUrl.TabIndex = 5;
            this.lblApiRootUrl.Text = "API Root URL:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(14, 204);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(75, 18);
            this.lblUsername.TabIndex = 6;
            this.lblUsername.Text = "Username:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(14, 236);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(71, 18);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password:";
            // 
            // btnCompleteRegistration
            // 
            this.btnCompleteRegistration.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompleteRegistration.Location = new System.Drawing.Point(53, 317);
            this.btnCompleteRegistration.Name = "btnCompleteRegistration";
            this.btnCompleteRegistration.Size = new System.Drawing.Size(158, 33);
            this.btnCompleteRegistration.TabIndex = 8;
            this.btnCompleteRegistration.Text = "Complete Registration";
            this.btnCompleteRegistration.UseVisualStyleBackColor = true;
            this.btnCompleteRegistration.Click += new System.EventHandler(this.btnCompleteRegistration_Click);
            // 
            // lblValidationText
            // 
            this.lblValidationText.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValidationText.ForeColor = System.Drawing.Color.Red;
            this.lblValidationText.Location = new System.Drawing.Point(17, 269);
            this.lblValidationText.Name = "lblValidationText";
            this.lblValidationText.Size = new System.Drawing.Size(323, 41);
            this.lblValidationText.TabIndex = 9;
            this.lblValidationText.Text = "Some fields are not valid. Check your entries and try again.";
            this.lblValidationText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblValidationText.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(217, 317);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 33);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblWorkbookPath
            // 
            this.lblWorkbookPath.AutoSize = true;
            this.lblWorkbookPath.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkbookPath.Location = new System.Drawing.Point(14, 140);
            this.lblWorkbookPath.Name = "lblWorkbookPath";
            this.lblWorkbookPath.Size = new System.Drawing.Size(75, 18);
            this.lblWorkbookPath.TabIndex = 11;
            this.lblWorkbookPath.Text = "Workbook:";
            // 
            // txtWorkbookPath
            // 
            this.txtWorkbookPath.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWorkbookPath.Location = new System.Drawing.Point(109, 137);
            this.txtWorkbookPath.Name = "txtWorkbookPath";
            this.txtWorkbookPath.Size = new System.Drawing.Size(231, 26);
            this.txtWorkbookPath.TabIndex = 12;
            this.txtWorkbookPath.Enter += new System.EventHandler(this.txtWorkbookPath_Enter);
            // 
            // dialogSelectWorkbookPath
            // 
            this.dialogSelectWorkbookPath.Filter = "Excel Workbooks|*.xlsx|Excel Macro-Enabled Workbooks|*.xlsxm";
            this.dialogSelectWorkbookPath.Title = "Select your Budget Workbook";
            // 
            // frmRegisterWorkbook
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(352, 362);
            this.Controls.Add(this.txtWorkbookPath);
            this.Controls.Add(this.lblWorkbookPath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblValidationText);
            this.Controls.Add(this.btnCompleteRegistration);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblApiRootUrl);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtApiRootUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblRegistrationText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmRegisterWorkbook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Workbook Registration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRegistrationText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtApiRootUrl;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblApiRootUrl;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnCompleteRegistration;
        private System.Windows.Forms.Label lblValidationText;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblWorkbookPath;
        private System.Windows.Forms.TextBox txtWorkbookPath;
        private System.Windows.Forms.OpenFileDialog dialogSelectWorkbookPath;
    }
}