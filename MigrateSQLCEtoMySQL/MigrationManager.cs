using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyBudget.DataModel;

namespace MigrateSQLCEtoMySQL
{
    public static class MigrationManager
    {
        public static void MigrateCategories()
        {
            Console.Write("0 % completed.....");

            using (BudgetEntities ctx = new BudgetEntities())
            {
                List<dimCategories> categoryList = (from cat in ctx.dimCategories
                                                    select cat).ToList();

                int numCategories = categoryList.Count();

                // create sql statement
                string sql = @"INSERT INTO dimCategory (CategoryKey, CategoryName, IsActive, LastUpdatedDate) VALUES ('{0}',@param_name, 1, NOW());";

                // loop through and add each category to the MySQL db
                for (int i = 0; i < numCategories; i++)
                {
                    string commandText = String.Format(sql, categoryList[i].CategoryKey.ToString());

                    List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>("@param_name", categoryList[i].CategoryName));

                    string response = MySQLCommandExecutor.ExecuteCommandWithParameters(commandText, parameters);

                    if (!response.Equals(MySQLCommandExecutor.defaultResponse))
                    {
                        Console.WriteLine("Error: " + response);
                    }
                                       
                }
            }

            Console.Write("\r100 % completed.....\n");
        }

        public static void MigrateSubCategories()
        {
            Console.Write("0 % completed.....");

            using (BudgetEntities ctx = new BudgetEntities())
            {
                List<dimSubCategories> subCategoryList = (from subCat in ctx.dimSubCategories
                                                          select subCat).ToList();

                int numSubCategories = subCategoryList.Count();

                // create sql statement
                string sql = @"INSERT INTO dimSubcategory (SubCategoryKey, CategoryKey, SubCategoryName, SubCategoryPrefix, IsActive, IsGoal, LastUpdatedDate) VALUES ('{0}','{1}',@param_name,'{2}',{3},'{4}', NOW());";

                // loop through and add each category to the MySQL db
                for (int i = 0; i < numSubCategories; i++)
                {
                    string commandText = String.Format(sql, 
                        subCategoryList[i].SubCategoryKey.ToString(), subCategoryList[i].CategoryKey.ToString(), subCategoryList[i].SubCategoryPrefix, subCategoryList[i].IsActive ? 1 : 0, subCategoryList[i].IsGoal ? 1 : 0);
                    
                    List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>("@param_name", subCategoryList[i].SubCategoryName));

                    string response = MySQLCommandExecutor.ExecuteCommandWithParameters(commandText, parameters);

                    if (!response.Equals(MySQLCommandExecutor.defaultResponse))
                    {
                        Console.WriteLine("Error: " + response);
                    }
                }
            }

            Console.Write("\r100 % completed.....\n");
        }

        internal static void MigratePaymentMethods()
        {
            Console.Write("0 % completed.....");

            using (BudgetEntities ctx = new BudgetEntities())
            {
                List<dimPaymentMethods> pmList = (from pm in ctx.dimPaymentMethods
                                                  select pm).ToList();

                int numPaymentMethods = pmList.Count();

                // create sql statement
                string sql = @"INSERT INTO dimPaymentMethod (PaymentMethodKey, PaymentMethodName, IsActive, LastUpdatedDate) VALUES ('{0}',@param_name,{1}, NOW())";

                // loop through and add each payment method to the MySQL db
                for (int i = 0; i < numPaymentMethods; i++)
                {
                    string commandText = String.Format(sql,
                        pmList[i].PaymentMethodKey.ToString(), pmList[i].IsActive ? 1 : 0);
                    List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>("@param_name", pmList[i].PaymentMethodName));

                    string response = MySQLCommandExecutor.ExecuteCommandWithParameters(commandText, parameters);

                    if (!response.Equals(MySQLCommandExecutor.defaultResponse))
                    {
                        Console.WriteLine("Error: " + response);
                    }
                }
            }

            Console.Write("\r100 % completed.....\n");
        }

        internal static void MigrateFacts()
        {
            Console.WriteLine("0 % completed.....");

            using (BudgetEntities ctx = new BudgetEntities())
            {
                List<factLineItems> factsList = (from f in ctx.factLineItems
                                                 select f).ToList();

                int numFacts = factsList.Count();

                // create sql statement
                string sql = @"INSERT INTO factLineItem (UniqueKey, MonthId, DayOfMonth, DayOfWeekId, Year, SubCategoryKey, 
                                                         Description, Amount, TypeId, SubtypeId, Quarter, PaymentMethodKey, 
                                                         StatusId, LastUpdatedDate) 
                               VALUES ('{0}',{1},{2},{3},{4},'{5}',@param_description,{6},{7},{8},{9},'{10}',{11}, NOW())";

                // loop through and add each payment method to the MySQL db
                for (int i = 0; i < numFacts; i++)
                {
                    string commandText = String.Format(sql,
                        factsList[i].UniqueKey.ToString(), factsList[i].MonthId, factsList[i].DayOfMonthId,
                        factsList[i].DayOfWeekId, factsList[i].YearId, factsList[i].CategoryKey.ToString(),
                        factsList[i].Amount, factsList[i].TypeId, factsList[i].SubTypeId, factsList[i].QuarterId,
                        factsList[i].PaymentMethodId.ToString(), factsList[i].StatusId);

                    List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string,object>("@param_description", factsList[i].Description));
                    
                    string response = MySQLCommandExecutor.ExecuteCommandWithParameters(commandText, parameters);

                    if (!response.Equals(MySQLCommandExecutor.defaultResponse))
                    {
                        Console.WriteLine("Error: " + Environment.NewLine + response + Environment.NewLine + "SubCategoryKey: " + factsList[i].CategoryKey.ToString());
                    }                            
                }
            }

            Console.WriteLine("100 % completed.....\n");
        }
    }
}
