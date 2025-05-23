using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
	public partial class agendagympass : System.Web.UI.Page
	{
        private string _strEventos;
        protected string strEventos { get { return this._strEventos; } }
        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Agenda Gym Pass");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        CargarSedes();

                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {

                    }
                    if (ViewState["Consulta"].ToString() == "1" || ViewState["CrearModificar"].ToString() == "1")
                    {
                        int idSedeUsuario = Convert.ToInt32(Session["idSede"]);

                        divFiltroSede.Visible = (idSedeUsuario == 11);
                        CargarSedes();
                        CargarEstados();
                        CargarAgenda();
                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        btnAsistencia.Visible = true;
                        btnCancelar.Visible = true;
                        btnEliminar.Visible = true;
                        btnNoAsistencia.Visible = true;
                        btnAsistencia.Visible = true;
                        btnCancelar.Visible = true;
                    }
                    //indicadores01.Visible = false;
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
            int idSedeUsuario = Convert.ToInt32(Session["idSede"]);

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarGymPassAgendaPorSede(int.Parse(ddlSedes.SelectedItem.Value.ToString()));

            int? idSede;
            string estado = ddlEstados.SelectedValue == "Todos" ? null : ddlEstados.SelectedValue;

            if (idSedeUsuario == 11)
            {
                idSede = ddlSedes.SelectedItem.Text == "Todas" || ddlSedes.SelectedItem.Value == "0"
                    ? (int?)null
                    : Convert.ToInt32(ddlSedes.SelectedItem.Value);
            }
            else
            {
                idSede = idSedeUsuario;
            }

            ViewState["FiltroSede"] = idSede;
            ViewState["FiltroEstado"] = estado;

            DataTable dt = cg.ConsultarGymPassAgenda(idSede, estado);

            _strEventos = "events: [\r\n";

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IFormatProvider provider = new CultureInfo("en-US");
                    DateTime dtFecha = Convert.ToDateTime(dt.Rows[i]["FechaHora"], provider);
                    //DateTime dtFin = Convert.ToDateTime(dt.Rows[i]["FechaHoraFin"].ToString(), provider);
                    DateTime dtIni = Convert.ToDateTime(dt.Rows[i]["FechaHoraIni"].ToString(), provider);
                    DateTime dtFin = Convert.ToDateTime(dt.Rows[i]["FechaHoraFin"].ToString(), provider);
                    DateTime dtFecha = Convert.ToDateTime(dt.Rows[i]["FechaHora"], provider);

                    string strFechaHora = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtFecha);
                    //string strFechaHoraFin = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtFin);
                    string strFechaHoraIni = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtIni);
                    string strFechaHoraFin = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtFin);
                    string strFechaHora = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtFecha);

                    _strEventos += "{\r\n";
                    _strEventos += "id: '" + dt.Rows[i]["idAgenda"].ToString() + "',\r\n";
                    //_strEventos += "title: '" + dt.Rows[i]["NombreEmpleado"].ToString() + "',\r\n";
                    //_strEventos += "start: '" + dt.Rows[i]["FechaHoraIni"].ToString() + "',\r\n";
                    _strEventos += "start: '" + strFechaHora + "',\r\n";
                    //_strEventos += "end: '" + dt.Rows[i]["FechaHoraFin"].ToString() + "',\r\n";
                    //_strEventos += "end: '" + strFechaHoraFin + "',\r\n";
                    //_strEventos += "className: 'bg-primary',\r\n";
                    _strEventos += "id: '" + dt.Rows[i]["idAgenda"].ToString() + "',\r\n";
                    _strEventos += "start: '" + strFechaHora + "',\r\n";

                    if (dt.Rows[i]["idAgenda"].ToString() != "")
                    if (dt.Rows[i]["idAfiliado"].ToString() != "")
                    string nombreEstado = dt.Rows[i]["Estado"].ToString();
                    string nombreCompleto = dt.Rows[i]["Nombres"].ToString() + " " + dt.Rows[i]["Apellidos"].ToString();

                    _strEventos += $"title: '{nombreCompleto}',\r\n";

                    if (nombreEstado == "Cancelado")
                    {
                        if (dt.Rows[i]["Estado"].ToString() == "Cancelado")
                        {
                            _strEventos += "title: '" + dt.Rows[i]["Nombres"].ToString() + " " + dt.Rows[i]["Apellidos"].ToString() + "',\r\n";
                            _strEventos += "color: '#F8AC59',\r\n"; // Warning
                            _strEventos += "description: 'Gym Pass Cancelado.',\r\n";
                            _strEventos += "icon: 'user',\r\n";
                            //_strEventos += "idSede: " + dt.Rows[i]["idSede"] + ",\r\n";
                            _strEventos += "btnAsistencia: 'none',\r\n";
                            _strEventos += "btnCancelar: 'none',\r\n";
                        } 
                        else if (dt.Rows[i]["Estado"].ToString() == "Asistió")
                        {
                            _strEventos += "title: '" + dt.Rows[i]["Nombres"].ToString() + " " + dt.Rows[i]["Apellidos"].ToString() + "',\r\n";
                            _strEventos += "color: '#1C84C6',\r\n"; // Success
                            _strEventos += "description: 'Gym Pass Asistió.',\r\n";
                            _strEventos += "icon: 'user',\r\n";
                            //_strEventos += "idSede: " + dt.Rows[i]["idSede"] + ",\r\n";
                            _strEventos += "btnAsistencia: 'none',\r\n";
                            _strEventos += "btnCancelar: 'none',\r\n";
                        }
                        else
                        {
                            _strEventos += "title: '" + dt.Rows[i]["Nombres"].ToString() + " " + dt.Rows[i]["Apellidos"].ToString() + "',\r\n";
                            _strEventos += "color: '#1ab394',\r\n"; // Primary
                            _strEventos += "description: 'Gym Pass Agendado.',\r\n";
                            _strEventos += "icon: 'user',\r\n";
                            //_strEventos += "idSede: " + dt.Rows[i]["idSede"] + ",\r\n";
                            _strEventos += "btnAsistencia: 'inline',\r\n";
                            _strEventos += "btnCancelar: 'inline',\r\n";
                        }
                        _strEventos += "title: '" + dt.Rows[i]["Nombres"].ToString() + " " + dt.Rows[i]["Apellidos"].ToString() + "',\r\n";
                        if (dt.Rows[i]["Cancelada"].ToString() != "0")
                        _strEventos += "color: '#F8AC59',\r\n"; // Warning
                        _strEventos += "description: 'El usuario Canceló la cita de Gym Pass.',\r\n";
                        _strEventos += "icon: 'user',\r\n";
                        _strEventos += "btnAsistencia: 'none',\r\n";
                        _strEventos += "btnNoAsistencia: 'none',\r\n";
                        _strEventos += "btnCancelar: 'none',\r\n";
                    } 
                    else if (nombreEstado == "Asistió")
                        {
                            _strEventos += "color: '#ed5565',\r\n"; //danger
                            _strEventos += "title: '" + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                            _strEventos += "description: 'Cita cancelada: " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                            _strEventos += "icon: 'id-card',\r\n";
                            _strEventos += "btnEliminar: 'none',\r\n";
                        _strEventos += "color: '#1C84C6',\r\n"; // Success
                        _strEventos += "description: 'El usuario Asistió a la cita de Gym Pass.',\r\n";
                        _strEventos += "icon: 'user',\r\n";
                        _strEventos += "btnAsistencia: 'none',\r\n";
                        _strEventos += "btnNoAsistencia: 'none',\r\n";
                        _strEventos += "btnCancelar: 'none',\r\n";
                        }
                        else
                    else if (nombreEstado == "No Asistió")
                        {
                            _strEventos += "color: '#F8AC59',\r\n"; //warning
                            _strEventos += "title: '" + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                            _strEventos += "description: 'Cita asignada: " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                            _strEventos += "icon: 'id-card',\r\n";
                            _strEventos += "btnEliminar: 'none',\r\n";
                        }
                        _strEventos += "color: '#DC3545',\r\n"; // Danger
                        _strEventos += "description: 'El usuario No Asistió a la cita de Gym Pass.',\r\n";
                        _strEventos += "icon: 'user',\r\n";
                        _strEventos += "btnAsistencia: 'none',\r\n";
                        _strEventos += "btnNoAsistencia: 'none',\r\n";
                        _strEventos += "btnCancelar: 'none',\r\n";
                    }
                    else
                    {
                        _strEventos += "title: '" + dt.Rows[i]["NombreEmpleado"].ToString() + "',\r\n";
                        _strEventos += "color: '#1ab394',\r\n";
                        _strEventos += "description: 'Gym Pass Agendado.',\r\n";
                        _strEventos += "icon: 'user',\r\n";
                        _strEventos += "btnEliminar: 'inline',\r\n";

                        //if (dt.Rows[i]["Cancelada"].ToString() != "0")
                        //{
                        //    _strEventos += "color: '#ed5565',\r\n"; //danger
                        //    _strEventos += "title: '" + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                        //    _strEventos += "description: 'Cita cancelada: " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                        //    _strEventos += "icon: 'id-card',\r\n";
                        //    _strEventos += "btnEliminar: 'none',\r\n";
                        //}
                        //else
                        //{
                        //    _strEventos += "color: '#F8AC59',\r\n"; //warning
                        //    _strEventos += "title: '" + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                        //    _strEventos += "description: 'Cita asignada: " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                        //    _strEventos += "icon: 'id-card',\r\n";
                        //    _strEventos += "btnEliminar: 'none',\r\n";
                        //}
                        _strEventos += "color: '#1ab394',\r\n"; // Primary
                        _strEventos += "description: 'La cita de Gym Pass ha sido Agendada.',\r\n";
                        _strEventos += "icon: 'user',\r\n";
                        _strEventos += "btnAsistencia: 'inline',\r\n";
                        _strEventos += "btnNoAsistencia: 'inline',\r\n";
                        _strEventos += "btnCancelar: 'inline',\r\n";
                    }

                    //_strEventos += "color: '#DBADFF',\r\n";
                    //_strEventos += "todoeldia: 0,\r\n";
                    _strEventos += "display: 'block',\r\n";
                    _strEventos += "allDay: false,\r\n";
                    _strEventos += "},\r\n";
                }
            }

            dt.Dispose();

            AgregarFestivos(_strEventos, "2025");
        }

        private string AgregarFestivos(string eventos, string anho)
        {
            //https://www.festivos.com.co/calendario
            _strEventos = eventos;

            if (anho == "2025")
            {
                _strEventos += "{\r\n";
                _strEventos += "start: '2025-01-01',\r\n";
                _strEventos += "end: '2025-01-01',\r\n";
                _strEventos += "title: 'Año nuevo',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-01-06',\r\n";
                _strEventos += "end: '2025-01-06',\r\n";
                _strEventos += "title: 'Reyes magos',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-03-24',\r\n";
                _strEventos += "end: '2025-03-24',\r\n";
                _strEventos += "title: 'Día de San José',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-04-17',\r\n";
                _strEventos += "end: '2025-04-17',\r\n";
                _strEventos += "title: 'Jueves Santo',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-04-18',\r\n";
                _strEventos += "end: '2025-04-18',\r\n";
                _strEventos += "title: 'Viernes Santo',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-05-01',\r\n";
                _strEventos += "end: '2025-05-01',\r\n";
                _strEventos += "title: 'Día del Trabajo',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-06-02',\r\n";
                _strEventos += "end: '2025-06-02',\r\n";
                _strEventos += "title: 'Ascensión de Jesús',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-06-23',\r\n";
                _strEventos += "end: '2025-06-23',\r\n";
                _strEventos += "title: 'Corpus Christi',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-06-30',\r\n";
                _strEventos += "end: '2025-06-30',\r\n";
                _strEventos += "title: 'Sagrado Corazón de Jesús',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-07-20',\r\n";
                _strEventos += "end: '2025-07-20',\r\n";
                _strEventos += "title: 'Día de la Independencia',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-08-07',\r\n";
                _strEventos += "end: '2025-08-07',\r\n";
                _strEventos += "title: 'Batalla de Boyacá',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-08-18',\r\n";
                _strEventos += "end: '2025-08-18',\r\n";
                _strEventos += "title: 'Asunción de la virgen',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-10-13',\r\n";
                _strEventos += "end: '2025-10-13',\r\n";
                _strEventos += "title: 'Día de la raza',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-11-03',\r\n";
                _strEventos += "end: '2025-11-03',\r\n";
                _strEventos += "title: 'Todos los santos',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-11-17',\r\n";
                _strEventos += "end: '2025-11-17',\r\n";
                _strEventos += "title: 'Independencia de Cartagena',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-12-08',\r\n";
                _strEventos += "end: '2025-12-08',\r\n";
                _strEventos += "title: 'Inmaculada concepción',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-12-25',\r\n";
                _strEventos += "end: '2025-12-25',\r\n";
                _strEventos += "title: 'Navidad',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "display: 'background',\r\n";
                _strEventos += "},\r\n";

                _strEventos += "],\r\n";
            }

            if (anho == "2026")
            {

            }

            return eventos;
        }

        private void CargarSedes()
        {
            int idSedeUsuario = Convert.ToInt32(Session["idSede"]);

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSedes("Gimnasio");

            int? idSede = (idSedeUsuario == 11) ? (int?)null : idSedeUsuario;

            DataTable dt = cg.ConsultaCargarSedesPorId(idSede, "Gimnasio");

            ddlSedes.Items.Clear();
            ddlSedes.Items.Add(new ListItem("Todas", "0"));

            if (dt != null && dt.Rows.Count > 0)
            {
            ddlSedes.DataSource = dt;
                ddlSedes.DataValueField = "idSede";
                ddlSedes.DataTextField = "NombreSede";
            ddlSedes.DataBind();
            }

            dt.Dispose();
        }

        protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ltSede.Text = ddlSedes.SelectedItem.Text;

            int? nuevoIdSede = ddlSedes.SelectedItem.Text == "Todas" || ddlSedes.SelectedItem.Value == "0"
                ? (int?)null
                : Convert.ToInt32(ddlSedes.SelectedItem.Value);

            int? idSedeAnterior = ViewState["FiltroSede"] as int?;

            ltSede.Text = ddlSedes.SelectedItem.Text.ToString();
            if (nuevoIdSede != idSedeAnterior)
            {
            CargarAgenda();
        }
        }

        private void CargarEstados()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstadosGymPassAgenda();

            ddlEstados.Items.Clear();
            ddlEstados.Items.Add(new ListItem("Todos", "Todos"));

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlEstados.DataSource = dt;
                ddlEstados.DataValueField = "Estados";
                ddlEstados.DataTextField = "Estados";
                ddlEstados.DataBind();
            }

            dt.Dispose();
        }

        protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSedes.SelectedItem.Value.ToString() != "")
            string nuevoEstado = ddlEstados.SelectedValue == "Todos" ? null : ddlEstados.SelectedValue;
            string estadoAnterior = ViewState["FiltroEstado"] as string;

            if (nuevoEstado != estadoAnterior)
            {
                ltSede.Text = ddlSedes.SelectedItem.Text.ToString();
                CargarAgenda();
            }
        }
    }
}