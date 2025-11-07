using fpWebApp.Services;
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
            //Application["ListaUsuarios"] = new List<string>();
            Application["ListaUsuarios"] = new List<UsuarioOnline>();
        }

        void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["VisitorsCount"] = (int)Application["VisitorsCount"] + 1;
            Application.UnLock();
        }

        void Session_End(object sender, EventArgs e)
        {
            // Si por alguna razón no hay usuario en sesión, no hacemos nada.
            if (Session["Usuario"] == null) return;

            string usuario = Session["NombreUsuario"].ToString();

            Application.Lock();
            var lista = (List<UsuarioOnline>)Application["ListaUsuarios"];

            // Eliminamos al usuario cuyo nombre coincide con el que expiró
            lista.RemoveAll(x => x.Usuario == usuario);

            Application["ListaUsuarios"] = lista;
            Application.UnLock();
        }
    }
}