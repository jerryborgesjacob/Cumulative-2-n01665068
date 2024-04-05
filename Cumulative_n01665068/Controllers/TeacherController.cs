using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cumulative_n01665068.Models;


namespace Cumulative_n01665068.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This method is used to list the Names of all the Teacher in the List View
        /// </summary>
        /// <returns>A View that displays the Full Names of all Teachers</returns>
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            List <Teacher> Teachers = controller.ListTeachers();
            return View(Teachers);
        }


        /// <summary>
        /// This method is used to show the details of the selected teacher
        /// </summary>
        /// <returns>A View that displays all the Details of the teacher selected in the List View</returns>
        public ActionResult Show(int Id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(Id);

            return View(NewTeacher);
        }


        public ActionResult Delete (int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id) 
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher DelTeacher = Controller.FindTeacher(id);

            return View(DelTeacher);
        }

        public ActionResult New() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create (string TeacherFName, string TeacherLName, String EmpNumber, DateTime HireDate, Decimal Salary)
        {
            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFName = TeacherFName;
            NewTeacher.TeacherLName = TeacherLName;
            NewTeacher.EmpNumber = EmpNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
    }
}