using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class micuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltNombreUsuario.Text = Session["NombreUsuario"].ToString();
            ltCargo.Text = Session["Cargo"].ToString();
            ltFoto.Text = "<img src=\"img/empleados/" + Session["Foto"].ToString() + "\" class=\"img-circle circle-border m-b-md\" alt=\"profile\">";
        }
    }
}