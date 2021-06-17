using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace MediCure
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var authcookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authcookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authcookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    //Role Based
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new GenericPrincipal(new FormsIdentity(authTicket), roles);
                }
            }
        }
    }
}
