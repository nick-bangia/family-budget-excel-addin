using System;
using System.Windows.Forms;
using FamilyBudget.Common.Config;

namespace FamilyBudget.RegisterAddIn
{
    public partial class frmRegisterWorkbook : Form
    {
        public frmRegisterWorkbook()
        {
            InitializeComponent();
        }

        private void btnCompleteRegistration_Click(object sender, System.EventArgs e)
        {
            if (ValidateEntries())
            {
                // Register the workbook if it is valid
                RegisterWorkbook(txtWorkbookPath.Text, txtApiRootUrl.Text, txtUsername.Text, txtPassword.Text);

                // exit the application once registration has completed
                Application.Exit();
            }
        }

        private void txtWorkbookPath_Enter(object sender, EventArgs e)
        {
            // when the txtWorkbookPath text box gets focus, open a file dialog to choose the workbook
            DialogResult dialogResult = dialogSelectWorkbookPath.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                txtWorkbookPath.Text = dialogSelectWorkbookPath.FileName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // close the registration dialog
            Application.Exit();
        }

        private bool ValidateEntries()
        {
            bool isValid = true;

            Uri apiRootUri;
            // check if the Api Root Uri is valid
            isValid = isValid && 
                Uri.TryCreate(txtApiRootUrl.Text, UriKind.Absolute, out apiRootUri) &&
                (apiRootUri.Scheme == Uri.UriSchemeHttp || apiRootUri.Scheme == Uri.UriSchemeHttps);

            // check if a workbook has been selected
            isValid = isValid &&
                !String.IsNullOrWhiteSpace(txtWorkbookPath.Text) && (txtWorkbookPath.Text.EndsWith(".xlsx") || txtWorkbookPath.Text.EndsWith(".xlsm"));

            // check if the username & password are entered
            isValid = isValid && 
                !String.IsNullOrWhiteSpace(txtUsername.Text) && !String.IsNullOrWhiteSpace(txtPassword.Text);
            
            // set the validation text
            lblValidationText.Visible = !isValid;
            
            // return the validity
            return isValid;
        }

        private void RegisterWorkbook(string workbookPath, string apiRootUrl, string username, string password)
        {
            // Set the configured values
            AddInConfiguration.RegisteredWorkbookPath = workbookPath;
            AddInConfiguration.APIConfiguration.RootUri = apiRootUrl;
            AddInConfiguration.APIConfiguration.Username = username;
            AddInConfiguration.APIConfiguration.Password = password;
        }
    }
}
