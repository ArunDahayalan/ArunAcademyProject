using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademyApplication.Models;

namespace AcademyApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FacultyLogin()
        {
            return View();
        }

        public JsonResult Login(Students student)
        {
            Login login = new Login();
            string result = login.StudenLogin(student);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Register()
        {
           string isAdmin =  System.Web.HttpContext.Current.Request.Cookies["AdminCookie"]["IsAdmin"];
            if (isAdmin == "True")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public JsonResult ResisterStudent(Students student)
        {
            Login login = new Login();
            string result = login.StudentRegister(student);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}