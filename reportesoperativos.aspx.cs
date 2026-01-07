using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public partial class reportesoperativos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("es-CO");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (!IsPostBack)
            {
                ObtenerReporteSeleccionado();
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Mis ventas");
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
                            txbFechaIni.Attributes.Add("type", "date");
                            txbFechaIni.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                            txbFechaFin.Attributes.Add("type", "date");
                            txbFechaFin.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                        }
                    }

                    ObtenerReporteSeleccionado();
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

        //private void listaVentas()
        //{
        //    clasesglobales cg = new clasesglobales();
        //    try
        //    {
        //        //ltMes2.Text = ltMes1.Text;
        //        //ltMes3.Text = ltMes1.Text;
        //        //ltMes4.Text = ltMes1.Text;

        //        int filtroMedioPago = Convert.ToInt32(ddlTipoReporte.SelectedValue);

        //        //DataTable dt = cg.ConsultarPagosPorTipoPorAsesor(Convert.ToInt32(Session["IdUsuario"].ToString()), filtroMedioPago, txbFechaIni.Value, txbFechaFin.Value, out decimal valorTotal);

        //        string strQuery = @"
        //            SELECT  
        //             ppa.idAfiliadoPlan AS idAfilPlan, 
        //                MAX(a.DocumentoAfiliado) AS Documento, 
        //                MAX(CONCAT(a.NombreAfiliado,' ',a.ApellidoAfiliado)) AS Afiliado, 
        //                SUM(ppa.Valor) AS Sumatoria,
        //             MAX(ppa.FechaHoraPago) AS FechaHora, 
        //             MAX(ppa.EstadoPago) AS Est, 
        //             MAX(p.NombrePlan) AS Plan, 
        //             MAX(ppa.idSiigoFactura) AS idSiigo 
        //            FROM PagosPlanAfiliado ppa
        //            INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan
        //            INNER JOIN Afiliados a ON a.idAfiliado = ap.idAfiliado
        //            LEFT JOIN planes p ON p.idPlan = ap.idPlan
        //            WHERE 
        //             ppa.idUsuario = " + Session["IdUsuario"].ToString() + @"
        //             AND DATE(ppa.FechaHoraPago) BETWEEN '" + txbFechaIni.Value.ToString() + @"' AND '" + txbFechaFin.Value.ToString() + @"' 
        //            GROUP BY ppa.idAfiliadoPlan 
        //            ORDER BY FechaHora DESC";

        //        DataTable dt = cg.TraerDatos(strQuery);

        //        rpPagos.DataSource = dt;
        //        rpPagos.DataBind();

        //        DataTable dt1 = cg.ConsultarPagosPorTipoPorAsesorSinFechas(Convert.ToInt32(Session["IdUsuario"].ToString()), filtroMedioPago, out decimal valorTotal1);

        //        /////////////////////////////////INDICADORES/////////////////////////////////////////////////////
        //        DateTime hoyInicio = DateTime.Today;
        //        DateTime hoyFin = hoyInicio.AddDays(1).AddTicks(-1);

        //        DateTime ayerInicio = DateTime.Today.AddDays(-1);                 // 2025-11-25 00:00:00
        //        DateTime ayerFin = ayerInicio.AddHours(23).AddMinutes(59).AddSeconds(59); // 2025-11-25 23:59:59

        //        DateTime mesInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        //        DateTime mesFin = mesInicio.AddMonths(1).AddTicks(-1);


        //        DataRow[] filasHoy = dt.Select(
        //            $"FechaHora >= #{hoyInicio:MM/dd/yyyy HH:mm:ss}# AND FechaHora <= #{hoyFin:MM/dd/yyyy HH:mm:ss}#"
        //        );

        //        DataRow[] filasAyer = dt1.Select(
        //            $"FechaHoraPago >= #{ayerInicio:yyyy-MM-dd HH:mm:ss}# AND FechaHoraPago <= #{ayerFin:yyyy-MM-dd HH:mm:ss}#"
        //        );

        //        DataRow[] filasMes = dt1.Select(
        //            $"FechaHoraPago >= #{mesInicio:MM/dd/yyyy HH:mm:ss}# AND FechaHoraPago <= #{mesFin:MM/dd/yyyy HH:mm:ss}#"
        //        );


        //        // Ventas de hoy
        //        decimal ventasHoy = 0;

        //        // Validar que sí existan filas
        //        if (filasHoy.Length > 0)
        //        {
        //            ventasHoy = Convert.ToDecimal(filasHoy[0].ItemArray[3]);
        //        }
        //        else
        //        {
        //            // No hay registros → valor por defecto
        //            ventasHoy = 0;
        //        }


        //        // Ventas de ayer
        //        decimal ventasAyer = 0;
        //        int registrosAyer = 0;


        //        if (filasAyer.Length > 0)
        //        {
        //            ventasAyer = filasAyer.Sum(f => Convert.ToDecimal(f["Valor"]));
        //        }

        //        //ltVentasMes.Text = "$ " + ventasAyer.ToString("N0");

        //        // Ventas del mes
        //        decimal ventasMes = filasMes.Length > 0 ? filasMes.Sum(r => r.Field<int>("Valor")) : 0;

        //        // Cantidad de transacciones hoy
        //        int transaccionesHoy = filasHoy.Length;

        //        // Ticket promedio del día
        //        decimal ticketPromedioHoy = transaccionesHoy > 0 ? ventasHoy / transaccionesHoy : 0;

        //        //ltVentasHoy.Text = "$ " + ventasHoy.ToString("N0");
        //        //ltVentasAyer.Text = "$ " + ventasAyer.ToString("N0");
        //        //ltVentasMes.Text = "$ " + ventasMes.ToString("N0");
        //        ////ltTicketPromedio.Text = "$ " + ticketPromedioHoy.ToString("N0");
        //        //ltTransaccionesHoy.Text = transaccionesHoy.ToString("N0");

        //        dt.Dispose();

        //    }
        //    catch (Exception ex)
        //    {

        //        ltMensaje.Text =
        //         "<div class='alert alert-danger alert-dismissable'>" +
        //         "<button aria-hidden='true' data-dismiss='alert' class='close' type='button'>×</button>" +
        //         "<strong>Error:</strong> " + ex.Message.ToString() +
        //         "</div>";
        //    }
        //}

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable dt = ObtenerReporteSeleccionado();

            if (dt == null || dt.Rows.Count == 0)
            {
                MostrarAlerta("Info", "No hay datos para mostrar", "info");
                return;
            }

            ViewState["ReporteActual"] = dt;

            gvReporte.DataSource = dt;
            gvReporte.DataBind();
        }



        private DataTable ObtenerReporteSeleccionado()
        {
            clasesglobales cg = new clasesglobales();

            DateTime fechaIni, fechaFin;
            if (!DateTime.TryParse(txbFechaIni.Value, out fechaIni) ||
                !DateTime.TryParse(txbFechaFin.Value, out fechaFin))
            {
                MostrarAlerta("Error", "Rango de fechas inválido", "warning");
                return null;
            }

            int tipoReporte = Convert.ToInt32(ddlTipoReporte.SelectedValue);
            DataTable dt = null;

            switch (tipoReporte)
            {
                case 1: // Ventas por asesor
                    dt = cg.ConsultarRankingAsesoresPorFecha(fechaIni, fechaFin);
                    break;

                case 2:
                    dt = cg.ConsultarRankingCanalesDeVentaPorFecha(fechaIni, fechaFin);
                    break;

                    //case 3:
                    //    dt = cg.ReporteVentasTotales(fechaIni, fechaFin);
                    //    break;

                    // agrega los demás casos aquí
            }

            return dt;
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

        private void CargarGrid(DataTable dt)
        {
            gvReporte.DataSource = dt;
            gvReporte.DataBind();
        }



        private void ReporteVentasPorSede(DateTime fechaIni, DateTime fechaFin)
        {
            //DataTable dt = cg.ConsultarVentasPorAsesor(fechaIni, fechaFin);
            //gvReporte.DataSource = dt;
            //gvReporte.DataBind();
        }

        private void ReporteVentasTotales(DateTime fechaIni, DateTime fechaFin)
        {
            //DataTable dt = cg.ConsultarVentasTotales(fechaIni, fechaFin);
            //gvReporte.DataSource = dt;
            //gvReporte.DataBind();
        }

        private void ReporteVentasPorPlanes(DateTime fechaIni, DateTime fechaFin)
        {
            //DataTable dt = cg.ConsultarVentasTotales(fechaIni, fechaFin);
            //gvReporte.DataSource = dt;
            //gvReporte.DataBind();
        }

        private void ReporteVentasCorporativo(DateTime fechaIni, DateTime fechaFin)
        {
            //DataTable dt = cg.ConsultarVentasTotales(fechaIni, fechaFin);
            //gvReporte.DataSource = dt;
            //gvReporte.DataBind();
        }



        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["ReporteActual"] == null)
                {
                    MostrarAlerta("Info", "Primero genere el reporte", "info");
                    return;
                }

                DataTable dt = (DataTable)ViewState["ReporteActual"];

                clasesglobales cg = new clasesglobales();
                string nombre = $"Reporte_{DateTime.Now:yyyyMMdd_HHmmss}";

                cg.ExportarExcelOk(dt, nombre);
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", ex.Message, "error");
            }
        }

        protected void lbExportarPdf_Click(object sender, EventArgs e)
        {
            try
            {
                clasesglobales cg = new clasesglobales();

                DateTime fechaIni;
                DateTime fechaFin;

                if (!DateTime.TryParse(txbFechaIni.Value, out fechaIni) ||
                    !DateTime.TryParse(txbFechaFin.Value, out fechaFin))
                {
                    MostrarAlerta("Error", "Debe seleccionar un rango de fechas válido.", "warning");
                    return;
                }

                int tipoReporte = Convert.ToInt32(ddlTipoReporte.SelectedValue);

                DataTable dt = null;

                // 🔁 MISMA lógica del switch del grid
                switch (tipoReporte)
                {
                    case 1:
                        dt = cg.ConsultarRankingAsesoresPorFecha(fechaIni, fechaFin);
                        break;

                    default:
                        MostrarAlerta("Info", "Este reporte aún no tiene exportación PDF.", "info");
                        return;
                }

                if (dt == null || dt.Rows.Count == 0)
                {
                    MostrarAlerta("Info", "No hay datos para exportar.", "info");
                    return;
                }

                string nombreArchivo = $"Reporte_{DateTime.Now:yyyyMMdd_HHmmss}";
                cg.ExportarPDFGen(dt, nombreArchivo);
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", "Error al generar el PDF: " + ex.Message, "error");
            }
        }



        public void ExportarPDF(DataTable dtDetalle, DataTable dtTotales, string nombreArchivo)
        {
            if (dtDetalle == null || dtDetalle.Rows.Count == 0)
                throw new Exception("No hay datos para exportar");

            // Crear documento
            Document doc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            PdfWriter.GetInstance(doc, HttpContext.Current.Response.OutputStream);
            doc.Open();

            // Título
            Paragraph titulo = new Paragraph("Reporte generado",
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);
            doc.Add(new Paragraph(" "));

            // Tabla dinámica
            PdfPTable tabla = new PdfPTable(dtDetalle.Columns.Count);
            tabla.WidthPercentage = 100;

            // Encabezados
            foreach (DataColumn col in dtDetalle.Columns)
            {
                tabla.AddCell(new PdfPCell(new Phrase(col.ColumnName))
                {
                    BackgroundColor = BaseColor.LIGHT_GRAY
                });
            }

            // Datos
            foreach (DataRow row in dtDetalle.Rows)
            {
                foreach (var item in row.ItemArray)
                    tabla.AddCell(item?.ToString() ?? "");
            }

            doc.Add(tabla);

            // Totales (SI EXISTEN)
            if (dtTotales != null && dtTotales.Rows.Count > 0)
            {
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("Totales", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)));

                foreach (DataColumn col in dtTotales.Columns)
                {
                    doc.Add(new Paragraph($"{col.ColumnName}: {dtTotales.Rows[0][col]}"));
                }
            }

            doc.Close();

            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition",
                $"attachment;filename={nombreArchivo}.pdf");
            HttpContext.Current.Response.End();
        }



        protected void rpPagos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;
                int idAfilPlan;
                if (row["idAfilPlan"] is DBNull)
                {
                    idAfilPlan = 0;
                }
                else
                {
                    idAfilPlan = Convert.ToInt32(row["idAfilPlan"]);
                }

                string strQuery = @"
                    SELECT  
                        ppa.idPago AS Pago, 
                        ppa.IdReferencia AS Ref, 
                        ppa.FechaHoraPago AS Fecha, 
                        ppa.Valor,
                        mp.NombreMedioPago AS 'Medio de pago'
                    FROM PagosPlanAfiliado ppa
                        INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
                        INNER JOIN MediosDePago mp ON mp.idMedioPago = ppa.idMedioPago
                    WHERE 
                        ppa.idAfiliadoPlan = " + idAfilPlan.ToString() + @"";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);

                Repeater rpDetallesPago = (Repeater)e.Item.FindControl("rpDetallesPago");
                rpDetallesPago.DataSource = dt;
                rpDetallesPago.DataBind();
            }
        }
    }
}