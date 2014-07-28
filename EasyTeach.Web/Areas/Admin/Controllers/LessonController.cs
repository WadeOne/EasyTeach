using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyTeach.Web.Areas.Admin.Controllers
{
    public class LessonController : Controller
    {
        // GET: Admin/Lesson
        public ActionResult Index()
        {
            return View();
        }
    }
}