using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FamilyBudget.AddIn.Controllers;
using FamilyBudget.Data.Domain;
using FamilyBudget.Data.Enums;
using FamilyBudget.Data.Utilities;

namespace FamilyBudget.AddIn.UI
{
    public partial class frmSearchItems : Form
    {
        private static Point CONTROL_REF_POINT = new Point() 
        {
            X = 253,
            Y = 194
        };

        private static EnumListMember[] DAYS_IN_MONTH = 
        {
            new EnumListMember("1",1), new EnumListMember("2",2), new EnumListMember("3",3), new EnumListMember("4",4), new EnumListMember("5",5),
            new EnumListMember("6",6), new EnumListMember("7",7), new EnumListMember("8",8), new EnumListMember("1",1), new EnumListMember("10",10),
            new EnumListMember("11",11), new EnumListMember("12",1), new EnumListMember("13",13), new EnumListMember("14",14), new EnumListMember("15",15),
            new EnumListMember("16",16), new EnumListMember("17",17), new EnumListMember("18",18), new EnumListMember("19",19), new EnumListMember("20",20),
            new EnumListMember("21",21), new EnumListMember("22",22), new EnumListMember("23",23), new EnumListMember("24",24), new EnumListMember("25",25),
            new EnumListMember("26",26), new EnumListMember("27",27), new EnumListMember("28",28), new EnumListMember("29",29), new EnumListMember("30",30),
            new EnumListMember("31",31)
        };

        private BindingList<EnumListMember> searchCriteriaListData;
        private SearchCriteria searchCriteria;
        
        private const string CONTAINS_LABEL = "contains";
        private const string EQUAL_LABEL = "=";
        private SearchFields currentlySelectedField;

        public frmSearchItems()
        {
            InitializeComponent();
        }

        public void NotifyUser(string message)
        {
            MessageBox.Show(message);
        }

        private void frmSearchItems_Load(object sender, EventArgs e)
        {
            // bind initial data sources
            SearchFieldBindingSource.DataSource = EnumUtil.GetEnumMemberArray(typeof(SearchFields));
            CompareOperatorBindingSource.DataSource = EnumUtil.GetEnumMemberArray(typeof(Comparators));
            searchCriteriaListData = new BindingList<EnumListMember>();
            listSearchCriteria.DataSource = searchCriteriaListData;
            listSearchCriteria.DisplayMember = "DisplayValue";
            listSearchCriteria.ValueMember = "ActualValue";

            // set default selections
            cbSearchField.SelectedIndex = (int)SearchFields.DATE;
            RefreshSearchForm();
            
            // initialize the search criteria object
            searchCriteria = new SearchCriteria();
        }

        private void cbSearchField_SelectedIndexChanged(object sender, EventArgs e)
        {
            // refresh the search form when a new search field is chosen
            RefreshSearchForm();
        }

        private void cbComparators_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((currentlySelectedField == SearchFields.AMOUNT || currentlySelectedField == SearchFields.DATE) &&
                 cbComparators.SelectedItem != null)
            {
                // if amount or date, the value chosen in comparators combo box affects what is displayed
                EnumListMember comparator = (EnumListMember)cbComparators.SelectedItem;
                Comparators chosenComparator = (Comparators)comparator.ActualValue;

                SetVisibilityOfRangeFields(chosenComparator);                
            }            
        }

        private void btnRemoveSelectedCriteria_Click(object sender, EventArgs e)
        {
            // get the field for the selected criteria
            EnumListMember criteriaToRemove = (EnumListMember)listSearchCriteria.SelectedItem;
            SearchFields fieldToRemove = (SearchFields)criteriaToRemove.ActualValue;

            // clear the field from the search criteria
            searchCriteria.ClearField(fieldToRemove);

            // clear the entry from the binding list
            searchCriteriaListData.Remove(criteriaToRemove);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // discard the old search criteria, and start new
            searchCriteria = new SearchCriteria();
            searchCriteriaListData.Clear();
        }

