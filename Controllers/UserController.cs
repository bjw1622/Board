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
        public ActionResult Index()
        {
            return View();
        }

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
    }
}

