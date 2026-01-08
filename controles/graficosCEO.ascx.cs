using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace fpWebApp.controles
{
    public partial class graficosCEO : System.Web.UI.UserControl
    {
        protected string Grafico1 = "{}";
        protected string Grafico2 = "{}";
        protected string Grafico3 = "{}";
        protected void Page_Load(object sender, EventArgs e)
        {
            CrearGrafico1();
            CrearGrafico2();
        }

        private void CrearGrafico1()
        {
            clasesglobales cg = new clasesglobales();

            string query = @"
                SELECT 
                    DATE_FORMAT(ppa.FechaHoraPago, '%Y-%m') AS periodo_orden,
                    DATE_FORMAT(ppa.FechaHoraPago, '%Y %M') AS periodo,
                    SUM(ppa.Valor) AS ventas_nuevas_da
                FROM PagosPlanAfiliado ppa
                INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan
                INNER JOIN Planes p ON ap.idPlan = p.idPlan
                WHERE
                    ppa.idUsuario NOT IN (156) 
                    AND ap.idPlan IN (1, 17, 20, 21)
                    -- Solo ventas nuevas
                    AND DATE_FORMAT(ppa.FechaHoraPago, '%Y-%m') = DATE_FORMAT(ap.FechaInicioPlan, '%Y-%m')
                    -- Últimos 6 meses
                    AND ppa.FechaHoraPago >= DATE_FORMAT(DATE_SUB(CURDATE(), INTERVAL 5 MONTH), '%Y-%m-01')
                    AND ppa.FechaHoraPago <  DATE_FORMAT(DATE_ADD(CURDATE(), INTERVAL 1 MONTH), '%Y-%m-01')
                GROUP BY periodo_orden, periodo
                ORDER BY periodo_orden;";

            DataTable dt1 = cg.TraerDatos(query);

            query = @"
                SELECT 
                    DATE_FORMAT(ppa.FechaHoraPago, '%Y-%m') AS periodo_orden,
                    DATE_FORMAT(ppa.FechaHoraPago, '%Y %M') AS periodo,
                    SUM(ppa.Valor) AS ventas_nuevas_da
                FROM PagosPlanAfiliado ppa
                INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan
                INNER JOIN Planes p ON ap.idPlan = p.idPlan
                WHERE
                    ppa.idUsuario = 156 
                    AND ap.idPlan IN (18, 19, 20, 21)
                    -- Solo ventas nuevas
                    AND DATE_FORMAT(ppa.FechaHoraPago, '%Y-%m') = DATE_FORMAT(ap.FechaInicioPlan, '%Y-%m')
                    -- Últimos 6 meses
                    AND ppa.FechaHoraPago >= DATE_FORMAT(DATE_SUB(CURDATE(), INTERVAL 5 MONTH), '%Y-%m-01')
                    AND ppa.FechaHoraPago <  DATE_FORMAT(DATE_ADD(CURDATE(), INTERVAL 1 MONTH), '%Y-%m-01')
                GROUP BY periodo_orden, periodo
                ORDER BY periodo_orden;";

            DataTable dt2 = cg.TraerDatos(query);


            if (dt1.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                //List<int> cantidades = new List<int>();
                List<int> sumatoria = new List<int>();

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    nombres.Add(dt1.Rows[i]["periodo"].ToString());
                    int sumatorias = Convert.ToInt32(dt1.Rows[i]["ventas_nuevas_da"].ToString()) + Convert.ToInt32(dt2.Rows[i]["ventas_nuevas_da"].ToString());
                    sumatoria.Add(sumatorias);
                }

                //foreach (DataRow row in dt1.Rows)
                //{
                //    nombres.Add(row["periodo"].ToString());
                //    //cantidades.Add(Convert.ToInt32(row["cuantos"]));
                //    sumatoria.Add(Convert.ToInt32(row["ventas_nuevas_da"]));
                //}

                var serializer = new JavaScriptSerializer();
                string nombresJson = serializer.Serialize(nombres);
                //string cantidadesJson = serializer.Serialize(cantidades);
                string sumatoriaJson = serializer.Serialize(sumatoria);

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "dataChart1",
                    $"var nombres1 = {nombresJson}; var sumatoria1 = {sumatoriaJson};",
                    true
                );
            }

            dt1.Dispose();
            dt2.Dispose();

            //// Convertir los datos a listas para Chart.js
            //var labels = new System.Collections.Generic.List<string>();
            //var ventas_web = new System.Collections.Generic.List<decimal>();
            //var ventas_counter = new System.Collections.Generic.List<decimal>();
            //var cantidad_web = new System.Collections.Generic.List<decimal>();
            //var cantidad_counter = new System.Collections.Generic.List<decimal>();

            //foreach (DataRow row in dt.Rows)
            //{
            //    labels.Add((row["periodo"].ToString()));
            //    ventas_web.Add(Convert.ToDecimal(row["ventas_web"]));
            //    ventas_counter.Add(Convert.ToDecimal(row["ventas_counter"]));
            //    cantidad_web.Add(Convert.ToDecimal(row["cuantos_ventas_web"]));
            //    cantidad_counter.Add(Convert.ToDecimal(row["cuantos_ventas_counter"]));
            //}

            //// Crear objeto para enviar a JS
            //var datos = new
            //{
            //    labels = labels,
            //    ventas_web = ventas_web,
            //    ventas_counter = ventas_counter,
            //    cantidad_web = cantidad_web,
            //    cantidad_counter = cantidad_counter
            //};

            //dt.Dispose();

            //Grafico1 = JsonConvert.SerializeObject(datos);
        }

        private void CrearGrafico2()
        {
            //Comparativo de Ventas y Cantidad por Usuario
            clasesglobales cg = new clasesglobales();
            //int anio = Convert.ToDateTime(fechaIni).Year;
            //int mes = Convert.ToDateTime(fechaIni).Month;

            //int anio = Convert.ToInt32(annio);
            //int mes = Convert.ToInt32(fechaIni);

            string query = @"
                SELECT s.NombreSede, COUNT(a.idAfiliado) AS cuantos 
                FROM afiliados a 
                INNER JOIN sedes s ON s.idSede = a.idSede 
                WHERE EstadoAfiliado = 'Activo' 
                GROUP BY a.idSede 
                ORDER BY s.NombreSede;";

            DataTable dt = cg.TraerDatos(query);

            if (dt.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                List<int> cantidades = new List<int>();
                //List<int> sumatoria = new List<int>();

                foreach (DataRow row in dt.Rows)
                {
                    nombres.Add(row["NombreSede"].ToString());
                    cantidades.Add(Convert.ToInt32(row["cuantos"]));
                    //sumatoria.Add(Convert.ToInt32(row["sumatoria"]));
                }

                var serializer = new JavaScriptSerializer();
                string nombresJson = serializer.Serialize(nombres);
                string cantidadesJson = serializer.Serialize(cantidades);
                //string sumatoriaJson = serializer.Serialize(sumatoria);

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "dataChart2",
                    $"var nombres2 = {nombresJson}; var cantidades2 = {cantidadesJson};",
                    true
                );
            }

            dt.Dispose();

            // Convertir los datos a listas para Chart.js
            //var labels = new System.Collections.Generic.List<string>();
            //var ventas = new System.Collections.Generic.List<decimal>();
            //var cantidad = new System.Collections.Generic.List<decimal>();

            //foreach (DataRow row in dt.Rows)
            //{
            //    labels.Add((row["NombreSede"]).ToString());
            //    //ventas.Add(Convert.ToDecimal(row["sumatoria"]));
            //    cantidad.Add(Convert.ToDecimal(row["cuantos"]));
            //}

            //// Crear objeto para enviar a JS
            //var datos = new
            //{
            //    labels = labels,
            //    ventas = ventas,
            //    cantidad = cantidad
            //};

            //dt.Dispose();

            //Grafico2 = JsonConvert.SerializeObject(datos);
        }

        private void CrearGrafico3(string fechaIni, string annio)
        {
            //Comparativo de Ventas y Cantidad por Canal de Venta
            clasesglobales cg = new clasesglobales();
            //int anio = Convert.ToDateTime(fechaIni).Year;
            //int mes = Convert.ToDateTime(fechaIni).Month;

            int anio = Convert.ToInt32(annio);
            int mes = Convert.ToInt32(fechaIni);

            string query = @"
                SELECT 
                    ppa.idCanalVenta, cv.NombreCanalVenta, 
                    COUNT(*) AS cuantos,
                    SUM(ppa.valor) AS sumatoria
                FROM pagosplanafiliado ppa 
                INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
                INNER JOIN CanalesVenta cv ON ppa.idCanalVenta = cv.idCanalVenta 
                WHERE ((ppa.idUsuario = 156 AND ap.idPlan IN (18,19,20,21)) OR (ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17,20,21))) 
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

    }
}