using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediCure.Models
{
    public class InputOrder
    {
        public int DrugID { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public List<SelectListItem> DrugNames { get; set; }
    }
}