﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Learning_site.Models
{
    public class Assignment
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Subject")]
        public string subject { get; set; }
        public string File { get; set; }
        public Double Grade { get; set; }
        public int courseId { get; set; }
        public string studentId { get; set; }
        public virtual ApplicationUser student { get; set; }

    }
}