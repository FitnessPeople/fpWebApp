using fpWebApp.Services;
using System;
using System.Collections.Generic;

namespace fpWebApp.controles
{
    public partial class footer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var lista = Application["ListaUsuarios"] as List<UsuarioOnline>;
            lblCount.Text = lista != null ? lista.Count.ToString() : "0";

            lblAnho.Text = DateTime.Now.Year.ToString();
        }
    }
}