using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AcademyApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
           "resultpdf",
           "studentresultpdf/{groupId}",
           new { Controller = "Test", action = "StudentResultsPdf" }
       );

            routes.MapRoute(
            "studentloginregistervalidate",
            "studentloginregister",
            new { Controller = "Login", action = "ResisterStudent" }
        );

            routes.MapRoute(
             "stdentregister",
             "account/register",
             new { Controller = "Login", action = "Register" }
         );

            routes.MapRoute(
             "TestPage",
             "testschedule",
             new { Controller = "Test", action = "Index" }
         );

            routes.MapRoute(
           "ContactForm",
           "contactformsubmission",
           new { Controller = "Academy", action = "ContactusFormValues" }
       );

            routes.MapRoute(
            "Testcomplete",
            "testresult/{groupid}",
            new { Controller = "Test", action = "TestResult" }
        );

            routes.MapRoute(
             "TestQuiz",
             "testquestions",
             new { Controller = "Test", action = "TestQuestions" }
         );

            routes.MapRoute(
              "OnlineTest",
              "writetest/{groupId}",
              new { Controller = "Test", action = "OnlineTest" }
          );

            routes.MapRoute(
              "OnlineTestDescription",
              "testdescription/{groupid}",
              new { Controller = "Test", action = "TestDescription" }
          );

            routes.MapRoute(
              "StudentLoginValidate",
              "studentloginsubmit",
              new { Controller = "Login", action = "Login" }
          );

            routes.MapRoute(
               "StudentLoginPage",
               "studentlogin",
               new { Controller = "Login", action = "Index" }
           );

            routes.MapRoute(
               "FacultyLoginPage",
               "facultylogin",
               new { Controller = "Login", action = "FacultyLogin" }
           );

            routes.MapRoute(
                "AboutPage",
                "aboutus",
                new { Controller = "Academy", action = "Index" }
            );

            routes.MapRoute(
                "CoursePage",
                "courses",
                new { Controller = "Course", action = "Index" }
            );

            routes.MapRoute(
               "GaleryPage",
               "galery",
               new { Controller = "Media", action = "Index" }
           );

            routes.MapRoute(
               "AdmissionPage",
               "admission",
               new { Controller = "Academy", action = "Admission" }
           );

            routes.MapRoute(
              "AchievementsPage",
              "achievements",
              new { Controller = "Academy", action = "Achivements" }
          );

            routes.MapRoute(
             "MaterialsPage",
             "materials",
             new { Controller = "Course", action = "Materials" }
         );

            

            routes.MapRoute(
            "ContactPage",
            "contact",
            new { Controller = "Academy", action = "Contactus" }
        );

            routes.MapRoute(
            "SuccessStoryPage",
            "success-story",
            new { Controller = "Academy", action = "SuccessStory" }
        );


            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
