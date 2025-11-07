using System;
using System.Collections.Generic;

namespace fpWebApp.controles
{
    public partial class header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["idPerfil"].ToString() != "1")
            //{
                //divCambioPerfil.Attributes.Add("style", "display: none;");
                ltNroUsuarios.Text = Application["VisitorsCount"].ToString();

                var listaUsuarios = (List<string>)Application["ListaUsuarios"];
                rpUsuariosEnLinea.DataSource = listaUsuarios;
                rpUsuariosEnLinea.DataBind();

            //}
        }
    }
}