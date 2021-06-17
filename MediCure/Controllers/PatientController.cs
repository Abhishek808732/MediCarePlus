using MediCure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MediCure.Controllers
{
    [Authorize]
    [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
    public class PatientController : Controller
    {

        // GET: Patient
        public ActionResult Index()
        {
            if (Request.IsAuthenticated == false)
                return RedirectToAction("UserLogin", "Account");

            var username = System.Web.HttpContext.Current.User.Identity.Name;
            using (MediCureEntities db = new MediCureEntities())
            {
                ViewBag.LoggedUserName = db.UserLogins.FirstOrDefault(a => a.EmailID == username).UserName;
            }
                return View();
        }
        public ActionResult UserLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("UserLogin", "Account");
        }

        [HttpGet]
        public ActionResult ChangeUserPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeUserPassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (MediCureEntities db = new MediCureEntities())
            {
                var username = System.Web.HttpContext.Current.User.Identity.Name;
                var getdata = db.UserLogins.FirstOrDefault(a => a.EmailID == username);
                getdata.Password = model.Password;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult MessageBox()
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            using (MediCureEntities db = new MediCureEntities())
            {
                ViewBag.LoggedUserName = db.UserLogins.FirstOrDefault(a => a.EmailID == username).UserName;
            }
            return View();
        }

        public ActionResult History()
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            using (MediCureEntities db = new MediCureEntities())
            {
                var getId = db.UserLogins.FirstOrDefault(a => a.EmailID == username).LoginID;
                ViewBag.LoggedUserName = db.UserLogins.FirstOrDefault(a => a.EmailID == username).UserName;
                var getdata = db.Records.Where(a => a.LoginID == getId).ToList();
                List<RecordModel> lst = new List<RecordModel>();
                foreach (var item in getdata)
                {
                    lst.Add(new RecordModel
                    {
                        LoginID = item.LoginID,
                        UserName = item.UserName,
                        Date = item.Date,
                        Symptoms = item.Symptoms
                    });
                }
                return View(lst);
            }
        }

        /*public ActionResult Appointment()
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            using (MediCureEntities db = new MediCureEntities())
            {
                ViewBag.LoggedUserName = db.UserLogins.FirstOrDefault(a => a.EmailID == username).UserName;
            }
            return View();
        }*/

        [HttpGet]
        public ActionResult Appointment()
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            using (MediCureEntities db = new MediCureEntities())
            {
                var getdata = db.UserLogins.FirstOrDefault(a => a.EmailID == username);
                ViewBag.LoggedUserName = getdata.UserName;
                AppointmentModel model = new AppointmentModel();
                model.LoginID = getdata.LoginID;
                model.lstDoc = CommonMethod.GetDocData();
                model.UserName = getdata.UserName;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult InsertAppointment(AppointmentModel model)
        {
            using (MediCureEntities db = new MediCureEntities())
            {
                Appointment app = new Appointment();
                app.LoginID = model.LoginID;
                app.UserName = model.UserName;
                app.AppointmentDate = model.date;
                app.PreferredSlot = model.PreferredSlot;
                app.DoctorName = model.DoctorName;
                app.Phone = model.Phone;
                app.Message = model.Message;
                app.Status = "Slot Booked";
                db.Appointments.Add(app);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult ViewProfile()
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            using (MediCureEntities db = new MediCureEntities())
            {
                ViewBag.LoggedUserName = db.UserLogins.FirstOrDefault(a => a.EmailID == username).UserName;
                var getdata = db.UserLogins.FirstOrDefault(a => a.EmailID == username);
                LoginModel model = new LoginModel();
                model.LoginID = getdata.LoginID;
                model.UserName = getdata.UserName;
                model.Password = getdata.Password;
                model.Gender = getdata.Gender;
                model.EmailID = getdata.EmailID;
                model.Contact = getdata.Contact;
                model.Address = getdata.Address;
                return View(model);
            }
        }


    }
}