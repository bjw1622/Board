using System.Configuration;
using System.Data.SqlClient;

namespace Board.Models
{
    public class Connection
    {
        protected SqlConnection con;
        public void Conn()
        {
            string constr = ConfigurationManager.ConnectionStrings["BoardTestDB"].ToString();
            con = new SqlConnection(constr);
        }
        public void ConOpen()
        {
            con.Open();

        }
        public void ConClose()
        {
            //con.Close();
            con.Dispose();
        }
    }
}