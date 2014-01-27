using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HouseholdBudget.Data.Protocol;
using System.Data.Objects;

namespace HouseholdBudget.UI
{
    public partial class frmUpdateCategories : Form
    {
        #region Properties
        private LiveDataObject categoryDataObject;
        #endregion

        public frmUpdateCategories()
        {
            InitializeComponent();
        }

        private void frmUpdateCategories_Load(object sender, EventArgs e)
        {
            categoryDataObject = Controller.GetCategories();
            categoryBindingSource.DataSource = categoryDataObject.dataSource;
        }

        private void categoryDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            categoryDataObject.objectContext.SaveChanges();
        }
    }
}
