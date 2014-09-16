using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using HouseholdBudget.Data.Interfaces;
using HouseholdBudget.Data.Protocol;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.Data.Domain;
using HouseholdBudget.Data.Utilities;
using HouseholdBudget.DataModel;
using System.Data.Objects;

namespace HouseholdBudget.Data.Implementation
{
    public class PaymentMethodMapper : IPaymentMethodMapper
    {
        #region Properties

        private static readonly ILog logger = LogManager.GetLogger("DBPaymentMethodMapper");

        #endregion

        #region Constructor

        public PaymentMethodMapper()
        {
        }

        #endregion

        #region IPaymentMethodMapper

        public LiveDataObject GetPaymentMethods()
        {
            return GetPaymentMethodDataSource();
        }

        public OperationStatus AddNewPaymentMethod(string methodName, bool isActive)
        {
            return SavePaymentMethodToDB(dimPaymentMethods.CreatedimPaymentMethods(Guid.NewGuid(), methodName, isActive));
        }

        public string GetPaymentMethodList(char delimiter)
        {
            return GetPaymentMethodEnumerable().ToDelimitedString(delimiter);
        }

        public PaymentMethod GetPaymentMethodByName(string paymentMethodName)
        {
            return GetPaymentMethodByNameFromDB(paymentMethodName);
        }

        public PaymentMethod GetDefaultPaymentMethod()
        {
            return GetDefaultPaymentMethodFromDB();
        }
        #endregion

        #region Private Methods

        private LiveDataObject GetPaymentMethodDataSource()
        {
            BudgetEntities ctx = new BudgetEntities();
            var paymentMethods = ctx.dimPaymentMethods.OrderBy("it.PaymentMethodName");

            return new LiveDataObject()
            {
                dataSource = paymentMethods.Execute(MergeOption.OverwriteChanges),
                objectContext = ctx
            };            
        }

        private OperationStatus SavePaymentMethodToDB(dimPaymentMethods newPaymentMethod)
        {
            using (BudgetEntities ctx = new BudgetEntities())
            {
                var paymentMethods = from pm in ctx.dimPaymentMethods
                                     where pm.PaymentMethodName == newPaymentMethod.PaymentMethodName
                                     select pm;

                // if a payment method with this name already exists, return failure
                if (paymentMethods.Count() > 0)
                {
                    logger.Info("Attempted to add a new payment method that already exists!");
                    return OperationStatus.FAILURE;
                }

                // otherwise, add to the DB
                // add object to context
                logger.Info(String.Format("Adding a new Payment Method: {0}.", newPaymentMethod.PaymentMethodName));
                ctx.dimPaymentMethods.AddObject(newPaymentMethod);

                // save changes to DB
                ctx.SaveChanges();

                // return success operation
                return OperationStatus.SUCCESS;
            }
        }

        private IEnumerable<string> GetPaymentMethodEnumerable()
        {
            using (BudgetEntities ctx = new BudgetEntities())
            {
                List<string> paymentMethods = (from pm in ctx.dimPaymentMethods
                                               orderby pm.PaymentMethodName
                                               select pm.PaymentMethodName).ToList<string>();

                return paymentMethods;
            }
        }

        private PaymentMethod GetPaymentMethodByNameFromDB(string paymentMethodName)
        {
            PaymentMethod paymentMethodInfo = null;

            try
            {
                using (BudgetEntities ctx = new BudgetEntities())
                {
                    // get a list of Guids that match the category name
                    // there should realistically only ever be one match
                    List<dimPaymentMethods> matches = (from pm in ctx.dimPaymentMethods
                                            where pm.PaymentMethodName == paymentMethodName
                                            select pm).ToList();

                    if (matches.Count > 0)
                    {
                        // if a match exists, take the first one (there should only by one)
                        paymentMethodInfo = new PaymentMethod()
                        {
                           PaymentMethodName = matches[0].PaymentMethodName,
                           PaymentMethodKey = matches[0].PaymentMethodKey,
                           IsActive = matches[0].IsActive
                        };
                    }

                    return paymentMethodInfo;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred while attempting to get the key for the payment method " + paymentMethodName + "."
                    + Environment.NewLine + Environment.NewLine +
                    "Error details: " + Environment.NewLine +
                    ex.Message);

                return paymentMethodInfo;
            }
        }

        private PaymentMethod GetDefaultPaymentMethodFromDB()
        {
            try
            {
                using (BudgetEntities ctx = new BudgetEntities())
                {
                    List<dimPaymentMethods> paymentMethods = (from pm in ctx.dimPaymentMethods
                                                              where pm.IsActive
                                                              orderby pm.PaymentMethodKey
                                                              select pm).ToList();

                    if (paymentMethods.Count > 0)
                    {
                        return new PaymentMethod()
                        {
                            PaymentMethodKey = paymentMethods[0].PaymentMethodKey,
                            PaymentMethodName = paymentMethods[0].PaymentMethodName,
                            IsActive = paymentMethods[0].IsActive
                        };
                    }
                    else
                    {
                        return null;
                    }                    
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred while attempting to get the default payment method."
                    + Environment.NewLine + Environment.NewLine +
                    "Error details: " + Environment.NewLine +
                    ex.Message);

                return null;
            }
        }
        #endregion
    }
}
