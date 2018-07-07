using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademyApplication.Entity;
using AcademyApplication.Models;

namespace AcademyApplication.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Materials()
        {
            return View();
        }
        public ActionResult QuestionsCreation()
        {
            try
            {
                using (ATCACADEMYEntities academyEntity = new ATCACADEMYEntities())
                {
                    academyEntity.Database.Connection.Open();
                    ViewBag.QuestionGroup = new SelectList(academyEntity.PROC_GetQuestionGroup().ToList(), "QuestionGroupID", "QuestionGroupName");
                    ViewBag.QuestionType = new SelectList(academyEntity.PROC_GetQuestionType().ToList(), "QuestionTypeID", "QuestionTypeName");
                    academyEntity.Database.Connection.Close();
                }
            }
            catch
            {

            }
            return View();
        }
        [HttpPost]
        public JsonResult QuestionsSubmit(int questionGroup, int questionType, string question, HttpPostedFileBase[] uploadQuestion, string option1, HttpPostedFileBase[] uploadOption1, string option2, HttpPostedFileBase[] uploadOption2, string option3, HttpPostedFileBase[] uploadOption3, string option4, HttpPostedFileBase[] uploadOption4, int correctAnswer)
        {

            int insertCheck = 0;
            try
            {
                string questionPath = "~/Images/Questions/";
                if (!System.IO.Directory.Exists(questionPath))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(questionPath));
                }
                if (uploadQuestion != null)
                    uploadQuestion[0].SaveAs(Server.MapPath(questionPath + uploadQuestion[0].FileName));
                string optionsPath = "~/Images/Options/";
                if (!System.IO.Directory.Exists(optionsPath))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(optionsPath));
                }
                if (uploadOption1 != null)
                    uploadOption1[0].SaveAs(Server.MapPath(optionsPath + uploadOption1[0].FileName));

                if (uploadOption2 != null)
                    uploadOption2[0].SaveAs(Server.MapPath(optionsPath + uploadOption2[0].FileName));

                if (uploadOption3 != null)
                    uploadOption3[0].SaveAs(Server.MapPath(optionsPath + uploadOption3[0].FileName));

                if (uploadOption4 != null)
                    uploadOption4[0].SaveAs(Server.MapPath(optionsPath + uploadOption4[0].FileName));

                question = question + (uploadQuestion != null ? " ImageName: " + uploadQuestion[0].FileName : "");
                option1 = option1 + (uploadOption1 != null ? " ImageName: " + uploadOption1[0].FileName : "");
                option2 = option2 + (uploadOption2 != null ? " ImageName: " + uploadOption2[0].FileName : "");
                option3 = option3 + (uploadOption3 != null ? " ImageName: " + uploadOption3[0].FileName : "");
                option4 = option4 + (uploadOption4 != null ? " ImageName: " + uploadOption4[0].FileName : "");
                using (ATCACADEMYEntities db = new ATCACADEMYEntities())
                {
                    db.Database.Connection.Open();
                    insertCheck = db.PROC_InsertQuestions(questionGroup, questionType, question, option1, option2, option3, option4, correctAnswer);
                    db.Database.Connection.Close();
                }

                return Json(insertCheck, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(insertCheck, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult QuestionGroupCreation()
        {

            return View();
        }
        [HttpPost]
        public JsonResult QuestionGroupSubmit(string questionGroup)
        {
            int insertCheck = 0;
            try
            {
                using (ATCACADEMYEntities db = new ATCACADEMYEntities())
                {
                    db.Database.Connection.Open();
                    insertCheck = db.PROC_InsertQuestionGroup(questionGroup);
                    db.Database.Connection.Close();
                }

                return Json(insertCheck, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(insertCheck, JsonRequestBehavior.AllowGet);
            }
        }

    }
}