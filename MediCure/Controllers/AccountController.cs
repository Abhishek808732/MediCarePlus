using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MediCure.Models;

namespace MediCure.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult UserLogin()
        {
            if (Request.IsAuthenticated == true)
                return RedirectToAction("Index", "MediCureMember");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (MediCureEntities db = new MediCureEntities())
            {
                //var checkdata = db.Users.FirstOrDefault(a => a.EmailID == model.EmailId && a.Password == model.Password);
                var checkdata = (from user in db.UserLogins
                                 join role in db.RoleDetails
                                 on user.RoleID equals role.RoleID
                                 where user.EmailID == model.EmailID && user.Password == model.Password
                                 select new
                                 {
                                     user.EmailID,
                                     role.RoleName
                                 }).FirstOrDefault();

                if (checkdata != null)
                {
                    //Navigate to proper page
                    FormsAuthentication.SetAuthCookie(model.EmailID, false);
                    var authticket = new FormsAuthenticationTicket(1, checkdata.EmailID, DateTime.Now, DateTime.Now.AddMinutes(30), false, checkdata.RoleName);
                    string encryptticket = FormsAuthentication.Encrypt(authticket);
                    var authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptticket);
                    HttpContext.Response.Cookies.Add(authcookie);
                    return RedirectToAction("Index", "MediCureMember");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid EmailId or Password";
                    return View(model);
                }
            }
        }


        public ActionResult UserLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("UserLogin", "Account");
        }
    }
}