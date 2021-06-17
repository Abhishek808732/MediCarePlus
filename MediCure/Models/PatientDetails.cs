using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediCure.Models
{
    public class PatientDetails
    {
        public int LoginID { get; set; }
        public string UserName { get; set; }

        public string Contact { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
    }
}