using Newtonsoft.Json;
using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class reporteventas : System.Web.UI.Page
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
                    ValidarPermisos("Ventas");
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
                            CargarPlanes();
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
                            //txbFechaIni.Value = DateTime.Now.ToString("yyyy-MM-01").ToString();
                            //txbFechaFin.Attributes.Add("type", "date");
                            //txbFechaFin.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                            CargarPlanes();
                        }
                    }
                    listaVentas();
                    //listaTransaccionesPorFecha(Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()),Convert.ToInt32(ddlPlanes.SelectedValue.ToString()),txbFechaIni.Value.ToString(),txbFechaFin.Value.ToString());
                    VentasWeb();
                    VentasCounter();
                }
                else
                {
                    Response.Redirect("logout.aspx");
                }
            }
        }

        private void VentasWeb()
        {
            int annio = Convert.ToInt32(ddlAnnio.SelectedItem.Value.ToString());
            int mes = Convert.ToInt32(ddlMes.SelectedItem.Value.ToString());

            string strQuery = "";

            if (ddlPlanes.SelectedValue.ToString() == "0")
            {
                strQuery = @"SELECT ppa.valor 
                    FROM PagosPlanAfiliado ppa 
                    INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
                    WHERE ppa.idUsuario = 156 AND ap.idPlan IN (18,19) 
	                    AND (ppa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @") 
	                    AND MONTH(ppa.fechaHoraPago) = " + mes + @" 
                        AND YEAR(ppa.fechaHoraPago) = " + annio + @" 
                        AND MONTH(ap.FechaInicioPlan) IN (" + mes + @");";
            }
            else
            {
                strQuery = @"SELECT ppa.valor 
                    FROM PagosPlanAfiliado ppa 
                    INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
                    WHERE ppa.idUsuario = 156 AND ap.idPlan IN (18,19) 
	                    AND (ppa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @") 
	                    AND MONTH(ppa.fechaHoraPago) = " + mes + @" 
                        AND YEAR(ppa.fechaHoraPago) = " + annio + @" 
                        AND MONTH(ap.FechaInicioPlan) IN (" + mes + @") 
                        AND ap.idPlan = " + ddlPlanes.SelectedValue.ToString();
            }

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            decimal sumatoriaValor = 0;

            if (dt.Rows.Count > 0)
            {
                object suma = dt.Compute("SUM(Valor)", "");
                sumatoriaValor = suma != DBNull.Value ? Convert.ToDecimal(suma) : 0;
            }

            ltCuantos2.Text = "$ " + String.Format("{0:N0}", sumatoriaValor);
            ltRegistros2.Text = dt.Rows.Count.ToString();

            dt.Dispose();
        }

        private void VentasCounter()
        {
            int annio = Convert.ToInt32(ddlAnnio.SelectedItem.Value.ToString());
            int mes = Convert.ToInt32(ddlMes.SelectedItem.Value.ToString());
            string strQuery = "";

            if (ddlPlanes.SelectedValue.ToString() == "0")
            {
                strQuery = @"SELECT ppa.valor 
                    FROM PagosPlanAfiliado ppa 
                    INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
                    WHERE ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17) 
	                    AND (ppa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @") 
	                    AND MONTH(ppa.fechaHoraPago) = " + mes + @" 
                        AND YEAR(ppa.fechaHoraPago) = " + annio + @" 
                        AND MONTH(ap.FechaInicioPlan) IN (" + mes + @");";
            }
            else
            {
                strQuery = @"SELECT ppa.valor 
                    FROM PagosPlanAfiliado ppa 
                    INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
                    WHERE ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17) 
	                    AND (ppa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @") 
	                    AND MONTH(ppa.fechaHoraPago) = " + mes + @" 
                        AND YEAR(ppa.fechaHoraPago) = " + annio + @" 
                        AND MONTH(ap.FechaInicioPlan) IN (" + mes + @") 
                        AND ap.idPlan = " + ddlPlanes.SelectedValue.ToString();
            }

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            decimal sumatoriaValor = 0;

            if (dt.Rows.Count > 0)
            {
                object suma = dt.Compute("SUM(Valor)", "");
                sumatoriaValor = suma != DBNull.Value ? Convert.ToDecimal(suma) : 0;
            }

            ltCuantos3.Text = "$ " + String.Format("{0:N0}", sumatoriaValor);
            ltRegistros3.Text = dt.Rows.Count.ToString();

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

        private void CargarPlanes()
        {
            ddlPlanes.Items.Clear();
            ListItem li = new ListItem("Todos", "0");
            ddlPlanes.Items.Add(li);
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanesVigentes();

            ddlPlanes.DataSource = dt;
            ddlPlanes.DataBind();
            dt.Dispose();
        }

        private void listaVentas()
        {

            ltMes1.Text = ddlMes.SelectedItem.Text.ToString() + " " + ddlAnnio.SelectedItem.Text.ToString();
            ltMes2.Text = ltMes1.Text;
            ltMes3.Text = ltMes1.Text;
            ltMes4.Text = ltMes1.Text;
            ltMes5.Text = ltMes1.Text;
            

            int annio = Convert.ToInt32(ddlAnnio.SelectedItem.Value.ToString());
            int mes = Convert.ToInt32(ddlMes.SelectedItem.Value.ToString());

            //string query = @"
            //    SELECT  
            //        ppa.*,
            //        a.DocumentoAfiliado,
            //        CONCAT_WS(' ', a.NombreAfiliado, a.ApellidoAfiliado) AS NombreAfiliado,
            //        CONCAT('$', FORMAT(ppa.valor, 2)) AS Valor,
            //        u.NombreUsuario AS Usuario,
            //        cv.NombreCanalVenta AS CanalVenta, NombreMedioPago, p.NombrePlan  
            //    FROM pagosplanafiliado ppa
            //     INNER JOIN afiliadosplanes ap ON ap.idAfiliadoPlan = ppa.idAfiliadoPlan
            //     INNER JOIN afiliados a ON a.idAfiliado = ap.idAfiliado    
            //     INNER JOIN usuarios u ON u.idUsuario = ppa.idUsuario  
            //     INNER JOIN empleados e ON e.DocumentoEmpleado = u.idEmpleado
            //     INNER JOIN canalesventa cv ON cv.idCanalVenta = e.idCanalVenta 
            //     INNER JOIN mediosdepago mp ON mp.idMedioPago = ppa.idMedioPago 
            //     INNER JOIN planes p ON p.idPlan = ap.idPlan 
            //    WHERE (ppa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @")
            // AND DATE(ppa.FechaHoraPago) BETWEEN '" + txbFechaIni.Value.ToString() + @"' 
            //        AND '" + txbFechaFin.Value.ToString() + @"' 
            // AND ap.idPlan IN (1,17,18,19) 
            //    AND MONTH(ap.FechaInicioPlan) = MONTH('" + txbFechaIni.Value.ToString() + @"') 
            //    AND YEAR(ap.FechaInicioPlan) = YEAR('" + txbFechaIni.Value.ToString() + @"') 
            //    ORDER BY ppa.idPago DESC;";

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
                AND (ppa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @")
                AND ap.idPlan IN (1, 17) 
                AND MONTH(ppa.fechaHoraPago) = " + mes + @" 
                AND YEAR(ppa.fechaHoraPago) = " + annio + @" 
                AND MONTH(ap.FechaInicioPlan) IN (" + mes + @") 
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
                AND (ppa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @")
                AND ap.idPlan IN (18,19) 
                AND MONTH(ppa.fechaHoraPago) = " + mes + @" 
                AND YEAR(ppa.fechaHoraPago) = " + annio + @" 
                AND MONTH(ap.FechaInicioPlan) IN (" + mes + @") 
                ORDER BY idPago DESC";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(query);
            rpPagos.DataSource = dt;
            rpPagos.DataBind();

            decimal sumatoriaValor = 0;

            if (dt.Rows.Count > 0)
            {
                object suma = dt.Compute("SUM(Valor)", "");
                sumatoriaValor = suma != DBNull.Value ? Convert.ToDecimal(suma) : 0;
            }

            ltCuantos1.Text = "$ " + String.Format("{0:N0}", sumatoriaValor);
            ltRegistros1.Text = dt.Rows.Count.ToString();
            dt.Dispose();

            //CrearGrafico1(txbFechaIni.Value.ToString());
            //CrearGrafico2(txbFechaIni.Value.ToString());
            //CrearGrafico3(txbFechaIni.Value.ToString());
            //CrearGrafico4(txbFechaIni.Value.ToString());
            //CrearGrafico5(txbFechaIni.Value.ToString());
            //CrearGrafico6(txbFechaIni.Value.ToString());

            CrearGrafico1(ddlMes.SelectedItem.Value);
            CrearGrafico2(ddlMes.SelectedItem.Value);
            CrearGrafico3(ddlMes.SelectedItem.Value);
            CrearGrafico4(ddlMes.SelectedItem.Value);
            CrearGrafico5(ddlMes.SelectedItem.Value);
            CrearGrafico6(ddlMes.SelectedItem.Value);
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
                WHERE ((ppa.idUsuario = 156 AND ap.idPlan IN (18,19)) OR (ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17))) 
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
                WHERE ((ppa.idUsuario = 156 AND ap.idPlan IN (18,19)) OR (ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17))) 
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
                WHERE ((ppa.idUsuario = 156 AND ap.idPlan IN (18,19)) OR (ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17))) 
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
                WHERE ((ppa.idUsuario = 156 AND ap.idPlan IN (18,19)) OR (ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17))) 
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
                WHERE ((ppa.idUsuario = 156 AND ap.idPlan IN (18,19)) OR (ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17))) 
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
                WHERE ((ppa.idUsuario = 156 AND ap.idPlan IN (18,19)) OR (ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17))) 
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
            VentasCounter();
            VentasWeb();
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