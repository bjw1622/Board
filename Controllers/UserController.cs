using Board.Entitys;
using Board.Models;
using System.Web.Mvc;

namespace Board.Controllers
{
    public class UserController : Controller
    {

        // 회원 가입
        public ActionResult SignUp()
        {
            return View();
        }

        // 회원 가입
        [HttpPost]
        public ActionResult SignUp(UserEntity obj)
        {
            User user = new User();
            if (ModelState.IsValid)
            {
                user.AddUser(obj);
            }
            return Content("<script language='javascript' type='text/javascript'> " +
                "alert('회원가입이 완료 되었습니다.');" +
                "location.href='/User/Login'" +
                "</script>");
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
            User user = new User();
            var result = user.LogIn(obj);
            if (result.Email != null)
            {
                Session["Name"] = result.Name;
                Session["Email"] = result.Email;
                return RedirectToAction("Index", "Board");
            }
            return Content("<script language='javascript' type='text/javascript'> " +
                "alert('로그인 정보가 일치하지 않습니다.');" +
                "location.href='/User/Login'" +
                "</script>");
        }

        [HttpPost]
        public JsonResult EmailCheck(UserEntity obj)
        {
            User user = new User();
            var result = user.EmailCheck(obj);
            return Json(result);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "User");
        }
    }
}

