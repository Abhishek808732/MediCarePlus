using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MediCure.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Please Enter Your Email ID")]
        [EmailAddress(ErrorMessage ="Please Enter a Valid Email Address")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        public string Password { get; set; }

        public int LoginID { get; set; }
        public string UserName { get; set; }
        public string Contact { get; set; }
        public string Gender { get; set; }
        public int RoleID { get; set; }
        public string Address { get; set; }
        public string RoleName { get; set; }
        public List<SelectListItem> lstrolenames { get; set; }

    }
}