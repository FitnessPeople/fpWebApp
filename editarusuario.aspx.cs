using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class editarusuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Usuarios");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        txbEmail.Attributes.Add("type", "email");
                        ddlEmpleados.Enabled = false;
                        txbClave.Enabled = false;
                        txbClave.Attributes.Add("autocomplete", "off");
                        txbEmail.Attributes.Add("autocomplete", "off");
                        CargarCargos();
                        CargarPerfiles();
                        CargarEmpleados();
                        CargarCanalesVenta();
                        CargarDatosUsuario();

                    }
                    else
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("logout");
                }
            }
        }

        private void ValidarPermisos(string strPagina)
        {
            ViewState["SinPermiso"] = "1";
            ViewState["Consulta"] = "0";
            ViewState["Exportar"] = "0";
            ViewState["CrearModificar"] = "0";
            ViewState["Borrar"] = "0";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ValidarPermisos(strPagina, Session["idPerfil"].ToString(), Session["idusuario"].ToString());

            if (dt.Rows.Count > 0)
            {
                ViewState["SinPermiso"] = dt.Rows[0]["SinPermiso"].ToString();
                ViewState["Consulta"] = dt.Rows[0]["Consulta"].ToString();
                ViewState["Exportar"] = dt.Rows[0]["Exportar"].ToString();
                ViewState["CrearModificar"] = dt.Rows[0]["CrearModificar"].ToString();
                ViewState["Borrar"] = dt.Rows[0]["Borrar"].ToString();
            }

            dt.Dispose();
        }

        private void CargarCargos()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCargos();
            ddlCargo.DataSource = dt;
            ddlCargo.DataBind();
            dt.Dispose();
        }

        private void CargarCanalesVenta()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarCanalesVenta();

                ddlCanalVenta.DataSource = dt;
                ddlCanalVenta.DataTextField = "NombreCanalVenta";
                ddlCanalVenta.DataValueField = "idCanalVenta";
                ddlCanalVenta.DataBind();

                ListItem item = ddlCanalVenta.Items.FindByValue("15");
                if (item != null)
                {
                    ddlCanalVenta.Items.Remove(item);
                }

                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void CargarPerfiles()
        {
            clasesglobales cg = new clasesglobales();
            try
            {

                int idPerfil = Convert.ToInt32(Session["idPerfil"]);

                DataTable dt = cg.ConsultarPerfiles();

                int[] perfilesRestringidos = { 2, 4, 6, 10, 11, 24, 25, 36 };

                if (idPerfil == 2 || idPerfil == 11 || idPerfil == 36)
                {
                    var perfilesPermitidos = dt.AsEnumerable()
                                               .Where(r => perfilesRestringidos.Contains(Convert.ToInt32(r["IdPerfil"])));

                    if (perfilesPermitidos.Any())
                        dt = perfilesPermitidos.CopyToDataTable();
                    else
                        dt = dt.Clone();
                }

                ddlPerfiles.DataSource = dt;
                ddlPerfiles.DataBind();

                dt.Dispose();

            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void CargarEmpleados()
        {
            string strQuery = "SELECT * FROM Empleados WHERE Estado = 'Activo' ORDER BY NombreEmpleado";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlEmpleados.DataSource = dt;
            ddlEmpleados.DataBind();

            dt.Dispose();
        }


        private void CargarDatosUsuario()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                //string strQuery = "SELECT * FROM Usuarios WHERE idUsuario = " + Request.QueryString["editid"].ToString();               
                DataTable dt = cg.ConsultarUsuarioEmpleadoPorId(Convert.ToInt32(Request.QueryString["editid"].ToString()));

                txbNombre.Text = dt.Rows[0]["NombreUsuario"].ToString();
                if (dt.Rows[0]["idCargoUsuario"].ToString() != "")
                {
                    ddlCargo.SelectedIndex = Convert.ToInt32(ddlCargo.Items.IndexOf(ddlCargo.Items.FindByValue(dt.Rows[0]["idCargoUsuario"].ToString())));
                }
                txbEmail.Text = dt.Rows[0]["EmailUsuario"].ToString();
                //txbClave.Text = dt.Rows[0]["ClaveUsuario"].ToString();
                if (dt.Rows[0]["idPerfil"].ToString() != "")
                {
                    ddlPerfiles.SelectedIndex = Convert.ToInt32(ddlPerfiles.Items.IndexOf(ddlPerfiles.Items.FindByValue(dt.Rows[0]["idPerfil"].ToString())));
                }
                if (dt.Rows[0]["idEmpleado"].ToString() != "")
                {
                    ddlEmpleados.SelectedIndex = Convert.ToInt32(ddlEmpleados.Items.IndexOf(ddlEmpleados.Items.FindByValue(dt.Rows[0]["idEmpleado"].ToString())));
                }
                if (dt.Rows[0]["idCanalVenta"].ToString() != "")
                {
                    ddlCanalVenta.SelectedIndex = Convert.ToInt32(ddlCanalVenta.Items.IndexOf(ddlCanalVenta.Items.FindByValue(dt.Rows[0]["idCanalVenta"].ToString())));
                }

                rblEstado.Items.FindByValue(dt.Rows[0]["EstadoUsuario"].ToString()).Selected = true;

                dt.Dispose();
            }

            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }


        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            string strInitData = TraerData();

            try
            {
                string strHashClave = cg.ComputeSha256Hash(txbClave.Text.Trim());


                int idUsuario = 0;
                int.TryParse(Request.QueryString["editid"], out idUsuario);

                string email = txbEmail?.Text?.Trim() ?? "";

                string nombre = txbNombre?.Text?.Trim() ?? "";

                //string clave = null;
                //if (!string.IsNullOrWhiteSpace(txbClave?.Text))
                //{
                //    clave = cg.ComputeSha256Hash(txbClave.Text.Trim());
                //}

                string clavePlano = "Fitness2025";
                string clave = cg.ComputeSha256Hash(clavePlano);

                txbClave.Attributes["value"] = clavePlano;

                int idCargo = 0;
                int.TryParse(ddlCargo?.SelectedValue, out idCargo);

                int idPerfil = 0;
                int.TryParse(ddlPerfiles?.SelectedValue, out idPerfil);

                int idCanalVenta = 0;
                int.TryParse(ddlCanalVenta?.SelectedValue, out idCanalVenta);

                string idEmpleado = ddlEmpleados?.SelectedValue ?? "";

                string estado = rblEstado?.SelectedValue ?? "";

                string rta = cg.ActualizarUsuario(idUsuario, email, clave, nombre, idCargo, idPerfil, idEmpleado, estado, idCanalVenta);

                if (rta == "OK")
                {
                    string strNewData = TraerData();

                    cg.InsertarLog(
                        Session["idusuario"].ToString(),
                        "usuarios",
                        "Modifica",
                        "El usuario modificó información del correo: " + email + ".",
                        strInitData,
                        strNewData
                    );

                    string script = @"
                        Swal.fire({
                            title: 'Usuario actualizado correctamente',
                            html: 'La clave del usuario ha sido cambiada por defecto.<br><br><b>Recuerde informar al usuario.</b>',
                            icon: 'success',
                            timer: 3000,
                            showConfirmButton: false,
                            timerProgressBar: true
                        }).then(() => {
                            window.location.href = 'usuarios.aspx';
                        });
                    ";

                    ScriptManager.RegisterStartupScript(this, GetType(), "SuccessUsuario", script, true);
                }
                else
                {
                    string script = @"
                        Swal.fire({
                            title: 'Error',
                            text: '" + rta.Replace("'", "\\'") + @"',
                            icon: 'error'
                        });
                    ";

                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorUsuario", script, true);
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private string TraerData()
        {
            string strQuery = "SELECT * FROM usuarios WHERE idUsuario = " + Request.QueryString["editid"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();

            return strData;
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            clasesglobales cg = new clasesglobales();

            // tipo puede ser: 'success', 'error', 'warning', 'info', 'question'
            string script = $@"
                Swal.hideLoading();
                Swal.fire({{
                title: '{titulo}',
                text: '{mensaje}',
                icon: '{tipo}', 
                allowOutsideClick: false, 
                showCloseButton: false, 
                confirmButtonText: 'Aceptar'
            }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
        }
    }
}
