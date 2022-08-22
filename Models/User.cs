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

        // 회원 가입
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

        // 로그인
        public int LogIn(UserEntity obj)
        {
            Conn();
            con.Open();
            int result;
            using (SqlCommand com = new SqlCommand("dbo.LogInUser", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Email", obj.Email);
                com.Parameters.AddWithValue("@Pw", obj.Pw);
                result = (int)com.ExecuteScalar();
            }
            con.Close();
            con.Dispose();
            // result가 아닌 name 반환
            return result;
        }

        // 이메일 중복 체크
        public int EmailCheck(UserEntity obj)
        {
            int result = -1;
            try
            {
                Conn();
                con.Open();
                using (SqlCommand com = new SqlCommand("dbo.EmailCheck", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Email", obj.Email);
                    result = (int)com.ExecuteScalar();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return result;
        }
    }
}