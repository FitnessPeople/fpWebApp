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
            string strQuery = "SELECT * FROM estacionalidad";

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
                    display = row["Mostrar"]
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