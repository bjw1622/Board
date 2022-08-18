using Board.Entitys;
using Board.Models;
using System;
using System.Collections.Generic;
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
            ViewBag.Board = boards.GetBoardList();
            // 게시판 전체 갯수
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
        public ActionResult Write(BoardEntity obj)
        {
            boards.WriteBoard(obj);
            return RedirectToAction("Index", "Board");
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

        // 상세 페이지 - 댓글 추가 기능
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
    }
}