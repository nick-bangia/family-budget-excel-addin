namespace FamilyBudget.AddIn.UI
{
    partial class frmEnterJournalEntries
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
            this.lbJournalEntries = new System.Windows.Forms.ListBox();
            this.lblJournalEntryEntry = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.cbFromSubcategory = new System.Windows.Forms.ComboBox();
            this.fromSubcategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbToSubcategory = new System.Windows.Forms.ComboBox();
            this.toSubcategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dtOnDate = new System.Windows.Forms.DateTimePicker();
            this.btnAddNewJournalEntry = new System.Windows.Forms.Button();
            this.btnUpdateJournalEntry = new System.Windows.Forms.Button();
            this.btnSaveJournalEntries = new System.Windows.Forms.Button();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnResetForm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fromSubcategoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toSubcategoryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lbJournalEntries
            // 
            this.lbJournalEntries.FormattingEnabled = true;
            this.lbJournalEntries.Location = new System.Drawing.Point(12, 12);
            this.lbJournalEntries.Name = "lbJournalEntries";
            this.lbJournalEntries.Size = new System.Drawing.Size(679, 95);
            this.lbJournalEntries.TabIndex = 0;
            this.lbJournalEntries.SelectedIndexChanged += new System.EventHandler(this.lbJournalEntries_SelectedIndexChanged);
            // 
            // lblJournalEntryEntry
            // 
            this.lblJournalEntryEntry.AutoSize = true;
            this.lblJournalEntryEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJournalEntryEntry.Location = new System.Drawing.Point(12, 160);
            this.lblJournalEntryEntry.Name = "lblJournalEntryEntry";
            this.lblJournalEntryEntry.Size = new System.Drawing.Size(52, 16);
            this.lblJournalEntryEntry.TabIndex = 1;
            this.lblJournalEntryEntry.Text = "Move $";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(60, 160);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(81, 20);
            this.txtAmount.TabIndex = 2;
            // 
            // cbFromSubcategory
            // 
            this.cbFromSubcategory.DataSource = this.fromSubcategoryBindingSource;
            this.cbFromSubcategory.DisplayMember = "Name";
            this.cbFromSubcategory.FormattingEnabled = true;
            this.cbFromSubcategory.Location = new System.Drawing.Point(181, 160);
            this.cbFromSubcategory.Name = "cbFromSubcategory";
            this.cbFromSubcategory.Size = new System.Drawing.Size(121, 21);
            this.cbFromSubcategory.TabIndex = 3;
            this.cbFromSubcategory.ValueMember = "Key";
            // 
            // fromSubcategoryBindingSource
            // 
            this.fromSubcategoryBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.Subcategory);
            // 
            // cbToSubcategory
            // 
            this.cbToSubcategory.DataSource = this.toSubcategoryBindingSource;
            this.cbToSubcategory.DisplayMember = "Name";
            this.cbToSubcategory.FormattingEnabled = true;
            this.cbToSubcategory.Location = new System.Drawing.Point(335, 160);
            this.cbToSubcategory.Name = "cbToSubcategory";
            this.cbToSubcategory.Size = new System.Drawing.Size(121, 21);
            this.cbToSubcategory.TabIndex = 4;
            this.cbToSubcategory.ValueMember = "Key";
            // 
            // toSubcategoryBindingSource
            // 
            this.toSubcategoryBindingSource.DataSource = typeof(FamilyBudget.Common.Domain.Subcategory);
            // 
            // dtOnDate
            // 
            this.dtOnDate.Location = new System.Drawing.Point(491, 160);
            this.dtOnDate.Name = "dtOnDate";
            this.dtOnDate.Size = new System.Drawing.Size(200, 20);
            this.dtOnDate.TabIndex = 5;
            // 
            // btnAddNewJournalEntry
            // 
            this.btnAddNewJournalEntry.Location = new System.Drawing.Point(15, 230);
            this.btnAddNewJournalEntry.Name = "btnAddNewJournalEntry";
            this.btnAddNewJournalEntry.Size = new System.Drawing.Size(75, 23);
            this.btnAddNewJournalEntry.TabIndex = 6;
            this.btnAddNewJournalEntry.Text = "Add";
            this.btnAddNewJournalEntry.UseVisualStyleBackColor = true;
            this.btnAddNewJournalEntry.Click += new System.EventHandler(this.btnAddNewJournalEntry_Click);
            // 
            // btnUpdateJournalEntry
            // 
            this.btnUpdateJournalEntry.Location = new System.Drawing.Point(97, 230);
            this.btnUpdateJournalEntry.Name = "btnUpdateJournalEntry";
            this.btnUpdateJournalEntry.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateJournalEntry.TabIndex = 7;
            this.btnUpdateJournalEntry.Text = "Update";
            this.btnUpdateJournalEntry.UseVisualStyleBackColor = true;
            this.btnUpdateJournalEntry.Click += new System.EventHandler(this.btnUpdateJournalEntry_Click);
            // 
            // btnSaveJournalEntries
            // 
            this.btnSaveJournalEntries.Location = new System.Drawing.Point(616, 230);
            this.btnSaveJournalEntries.Name = "btnSaveJournalEntries";
            this.btnSaveJournalEntries.Size = new System.Drawing.Size(75, 23);
            this.btnSaveJournalEntries.TabIndex = 8;
            this.btnSaveJournalEntries.Text = "Save";
            this.btnSaveJournalEntries.UseVisualStyleBackColor = true;
            this.btnSaveJournalEntries.Click += new System.EventHandler(this.btnSaveJournalEntries_Click);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Location = new System.Drawing.Point(12, 114);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(112, 23);
            this.btnRemoveSelected.TabIndex = 9;
            this.btnRemoveSelected.Text = "Remove Selected";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // btnResetForm
            // 
            this.btnResetForm.Location = new System.Drawing.Point(178, 230);
            this.btnResetForm.Name = "btnResetForm";
            this.btnResetForm.Size = new System.Drawing.Size(82, 23);
            this.btnResetForm.TabIndex = 10;
            this.btnResetForm.Text = "Reset Form";
            this.btnResetForm.UseVisualStyleBackColor = true;
            this.btnResetForm.Click += new System.EventHandler(this.btnResetForm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(143, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "from";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(310, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "to";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(462, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "on";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(77, 187);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(614, 20);
            this.txtReason.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Reason:";
            // 
            // frmEnterJournalEntries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 262);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnResetForm);
            this.Controls.Add(this.btnRemoveSelected);
            this.Controls.Add(this.btnSaveJournalEntries);
            this.Controls.Add(this.btnUpdateJournalEntry);
            this.Controls.Add(this.btnAddNewJournalEntry);
            this.Controls.Add(this.dtOnDate);
            this.Controls.Add(this.cbToSubcategory);
            this.Controls.Add(this.cbFromSubcategory);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblJournalEntryEntry);
            this.Controls.Add(this.lbJournalEntries);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmEnterJournalEntries";
            this.Text = "Enter Journal Entries";
            this.Load += new System.EventHandler(this.frmEnterJournalEntries_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fromSubcategoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toSubcategoryBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbJournalEntries;
        private System.Windows.Forms.Label lblJournalEntryEntry;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.ComboBox cbFromSubcategory;
        private System.Windows.Forms.ComboBox cbToSubcategory;
        private System.Windows.Forms.DateTimePicker dtOnDate;
        private System.Windows.Forms.Button btnAddNewJournalEntry;
        private System.Windows.Forms.Button btnUpdateJournalEntry;
        private System.Windows.Forms.Button btnSaveJournalEntries;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.BindingSource fromSubcategoryBindingSource;
        private System.Windows.Forms.Button btnResetForm;
        private System.Windows.Forms.BindingSource toSubcategoryBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label label4;
    }
}