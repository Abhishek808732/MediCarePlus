//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MediCure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        public int AppointmentID { get; set; }
        public int LoginID { get; set; }
        public string UserName { get; set; }
        public System.DateTime AppointmentDate { get; set; }
        public string PreferredSlot { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string DoctorName { get; set; }
    }
}
