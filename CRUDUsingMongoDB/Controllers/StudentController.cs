using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDUsingMongoDB.Helpers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace CRUDUsingMongoDB.Controllers
{
    public class StudentController : Controller
    {
        IMongoClient mongoClient;
        IMongoDatabase database;
        public StudentController()
        {

        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            var result = CRUDHelper.InsertRecord("College", "Students", new List<Student> {

                new Student
                {
                    Name = "Jithendra"
                },
                new Student
                {
                    Name = "Rajesh"
                },
                new Student
                {
                    Name = "MALtech"
                },new Student
                {
                    Name = "Jithendra"
                },
                new Student
                {
                    Name = "Rajesh"
                },
                new Student
                {
                    Name = "MALtech"
                }

            });

            if (result.Equals(true))
            {
                return View();
            }

            return View("Error");

        }

        public IActionResult FindStudent()
        {
            return View();
        }

        [Route("emplist")]
        public IActionResult ListStudents()
        {
            var result = CRUDHelper.ListRecordsAsync<Student>("College","Students");
            return View(result);

        }

        public IActionResult EditStudent(Guid guid)
        {
            var result = CRUDHelper.LoadRecordById<Student>("College", "Students", guid);
            return View(result);
        }

        public IActionResult DetailsStudent(Guid guid)
        {
            var result = CRUDHelper.LoadRecordById<Student>("College", "Students", guid);
            return View(result);
        }

        [HttpGet]
        public IActionResult DeleteStudent(Guid guid)
        {
            var result = CRUDHelper.LoadRecordById<Student>("College", "Students", guid);
            return View(result);
        }

        [HttpPost]
        public IActionResult DeleteStudent(Student student)
        {
            var result = CRUDHelper.DeleteRecordById<Student>("College", "Students", student.Id);
            return RedirectToAction("ListStudents");
        }

        [HttpPost]
        public IActionResult EditStudent(Student student)
        {
            var result = CRUDHelper.UpdateRecordById<Student>("College", "Students", student.Id, student);
            return RedirectToAction("ListStudents");
        }

    }

    public class Student
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
