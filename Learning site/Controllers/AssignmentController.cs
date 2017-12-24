using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Learning_site.Models;
using System.IO;

namespace Learning_site.Controllers
{
    public class AssignmentController : Controller
    {
        private ApplicationDbContext _context;

        public AssignmentController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Assignment



        //  [Route("Assignment/{Id}")]
        //[Authorize(Roles = "student")]
        public ActionResult Index(int Id)
        {


            Session["CourseID"] = Id;
            string currentUserId = User.Identity.GetUserId();


            var EnrolledCourses = _context.EnrolledCourses.Where(c => c.StudentId == currentUserId).SingleOrDefault(c => c.CourseId == Id);

            if (EnrolledCourses == null)
            {
                return HttpNotFound();
            }



            var Assignments = _context.Assignments.Where(c => c.studentId == currentUserId).Where(c=>c.courseId== Id).ToList();

  

            
            return View(Assignments);


        }




        // GET: Assignment/Create
        [Authorize(Roles = "student")]
        public ActionResult Create()
        {


            return View();
        }
        // POST: Assignment/Create
        [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult Create(Assignment assignment,HttpPostedFileBase uploadAssign)
        {
           
                string path = Path.Combine(Server.MapPath("~/upload/Assignments"), uploadAssign.FileName);
                uploadAssign.SaveAs(path);
                string currentUserId = User.Identity.GetUserId();
                assignment.File = uploadAssign.FileName;
                assignment.courseId = (int)Session["CourseID"];

                assignment.studentId = currentUserId;
                _context.Assignments.Add(assignment);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = assignment.courseId });
    


        }

        // GET: Assignment/Edit/5
        [Authorize(Roles = "student")]
        public ActionResult Edit(int id)
        {



            string currentStdentId = User.Identity.GetUserId();

            var Assignment = _context.Assignments.Where(a=>a.studentId== currentStdentId).SingleOrDefault(a => a.Id == id);
            if (Assignment == null)
                return HttpNotFound();
            

            return View(Assignment);
        }

        // POST: Assignment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Assignment assignment, HttpPostedFileBase uploadAssign)
        {
            try
            {
                // TODO: Add update logic here
                var assignInDb = _context.Assignments.SingleOrDefault(a => a.Id == assignment.Id);

                if (assignInDb == null)
                    return HttpNotFound();

                if (uploadAssign != null)
                {
                    string path = Path.Combine(Server.MapPath("~/upload/Assignments"), uploadAssign.FileName);
                    uploadAssign.SaveAs(path);
                    assignInDb.File = uploadAssign.FileName;
                }

                assignInDb.subject = assignment.subject;
                _context.SaveChanges();

                return RedirectToAction("Index", new { id = assignInDb.courseId });
            }
            catch
            {
                return View();
            }
        }

        // GET: Assignment/Delete/5
        public ActionResult Delete(int id)
        {

            string currentStdentId = User.Identity.GetUserId();

            var Assignment = _context.Assignments.Where(a => a.studentId == currentStdentId).SingleOrDefault(a => a.Id == id);
            if (Assignment == null)
                return HttpNotFound();


            _context.Assignments.Remove(Assignment);
            _context.SaveChanges();

            return RedirectToAction("Index",new { id= Assignment.courseId});

        }

       
    }
}
