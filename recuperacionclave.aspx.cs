using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class recuperacionclave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblAnho.Text = DateTime.Now.Year.ToString();
                if (Request.QueryString["u"] != null && Request.QueryString["p"] != null)
                {
                    ltUsuario.Text = Request.QueryString["u"].ToString();
                    string strUsuario = Request.QueryString["u"].ToString();
                    string strClave = Request.QueryString["p"].ToString();
                    string strQuery = "SELECT * FROM usuarios " +
                        "WHERE EmailUsuario = '" + strUsuario + "' " +
                        "AND ClaveUsuario = '" + strClave + "' ";

                    clasesglobales cg = new clasesglobales();
                    DataTable dt = cg.TraerDatos(strQuery);

                    if (dt.Rows.Count == 0)
                    {
                        Response.Redirect("default");
                    }
                }
                else
                {
                    Response.Redirect("default");
                }
            }
        }

        protected void btnRestaurar_Click(object sender, EventArgs e)
        {
            string strNuevaClave = txbNuevaClave.Text.ToString().Trim();

            clasesglobales cg = new clasesglobales();
            string strHashClave = cg.ComputeSha256Hash(strNuevaClave);

            string strQuery = "UPDATE Usuarios SET ClaveUsuario = '" + strHashClave + "' " +
                "WHERE EmailUsuario = '" + Request.QueryString["u"].ToString() + "' ";

            string strRespuesta = cg.TraerDatosStr(strQuery);

            if (strRespuesta == "OK")
            {
                string strMensaje = "Su clave ha sido restaurada con éxito.<br />";
                strMensaje += "<a class=\"alert-link\" href=\"default\">Regresar al inicio</a>.";
                ltMensaje.Text = strMensaje;
                divMensaje.Visible = true;
            }
            else
            {
                string strMensaje = "Error al restaurar la clave.<br />";
                strMensaje += "<a class=\"alert-link\" href=\"#\">Intente nuevamente</a>.";
                ltMensaje.Text = strMensaje;
                divMensaje.Visible = true;
            }
        }
    }
}