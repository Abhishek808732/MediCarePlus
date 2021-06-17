using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediCure.Models;

namespace MediCure.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DoctorsList()
        {
            return View();
        }

        public ActionResult MedicineDirectory()
        {
            using (MediCureEntities db = new MediCureEntities())
            {
                var getdata = (from c in db.MedicineDirectories
                               select new
                               {
                                   c.DrugName,
                                   c.Manufacturer,
                                   c.UsedFor,
                                   c.Description
                               }).ToList();

                List<medmodel> meddata = new List<medmodel>();
                foreach (var item in getdata)
                {
                    meddata.Add(new medmodel
                    {
                        DrugName = item.DrugName,
                        Manufacturer = item.Manufacturer,
                        UsedFor = item.UsedFor,
                        Description = item.Description
                    });
                }

                return View(meddata);
            }
        }

        public ActionResult UserLogin()
        {
            return View();
        }
    }
}