using fpWebApp.Services;
using System;
using System.Collections.Generic;

namespace fpWebApp
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idusuario"] != null)
            {
                clasesglobales cg = new clasesglobales();
                cg.InsertarLog(Session["idusuario"].ToString(), "usuarios", "Logout", "El usuario cerró sesión.", "", "");

                Application.Lock();
                var lista = (List<UsuarioOnline>)Application["ListaUsuarios"];
                lista.RemoveAll(x => x.Usuario == Session["NombreUsuario"].ToString());
                Application["ListaUsuarios"] = lista;
                Application.UnLock();
            }

            Session.RemoveAll();
            Session.Abandon();
            Session.Clear();
            Response.Redirect("default");
        }
    }
}