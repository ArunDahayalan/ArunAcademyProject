using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AcademyApplication.Entity;
using System.IO;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AcademyApplication.Models
{
    public class test : PdfPTable
    {
        public List<QuestionList> allQuestions { get; set;}

        public string GeneratePDF(int groupId)
        {
            string isPdfWrited = "false";
            try
            {
                int studentId = int.Parse(System.Web.HttpContext.Current.Request.Cookies["UserCookie"]["UserId"]);
                using (var academyEntity = new ATCACADEMYEntities())
                {
                    var pdfDocumentDetails = (from res in academyEntity.Results
                                              from stu in academyEntity.Students
                                              from questionGroup in academyEntity.QuestionGroups
                                              where res.StudentID == studentId && stu.StudentID == studentId
                                              && questionGroup.QuestionGroupID == groupId
                                              select new PdfWritterDetails
                                              {
                                                  testGroupname = questionGroup.QuestionGroupName,
                                                  studentName = stu.StudentName,
                                                  studentId = studentId,
                                                  WrongAnswers = res.wronganswers,
                                                  resultMark = res.Marks
                                              }).ToList();


                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        Document document = new Document(PageSize.A4, 10, 10, 10, 10);

                        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                        document.Open();

                        string imageURL = System.Configuration.ConfigurationManager.AppSettings["imglocation"] + "\\atclogo.jpg";
                        LogWrite(imageURL);
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);

                        //Resize image depend upon your need

                        jpg.ScaleToFit(140f, 120f);

                        //Give space before image

                        jpg.SpacingBefore = 10f;

                        //Give some space after the image

                        jpg.SpacingAfter = 1f;

                        jpg.Alignment = Element.ALIGN_CENTER;

                        document.Add(jpg);

                        string text = @"ATC Educational Institution";
                        Paragraph paragraph = new Paragraph();
                        paragraph.SpacingBefore = 30;
                        paragraph.SpacingAfter = 10;
                        paragraph.Alignment = Element.ALIGN_CENTER;
                        paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 20f, BaseColor.BLACK);
                        paragraph.Add(text);
                        document.Add(paragraph);

                        string text1 = pdfDocumentDetails[0].testGroupname;
                        Paragraph paragraph1 = new Paragraph();
                        paragraph1.SpacingBefore = 10;
                        paragraph1.SpacingAfter = 50;
                        paragraph1.Alignment = Element.ALIGN_CENTER;
                        paragraph1.Font = FontFactory.GetFont(FontFactory.HELVETICA, 20f, BaseColor.BLACK);
                        paragraph1.Add(text1);
                        document.Add(paragraph1);

                        string text2 = "Student ID : " + pdfDocumentDetails[0].studentId.ToString();
                        Paragraph paragraph2 = new Paragraph();
                        paragraph2.SpacingBefore = 10;
                        paragraph2.SpacingAfter = 20;
                        paragraph2.Alignment = Element.ALIGN_CENTER;
                        paragraph2.Font = FontFactory.GetFont(FontFactory.HELVETICA, 20f, BaseColor.BLACK);
                        paragraph2.Add(text2);
                        document.Add(paragraph2);

                        string text3 = "Student Name : " + pdfDocumentDetails[0].studentName;
                        Paragraph paragraph3 = new Paragraph();
                        paragraph3.SpacingBefore = 10;
                        paragraph3.SpacingAfter = 20;
                        paragraph3.Alignment = Element.ALIGN_CENTER;
                        paragraph3.Font = FontFactory.GetFont(FontFactory.HELVETICA, 20f, BaseColor.BLACK);
                        paragraph3.Add(text3);
                        document.Add(paragraph3);

                        string text4 = "Wrong Answers : " + pdfDocumentDetails[0].WrongAnswers.ToString();
                        Paragraph paragraph4 = new Paragraph();
                        paragraph4.SpacingBefore = 10;
                        paragraph4.SpacingAfter = 20;
                        paragraph4.Alignment = Element.ALIGN_CENTER;
                        paragraph4.Font = FontFactory.GetFont(FontFactory.HELVETICA, 20f, BaseColor.BLACK);
                        paragraph4.Add(text4);
                        document.Add(paragraph4);

                        string text5 = "Result : " + pdfDocumentDetails[0].resultMark.ToString();
                        Paragraph paragraph5 = new Paragraph();
                        paragraph5.SpacingBefore = 10;
                        paragraph5.SpacingAfter = 20;
                        paragraph5.Alignment = Element.ALIGN_CENTER;
                        paragraph5.Font = FontFactory.GetFont(FontFactory.HELVETICA, 20f, BaseColor.BLACK);
                        paragraph5.Add(text5);
                        document.Add(paragraph5);

                        
                        document.Close();
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ContentType = "application/pdf";

                        string pdfName = pdfDocumentDetails[0].studentName +  "_score";
                        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + pdfName + ".pdf");
                        HttpContext.Current.Response.ContentType = "application/pdf";
                        HttpContext.Current.Response.Buffer = true;
                        HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                        HttpContext.Current.Response.BinaryWrite(bytes);
                        HttpContext.Current.Response.End();
                        HttpContext.Current.Response.Close();
                        isPdfWrited = "true";
                    }
                }
            }
            catch (Exception ex)
            {
                LogWrite(ex.Message);
                LogWrite(ex.StackTrace.ToString());
                isPdfWrited = "error";
            }
            return isPdfWrited;
        }

        public List<QuestionList> GetQuestionList(int groupId)
        {
            //List<QuestionList> questionList = new List<QuestionList>();
            try
            {
                using (var academyEntity = new ATCACADEMYEntities())
                {
                    allQuestions = (from questions in academyEntity.Questions
                                    from questiongroup in academyEntity.QuestionGroups
                                    where questions.IsValid == true && questions.QuestionGroupID == groupId
                                    select new QuestionList
                                    {
                                        questionId = questions.QuestionID,
                                        question = questions.Question1,
                                        questionType = questions.QuestionTypeID,
                                        testGroupName = questiongroup.QuestionGroupName,
                                        isHaveImage = questions.IsHaveImage
                                        // OptionAnswers = suggested.OptionAnswer.Where(suggested.QuestionID => suggested.QuestionID  == questions.QuestionID)
                                    }).ToList();
                    foreach (var x in allQuestions)
                    {
                        x.OptionAnswers = (from suggested in academyEntity.Options
                                           where suggested.QuestionID == x.questionId
                                           select new OptionalAnswers
                                           {
                                               sequence = suggested.Sequence,
                                               optionList = suggested.OptionAnswer,
                                               isHaveImage = suggested.IsHaveImage
                                           }).ToList();
                    }
                }
            }
            catch(Exception ex)
            {
                allQuestions = null;
            }
            return allQuestions;
        }

        public bool IsStudentAlreadyTestDone()
        {
            bool isTestDone = false;
            int userId = int.Parse(System.Web.HttpContext.Current.Request.Cookies["UserCookie"]["UserId"]);
            try
            {
                using (var academyEntity = new ATCACADEMYEntities())
                {
                    isTestDone = (from result in academyEntity.Results
                                  where userId == result.StudentID
                                  select result.ResultID).Any();
                }
            }
            catch(Exception ex)
            {
                isTestDone = false;
            }
            return isTestDone;
        }

        public int? CheckQuestionAnswers(SubmittedAnswers answerSheet)
        {
            int? result = 0;
            int resultVlue = 0;
            List<GetQuestionAnswers> questionandanswers = new List<GetQuestionAnswers>();
            try
            {
                using (var academyEntity = new ATCACADEMYEntities())
                {
                    questionandanswers = (from ques in academyEntity.Questions
                                          where ques.IsValid == true
                                          select new GetQuestionAnswers
                                          {
                                              QuestionsList = ques.QuestionID,
                                              AnswersList = ques.Answer
                                          }).ToList();

                    for (int i = 0; i < questionandanswers.Count; i++)
                    {
                        if (questionandanswers[i].AnswersList == answerSheet.AnswersList[i])
                        {
                            result += 1;
                            resultVlue += 1;
                        }
                    }
                    int? minusValue = questionandanswers.Count - result;
                    double? modulateValue = minusValue * 0.33;
                    result = result - ((int)modulateValue);
                    int minusValueOne = questionandanswers.Count - resultVlue;
                    double modulateValueOne = minusValueOne * 0.33;
                    resultVlue = resultVlue- ((int)modulateValueOne);

                    Result res = new Result();
                    res.StudentID = int.Parse(System.Web.HttpContext.Current.Request.Cookies["UserCookie"]["UserId"]);
                    res.QuestionGroupID = answerSheet.QuestionGroupId;
                    res.Marks = resultVlue;
                    res.wronganswers = minusValue;
                    academyEntity.Results.Add(res);
                    academyEntity.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public int? GetTestResult(int groupId)
        {
            int? result = 0;
            try
            {
                using (var academyEntity = new ATCACADEMYEntities())
                {
                    int userId = int.Parse(System.Web.HttpContext.Current.Request.Cookies["UserCookie"]["UserId"]);
                    result = (from res in academyEntity.Results
                              where res.StudentID == userId && res.QuestionGroupID == groupId
                              select res.Marks).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                result = null;
            }
            return result;
        }

        public List<QuestionGroupList> GetQuestionGroupId()
        {
            List<QuestionGroupList> questionGroup = new List<QuestionGroupList>();
            try
            {
                using (var academyEntity = new ATCACADEMYEntities())
                {
                    // academyEntity
                    LogWrite("entity open");
                    questionGroup = (from quesgroup in academyEntity.QuestionGroups
                                     where quesgroup.IsValid == true
                                     select new QuestionGroupList
                                     {
                                         QuestionGroupId = quesgroup.QuestionGroupID,
                                         QuestionGroupname = quesgroup.QuestionGroupName
                                     }).ToList();
                    LogWrite("fetched data");
                   
                }
            }
            catch(Exception ex)
            {
                questionGroup = null;
                LogWrite(ex.Message);
                    LogWrite(ex.InnerException.ToString());
                LogWrite(ex.StackTrace.ToString());
            }
                return questionGroup;
        }
        private string m_exePath = string.Empty;
        public void LogWrite(string logMessage)
        {
            m_exePath = Path.GetDirectoryName(System.Configuration.ConfigurationManager.AppSettings["loglocation"]);
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }

    public class QuestionList
    {
        public int questionId { get; set; }

        public string question { get; set; }

        public List<OptionalAnswers> OptionAnswers { get; set; }

        public int questionType { get; set; }

        public string testGroupName { get; set; }

        public bool isHaveImage { get; set; }
    }

    public class OptionalAnswers
    {
        public int sequence { get; set; }

        public string optionList { get; set; }

        public bool isHaveImage { get; set; }
    }

    public class SubmittedAnswers
    {
        public List<int> QuestionsList { get; set; }

        public List<int> AnswersList { get; set; }

        public int QuestionGroupId { get; set; }
    }

    public class GetQuestionAnswers
    {
        public int QuestionsList { get; set; }

        public int AnswersList { get; set; }
    }

    public class QuestionGroupList
    {
        public int QuestionGroupId { get; set; }

        public string QuestionGroupname { get; set; }
    }

    public class PdfWritterDetails
    {
        public string testGroupname { get; set; }

        public string studentName { get; set; }

        public int studentId { get; set; }

        public int resultMark { get; set; }

        public int? WrongAnswers { get; set; }
    }
}