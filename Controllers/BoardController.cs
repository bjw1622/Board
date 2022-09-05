using Board.Entitys;
using Board.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Board.Controllers
{
    public class BoardController : Controller
    {
        // 게시판 데이터 가져오기
        public ActionResult Index()
        {
            if (Session["Email"] != null)
            {
                Boards boards = new Boards();
                ViewBag.Board = boards.GetBoardList();
                return View();
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        // 페이징
        public JsonResult IndexPaging(PageEntity obj)
        {
            Boards boards = new Boards();
            List<BoardEntity> boar = boards.PagingBoardList(obj);
            return Json(boar);

        }

        // 글쓰기
        public ActionResult Write()
        {
            return View();
        }

        [HttpPost]
        // 글쓰기
        public void Write(BoardFileEntity obj)
        {
            Boards boards = new Boards();
            boards.WriteBoard(obj);
            boards.WriteBoardFile(obj);
        }

        [HttpPost]
        public void UploadFiles()
        {
            if (Request.Files.Count > 0)
            {
                var files = Request.Files;

                //iterating through multiple file collection   
                foreach (string str in files)
                {
                    HttpPostedFileBase file = Request.Files[str] as HttpPostedFileBase;
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                    }
                }
            }
        }

        // 상세 페이지
        public ActionResult Detail(int boardNum)
        {
            Boards boards = new Boards();
            // 게시판 상세 정보 가져오기
            ViewBag.detailInfo = boards.DetailBoard(boardNum);
            // 댓글 가져오기
            ViewBag.replyList = boards.ReadReply(boardNum);
            // ReplyID 최신화 하기
            ViewBag.MaxReplyID = boards.GetReplyID(boardNum);
            // 첨부파일 이미지 경로 가져오기
            ViewBag.GetFileImg = boards.GetFileImg(boardNum);
            return View();
        }

        [HttpPost]
        // 상세 페이지 - 수정
        public void Update(BoardEntity obj)
        {
            Boards boards = new Boards();
            boards.UpdateBoard(obj);
        }

        // 상세 페이지 - 삭제
        public ActionResult Delete(int boardNum)
        {
            Boards boards = new Boards();
            boards.DeleteBoard(boardNum);
            return RedirectToAction("Index", "Board");
        }

        // 상세 페이지 - 추천 업데이트
        [HttpPost]
        public JsonResult RecommandUpdate(RecommandEntity obj)
        {
            Boards boards = new Boards();
            // 보드 넘버랑 이메일로 0인지 1인지
            int recommand = boards.GetRecommandNumber(obj);
            if (recommand == -1)
            {
                obj.RecommandCount += 1;
                //테이블에 보드넘버,이메일,1로 추가하기
                boards.UpdateRecommand(obj);
            }
            if (recommand == 1)
            {
                obj.RecommandCount -= 1;
                boards.SetRecomandDisabled(obj);
            }
            if (recommand == 0)
            {
                obj.RecommandCount += 1;
                boards.SetRecomandActive(obj);

            }
            boards.RecommandCountUpdate(obj);
            return Json(
                new
                {
                    recommand,
                    obj.RecommandCount
                }
                );
        }

        // 인덱스 페이지 - 검색과 페이징 기능
        [HttpPost]
        public JsonResult PageAndFind(PageAndFindEntity obj)
        {
            Boards boards = new Boards();
            List<BoardEntity> boar = boards.PagingAndFindingBoardList(obj);
            var result = boards.FindBoardCount(obj);
            return Json(
                new
                {
                    List = boar,
                    Result = result,
                }
                );
        }

        // 상세 페이지 - 댓글 추가 기능, 대댓글 추가
        [HttpPost]
        public JsonResult Reply(ReplyEntity obj)
        {
            Boards boards = new Boards();
            // 댓글 추가하고
            boards.AddReply(obj);
            // 댓글 개수 불러오는 sq
            ViewBag.replyList = boards.ReadReply(obj.BoardNum);
            //Board 댓글 총 갯수 업데이트하기
            boards.UpdateReplyCount(obj.BoardNum, obj.ReplyID);
            // 댓글 개수 return
            return Json(new
            {
                replyList = ViewBag.replyList,
            });
        }

        // 상세 페이지 - 답글 보기
        [HttpPost]
        public JsonResult ReadReReplyList(ReplyEntity obj)
        {
            Boards boards = new Boards();
            return Json(boards.ReadReReply(obj));
        }

        // 상세 페이지 - 댓글 삭제
        [HttpPost]
        public void RemoveReply(ReplyEntity obj)
        {
            Boards boards = new Boards();
            boards.RemoveReply(obj);
            boards.UpdateReplyCount(obj.BoardNum, obj.ReplyID);
        }

        // 상세 페이지 - 답글 삭제
        [HttpPost]
        public void RemoveReReply(ReplyEntity obj)
        {
            Boards boards = new Boards();
            boards.RemoveReReply(obj);
            boards.UpdateReplyCount(obj.BoardNum, obj.ReplyID);
        }
    }
}