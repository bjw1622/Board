﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Board.Controllers
{
    public class BoardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}