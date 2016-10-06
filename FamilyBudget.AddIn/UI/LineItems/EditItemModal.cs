using System;
using System.ComponentModel;
using System.Windows.Forms;
using FamilyBudget.AddIn.Controllers;
using FamilyBudget.AddIn.DataControllers;
using FamilyBudget.AddIn.Enums;
using FamilyBudget.AddIn.Utilities;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Enums;
using FamilyBudget.Common.Utilities;

namespace FamilyBudget.AddIn.UI
{
    public partial class frmItem : Form
    {

        #region Properties
        private BindingList<Category> categoryDataObject;
        private BindingList<Subcategory> subCategoryDataObject;
        private BindingList<PaymentMethod> paymentMethodDataObject;
        private DenormalizedLineItem originalLineItem;
        private int listObjectIndex;
        private int lineItemIndex;
        private DataWorksheetType worksheetType;
        #endregion

        #region Constructors
        public frmItem()
        {
            InitializeComponent();
        }

        public frmItem(int listObjectIndex, int lineItemIndex, DataWorksheetType worksheetType)
        {
            InitializeComponent();

            this.listObjectIndex = listObjectIndex;
            this.lineItemIndex = lineItemIndex;
            this.worksheetType = worksheetType;
        }
        #endregion

        #region Event Handlers
        private void frmItem_Load(object sender, EventArgs e)
        {
            // populate category combo box
            categoryDataObject = CategoriesController.GetCategories(false);
            categoryBindingSource.DataSource = categoryDataObject;

            // populate payment method combo box
            paymentMethodDataObject = PaymentMethodsController.GetPaymentMethods();
            paymentMethodBindingSource.DataSource = paymentMethodDataObject;

            // populate transaction type combo box
            typeBindingSource.DataSource = EnumUtil.GetEnumMemberArray(typeof(LineItemType));

            // populate status combo box
            statusBindingSource.DataSource = EnumUtil.GetEnumMemberArray(typeof(LineItemStatus));
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // reset the form to its original values
            HydrateForm(originalLineItem);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // confirm from user that delete is desired
            DialogResult choice = MessageBox.Show("Are you sure you want to delete this item? This action is irreversible.",
                                                  "Confirm Deletion",
                                                  MessageBoxButtons.YesNo);

            if (choice == DialogResult.Yes)
            {
                if (String.IsNullOrWhiteSpace(originalLineItem.Key))
                {
                    WorksheetDataController.RemoveLineItem(listObjectIndex, lineItemIndex, worksheetType);
                }
                else
                {
                    // if delete is confirmed, do it, and refresh the data
                    LineItemsController.DeleteLineItem(originalLineItem.Key);
                    WorksheetDataController.RemoveLineItem(listObjectIndex, lineItemIndex, worksheetType);
                    LineItemsController.PopulateDataSheet(rebuild: true);
                    WorkbookUtil.RefreshPivotTables();
                }
                
                // close the form
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // update the denormalized line item with updated values
            originalLineItem.Year = dtpTxDate.Value.Year;
            originalLineItem.MonthInt = (short)dtpTxDate.Value.Month;
            originalLineItem.Day = (short)dtpTxDate.Value.Day;
            // to comply with ODBC standard for Days Of Week, add 1 to the System.DayOfWeek Enumeration
            originalLineItem.DayOfWeekId = (short)(((short)dtpTxDate.Value.DayOfWeek) + 1);
            originalLineItem.Quarter = DateUtil.GetQuarterForMonth(dtpTxDate.Value.Month);
            originalLineItem.Category = cbCategory.Text;
            originalLineItem.CategoryKey = (string)cbCategory.SelectedValue;
            originalLineItem.SubCategory = cbSubcategory.Text;
            originalLineItem.SubCategoryKey = (string)cbSubcategory.SelectedValue;
            originalLineItem.Description = txtDescription.Text;
            originalLineItem.Amount = Decimal.Parse(txtTxAmount.Text);
            originalLineItem.IsTaxDeductible = chkTaxDeductible.Checked;
            originalLineItem.Type = (LineItemType)cbTxType.SelectedValue;
            originalLineItem.PaymentMethod = cbPaymentMethod.Text;
            originalLineItem.PaymentMethodKey = (string)cbPaymentMethod.SelectedValue;
            originalLineItem.Status = (LineItemStatus)cbStatus.SelectedValue;

            Guid uniqueKey = Guid.Empty;
            if (Guid.TryParse(originalLineItem.Key, out uniqueKey))
            {
                // if the uniqueKey field is a valid Guid, then update the item via the API
                originalLineItem = LineItemsController.UpdateLineItem(originalLineItem);
            }
            else
            {
                // otherwise, add it
                originalLineItem = LineItemsController.AddNewLineItem(originalLineItem);
            }

            // update the line item in the data worksheet
            WorksheetDataController.UpdateLineItem(this.listObjectIndex, this.lineItemIndex, this.worksheetType, originalLineItem);

            // refresh the data sheet & pivot tables if the APIState of the lineItem is OK
            if (originalLineItem.APIState.Contains("success"))
            {
                LineItemsController.PopulateDataSheet(rebuild: true);
                WorkbookUtil.RefreshPivotTables();
                WorkbookUtil.ShowWorksheetByName(Properties.Resources.DataWorksheetName + EnumUtil.GetFriendlyName(worksheetType));
            }

            // close the form
            this.Close();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // rehydrate the subcategories list with new subcategories
            if (cbCategory.SelectedValue != null)
            {
                subCategoryDataObject = CategoriesController.GetFilteredSubcategories((string)cbCategory.SelectedValue);
                subcategoryBindingSource.DataSource = subCategoryDataObject;
                cbSubcategory.SelectedIndex = 0;
            }
        }
        #endregion

        public void HydrateForm(DenormalizedLineItem lineItem)
        {
            // make a copy of the lineItem to save for resetting purposes
            originalLineItem = lineItem.Clone() as DenormalizedLineItem;

            // check the line item to check if this is a new item, or current item
            if (String.IsNullOrWhiteSpace(lineItem.Key))
            {
                this.Text = "New Line Item";
            }
            
            // populate the form
            // date
            dtpTxDate.Value = new DateTime(lineItem.Year, lineItem.MonthInt, lineItem.Day);

            // category
            // Otherwise, show messagebox error.
            if (!String.IsNullOrWhiteSpace(lineItem.CategoryKey))
            {
                cbCategory.SelectedValue = lineItem.CategoryKey;

                // get filtered subcategories by currently chosen category
                subCategoryDataObject = CategoriesController.GetFilteredSubcategories(lineItem.CategoryKey);
                subcategoryBindingSource.DataSource = subCategoryDataObject;
                cbSubcategory.SelectedValue = lineItem.SubCategoryKey;
            }

            // description
            txtDescription.Text = lineItem.Description;

            // transaction amount
            txtTxAmount.Text = Math.Round(lineItem.Amount, 2).ToString();

            // transaction type
            cbTxType.SelectedIndex = (int)lineItem.Type;

            // payment method
            if (!String.IsNullOrWhiteSpace(lineItem.PaymentMethodKey))
            {
                cbPaymentMethod.SelectedValue = lineItem.PaymentMethodKey;
            }
            else
            {
                cbPaymentMethod.SelectedIndex = 0;
            }                       

            // status
            cbStatus.SelectedIndex = (int)lineItem.Status;

            // Tax Deductible
            chkTaxDeductible.Checked = lineItem.IsTaxDeductible;
        }
    }   
}
