using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace fpWebApp
{
    public partial class obtenermetascomerciales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DateTime hoy = DateTime.Today;
            //int mes = hoy.Month;
            //int anio = hoy.Year;
            string filtro = Request.QueryString["filtro"];

            if (filtro == "" || filtro is null)
            {
                filtro = Session["idCanalVenta"].ToString();
            }

            //string strQuery = "SELECT e.FechaInicio, e.FechaFin, " +
            //    "e.idEstacionalidad, e.Titulo, e.Renderizado, e.Color, e.TodoElDia, e.Mostrar, " +
            //    "(mc.Presupuesto * e.Titulo / 100) AS metaDia, " +
            //    "IF(SUM(ppa.Valor) IS NULL, 0, SUM(ppa.Valor)) AS pagado, mc.Valor  " +
            //    "FROM estacionalidad e " +
            //    "INNER JOIN metascomerciales mc " +
            //    "ON mc.mes = " + Request.QueryString["mes"].ToString() + " " +
            //    "AND mc.annio = " + Request.QueryString["anio"].ToString() + " " +
            //    "AND mc.idCanalVenta = 9 " +
            //    "LEFT JOIN pagosplanafiliado ppa " +
            //    "ON DATE(ppa.FechaHoraPago) = e.FechaInicio " +
            //    "WHERE MONTH(e.FechaInicio) = " + Request.QueryString["mes"].ToString() + " " +
            //    "AND YEAR(e.FechaInicio) = " + Request.QueryString["anio"].ToString() + " " +
            //    "GROUP BY e.FechaInicio, (mc.Valor * e.Titulo / 100), e.Titulo, e.idEstacionalidad, " +
            //    "e.FechaFin, e.Renderizado, e.Color, e.TodoElDia, e.Mostrar, mc.Presupuesto ";

            clasesglobales cg = new clasesglobales();

            DataTable dt = cg.ConsultarEstacionalidadPorDia(Convert.ToInt32(filtro),Convert.ToInt32(Request.QueryString["mes"].ToString()),Convert.ToInt32(Request.QueryString["anio"].ToString()));


            var lista = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                int metaDia = Convert.ToInt32(row["metaDia"]);
                //int pagado = (row["pagado"] == DBNull.Value || row["pagado"] == null) ? 0 : Convert.ToInt32(row["pagado"]);
                int pagado = Convert.ToInt32(row["pagado"]);
                int intDiferencia = Convert.ToInt32(row["metaSedeDia"]) - pagado;
                decimal dblCumplimiento = metaDia > 0 ? ((decimal)pagado / metaDia) * 100 : 0;

                string strColor = "";
                if (dblCumplimiento < 85)
                    strColor = "#ed5565";
                if (dblCumplimiento >= 85 && dblCumplimiento < 95)
                    strColor = "#f8ac59";
                if (dblCumplimiento >= 95)
                    strColor = "#1ab394";

                lista.Add(new
                {
                    id = row["idEstacionalidad"],
                    title = "Meta: $ " + String.Format("{0:N0}", row["metaSedeDia"]),
                    valor = row["Valor"],
                    ventas = Convert.ToInt32(row["pagado"]),
                    description = "Meta: $ " + String.Format("{0:N0}", row["metaSedeDia"]) + "\r\n" +
                    "Ventas: $ " + String.Format("{0:N0}", pagado) + "\r\n" +
                    "Diferencia: $ " + String.Format("{0:N0}", intDiferencia) + "\r\n" +
                    "Cumplimiento: " + String.Format("{0:N0}", dblCumplimiento) + "%",
                    start = row["FechaInicio"],
                    end = row["FechaFin"],
                    rendering = row["Renderizado"],
                    color = strColor,
                    allDay = row["TodoElDia"],
                    backgroundColor = strColor,
                    display = row["Mostrar"],
                });
            }

            string json = JsonConvert.SerializeObject(lista);

            Response.Clear();
            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();
        }
    }
}