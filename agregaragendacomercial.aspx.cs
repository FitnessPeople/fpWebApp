using System;
using System.Collections.Generic;
using System.Web.Services;

namespace fpWebApp
{
    public partial class agregaragendacomercial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.HttpMethod == "POST")
            //{
            //    string json = new StreamReader(Request.InputStream).ReadToEnd();
            //    JavaScriptSerializer js = new JavaScriptSerializer();
            //    List<Evento> eventos = js.Deserialize<List<Evento>>(json);

            //    foreach (var evento in eventos)
            //    {
            //        string strQuery = "INSERT INTO agendacomercial (Titulo, FechaInicio, FechaFin, TodoElDia) " +
            //        "VALUES (" + evento.title + ", " + evento.start + ", " + evento.end + ", " + evento.allDay + ")";
            //        clasesglobales cg = new clasesglobales();
            //        cg.TraerDatosStr(strQuery);
            //    }
            //}
        }

        [WebMethod]
        public static int GuardarEventos(List<Evento> eventos)
        {
            //string json = new StreamReader(Request.InputStream).ReadToEnd();
            //JavaScriptSerializer js = new JavaScriptSerializer();
            int guardados = 0;

            foreach (var evento in eventos)
            {
                string strQuery = "INSERT INTO estacionalidad (titulo, fecha_inicio, fecha_fin, todo_el_dia) " +
                "VALUES ('" + evento.title + "', '" + evento.start + "', '" + evento.end + "', " + evento.allDay + ")";
                clasesglobales cg = new clasesglobales();
                cg.TraerDatosStr(strQuery);
            }

            return guardados;
        }

        public class Evento
        {
            public string title { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public bool allDay { get; set; }
        }
    }
}