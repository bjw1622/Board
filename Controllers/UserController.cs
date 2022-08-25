using Board.Entitys;
using Board.Models;
using System.Web.Mvc;

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
            if (result.Count != 0)
            {
                Session["Name"] = result[0].Name;
                Session["Email"] = result[0].Email;
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

