﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using FamilyBudget.AddIn.Controllers;
using FamilyBudget.AddIn.DataControllers;
using FamilyBudget.AddIn.Enums;
using FamilyBudget.AddIn.Utilities;
using FamilyBudget.Data.Domain;
using FamilyBudget.Data.Enums;
using FamilyBudget.Data.Protocol;
using FamilyBudget.Data.Utilities;

namespace FamilyBudget.AddIn.UI
{
    public partial class frmItem : Form
    {

        #region Properties
        private LiveDataObject categoryDataObject;
        private LiveDataObject subCategoryDataObject;
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
            categoryDataObject = CategoriesController.GetCategories();
            categoryBindingSource.DataSource = categoryDataObject.dataSource;

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
                if (originalLineItem.UniqueKey == Guid.Empty)
                {
                    WorksheetDataController.RemoveLineItem(listObjectIndex, lineItemIndex, worksheetType);
                }
                else
                {
                    // if delete is confirmed, do it, and refresh the data
                    LineItemsController.DeleteLineItem(originalLineItem.UniqueKey);
                    WorksheetDataController.RemoveLineItem(listObjectIndex, lineItemIndex, worksheetType);
                    LineItemsController.RebuildDataSheet();
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
            originalLineItem.DayOfWeekId = (short)dtpTxDate.Value.DayOfWeek;
            originalLineItem.Quarter = DateUtil.GetQuarterForMonth(dtpTxDate.Value.Month);
            originalLineItem.Category = cbCategory.Text;
            originalLineItem.CategoryKey = (Guid)cbCategory.SelectedValue;
            originalLineItem.SubCategory = cbSubcategory.Text;
            originalLineItem.SubCategoryKey = (Guid)cbSubcategory.SelectedValue;
            originalLineItem.Description = txtDescription.Text;
            originalLineItem.Amount = Decimal.Parse(txtTxAmount.Text);
            originalLineItem.Type = (LineItemType)cbTxType.SelectedValue;
            originalLineItem.PaymentMethod = cbPaymentMethod.Text;
            originalLineItem.PaymentMethodKey = (Guid)cbPaymentMethod.SelectedValue;
            originalLineItem.Status = (LineItemStatus)cbStatus.SelectedValue;

            if (originalLineItem.UniqueKey != Guid.Empty)
            {
                LineItemsController.UpdateLineItem(originalLineItem);
                LineItemsController.RebuildDataSheet();
                WorkbookUtil.RefreshPivotTables();
            }

            // update the line item in the data worksheet
            WorksheetDataController.UpdateLineItem(this.listObjectIndex, this.lineItemIndex, this.worksheetType, originalLineItem);

            // close the form
            this.Close();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // rehydrate the subcategories list with new subcategories
            if (cbCategory.SelectedValue != null)
            {
                subCategoryDataObject = CategoriesController.GetFilteredSubCategories((Guid)cbCategory.SelectedValue);
                subcategoryBindingSource.DataSource = subCategoryDataObject.dataSource;
                cbSubcategory.SelectedIndex = 0;
            }
        }
        #endregion

        public void HydrateForm(DenormalizedLineItem lineItem)
        {
            // make a copy of the lineItem to save for resetting purposes
            originalLineItem = lineItem.Clone() as DenormalizedLineItem;

            // check the line item to check if this is a new item, or current item
            if (lineItem.UniqueKey == Guid.Empty)
            {
                this.Text = "New Line Item";
            }
            
            // populate the form
            // date
            dtpTxDate.Value = new DateTime(lineItem.Year, lineItem.MonthInt, lineItem.Day);

            // category
            // Otherwise, show messagebox error.
            cbCategory.SelectedValue = lineItem.CategoryKey;

            // get filtered subcategories by currently chosen category
            subCategoryDataObject = CategoriesController.GetFilteredSubCategories(lineItem.CategoryKey);
            subcategoryBindingSource.DataSource = subCategoryDataObject.dataSource;
            cbSubcategory.SelectedValue = lineItem.SubCategoryKey;

            // description
            txtDescription.Text = lineItem.Description;

            // transaction amount
            txtTxAmount.Text = Math.Round(lineItem.Amount, 2).ToString();

            // transaction type
            cbTxType.SelectedIndex = (int)lineItem.Type;

            // payment method
            if (lineItem.PaymentMethodKey != Guid.Empty)
            {
                cbPaymentMethod.SelectedValue = lineItem.PaymentMethodKey;
            }
            else
            {
                cbPaymentMethod.SelectedIndex = 0;
            }                       

            // status
            cbStatus.SelectedIndex = (int)lineItem.Status;
        }
    }   
}