using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class editarempleado : System.Web.UI.Page
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
                        txbFechaNac.Attributes.Add("type", "date");
                        txbFechaInicio.Attributes.Add("type", "date");
                        txbFechaFinal.Attributes.Add("type", "date");
                        txbEmail.Attributes.Add("type", "email");
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
                        CargarCanalesVenta();
                        CargarCargos();
                        CargarEstadoCivil();
                        CargarGeneros();
                        CargarEmpleado();
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
            ddlEmpresasFP.DataSource = dt;
            ddlEmpresasFP.DataBind();
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


        private void CargarEmpleado()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.CargarEmpleados(Request.QueryString["editid"].ToString());
            txbDocumento.Text = dt.Rows[0]["DocumentoEmpleado"].ToString();

            if (dt.Rows[0]["idTipoDocumento"].ToString() != "")
            {
                ddlTipoDocumento.SelectedIndex = Convert.ToInt16(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt.Rows[0]["idTipoDocumento"].ToString())));
            }
            else
            {
                ddlTipoDocumento.SelectedItem.Value = "0";
            }

            txbNombre.Text = dt.Rows[0]["NombreEmpleado"].ToString();
            ltNombre.Text = dt.Rows[0]["NombreEmpleado"].ToString();
            txbTelefono.Text = dt.Rows[0]["TelefonoEmpleado"].ToString();
            txbTelefonoCorp.Text = dt.Rows[0]["TelefonoCorporativo"].ToString();
            ltTelefono.Text = dt.Rows[0]["TelefonoEmpleado"].ToString();
            txbEmail.Text = dt.Rows[0]["EmailEmpleado"].ToString();
            txbEmailCorp.Text = dt.Rows[0]["EmailCorporativo"].ToString();
            txbDireccion.Text = dt.Rows[0]["DireccionEmpleado"].ToString();
            ddlCiudadEmpleado.SelectedIndex = Convert.ToInt32(ddlCiudadEmpleado.Items.IndexOf(ddlCiudadEmpleado.Items.FindByValue(dt.Rows[0]["idCiudadEmpleado"].ToString())));
            DateTime dt14 = DateTime.Now.AddYears(-14);
            DateTime dt80 = DateTime.Now.AddYears(-80);
            txbFechaNac.Attributes.Add("min", dt80.Year.ToString() + "-" + String.Format("{0:MM}", dt80) + "-" + String.Format("{0:dd}", dt80));
            txbFechaNac.Attributes.Add("max", dt14.Year.ToString() + "-" + String.Format("{0:MM}", dt14) + "-" + String.Format("{0:dd}", dt14));

            DateTime dtFecha = new DateTime();
            if (dt.Rows[0]["FechaNacEmpleado"].ToString() != "")
            {
                dtFecha = Convert.ToDateTime(dt.Rows[0]["FechaNacEmpleado"].ToString());
            }
            txbFechaNac.Text = dtFecha.ToString("yyyy-MM-dd");
            ltCiudad.Text = dt.Rows[0]["NombreCiudad"].ToString();
            if (dt.Rows[0]["FotoEmpleado"].ToString() != "")
            {
                imgFoto.Src = "img/empleados/" + dt.Rows[0]["FotoEmpleado"].ToString();
                ViewState["FotoEmpleado"] = dt.Rows[0]["FotoEmpleado"].ToString();
            }
            else
            {
                imgFoto.Src = "img/empleados/nofoto.png";
            }
            txbContrato.Text = dt.Rows[0]["NroContrato"].ToString();
            if (dt.Rows[0]["TipoContrato"].ToString() != "")
            {
                ddlTipoContrato.SelectedIndex = Convert.ToInt16(ddlTipoContrato.Items.IndexOf(ddlTipoContrato.Items.FindByText(dt.Rows[0]["TipoContrato"].ToString())));
            }
            DateTime dtFechaIni = Convert.ToDateTime(dt.Rows[0]["FechaInicio"].ToString());
            txbFechaInicio.Text = dtFechaIni.ToString("yyyy-MM-dd");
            DateTime dtFechaFin = Convert.ToDateTime(dt.Rows[0]["FechaFinal"].ToString());
            txbFechaFinal.Text = dtFechaFin.ToString("yyyy-MM-dd");
            ddlSedes.SelectedIndex = Convert.ToInt32(ddlSedes.Items.IndexOf(ddlSedes.Items.FindByValue(dt.Rows[0]["idSede"].ToString())));
            int sueldo = Convert.ToInt32(dt.Rows[0]["Sueldo"]);
            txbSueldo.Text = sueldo.ToString("C0", new CultureInfo("es-CO"));

            if (dt.Rows[0]["GrupoNomina"].ToString() != "")
            {
                ddlGrupo.SelectedIndex = Convert.ToInt16(ddlGrupo.Items.IndexOf(ddlGrupo.Items.FindByValue(dt.Rows[0]["GrupoNomina"].ToString())));
            }
            if (dt.Rows[0]["idEps"].ToString() != "")
            {
                ddlEps.SelectedIndex = Convert.ToInt16(ddlEps.Items.IndexOf(ddlEps.Items.FindByValue(dt.Rows[0]["idEps"].ToString())));
            }

            if (dt.Rows[0]["idFondoPension"].ToString() != "")
            {
                ddlFondoPension.SelectedIndex = Convert.ToInt16(ddlFondoPension.Items.IndexOf(ddlFondoPension.Items.FindByValue(dt.Rows[0]["idFondoPension"].ToString())));
            }

            if (dt.Rows[0]["idArl"].ToString() != "")
            {
                ddlArl.SelectedIndex = Convert.ToInt16(ddlArl.Items.IndexOf(ddlArl.Items.FindByValue(dt.Rows[0]["idArl"].ToString())));
            }

            if (dt.Rows[0]["idCajaComp"].ToString() != "")
            {
                ddlCajaComp.SelectedIndex = Convert.ToInt16(ddlCajaComp.Items.IndexOf(ddlCajaComp.Items.FindByValue(dt.Rows[0]["idCajaComp"].ToString())));
            }

            if (dt.Rows[0]["idCesantias"].ToString() != "")
            {
                ddlCesantias.SelectedIndex = Convert.ToInt16(ddlCesantias.Items.IndexOf(ddlCesantias.Items.FindByValue(dt.Rows[0]["idCesantias"].ToString())));
            }

            rblEstado.Items.FindByValue(dt.Rows[0]["Estado"].ToString()).Selected = true;

            if (dt.Rows[0]["idCanalVenta"].ToString() != "")
            {
                ddlCanalVenta.SelectedIndex = Convert.ToInt16(ddlCanalVenta.Items.IndexOf(ddlCanalVenta.Items.FindByValue(dt.Rows[0]["idCanalVenta"].ToString())));
            }

            rblEstado.Items.FindByValue(dt.Rows[0]["Estado"].ToString()).Selected = true;

            if (dt.Rows[0]["idEmpresaFP"].ToString() != "")
            {
                ddlEmpresasFP.SelectedIndex = Convert.ToInt16(ddlEmpresasFP.Items.IndexOf(ddlEmpresasFP.Items.FindByValue(dt.Rows[0]["idEmpresaFP"].ToString())));
            }

            if (dt.Rows[0]["idEstadoCivil"].ToString() != "")
            {
                ddlEstadoCivil.SelectedIndex = Convert.ToInt16(ddlEstadoCivil.Items.IndexOf(ddlEstadoCivil.Items.FindByValue(dt.Rows[0]["idEstadoCivil"].ToString())));
            }

            if (dt.Rows[0]["idGenero"].ToString() != "")
            {
                ddlGenero.SelectedIndex = Convert.ToInt16(ddlGenero.Items.IndexOf(ddlGenero.Items.FindByValue(dt.Rows[0]["idGenero"].ToString())));
            }

            if (dt.Rows[0]["idCargo"].ToString() != "")
            {
                ddlCargo.SelectedIndex = Convert.ToInt32(ddlCargo.Items.IndexOf(ddlCargo.Items.FindByValue(dt.Rows[0]["idCargo"].ToString())));
            }

            dt.Dispose();
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

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            string strFilename = "";
            // Actualiza la tabla Empleados
            if (ViewState["FotoEmpleado"] != null)
            {
                strFilename = ViewState["FotoEmpleado"].ToString();
            }
            else
            {
                strFilename = "nofoto.png";
            }

            HttpPostedFile postedFile = Request.Files["fileFoto"];

            if (postedFile != null && postedFile.ContentLength > 0)
            {
                //Save the File.
                string filePath = Server.MapPath("img//afiliados//") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                strFilename = postedFile.FileName;
            }

            string strInitData = TraerData();
            try
            {
                clasesglobales cg = new clasesglobales();

                string mensaje = cg.ActualizarEmpleado(txbDocumento.Text.ToString(), Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value.ToString()),
                    txbNombre.Text.ToString(), txbTelefono.Text.ToString(), txbTelefonoCorp.Text.ToString(), 
                    txbEmail.Text.ToString(), txbEmailCorp.Text.ToString(), txbDireccion.Text.ToString(),
                    Convert.ToInt32(ddlCiudadEmpleado.SelectedItem.Value.ToString()), txbFechaNac.Text.ToString(), strFilename,
                    txbContrato.Text.ToString(), ddlTipoContrato.SelectedItem.Value.ToString(), Convert.ToInt32(ddlEmpresasFP.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlSedes.SelectedItem.Value.ToString()), txbFechaInicio.Text.ToString(), txbFechaFinal.Text.ToString(),
                    Convert.ToInt32(Regex.Replace(txbSueldo.Text, @"[^\d]", "")), ddlGrupo.SelectedItem.Value.ToString(), Convert.ToInt32(ddlEps.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlFondoPension.SelectedItem.Value.ToString()), Convert.ToInt32(ddlArl.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlCajaComp.SelectedItem.Value.ToString()), Convert.ToInt32(ddlCesantias.SelectedItem.Value.ToString()),
                    rblEstado.Text.ToString(), Convert.ToInt32(ddlGenero.SelectedItem.Value.ToString()), Convert.ToInt32(ddlEstadoCivil.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlCanalVenta.SelectedItem.Value.ToString()), Convert.ToInt32(ddlCargo.SelectedItem.Value.ToString()));

                if (rblEstado.Text.ToString() == "Inactivo")
                {
                    string restpuestaEstado = cg.ActualizarEstadoUsuario(txbDocumento.Text.ToString());
                }

                string strNewData = TraerData();

                //cg.InsertarLog(Session["idusuario"].ToString(), "Empleados", "Modifica", "El usuario modificó datos al empleado con documento " + txbDocumento.Text.ToString() + ".", strInitData, strNewData);

                if (mensaje == "OK")
                {
                    cg.InsertarLog(Session["idusuario"].ToString(), "Empleados", "Modifica", "El usuario modificó datos al empleado con documento " + txbDocumento.Text.ToString() + ".", strInitData, strNewData);

                    string script = @"
                        Swal.fire({
                            title: 'El empleado se actualizó de forma exitosa',
                            text: 'Texto.',
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

                //Response.Redirect("empleados");


            }
            catch (OdbcException ex)
            {
                string script = @"
                    Swal.fire({
                        title: 'Error',
                        text: 'Ha ocurrido un error inesperado. " + ex.Message.ToString() + @"',
                        icon: 'error'
                    }).then(() => {
                        window.location.href = 'editarempleado?" + Request.QueryString["editid"].ToString() + @"';
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
            }
        }

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEmpleado(Request.QueryString["editid"].ToString());

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();

            return strData;
        }
    }
}