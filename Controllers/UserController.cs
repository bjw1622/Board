using Board.Entitys;
using Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Board.Controllers
{
    public class UserController : Controller
    {
        User user = new User();
        // GET: User
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUsers(UserEntity obj)
        {
            user.AddUser(obj);
            return View();
        }

        //이메일 중복 체크
        //public ActionResult CheckEmail(string email)
        //{
        //    user.CheckEmail(email);
        //    return View();
        //}

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LoginCheck(string Email, string Pw)
        {
            var check = user.LoginCheck(Email,Pw);
            // 로그인 성공
            if(check == 1)
            {
                return View();
            }
            //로그인 실패
            else
            {
            }
        }
        
    }
}

