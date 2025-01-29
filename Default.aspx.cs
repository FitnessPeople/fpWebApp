using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txbEmail.Attributes.Add("type", "email");
        }

        private bool YourValidationFunction(string UserName, string Password)
        {
            bool boolReturnValue = false;
            Password = Password.Replace("'", "");
            UserName = UserName.Replace("'", "");
            string strQuery = "SELECT u.*, e.*, p.* " +
                "FROM Usuarios u " +
                "LEFT JOIN Empleados p ON u.idEmpleado = p.DocumentoEmpleado " +
                "LEFT JOIN Empresas e ON u.idEmpresa = e.idEmpresa " +
                "WHERE u.EmailUsuario = '" + UserName + "' " +
                "AND u.ClaveUsuario = '" + Password + "' " +
                "AND u.EstadoUsuario = 'Activo' ";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
            //DataTable dt = TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                Session["idusuario"] = dt.Rows[0]["idUsuario"].ToString();
                Session["NombreUsuario"] = dt.Rows[0]["NombreUsuario"].ToString();
                Session["idEmpresa"] = dt.Rows[0]["idEmpresa"].ToString();
                Session["Cargo"] = dt.Rows[0]["CargoUsuario"].ToString();
                Session["Foto"] = dt.Rows[0]["FotoEmpleado"].ToString();
                Session["idPerfil"] = dt.Rows[0]["idPerfil"].ToString();
                boolReturnValue = true;
            }
            else
            {
                divMensaje.Visible = true;
            }

            dt.Dispose();

            return boolReturnValue;
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txbEmail.Text.ToString();
            string clave = txbPassword.Text.ToString();

            if (YourValidationFunction(usuario, clave))
            {
                clasesglobales cg = new clasesglobales();
                cg.InsertarLog(Session["idusuario"].ToString(), "usuarios", "Login", "El usuario inicio sesión.", "", "");
                Response.Redirect("inicio");
            }
        }
    }
}