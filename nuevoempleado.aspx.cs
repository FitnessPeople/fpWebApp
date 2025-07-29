using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace fpWebApp
{
    public partial class nuevoempleado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Empleados");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        txbDocumento.Attributes.Add("type", "number");
                        txbTelefono.Attributes.Add("type", "number");
                        txbTelefonoCorp.Attributes.Add("type", "number");
                        txbFechaNac.Attributes.Add("type", "date");
                        txbFechaInicio.Attributes.Add("type", "date");
                        txbFechaFinal.Attributes.Add("type", "date");
                        txbEmail.Attributes.Add("type", "email");
                        txbEmailCorp.Attributes.Add("type", "email");

                        DateTime dt14 = DateTime.Now.AddYears(-14);
                        DateTime dt80 = DateTime.Now.AddYears(-80);
                        txbFechaNac.Attributes.Add("min", dt80.Year.ToString() + "-" + String.Format("{0:MM}", dt80) + "-" + String.Format("{0:dd}", dt80));
                        txbFechaNac.Attributes.Add("max", dt14.Year.ToString() + "-" + String.Format("{0:MM}", dt14) + "-" + String.Format("{0:dd}", dt14));

                        CargarTipoDocumento();
                        CargarCiudad();
                        CargarSedes();
                        CargarEps();
                        CargarFondoPension();
                        CargarArl();
                        CargarCajaComp();
                        CargarCesantias();
                        CargarEmpresasFP();
                        CargarCanalesVenta();
                        CargarCargos();
                        CargarEstadoCivil();
                        CargarGeneros();
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

        private void CargarTipoDocumento()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultartiposDocumento();
            ddlTipoDocumento.DataSource = dt;
            ddlTipoDocumento.DataBind();
            dt.Dispose();
        }

        private void CargarCiudad()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCiudadesCol();
            ddlCiudadEmpleado.DataSource = dt;
            ddlCiudadEmpleado.DataBind();
            dt.Dispose();
        }

        private void CargarSedes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSedes("Todos");
            ddlSedes.DataSource = dt;
            ddlSedes.DataBind();
            dt.Dispose();
        }

        private void CargarEps()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarEpss();
            ddlEps.DataSource = dt;
            ddlEps.DataBind();
            dt.Dispose();
        }

        private void CargarFondoPension()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarPensiones();
            ddlFondoPension.DataSource = dt;
            ddlFondoPension.DataBind();
            dt.Dispose();
        }

        private void CargarArl()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarArls();
            ddlArl.DataSource = dt;
            ddlArl.DataBind();
            dt.Dispose();
        }

        private void CargarCajaComp()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCajasComp();
            ddlCajaComp.DataSource = dt;
            ddlCajaComp.DataBind();
            dt.Dispose();
        }

        private void CargarCesantias()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarCesantias();
            ddlCesantias.DataSource = dt;
            ddlCesantias.DataBind();
            dt.Dispose();
        }
        private void CargarCanalesVenta()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarCanalesVenta();
            ddlCanalVenta.DataSource = dt;
            ddlCanalVenta.DataBind();
            dt.Dispose();
        }

        private void CargarCargos()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarCargos();
            ddlCargo.DataSource = dt;
            ddlCargo.DataBind();
            dt.Dispose();
        }

        private void CargarEmpresasFP()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarEmpresasFP();
            ddlempresasFP.DataSource = dt;
            ddlempresasFP.DataBind();
            dt.Dispose();
        }

        private void CargarEstadoCivil()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarEstadosCiviles();
            ddlEstadoCivil.DataSource = dt;
            ddlEstadoCivil.DataBind();
            dt.Dispose();
        }

        private void CargarGeneros()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarGeneros();
            ddlGenero.DataSource = dt;
            ddlGenero.DataBind();
            dt.Dispose();
        }

        private bool ExisteDocumento(string strDocumento)
        {
            bool rta = false;
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarExisteDocEmpleado(strDocumento);

            if (dt.Rows.Count > 0)
            {
                rta = true;
            }

            dt.Dispose();
            return rta;
        }

        private bool ExisteEmail(string strEmail)
        {
            bool rta = false;
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarExisteEmailEmpleado(strEmail);

            if (dt.Rows.Count > 0)
            {
                rta = true;
            }

            dt.Dispose();
            return rta;
        }

        private bool ExisteTelefono(string strTelefono)
        {
            bool rta = false;
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarExisteTelEmpleado(strTelefono);

            if (dt.Rows.Count > 0)
            {
                rta = true;
            }

            dt.Dispose();
            return rta;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validar si existe por Cedula, Email y/o Telefono
            if (ExisteDocumento(txbDocumento.Text.ToString().Trim()))
            {
                //divMensaje1.Visible = true;
                string script = @"
                    Swal.fire({
                        title: 'Error',
                        text: 'Un empleado con este documento ya existe!',
                        icon: 'error'
                    }).then((result) => {
                        if (result.isConfirmed) {
                                            
                        }
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
            }
            else
            {
                //if (ExisteEmail(txbEmail.Text.ToString().Trim()))
                //{
                //    divMensaje2.Visible = true;
                //}
                //else
                //{
                    //if (ExisteTelefono(txbTelefono.Text.ToString().Trim()))
                    //{
                    //    divMensaje3.Visible = true;
                    //}
                    //else
                    //{
                        string strFilename = "";
                        HttpPostedFile postedFile = Request.Files["fileFoto"];

                        if (postedFile != null && postedFile.ContentLength > 0)
                        {
                            //Save the File.
                            string filePath = Server.MapPath("img//empleados//") + Path.GetFileName(postedFile.FileName);
                            postedFile.SaveAs(filePath);
                            strFilename = postedFile.FileName;
                        }

                        try
                        {
                            clasesglobales cg = new clasesglobales();
                            string mensaje = cg.InsertarNuevoEmpleado(txbDocumento.Text.ToString(), Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value.ToString()),
                                txbNombre.Text.ToString(), txbTelefono.Text.ToString(), txbTelefonoCorp.Text.ToString(), txbEmail.Text.ToString(), 
                                txbEmailCorp.Text.ToString(), txbDireccion.Text.ToString(),
                                Convert.ToInt32(ddlCiudadEmpleado.SelectedItem.Value.ToString()), txbFechaNac.Text.ToString(), strFilename, txbContrato.Text.ToString(),
                                ddlTipoContrato.SelectedItem.Value.ToString(), Convert.ToInt32(ddlempresasFP.SelectedItem.Value.ToString()),
                                Convert.ToInt32(ddlSedes.SelectedItem.Value.ToString()), txbFechaInicio.Text.ToString(), txbFechaFinal.Text.ToString(),
                                Convert.ToInt32(Regex.Replace(txbSueldo.Text, @"[^\d]", "")), ddlGrupo.SelectedItem.Value.ToString(), Convert.ToInt32(ddlEps.SelectedItem.Value.ToString()),
                                Convert.ToInt32(ddlFondoPension.SelectedItem.Value.ToString()), Convert.ToInt32(ddlArl.SelectedItem.Value.ToString()),
                                Convert.ToInt32(ddlCajaComp.SelectedItem.Value.ToString()), Convert.ToInt32(ddlCesantias.SelectedItem.Value.ToString()), "Activo",
                                Convert.ToInt32(ddlGenero.SelectedItem.Value.ToString()), Convert.ToInt32(ddlEstadoCivil.SelectedItem.Value.ToString()),
                                Convert.ToInt32(ddlCanalVenta.SelectedItem.Value.ToString()), Convert.ToInt32(ddlCargo.SelectedItem.Value.ToString()));

                            if (mensaje == "OK")
                            {
                                cg.InsertarLog(Session["idusuario"].ToString(), "empleados", "Agrega", "El usuario agregó un nuevo empleado con documento: " + txbDocumento.Text.ToString() + ".", "", "");

                                string script = @"
                                    Swal.fire({
                                        title: 'El empleado se creo de forma exitosa',
                                        text: '',
                                        icon: 'success',
                                        timer: 3000, // 3 segundos
                                        showConfirmButton: false,
                                        timerProgressBar: true
                                    }).then(() => {
                                        window.location.href = 'empleados';
                                    });
                                    ";
                                ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                            }
                            else
                            {
                                string script = @"
                                    Swal.fire({
                                        title: 'Error',
                                        text: '" + mensaje.Replace("'", "\\'") + @"',
                                        icon: 'error'
                                    }).then((result) => {
                                        if (result.isConfirmed) {
                                            
                                        }
                                    });
                                ";
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                            }

                                                        
                        }
                        catch (SqlException ex)
                        {
                            string script = @"
                                Swal.fire({
                                    title: 'Error',
                                    text: 'Ha ocurrido un error inesperado. " + ex.Message.ToString() + @"',
                                    icon: 'error'
                                }).then(() => {
                                    window.location.href = 'nuevoempleado';
                                });
                            ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                        }
                    //}
                //}
            }
        }
    }
}