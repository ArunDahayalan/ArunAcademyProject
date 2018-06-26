using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademyApplication.Models;

namespace AcademyApplication.Controllers
{
    public class AcademyController : Controller
    {
        // GET: Aboutus
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admission()
        {
            return View();
        }

        public ActionResult Achivements()
        {
            return View();
        }

        public ActionResult Contactus()
        {
            return View();
        }

        public JsonResult ContactusFormValues(ContactFormValues formVlues)
        {
            Academy academy = new Academy();
            string contactus = academy.AddContactUsValues(formVlues);
            return Json(contactus, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SuccessStory()
        {
            return View();
        }
    }
}