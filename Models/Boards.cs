using Board.Entitys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Board.Models
{
    public class Boards
    {
        private SqlConnection con;
        private List<ReplyEntity> replyEntity = new List<ReplyEntity>();

        public void Conn()
        {
            string constr = ConfigurationManager.ConnectionStrings["BoardDB"].ToString();
            con = new SqlConnection(constr);
        }

        // 게시판 글쓰기 기능
        public void WriteBoard(BoardFileEntity obj)
        {
            Conn();
            con.Open();
            using (SqlCommand com = new SqlCommand("dbo.WriteBoard", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", 0);
                com.Parameters.AddWithValue("@Title", obj.Title);
                com.Parameters.AddWithValue("@Name", obj.Name);
                com.Parameters.AddWithValue("@MainContent", obj.MainContent);
                com.Parameters.AddWithValue("@Email", obj.Email);
                com.Parameters.AddWithValue("@ReplyCount", obj.ReplyCount);
                com.Parameters.AddWithValue("@RecommandCount", obj.RecommandCount);
                com.ExecuteNonQuery();
            }
            con.Close();
            con.Dispose();
        }

        // 글쓰기 첨부파일 추가
        public void WriteBoardFile(BoardFileEntity obj)
        {
            Conn();
            con.Open();
            using (SqlCommand com = new SqlCommand("dbo.WriteBoardFile", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", 0);
                com.Parameters.AddWithValue("@FileNum", 0);
                com.Parameters.AddWithValue("@FileName", obj.FileName);
                com.Parameters.AddWithValue("@FileName2", obj.FileName2);
                com.ExecuteNonQuery();
            }
            con.Close();
            con.Dispose();
        }

        // 게시판 목록 가져오기
        public List<BoardEntity> GetBoardList()
        {
            List<BoardEntity> boardEntity = new List<BoardEntity>();
            Conn();
            con.Open();
            using (SqlCommand com = new SqlCommand("dbo.SelectBoard", con))
            {
                com.CommandType = CommandType.StoredProcedure;
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

        // 게시판 목록 가져오기
        public List<FileEntity> GetFileImg(int boardNum)
        {
            List<FileEntity> fileimg = new List<FileEntity>();
            Conn();
            con.Open();
            using (SqlCommand com = new SqlCommand("dbo.SelectFileName", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", boardNum);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    FileEntity files = new FileEntity();
                    files.FileName = Convert.ToString(reader["FileName"]);
                    files.FileName2 = Convert.ToString(reader["FileName2"]);
                    fileimg.Add(files);
                }
            }
            con.Close();
            con.Dispose();
            return fileimg;
        }


        // 게시판 전체 목록 가져오기
        public List<BoardEntity> GetTopBoardList()
        {
            List<BoardEntity> boardEntity = new List<BoardEntity>();
            Conn();
            con.Open();
            using (SqlCommand com = new SqlCommand("dbo.SelectBoardByTop", con))
            {
                com.CommandType = CommandType.StoredProcedure;
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
                    boards.Email = Convert.ToString(reader["Email"]);

                }
            }
            con.Close();
            con.Dispose();
            return boards;
        }

        // Update 게시판
        public void UpdateBoard(BoardEntity obj)
        {
            try {
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
        public void RecommandCountUpdate(RecommandEntity obj)
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
            List<BoardEntity> boardEntity = new List<BoardEntity>();
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
            List<BoardEntity> boardEntity = new List<BoardEntity>();
            try
            {
                Conn();
                con.Open();
                using (SqlCommand com = new SqlCommand("dbo.FindingAndPaging", con))
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return boardEntity;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return boardEntity;
        }

        // 검색 했을 때의 페이지 수 가져오기
        public int FindBoardCount(PageAndFindEntity obj)
        {
            int result = 0;
            try
            {
                Conn();
                con.Open();
                using (SqlCommand com = new SqlCommand("dbo.FindBoardListCount", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Variable", obj.Variable);
                    com.Parameters.AddWithValue("@Input", obj.Input);
                    result = (int)(com.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return result;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return result;
        }

        // 댓글 추가
        public List<ReplyEntity> AddReply(ReplyEntity obj)
        {
            try
            {
                Conn();
                con.Open();
                using (SqlCommand com = new SqlCommand("dbo.InsertReply", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
                    com.Parameters.AddWithValue("@ReplyID", obj.ReplyID);
                    com.Parameters.AddWithValue("@ReplyContent", obj.ReplyContent);
                    com.Parameters.AddWithValue("@ParentReplyID", obj.ParentReplyID);
                    com.Parameters.AddWithValue("@Email", obj.Email);
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        ReplyEntity replys = new ReplyEntity();
                        replys.BoardNum = Convert.ToInt32(reader["BoardNum"]);
                        replys.ReplyID = Convert.ToInt32(reader["ReplyID"]);
                        replys.ReplyContent = Convert.ToString(reader["ReplyContent"]);
                        replys.Email = Convert.ToString(reader["Email"]);
                        replyEntity.Add(replys);
                    }
                }
                return replyEntity;
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
            return replyEntity;

        }

        // 상세페이지 - 댓글 불러오기
        public List<ReplyEntity> ReadReply(int boardNum)
        {
            Conn();
            con.Open();
            ReplyEntity boards = new ReplyEntity();
            using (SqlCommand com = new SqlCommand("dbo.SelectReply", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", boardNum);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    ReplyEntity replys = new ReplyEntity();
                    replys.BoardNum = Convert.ToInt32(reader["BoardNum"]);
                    replys.ReplyID = Convert.ToInt32(reader["ReplyID"]);
                    replys.ReplyContent = Convert.ToString(reader["ReplyContent"]);
                    replys.Email = Convert.ToString(reader["Email"]);
                    replys.ParentReplyID = Convert.ToInt32(reader["ParentReplyID"]);
                    replyEntity.Add(replys);

                }
            }
            con.Close();
            con.Dispose();
            return replyEntity;
        }

        public int GetReplyID(int boardNum)
        {
            // ReplyID 가장 큰 값
            int result;

            Conn();
            con.Open();
            ReplyEntity boards = new ReplyEntity();
            using (SqlCommand com = new SqlCommand("dbo.GetReplyID", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", boardNum);
                Object nullCheck = com.ExecuteScalar();
                if (nullCheck.Equals(DBNull.Value))
                {
                    result = 0;
                }
                else
                {
                    result = (int)com.ExecuteScalar();
                }
            }
            con.Close();
            con.Dispose();
            return result;
        }

        // 상세페이지 - 대댓글 불러오기
        public List<ReplyEntity> ReadReReply(ReplyEntity obj)
        {
            Conn();
            con.Open();
            ReplyEntity boards = new ReplyEntity();
            using (SqlCommand com = new SqlCommand("dbo.SelectReReply", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
                com.Parameters.AddWithValue("@ParentReplyID", obj.ParentReplyID);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    ReplyEntity replys = new ReplyEntity();
                    replys.BoardNum = Convert.ToInt32(reader["BoardNum"]);
                    replys.ReplyID = Convert.ToInt32(reader["ReplyID"]);
                    replys.ReplyContent = Convert.ToString(reader["ReplyContent"]);
                    replys.ParentReplyID = Convert.ToInt32(reader["ParentReplyID"]);
                    replys.Email = Convert.ToString(reader["Email"]);
                    replyEntity.Add(replys);

                }
            }
            con.Close();
            con.Dispose();
            return replyEntity;
        }

        public void UpdateReplyCount(int boardNum, int replyCount)
        {
            Conn();
            con.Open();
            ReplyEntity boards = new ReplyEntity();
            using (SqlCommand com = new SqlCommand("dbo.BoardReply", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", boardNum);
                com.Parameters.AddWithValue("@ReplyCount", replyCount);
                SqlDataReader reader = com.ExecuteReader();
            }
            con.Close();
            con.Dispose();
        }

        public int GetRecommandNumber(RecommandEntity obj)
        {
            // ReplyID 가장 큰 값
            int result;
            Conn();
            con.Open();
            using (SqlCommand com = new SqlCommand("dbo.GetRecommandNumber", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
                com.Parameters.AddWithValue("@Email", obj.Email);
                Object nullCheck = com.ExecuteScalar();
                if (nullCheck == null)
                {
                    //존재 X
                    result = -1;
                }
                else
                {
                    // 0또는 1 반환
                    result = (int)com.ExecuteScalar();
                }
            }
            con.Close();
            con.Dispose();
            return result;
        }

        public void UpdateRecommand(RecommandEntity obj)
        {
            Conn();
            con.Open();
            ReplyEntity boards = new ReplyEntity();
            using (SqlCommand com = new SqlCommand("dbo.UpdateRecommand", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
                com.Parameters.AddWithValue("@Email", obj.Email);
                SqlDataReader reader = com.ExecuteReader();
            }
            con.Close();
            con.Dispose();
        }
        public void SetRecomandDisabled(RecommandEntity obj)
        {
            Conn();
            con.Open();
            ReplyEntity boards = new ReplyEntity();
            using (SqlCommand com = new SqlCommand("dbo.SetRecommandDisabled", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
                com.Parameters.AddWithValue("@Email", obj.Email);
                SqlDataReader reader = com.ExecuteReader();
            }
            con.Close();
            con.Dispose();
        }

        public void SetRecomandActive(RecommandEntity obj)
        {
            Conn();
            con.Open();
            ReplyEntity boards = new ReplyEntity();
            using (SqlCommand com = new SqlCommand("dbo.SetRecommandActive", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
                com.Parameters.AddWithValue("@Email", obj.Email);
                SqlDataReader reader = com.ExecuteReader();
            }
            con.Close();
            con.Dispose();
        }

        // 댓글 삭제
        public void RemoveReply(ReplyEntity obj)
        {
            Conn();
            con.Open();
            ReplyEntity boards = new ReplyEntity();
            using (SqlCommand com = new SqlCommand("dbo.RemoveReply", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
                com.Parameters.AddWithValue("@ReplyID", obj.ReplyID);
                SqlDataReader reader = com.ExecuteReader();
            }
            con.Close();
            con.Dispose();
        }

        // 자식 댓글 삭제
        public void RemoveReReply(ReplyEntity obj)
        {
            Conn();
            con.Open();
            ReplyEntity boards = new ReplyEntity();
            using (SqlCommand com = new SqlCommand("dbo.RemoveReReply", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
                com.Parameters.AddWithValue("@ReplyID", obj.ReplyID);
                SqlDataReader reader = com.ExecuteReader();
            }
            con.Close();
            con.Dispose();
        }
    }

}