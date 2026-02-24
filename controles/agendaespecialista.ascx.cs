using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class agendaespecialista : System.Web.UI.UserControl
    {
        private string _strEventos;
        protected string strEventos { get { return this._strEventos; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarAgenda();
        }

        private void CargarAgenda()
        {
            ltEspecialista.Text = Session["NombreUsuario"].ToString();

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarAgendaPorEspecialista(Convert.ToInt32(Session["idUsuario"].ToString()));

            _strEventos = "events: [\r\n";

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IFormatProvider provider = new CultureInfo("en-US");
                    DateTime dtIni = Convert.ToDateTime(dt.Rows[i]["FechaHoraIni"].ToString(), provider);
                    DateTime dtFin = Convert.ToDateTime(dt.Rows[i]["FechaHoraFin"].ToString(), provider);

                    string strFechaHoraIni = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtIni);
                    string strFechaHoraFin = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtFin);

                    if (dt.Rows[i]["idAfiliado"].ToString() != "")
                    {
                        _strEventos += "{\r\n";
                        _strEventos += "id: '" + dt.Rows[i]["idDisponibilidad"].ToString() + "',\r\n";

                        if (dt.Rows[i]["Genero"].ToString() == "Masculino")
                        {
                            _strEventos += "title: `👨 " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "\r\nSede: " + dt.Rows[i]["NombreSede"].ToString() + "`,\r\n";
                        }
                        if (dt.Rows[i]["Genero"].ToString() == "Femenino")
                        {
                            _strEventos += "title: `👩‍🦰 " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "\r\nSede: " + dt.Rows[i]["NombreSede"].ToString() + "`,\r\n";
                        }
                        if (dt.Rows[i]["Genero"].ToString() == "Otro")
                        {
                            _strEventos += "title: `🤷‍♀️ " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "\r\nSede: " + dt.Rows[i]["NombreSede"].ToString() + "`,\r\n";
                        }

                        _strEventos += "start: '" + strFechaHoraIni + "',\r\n";
                        _strEventos += "end: '" + strFechaHoraFin + "',\r\n";
                        _strEventos += "color: '#F8AC59',\r\n";
                        _strEventos += "url: 'verhistoriaclinica?idAfiliado=" + dt.Rows[i]["idAfiliado"].ToString() + "',\r\n";
                        //_strEventos += "btnAsignar: 'none',\r\n";
                        _strEventos += "allDay: false,\r\n";
                        _strEventos += "},\r\n";
                    }
                    else
                    {
                        _strEventos += "{\r\n";
                        _strEventos += "id: '" + dt.Rows[i]["idDisponibilidad"].ToString() + "',\r\n";
                        //_strEventos += "title: `👨‍⚕️" + dt.Rows[i]["NombreEmpleado"].ToString() + "\r\nSede: " + dt.Rows[i]["NombreSede"].ToString() + "`,\r\n";
                        _strEventos += "title: `👨‍⚕ Disponible \r\nSede: " + dt.Rows[i]["NombreSede"].ToString() + " - " + dt.Rows[i]["NombreConsultorio"].ToString() + "`,\r\n";
                        _strEventos += "start: '" + strFechaHoraIni + "',\r\n";
                        _strEventos += "end: '" + strFechaHoraFin + "',\r\n";
                        _strEventos += "color: '#1ab394',\r\n";
                        //_strEventos += "url: 'historiasclinicas?id=" + dt.Rows[i]["DocumentoAfiliado"].ToString() + "',\r\n";
                        //_strEventos += "btnAsignar: 'none',\r\n";
                        _strEventos += "allDay: false,\r\n";
                        _strEventos += "},\r\n";
                    }
                }
            }

            dt.Dispose();

            AgregarFestivos(_strEventos, "2026");

            _strEventos += "],\r\n";

        }

        private string AgregarFestivos(string eventos, string anho)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarDiasFestivosPorAnnio(Convert.ToInt16(anho));

            _strEventos = eventos;

            foreach (DataRow row in dt.Rows)
            {
                _strEventos += "{\r\n";
                _strEventos += "start: '" + Convert.ToDateTime(row["Fecha"]).ToString("yyyy-MM-ddTHH:mm:ss") + "',\r\n";
                _strEventos += "end: '" + Convert.ToDateTime(row["Fecha"]).ToString("yyyy-MM-ddTHH:mm:ss") + "',\r\n";
                _strEventos += "title: '" + row["Titulo"].ToString() + "',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";
            }

            return eventos;
        }
    }
}