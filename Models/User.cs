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
    public class User : Connection
    {
        // 회원 가입
        public void AddUser(UserEntity obj)
        {
            Conn();
            ConOpen();
            using (SqlCommand com = new SqlCommand("dbo.InsertUser", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Email", obj.Email);
                com.Parameters.AddWithValue("@Pw", obj.Pw);
                com.Parameters.AddWithValue("@Name", obj.Name);
                com.Parameters.AddWithValue("@Birth", obj.Birth);
                com.Parameters.AddWithValue("@CreateDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                com.ExecuteNonQuery();
            }
            ConClose();
        }

        // 로그인
        public UserEntity LogIn(UserEntity obj)
        {
            Conn();
            ConOpen();
            UserEntity users = new UserEntity();
            using (SqlCommand com = new SqlCommand("dbo.LogInUser", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Email", obj.Email);
                com.Parameters.AddWithValue("@Pw", obj.Pw);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    users.Email = Convert.ToString(reader["Email"]);
                    users.Name = Convert.ToString(reader["Name"]);
                }
            }
            ConClose();
            return users;
        }

        // 이메일 중복 체크
        public int EmailCheck(UserEntity obj)
        {
            int result = -1;
            try
            {
                Conn();
                ConOpen();
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
                ConClose();
            }
            return result;
        }
    }
}