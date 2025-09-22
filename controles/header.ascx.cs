using System;

namespace fpWebApp.controles
{
    public partial class header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idPerfil"].ToString() != "1")
            {
                divCambioPerfil.Attributes.Add("style", "display: none;");
            }
        }
    }
}