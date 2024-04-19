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

        /// <summary>
        /// This method is used to delete a teacher from the list
        /// </summary>
        /// <param name="Id">TeacherID of the teacher who is to be deleted from the table. </param>
        /// <returns>Takes the user to the List with all teachers</returns>
        public ActionResult Delete (int Id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(Id);
            return RedirectToAction("List");
        }

        /// <summary>
        /// This method will ask the user for confirmation before deleting a record from the teacher table.
        /// </summary>
        /// <param name="Id">TeacherID of the teacher</param>
        /// <returns>Takes the user to the view with the List of all teachers</returns>
        public ActionResult DeleteConfirm(int Id) 
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher DelTeacher = controller.FindTeacher(Id);

            return View(DelTeacher);
        }

       
        public ActionResult New() 
        {
            return View();
        }

        /// <summary>
        /// This method adds a new teacher in the table
        /// </summary>
        /// <param name="TeacherFName">First Name of the teacher</param>
        /// <param name="TeacherLName">Last Name of the teacher</param>
        /// <param name="EmpNumber">EmployeeNumber of the teacher</param>
        /// <param name="HireDate">Hire Date of the teacher</param>
        /// <param name="Salary">Salary of the Teacher</param>
        /// <returns>Redirects the user to the List with all teachers</returns>
        [HttpPost]
        public ActionResult Create (string TeacherFName, string TeacherLName, string EmpNumber, DateTime HireDate, decimal Salary)
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
        
        /// <summary>
        /// This method updates the teacher's data in the table
        /// </summary>
        /// <param name="Id">TeacherID of the teacher to be updated</param>
        /// <returns>List view of the Teachers</returns>
        public ActionResult Update (int Id) 
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(Id);
            return View(SelectedTeacher);
        }

        /// <summary>
        /// POST method that send the data to the table
        /// </summary>
        /// <param name="Id">TeacherID of the teacher to be updated</param>
        /// <param name="TeacherFName">First Name of the teacher to be updated</param>
        /// <param name="TeacherLName">Last Name of the teacher to be updated</param>
        /// <param name="EmpNumber">Employee Number of the teacher to be updated</param>
        /// <param name="HireDate">Hire Date of the teacher to be updated</param>
        /// <param name="Salary">Salary of the teacher to be updated</param>
        /// <returns>The 'Show' View of the teacher</returns>
        [HttpPost]
        public ActionResult Update (int Id, string TeacherFName, string TeacherLName, string EmpNumber, DateTime HireDate, decimal Salary)
        {
            Teacher TeacherRecord = new Teacher();
            TeacherRecord.TeacherFName = TeacherFName;
            TeacherRecord.TeacherLName = TeacherLName;
            TeacherRecord.EmpNumber = EmpNumber;
            TeacherRecord.HireDate = HireDate;
            TeacherRecord.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(Id, TeacherRecord);
            return RedirectToAction("Show/"+Id);


        }
    }
}