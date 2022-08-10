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
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserEntity obj)
        {
            if (ModelState.IsValid)
            {
                user.AddUser(obj);
                return RedirectToAction("Index", "Board");
            }
            return View(obj);
        }
    }
}

