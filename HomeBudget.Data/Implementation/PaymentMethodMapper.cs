using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using HouseholdBudget.Data.Interfaces;
using HouseholdBudget.Data.Protocol;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.Data.Domain;
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

        #endregion


        
    }
}
