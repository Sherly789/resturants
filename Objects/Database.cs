using System.Data;
using System.Data.SqlClient;

namespace DiningList
{
    public class DB
    {
      public static SqlConnection Connection()
      {
        SqlConnection conn = new SqlConnection(DBConfiguration.ConnectionString);
        return conn;
      }
    }
}
