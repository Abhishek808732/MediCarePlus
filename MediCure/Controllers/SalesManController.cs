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
    public class SalesManController : Controller
    {
        // GET: SalesMan
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

        [HttpGet]
        public ActionResult FirstPage()
        {
            return View();
        }

        public ActionResult DrugList()
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            using (MediCureEntities db = new MediCureEntities())
            {
                ViewBag.LoggedUserName = db.UserLogins.FirstOrDefault(a => a.EmailID == username).UserName;
            }
            MediCureEntities DB = new MediCureEntities();
            List<Drug_Table> drugs = DB.Drug_Table.ToList();
            return View(drugs);
        }
        public ActionResult Payment(SupplierModel model)
        {
            try
            {
                MediCureEntities db = new MediCureEntities();
                Order_Table sup = new Order_Table();
                //sup.LoginID = model.LoginID;
                sup.OrderID = model.OrderID;
                sup.DrugID = (int)model.DrugID;
                sup.Quantity = (int)model.Quantity;
                sup.Status = model.Status;
                db.Order_Table.Add(sup);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            return RedirectToAction("DrugList");
        }

        [HttpGet]
        public ActionResult OrderDetails()
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            using (MediCureEntities db = new MediCureEntities())
            {
                ViewBag.LoggedUserName = db.UserLogins.FirstOrDefault(a => a.EmailID == username).UserName;
                var getdata = (from c in db.Order_Table
                               select new
                               {
                                   c.DrugID,
                                   c.OrderID,
                                   c.Quantity,
                                   c.Status
                               });
                List<Salespersonmodel> lstData = new List<Salespersonmodel>();
                foreach (var item in getdata)
                {
                    lstData.Add(new Salespersonmodel
                    {
                        OrderID = item.OrderID,
                        DrugID = item.DrugID,
                        Quantity = item.Quantity,
                        Status = item.Status
                    });
                }

                return View(lstData);
            }
        }
        [HttpGet]
        public ActionResult AcceptedOrdered(int id)
        {
            Salespersonmodel model = new Salespersonmodel();
            using (MediCureEntities db = new MediCureEntities())
            {
                model.Status = "Awaiting approval by Supplier";
                var updatedata = db.Order_Table.FirstOrDefault(a => a.OrderID == id);
                updatedata.Status = model.Status;

                db.SaveChanges();
                return RedirectToAction("OrderDetails");
            }

        }
        [HttpGet]
        public ActionResult RejectOrdered(int id)
        {
            Salespersonmodel model = new Salespersonmodel();
            using (MediCureEntities db = new MediCureEntities())
            {
                model.Status = "Rejected by Supplier";
                var updatedata = db.Order_Table.FirstOrDefault(a => a.OrderID == id);
                updatedata.Status = model.Status;

                db.SaveChanges();
                return RedirectToAction("OrderDetails");
            }

        }
    }
}