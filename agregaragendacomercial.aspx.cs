using System;
using System.Collections.Generic;
using System.Web.Services;

namespace fpWebApp
{
    public partial class agregaragendacomercial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [WebMethod]
        public static string GuardarEventos(List<EventoDTO> eventos)
        {
            foreach (var evento in eventos)
            {
                string dtInicio = Convert.ToDateTime(evento.start).ToString("yyyy-MM-dd");
                string dtFin = dtInicio;
                string strQuery = "INSERT INTO estacionalidad (titulo, fecha_inicio, fecha_fin, todo_el_dia) " +
                "VALUES ('" + evento.title + "', '" + dtInicio + "', '" + dtFin + "', " + evento.allDay + ")";
                clasesglobales cg = new clasesglobales();
                cg.TraerDatosStr(strQuery);
            }

            return "Ok";
        }

        public class EventoDTO
        {
            public string id { get; set; }
            public string title { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public bool allDay { get; set; }
        }
    }
}