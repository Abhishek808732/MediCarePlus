using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediCure.Models
{
    public class DepartmentModel
    {
        public int DepartmentID { get; set; }
        public List<SelectListItem> lstDept { get; set; }
    }
}