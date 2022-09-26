using MVCWithEFDBF3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWithEFDBF3.Controllers
{
    public class StudentController : Controller
    {
      MVCDBEntities dc = new MVCDBEntities();
        public ViewResult DisplayStudents()
        {
            return View(dc.Student_Select(null, true));
        }
        public ViewResult DisplayStudent(int Sid)
        {
            var Student = dc.Student_Select(Sid,true).Single();
            return View(Student);   
        }
        [HttpGet]
        public ViewResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult AddStudent(Student_Select_Result Student, HttpPostedFileBase selectedFile)
        {
            if (selectedFile != null)
            {
                string PhysicalPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(PhysicalPath))
                    Directory.CreateDirectory(PhysicalPath);
                selectedFile.SaveAs(PhysicalPath + selectedFile.FileName);
                Student.Photo = selectedFile.FileName;
            }
            dc.Student_Insert(Student.Sid,Student.Name,Student.Class,Student.Fees,Student.Photo);
            return RedirectToAction("DisplayStudents");
        }
        public ViewResult EditStudent(int Sid)
        {
            var Student = dc.Student_Select(Sid, true).Single();
            TempData["Photo"] = Student.Photo;
            return View(Student);
        }
        public RedirectToRouteResult UploadStudent(Student_Select_Result Student,HttpPostedFileBase selectedFile)
        {
            if (selectedFile != null)
            {
                string PhysicalPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(PhysicalPath))
                    Directory.CreateDirectory(PhysicalPath);
                selectedFile.SaveAs(PhysicalPath + selectedFile.FileName);
                Student.Photo = selectedFile.FileName;
            }
            else if (TempData["Photo"] != null)
            {
                Student.Photo = TempData["Photo"].ToString();
            }
            dc.Student_Update(Student.Sid, Student.Name, Student.Class, Student.Fees, Student.Photo);
            return RedirectToAction("DisplayStudents");
        }
        public ViewResult DeleteStudent(int sid)
        {
            return View(dc.Student_Select(sid, true).Single());
        }
        [HttpPost]
        public RedirectToRouteResult DeleteStudent(Student_Select_Result student)
        {
            dc.Student_Delete(student.Sid);
            return RedirectToAction("DisplayStudents");
        }
    }
}