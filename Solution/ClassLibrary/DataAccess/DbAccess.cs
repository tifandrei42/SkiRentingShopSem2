using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataAccess
{
    public class DbAccess
    {
        protected SqlConnection connection;

        public DbAccess()
        {
            string connection =
                "Server=mssqlstud.fhict.local;" +
                "Database=dbi455422;" +
                "User Id=dbi455422;" +
                "Password=zed_987654321;" +
                "TrustServerCertificate=True;";

            this.connection = new SqlConnection(connection);
        }
    }
}
