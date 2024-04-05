using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cumulative_n01665068.Models;
using MySql.Data.MySqlClient;
namespace Cumulative_n01665068.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolContext School = new SchoolContext();

        /// <summary>
        /// Connects to the Database and provides the details of Teachers in the Database 
        /// </summary>
        /// <returns>The Details of the Teachers</returns>
        /// <example>
        /// localhost.xx/api/TeacherData -> The List of Teachers in an XML File (The data is too big to display a sample output)
        /// </example>
        
        [HttpGet]
        [Route("api/TeacherData")] //This will display the Teachers' Data in an XML File
        public List<Teacher> ListTeachers()
        {
            //Create an instance of the connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and the database
            Conn.Open();

            //Establish a new command 
            MySqlCommand cmd = Conn.CreateCommand();

            //Query to Execute
            cmd.CommandText = "SELECT * FROM teachers";

            //Gather the result set into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create a list of Teachers' Details that will be stored later
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through the Result Set
            while (ResultSet.Read())
            {
                //Access The Table Column by Column Names
                string TeacherName = ResultSet["teacherfname"].ToString() + " " + ResultSet["teacherlname"].ToString();
                int TeacherID = Convert.ToInt32(ResultSet["teacherid"]);
                string EmpNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                //Creating a New Object to store the data
                Teacher NewTeacher = new Teacher();

                //Adding the Teachers' Details to the List created above
                NewTeacher.TeacherID = TeacherID;
                NewTeacher.TeacherName = TeacherName;
                NewTeacher.EmpNumber = EmpNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                Teachers.Add(NewTeacher);

            }

            //Close the connection between the Database and the Web Server
            Conn.Close();

            //Return the list of Teachers' Names
            return Teachers;


        }


        /// <summary>
        /// Connects to the Database and provides all the Details about a Teacher that is requested (using their TeacherID) 
        /// </summary>
        /// <returns>All Details of the specific Teacher in the database</returns>
        /// <example>
        /// localhost.xx/api/TeachersData/FindTeacher/4 -> Displays EmpNumber, HireDate, Salary, TeacherID and Full Name of Jessica Morris. 
        /// (I tried to copy and paste the XML output, but the tags in XML started throwing errors in the code)
        /// </example>

        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{Id}")]

        public Teacher FindTeacher(int Id)
        {

            //Create an instance of the connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and the database
            Conn.Open();

            //Establish a new command 
            MySqlCommand cmd = Conn.CreateCommand();

            //Query to Execute
            cmd.CommandText = "SELECT * FROM teachers where TeacherID = " + Id;

            //Gather the result set into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();


            //Creating a New Object to store the data
            Teacher NewTeacher = new Teacher();

            //Loop Through the Result Set
            while (ResultSet.Read()) 
            {
                //Access The Table Column by Column Names
                string TeacherName = ResultSet["teacherfname"].ToString() + " " + ResultSet["teacherlname"].ToString();
                int TeacherID = Convert.ToInt32(ResultSet["teacherid"]);
                string EmpNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);


                //Adding the Teachers' Details to the List created above
                NewTeacher.TeacherID = TeacherID;
                NewTeacher.TeacherName = TeacherName;
                NewTeacher.EmpNumber = EmpNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }

            return NewTeacher;
        }

        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of the connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and the database
            Conn.Open();

            //Establish a new command 
            MySqlCommand cmd = Conn.CreateCommand();

            //Query to Execute
            cmd.CommandText = " DELETE FROM teachers WHERE teacherid = @id ";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();


            cmd.ExecuteNonQuery();

            Conn.Close();


        }

        [HttpPost]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of the connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and the database
            Conn.Open();

            //Establish a new command 
            MySqlCommand cmd = Conn.CreateCommand();

            //Query to Execute
            cmd.CommandText = " INSERT INTO teachers values (@TeacherFName, @TeacherLName, @EmpNumber, @HireDate, @Salary)";
            cmd.Parameters.AddWithValue("@TeacherFName", NewTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@TeacherLName", NewTeacher.TeacherLName);
            cmd.Parameters.AddWithValue("@EmpNumber", NewTeacher.EmpNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }
    }
}
