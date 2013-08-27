using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HouseholdBudget.UI
{
    public partial class ProgressModal : Form
    {
        public event EventHandler OnCancelBtnClicked;

        public ProgressModal()
        {
            InitializeComponent();
        }

        public void UpdateProgress(int percentComplete, string message)
        {
            ImportingLineItemsLabel.Visible = true;
            ImportingLineItemsLabel.Text = message;
            lblPercent.Text = percentComplete.ToString() + "%";
            ProgressBar.Step = percentComplete - ProgressBar.Value;
            ProgressBar.PerformStep();

            this.Refresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // if cancel is clicked, and the OnCancelBtnClicked event has been subscribed to,
            // fire the event
            if (OnCancelBtnClicked != null)
            {
                OnCancelBtnClicked(this, new EventArgs());
            }
        }                
    }
}
