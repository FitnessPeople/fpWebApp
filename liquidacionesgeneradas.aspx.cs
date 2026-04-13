using fpWebApp.Services;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class liquidacionesgeneradas : System.Web.UI.Page
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
                            //CargarEmpresas();
                            CargarLiquidacionesVigentes();
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

        protected void CargarLiquidacionesVigentes()
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                DataTable dt = cg.ConsultarLiquidacionesCarteraPendientes();
                gvLiquidaciones.DataSource = dt;
                gvLiquidaciones.DataBind();

                bool hayDatos = dt != null && dt.Rows.Count > 0;

                gvLiquidaciones.Visible = hayDatos;
                lblSinDatos.Visible = !hayDatos;
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void CargarIndicadores()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarIndicadoresLiquidacionesGeneradas();

                lblEmpresas.Text = dt.Rows[0]["EmpresasConCarteraPendiente"].ToString();
                lblCartera.Text = string.Format("{0:C}", dt.Rows[0]["TotalCarteraPendiente"]);
                lblPendientes.Text = dt.Rows[0]["LiquidacionesPendientesFacturar"].ToString();
                lblMes.Text = dt.Rows[0]["LiquidacionesMesActual"].ToString();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
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

        protected void gvLiquidaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            // PANEL OCULTO
            Panel pnl = new Panel();
            pnl.ID = "pnlDetalle";
            pnl.Visible = false;
            pnl.CssClass = "p-2 bg-light";

            GridView gvDetalle = new GridView();
            gvDetalle.ID = "gvDetalle";
            gvDetalle.AutoGenerateColumns = false;
            gvDetalle.CssClass = "table table-sm table-bordered";

            gvDetalle.Columns.Add(new BoundField { DataField = "NombreAfiliado", HeaderText = "Afiliado" });
            gvDetalle.Columns.Add(new BoundField { DataField = "DocumentoAfiliado", HeaderText = "Documento" });
            gvDetalle.Columns.Add(new BoundField { DataField = "NombrePlan", HeaderText = "Plan" });
            gvDetalle.Columns.Add(new BoundField
            {
                DataField = "ValorFacturar",
                HeaderText = "Valor",
                DataFormatString = "{0:C0}",
                ItemStyle = { HorizontalAlign = HorizontalAlign.Right }
            });

            TemplateField tf = new TemplateField { HeaderText = "DCR" };
            tf.ItemTemplate = new CompiledTemplateBuilder(container =>
            {
                LinkButton btn = new LinkButton();
                btn.Text = "📄 DCR";
                btn.CssClass = "btn btn-danger btn-xs";
                btn.DataBinding += (s, ev) =>
                {
                    GridViewRow row = (GridViewRow)btn.NamingContainer;
                    btn.CommandArgument =
                        DataBinder.Eval(row.DataItem, "idLiquidacionDetalle").ToString();
                };
                btn.Click += btnVerDCRDetalle_Click;

                container.Controls.Add(btn);
            });

            gvDetalle.Columns.Add(tf);

            pnl.Controls.Add(gvDetalle);

            TableCell cell = new TableCell();
            cell.ColumnSpan = gvLiquidaciones.Columns.Count;
            cell.Controls.Add(pnl);

            e.Row.Cells[e.Row.Cells.Count - 1].Controls.Add(cell.Controls[0]);
        }

        //protected void gvLiquidaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName != "ToggleDetalle")
        //        return;

        //    int idLiquidacion = Convert.ToInt32(e.CommandArgument);

        //    clasesglobales cg = new clasesglobales();
        //    gvDetalle.DataSource = cg.ConsultarDetalleLiquidacionesPorIdLiq(idLiquidacion);
        //    gvDetalle.DataBind();

        //    pnlDetalle.Visible = !pnlDetalle.Visible;
        //}

        protected void gvLiquidaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GenerarFactura")
            {
                int idLiquidacion = Convert.ToInt32(e.CommandArgument);

                GenerarFacturaCorporativa(idLiquidacion);
            }
        }

        //private void GenerarFacturaCorporativa(int idLiquidacion)
        //{
        //    clasesglobales cg = new clasesglobales();

        //    DataTable dt = cg.ObtenerDatosLiquidacionFactura(idLiquidacion);

        //    if (dt.Rows.Count > 0)
        //    {
        //        DataRow row = dt.Rows[0];

        //        int idCartera = Convert.ToInt32(row["idCartera"]);
        //        int idAfiliadoPlan = Convert.ToInt32(row["idAfiliadoPlan"]);
        //        string documentoEmpresa = row["DocumentoEmpresa"].ToString();
        //        string documentoAfiliado = row["DocumentoAfiliado"].ToString();
        //        decimal valorPlan = Convert.ToDecimal(row["ValorPlan"]);
        //        decimal descuento = Convert.ToDecimal(row["Descuento"]);
        //        decimal valorFacturado = Convert.ToDecimal(row["ValorFacturar"]);
        //        int diasFacturados = Convert.ToInt32(row["DiasFacturados"]);

        //        DateTime periodoInicio = Convert.ToDateTime(row["PeriodoInicio"]);
        //        DateTime periodoFin = Convert.ToDateTime(row["PeriodoFin"]);

        //        string numeroFactura = ""; // luego lo devuelve Siigo

        //        int idUsuario = Convert.ToInt32(Session["idUsuario"]);

        //        //string idFactura = InsertarFacturaCorporativo(
        //        //    idCartera,
        //        //    idLiquidacion,
        //        //    idAfiliadoPlan,
        //        //    documentoEmpresa,
        //        //    documentoAfiliado,
        //        //    numeroFactura,
        //        //    DateTime.Now,
        //        //    periodoInicio,
        //        //    periodoFin,
        //        //    diasFacturados,
        //        //    valorPlan,
        //        //    descuento,
        //        //    valorFacturado,
        //        //    idUsuario
        //        //);

        //        // Aquí puedes llamar la generación en SIIGO
        //       // GenerarFacturaSiigo(idFactura);
        //    }
        //}
        private void GenerarFacturaCorporativa(int idLiquidacion)
        {
            clasesglobales cg = new clasesglobales();

            DataSet ds = cg.ObtenerDatosLiquidacionFactura(idLiquidacion);

            if (ds.Tables.Count >= 2)
            {
                DataTable cabecera = ds.Tables[0];
                DataTable detalle = ds.Tables[1];
                int diasFactura = 0;

                if (cabecera.Rows.Count > 0 && detalle.Rows.Count > 0)
                {
                    DataRow rowCab = cabecera.Rows[0];

                    string documentoEmpresa = rowCab["DocumentoEmpresa"].ToString();
                    decimal totalFactura = Convert.ToDecimal(rowCab["TotalLiquidado"]);

                    DataTable dt1 = cg.ConsultarEmpresaAfiliadaPorDocumento(documentoEmpresa);
                    if (dt1.Rows.Count > 0)
                    {
                        diasFactura = Convert.ToInt32(dt1.Rows[0]["DiasCredito"].ToString());
                    }

                    int idUsuario = Convert.ToInt32(Session["idUsuario"]);

                    DateTime periodoInicio = DateTime.Now; // puedes calcularlo
                    DateTime periodoFin = DateTime.Now;

                    foreach (DataRow rowDet in detalle.Rows)
                    {
                        int idCartera = Convert.ToInt32(rowDet["idCartera"]);
                        int idAfiliadoPlan = Convert.ToInt32(rowDet["idAfiliadoPlan"]);
                        string documentoAfiliado = rowDet["DocumentoAfiliado"].ToString();

                        decimal valorPlan = Convert.ToDecimal(rowDet["ValorPlan"]);
                        decimal descuento = Convert.ToDecimal(rowDet["Descuento"]);
                        decimal valorFacturado = Convert.ToDecimal(rowDet["ValorFacturar"]);
                        int diasFacturados = Convert.ToInt32(rowDet["DiasFacturados"]);

                        string numeroFactura = ""; // luego lo devuelve Siigo

                        //string idFactura = InsertarFacturaCorporativo(
                        //    idCartera,
                        //    idLiquidacion,
                        //    idAfiliadoPlan,
                        //    documentoEmpresa,
                        //    documentoAfiliado,
                        //    numeroFactura,
                        //    DateTime.Now,
                        //    periodoInicio,
                        //    periodoFin,
                        //    diasFactura,
                        //    valorPlan,
                        //    descuento,
                        //    valorFacturado,
                        //    idUsuario
                        //);
                    }

                    // después puedes generar la factura en Siigo
                    // GenerarFacturaSiigo(idLiquidacion);
                }
            }
        }
        protected void btnVerDCRDetalle_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idDetalle = Convert.ToInt32(btn.CommandArgument);

            //GenerarPdfDCRPorDetalle(idDetalle);
        }

        protected async void btnFacturar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idLiquidacion = Convert.ToInt32(btn.CommandArgument);
            clasesglobales cg = new clasesglobales();

            int idAfiliado = Convert.ToInt32(Session["IdAfiliado"]);
            int idUsuario = Convert.ToInt32(Session["idusuario"]);


            int idAfiliadoPlan = 0;

            int idCanalVenta = 0;
            DataTable dtCanal = cg.ConsultarUsuarioSedePerfilPorId(idUsuario);
            if (dtCanal.Rows.Count > 0)
                idCanalVenta = Convert.ToInt32(dtCanal.Rows[0]["idCanalVenta"]);

            DataTable mediosPago = cg.ConsultarMediosDePago();
            DataTable dtAfiliado = cg.ConsultarAfiliadoPorId(idAfiliado);
            string docAfiliado = dtAfiliado.Rows[0]["DocumentoAfiliado"].ToString();
            string valorPagadoTexto = "0";
            string valorLimpio = Regex.Replace(valorPagadoTexto, @"[^\d]", "");
            int valorPagado;
            int.TryParse(valorLimpio, out valorPagado);

           
                DataTable dtActivo = cg.ConsultarAfiliadoEstadoActivo(idAfiliado);
                //string rtaCRM = cg.ActualizarEstadoCRMPagoPlan(idcrm, dtActivo.Rows[0]["NombrePlan"].ToString(), valorPagado, idUsuario, 3);
                //RegistrarPagos(cg, mediosPago, idAfiliadoPlan, idUsuario, idCanalVenta, idcrm, null);


                var resultado = await GenerarFacturacorporativoAsync(idAfiliadoPlan, ViewState["codSiigoPlan"].ToString(), ViewState["nombrePlan"].ToString(), valorPagado.ToString());

                if (!resultado.ok)
                {
                    MostrarAlerta(
                        "Error",
                        "No se pudo generar la factura en Siigo. Puede reintentarse.",
                        "warning"
                    );
                    return;
                }
            }


    

         private async Task<(bool ok, string idSiigoFactura)> GenerarFacturacorporativoAsync(int idAfiliadoPlan, string codSiigoPlan, string nombrePlan, string precioPlan)
        {
            string idAfiliado = Session["IdAfiliado"].ToString();
            string urlRedirect = $"planesAfiliado?id={idAfiliado}";

            try
            {
                string observaciones = $"Pago correspondiente del plan {nombrePlan} por valor de ${precioPlan}.";

                string fechaActual = DateTime.Now.ToString("yyyy-MM-dd");

                clasesglobales cg = new clasesglobales();

                string ambiente = cg.GetAppSetting("AmbienteSiigo");
                string idIntegracionSiigoStr = cg.GetAppSetting("idIntegracionSiigo");
                int idIntegracionSiigo = Convert.ToInt32(idIntegracionSiigoStr);

                DataTable dtIntegracion = cg.ConsultarIntegracionPorId(idIntegracionSiigo);
                string url = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? dtIntegracion.Rows[0]["url"].ToString() : null;
                string username = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? dtIntegracion.Rows[0]["username"].ToString() : null;
                string accessKey = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? dtIntegracion.Rows[0]["accessKey"].ToString() : null;
                string partnerId = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? dtIntegracion.Rows[0]["partnerId"].ToString() : null;

                int idDocumentType = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? Convert.ToInt32(dtIntegracion.Rows[0]["idDocumentType"].ToString()) : 0;
                int idSellerUser = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? Convert.ToInt32(dtIntegracion.Rows[0]["idSellerUser"].ToString()) : 0;
                int idPayment = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? Convert.ToInt32(dtIntegracion.Rows[0]["idPayment"].ToString()) : 0;
                dtIntegracion.Dispose();


                // 1. Creación de factura en Siigo
                var siigoClient = new SiigoClient(
                    new HttpClient(),
                    url,
                    username,
                    accessKey,
                    partnerId
                );

                DataTable dtAfi = cg.ConsultarAfiliadoPorId(Convert.ToInt32(idAfiliado));

                if (dtAfi.Rows.Count == 0) return (false, null);
                string nroDoc = dtAfi.Rows[0]["DocumentoAfiliado"].ToString();
                string strNombre = LimpiarTextoSiigo(dtAfi.Rows[0]["NombreAfiliado"].ToString());
                string strApellido = LimpiarTextoSiigo(dtAfi.Rows[0]["ApellidoAfiliado"].ToString());
                string strCelular = Regex.Replace(dtAfi.Rows[0]["CelularAfiliado"].ToString(), @"[^\d]", "");
                string strEmail = dtAfi.Rows[0]["EmailAfiliado"].ToString();

                int idSede = Convert.ToInt32(dtAfi.Rows[0]["idSede"].ToString());
                dtAfi.Dispose();

                DataTable dtCodSiigoDocumento = cg.ConsultarCodigoSiigoPorDocumento(nroDoc);
                string idTipoDocSiigo = dtCodSiigoDocumento.Rows[0]["CodSiigo"].ToString();
                dtCodSiigoDocumento.Dispose();

                DataTable dtSede = cg.ConsultarSedePorId(idSede);
                string direccion = dtSede.Rows[0]["DireccionSede"].ToString();
                int idCiudad = Convert.ToInt32(dtSede.Rows[0]["idCiudadSede"].ToString());
                dtSede.Dispose();

                DataTable dtCiudad = cg.ConsultarCiudadSedeSiigoPorId(idCiudad);
                string codEstado = dtCiudad.Rows[0]["CodigoEstado"].ToString();
                string codCiudad = dtCiudad.Rows[0]["CodigoCiudad"].ToString();
                dtCiudad.Dispose();

                await siigoClient.ManageCustomerAsync(idTipoDocSiigo, nroDoc, strNombre, strApellido, direccion, codEstado, codCiudad, strCelular, strEmail);

                DataTable dtSedeCostCenter = cg.ConsultarSedePorId(idSede);
                int idCostCenter = dtSedeCostCenter != null && dtSedeCostCenter.Rows.Count > 0 ? Convert.ToInt32(dtSedeCostCenter.Rows[0]["idCostCenterSiigo"].ToString()) : 0;
                dtSedeCostCenter.Dispose();

                string _codSiigoPlan;
                string _nombrePlan;
                int _precioPlan;

                //  PRUEBAS
                if (idIntegracionSiigo == 3)
                {
                    idCostCenter = 621;
                    _codSiigoPlan = "COD2433";
                    _nombrePlan = "Pago de suscripción";
                    _precioPlan = 10000;
                }
                //  PRODUCCIÓN
                else if (idIntegracionSiigo == 6)
                {
                    _codSiigoPlan = codSiigoPlan;
                    _nombrePlan = nombrePlan;
                    _precioPlan = int.Parse(precioPlan);
                }
                else
                {
                    throw new Exception("Id de integración Siigo no válido.");
                }
                string idSiigoFactura = await siigoClient.RegisterInvoiceAsync(
                    nroDoc,
                    _codSiigoPlan,
                    _nombrePlan,
                    _precioPlan,
                    observaciones,
                    idSellerUser,
                    idDocumentType,
                    fechaActual,
                    idCostCenter,
                    idPayment
                );

                //Session["idAfiliadoPlan"] = idAfiliadoPlan;
                //string referencia = Session["documentoAfiliado"].ToString() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                //string codDatafono = Session["codDatafono"].ToString();

                return (true, idSiigoFactura);
            }
            catch (Exception ex)
            {
                //MostrarAlerta("Error", "El pago fue aprobado, pero ocurrió un error en el registro interno. Por favor, comunicarse con el área de sistemas.", "error");
                System.Diagnostics.Debug.WriteLine("Error en ProcesarPagoExitosoAsync: " + ex.ToString());
                return (false, null);
            }
        }

        public static string LimpiarTextoSiigo(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            // Normaliza (separa acentos)
            texto = texto.Normalize(NormalizationForm.FormD);

            var sb = new StringBuilder();

            foreach (char c in texto)
            {
                var categoria = Char.GetUnicodeCategory(c);

                // Letras, números, espacio
                if (categoria == UnicodeCategory.UppercaseLetter ||
                    categoria == UnicodeCategory.LowercaseLetter ||
                    categoria == UnicodeCategory.DecimalDigitNumber ||
                    categoria == UnicodeCategory.SpaceSeparator)
                {
                    sb.Append(c);
                }
            }

            return sb
                .ToString()
                .Normalize(NormalizationForm.FormC)
                .Trim();
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        protected void gvLiquidaciones_RowCommand1(object sender, GridViewCommandEventArgs e)
        {

        }
    }


    }
