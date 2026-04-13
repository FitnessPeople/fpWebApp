using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class liquidarcartera : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("es-CO");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (!IsPostBack)
            {
                //ObtenerReporteSeleccionado();
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Liquidar cartera");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        //No tiene acceso a esta página
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    else
                    {
                        //Si tiene acceso a esta página
                        divBotonesLista.Visible = false;
                        //btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //CargarPlanes();
                            //lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            //txbFechaIni.Attributes.Add("type", "date");
                            //txbFechaIni.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                            //txbFechaFin.Attributes.Add("type", "date");
                            //txbFechaFin.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                            CargarEmpresas();
                            CargarIndicadores();
                        }
                    }

                    //ObtenerReporteSeleccionado();
                }
                else
                {
                    Response.Redirect("logout.aspx");
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


        private void CargarIndicadores()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarIndicadoresGenerarLiquidacion();

                lblEmpresas.Text = dt.Rows[0]["EmpresasConCarteraPendiente"].ToString();
                lblCartera.Text = string.Format("{0:C}", dt.Rows[0]["TotalCarteraPendiente"]);
                lblPendientes.Text = dt.Rows[0]["LiquidacionesPendientesFacturar"].ToString();
                lblMes.Text = dt.Rows[0]["LiquidacionesGeneradasMes"].ToString();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void CargarEmpresas()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarEmpresasAfiliadas();

                ddlEmpresa.DataSource = dt;
                ddlEmpresa.DataValueField = "DocumentoEmpresa";
                ddlEmpresa.DataTextField = "NombreComercial";
                ddlEmpresa.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog,"error");
            } 
        }

        protected void btnGestionar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvCartera.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkItem");

                if (chk != null && chk.Checked)
                {
                    string idAfiliadoPlan = row.Cells[1].Text; 
                    string documento = row.Cells[2].Text;
                    
                }
            }
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {

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



        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {

            try
            {
                clasesglobales cg = new clasesglobales();
                if (ViewState["ReporteActual"] == null)
                {
                    MostrarAlerta("Info", "Primero genere el reporte", "info");
                    return;
                }

                //int tipoReporte = Convert.ToInt32(ddlTipoReporte.SelectedValue);
                string tituloReporte = string.Empty;
                string nombreArchivo = string.Empty;
                string usuario = Session["NombreUsuario"] as string ?? "Usuario";

                DataTable dt = null;


                if (dt == null || dt.Rows.Count == 0)
                {
                    MostrarAlerta("Info", "No hay datos para exportar.", "info");
                    return;
                }

                cg.ExportarExcelGen(dt, nombreArchivo, tituloReporte, usuario);
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", ex.Message, "error");
            }
        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvCartera.DataSource = null;
            gvCartera.DataBind();

            gvCartera.Visible = false;
            btnGenerarLiquidacion.Visible = false;
            lblSinDatos.Visible = false;
        }

        protected void lbExportarPdf_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DateTime fechaIni;
                DateTime fechaFin;
                string tituloReporte = string.Empty;
                string nombreArchivo = string.Empty;
                string usuario = Session["NombreUsuario"] as string ?? "Usuario";

                DataTable dt = null;

                if (dt == null || dt.Rows.Count == 0)
                {
                    MostrarAlerta("Info", "No hay datos para exportar.", "info");
                    return;
                }

                cg.ExportarPDFGen(dt, nombreArchivo, tituloReporte);
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", "Error al generar el PDF: " + ex.Message, "error");
            }
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                string documentoEmpresa = ddlEmpresa.SelectedValue;

                if (string.IsNullOrEmpty(documentoEmpresa))
                    return;

                DataTable dt = cg.CargarCarteraPorNit(documentoEmpresa);

                gvCartera.DataSource = dt;
                gvCartera.DataBind();

                bool hayDatos = dt != null && dt.Rows.Count > 0;

                btnGenerarLiquidacion.Visible = hayDatos;
                gvCartera.Visible = hayDatos;

                lblSinDatos.Visible = !hayDatos;
                if (!hayDatos)
                {
                    lblSinDatos.Text = "<div class=\"alert alert-info text-center\">\r\n    <i class=\"fa fa-info-circle\"></i> No existen registros asociados a la empresa seleccionada.\r\n</div>\r\n";
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso",
                    "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog,
                    "error");
            }
        }

        protected void btnGenerarLiquidacion_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                string documentoEmpresa = ddlEmpresa.SelectedValue;
                int idUsuario = Convert.ToInt32(Session["idUsuario"]);

                int idLiquidacion = cg.GenerarLiquidacionCartera(documentoEmpresa, idUsuario);

                if (idLiquidacion > 0)
                {
                    string script = $@"
                                Swal.fire({{
                                    title: '¡Liquidación generada correctamente!',
                                    text: 'Liquidación N° {idLiquidacion} creada correctamente.',
                                    icon: 'success',
                                    timer: 3000,
                                    showConfirmButton: false,
                                    timerProgressBar: true
                                }}).then(() => {{
                                    window.location.href = 'liquidacionesgeneradas';
                                }});
                            ";

                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "ExitoMensaje",
                        script,
                        true
                    );
                }

            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");

            }

        }
    }

}
