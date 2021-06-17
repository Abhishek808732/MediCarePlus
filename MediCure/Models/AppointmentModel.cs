using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediCure.Models
{
    public class AppointmentModel
    {
        public int AppointmentID { get; set; }
        public int LoginID { get; set; }
        public string UserName { get; set; }
        public DateTime date { get; set; }
       // public int DepartmentID { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public string PreferredSlot { get; set; }

        public List<SelectListItem> lstDept { get; set; }
        public List<SelectListItem> lstDoc { get; set; }
        public string DoctorName { get; set; }
    }
}