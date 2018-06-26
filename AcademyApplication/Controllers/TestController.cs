using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademyApplication.Models;

namespace AcademyApplication.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            
            test testGroup = new test();
            testGroup.LogWrite("test page sarted");
            List<QuestionGroupList> quesgroup = new List<QuestionGroupList>();
            quesgroup = testGroup.GetQuestionGroupId();
            testGroup.LogWrite("data fetched");
            ViewBag.QuestionGroupList = quesgroup;
            return View();
        }

        public ActionResult OnlineTest(int groupId)
        {
            test abc = new test();
            List<QuestionList> questionList = new List<QuestionList>();
            questionList = abc.GetQuestionList(groupId);
            ViewBag.Questions = questionList;
            ViewBag.GroupId = groupId;
            return View();
        }

        public JsonResult TestQuestions(SubmittedAnswers answersSheet)
        {
            test abc = new test();
            int? result = abc.CheckQuestionAnswers(answersSheet);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TestDescription(int groupId)
        {
            test testconduct = new test();
            bool isTestDone = testconduct.IsStudentAlreadyTestDone();
            ViewBag.GroupId = groupId;
            ViewBag.IsAlreadyTestDone = isTestDone;
            return View();
        }

        public JsonResult StudentResultsPdf(int groupId)
        {
            string result = string.Empty;
            test TestPdf = new test();
            result = TestPdf.GeneratePDF(groupId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TestResult(int groupId)
        {
            test marks = new test();
            int? result = marks.GetTestResult(groupId);
            ViewBag.Result = result;
            ViewBag.Group = groupId;
            return View();
        }
    }
}