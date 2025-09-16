using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace fpWebApp
{
    public partial class obtenerestacionalidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string filtro = Request.QueryString["filtro"];

            if (filtro == "" || filtro is null)
            {
                filtro = Session["idCanalVenta"].ToString();
            }

            //string strQuery = "SELECT *, (mc.Valor * e.Titulo / 100) metaDia " +
            //    "FROM estacionalidad e, metascomerciales mc " +
            //    "WHERE mc.idCanalVenta = " + filtro + " " +
            //    "AND mc.mes = " + Request.QueryString["mes"].ToString() + " " +
            //    "AND mc.annio = " + Request.QueryString["anio"].ToString() + " " +
            //    "AND MONTH(e.FechaInicio) = " + Request.QueryString["mes"].ToString() + " " +
            //    "AND YEAR(e.FechaInicio) = " + Request.QueryString["anio"].ToString() + " ";

            string strQuery = "SELECT e.FechaInicio, e.FechaFin, " +
                "e.idEstacionalidad, e.Titulo, e.Renderizado, e.Color, e.TodoElDia, e.Mostrar, " +
                "(mc.Presupuesto * e.Titulo / 100) metaDia, SUM(ppa.Valor) pagado " +
                "FROM estacionalidad e " +
                "INNER JOIN metascomerciales mc " +
                "ON mc.mes = " + Request.QueryString["mes"].ToString() + " " +
                "AND mc.annio = " + Request.QueryString["anio"].ToString() + " " +
                "AND mc.idCanalVenta = 9 " +
                "LEFT JOIN pagosplanafiliado ppa " +
                "ON DATE(ppa.FechaHoraPago) = e.FechaInicio " +
                "WHERE MONTH(e.FechaInicio) = " + Request.QueryString["mes"].ToString() + " " +
                "AND YEAR(e.FechaInicio) = " + Request.QueryString["anio"].ToString() + " " +
                "GROUP BY e.FechaInicio, (mc.Presupuesto * e.Titulo / 100), e.Titulo, e.idEstacionalidad, " +
                "e.FechaFin, e.Renderizado, e.Color, e.TodoElDia, e.Mostrar ";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            var lista = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new
                {
                    id = row["idEstacionalidad"],
                    title = row["Titulo"],
                    start = row["FechaInicio"],
                    end = row["FechaFin"],
                    rendering = row["Renderizado"],
                    color = row["Color"],
                    allDay = row["TodoElDia"],
                    backgroundColor = row["Color"],
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