using Newtonsoft.Json;
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
    public partial class reporteventasasesor : System.Web.UI.Page
    {
        protected string Grafico1 = "{}";
        protected string Grafico2 = "{}";
        protected string Grafico3 = "{}";
        protected string Grafico4 = "{}";
        protected string Grafico5 = "{}";
        protected string Grafico6 = "{}";

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

                    int filtroMedioPago = int.TryParse(ddlTipoPago.SelectedValue, out int valor)
                    ? valor
                    : 0;
                    listaVentas();

                    //VentasCounter();
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
                ltMes5.Text = ltMes1.Text;

                CalcularTotalesVentas();

                int filtroMedioPago = Convert.ToInt32(ddlTipoPago.SelectedValue);

                DataTable dt = cg.ConsultarPagosPorTipoPorAsesor(Convert.ToInt32(Session["IdUsuario"].ToString()), filtroMedioPago, txbFechaIni.Value, txbFechaFin.Value, out decimal valorTotal);

                rpPagos.DataSource = dt;
                rpPagos.DataBind();

                /////////////////////////////////INDICADORES/////////////////////////////////////////////////////
                DateTime hoyInicio = DateTime.Today;                          
                DateTime hoyFin = hoyInicio.AddDays(1).AddTicks(-1);

                DateTime ayerInicio = DateTime.Today.AddDays(-1);                 // 2025-11-25 00:00:00
                DateTime ayerFin = ayerInicio.AddHours(23).AddMinutes(59).AddSeconds(59); // 2025-11-25 23:59:59

                DateTime mesInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DateTime mesFin = mesInicio.AddMonths(1).AddTicks(-1);


                DataRow[] filasHoy = dt.Select(
                    $"FechaHoraPago >= #{hoyInicio:MM/dd/yyyy HH:mm:ss}# AND FechaHoraPago <= #{hoyFin:MM/dd/yyyy HH:mm:ss}#"
                );

                DataRow[] filasAyer = dt.Select(
                    $"FechaHoraPago >= #{ayerInicio:yyyy-MM-dd HH:mm:ss}# AND FechaHoraPago <= #{ayerFin:yyyy-MM-dd HH:mm:ss}#"
                );

                DataRow[] filasMes = dt.Select(
                    $"FechaHoraPago >= #{mesInicio:MM/dd/yyyy HH:mm:ss}# AND FechaHoraPago <= #{mesFin:MM/dd/yyyy HH:mm:ss}#"
                );


                // Ventas de hoy
                decimal ventasHoy = filasHoy.Length > 0 ? filasHoy.Sum(r => r.Field<int>("Valor")) : 0;

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
                ltVentasMes.Text = "$ " + ventasAyer.ToString("N0");
                //ltVentasMes.Text = "$ " + ventasMes.ToString("N0");
                ltTicketPromedio.Text = "$ " + ticketPromedioHoy.ToString("N0");
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

        private void CalcularTotalesVentas()
        {
            clasesglobales cg = new clasesglobales();

            string strQuery = @"
                SELECT 
                    YEAR(FechaHoraPago) AS Anio,
                    MONTH(FechaHoraPago) AS Mes,
                    COUNT(*) AS TotalRegistros
                FROM PagosPlanAfiliado
                GROUP BY YEAR(FechaHoraPago), MONTH(FechaHoraPago)
                ORDER BY Anio, Mes;
                ";

            decimal sumatoriaValor = 0;
            decimal sumatoriaRegistros = 0;

            DataTable dt = cg.TraerDatos(strQuery);

            string filtroMedioPago = "";
            if (ddlTipoPago.SelectedValue != "0") // 0 = Todos
            {
                filtroMedioPago = " AND ppa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue);
            }

            foreach (DataRow dr in dt.Rows)
            {
                string query = @"
                SELECT ppa.idPago, ppa.idAfiliadoPlan, ppa.IdReferencia, ppa.FechaHoraPago, ppa.EstadoPago, ppa.Valor, 
                    a.DocumentoAfiliado,
                    CONCAT_WS(' ', a.NombreAfiliado, a.ApellidoAfiliado) AS NombreAfiliado,
                    u.NombreUsuario AS Usuario,
                    cv.NombreCanalVenta AS CanalVenta, NombreMedioPago, p.NombrePlan  
                FROM PagosPlanAfiliado ppa 
                INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
                INNER JOIN afiliados a ON a.idAfiliado = ap.idAfiliado    
                INNER JOIN usuarios u ON u.idUsuario = ppa.idUsuario  
                INNER JOIN empleados e ON e.DocumentoEmpleado = u.idEmpleado
                INNER JOIN canalesventa cv ON cv.idCanalVenta = e.idCanalVenta 
                INNER JOIN mediosdepago mp ON mp.idMedioPago = ppa.idMedioPago 
                INNER JOIN planes p ON p.idPlan = ap.idPlan 
                WHERE ppa.idUsuario NOT IN (156) 
                " + filtroMedioPago + @" 
                AND ap.idPlan IN (1, 17) 
                AND MONTH(ppa.fechaHoraPago) = " + dr["Mes"].ToString() + @" 
                AND YEAR(ppa.fechaHoraPago) = " + dr["Anio"].ToString() + @" 
                AND MONTH(ap.FechaInicioPlan) IN (" + dr["Mes"].ToString() + @") 
                UNION ALL
                SELECT ppa.idPago, ppa.idAfiliadoPlan, ppa.IdReferencia, ppa.FechaHoraPago, ppa.EstadoPago, ppa.Valor, 
                    a.DocumentoAfiliado,
                    CONCAT_WS(' ', a.NombreAfiliado, a.ApellidoAfiliado) AS NombreAfiliado, 
                    u.NombreUsuario AS Usuario,
                    cv.NombreCanalVenta AS CanalVenta, NombreMedioPago, p.NombrePlan  
                FROM PagosPlanAfiliado ppa 
                INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
                INNER JOIN afiliados a ON a.idAfiliado = ap.idAfiliado    
                INNER JOIN usuarios u ON u.idUsuario = ppa.idUsuario  
                INNER JOIN empleados e ON e.DocumentoEmpleado = u.idEmpleado
                INNER JOIN canalesventa cv ON cv.idCanalVenta = e.idCanalVenta 
                INNER JOIN mediosdepago mp ON mp.idMedioPago = ppa.idMedioPago 
                INNER JOIN planes p ON p.idPlan = ap.idPlan 
                WHERE ppa.idUsuario = 156 
                " + filtroMedioPago + @" 
                AND ap.idPlan IN (18,19) 
                AND MONTH(ppa.fechaHoraPago) = " + dr["Mes"].ToString() + @" 
                AND YEAR(ppa.fechaHoraPago) = " + dr["Anio"].ToString() + @" 
                AND MONTH(ap.FechaInicioPlan) IN (" + dr["Mes"].ToString() + @") 
                ORDER BY idPago DESC";

                DataTable dt1 = cg.TraerDatos(query);

                if (dt1.Rows.Count > 0)
                {
                    object suma = dt1.Compute("SUM(Valor)", "");
                    sumatoriaValor += suma != DBNull.Value ? Convert.ToDecimal(suma) : 0;
                    sumatoriaRegistros += dt1.Rows.Count;
                }
            }

            ltTransaccionesHoy.Text = "$ " + String.Format("{0:N0}", sumatoriaValor);
            ltRegistros4.Text = sumatoriaRegistros.ToString();
        }

        private void HistorialCobrosRechazados()
        {
            clasesglobales cg = new clasesglobales();

            string strQuery = @"
                SELECT ap.idAfiliadoPlan, a.DocumentoAfiliado, CONCAT(a.NombreAfiliado, "" "", a.ApellidoAfiliado) AS NombreCompletoAfiliado, 
                COUNT(a.idAfiliado) AS Intentos, MAX(hcr.FechaIntento) AS UltimoIntento, MAX(hcr.MensajeEstado) AS Mensaje 
                FROM HistorialCobrosRechazados AS hcr 
                INNER JOIN AfiliadosPlanes AS ap ON ap.idAfiliadoPlan = hcr.idAfiliadoPlan 
                INNER JOIN Afiliados AS a ON a.idAfiliado = ap.idAfiliado 
                GROUP BY ap.idAfiliadoPlan, a.DocumentoAfiliado, NombreCompletoAfiliado;
                ";


            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos.Text = dt.Rows.Count.ToString();

            rpHistorialCobrosRechazados.DataSource = dt;
            rpHistorialCobrosRechazados.DataBind();
        }

        private void CrearGrafico1(string fechaIni)
        {
            //Comparativo de Ventas y Cantidad Diario
            clasesglobales cg = new clasesglobales();
            //int anio = Convert.ToDateTime(fechaIni).Year;
            //int mes = Convert.ToDateTime(fechaIni).Month;

            int anio = 2025;
            int mes = Convert.ToInt32(fechaIni);

            string query = @"
                SELECT 
	                COUNT(DISTINCT ppa.idAfiliadoPlan) AS cuantos, 
	                DATE(ppa.FechaHoraPago) AS dia,
	                SUM(ppa.valor) AS sumatoria 
                FROM PagosPlanAfiliado ppa 
                INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
                WHERE ((ppa.idUsuario = 156 AND ap.idPlan IN (18,19,20,21)) OR (ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17))) 
	                AND (ppa.idMedioPago = 4)    
                    AND MONTH(ppa.FechaHoraPago) = " + mes.ToString() + @" 
                    AND YEAR(ap.FechaInicioPlan) = " + anio.ToString() + @" 
                    AND MONTH(ap.FechaInicioPlan) = " + mes.ToString() + @" 
                GROUP BY DATE(FechaHoraPago) 
                ORDER BY dia;";

            DataTable dt = cg.TraerDatos(query);

            // Convertir los datos a listas para Chart.js
            var labels = new System.Collections.Generic.List<string>();
            var ventas = new System.Collections.Generic.List<decimal>();
            var cantidad = new System.Collections.Generic.List<decimal>();

            foreach (DataRow row in dt.Rows)
            {
                labels.Add(((DateTime)row["dia"]).ToString("dd MMM"));
                ventas.Add(Convert.ToDecimal(row["sumatoria"]));
                cantidad.Add(Convert.ToDecimal(row["cuantos"]));
            }

            // Crear objeto para enviar a JS
            var datos = new
            {
                labels = labels,
                ventas = ventas,
                cantidad = cantidad
            };

            dt.Dispose();

            Grafico1 = JsonConvert.SerializeObject(datos);
        }

        private void CrearGrafico2(string fechaIni)
        {
            //Comparativo de Ventas y Cantidad por Usuario
            clasesglobales cg = new clasesglobales();
            //int anio = Convert.ToDateTime(fechaIni).Year;
            //int mes = Convert.ToDateTime(fechaIni).Month;

            int anio = 2025;
            int mes = Convert.ToInt32(fechaIni);

            string query = @"
                SELECT 
                    ppa.idUsuario, u.NombreUsuario, 
                    COUNT(*) AS cuantos,
                    SUM(ppa.valor) AS sumatoria
                FROM pagosplanafiliado ppa  
                INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
                INNER JOIN Usuarios u ON ppa.idUsuario = u.idUsuario 
                WHERE ((ppa.idUsuario = 156 AND ap.idPlan IN (18,19,20,21)) OR (ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17))) 
	                AND (ppa.idMedioPago = 4)    
                    AND MONTH(ppa.FechaHoraPago) = " + mes.ToString() + @" 
                    AND YEAR(ap.FechaInicioPlan) = " + anio.ToString() + @" 
                    AND MONTH(ap.FechaInicioPlan) = " + mes.ToString() + @" 
                GROUP BY ppa.idUsuario 
                ORDER BY ppa.idUsuario;";

            DataTable dt = cg.TraerDatos(query);

            // Convertir los datos a listas para Chart.js
            var labels = new System.Collections.Generic.List<string>();
            var ventas = new System.Collections.Generic.List<decimal>();
            var cantidad = new System.Collections.Generic.List<decimal>();

            foreach (DataRow row in dt.Rows)
            {
                labels.Add((row["NombreUsuario"]).ToString());
                ventas.Add(Convert.ToDecimal(row["sumatoria"]));
                cantidad.Add(Convert.ToDecimal(row["cuantos"]));
            }

            // Crear objeto para enviar a JS
            var datos = new
            {
                labels = labels,
                ventas = ventas,
                cantidad = cantidad
            };

            dt.Dispose();

            Grafico2 = JsonConvert.SerializeObject(datos);
        }

        private void CrearGrafico3(string fechaIni)
        {
            //Comparativo de Ventas y Cantidad por Canal de Venta
            clasesglobales cg = new clasesglobales();
            //int anio = Convert.ToDateTime(fechaIni).Year;
            //int mes = Convert.ToDateTime(fechaIni).Month;

            int anio = 2025;
            int mes = Convert.ToInt32(fechaIni);

            string query = @"
                SELECT 
                    ppa.idCanalVenta, cv.NombreCanalVenta, 
                    COUNT(*) AS cuantos,
                    SUM(ppa.valor) AS sumatoria
                FROM pagosplanafiliado ppa 
                INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
                INNER JOIN CanalesVenta cv ON ppa.idCanalVenta = cv.idCanalVenta 
                WHERE ((ppa.idUsuario = 156 AND ap.idPlan IN (18,19,20,21)) OR (ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17))) 
	                AND (ppa.idMedioPago = 4)    
                    AND MONTH(ppa.FechaHoraPago) = " + mes.ToString() + @" 
                    AND YEAR(ap.FechaInicioPlan) = " + anio.ToString() + @" 
                    AND MONTH(ap.FechaInicioPlan) = " + mes.ToString() + @" 
                GROUP BY ppa.idCanalVenta 
                ORDER BY ppa.idCanalVenta;";

            DataTable dt = cg.TraerDatos(query);

            // Convertir los datos a listas para Chart.js
            var labels = new System.Collections.Generic.List<string>();
            var ventas = new System.Collections.Generic.List<decimal>();
            var cantidad = new System.Collections.Generic.List<decimal>();

            foreach (DataRow row in dt.Rows)
            {
                labels.Add((row["NombreCanalVenta"]).ToString());
                ventas.Add(Convert.ToDecimal(row["sumatoria"]));
                cantidad.Add(Convert.ToDecimal(row["cuantos"]));
            }

            dt.Dispose();

            // Crear objeto para enviar a JS
            var datos = new
            {
                labels = labels,
                ventas = ventas,
                cantidad = cantidad
            };

            Grafico3 = JsonConvert.SerializeObject(datos);
        }

        private void CrearGrafico4(string fechaIni)
        {
            //Comparativo de Ventas y Cantidad por Banco
            clasesglobales cg = new clasesglobales();
            //int anio = Convert.ToDateTime(fechaIni).Year;
            //int mes = Convert.ToDateTime(fechaIni).Month;

            int anio = 2025;
            int mes = Convert.ToInt32(fechaIni);

            string query = @"
                SELECT 
                    Banco, 
                    COUNT(*) AS cuantos,
                    SUM(ppa.valor) AS sumatoria
                FROM pagosplanafiliado ppa 
                INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
                WHERE ((ppa.idUsuario = 156 AND ap.idPlan IN (18,19,20,21)) OR (ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17))) 
	                AND (ppa.idMedioPago = 4)    
                    AND MONTH(ppa.FechaHoraPago) = " + mes.ToString() + @" 
                    AND YEAR(ap.FechaInicioPlan) = " + anio.ToString() + @" 
                    AND MONTH(ap.FechaInicioPlan) = " + mes.ToString() + @" 
                GROUP BY Banco 
                ORDER BY Banco;";

            DataTable dt = cg.TraerDatos(query);

            // Convertir los datos a listas para Chart.js
            var labels = new System.Collections.Generic.List<string>();
            var ventas = new System.Collections.Generic.List<decimal>();
            var cantidad = new System.Collections.Generic.List<decimal>();

            foreach (DataRow row in dt.Rows)
            {
                labels.Add((row["Banco"]).ToString());
                ventas.Add(Convert.ToDecimal(row["sumatoria"]));
                cantidad.Add(Convert.ToDecimal(row["cuantos"]));
            }

            dt.Dispose();

            // Crear objeto para enviar a JS
            var datos = new
            {
                labels = labels,
                ventas = ventas,
                cantidad = cantidad
            };

            Grafico4 = JsonConvert.SerializeObject(datos);
        }

        private void CrearGrafico5(string fechaIni)
        {
            //Comparativo de Ventas y Cantidad por Medio de Pago
            clasesglobales cg = new clasesglobales();
            //int anio = Convert.ToDateTime(fechaIni).Year;
            //int mes = Convert.ToDateTime(fechaIni).Month;

            int anio = 2025;
            int mes = Convert.ToInt32(fechaIni);

            string query = @"
                SELECT 
                    ppa.idMedioPago, mp.NombreMedioPago, 
                    COUNT(*) AS cuantos,
                    SUM(ppa.valor) AS sumatoria
                FROM pagosplanafiliado ppa 
                INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
                INNER JOIN MediosdePago mp ON ppa.idMedioPago = mp.idMedioPago 
                WHERE ((ppa.idUsuario = 156 AND ap.idPlan IN (18,19,20,21)) OR (ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17))) 
	                AND (ppa.idMedioPago = 4)    
                    AND MONTH(ppa.FechaHoraPago) = " + mes.ToString() + @" 
                    AND YEAR(ap.FechaInicioPlan) = " + anio.ToString() + @" 
                    AND MONTH(ap.FechaInicioPlan) = " + mes.ToString() + @" 
                GROUP BY ppa.idMedioPago 
                ORDER BY ppa.idMedioPago;";

            DataTable dt = cg.TraerDatos(query);

            // Convertir los datos a listas para Chart.js
            var labels = new System.Collections.Generic.List<string>();
            var ventas = new System.Collections.Generic.List<decimal>();
            var cantidad = new System.Collections.Generic.List<decimal>();

            foreach (DataRow row in dt.Rows)
            {
                labels.Add((row["NombreMedioPago"]).ToString());
                ventas.Add(Convert.ToDecimal(row["sumatoria"]));
                cantidad.Add(Convert.ToDecimal(row["cuantos"]));
            }

            dt.Dispose();

            // Crear objeto para enviar a JS
            var datos = new
            {
                labels = labels,
                ventas = ventas,
                cantidad = cantidad
            };

            Grafico5 = JsonConvert.SerializeObject(datos);
        }

        private void CrearGrafico6(string fechaIni)
        {
            //Comparativo de Ventas y Cantidad por Plan
            clasesglobales cg = new clasesglobales();
            //int anio = Convert.ToDateTime(fechaIni).Year;
            //int mes = Convert.ToDateTime(fechaIni).Month;

            int anio = 2025;
            int mes = Convert.ToInt32(fechaIni);

            string query = @"
                SELECT 
                    p.NombrePlan, 
                    COUNT(*) AS cuantos,
                    SUM(ppa.valor) AS sumatoria
                FROM pagosplanafiliado ppa 
                INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
                INNER JOIN planes p ON p.idPlan = ap.idPlan 
                WHERE ((ppa.idUsuario = 156 AND ap.idPlan IN (18,19,20,21)) OR (ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17))) 
	                AND (ppa.idMedioPago = 4)    
                    AND MONTH(ppa.FechaHoraPago) = " + mes.ToString() + @" 
                    AND YEAR(ap.FechaInicioPlan) = " + anio.ToString() + @" 
                    AND MONTH(ap.FechaInicioPlan) = " + mes.ToString() + @" 
                GROUP BY p.NombrePlan 
                ORDER BY p.NombrePlan;";

            DataTable dt = cg.TraerDatos(query);

            // Convertir los datos a listas para Chart.js
            var labels = new System.Collections.Generic.List<string>();
            var ventas = new System.Collections.Generic.List<decimal>();
            var cantidad = new System.Collections.Generic.List<decimal>();

            foreach (DataRow row in dt.Rows)
            {
                labels.Add((row["NombrePlan"]).ToString());
                ventas.Add(Convert.ToDecimal(row["sumatoria"]));
                cantidad.Add(Convert.ToDecimal(row["cuantos"]));
            }

            dt.Dispose();

            // Crear objeto para enviar a JS
            var datos = new
            {
                labels = labels,
                ventas = ventas,
                cantidad = cantidad
            };

            Grafico6 = JsonConvert.SerializeObject(datos);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            listaVentas();
            //VentasCounter();
            //VentasWeb();
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {

        }

        protected void btnDetalle_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "mostrarDetalle")
            {
                int idAfiliadoPlan = int.Parse(e.CommandArgument.ToString());

                Literal ltDetalleModal = (Literal)Page.FindControl("ltDetalleModal");

                if (ltDetalleModal != null)
                {
                    //ltDetalleModal.Text = listarDetalle(idAfiliadoPlan);
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal",
                   "setTimeout(function() { $('#ModalDetalle').modal('show'); }, 500);", true);
            }
        }
    }
}