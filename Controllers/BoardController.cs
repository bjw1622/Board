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
        Boards boards = new Boards();


        // 게시판 데이터 가져오기
        public ActionResult Index()
        {
            //Session["Name"].ToString();
            ViewBag.Board = boards.GetBoardList();
            return View();
        }

        [HttpPost]
        // 페이징
        public JsonResult IndexPaging(PageEntity obj)
        {
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
        public ActionResult Write(BoardFileEntity obj)
        {
            // board 데이터
            boards.WriteBoard(obj);
            boards.WriteBoardFile(obj);
            return RedirectToAction("Index", "Board");
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
        public JsonResult Update(BoardEntity obj)
        {
            boards.UpdateBoard(obj);
            return Json(obj);
        }

        // 상세 페이지 - 삭제
        public ActionResult Delete(int boardNum)
        {
            boards.DeleteBoard(boardNum);
            return RedirectToAction("Index", "Board");
        }

        // 상세 페이지 - 추천 업데이트
        [HttpPost]
        public JsonResult RecommandUpdate(BoardEntity obj)
        {
            boards.RecommandCountUpdate(obj);
            return Json(obj);
        }

        // 인덱스 페이지 - 검색과 페이징 기능
        [HttpPost]
        public JsonResult PageAndFind(PageAndFindEntity obj)
        {
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
            // 댓글 추가하고
            boards.AddReply(obj);

            // 댓글 개수 불러오는 sq
            ViewBag.replyList = boards.ReadReply(obj.BoardNum);

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
            return Json(boards.ReadReReply(obj));
        }


    }
}