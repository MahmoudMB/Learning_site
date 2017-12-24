using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Learning_site.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string code { get; set; }
        public string InstructorId { get; set; }
        public  virtual ApplicationUser instructor { get; set; }



    }
}