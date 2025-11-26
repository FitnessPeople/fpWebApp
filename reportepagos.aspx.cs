using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using NPOI.SS.Formula.Functions;

namespace fpWebApp
{
    public partial class reportepagos : System.Web.UI.Page
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
                    ValidarPermisos("Ingresos");
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
                            txbFechaIni.Attributes.Add("type", "date");
                            txbFechaIni.Value = DateTime.Now.ToString("yyyy-MM-01").ToString();
                            txbFechaFin.Attributes.Add("type", "date");
                            txbFechaFin.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                            CargarPlanes();
                        }

                        //listaTransacciones();
                        listaTransaccionesPorFecha(
                            Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()),
                            Convert.ToInt32(ddlPlanes.SelectedValue.ToString()),
                            txbFechaIni.Value.ToString(),
                            txbFechaFin.Value.ToString());
                        VentasWeb();
                        VentasCounter();
                    }
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

        private void CargarPlanes()
        {
            ddlPlanes.Items.Clear();
            //ListItem li = new ListItem("Todos los planes", "0");
            //ddlPlanes.Items.Add(li);
            clasesglobales cg = new clasesglobales();
            //DataTable dt = cg.ConsultarPlanesVigentes();
            string strQuery = @"
                SELECT 
                    CASE 
                        WHEN DebitoAutomatico = 1 THEN 'Débito automático'
                        ELSE NombrePlan
                    END AS NombrePlanAgrupado,
                    COUNT(*) AS Cantidad, 
                    CASE 
                        WHEN DebitoAutomatico = 1 THEN 0
                        ELSE idPlan
                    END AS idPlanAgrupado 
                FROM planes 
                WHERE (DATEDIFF(FechaFinal, CURDATE()) >= 0 OR Permanente = 1) 
                GROUP BY NombrePlanAgrupado, idPlanAgrupado;";
            DataTable dt = cg.TraerDatos(strQuery);

            ddlPlanes.DataSource = dt;
            ddlPlanes.DataBind();
            dt.Dispose();
        }

        //private void listaTransacciones()
        //{
        //    decimal valorTotal = 0;
        //    int totalRegistros = 0;
        //    clasesglobales cg = new clasesglobales();
        //    DataTable dt = cg.ConsultarPagosRecientes(out valorTotal, out totalRegistros);
        //    rpPagos.DataSource = dt;
        //    rpPagos.DataBind();
        //    dt.Dispose();

        //    ltCuantos1.Text = "$ " + String.Format("{0:N0}", valorTotal);
        //    ltRegistros1.Text = totalRegistros.ToString();
        //}

        private void listaTransaccionesPorFecha(int tipoPago, int idPlan, string fechaIni, string fechaFin)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosPorTipo(Convert.ToInt32(Session["idCanalVenta"].ToString()), tipoPago, idPlan, fechaIni, fechaFin, out decimal valorTotal);
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

            CrearGrafico1(fechaIni);
            CrearGrafico2(fechaIni);
            CrearGrafico3(fechaIni);
            CrearGrafico4(fechaIni);
            CrearGrafico5(fechaIni);
            CrearGrafico6(fechaIni);
        }

        private void CrearGrafico1(string fechaIni)
        {
            //Comparativo de Pagos y Cantidad Diario
            clasesglobales cg = new clasesglobales();
            int anio = Convert.ToDateTime(fechaIni).Year;
            int mes = Convert.ToDateTime(fechaIni).Month;

            string query = "";
            if (Session["idSede"].ToString() != "11")
            {
                query = @"
                SELECT 
                    DATE(FechaHoraPago) AS dia,
                    COUNT(*) AS cuantos,
                    SUM(valor) AS sumatoria
                FROM pagosplanafiliado
                WHERE YEAR(FechaHoraPago) = " + anio.ToString() + @" 
                    AND MONTH(FechaHoraPago) = " + mes.ToString() + @" 
                    AND idCanalVenta = " + Convert.ToInt32(Session["idCanalVenta"].ToString()) + @" 
                GROUP BY DATE(FechaHoraPago) 
                ORDER BY dia;";
            }
            else
            {
                query = @"
                SELECT 
                    DATE(FechaHoraPago) AS dia,
                    COUNT(*) AS cuantos,
                    SUM(valor) AS sumatoria
                FROM pagosplanafiliado
                WHERE YEAR(FechaHoraPago) = " + anio.ToString() + @" 
                    AND MONTH(FechaHoraPago) = " + mes.ToString() + @" 
                GROUP BY DATE(FechaHoraPago) 
                ORDER BY dia;";
            }

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
            int anio = Convert.ToDateTime(fechaIni).Year;
            int mes = Convert.ToDateTime(fechaIni).Month;
            string query = "";

            if (Session["idSede"].ToString() != "11")
            {
                query = @"
                SELECT 
                    ppa.idUsuario, u.NombreUsuario, 
                    COUNT(*) AS cuantos,
                    SUM(valor) AS sumatoria
                FROM pagosplanafiliado ppa, usuarios u 
                WHERE YEAR(FechaHoraPago) = " + anio.ToString() + @" 
                    AND MONTH(FechaHoraPago) = " + mes.ToString() + @" 
                    AND ppa.idUsuario = u.idUsuario 
                    AND idCanalVenta = " + Convert.ToInt32(Session["idCanalVenta"].ToString()) + @" 
                GROUP BY ppa.idUsuario 
                ORDER BY ppa.idUsuario;";
            }
            else
            {
                query = @"
                SELECT 
                    ppa.idUsuario, u.NombreUsuario, 
                    COUNT(*) AS cuantos,
                    SUM(valor) AS sumatoria
                FROM pagosplanafiliado ppa, usuarios u 
                WHERE YEAR(FechaHoraPago) = " + anio.ToString() + @" 
                    AND MONTH(FechaHoraPago) = " + mes.ToString() + @" 
                    AND ppa.idUsuario = u.idUsuario 
                GROUP BY ppa.idUsuario 
                ORDER BY ppa.idUsuario;";
            }

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
            int anio = Convert.ToDateTime(fechaIni).Year;
            int mes = Convert.ToDateTime(fechaIni).Month;

            string query = "";

            if (Session["idSede"].ToString() != "11")
            {
                query = @"
                SELECT 
                    ppa.idCanalVenta, cv.NombreCanalVenta, 
                    COUNT(*) AS cuantos,
                    SUM(valor) AS sumatoria
                FROM pagosplanafiliado ppa, CanalesVenta cv  
                WHERE YEAR(FechaHoraPago) = " + anio.ToString() + @" 
                    AND MONTH(FechaHoraPago) = " + mes.ToString() + @" 
                    AND idCanalVenta = " + Convert.ToInt32(Session["idCanalVenta"].ToString()) + @" 
                    AND ppa.idCanalVenta = cv.idCanalVenta 
                GROUP BY ppa.idCanalVenta 
                ORDER BY ppa.idCanalVenta;";
            }
            else
            {
                query = @"
                SELECT 
                    ppa.idCanalVenta, cv.NombreCanalVenta, 
                    COUNT(*) AS cuantos,
                    SUM(valor) AS sumatoria
                FROM pagosplanafiliado ppa, CanalesVenta cv  
                WHERE YEAR(FechaHoraPago) = " + anio.ToString() + @" 
                    AND MONTH(FechaHoraPago) = " + mes.ToString() + @" 
                    AND ppa.idCanalVenta = cv.idCanalVenta 
                GROUP BY ppa.idCanalVenta 
                ORDER BY ppa.idCanalVenta;";
            }

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
            int anio = Convert.ToDateTime(fechaIni).Year;
            int mes = Convert.ToDateTime(fechaIni).Month;

            string query = "";

            if (Session["idSede"].ToString() != "11")
            {
                query = @"
                SELECT 
                    Banco, 
                    COUNT(*) AS cuantos,
                    SUM(valor) AS sumatoria
                FROM pagosplanafiliado 
                WHERE YEAR(FechaHoraPago) = " + anio.ToString() + @" 
                    AND MONTH(FechaHoraPago) = " + mes.ToString() + @" 
                    AND idCanalVenta = " + Convert.ToInt32(Session["idCanalVenta"].ToString()) + @" 
                GROUP BY Banco 
                ORDER BY Banco;";
            }
            else
            {
                query = @"
                SELECT 
                    Banco, 
                    COUNT(*) AS cuantos,
                    SUM(valor) AS sumatoria
                FROM pagosplanafiliado 
                WHERE YEAR(FechaHoraPago) = " + anio.ToString() + @" 
                    AND MONTH(FechaHoraPago) = " + mes.ToString() + @" 
                GROUP BY Banco 
                ORDER BY Banco;";
            }

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
            int anio = Convert.ToDateTime(fechaIni).Year;
            int mes = Convert.ToDateTime(fechaIni).Month;

            string query = "";

            if (Session["idSede"].ToString() != "11")
            {
                query = @"
                SELECT 
                    ppa.idMedioPago, mp.NombreMedioPago, 
                    COUNT(*) AS cuantos,
                    SUM(valor) AS sumatoria
                FROM pagosplanafiliado ppa, MediosdePago mp 
                WHERE YEAR(FechaHoraPago) = " + anio.ToString() + @" 
                    AND MONTH(FechaHoraPago) = " + mes.ToString() + @" 
                    AND idCanalVenta = " + Convert.ToInt32(Session["idCanalVenta"].ToString()) + @" 
                    AND ppa.idMedioPago = mp.idMedioPago 
                GROUP BY ppa.idMedioPago 
                ORDER BY ppa.idMedioPago;";
            }
            else
            {
                query = @"
                SELECT 
                    ppa.idMedioPago, mp.NombreMedioPago, 
                    COUNT(*) AS cuantos,
                    SUM(valor) AS sumatoria
                FROM pagosplanafiliado ppa, MediosdePago mp 
                WHERE YEAR(FechaHoraPago) = " + anio.ToString() + @" 
                    AND MONTH(FechaHoraPago) = " + mes.ToString() + @" 
                    AND ppa.idMedioPago = mp.idMedioPago 
                GROUP BY ppa.idMedioPago 
                ORDER BY ppa.idMedioPago;";
            }

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
            int anio = Convert.ToDateTime(fechaIni).Year;
            int mes = Convert.ToDateTime(fechaIni).Month;

            string query = "";

            if (Session["idSede"].ToString() != "11")
            {
                query = @"
                SELECT 
                    p.NombrePlan, 
                    COUNT(*) AS cuantos,
                    SUM(ppa.valor) AS sumatoria
                FROM pagosplanafiliado ppa 
                INNER JOIN afiliadosplanes ap ON ap.idAfiliadoPlan = ppa.idAfiliadoPlan 
                INNER JOIN planes p ON p.idPlan = ap.idPlan 
                WHERE YEAR(FechaHoraPago) = " + anio.ToString() + @" 
                    AND MONTH(FechaHoraPago) = " + mes.ToString() + @" 
                    AND idCanalVenta = " + Convert.ToInt32(Session["idCanalVenta"].ToString()) + @" 
                GROUP BY p.NombrePlan 
                ORDER BY p.NombrePlan;";
            }
            else
            {
                query = @"
                SELECT 
                    p.NombrePlan, 
                    COUNT(*) AS cuantos,
                    SUM(ppa.valor) AS sumatoria
                FROM pagosplanafiliado ppa 
                INNER JOIN afiliadosplanes ap ON ap.idAfiliadoPlan = ppa.idAfiliadoPlan 
                INNER JOIN planes p ON p.idPlan = ap.idPlan 
                WHERE YEAR(FechaHoraPago) = " + anio.ToString() + @" 
                    AND MONTH(FechaHoraPago) = " + mes.ToString() + @" 
                GROUP BY p.NombrePlan 
                ORDER BY p.NombrePlan;";
            }

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

        private void VentasWeb()
        {
            string strQuery = "";

            if (ddlPlanes.SelectedValue.ToString() == "0")
            {
                strQuery = @"SELECT pa.Valor 
                FROM pagosplanafiliado pa
                    INNER JOIN afiliadosplanes ap ON ap.idAfiliadoPlan = pa.idAfiliadoPlan
                    INNER JOIN afiliados a ON a.idAfiliado = ap.idAfiliado    
                    INNER JOIN usuarios u ON u.idUsuario = pa.idUsuario  
                    INNER JOIN mediosdepago mp ON mp.idMedioPago = pa.idMedioPago 
                    INNER JOIN planes p ON p.idPlan = ap.idPlan 
                WHERE pa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @" 
                AND DATE(pa.FechaHoraPago) 
                BETWEEN IFNULL(NULLIF('" + txbFechaIni.Value.ToString() + @"', ''), DATE_FORMAT(CURDATE(), '%Y-%m-01')) 
                AND IFNULL(NULLIF('" + txbFechaFin.Value.ToString() + @"', ''), CURDATE()) 
                AND u.idUsuario = 156";
            }
            else
            {
                strQuery = @"SELECT pa.Valor 
                FROM pagosplanafiliado pa
                    INNER JOIN afiliadosplanes ap ON ap.idAfiliadoPlan = pa.idAfiliadoPlan
                    INNER JOIN afiliados a ON a.idAfiliado = ap.idAfiliado    
                    INNER JOIN usuarios u ON u.idUsuario = pa.idUsuario  
                    INNER JOIN mediosdepago mp ON mp.idMedioPago = pa.idMedioPago 
                    INNER JOIN planes p ON p.idPlan = ap.idPlan 
                WHERE pa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @" 
                AND DATE(pa.FechaHoraPago) 
                BETWEEN IFNULL(NULLIF('" + txbFechaIni.Value.ToString() + @"', ''), DATE_FORMAT(CURDATE(), '%Y-%m-01')) 
                AND IFNULL(NULLIF('" + txbFechaFin.Value.ToString() + @"', ''), CURDATE()) 
                AND u.idUsuario = 156 
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
            string strQuery = "";

            if (ddlPlanes.SelectedValue.ToString() == "0")
            {
                strQuery = @"SELECT pa.Valor 
                FROM pagosplanafiliado pa
                    INNER JOIN afiliadosplanes ap ON ap.idAfiliadoPlan = pa.idAfiliadoPlan
                    INNER JOIN afiliados a ON a.idAfiliado = ap.idAfiliado    
                    INNER JOIN usuarios u ON u.idUsuario = pa.idUsuario  
                    INNER JOIN mediosdepago mp ON mp.idMedioPago = pa.idMedioPago 
                    INNER JOIN planes p ON p.idPlan = ap.idPlan 
                WHERE pa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @" 
                AND DATE(pa.FechaHoraPago) 
                BETWEEN IFNULL(NULLIF('" + txbFechaIni.Value.ToString() + @"', ''), DATE_FORMAT(CURDATE(), '%Y-%m-01')) 
                AND IFNULL(NULLIF('" + txbFechaFin.Value.ToString() + @"', ''), CURDATE()) 
                AND u.idUsuario = 152";
            }
            else
            {
                strQuery = @"SELECT pa.Valor 
                FROM pagosplanafiliado pa
                    INNER JOIN afiliadosplanes ap ON ap.idAfiliadoPlan = pa.idAfiliadoPlan
                    INNER JOIN afiliados a ON a.idAfiliado = ap.idAfiliado    
                    INNER JOIN usuarios u ON u.idUsuario = pa.idUsuario  
                    INNER JOIN mediosdepago mp ON mp.idMedioPago = pa.idMedioPago 
                    INNER JOIN planes p ON p.idPlan = ap.idPlan 
                WHERE pa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @" 
                AND DATE(pa.FechaHoraPago) 
                BETWEEN IFNULL(NULLIF('" + txbFechaIni.Value.ToString() + @"', ''), DATE_FORMAT(CURDATE(), '%Y-%m-01')) 
                AND IFNULL(NULLIF('" + txbFechaFin.Value.ToString() + @"', ''), CURDATE()) 
                AND u.idUsuario = 152 
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

        private string listarDetalle(int idAfiliadoPlan)
        {
            string parametro = string.Empty;
            string nomAfiliado = string.Empty;
            string mensaje = string.Empty;
            string tipoPago = string.Empty;
            StringBuilder sb = new StringBuilder();
            clasesglobales cg = new clasesglobales();
            //int idempresa = 1; //Wompi pruebas 
            int idempresa = 4; //Wompi produccion

            try
            {
                DataTable dt = cg.ConsultarPagosPorId(idAfiliadoPlan);
                tipoPago = dt.Rows[0]["NombreMedioPago"].ToString();


                switch (tipoPago)
                {

                    case "Efectivo":
                        sb.Append("<table class=\"table table-bordered table-striped\">");
                        sb.Append("<tr>");
                        sb.Append("<th>ID</th><th>Documento afiliado</th><th>Nombre afiliado</th><th>Tipo pago</th><th>Valor</th><th>Fecha Cre.</th>");
                        sb.Append("<th>Estado</th><th>Usuario</th><th>Canal de Venta</th>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append($"<td>{dt.Rows[0]["idAfiliadoPlan"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["DocumentoAfiliado"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["NombreAfiliado"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["NombreMedioPago"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["Valor"].ToString()}</td>");
                        sb.Append($"<td>" + String.Format("{0:dd MMM yyyy}", Convert.ToDateTime(dt.Rows[0]["FechaHoraPago"].ToString())) + "</td>");
                        sb.Append($"<td>{"Aprobado"}</td>");
                        sb.Append($"<td>{dt.Rows[0]["Usuario"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["CanalVenta"].ToString()}</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        break;
                    case "Transferencia":
                        sb.Append("<table class=\"table table-bordered table-striped\">");
                        sb.Append("<tr>");
                        sb.Append("<th>ID</th><th>Documento afiliado</th><th>Nombre afiliado</th><th>Tipo pago</th><th>Valor</th><th>Fecha Cre.</th>");
                        sb.Append("<th>Banco</th><th>Estado</th><th>Usuario</th><th>Canal de Venta</th>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append($"<td>{dt.Rows[0]["idAfiliadoPlan"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["DocumentoAfiliado"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["NombreAfiliado"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["NombreMedioPago"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["Valor"].ToString()}</td>");
                        sb.Append($"<td>" + String.Format("{0:dd MMM yyyy}", Convert.ToDateTime(dt.Rows[0]["FechaHoraPago"].ToString())) + "</td>");
                        sb.Append($"<td>{dt.Rows[0]["Banco"].ToString()}</td>");
                        sb.Append($"<td>{"Aprobado"}</td>");
                        sb.Append($"<td>{dt.Rows[0]["Usuario"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["CanalVenta"].ToString()}</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");

                        break;
                    case "Datafono":

                        break;
                    case "Pago en línea":

                        DataTable dti = cg.ConsultarUrl(idempresa);

                        if (dt.Rows.Count > 0)
                        {
                            //parametro = dt.Rows[0]["IdReferencia"].ToString();
                            parametro = dt.Rows[0]["DataIdTransaction"].ToString();
                            nomAfiliado = dt.Rows[0]["NombreAfiliado"].ToString();
                        }

                        string url = dti.Rows[0]["urlTest"].ToString() + "/transactions/" + parametro;
                        string[] respuesta = cg.EnviarPeticionGet(url, idempresa.ToString(), out mensaje);
                        JToken token = JToken.Parse(respuesta[0]);
                        string prettyJson = token.ToString(Formatting.Indented);

                        if (mensaje == "Ok") //Verifica respuesta ok
                        {
                            JObject jsonData = JObject.Parse(prettyJson);

                            List<pagoswompidet> listaPagos = new List<pagoswompidet>
                        {
                            new pagoswompidet
                            {
                                Id = jsonData["data"]["id"]?.ToString(),
                                FechaCreacion = jsonData["data"]["created_at"]?.ToString(),
                                FechaFinalizacion = jsonData["data"]["finalized_at"]?.ToString(),
                                Valor = ((jsonData["data"]["amount_in_cents"]?.Value<int>() ?? 0) / 100).ToString("N0") + " " + jsonData["data"]["currency"]?.ToString(),
                                Moneda = jsonData["data"]["currency"]?.ToString(),
                                MetodoPago = jsonData["data"]["payment_method_type"]?.ToString(),
                                Estado = jsonData["data"]["status"]?.ToString(),
                                Referencia = jsonData["data"]["reference"]?.ToString(),
                                NombreTarjeta = jsonData["data"]["payment_method"]["extra"]["name"]?.ToString(),
                                UltimosDigitos = jsonData["data"]["payment_method"]["extra"]["last_four"]?.ToString(),
                                MarcaTarjeta = jsonData["data"]["payment_method"]["extra"]["brand"]?.ToString(),
                                TipoTarjeta = jsonData["data"]["payment_method"]["extra"]["card_type"]?.ToString(),
                                NombreComercio = jsonData["data"]["merchant"]["name"]?.ToString(),
                                ContactoComercio = jsonData["data"]["merchant"]["contact_name"]?.ToString(),
                                TelefonoComercio = jsonData["data"]["merchant"]["phone_number"]?.ToString(),
                                URLRedireccion = jsonData["data"]["redirect_url"]?.ToString(),
                                PaymentLinkId = jsonData["data"]["payment_link_id"]?.ToString(),
                                PublicKeyComercio = jsonData["data"]["merchant"]["public_key"]?.ToString(),
                                EmailComercio = jsonData["data"]["merchant"]["email"]?.ToString() }
                                //Estado3DS = jsonData["data"]["payment_method"]["extra"]["three_ds_auth"]["three_ds_auth"]["current_step_status"]?.ToString()                                }
                            };

                            sb.Append("<table class=\"table table-bordered table-striped\">");
                            sb.Append("<tr>");
                            sb.Append("<th>ID</th><th>Afiliado</th><th>Fecha Cre.</th><th>Fecha Fin.</th><th>Valor</th>");
                            sb.Append("<th>Método Pago</th><th>Estado</th><th>Referencia</th><th>Usuario</th><th>Canal de Venta</th>");
                            sb.Append("</tr>");

                            foreach (var pago in listaPagos)
                            {
                                sb.Append("<tr>");
                                sb.Append($"<td>{pago.Id}</td>");
                                sb.Append($"<td>{nomAfiliado}</td>");
                                sb.Append($"<td>" + String.Format("{0:dd MMM yyyy}", Convert.ToDateTime(pago.FechaCreacion)) + "</td>");
                                sb.Append($"<td>" + String.Format("{0:dd MMM yyyy}", Convert.ToDateTime(pago.FechaFinalizacion)) + "</td>");
                                sb.Append($"<td>{pago.Valor}</td>");
                                sb.Append($"<td>{pago.MetodoPago}</td>");
                                sb.Append($"<td>{pago.Estado}</td>");
                                sb.Append($"<td>{pago.Referencia}</td>");
                                sb.Append($"<td>{dt.Rows[0]["Usuario"].ToString()}</td>");
                                sb.Append($"<td>{dt.Rows[0]["CanalVenta"].ToString()}</td>");
                                sb.Append("</tr>");
                            }

                            sb.Append("</table>");

                            //return sb.ToString();
                        }
                        else
                        {
                            return prettyJson;
                        }
                        break;
                    default:
                        prettyJson = JsonConvert.SerializeObject(new { error = "Sin datos del tipo de pago: " });
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Error");
            }

            return sb.ToString();
        }

        protected void rpPagos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnVer = (HtmlAnchor)e.Item.FindControl("btnVer");
                    btnVer.Attributes.Add("href", "reportepagoswompi?verid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnVer.Visible = false;
                }
            }

        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Arreglar datos quemados
                string consultaSQL = @"SELECT a.DocumentoAfiliado AS 'Documento de Afiliado', CONCAT_WS(' ', a.NombreAfiliado, a.ApellidoAfiliado) AS 'Nombre de Afiliado', 
                        pa.Valor AS 'Valor', pa.idReferencia AS 'Nro. de Referencia', mp.NombreMedioPago AS 'Tipo de Pago', 
                        pa.Banco AS 'Entidad Bancaría', pa.FechaHoraPago AS 'Fecha de Pago', pa.estadoPago AS 'Estado', 
                        u.NombreUsuario AS 'Nombre de Usuario', cv.NombreCanalVenta AS 'Canal de Venta' 
                        FROM pagosplanafiliado pa
                        INNER JOIN afiliadosplanes ap ON ap.idAfiliadoPlan = pa.idAfiliadoPlan
                        INNER JOIN afiliados a ON a.idAfiliado = ap.idAfiliado    
                        INNER JOIN usuarios u ON u.idUsuario = pa.idUsuario  
                        INNER JOIN empleados e ON e.DocumentoEmpleado = u.idEmpleado
                        INNER JOIN canalesventa cv ON cv.idCanalVenta = e.idCanalVenta 
                        INNER JOIN mediosdepago mp ON mp.idMedioPago = pa.idMedioPago 
                        WHERE DATE(pa.FechaHoraPago) 
                            BETWEEN '"  + txbFechaIni.Value.ToString() + @"' 
                            AND '"  + txbFechaFin.Value.ToString() + @"'  
                        ORDER BY pa.FechaHoraPago DESC;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
                string nombreArchivo = $"ReportesPagos_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    cg.ExportarExcel(dt, nombreArchivo);
                }
                else
                {
                    Response.Write("<script>alert('No existen registros para esta consulta');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
            }


            //try
            //{
            //    // se instaló NPOI en nuguet
            //    clasesglobales cg = new clasesglobales();
            //    DataTable dt = cg.ConsultarPagosPorTipo(ddlTipoPago.SelectedValue.ToString(), txbFechaIni.Value.ToString(), txbFechaFin.Value.ToString(), out decimal valortotal);
            //    if (dt.Rows.Count == 0)
            //    {
            //        dt = cg.ConsultarPagosRecientes(out decimal valorTotal, out int totalRegistros);

            //    }
            //    string nombreArchivo = $"{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

            //    if (dt.Rows.Count > 0)
            //    {
            //        IWorkbook workbook = new XSSFWorkbook();
            //        ISheet sheet = workbook.CreateSheet("Pagos");

            //        IRow headerRow = sheet.CreateRow(0);
            //        for (int i = 0; i < dt.Columns.Count; i++)
            //        {
            //            ICell cell = headerRow.CreateCell(i);
            //            cell.SetCellValue(dt.Columns[i].ColumnName);
            //        }

            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            IRow row = sheet.CreateRow(i + 1);
            //            for (int j = 0; j < dt.Columns.Count; j++)
            //            {
            //                object value = dt.Rows[i][j];
            //                row.CreateCell(j).SetCellValue(value != DBNull.Value ? value.ToString() : "");
            //            }
            //        }

            //        for (int i = 0; i < dt.Columns.Count; i++)
            //        {
            //            sheet.AutoSizeColumn(i);
            //        }

            //        using (MemoryStream memoryStream = new MemoryStream())
            //        {
            //            workbook.Write(memoryStream);
            //            workbook.Close();

            //            byte[] byteArray = memoryStream.ToArray();

            //            Response.Clear();
            //            Response.Buffer = true;
            //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //            Response.AddHeader("Content-Disposition", $"attachment; filename={nombreArchivo}.xlsx");
            //            Response.BinaryWrite(byteArray);
            //            Response.Flush();
            //            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
            //}
        }


        protected void btnDetalle_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "mostrarDetalle")
            {
                int idAfiliadoPlan = int.Parse(e.CommandArgument.ToString());

                Literal ltDetalleModal = (Literal)Page.FindControl("ltDetalleModal");

                if (ltDetalleModal != null)
                {
                    ltDetalleModal.Text = listarDetalle(idAfiliadoPlan);
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal",
                   "setTimeout(function() { $('#ModalDetalle').modal('show'); }, 500);", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //if (rblValor.SelectedItem != null)
            //{
                listaTransaccionesPorFecha(
                    Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()),
                    Convert.ToInt32(ddlPlanes.SelectedValue.ToString()),
                    txbFechaIni.Value.ToString(),
                    txbFechaFin.Value.ToString());
                VentasCounter();
                VentasWeb();
            //}
            //else
            //{
            //    listaTransaccionesPorFecha(
            //        Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()),
            //        0,
            //        txbFechaIni.Value.ToString(),
            //        txbFechaFin.Value.ToString());
            //    VentasCounter();
            //    VentasWeb();
            //}
        }
    }
}