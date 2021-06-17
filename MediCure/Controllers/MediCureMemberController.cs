using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MediCure.Models;

namespace MediCure.Controllers
{
    [Authorize]
    [OutputCache(NoStore =true , Location =System.Web.UI.OutputCacheLocation.None)]
    public class MediCureMemberController : Controller
    {
        // GET: MediCureMember
        [HttpGet]
        public ActionResult Index()
        {
            if (Request.IsAuthenticated == false)
               return RedirectToAction("UserLogin", "Account");

            var username = System.Web.HttpContext.Current.User.Identity.Name;
            using (MediCureEntities db = new MediCureEntities())
            {
                var rolename = db.UserLogins.FirstOrDefault(a => a.EmailID == username).RoleID;

                if (rolename == 1)
                {
                    //REDIRECT TO PATIENT
                    return RedirectToAction("Index", "Patient");
                }
                else if (rolename == 2)
                {
                    //REDIRECT TO PHYSICIAN
                    return RedirectToAction("Index", "Physician");
                }
                else if (rolename == 3)
                {
                    //REDIRECT TO SUPPLIER
                    return RedirectToAction("Index", "Supplier");
                }
                else if (rolename == 4)
                {
                    //REDIRECT TO SALESMAN
                    return RedirectToAction("Index", "SalesMan");
                }
                else
                { 
                    ViewBag.LoggedUserName = db.UserLogins.FirstOrDefault(a => a.EmailID == username).UserName;
                    LoginModel model = new LoginModel();
                    model.lstrolenames = CommonMethod.RoleData();
                    return View(model);
                }
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

        public ActionResult InsertUserData(LoginModel model)
        {
            if (model.LoginID > 0)
            {
                //update
                using (MediCureEntities db = new MediCureEntities())
                {
                    var updatedata = db.UserLogins.FirstOrDefault(a => a.LoginID == model.LoginID);
                    updatedata.UserName = model.UserName;
                    updatedata.Gender = model.Gender;
                    updatedata.Contact = model.Contact;
                    updatedata.EmailID = model.EmailID;
                    updatedata.Address = model.Address;
                    updatedata.RoleID = model.RoleID;
                    updatedata.Password = model.Password;

                    db.SaveChanges();
                }
            }

            else
            {
                //insert
                using (MediCureEntities db = new MediCureEntities())
                {
                    UserLogin user = new UserLogin();
                    user.UserName = model.UserName;
                    user.Contact = model.Contact;
                    user.Gender = model.Gender;
                    user.EmailID = model.EmailID;
                    user.RoleID = model.RoleID;
                    user.Address = model.Address;
                    user.Password = model.Password;

                    db.UserLogins.Add(user);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ViewUserData", "MediCureMember");
        }


        public ActionResult ViewUserData()
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;

            using (MediCureEntities db = new MediCureEntities())
            {
                ViewBag.LoggedUserName = db.UserLogins.FirstOrDefault(a => a.EmailID == username).UserName;
                var getdata = (from u in db.UserLogins
                               join r in db.RoleDetails
                               on u.RoleID equals r.RoleID
                               select new
                               {
                                   u.LoginID,
                                   u.UserName,
                                   u.Gender,
                                   u.EmailID,
                                   u.Contact,
                                   u.Address,
                                   r.RoleName,
                                   u.Password
                               }).ToList();

                List<LoginModel> lstdata = new List<LoginModel>();

                foreach (var item in getdata)
                {
                    lstdata.Add(new LoginModel
                    {
                        LoginID = item.LoginID,
                        UserName = item.UserName,
                        Address = item.Address,
                        Contact = item.Contact,
                        EmailID = item.EmailID,
                        Gender = item.Gender,
                        RoleName = item.RoleName,
                        Password = item.Password
                    });
                }
                return View(lstdata);
            }

        }

        public ActionResult SearchUserData(string searchuser)
        {
            using (MediCureEntities db = new MediCureEntities())
            {
                var getdata = (from u in db.UserLogins
                               join r in db.RoleDetails
                               on u.RoleID equals r.RoleID
                               where (u.UserName.Contains(searchuser) || r.RoleName.Contains(searchuser))
                               select new
                               {
                                   u.LoginID,
                                   u.UserName,
                                   u.Gender,
                                   u.EmailID,
                                   u.Contact,
                                   u.Address,
                                   r.RoleName,
                                   u.Password
                               }).ToList();

                List<LoginModel> lstdata = new List<LoginModel>();

                foreach (var item in getdata)
                {
                    lstdata.Add(new LoginModel
                    {
                        LoginID = item.LoginID,
                        UserName = item.UserName,
                        Address = item.Address,
                        Contact = item.Contact,
                        EmailID = item.EmailID,
                        Gender = item.Gender,
                        RoleName = item.RoleName,
                        Password = item.Password
                    });
                }
                return View("ViewUserData", lstdata);
            }

        }

        public ActionResult EditUserData(int id)
        {
            using (MediCureEntities db = new MediCureEntities())
            {
                var getdata = db.UserLogins.FirstOrDefault(a => a.LoginID == id);

                LoginModel userModel = new LoginModel();
                userModel.LoginID = getdata.LoginID;
                userModel.UserName = getdata.UserName;
                userModel.Gender = getdata.Gender;
                userModel.EmailID = getdata.EmailID;
                userModel.Contact = getdata.Contact;
                userModel.Address = getdata.Address;
                userModel.RoleID = getdata.RoleID;
                userModel.Password = getdata.Password;
                userModel.lstrolenames = CommonMethod.RoleData();

                return View("Index", userModel);
            }

        }

        public ActionResult DeleteUserData(int id)
        {
            using (MediCureEntities db = new MediCureEntities())
            {
                var deletedata = db.UserLogins.FirstOrDefault(a => a.LoginID == id);

                db.UserLogins.Remove(deletedata);
                db.SaveChanges();

                return RedirectToAction("ViewUserData", "MediCureMember");
            }
        }

    }
}