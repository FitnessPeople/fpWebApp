using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["idUsuario"] = 1;
                Session["NombreUsuario"] = "Christian Morales";
                Session["idEmpresa"] = 1;
                Session["Cargo"] = "WebMaster";
                Session["Foto"] = "chrismo.jpg";
                Session["idPerfil"] = 1;
                if (Session["idUsuario"] != null)
                {

                }
                else
                {
                    Response.Redirect("logout.aspx");
                }
            }
        }
    }
}