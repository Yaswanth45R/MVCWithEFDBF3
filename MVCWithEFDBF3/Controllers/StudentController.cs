using MVCWithEFDBF3.Models;
using System;
using System.Collections.Generic;
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
    }
}