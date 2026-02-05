using System;
using System.Data;
using System.Globalization;

namespace fpWebApp
{
    public partial class agendaespecialista : System.Web.UI.Page
    {
        private string _strEventos;
        protected string strEventos { get { return this._strEventos; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Agenda especialista");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        CargarAgenda();
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        CargarAgenda();
                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        //btnAsignar.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("logout");
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

                        if(dt.Rows[i]["Genero"].ToString() == "Masculino")
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
                        _strEventos += "title: `👨‍⚕ Disponible \r\nSede: " + dt.Rows[i]["NombreSede"].ToString() + "`,\r\n";
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