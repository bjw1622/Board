using System.Configuration;
using System.Data.SqlClient;

namespace Board.Models
{
    public class Connection
    {
        public SqlConnection con;
        public void Conn()
        {
            string constr = ConfigurationManager.ConnectionStrings["BoardDB"].ToString();
            con = new SqlConnection(constr);
        }
    }
}