using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HouseholdBudget.Events;

namespace HouseholdBudget.UI
{
    internal partial class frmNewCategory : Form
    {
        internal event EventHandler<CategoryEventArgs> CategorySaved;
        internal event EventHandler UserCancelled;

        internal frmNewCategory()
        {
            InitializeComponent();
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // if there is at least one subscriber to the saved event, fire the event
            // else, report error
            if (CategorySaved != null)
            {
                CategoryEventArgs args = new CategoryEventArgs()
                {
                    CategoryName = txtCategoryName.Text,
                    SubCategoryName = txtSubCategoryName.Text,
                    SubCategoryPrefix = txtSubCategoryPrefix.Text
                };

                // fire saved event
                CategorySaved(this, args);
            }
            else
            {
                // report error and close the form
                MessageBox.Show("There is no event handler tied to the save event!");
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // if there is at least one subscriber, fire the event, otherwise, report error
            if (UserCancelled != null)
            {
                UserCancelled(this, new EventArgs());
            }
            else
            {
                // report error and close the form
                MessageBox.Show("There is no event handler tied to the cancel event!");
                this.Close();
            }
        }
    }
}
