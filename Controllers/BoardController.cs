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
        
        // SELECT로 데이터 가져오기
        public ActionResult Index()
        {
            return View(boards.GetBoardList());
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
            return View();
        }

        // 상세 페이지
        public ActionResult Detail(int boardNum)
        {
            Console.WriteLine(boardNum);
            return View(boards.DetailBoard(boardNum));
        }

        [HttpPost]
        // 상세 페이지 - 수정
        public JsonResult Update (BoardEntity obj)
        {
            boards.UpdateBoard(obj);
            return Json(obj);
        }

        [HttpPost]
        // 상세 페이지 - 수정
        public ActionResult Delete(int boardNum)
        {
            return RedirectToAction("Index", "Board");
        }
    }
}