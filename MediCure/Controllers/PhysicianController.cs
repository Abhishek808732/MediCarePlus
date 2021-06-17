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
    public class PhysicianController : Controller
    {
        // GET: Physician
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


        public ActionResult ViewPatient()
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            using (MediCureEntities db = new MediCureEntities())
            {
                ViewBag.LoggedUserName = db.UserLogins.FirstOrDefault(a => a.EmailID == username).UserName;
                List<PatientDetails> patientDetails = new List<PatientDetails>();
                var getdata = from user in db.UserLogins
                               where user.RoleID == 1
                               select new { 
                                   user.LoginID,
                                   user.UserName,
                                   user.Contact,
                                   user.Address,
                                   user.Gender };

                foreach (var item in getdata)
                {
                    patientDetails.Add
                        (
                        new PatientDetails
                        {
                            LoginID = item.LoginID,
                            UserName = item.UserName,
                            Contact = item.Contact,
                            Address = item.Address,
                            Gender = item.Gender
                        });
                }
                return View(patientDetails);
            }
        }

        public ActionResult ViewAppointment()
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            using (MediCureEntities db = new MediCureEntities())
            {
                var docname = db.UserLogins.FirstOrDefault(a => a.EmailID == username).UserName;
                ViewBag.LoggedUserName = docname;
                List<AppointmentModel> appointmentDetails = new List<AppointmentModel>();
                var getdata = from user in db.Appointments
                              select new
                              {
                                  user.AppointmentID,
                                  user.UserName,
                                  user.AppointmentDate,
                                  user.PreferredSlot,
                                  user.Phone,
                                  user.Message
                              };

                foreach (var item in getdata)
                {
                    appointmentDetails.Add
                        (
                        new AppointmentModel
                        {
                            AppointmentID=item.AppointmentID,
                            UserName=item.UserName,
                            date=item.AppointmentDate,
                            PreferredSlot=item.PreferredSlot,
                            Phone=item.Phone,
                            Message=item.Message
                        });
                }
                return View(appointmentDetails);
            }
        }




        [HttpPost]
        public ActionResult SearchPatient(string search)
        {
           
            using (MediCureEntities db = new MediCureEntities())
            {
                var getdata = (from p in db.UserLogins
                               where p.RoleID==1 && p.UserName.Contains(search)
                               select new
                               {
                                   p.LoginID,
                                   p.UserName,
                                   p.Contact,
                                   p.Address,
                                   p.Gender

                               }).ToList();

                List<PatientDetails> lstdata = new List<PatientDetails>();
                foreach (var item in getdata)
                {
                    lstdata.Add(
                        new PatientDetails
                        {
                            LoginID = item.LoginID,
                            UserName = item.UserName,
                            Contact = item.Contact,
                            Address = item.Address,
                            Gender = item.Gender
                        });

                }
                return View("ViewPatient", lstdata);
            }
        }

        //Sample Code

        [HttpPost]
        public ActionResult Place_Order_Final(InputOrder model)
        {
            using (MediCureEntities db = new MediCureEntities())
            {
                Order_Table obj = new Order_Table();
                obj.DrugID = model.DrugID;
                obj.Quantity = model.Quantity;
                obj.Status = "Awaiting approval by Salesperson";
                db.Order_Table.Add(obj);
                db.SaveChanges();
            }
            using (MediCureEntities db = new MediCureEntities())
            {
                var getdata = (from c in db.Order_Table
                               select new
                               {
                                   c.DrugID,
                                   c.OrderID,
                                   c.Quantity,
                                   c.Status,

                               });
                List<InputOrder> datalist = new List<InputOrder>();
                foreach (var item in getdata)
                {
                    datalist.Add(new InputOrder
                    {
                        DrugID = item.DrugID,
                        OrderID = item.OrderID,
                        Quantity = item.Quantity,
                        Status = item.Status,
                    });

                }
                return View(datalist);
            }

        }

        public ActionResult Place_Order()
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            using (MediCureEntities db = new MediCureEntities())
            {
                ViewBag.LoggedUserName = db.UserLogins.FirstOrDefault(a => a.EmailID == username).UserName;
                InputOrder model = new InputOrder();
                model.DrugNames = CommonMethod.GetDrugData();
                return View(model);
            }
        }
    }
    // Code End
}