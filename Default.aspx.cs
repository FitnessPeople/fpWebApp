using System;
using System.Data;
using System.Web.Services.Description;
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

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            //string usuario = txbEmail.Text.ToString() + ddlDominio.SelectedItem.Value.ToString();
            string usuario = txbEmail.Text.ToString();
            string clave = txbPassword.Text.ToString();

            clasesglobales cg = new clasesglobales();
            string strHashClave = cg.ComputeSha256Hash(clave);

            if (ValidacionUsuario(usuario, strHashClave))
            {
                divUsuario.Visible = false;
                divPassword.Visible = false;
                btnIngresar.Visible = false;

                // Crear Código
                int longitudCodigo = 6;
                string codigo = cg.GenerarCodigo(longitudCodigo);

                ltCodigo.Text = codigo;   // Quitar esta línea de código cuando se ponga en producción

                // Si ya tiene código con fecha de hoy, no se pide el codigo... Solo pide el código una vez por día.

                DataTable dt = cg.ConsultarFechaCodigo(Convert.ToInt32(Session["idUsuario"].ToString()));

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
                        cg.ActualizarCodigoUsuario(Convert.ToInt32(Session["idUsuario"].ToString()), codigo);

                        //Enviar por correo
                        //cg.EnviarCorreo("info@fitnesspeoplecmd.com", Session["usuario"].ToString(), "Clave acceso", "Clave de acceso: " + codigo);

                        //Mostrar div para escribir el código
                        divCodigo.Visible = true;
                    }
                }
                else
                {
                    cg.ActualizarCodigoUsuario(Convert.ToInt32(Session["idUsuario"].ToString()), codigo);

                    //Enviar por correo
                    //cg.EnviarCorreo("info@fitnesspeoplecmd.com", Session["usuario"].ToString(), "Clave acceso", "Clave de acceso: " + codigo);

                    //Mostrar div para escribir el código
                    divCodigo.Visible = true;
                }

            }
        }

        private bool ValidacionUsuario(string UserName, string Password)
        {
            bool boolReturnValue = false;
            UserName = UserName.Replace("'", "");

            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ValidarUsuario(UserName, Password);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["EstadoUsuario"].ToString() == "Inactivo")
                    {
                        MostrarAlerta("Usuario inactivo", "Consulte al administrador", "error");
                    }
                    else
                    {
                        Session["idUsuario"] = dt.Rows[0]["idUsuario"].ToString();
                        Session["NombreUsuario"] = dt.Rows[0]["NombreUsuario"].ToString();
                        Session["idEmpresa"] = dt.Rows[0]["idEmpresa"].ToString();
                        Session["Cargo"] = dt.Rows[0]["CargoUsuario"].ToString();
                        Session["Foto"] = dt.Rows[0]["FotoEmpleado"].ToString();
                        Session["idPerfil"] = dt.Rows[0]["idPerfil"].ToString();
                        Session["emailUsuario"] = dt.Rows[0]["EmailUsuario"].ToString();
                        //Session["idSede"] = dt.Rows[0]["idSede"].ToString();
                        Session["fechaNac"] = string.IsNullOrEmpty(dt.Rows[0]["FechaNacEmpleado"]?.ToString())
                            ? "2001-01-01"
                            : dt.Rows[0]["FechaNacEmpleado"].ToString();
                        Session["idSede"] = string.IsNullOrEmpty(dt.Rows[0]["idSede"]?.ToString())
                            ? "11"   // Sede Administrativa
                            : dt.Rows[0]["idSede"].ToString();
                        Session["idCanalVenta"] = string.IsNullOrEmpty(dt.Rows[0]["idCanalVenta"]?.ToString())
                            ? "12"   // Online
                            : dt.Rows[0]["idCanalVenta"].ToString();
                        Session["idEmpleado"] = dt.Rows[0]["idEmpleado"].ToString();
                        boolReturnValue = true;
                    }
                }
                else
                {
                    MostrarAlerta("Identificación o contraseña errada.", "Intente nuevamente.", "error");
                }

                dt.Dispose();
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error de conexión.", ex.Message.ToString(), "warning");
            }
            
            //string strMensaje;

            return boolReturnValue;
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            // tipo puede ser: 'success', 'error', 'warning', 'info', 'question'
            string script = $@"
            Swal.fire({{
                title: '{titulo}',
                text: '{mensaje}',
                icon: '{tipo}', 
                background: '#3C3C3C', 
                showCloseButton: true, 
                confirmButtonText: 'Aceptar', 
                customClass: {{
                    popup: 'alert',
                    confirmButton: 'btn-confirm-alert'
                }},
            }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
        }

        protected void btnIngresarCodigo_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.RevisarCodigo(Convert.ToInt32(Session["idUsuario"].ToString()), txbCodigo.Text.ToString());

            if (dt.Rows.Count > 0)
            {
                // Ingresa a FP+
                cg.InsertarLog(Session["idusuario"].ToString(), "usuarios", "Login", "El usuario inicio sesión.", "", "");
                Response.Redirect("micuenta");
            }
            else
            {
                MostrarAlerta("Código errado.", "Intente nuevamente.", "error");
            }
        }
    }
}