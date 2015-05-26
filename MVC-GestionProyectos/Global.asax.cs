using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MVC_GestionProyectos.Seguridad;

namespace MVC_GestionProyectos
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //protected void Application_Start(object sender, EventArgs e)
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Bootstrapper.Initialise();

            //Application["CountOnlineUsers"] = 0;
            //Application["OnlineUsers"] = 0;
        }

        protected void Application_PostAuthenticateRequest(object sender,
            EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                var identity =
                    new ProveedorIdentidad(HttpContext.Current.User.Identity);
                var principal = new PrincipalPersonalizado(identity);
                HttpContext.Current.User = principal;
            }
        }

        //void Session_Start(object sender, EventArgs e)
        //{
        //    Application.Lock();
        //    Application["OnlineUsers"] = (int)Application["OnlineUsers"] + 1;
        //    Application.UnLock();
        //}

        //void Session_End(object sender, EventArgs e)
        //{
        //    Application.Lock();
        //    Application["OnlineUsers"] = (int)Application["OnlineUsers"] - 1;
        //    Application.UnLock();
        //}
    }
}
