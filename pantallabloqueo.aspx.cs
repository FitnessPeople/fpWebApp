using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class pantallabloqueo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltUsuario.Text = Session["NombreUsuario"].ToString();
                ltFoto.Text = "<img alt=\"image\" class=\"img-circle circle-border\" src=\"img/empleados/" + Session["Foto"].ToString() + "\">";
                Session["idUsuario"] = null;
            }
        }

        protected void btnDesbloquear_Click(object sender, EventArgs e)
        {
            string usuario = Session["usuario"].ToString();
            string clave = txbPassword.Text.ToString();

            clasesglobales cg = new clasesglobales();
            string strHashClave = cg.ComputeSha256Hash(clave);

            if (YourValidationFunction(usuario, strHashClave))
            {
                cg.InsertarLog(Session["idusuario"].ToString(), "usuarios", "Login", "El usuario inicio sesión.", "", "");

                if (Request.QueryString.Count > 0)
                {
                    Response.Redirect(Request.QueryString["page"].ToString());
                }
                else
                {
                    Response.Redirect("inicio");
                }
            }
        }

        private bool YourValidationFunction(string UserName, string Password)
        {
            bool boolReturnValue = false;
            UserName = UserName.Replace("'", "");

            string strQuery = "SELECT u.*, e.*, p.* " +
                "FROM Usuarios u " +
                "LEFT JOIN Empleados p ON u.idEmpleado = p.DocumentoEmpleado " +
                "LEFT JOIN Empresas e ON u.idEmpresa = e.idEmpresa " +
                "WHERE u.EmailUsuario = '" + UserName + "' " +
                "AND u.ClaveUsuario = '" + Password + "' ";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            string strMensaje;

            if (dt.Rows.Count > 0)
            {
                Session["idUsuario"] = dt.Rows[0]["idUsuario"].ToString();
                boolReturnValue = true;
            }
            else
            {
                strMensaje = "Contraseña errada.<br />";
                strMensaje += "<a class=\"alert-link\" href=\"#\">Intente nuevamente</a>.";
                ltMensaje.Text = strMensaje;
                divMensaje.Visible = true;
            }

            dt.Dispose();

            return boolReturnValue;
        }
    }
}