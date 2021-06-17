using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MediCure.Models
{
    [Table("Drug_Table")]
    public class Spmodel1
    {
        public int DrugID { get; set; }
        public string DrugName { get; set; }
        public string DrugIDDrugDescription { get; set; }
        public int DrugQuantity { get; set; }
        public string Manufacturer { get; set; }
        public string UsedFor { get; set; }
        public int Price { get; set; }


    }
}