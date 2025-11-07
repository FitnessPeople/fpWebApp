using System;
using System.Collections.Generic;
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
            Application["ListaUsuarios"] = new List<string>();
        }

        void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["VisitorsCount"] = (int)Application["VisitorsCount"] + 1;
            Application.UnLock();
        }

        void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["VisitorsCount"] = (int)Application["VisitorsCount"] - 1;

            var lista = (List<string>)Application["ListaUsuarios"];
            if (Session["NombreUsuario"] != null)
            {
                lista.Remove(Session["NombreUsuario"].ToString());
            }

            Application["ListaUsuarios"] = lista;

            Application.UnLock();
        }
    }
}