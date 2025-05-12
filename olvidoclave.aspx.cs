using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class olvidoclave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblAnho.Text = DateTime.Now.Year.ToString();
        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            string usuario = txbEmail.Text.ToString() + ddlDominio.SelectedItem.Value.ToString();
            string strQuery = "SELECT EmailUsuario, ClaveUsuario FROM usuarios WHERE EmailUsuario = '" + usuario + "' ";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                string strMensaje = "Ha solicitado la recuperación de su clave.\r\n";
                strMensaje += "Por favor, diríjase al siguiente enlace para restaurarla:\r\n";
                //strMensaje += "<a href=\"recuperacionclave?u=" + usuario + "&p=" + dt.Rows[0]["ClaveUsuario"].ToString() + "\">Enlace</a>\r\n";
                strMensaje += "https://fpadmin.fitnesspeoplecolombia.com/recuperacionclave?u=" + usuario + "&p=" + dt.Rows[0]["ClaveUsuario"].ToString() + "\r\n\r\n";
                strMensaje += "Att. Sistemas Fitness People.\r\n";
                cg.EnviarCorreo("sistemas@fitnesspeoplecmd.com", usuario, "Recuperación de clave", strMensaje);

                strMensaje = "Ha solicitado la recuperación de su clave.<br />";
                strMensaje += "Revise su correo electrónico y siga las instrucciones.<br />";
                strMensaje += "<a class=\"alert-link\" href=\"default\">Regresar al inicio</a>.";
                ltMensaje.Text = strMensaje;
                divMensaje.Visible = true;
            }
            else
            {
                string strMensaje = "El email ingresado no existe en el sistema.<br />";
                strMensaje += "<a class=\"alert-link\" href=\"#\">Intente nuevamente</a>.";
                ltMensaje.Text = strMensaje;
                divMensaje.Visible = true;
            }
        }
    }
}