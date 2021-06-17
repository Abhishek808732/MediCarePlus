using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediCure.Models
{
    public class CommonMethod
    {
        public static List<SelectListItem> RoleData()
        {
            using (MediCureEntities db = new MediCureEntities())
            {
                var getdata = db.RoleDetails.ToList();

                List<SelectListItem> lstrolenames = new List<SelectListItem>();
                foreach (var item in getdata)
                {
                    lstrolenames.Add(new SelectListItem { Text = item.RoleName, Value = item.RoleID.ToString() });
                }

                return (lstrolenames);
            }
        }

       /* public static List<SelectListItem> GetDeptDetails()
        {
            using (MediCureEntities db = new MediCureEntities())
            {
                var getdata = db.Departments.ToList();
                List<SelectListItem> lstDept = new List<SelectListItem>();
                foreach (var item in getdata)
                {
                    lstDept.Add(new SelectListItem { Text = item.DepartmentName, Value = item.DepartmentID.ToString() });
                }
                return lstDept;
            }
        }*/

        public static List<SelectListItem> GetDrugData()
        {
            using (MediCureEntities db = new MediCureEntities())
            {
                var getdata = db.Drug_Table.ToList();
                List<SelectListItem> lstdrug = new List<SelectListItem>();
                foreach (var item in getdata)
                {
                    lstdrug.Add(new SelectListItem { Text = item.DrugName, Value = item.DrugID.ToString() });
                }
                return lstdrug;
            }
        }
        public static List<SelectListItem> GetDocData()
        {
            using (MediCureEntities db = new MediCureEntities())
            {
                var getdata = db.UserLogins.Where(a=>a.RoleID==2).ToList();
                List<SelectListItem> lstDoc = new List<SelectListItem>();
                foreach (var item in getdata)
                {
                    lstDoc.Add(new SelectListItem { Text = item.UserName, Value = item.UserName });
                }
                return lstDoc;
            }
        }
    }
}