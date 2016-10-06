using FamilyBudget.AddIn.Controllers;
using FamilyBudget.Common.Domain;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FamilyBudget.AddIn.UI
{
    public partial class frmEnterJournalEntries : Form
    {
        #region Properties
        private BindingList<Subcategory> subcategories;
        private BindingList<JournalEntry> journalEntries;
        #endregion

        public frmEnterJournalEntries()
        {
            InitializeComponent();
        }

        private void frmEnterJournalEntries_Load(object sender, EventArgs e)
        {
            // get the list of subcategories and bind it to the combo boxes
            subcategories = CategoriesController.GetSubcategories(forceGet: false);
            fromSubcategoryBindingSource.DataSource = subcategories;
            toSubcategoryBindingSource.DataSource = subcategories;

            // initalize a new journal entries list and bind it to the list box
            journalEntries = new BindingList<JournalEntry>();
            lbJournalEntries.DataSource = journalEntries;
            lbJournalEntries.DisplayMember = "Description";
            lbJournalEntries.ValueMember = "Description";

        }

        private void btnResetForm_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnAddNewJournalEntry_Click(object sender, EventArgs e)
        {
            // create a new journal entry from the form and add it to the bound list
            if (ValidateEntries())
            {
                JournalEntry je = new JournalEntry()
                {
                    Amount = Decimal.Parse(txtAmount.Text),
                    FromSubcategory = subcategories[cbFromSubcategory.SelectedIndex],
                    ToSubcategory = subcategories[cbToSubcategory.SelectedIndex],
                    OnDate = dtOnDate.Value.Date,
                    Reason = txtReason.Text
                };

                journalEntries.Add(je);
            }
        }

        private void btnUpdateJournalEntry_Click(object sender, EventArgs e)
        {
            // if the form is valid, clone the current journal entry and modify it as necessary
            // then remove the old one and add in the updated entry
            if (ValidateEntries())
            {
                JournalEntry currentJE = journalEntries[lbJournalEntries.SelectedIndex];
                JournalEntry updatedJE = null;
                if (currentJE != null)
                {
                    updatedJE = currentJE.Clone() as JournalEntry;
                    updatedJE.Amount = Decimal.Parse(txtAmount.Text);
                    updatedJE.FromSubcategory =subcategories[cbFromSubcategory.SelectedIndex];
                    updatedJE.ToSubcategory = subcategories[cbToSubcategory.SelectedIndex];
                    updatedJE.OnDate = dtOnDate.Value.Date;
                    updatedJE.Reason = txtReason.Text;
                }

                journalEntries.RemoveAt(lbJournalEntries.SelectedIndex);
                journalEntries.Add(updatedJE);
            }
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            // if there are no items in the list, do nothing. Else, remove the selected item from the bound data source
            if (lbJournalEntries.Items.Count > 0)
            {
                journalEntries.RemoveAt(lbJournalEntries.SelectedIndex);
            }
        }

        private void lbJournalEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            // when the selected item in the list box changes, update the form with that items values
            JournalEntry currentJE = journalEntries[lbJournalEntries.SelectedIndex];

            txtAmount.Text = currentJE.Amount.ToString();
            cbFromSubcategory.SelectedIndex = subcategories.IndexOf(currentJE.FromSubcategory);
            cbToSubcategory.SelectedIndex = subcategories.IndexOf(currentJE.ToSubcategory);
            dtOnDate.Value = currentJE.OnDate;
            txtReason.Text = currentJE.Reason;
        }

        private void btnSaveJournalEntries_Click(object sender, EventArgs e)
        {
            // save the journal entries
            LineItemsController.SaveJournalEntries(journalEntries);

            // close the modal
            this.Close();
        }

        private bool ValidateEntries()
        {
            // validate the entries in the form before adding or updating them
            bool isValid = true;

            decimal amount;
            if (!Decimal.TryParse(txtAmount.Text, out amount))
            {
                isValid = false;
                MessageBox.Show("Invalid amount entered. Please enter a valid number.");
            }

            return isValid;
        }

        private void ResetForm()
        {
            // reset the form
            txtAmount.Text = String.Empty;
            cbFromSubcategory.SelectedIndex = 0;
            cbToSubcategory.SelectedIndex = 0;
            dtOnDate.Value = DateTime.Today;
            txtReason.Text = String.Empty;
        }
    }
}