        private void RefreshSearchForm()
        {
            // get the search field that has been chosen
            if (cbSearchField.SelectedItem != null)
            {
                EnumListMember searchField = (EnumListMember)cbSearchField.SelectedItem;
                currentlySelectedField = (SearchFields)searchField.ActualValue;

                // find out if it is a comparable field, and adjust the display as necessary
                bool isComparable = currentlySelectedField.IsComparable();
                lblOperator.Visible = !isComparable;
                cbComparators.Visible = isComparable;

                // configure the form to load & show the correct combo boxes and 
                ConfigureSearchForm();
            }
        }
        
        private void SetVisibilityOfRangeFields(Comparators chosenComparator)
        {
            // set visibility of the amount/date fields based
            txtMaxAmount.Visible = lblAmountAnd.Visible =
                currentlySelectedField == SearchFields.AMOUNT && chosenComparator == Comparators.BETWEEN;
            dtpMaxDate.Visible = lblDateAnd.Visible =
                currentlySelectedField == SearchFields.DATE && chosenComparator == Comparators.BETWEEN;
        }

        private void ConfigureSearchForm()
        {
            // set some defaults
            lblOperator.Text = EQUAL_LABEL;
            cbCategory.Visible = cbDay.Visible = cbDayOfWeek.Visible = cbMonth.Visible =
                cbPaymentMethod.Visible = cbQuarter.Visible = cbStatus.Visible = cbSubCategory.Visible =
                cbSubType.Visible = cbType.Visible = txtTextValue.Visible = panelAmount.Visible =
                panelDates.Visible = false;

            switch (currentlySelectedField)
            {
                case SearchFields.AMOUNT:
                    PositionControl(panelAmount);
                    panelAmount.Visible = true;
                    txtMinAmount.Text = String.Empty;
                    txtMaxAmount.Text = String.Empty;
                    SetVisibilityOfRangeFields(
                        (Comparators)((EnumListMember)cbComparators.SelectedItem).ActualValue
                    );
                    break;
                case SearchFields.CATEGORY:
                    PositionControl(cbCategory);
                    cbCategory.Visible = true;
                    CategoryBindingSource.DataSource = CategoriesController.GetCategories(false);
                    break;
                case SearchFields.DATE:
                    PositionControl(panelDates);
                    panelDates.Visible = true;
                    SetVisibilityOfRangeFields(
                        (Comparators)((EnumListMember)cbComparators.SelectedItem).ActualValue
                    );
                    break;
                case SearchFields.DAY:
                    PositionControl(cbDay);
                    cbDay.Visible = true;
                    DayBindingSource.DataSource = DAYS_IN_MONTH;
                    break;
                case SearchFields.DAY_OF_WEEK:
                    PositionControl(cbDayOfWeek);
                    cbDayOfWeek.Visible = true;
                    DayOfWeekBindingSource.DataSource = EnumUtil.GetEnumMemberArray(typeof(Data.Enums.DayOfWeek));
                    break;
                case SearchFields.DESCRIPTION:
                    PositionControl(txtTextValue);
                    txtTextValue.Visible = true;
                    lblOperator.Text = CONTAINS_LABEL;
                    txtTextValue.Text = String.Empty;
                    break;
                case SearchFields.MONTH:
                    PositionControl(cbMonth);
                    cbMonth.Visible = true;
                    MonthBindingSource.DataSource = EnumUtil.GetEnumMemberArray(typeof(Months));
                    break;
                case SearchFields.PAYMENT_METHOD:
                    PositionControl(cbPaymentMethod);
                    cbPaymentMethod.Visible = true;
                    PaymentMethodBindingSource.DataSource = PaymentMethodsController.GetPaymentMethods();
                    break;
                case SearchFields.QUARTER:
                    PositionControl(cbQuarter);
                    cbQuarter.Visible = true;
                    QuarterBindingSource.DataSource = EnumUtil.GetEnumMemberArray(typeof(Quarters));
                    break;
                case SearchFields.STATUS:
                    PositionControl(cbStatus);
                    cbStatus.Visible = true;
                    StatusBindingSource.DataSource = EnumUtil.GetEnumMemberArray(typeof(LineItemStatus));
                    break;
                case SearchFields.SUBCATEGORY:
                    PositionControl(cbSubCategory);
                    cbSubCategory.Visible = true;
                    SubCategoryBindingSource.DataSource = CategoriesController.GetSubcategories(false);
                    break;
                case SearchFields.SUBTYPE:
                    PositionControl(cbSubType);
                    cbSubType.Visible = true;
                    SubTypeBindingSource.DataSource = EnumUtil.GetEnumMemberArray(typeof(LineItemSubType));
                    break;
                case SearchFields.TYPE:
                    PositionControl(cbType);
                    cbType.Visible = true;
                    TypeBindingSource.DataSource = EnumUtil.GetEnumMemberArray(typeof(LineItemType));
                    break;
                case SearchFields.YEAR:
                    PositionControl(txtTextValue);
                    txtTextValue.Visible = true;
                    txtTextValue.Text = String.Empty;
                    break;
                default:
                    break;
            }
        }

