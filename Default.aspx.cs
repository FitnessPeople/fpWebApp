using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.OpenXmlFormats.Spreadsheet;

namespace fpWebApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblAnho.Text = DateTime.Now.Year.ToString();
            txbEmail.Focus();
        }

        private bool YourValidationFunction(string UserName, string Password)
        {
            bool boolReturnValue = false;
            UserName = UserName.Replace("'", "");

            //string strQuery = "SELECT u.*, e.*, p.* " +
            //    "FROM Usuarios u " +
            //    "LEFT JOIN Empleados p ON u.idEmpleado = p.DocumentoEmpleado " +
            //    "LEFT JOIN Empresas e ON u.idEmpresa = e.idEmpresa " +
            //    "WHERE u.EmailUsuario = '" + UserName + "' " +
            //    "AND u.ClaveUsuario = '" + Password + "' ";
            string strQuery = "SELECT u.*, e.*, p.* " +
                "FROM Usuarios u " +
                "LEFT JOIN Empleados p ON u.idEmpleado = p.DocumentoEmpleado " +
                "LEFT JOIN Empresas e ON u.idEmpresa = e.idEmpresa " +
                "WHERE u.idEmpleado = '" + UserName + "' " +
                "AND u.ClaveUsuario = '" + Password + "' ";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            string strMensaje;

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["EstadoUsuario"].ToString() == "Inactivo")
                {
                    strMensaje = "Usuario inactivo.<br />";
                    strMensaje += "<a class=\"alert-link\" href=\"soporte\">Consulte al administrador.</a>.";
                    ltMensaje.Text = strMensaje;
                    divMensaje.Visible = true;
                }
                else
                {
                    Session["idUsuario"] = dt.Rows[0]["idUsuario"].ToString();
                    Session["NombreUsuario"] = dt.Rows[0]["NombreUsuario"].ToString();
                    Session["idEmpresa"] = dt.Rows[0]["idEmpresa"].ToString();
                    Session["Cargo"] = dt.Rows[0]["CargoUsuario"].ToString();
                    Session["Foto"] = dt.Rows[0]["FotoEmpleado"].ToString();
                    Session["idPerfil"] = dt.Rows[0]["idPerfil"].ToString();
                    Session["usuario"] = dt.Rows[0]["EmailUsuario"].ToString();
                    Session["idSede"] = dt.Rows[0]["idSede"].ToString();
                    Session["idEmpleado"] = dt.Rows[0]["idEmpleado"].ToString();
                    boolReturnValue = true;
                }
            }
            else
            {
                //strMensaje = "Email o contraseña errada.<br />";
                strMensaje = "Identificación o contraseña errada.<br />";
                strMensaje += "<a class=\"alert-link\" href=\"#\">Intente nuevamente</a>.";
                ltMensaje.Text = strMensaje;
                divMensaje.Visible = true;
            }

            dt.Dispose();

            return boolReturnValue;
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            //string usuario = txbEmail.Text.ToString() + ddlDominio.SelectedItem.Value.ToString();
            string usuario = txbEmail.Text.ToString();
            string clave = txbPassword.Text.ToString();

            clasesglobales cg = new clasesglobales();
            string strHashClave = cg.ComputeSha256Hash(clave);

            if (YourValidationFunction(usuario, strHashClave))
            {
                cg.InsertarLog(Session["idusuario"].ToString(), "usuarios", "Login", "El usuario inicio sesión.", "", "");
                Response.Redirect("micuenta");
            }
        }
    }
}