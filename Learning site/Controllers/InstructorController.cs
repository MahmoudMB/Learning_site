using Learning_site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using System.Data.Entity;
using System.IO;

namespace Learning_site.Content
{

    [Authorize(Roles ="instructor")]
    public class InstructorController : Controller
    {
        private ApplicationDbContext _context;

        public InstructorController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Instructor
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            var Courses = _context.Courses.Where(c => c.InstructorId == currentUserId).ToList();
            return View(Courses);


        }







        // GET: Instructor/Create
        public ActionResult CreateCourse()
        {
            return View();
        }

        // POST: Instructor/Create
        [HttpPost]
        public ActionResult CreateCourse(Course c)
        {
            try
            {
                Course course = new Course();

                var userId = User.Identity.GetUserId();

                course.InstructorId = userId;
                course.code = c.code;
                course.name = c.name;

                _context.Courses.Add(course);
                _context.SaveChanges();


                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        public ActionResult ShowEnrolledStudent(int Id)
        {
            Session["AssignmentCourseId"] = Id;
            //  Session["StudentId"] = User.Identity.GetUserId();


            var instructorID = User.Identity.GetUserId();

            var course = _context.Courses.Where(a => a.InstructorId == instructorID).SingleOrDefault(c => c.Id == Id);
            if (course == null)
            {
                return HttpNotFound();
            }

            var students = _context.EnrolledCourses.Include(a => a.Student).Where(a => a.CourseId == Id).ToList();


            return View(students);
        }



        public ActionResult Grade(int id)
        {




            var assign = _context.Assignments.SingleOrDefault(a => a.Id == id);
            return View(assign);
        }
        [HttpPost]
        public ActionResult Grade(Assignment assignment)
        {

            //    int assignId = (int) Session["assignId"];

            var assignInDb = _context.Assignments.SingleOrDefault(a => a.Id == assignment.Id);


            assignInDb.Grade = assignment.Grade;

            _context.SaveChanges();



            return RedirectToAction("Assignments");
        }




        public FileResult Download(int id) {


            var assign = _context.Assignments.SingleOrDefault(a => a.Id == id);
            string path = Path.Combine(Server.MapPath("~/upload/Assignments"), assign.File);


            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(assign.File));


        }

        [Route("Instructor/GetAssign/{Id}")]
        public ActionResult GetAssign(string Id)
        {
            Session["StudentId"] = Id;
            return RedirectToAction("Assignments");
        }

        public ActionResult Assignments()
        {

         
            int CourseId= (int)Session["AssignmentCourseId"];
            string stdid = Session["StudentId"].ToString();
            var Assignments = _context.Assignments.Where(c => c.studentId == stdid).Where(c => c.courseId == CourseId).ToList();

            return View(Assignments);



        }



        public ActionResult DeleteCourse(int id)
        {
            Course c = _context.Courses.SingleOrDefault(a => a.Id == id);
            _context.Courses.Remove(c);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

  
    }
}
