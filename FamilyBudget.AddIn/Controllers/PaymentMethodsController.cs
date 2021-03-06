﻿using FamilyBudget.AddIn.UI;
using FamilyBudget.Common.Interfaces;
using log4net;
using Microsoft.Office.Tools.Ribbon;
using System.ComponentModel;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Enums;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using FamilyBudget.Common;

namespace FamilyBudget.AddIn.Controllers
{
    internal static class PaymentMethodsController
    {
        #region Properties
        // editing form
        private static frmPaymentMethods paymentMethodsForm;

        // paymentMethod mapper interface
        private static IPaymentMethodAPI _paymentMethodAPI;
        private static IPaymentMethodAPI paymentMethodAPI
        {
            get
            {
                if (_paymentMethodAPI == null)
                {
                    // get the configured name of the interface to manage payment methods
                    Type mapperType = APIResolver.ResolveTypeForInterface(typeof(IPaymentMethodAPI));
                    if (mapperType != null)
                    {
                        _paymentMethodAPI = (IPaymentMethodAPI)Activator.CreateInstance(mapperType);
                    }
                    else
                    {
                        _paymentMethodAPI = null;
                    }
                }

                return _paymentMethodAPI;
            }
        }

        // logger
        private static readonly ILog logger = LogManager.GetLogger("FamilyBudget.AddIn_PaymentMethodController");
        #endregion

        internal static void btnManagePaymentMethods_Click(object sender, RibbonControlEventArgs e)
        {
            paymentMethodsForm = new frmPaymentMethods();
            paymentMethodsForm.Show();
        }

        internal static BindingList<PaymentMethod> GetPaymentMethods(bool force = false)
        {
            return paymentMethodAPI.GetPaymentMethods(force);
        }

        internal static OperationStatus AddNewPaymentMethod(PaymentMethod newPaymentMethod)
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
            paymentMethods.Add(newPaymentMethod);

            return paymentMethodAPI.AddNewPaymentMethods(paymentMethods);
        }

        internal static PaymentMethod GetPaymentMethodByName(string paymentMethodName)
        {
            return paymentMethodAPI.GetPaymentMethodByName(paymentMethodName);
        }

        internal static PaymentMethod GetDefaultPaymentMethod()
        {
            PaymentMethod pm = paymentMethodAPI.GetDefaultPaymentMethod();

            if (pm == null)
            {
                MessageBox.Show("To continue processing items, at least one Payment Method must exist.");
            }

            return pm;
        }
    }
}
