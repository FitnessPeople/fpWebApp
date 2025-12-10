using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class reporteventasasesor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("es-CO");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (!IsPostBack)
            {
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

                    listaVentas();
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

        private void listaVentas()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                ltMes2.Text = ltMes1.Text;
                ltMes3.Text = ltMes1.Text;
                ltMes4.Text = ltMes1.Text;

                int filtroMedioPago = Convert.ToInt32(ddlTipoPago.SelectedValue);

                //DataTable dt = cg.ConsultarPagosPorTipoPorAsesor(Convert.ToInt32(Session["IdUsuario"].ToString()), filtroMedioPago, txbFechaIni.Value, txbFechaFin.Value, out decimal valorTotal);

                string strQuery = @"
                    SELECT  
	                    ppa.idAfiliadoPlan AS idAfilPlan, 
                        MAX(a.DocumentoAfiliado) AS Documento, 
                        MAX(CONCAT(a.NombreAfiliado,' ',a.ApellidoAfiliado)) AS Afiliado, 
                        SUM(ppa.Valor) AS Sumatoria,
	                    MAX(ppa.FechaHoraPago) AS FechaHora, 
	                    MAX(ppa.EstadoPago) AS Est, 
	                    MAX(p.NombrePlan) AS Plan, 
	                    MAX(ppa.idSiigoFactura) AS idSiigo 
                    FROM PagosPlanAfiliado ppa
                    INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan
                    INNER JOIN Afiliados a ON a.idAfiliado = ap.idAfiliado
                    LEFT JOIN planes p ON p.idPlan = ap.idPlan
                    WHERE 
	                    ppa.idUsuario = " + Session["IdUsuario"].ToString() + @"
	                    AND DATE(ppa.FechaHoraPago) BETWEEN '" + txbFechaIni.Value.ToString() + @"' AND '" + txbFechaFin.Value.ToString() + @"' 
                    GROUP BY ppa.idAfiliadoPlan 
                    ORDER BY FechaHora DESC";

                DataTable dt = cg.TraerDatos(strQuery);

                rpPagos.DataSource = dt;
                rpPagos.DataBind();

                DataTable dt1 = cg.ConsultarPagosPorTipoPorAsesorSinFechas(Convert.ToInt32(Session["IdUsuario"].ToString()), filtroMedioPago, out decimal valorTotal1);

                /////////////////////////////////INDICADORES/////////////////////////////////////////////////////
                DateTime hoyInicio = DateTime.Today;
                DateTime hoyFin = hoyInicio.AddDays(1).AddTicks(-1);

                DateTime ayerInicio = DateTime.Today.AddDays(-1);                 // 2025-11-25 00:00:00
                DateTime ayerFin = ayerInicio.AddHours(23).AddMinutes(59).AddSeconds(59); // 2025-11-25 23:59:59

                DateTime mesInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DateTime mesFin = mesInicio.AddMonths(1).AddTicks(-1);


                DataRow[] filasHoy = dt.Select(
                    $"FechaHora >= #{hoyInicio:MM/dd/yyyy HH:mm:ss}# AND FechaHora <= #{hoyFin:MM/dd/yyyy HH:mm:ss}#"
                );

                DataRow[] filasAyer = dt1.Select(
                    $"FechaHoraPago >= #{ayerInicio:yyyy-MM-dd HH:mm:ss}# AND FechaHoraPago <= #{ayerFin:yyyy-MM-dd HH:mm:ss}#"
                );

                DataRow[] filasMes = dt1.Select(
                    $"FechaHoraPago >= #{mesInicio:MM/dd/yyyy HH:mm:ss}# AND FechaHoraPago <= #{mesFin:MM/dd/yyyy HH:mm:ss}#"
                );


                // Ventas de hoy
                decimal ventasHoy = 0;

                // Validar que sí existan filas
                if (filasHoy.Length > 0)
                {
                    ventasHoy = Convert.ToDecimal(filasHoy[0].ItemArray[3]);
                }
                else
                {
                    // No hay registros → valor por defecto
                    ventasHoy = 0;
                }


                // Ventas de ayer
                decimal ventasAyer = 0;
                int registrosAyer = 0;


                if (filasAyer.Length > 0)
                {
                    ventasAyer = filasAyer.Sum(f => Convert.ToDecimal(f["Valor"]));
                }

                ltVentasMes.Text = "$ " + ventasAyer.ToString("N0");

                // Ventas del mes
                decimal ventasMes = filasMes.Length > 0 ? filasMes.Sum(r => r.Field<int>("Valor")) : 0;

                // Cantidad de transacciones hoy
                int transaccionesHoy = filasHoy.Length;

                // Ticket promedio del día
                decimal ticketPromedioHoy = transaccionesHoy > 0 ? ventasHoy / transaccionesHoy : 0;

                ltVentasHoy.Text = "$ " + ventasHoy.ToString("N0");
                ltVentasAyer.Text = "$ " + ventasAyer.ToString("N0");
                ltVentasMes.Text = "$ " + ventasMes.ToString("N0");
                //ltTicketPromedio.Text = "$ " + ticketPromedioHoy.ToString("N0");
                ltTransaccionesHoy.Text = transaccionesHoy.ToString("N0");

                dt.Dispose();

            }
            catch (Exception ex)
            {

                ltMensaje.Text =
                 "<div class='alert alert-danger alert-dismissable'>" +
                 "<button aria-hidden='true' data-dismiss='alert' class='close' type='button'>×</button>" +
                 "<strong>Error:</strong> " + ex.Message.ToString() +
                 "</div>";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            listaVentas();
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                int filtroMedioPago = Convert.ToInt32(ddlTipoPago.SelectedValue);

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarPagosPorTipoPorAsesor(Convert.ToInt32(Session["IdUsuario"].ToString()), filtroMedioPago, txbFechaIni.Value, txbFechaFin.Value, out decimal valorTotal);

                string nombre = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                cg.ExportarExcelOk(dt, nombre);

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
            }
        }

        protected void lbExportarPdf_Click(object sender, EventArgs e)
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                int filtroMedioPago = Convert.ToInt32(ddlTipoPago.SelectedValue);

                DataSet ds = cg.ConsultarPagosPorTipoPorMedioPago(
                    Convert.ToInt32(Session["IdUsuario"]),
                    filtroMedioPago,
                    txbFechaIni.Value,
                    txbFechaFin.Value,
                    out decimal valorTotal
                );

                DataTable dtPagos = ds.Tables[0];      // detalle
                DataTable dtTotales = ds.Tables.Count > 2 ? ds.Tables[2] : null;

                string nombreArchivo = $"{DateTime.Now:yyyyMMdd_HHmmss}";
                cg.ExportarPDF(dtPagos, dtTotales, nombreArchivo);


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al generar PDF: " + ex.Message + "');</script>");
            }
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