﻿using Board.Entitys;
using Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace Board.Controllers
{
    public class UserController : Controller
    {
        User user = new User();
        UserEntity userEntity = new UserEntity();
        
        // 회원 가입
        public ActionResult SignUp()
        {
            return View();
        }

        // 회원 가입
        [HttpPost]
        public ActionResult SignUp(UserEntity obj)
        {
            // Entity 에서 required 해둔것들이 모두 충족되면 True
            if (ModelState.IsValid)
            {
                user.AddUser(obj);
                return RedirectToAction("Index", "Board");
            }
            return View(obj);
        }

        // 로그인
        public ActionResult LogIn()
        {
            return View();
        }

        // 로그인
        [HttpPost]
        public ActionResult LogIn(UserEntity obj)
        {
            var result = user.LogIn(obj);
            // 로그인 성공
            if(result == 1)
            {
                Session["Email"] = obj.Email;
                return RedirectToAction("Index", "Board");
            }
            // 로그인 실패
            MessageBox.Show("회원 정보가 없습니다.");
            return View();
        }

        //
        [HttpPost]
        public ActionResult EmailCheck(UserEntity obj)
        {
            var result = user.EmailCheck(obj);
            // 이메일 중복
            if (result == 1)
            {
                MessageBox.Show("이미 등록된 이메일 입니다!!");
            }
            // 정상 작동
            return View();
        }
    }
}

