using Board.Entitys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Board.Models
{
    public class Boards : Connection
    {
        // 게시판 글쓰기 기능
        public void WriteBoard(BoardEntity obj)
        {
            try
            {
                Conn();
                ConOpen();
                using (SqlCommand com = new SqlCommand("dbo.WriteBoard", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Title", obj.Title);
                    com.Parameters.AddWithValue("@Content", obj.Content);
                    com.Parameters.AddWithValue("@CreateDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    com.Parameters.AddWithValue("@Email", obj.Email);
                    com.ExecuteNonQuery();
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
        }

        //        // 글쓰기 첨부파일 추가
        //        public void WriteBoardFile(BoardFileEntity obj)
        //        {
        //            Conn();
        //            ConOpen();
        //            using (SqlCommand com = new SqlCommand("dbo.WriteBoardFile", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.AddWithValue("@BoardNum", 0);
        //                com.Parameters.AddWithValue("@FileNum", 0);
        //                com.Parameters.AddWithValue("@FileName", obj.FileName);
        //                com.Parameters.AddWithValue("@FileName2", obj.FileName2);
        //                com.ExecuteNonQuery();
        //            }
        //            ConClose();
        //        }

        // 게시판 목록 가져오기
        public List<BoardEntity> GetBoardList()
        {
            List<BoardEntity> boardEntity = new List<BoardEntity>();
            try
            {
                Conn();
                ConOpen();
                using (SqlCommand com = new SqlCommand("dbo.SelectBoard", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        BoardEntity boards = new BoardEntity();
                        boards.No = Convert.ToInt32(reader["No"]);
                        boards.Title = Convert.ToString(reader["Title"]);
                        boards.Name = Convert.ToString(reader["Name"]);
                        boardEntity.Add(boards);
                    }
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
            return boardEntity;
        }

        //        public List<FileEntity> GetFileImg(int boardNum)
        //        {
        //            List<FileEntity> fileimg = new List<FileEntity>();
        //            Conn();
        //            ConOpen();
        //            using (SqlCommand com = new SqlCommand("dbo.SelectFileName", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.AddWithValue("@BoardNum", boardNum);
        //                SqlDataReader reader = com.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    FileEntity files = new FileEntity();
        //                    files.FileName = Convert.ToString(reader["FileName"]);
        //                    files.FileName2 = Convert.ToString(reader["FileName2"]);
        //                    fileimg.Add(files);
        //                }
        //            }
        //            ConClose();
        //            return fileimg;
        //        }


        //        // 게시판 전체 목록 가져오기
        //        public List<BoardEntity> GetTopBoardList()
        //        {
        //            List<BoardEntity> boardEntity = new List<BoardEntity>();
        //            Conn();
        //            ConOpen();
        //            using (SqlCommand com = new SqlCommand("dbo.SelectBoardByTop", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                SqlDataReader reader = com.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    BoardEntity boards = new BoardEntity();
        //                    boards.BoardNum = Convert.ToInt32(reader["BoardNum"]);
        //                    boards.Title = Convert.ToString(reader["Title"]);
        //                    boards.Name = Convert.ToString(reader["Name"]);
        //                    boards.ReplyCount = Convert.ToInt32(reader["ReplyCount"]);
        //                    boards.RecommandCount = Convert.ToInt32(reader["RecommandCount"]);
        //                    boardEntity.Add(boards);
        //                }
        //            }
        //            ConClose();
        //            return boardEntity;
        //        }

        // 게시판 상세페이지 이동
        public BoardEntity DetailBoard(int boardNum)
        {
            BoardEntity boards = new BoardEntity();
            try
            {
                Conn();
                ConOpen();
                using (SqlCommand com = new SqlCommand("dbo.DetailBoard", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@No", boardNum);
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        boards.Title = Convert.ToString(reader["Title"]);
                        boards.Content = Convert.ToString(reader["Content"]);
                        boards.User_No = Convert.ToInt32(reader["User_No"]);
                        boards.Name = Convert.ToString(reader["Name"]);
                        boards.Email = Convert.ToString(reader["Email"]);
                    }
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
            return boards;
        }

        // Update 게시판
        public void UpdateBoard(BoardEntity obj)
        {
            Conn();
            ConOpen();
            using (SqlCommand com = new SqlCommand("dbo.UpdateBoard", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@No", obj.No);
                com.Parameters.AddWithValue("@Title", obj.Title);
                com.Parameters.AddWithValue("@Content", obj.Content);
                com.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                SqlDataReader reader = com.ExecuteReader();
            }
            ConClose();
        }

        //        // Delete 게시판
        //        public void DeleteBoard(int boardNum)
        //        {
        //            Conn();
        //            ConOpen();
        //            using (SqlCommand com = new SqlCommand("dbo.DeleteBoard", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.AddWithValue("@BoardNum", boardNum);
        //                SqlDataReader reader = com.ExecuteReader();
        //            }
        //            ConClose();
        //        }

        //        // 상세 페이지 - 추천 업데이트
        //        public void RecommandCountUpdate(RecommandEntity obj)
        //        {
        //            Conn();
        //            ConOpen();
        //            using (SqlCommand com = new SqlCommand("dbo.UpdateRecommandCount", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
        //                com.Parameters.AddWithValue("@RecommandCount", obj.RecommandCount);
        //                SqlDataReader reader = com.ExecuteReader();
        //            }
        //            ConClose();
        //        }

        // 페이징
        public List<BoardEntity> PagingBoardList(PageEntity obj)
        {
            List<BoardEntity> boardEntity = new List<BoardEntity>();
            try
            {
                Conn();
                ConOpen();
                using (SqlCommand com = new SqlCommand("dbo.PagingBoard", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@PageCount", obj.PageCount);
                    com.Parameters.AddWithValue("@PageNumber", obj.PageNumber);
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        BoardEntity boards = new BoardEntity();
                        boards.No = Convert.ToInt32(reader["No"]);
                        boards.Title = Convert.ToString(reader["Title"]);
                        boards.Name = Convert.ToString(reader["Name"]);
                        boardEntity.Add(boards);
                    }
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
            return boardEntity;
        }

        // 검색과 페이징
        public List<BoardEntity> PagingAndFindingBoardList(PageAndFindEntity obj)
        {
            List<BoardEntity> boardEntity = new List<BoardEntity>();
            try
            {
                Conn();
                ConOpen();
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
                        boards.No = Convert.ToInt32(reader["No"]);
                        boards.Title = Convert.ToString(reader["Title"]);
                        boards.Name = Convert.ToString(reader["Name"]);
                        boardEntity.Add(boards);
                    }
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
            return boardEntity;
        }

        // 검색 했을 때의 페이지 수 가져오기
        public int FindBoardCount(PageAndFindEntity obj)
        {
            int result = 0;
            try
            {
                Conn();
                ConOpen();
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
                ConClose();
            }
            return result;
        }

        //        // 댓글 추가
        //        public List<ReplyEntity> AddReply(ReplyEntity obj)
        //        {
        //            List<ReplyEntity> replyEntity = new List<ReplyEntity>();
        //            try
        //            {
        //                Conn();
        //                ConOpen();
        //                using (SqlCommand com = new SqlCommand("dbo.InsertReply", con))
        //                {
        //                    com.CommandType = CommandType.StoredProcedure;
        //                    com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
        //                    com.Parameters.AddWithValue("@ReplyID", obj.ReplyID);
        //                    com.Parameters.AddWithValue("@ReplyContent", obj.ReplyContent);
        //                    com.Parameters.AddWithValue("@ParentReplyID", obj.ParentReplyID);
        //                    com.Parameters.AddWithValue("@Email", obj.Email);
        //                    SqlDataReader reader = com.ExecuteReader();
        //                    while (reader.Read())
        //                    {
        //                        ReplyEntity replys = new ReplyEntity();
        //                        replys.BoardNum = Convert.ToInt32(reader["BoardNum"]);
        //                        replys.ReplyID = Convert.ToInt32(reader["ReplyID"]);
        //                        replys.ReplyContent = Convert.ToString(reader["ReplyContent"]);
        //                        replys.Email = Convert.ToString(reader["Email"]);
        //                        replyEntity.Add(replys);
        //                    }
        //                }
        //                return replyEntity;
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine(e);
        //            }
        //            finally
        //            {
        //                ConClose();
        //            }
        //            return replyEntity;

        //        }

        //        // 상세페이지 - 댓글 불러오기
        //        public List<ReplyEntity> ReadReply(int boardNum)
        //        {
        //            List<ReplyEntity> replyEntity = new List<ReplyEntity>();
        //            Conn();
        //            ConOpen();
        //            ReplyEntity boards = new ReplyEntity();
        //            using (SqlCommand com = new SqlCommand("dbo.SelectReply", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.AddWithValue("@BoardNum", boardNum);
        //                SqlDataReader reader = com.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    ReplyEntity replys = new ReplyEntity();
        //                    replys.BoardNum = Convert.ToInt32(reader["BoardNum"]);
        //                    replys.ReplyID = Convert.ToInt32(reader["ReplyID"]);
        //                    replys.ReplyContent = Convert.ToString(reader["ReplyContent"]);
        //                    replys.Email = Convert.ToString(reader["Email"]);
        //                    replys.ParentReplyID = Convert.ToInt32(reader["ParentReplyID"]);
        //                    replyEntity.Add(replys);

        //                }
        //            }
        //            ConClose();
        //            return replyEntity;
        //        }

        //        public int GetReplyID(int boardNum)
        //        {
        //            // ReplyID 가장 큰 값
        //            int result;
        //            List<ReplyEntity> replyEntity = new List<ReplyEntity>();
        //            Conn();
        //            ConOpen();
        //            ReplyEntity boards = new ReplyEntity();
        //            using (SqlCommand com = new SqlCommand("dbo.GetReplyID", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.AddWithValue("@BoardNum", boardNum);
        //                Object nullCheck = com.ExecuteScalar();
        //                if (nullCheck.Equals(DBNull.Value))
        //                {
        //                    result = 0;
        //                }
        //                else
        //                {
        //                    result = (int)com.ExecuteScalar();
        //                }
        //            }
        //            ConClose();
        //            return result;
        //        }

        //        // 상세페이지 - 대댓글 불러오기
        //        public List<ReplyEntity> ReadReReply(ReplyEntity obj)
        //        {
        //            List<ReplyEntity> replyEntity = new List<ReplyEntity>();
        //            Conn();
        //            ConOpen();
        //            ReplyEntity boards = new ReplyEntity();
        //            using (SqlCommand com = new SqlCommand("dbo.SelectReReply", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
        //                com.Parameters.AddWithValue("@ParentReplyID", obj.ParentReplyID);
        //                SqlDataReader reader = com.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    ReplyEntity replys = new ReplyEntity();
        //                    replys.BoardNum = Convert.ToInt32(reader["BoardNum"]);
        //                    replys.ReplyID = Convert.ToInt32(reader["ReplyID"]);
        //                    replys.ReplyContent = Convert.ToString(reader["ReplyContent"]);
        //                    replys.ParentReplyID = Convert.ToInt32(reader["ParentReplyID"]);
        //                    replys.Email = Convert.ToString(reader["Email"]);
        //                    replyEntity.Add(replys);

        //                }
        //            }
        //            ConClose();
        //            return replyEntity;
        //        }

        //        public void UpdateReplyCount(int boardNum, int replyCount)
        //        {
        //            Conn();
        //            ConOpen();
        //            ReplyEntity boards = new ReplyEntity();
        //            using (SqlCommand com = new SqlCommand("dbo.BoardReply", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.AddWithValue("@BoardNum", boardNum);
        //                com.Parameters.AddWithValue("@ReplyCount", replyCount);
        //                SqlDataReader reader = com.ExecuteReader();
        //            }
        //            ConClose();
        //        }

        //        public int GetRecommandNumber(RecommandEntity obj)
        //        {
        //            // ReplyID 가장 큰 값
        //            int result;
        //            Conn();
        //            ConOpen();
        //            using (SqlCommand com = new SqlCommand("dbo.GetRecommandNumber", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
        //                com.Parameters.AddWithValue("@Email", obj.Email);
        //                Object nullCheck = com.ExecuteScalar();
        //                if (nullCheck == null)
        //                {
        //                    //존재 X
        //                    result = -1;
        //                }
        //                else
        //                {
        //                    // 0또는 1 반환
        //                    result = (int)com.ExecuteScalar();
        //                }
        //            }
        //            ConClose();
        //            return result;
        //        }

        //        public void UpdateRecommand(RecommandEntity obj)
        //        {
        //            Conn();
        //            ConOpen();
        //            ReplyEntity boards = new ReplyEntity();
        //            using (SqlCommand com = new SqlCommand("dbo.UpdateRecommand", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
        //                com.Parameters.AddWithValue("@Email", obj.Email);
        //                SqlDataReader reader = com.ExecuteReader();
        //            }
        //            ConClose();
        //        }
        //        public void SetRecomandDisabled(RecommandEntity obj)
        //        {
        //            Conn();
        //            ConOpen();
        //            ReplyEntity boards = new ReplyEntity();
        //            using (SqlCommand com = new SqlCommand("dbo.SetRecommandDisabled", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
        //                com.Parameters.AddWithValue("@Email", obj.Email);
        //                SqlDataReader reader = com.ExecuteReader();
        //            }
        //            ConClose();
        //        }

        //        public void SetRecomandActive(RecommandEntity obj)
        //        {
        //            Conn();
        //            ConOpen();
        //            ReplyEntity boards = new ReplyEntity();
        //            using (SqlCommand com = new SqlCommand("dbo.SetRecommandActive", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
        //                com.Parameters.AddWithValue("@Email", obj.Email);
        //                SqlDataReader reader = com.ExecuteReader();
        //            }
        //            ConClose();
        //        }

        //        // 댓글 삭제
        //        public void RemoveReply(ReplyEntity obj)
        //        {
        //            Conn();
        //            ConOpen();
        //            ReplyEntity boards = new ReplyEntity();
        //            using (SqlCommand com = new SqlCommand("dbo.RemoveReply", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
        //                com.Parameters.AddWithValue("@ReplyID", obj.ReplyID);
        //                SqlDataReader reader = com.ExecuteReader();
        //            }
        //            ConClose();
        //        }

        //        // 자식 댓글 삭제
        //        public void RemoveReReply(ReplyEntity obj)
        //        {
        //            Conn();
        //            ConOpen();
        //            ReplyEntity boards = new ReplyEntity();
        //            using (SqlCommand com = new SqlCommand("dbo.RemoveReReply", con))
        //            {
        //                com.CommandType = CommandType.StoredProcedure;
        //                com.Parameters.AddWithValue("@BoardNum", obj.BoardNum);
        //                com.Parameters.AddWithValue("@ReplyID", obj.ReplyID);
        //                SqlDataReader reader = com.ExecuteReader();
        //            }
        //            ConClose();
        //        }
    }

}