        private void PositionControl(Control ctrl)
        {
            ctrl.Location = CONTROL_REF_POINT;
        }

        private void btnAddCriteria_Click(object sender, EventArgs e)
        {
            // every time this button is clicked, the search criteria object should be 
            // filled with a new criteria. The current list should be checked first to see if it has already been added
            
            // do a quick sanity check before evaluating criteria
            string criteriaDesc = String.Empty;
            if (ValidateCriteria(out criteriaDesc))
            {
                // create the criteria description string
                EnumListMember existingCriteria = searchCriteriaListData.FirstOrDefault(sc => ((SearchFields)sc.ActualValue) == currentlySelectedField);
                if (existingCriteria != null)
                {
                    int index = searchCriteriaListData.IndexOf(existingCriteria);
                    searchCriteriaListData[index] = new EnumListMember(criteriaDesc, (int)currentlySelectedField);
                }
                else
                {
                    searchCriteriaListData.Add(new EnumListMember(criteriaDesc, (int)currentlySelectedField));
                }
            }            
        }

        private bool ValidateCriteria(out string criteriaDesc)
        {
            bool isValid = true;
            Comparators selectedComparator = Comparators.NO_COMPARE;
            StringBuilder criteriaDescBuilder = new StringBuilder();
            criteriaDescBuilder.Append(EnumUtil.GetFriendlyName(currentlySelectedField));
            
            // switch through all fields and validate, generate descriptions
            switch (currentlySelectedField)
            {
                case SearchFields.AMOUNT:
                    #region Amount Validation
                    selectedComparator = ((Comparators)(((EnumListMember)cbComparators.SelectedItem).ActualValue));
                    if (searchCriteria.ValidateAmount(txtMinAmount.Text, txtMaxAmount.Text, selectedComparator))
                    {
                        criteriaDescBuilder.AppendFormat("  {0}  {1}{2}",
                            EnumUtil.GetFriendlyName(selectedComparator),
                            txtMinAmount.Text,
                            selectedComparator == Comparators.BETWEEN ? " and " + txtMaxAmount.Text : String.Empty
                        );
                    }
                    else
                    {
                        isValid = false;
                    }
                    #endregion
                    break;
                case SearchFields.CATEGORY:
                    searchCriteria.Category = (string)cbCategory.SelectedValue;
                    criteriaDescBuilder.AppendFormat("  =  {0}", cbCategory.Text);
                    break;
                case SearchFields.DATE:
                    #region Date Validation
                    selectedComparator = ((Comparators)(((EnumListMember)cbComparators.SelectedItem).ActualValue));
                    if (searchCriteria.ValidateDate(dtpMinDate.Value, dtpMaxDate.Value, selectedComparator))
                    {
                        criteriaDescBuilder.AppendFormat("  {0}  {1}{2}",
                            EnumUtil.GetFriendlyName(selectedComparator),
                            dtpMinDate.Value.ToShortDateString(),
                            selectedComparator == Comparators.BETWEEN ? " and " + dtpMaxDate.Value.ToShortDateString() : String.Empty
                        );
                    }
                    else
                    {
                        isValid = false;
                    }
                    #endregion
                    break;
                case SearchFields.DAY:
                    searchCriteria.Day = Convert.ToInt16(cbDay.SelectedValue);
                    criteriaDescBuilder.AppendFormat("  =  {0}", cbDay.SelectedValue.ToString());
                    break;
                case SearchFields.DAY_OF_WEEK:
                    searchCriteria.DayOfWeek = Convert.ToInt16(cbDayOfWeek.SelectedValue);
                    criteriaDescBuilder.AppendFormat("  =  {0}", EnumUtil.GetFriendlyName((Data.Enums.DayOfWeek)searchCriteria.DayOfWeek.Value));
                    break;
                case SearchFields.DESCRIPTION:
                    searchCriteria.DescriptionContains = txtTextValue.Text;
                    criteriaDescBuilder.AppendFormat(" contains {0}", txtTextValue.Text);
                    break;
                case SearchFields.MONTH:
                    searchCriteria.Month = Convert.ToInt16(cbMonth.SelectedValue);
                    criteriaDescBuilder.AppendFormat("  =  {0}", EnumUtil.GetFriendlyName((Months)searchCriteria.Month.Value));
                    break;
                case SearchFields.PAYMENT_METHOD:
                    searchCriteria.PaymentMethod = (string)cbPaymentMethod.SelectedValue;
                    criteriaDescBuilder.AppendFormat("  =  {0}", cbPaymentMethod.Text);
                    break;
                case SearchFields.QUARTER:
                    searchCriteria.Quarter = Convert.ToInt16(cbQuarter.SelectedValue);
                    criteriaDescBuilder.AppendFormat("  =  {0}", EnumUtil.GetFriendlyName((Quarters)searchCriteria.Quarter.Value));
                    break;
                case SearchFields.STATUS:
                    searchCriteria.Status = Convert.ToInt16(cbStatus.SelectedValue);
                    criteriaDescBuilder.AppendFormat("  =  {0}", EnumUtil.GetFriendlyName((LineItemStatus)searchCriteria.Status.Value));
                    break;
                case SearchFields.SUBCATEGORY:
                    searchCriteria.SubCategory = (string)cbSubCategory.SelectedValue;
                    criteriaDescBuilder.AppendFormat("  =  {0}", cbSubCategory.Text);
                    break;
                case SearchFields.SUBTYPE:
                    searchCriteria.SubType = Convert.ToInt16(cbSubType.SelectedValue);
                    criteriaDescBuilder.AppendFormat("  =  {0}", EnumUtil.GetFriendlyName((LineItemSubType)searchCriteria.SubType.Value));
                    break;
                case SearchFields.TYPE:
                    searchCriteria.Type = Convert.ToInt16(cbType.SelectedValue);
                    criteriaDescBuilder.AppendFormat("  =  {0}", EnumUtil.GetFriendlyName((LineItemType)searchCriteria.Type.Value));
                    break;
                case SearchFields.YEAR:
                    #region Year Validation
                    if (searchCriteria.ValidateYear(txtTextValue.Text))
                    {
                        criteriaDescBuilder.AppendFormat("  =  {0}", txtTextValue.Text);
                    }
                    else
                    {
                        isValid = false;
                    }
                    #endregion
                    break;
                default:
                    break;
            }

            criteriaDesc = criteriaDescBuilder.ToString();
            return isValid;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LineItemsController.SearchSubmitted(searchCriteria);
        }        
    }
}
