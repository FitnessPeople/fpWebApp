﻿using System;

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
            }
            Session.RemoveAll();
            Session.Abandon();
            Session.Clear();
            Response.Redirect("default.aspx");
        }
    }
}