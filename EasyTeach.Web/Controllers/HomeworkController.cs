using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyTeach.Web.Controllers
{
    public class HomeworkController : Controller
    {
        //
        // GET: /Homework/
        public ActionResult Homeworks()
        {
            return View();
        }

        public ActionResult Grid()
        {
            return View();
        }

        public ActionResult AddHomework()
        {
            return View();
        }
	}
}