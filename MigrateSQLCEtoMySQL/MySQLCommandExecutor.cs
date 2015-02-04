using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace MigrateSQLCEtoMySQL
{
    public static class MySQLCommandExecutor
    {
        public static MySQLConnectionInfo connectionInfo;
        public static MySqlConnection dbConnection;
        public static string defaultResponse = "The command was executed successfully.";
        
        
        public static void GetConnectionToDB()
        {
            string connStr = String.Format("server={0};uid={1};pwd={2};database={3}",
                connectionInfo.Server, connectionInfo.UserId, connectionInfo.Password, connectionInfo.Database);
            
            dbConnection = new MySqlConnection(connStr);
        }
        
        public static string ExecuteCommand(string commandText)
        {
            MySqlCommand command = new MySqlCommand(commandText, dbConnection);

            return ExecuteNonQuery(command);
        }

        internal static string ExecuteCommandWithParameters(string commandText, List<KeyValuePair<string, object>> parameters)
        {
            MySqlCommand command = new MySqlCommand(commandText, dbConnection);
            
            foreach (KeyValuePair<string, object> parameter in parameters)
            {
                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }

            // execute command and return result
            return ExecuteNonQuery(command);                        
        }

        private static string ExecuteNonQuery(MySqlCommand command)
        {
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return defaultResponse;
        }
    }
}
