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

            string strQuery = "SELECT *, (mc.Valor * e.Titulo / 100) metaDia " +
                "FROM estacionalidad e, metascomerciales mc " +
                "WHERE mc.idCanalVenta = " + filtro + " " +
                "AND mc.mes = " + Request.QueryString["mes"].ToString() + " " +
                "AND mc.annio = " + Request.QueryString["anio"].ToString() + " " +
                "AND MONTH(e.FechaInicio) = " + Request.QueryString["mes"].ToString() + " " +
                "AND YEAR(e.FechaInicio) = " + Request.QueryString["anio"].ToString() + " ";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            var lista = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new
                {
                    id = row["idEstacionalidad"],
                    title = row["Titulo"],
                    description = "Meta del día: $ " + String.Format("{0:N0}", row["metaDia"]),
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