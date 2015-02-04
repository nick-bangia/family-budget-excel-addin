using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MigrateSQLCEtoMySQL
{
    public class MySQLConnectionInfo
    {
        public MySQLConnectionInfo(string server, string db, string userId, string password)
        {
            this.Server = server;
            this.Database = db;
            this.UserId = userId;
            this.Password = password;
        }

        public string Server { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
