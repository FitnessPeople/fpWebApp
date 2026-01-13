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
            CrearGrafico3();
        }

        private void CrearGrafico1()
        {
            clasesglobales cg = new clasesglobales();

            string query = @"
                SELECT 
                    DATE_FORMAT(ppa.FechaHoraPago, '%Y-%m') AS periodo_orden,
                    DATE_FORMAT(ppa.FechaHoraPago, '%Y-%b') AS periodo,
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
                    DATE_FORMAT(ppa.FechaHoraPago, '%Y-%b') AS periodo,
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
        }

        private void CrearGrafico3()
        {
            clasesglobales cg = new clasesglobales();

            string query = @"
                SELECT 
                    DATE_FORMAT(ppa.FechaHoraPago, '%Y-%m') AS periodo_orden,
                    DATE_FORMAT(ppa.FechaHoraPago, '%Y-%b') AS periodo,
                    SUM(ppa.Valor) AS ingresos_totales 
                FROM PagosPlanAfiliado ppa
                INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan
                INNER JOIN Planes p ON ap.idPlan = p.idPlan
                WHERE
                    -- Últimos 6 meses
                    ppa.FechaHoraPago >= DATE_FORMAT(DATE_SUB(CURDATE(), INTERVAL 5 MONTH), '%Y-%m-01')
                    AND ppa.FechaHoraPago <  DATE_FORMAT(DATE_ADD(CURDATE(), INTERVAL 1 MONTH), '%Y-%m-01')
                GROUP BY periodo_orden, periodo
                ORDER BY periodo_orden;";

            DataTable dt = cg.TraerDatos(query);

            if (dt.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                List<int> sumatoria = new List<int>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    nombres.Add(dt.Rows[i]["periodo"].ToString());
                    int sumatorias = Convert.ToInt32(dt.Rows[i]["ingresos_totales"].ToString());
                    sumatoria.Add(sumatorias);
                }

                var serializer = new JavaScriptSerializer();
                string nombresJson = serializer.Serialize(nombres);
                //string cantidadesJson = serializer.Serialize(cantidades);
                string sumatoriaJson = serializer.Serialize(sumatoria);

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "dataChart3",
                    $"var nombres3 = {nombresJson}; var sumatoria3 = {sumatoriaJson};",
                    true
                );
            }

            dt.Dispose();
        }

    }
}