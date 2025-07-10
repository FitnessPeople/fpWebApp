using System;
using System.Web;
using System.Web.Routing;

namespace fpWebApp
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Application["VisitorsCount"] = 0;
        }

        void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["VisitorsCount"] = (int)Application["VisitorsCount"] + 1;
            Application.UnLock();
        }
    }
}