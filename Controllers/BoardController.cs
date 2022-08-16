﻿using Board.Entitys;
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
            ViewBag.Board = boards.GetBoardList();
            Console.WriteLine((ViewBag.Board).GetType());
            
            // 게시판 전체 갯수
            var boardCount = ViewBag.Board.Count;
            return View();
        }

        [HttpPost]
        //Index에서 검색 조건으로 조회
        public JsonResult IndexFinding(FindEntity obj)
        {
            List<BoardEntity> boar = boards.FindBoardList(obj);
            return Json(boar);

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
            return View(boards.DetailBoard(boardNum));
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
    }
}