using Board.Entitys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Board.Models
{
    public class User
    {
        // sqlConnection 
        private SqlConnection con;

        public void Conn()
        {
            string constr = ConfigurationManager.ConnectionStrings["BoardDB"].ToString();
            con = new SqlConnection(constr);
        }

        public void AddUser(UserEntity obj)
        {
            Conn();
            con.Open();
            // 사용할 프로시저의 이름을 설정
            using (SqlCommand com = new SqlCommand("dbo.InsertUser", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Email", obj.Email);
                com.Parameters.AddWithValue("@Pw", obj.Pw);
                com.Parameters.AddWithValue("@Name", obj.Name);
                com.Parameters.AddWithValue("@Birth", obj.Birth);
                com.ExecuteNonQuery();
            }
            con.Close();
            con.Dispose();
        }
        //public void CheckEmail(string email)
        //{
        //    Conn();
        //    con.Open();
        //    // 사용할 프로시저의 이름을 설정
        //    using (SqlCommand com = new SqlCommand("dbo.emailCheck", con))
        //    {
        //        com.CommandType = CommandType.StoredProcedure;
        //        var num = com.ExecuteNonQuery();
        //        Console.WriteLine(num);
        //    }
        //    con.Close();
        //    con.Dispose();
        //}
        int check;
        public int LoginCheck(string Email, string Pw)
        {
            Conn();
            con.Open();
            // 사용할 프로시저의 이름을 설정
            using (SqlCommand com = new SqlCommand("dbo.loginCheck", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@email", Email);
                com.Parameters.AddWithValue("@pw", Pw);
                check = (int)com.ExecuteScalar();
            }
            con.Close();
            con.Dispose();
            Console.WriteLine(check);
            return check;
        }
    }
}