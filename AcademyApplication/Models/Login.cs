using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AcademyApplication.Entity;

namespace AcademyApplication.Models
{
    public class Login
    {
        public string StudentRegister(Students studentDetails)
        {
            string isRegisterSuccess = "false";
            try
            {
                using (var academyEntity = new ATCACADEMYEntities())
                {
                    bool isALreadyRegistered = (from stu in academyEntity.Students
                                               where stu.IsActive == true && stu.Email == studentDetails.UserName
                                               select stu.StudentID).Any();
                    if (isALreadyRegistered)
                    {
                        isRegisterSuccess = "false";
                    }
                    else
                    {
                        Student student = new Student();
                        student.Email = studentDetails.UserName;
                        student.Password = studentDetails.PassWord;
                        student.StudentName = studentDetails.StudentName;
                        student.CreatedDate = DateTime.Now;
                        student.IsActive = true;
                        academyEntity.Students.Add(student);
                        academyEntity.SaveChanges();
                        isRegisterSuccess = "true";
                    }
                }
            }
            catch(Exception ex)
            {
                isRegisterSuccess = "dberror";
            }
            return isRegisterSuccess;
        }
        public string StudenLogin(Students student)
        {
            string isLogginSuccess = "false";
            try
            {
                using (var academyEntity = new ATCACADEMYEntities())
                {


                    var studentDetails = (from userdetails in academyEntity.Students
                                          where userdetails.Email == student.UserName
                                          && userdetails.Password == student.PassWord
                                          select userdetails.StudentID).FirstOrDefault();
                    if (studentDetails != 0)
                    {
                        isLogginSuccess = "true";
                        var userId = (from user in academyEntity.Students
                                      where user.IsActive == true && user.Email == student.UserName
                                      select user.StudentID).FirstOrDefault();
                        HttpCookie userInfo = new HttpCookie("UserCookie");
                        userInfo["UserId"] = userId.ToString();
                        userInfo.Expires.Add(new TimeSpan(24, 0, 0));
                        System.Web.HttpContext.Current.Response.Cookies.Add(userInfo);
                        var isAdmin = (from user in academyEntity.Students
                                      where user.IsActive == true && user.Email == student.UserName
                                      select user.isAdmin).FirstOrDefault();
                        HttpCookie userInfoone = new HttpCookie("AdminCookie");
                        userInfoone["IsAdmin"] = isAdmin.ToString();
                        userInfoone.Expires.Add(new TimeSpan(24, 0, 0));
                        System.Web.HttpContext.Current.Response.Cookies.Add(userInfoone);
                    }
                    else
                    {
                        isLogginSuccess = "false";
                    }
                }
            }
            catch(Exception ex)
            {
                isLogginSuccess = "dberror";
            }
            return isLogginSuccess;
        }
    }

    public class Students
    {
        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string StudentName { get; set; }
    }
}