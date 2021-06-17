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
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index()
        {
            if (Request.IsAuthenticated == false)
                return RedirectToAction("UserLogin", "Account");

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

                List<SupplierModel> lstData = new List<SupplierModel>();
                foreach (var item in getdata)
                {
                    lstData.Add(new SupplierModel
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
        public ActionResult DrugList()
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            using (MediCureEntities db = new MediCureEntities())
            {
                ViewBag.LoggedUserName = db.UserLogins.FirstOrDefault(a => a.EmailID == username).UserName;
                var getdata = (from c in db.Drug_Table
                               select new
                               {
                                   c.DrugID,
                                   c.DrugName,
                                   c.DrugDescription,
                                   c.DrugQuantity,
                                   c.Manufacturer,
                                   c.UsedFor
                               });
                List<SupplierModel> datalist = new List<SupplierModel>();
                foreach (var item in getdata)
                {
                    datalist.Add(new SupplierModel
                    {
                        DrugID = item.DrugID,
                        DrugName = item.DrugName,
                        DrugDescription = item.DrugDescription,
                        DrugQuantity = item.DrugQuantity,
                        Manufacturer = item.Manufacturer,
                        UsedFor = item.UsedFor
                    });

                }
                return View(datalist);
            }
        }


        [HttpGet]
        public ActionResult AcceptedOrdered(int id)
        {
            SupplierModel model = new SupplierModel();
            using (MediCureEntities db = new MediCureEntities())
            {
                model.Status = "Accepted by Supplier";
                var updatedata = db.Order_Table.FirstOrDefault(a => a.OrderID == id);
                updatedata.Status = model.Status;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }
        [HttpGet]
        public ActionResult RejectOrdered(int id)
        {
            SupplierModel model = new SupplierModel();
            using (MediCureEntities db = new MediCureEntities())
            {
                model.Status = "Rejected by Supplier";
                var updatedata = db.Order_Table.FirstOrDefault(a => a.OrderID == id);
                updatedata.Status = model.Status;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }
    }
}