using fpWebApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;

namespace fpWebApp
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                lblAnho.Text = DateTime.Now.Year.ToString();
                txbIdentificacion.Focus();
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            //string usuario = txbEmail.Text.ToString() + ddlDominio.SelectedItem.Value.ToString();
            string usuario = txbIdentificacion.Text.ToString();
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
                        //cg.ActualizarCodigoUsuario(Convert.ToInt32(Session["idUsuario"].ToString()), codigo);
                        Session["codigo"] = codigo;

                        //Enviar por correo
                        //cg.EnviarCorreo("info@fitnesspeoplecmd.com", Session["usuario"].ToString(), "Clave acceso", "Clave de acceso: " + codigo);

                        //Enviar a pagina de ingreso de codigo
                        //Response.Redirect("confirmarcodigo");                     Se debe dejar esta línea en producción
                        Response.Redirect("confirmarcodigo?ticket=" + codigo);      //Se debe comentar esta línea en producción
                    }
                }
                else
                {
                    //cg.ActualizarCodigoUsuario(Convert.ToInt32(Session["idUsuario"].ToString()), codigo);
                    Session["codigo"] = codigo;

                    //Enviar por correo
                    //cg.EnviarCorreo("info@fitnesspeoplecmd.com", Session["usuario"].ToString(), "Clave acceso", "Clave de acceso: " + codigo);

                    //Enviar a pagina de ingreso de codigo
                    //Response.Redirect("confirmarcodigo");                     Se debe dejar esta línea en producción
                    Response.Redirect("confirmarcodigo?ticket=" + codigo);      //Se debe comentar esta línea en producción
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
                //DataTable dt1 = cg.ValidarUsuario(UserName, Password);
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
                        Session["idCargoUsuario"] = dt.Rows[0]["idCargoUsuario"].ToString();
                        Session["CargoUsuario"] = dt.Rows[0]["NombreCargo"].ToString();
                        Session["Foto"] = dt.Rows[0]["FotoEmpleado"].ToString();
                        Session["idPerfil"] = dt.Rows[0]["idPerfil"].ToString();
                        Session["Perfil"] = dt.Rows[0]["Perfil"].ToString();
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

                        // Lista de usuarios en línea
                        Application.Lock();
                        var lista = (List<UsuarioOnline>)Application["ListaUsuarios"];

                        if (!lista.Any(x => x.Usuario == Session["NombreUsuario"].ToString()))
                        {
                            lista.Add(new UsuarioOnline
                            {
                                Usuario = Session["NombreUsuario"].ToString(),
                                Cargo = Session["CargoUsuario"].ToString(),
                                Foto = Session["Foto"].ToString()
                            });
                        }

                        Application["ListaUsuarios"] = lista;
                        Application.UnLock();

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
                showCloseButton: true, 
                confirmButtonText: 'Aceptar', 
            }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
        }

        
    }
}