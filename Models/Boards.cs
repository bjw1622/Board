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

        // 게시판 글쓰기 기능
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

        // 게시판 목록 가져오기
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

        // 게시판 전체 목록 가져오기
        public List<BoardEntity> GetTopBoardList()
        {
            Conn();
            con.Open();
            // 사용할 프로시저의 이름을 설정
            using (SqlCommand com = new SqlCommand("dbo.SelectBoardByTop", con))
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

        // 게시판 상세페이지 이동
        public BoardEntity DetailBoard(int boardNum)
        {
            Conn();
            con.Open();
            BoardEntity boards = new BoardEntity();
            // 사용할 프로시저의 이름을 설정
            using (SqlCommand com = new SqlCommand("dbo.DetailBoard", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", boardNum);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    boards.Title = Convert.ToString(reader["Title"]);
                    boards.Name = Convert.ToString(reader["Name"]);
                    boards.MainContent = Convert.ToString(reader["MainContent"]);
                    boards.RecommandCount = Convert.ToInt32(reader["RecommandCount"]);
                }
            }
            con.Close();
            con.Dispose();
            return boards;
        }

        // Update 게시판
        public void UpdateBoard(BoardEntity obj)
        {
            Conn();
            con.Open();
            using (SqlCommand com = new SqlCommand("dbo.UpdateBoard", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
                com.Parameters.AddWithValue("@Title", obj.Title);
                com.Parameters.AddWithValue("@MainContent", obj.MainContent);
                SqlDataReader reader = com.ExecuteReader();
            }
            con.Close();
            con.Dispose();
        }

        // Delete 게시판
        public void DeleteBoard(int boardNum)
        {
            Conn();
            con.Open();
            using (SqlCommand com = new SqlCommand("dbo.DeleteBoard", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", boardNum);
                SqlDataReader reader = com.ExecuteReader();
            }
            con.Close();
            con.Dispose();
        }

        // 상세 페이지 - 추천 업데이트
        public void RecommandCountUpdate(BoardEntity obj)
        {
            Conn();
            con.Open();
            using (SqlCommand com = new SqlCommand("dbo.UpdateRecommandCount", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
                com.Parameters.AddWithValue("@RecommandCount", obj.RecommandCount);
                SqlDataReader reader = com.ExecuteReader();
            }
            con.Close();
            con.Dispose();
        }

        // 페이징
        public List<BoardEntity> PagingBoardList(PageEntity obj)
        {
            Conn();
            con.Open();
            using (SqlCommand com = new SqlCommand("dbo.PagingBoard", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@PageCount", obj.PageCount);
                com.Parameters.AddWithValue("@PageNumber", obj.PageNumber);
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

        // 검색과 페이징
        public List<BoardEntity> PagingAndFindingBoardList(PageAndFindEntity obj)
        {
            Conn();
            con.Open();
            using(SqlCommand com = new SqlCommand("dbo.FindingAndPaging", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@PageCount", obj.PageCount);
                com.Parameters.AddWithValue("@PageNumber", obj.PageNumber);
                com.Parameters.AddWithValue("@Variable", obj.Variable);
                com.Parameters.AddWithValue("@Input", obj.Input);
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
