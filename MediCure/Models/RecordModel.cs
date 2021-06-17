using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediCure.Models
{
    public class RecordModel
    {
        public int RecordID { get; set; }
        public int LoginID { get; set; }
        public string UserName { get; set; }
        public System.DateTime Date { get; set; }
        public string Symptoms { get; set; }
    }
}