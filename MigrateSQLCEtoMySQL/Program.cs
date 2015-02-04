using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MigrateSQLCEtoMySQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Migrating SqlCE to MySQL....");
            Console.WriteLine();

            try
            {
                // setup MySQL Connection info
                MySQLCommandExecutor.connectionInfo = new MySQLConnectionInfo("localhost", "HomeBudget", "root", "");
                MySQLCommandExecutor.GetConnectionToDB();
                MySQLCommandExecutor.dbConnection.Open();

                // migrate dimension tables
                MigrateDimensionTables();

                // migrate fact table
                MigrateFactTable();

                // close the connection
                MySQLCommandExecutor.dbConnection.Close();

                Console.WriteLine();
                Console.WriteLine("Migration completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Migration ended with errors. Details below:");
                Console.WriteLine(ex.Message);
            }

            while (!Console.KeyAvailable) { }
        }

        private static void MigrateFactTable()
        {
            Console.WriteLine("Beginning Migration of Fact table...");

            // migrate factLineItems
            Console.WriteLine("Migrating factLineItems");
            MigrationManager.MigrateFacts();
        }

        private static void MigrateDimensionTables()
        {
            Console.WriteLine("Beginning Migration of Dimension Tables...");
            
            // get the data from each table, and migrate it
            Console.WriteLine("Migrating dimCategories");
            MigrationManager.MigrateCategories();

            Console.WriteLine("Migrating dimSubCategories");
            MigrationManager.MigrateSubCategories();

            Console.WriteLine("Migrating dimPaymentMethods");
            MigrationManager.MigratePaymentMethods();
        }        
    }
}
