using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediCure.Models
{
    public class ChangePasswordModel
    {
        public int LoginID { get; set; }
        [Required(ErrorMessage = "Please Enter New Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password")]
        [Compare("Password")]
        public string cnfPassword { get; set; }
    }
}