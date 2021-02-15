using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Class1
    {
        [Required(ErrorMessage ="Please enter name")]
        public String name { get; set; }
        public String email { get; set; }

        public String phone { get; set; }

        public bool withdraw { get; set; }

    }
}