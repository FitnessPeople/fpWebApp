using System;
using System.Data;
using System.Web.UI;

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
                    Session["idCanalVenta"] = dt.Rows[0]["idCanalVenta"].ToString();
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
                divUsuario.Visible = false;
                divPassword.Visible = false;
                
                // Crear Código
                int longitudCodigo = 6;
                string codigo = cg.GenerarCodigo(longitudCodigo);

                ltCodigo.Text = codigo;   // Quitar esta línea de código cuando se ponga en producción

                // Si ya tiene código con fecha de hoy, no se pide el codigo... Solo pide una vez por día el código

                string strQuery = "SELECT FechaCodigoIngreso FROM usuarios WHERE idUsuario = " + Session["idUsuario"].ToString();
                DataTable dt = cg.TraerDatos(strQuery);

                string strFechaIngreso = dt.Rows[0]["FechaCodigoIngreso"].ToString();

                if (strFechaIngreso != "")
                {
                    if (Convert.ToDateTime(strFechaIngreso) == DateTime.Now.Date)
                    {
                        cg.InsertarLog(Session["idusuario"].ToString(), "usuarios", "Login", "El usuario inicio sesión.", "", "");
                        Response.Redirect("micuenta");
                    }
                    else
                    {
                        //Inserta el código en la tabla usuarios
                        strQuery = "UPDATE usuarios " +
                            "SET CodigoIngreso = '" + codigo + "', FechaCodigoIngreso = CURDATE() " +
                            "WHERE idUsuario = " + Session["idUsuario"].ToString();
                        cg.TraerDatosStr(strQuery);

                        //Enviar por correo
                        //cg.EnviarCorreo("info@fitnesspeoplecmd.com", Session["usuario"].ToString(), "Clave acceso", "Clave de acceso: " + codigo);

                        //Mostrar div para escribir el código
                        divCodigo.Visible = true;
                    }
                }
                else
                {
                    //Inserta el código en la tabla usuarios
                    strQuery = "UPDATE usuarios " +
                        "SET CodigoIngreso = '" + codigo + "', FechaCodigoIngreso = CURDATE() " +
                        "WHERE idUsuario = " + Session["idUsuario"].ToString();
                    cg.TraerDatosStr(strQuery);

                    //Enviar por correo
                    //cg.EnviarCorreo("info@fitnesspeoplecmd.com", Session["usuario"].ToString(), "Clave acceso", "Clave de acceso: " + codigo);

                    //Mostrar div para escribir el código
                    divCodigo.Visible = true;
                }

            }
        }

        protected void btnIngresarCodigo_Click(object sender, EventArgs e)
        {
            string strMensaje;
            string strQuery = "SELECT * FROM usuarios " +
                "WHERE idUsuario = " + Session["idUsuario"].ToString() + " " +
                "AND CodigoIngreso = '" + txbCodigo.Text.ToString() + "' ";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                // Ingresa a FP+
                cg.InsertarLog(Session["idusuario"].ToString(), "usuarios", "Login", "El usuario inicio sesión.", "", "");
                Response.Redirect("micuenta");
            }
            else
            {
                strMensaje = "Código errado.<br />";
                strMensaje += "<a class=\"alert-link\" href=\"#\">Intente nuevamente</a>.";
                ltMensaje.Text = strMensaje;
                divMensaje.Visible = true;
            }
        }
    }
}