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

                case 3:
                    dt = cg.ConsultarRankingVentasTotalesPorFecha(fechaIni, fechaFin);
                    break; 
                case 4:
                    dt = cg.ConsultarRankingPlanesPorFecha(fechaIni, fechaFin);
                    break;
                case 5:
                    dt = cg.ConsultarUsuariosPlanesPorFecha(fechaIni, fechaFin);
                    break;
                case 6:
                    dt = cg.ConsultarVentasVsMetasPorFecha(fechaIni, fechaFin);
                    break; 
                case 9:
                    dt = cg.ConsultarAfiliadosActivosInactivosPorFecha(fechaIni, fechaFin);
                    break;
                case 18:
                    dt = cg.ConsultarEmpleadosActivosInactivos();
                    break;


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

                DateTime fechaIni, fechaFin;
                if (!DateTime.TryParse(txbFechaIni.Value, out fechaIni) ||
                    !DateTime.TryParse(txbFechaFin.Value, out fechaFin))
                {
                    MostrarAlerta("Error", "Rango de fechas inválido.", "warning");
                    return;
                }

                int tipoReporte = Convert.ToInt32(ddlTipoReporte.SelectedValue);
                string tituloReporte = string.Empty;
                string nombreArchivo = string.Empty;
                string usuario = Session["NombreUsuario"] as string ?? "Usuario";
               
                DataTable dt = null;

                switch (tipoReporte)
                {
                    case 1:
                        dt = cg.ConsultarRankingAsesoresPorFecha(fechaIni, fechaFin);
                        tituloReporte = $"Reporte Asesores desde {fechaIni:yyyy/MM/dd} hasta {fechaFin:yyyy/MM/dd}";
                        nombreArchivo = $"Reporte_Asesores_{DateTime.Now:yyyyMMdd_HHmmss}_{usuario}";
                        break;

                    case 2:
                        dt = cg.ConsultarRankingCanalesDeVentaPorFecha(fechaIni, fechaFin);
                        tituloReporte = $"Reporte Canales de venta desde {fechaIni:yyyy/MM/dd} hasta {fechaFin:yyyy/MM/dd}";
                        nombreArchivo = $"Reporte_Canales_venta_{DateTime.Now:yyyyMMdd_HHmmss}_{usuario}";
                        break;

                    case 3:
                        dt = cg.ConsultarRankingVentasTotalesPorFecha(fechaIni, fechaFin);
                        tituloReporte = $"Reporte Ventas totales desde {fechaIni:yyyy/MM/dd} hasta {fechaFin:yyyy/MM/dd}";
                        nombreArchivo = $"Reporte_Ventas_totales_{DateTime.Now:yyyyMMdd_HHmmss}_{usuario}";
                        break;

                    case 4:
                        dt = cg.ConsultarRankingPlanesPorFecha(fechaIni, fechaFin);
                        tituloReporte = $"Reporte Planes desde {fechaIni:yyyy/MM/dd} hasta {fechaFin:yyyy/MM/dd}";
                        nombreArchivo = $"Reporte_Planes_{DateTime.Now:yyyyMMdd_HHmmss}_{usuario}";
                        break;
                    case 5:
                        dt = cg.ConsultarRankingPlanesPorFecha(fechaIni, fechaFin);
                        tituloReporte = $"Reporte Usuarios Planes desde {fechaIni:yyyy/MM/dd} hasta {fechaFin:yyyy/MM/dd}";
                        nombreArchivo = $"Reporte_Usuarios_Planes_{DateTime.Now:yyyyMMdd_HHmmss}_{usuario}";
                        break;
                    case 6:
                        dt = cg.ConsultarVentasVsMetasPorFecha(fechaIni, fechaFin);
                        tituloReporte = $"Reporte Metas vs ventas Asesores desde {fechaIni:yyyy/MM/dd} hasta {fechaFin:yyyy/MM/dd}";
                        nombreArchivo = $"Reporte_Metas_Vs_Ventas_Asesores{DateTime.Now:yyyyMMdd_HHmmss}_{usuario}";
                        break;
                    case 9:
                        dt = cg.ConsultarAfiliadosActivosInactivosPorFecha(fechaIni, fechaFin);
                        tituloReporte = $"Reporte Afiliados activos/inactivos desde {fechaIni:yyyy/MM/dd} hasta {fechaFin:yyyy/MM/dd}";
                        nombreArchivo = $"Reporte_Afiliados_Activos/Inactivos{DateTime.Now:yyyyMMdd_HHmmss}_{usuario}";
                        break;
                    case 18:
                        dt = cg.ConsultarEmpleadosActivosInactivos();
                        tituloReporte = $"Reporte empleados activos/inactivos";
                        nombreArchivo = $"Reporte_empleados_activos/Inactivos";
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

                if (!DateTime.TryParse(txbFechaIni.Value, out fechaIni) ||
                    !DateTime.TryParse(txbFechaFin.Value, out fechaFin))
                {
                    MostrarAlerta("Error", "Debe seleccionar un rango de fechas válido.", "warning");
                    return;
                }

                int tipoReporte = Convert.ToInt32(ddlTipoReporte.SelectedValue);

                DataTable dt = null;

                switch (tipoReporte)
                {
                    case 1:
                        dt = cg.ConsultarRankingAsesoresPorFecha(fechaIni, fechaFin);
                        tituloReporte = $"Reporte Asesores desde {fechaIni:yyyy/MM/dd} hasta {fechaFin:yyyy/MM/dd}";
                        nombreArchivo = $"Reporte_Asesores_{DateTime.Now:yyyyMMdd_HHmmss}_{usuario}";
                        break;

                    case 2:
                        dt = cg.ConsultarRankingCanalesDeVentaPorFecha(fechaIni, fechaFin);
                        tituloReporte = $"Reporte Canales de venta desde {fechaIni:yyyy/MM/dd} hasta {fechaFin:yyyy/MM/dd}";
                        nombreArchivo = $"Reporte_Canales_venta_{DateTime.Now:yyyyMMdd_HHmmss}_{usuario}";
                        break;

                    case 3:
                        dt = cg.ConsultarRankingVentasTotalesPorFecha(fechaIni, fechaFin);
                        tituloReporte = $"Reporte Ventas totales desde {fechaIni:yyyy/MM/dd} hasta {fechaFin:yyyy/MM/dd}";
                        nombreArchivo = $"Reporte_Ventas_totales_{DateTime.Now:yyyyMMdd_HHmmss}_{usuario}";
                        break;

                    case 4:
                        dt = cg.ConsultarRankingPlanesPorFecha(fechaIni, fechaFin);
                        tituloReporte = $"Reporte Planes desde {fechaIni:yyyy/MM/dd} hasta {fechaFin:yyyy/MM/dd}";
                        nombreArchivo = $"Reporte_Planes_{DateTime.Now:yyyyMMdd_HHmmss}_{usuario}";
                        break;
                    case 5:
                        dt = cg.ConsultarRankingPlanesPorFecha(fechaIni, fechaFin);
                        tituloReporte = $"Reporte Usuarios Planes desde {fechaIni:yyyy/MM/dd} hasta {fechaFin:yyyy/MM/dd}";
                        nombreArchivo = $"Reporte_Usuarios_Planes_{DateTime.Now:yyyyMMdd_HHmmss}_{usuario}";
                        break;
                    case 6:
                        dt = cg.ConsultarVentasVsMetasPorFecha(fechaIni, fechaFin);
                        tituloReporte = $"Reporte Metas vs ventas Asesres desde {fechaIni:yyyy/MM/dd} hasta {fechaFin:yyyy/MM/dd}";
                        nombreArchivo = $"Reporte_Metas_Vs_Ventas_Asesres{DateTime.Now:yyyyMMdd_HHmmss}_{usuario}";
                        break;
                    case 9:
                        dt = cg.ConsultarAfiliadosActivosInactivosPorFecha(fechaIni, fechaFin);
                        tituloReporte = $"Reporte Afiliados activos/inactivos desde {fechaIni:yyyy/MM/dd} hasta {fechaFin:yyyy/MM/dd}";
                        nombreArchivo = $"Reporte_Afiliados_Activos/Inactivos{DateTime.Now:yyyyMMdd_HHmmss}_{usuario}";
                        break;
                    case 18:
                        dt = cg.ConsultarEmpleadosActivosInactivos();
                        tituloReporte = $"Reporte empleados activos/inactivos";
                        nombreArchivo = $"Reporte_empleados_Activos/Inactivos";
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

                cg.ExportarPDFGen(dt, nombreArchivo, tituloReporte);
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", "Error al generar el PDF: " + ex.Message, "error");
            }
        }



        //public void ExportarPDF(DataTable dtDetalle, DataTable dtTotales, string nombreArchivo)
        //{
        //    if (dtDetalle == null || dtDetalle.Rows.Count == 0)
        //        throw new Exception("No hay datos para exportar");

        //    // Crear documento
        //    Document doc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
        //    PdfWriter.GetInstance(doc, HttpContext.Current.Response.OutputStream);
        //    doc.Open();

        //    // Título
        //    Paragraph titulo = new Paragraph("Reporte generado",
        //        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));
        //    titulo.Alignment = Element.ALIGN_CENTER;
        //    doc.Add(titulo);
        //    doc.Add(new Paragraph(" "));

        //    // Tabla dinámica
        //    PdfPTable tabla = new PdfPTable(dtDetalle.Columns.Count);
        //    tabla.WidthPercentage = 100;

        //    // Encabezados
        //    foreach (DataColumn col in dtDetalle.Columns)
        //    {
        //        tabla.AddCell(new PdfPCell(new Phrase(col.ColumnName))
        //        {
        //            BackgroundColor = BaseColor.LIGHT_GRAY
        //        });
        //    }

        //    // Datos
        //    foreach (DataRow row in dtDetalle.Rows)
        //    {
        //        foreach (var item in row.ItemArray)
        //            tabla.AddCell(item?.ToString() ?? "");
        //    }

        //    doc.Add(tabla);

        //    // Totales (SI EXISTEN)
        //    if (dtTotales != null && dtTotales.Rows.Count > 0)
        //    {
        //        doc.Add(new Paragraph(" "));
        //        doc.Add(new Paragraph("Totales", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)));

        //        foreach (DataColumn col in dtTotales.Columns)
        //        {
        //            doc.Add(new Paragraph($"{col.ColumnName}: {dtTotales.Rows[0][col]}"));
        //        }
        //    }

        //    doc.Close();

        //    HttpContext.Current.Response.ContentType = "application/pdf";
        //    HttpContext.Current.Response.AddHeader("content-disposition",
        //        $"attachment;filename={nombreArchivo}.pdf");
        //    HttpContext.Current.Response.End();
        //}



        //protected void rpPagos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        DataRowView row = (DataRowView)e.Item.DataItem;
        //        int idAfilPlan;
        //        if (row["idAfilPlan"] is DBNull)
        //        {
        //            idAfilPlan = 0;
        //        }
        //        else
        //        {
        //            idAfilPlan = Convert.ToInt32(row["idAfilPlan"]);
        //        }

        //        string strQuery = @"
        //            SELECT  
        //                ppa.idPago AS Pago, 
        //                ppa.IdReferencia AS Ref, 
        //                ppa.FechaHoraPago AS Fecha, 
        //                ppa.Valor,
        //                mp.NombreMedioPago AS 'Medio de pago'
        //            FROM PagosPlanAfiliado ppa
        //                INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
        //                INNER JOIN MediosDePago mp ON mp.idMedioPago = ppa.idMedioPago
        //            WHERE 
        //                ppa.idAfiliadoPlan = " + idAfilPlan.ToString() + @"";

        //        clasesglobales cg = new clasesglobales();
        //        DataTable dt = cg.TraerDatos(strQuery);

        //        Repeater rpDetallesPago = (Repeater)e.Item.FindControl("rpDetallesPago");
        //        rpDetallesPago.DataSource = dt;
        //        rpDetallesPago.DataBind();
        //    }
        //}
    }
}