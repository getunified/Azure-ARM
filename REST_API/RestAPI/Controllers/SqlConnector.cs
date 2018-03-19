using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    public class SqlConnector
    {
        public SqlConnector()
        {

        }

        public SqlConnection CloudConnect()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = Properties.Resources.DataSource;
            builder.UserID = Properties.Resources.UserID;
            builder.Password = Properties.Resources.DBPassword;
            builder.InitialCatalog = Properties.Resources.InitialCatalog;

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            return connection;
        }
        
        public SqlConnection LocalConnect()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = ".";
            builder.UserID = "TestID";
            builder.Password = "testtest";
            builder.InitialCatalog = "Test1";

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            return connection;
        }
    }
}
