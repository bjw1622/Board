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
    public class Boards
    {
        int num = 0;
        // sqlConnection 
        private SqlConnection con;
        List<BoardEntity> boardEntity = new List<BoardEntity>();

        public void Conn()
        {
            string constr = ConfigurationManager.ConnectionStrings["BoardDB"].ToString();
            con = new SqlConnection(constr);
        }

        // 회원 가입
        public void WriteBoard(BoardEntity obj)
        {
            Conn();
            con.Open();
            // 사용할 프로시저의 이름을 설정
            using (SqlCommand com = new SqlCommand("dbo.WriteBoard", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", num);
                com.Parameters.AddWithValue("@Title", obj.Title);
                com.Parameters.AddWithValue("@Name", obj.Name);
                com.Parameters.AddWithValue("@MainContent", obj.MainContent);
                com.Parameters.AddWithValue("@ReplyCount", obj.ReplyCount);
                com.Parameters.AddWithValue("@RecommandCount", obj.RecommandCount);
                com.ExecuteNonQuery();
                num += 1;
            }
            con.Close();
            con.Dispose();
        }

        // 회원 가입
        public List<BoardEntity> GetBoardList()
        {
            Conn();
            con.Open();
            // 사용할 프로시저의 이름을 설정
            using (SqlCommand com = new SqlCommand("dbo.SelectBoard", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.ExecuteNonQuery();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    BoardEntity boards = new BoardEntity();
                    boards.BoardNum = Convert.ToInt32(reader["BoardNum"]);
                    boards.Title = Convert.ToString(reader["Title"]);
                    boards.Name = Convert.ToString(reader["Name"]);
                    boards.ReplyCount = Convert.ToInt32(reader["ReplyCount"]);
                    boards.RecommandCount = Convert.ToInt32(reader["RecommandCount"]);
                    boardEntity.Add(boards);
                }
            }
            con.Close();
            con.Dispose();
            return boardEntity;
        }
    }
}