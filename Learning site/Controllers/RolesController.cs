using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Learning_site.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Learning_site.Controllers
{
    public class RolesController : Controller
    {

        private ApplicationDbContext _context;

        public RolesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Roles
        public ActionResult Index()
        {
            return View(_context.Roles.ToList());
        }


        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create(IdentityRole identityRole)
        {
            
                if (ModelState.IsValid)
                {
                    _context.Roles.Add(identityRole);
                    _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(identityRole);
               
         
        }

    }
}
