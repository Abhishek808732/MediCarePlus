using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediCure.Models
{
    public class Salespersonmodel
    {
        public int OrderID { get; set; }
        public int DrugID { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }

        // public int DrugID { get; set; }
        public string DrugName { get; set; }
        public string DrugDescription { get; set; }
        public Nullable<int> DrugQuantity { get; set; }
        public string Manufacturer { get; set; }
        public string UsedFor { get; set; }
    }
}