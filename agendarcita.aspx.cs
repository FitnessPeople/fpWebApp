using System;
using System.Data;
using System.Globalization;

namespace fpWebApp
{
	public partial class agendarcita : System.Web.UI.Page
	{
        private string _strEventos;
        protected string strEventos { get { return this._strEventos; } }
        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Agendar cita");
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
                        CargarSedes();
                        btnAsignar.Visible = true;
                        divAfil.Visible = true;
                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        
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

        private void CargarSedes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSedes("Gimnasio");

            ddlSedes.Items.Clear();
            ddlSedes.DataSource = dt;
            ddlSedes.DataBind();

            dt.Dispose();

            string idSede = Session["idSede"].ToString(); // ejemplo

            // Marca como seleccionado el ítem correspondiente
            if (ddlSedes.Items.FindByValue(idSede) != null)
            {
                ddlSedes.SelectedValue = idSede;
            }

            ltSede.Text = ddlSedes.SelectedItem.Text.ToString();

            CargarAgenda();
        }

        private void CargarAfiliados()
        {
            string strQuery = @"SELECT a.idAfiliado, 
                CONCAT(a.NombreAfiliado, ' ', a.ApellidoAfiliado, ' - ', a.DocumentoAfiliado) AS DocNombreAfiliado 
                FROM afiliados a 
                INNER JOIN AfiliadosPlanes ap ON ap.idAfiliado = a.idAfiliado AND ap.EstadoPlan = 'Activo' 
                WHERE EstadoAfiliado = 'Activo' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlAfiliados.DataSource = dt;
            ddlAfiliados.DataBind();

            dt.Dispose();
        }

        protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSedes.SelectedItem.Value.ToString() != "")
            {
                ltSede.Text = ddlSedes.SelectedItem.Text.ToString();
                CargarAgenda();
            }
        }

        private void CargarAgenda()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarAgendaPorSedePorEspecialidad(int.Parse(ddlSedes.SelectedItem.Value.ToString()), int.Parse(ddlEspecialidad.SelectedItem.Value.ToString()));

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

                    _strEventos += "{\r\n";
                    _strEventos += "id: '" + dt.Rows[i]["idDisponibilidad"].ToString() + "',\r\n";
                    _strEventos += "title: '" + dt.Rows[i]["NombreEmpleado"].ToString() + "',\r\n";
                    _strEventos += "start: '" + strFechaHoraIni + "',\r\n";
                    _strEventos += "end: '" + strFechaHoraFin + "',\r\n";

                    if (dt.Rows[i]["idAfiliado"].ToString() != "")
                    {
                        _strEventos += "color: '#F8AC59',\r\n";
                        _strEventos += "title: '" + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                        _strEventos += "description: 'Cita asignada a: " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + ". Atiende:" + dt.Rows[i]["NombreEmpleado"].ToString() + "',\r\n";
                        _strEventos += "icon: 'id-card',\r\n";
                        _strEventos += "btnAsignar: 'none',\r\n";
                        _strEventos += "divAfil: 'none',\r\n";
                    }
                    else
                    {
                        if (dt.Rows[i]["Cancelada"].ToString() != "0")
                        {
                            _strEventos += "color: '#ed5565',\r\n";
                            _strEventos += "title: '" + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                            _strEventos += "description: 'Cita cancelada: " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                            _strEventos += "icon: 'id-card',\r\n";
                            _strEventos += "btnAsignar: 'none',\r\n";
                            _strEventos += "divAfil: 'none',\r\n";
                        }
                        else
                        {
                            _strEventos += "title: '" + dt.Rows[i]["NombreEmpleado"].ToString() + "',\r\n";
                            _strEventos += "color: '#1ab394',\r\n";
                            _strEventos += "description: 'Cita disponible.',\r\n";
                            _strEventos += "icon: 'user-doctor',\r\n";
                            _strEventos += "btnAsignar: 'inline',\r\n";
                            _strEventos += "divAfil: 'inline',\r\n";
                        }
                    }

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

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarAgenda();
            CargarAfiliados();
        }

        //protected void btnAfiliado_Click(object sender, EventArgs e)
        //{   
        //    string[] strDocumento = txbAfiliado.Text.ToString().Split('-');
        //    string strQuery = "SELECT * FROM Afiliados a " +
        //        "LEFT JOIN Sedes s ON a.idSede = s.idSede " +
        //        "WHERE DocumentoAfiliado = '" + strDocumento[0].Trim() + "' ";
        //    clasesglobales cg = new clasesglobales();
        //    DataTable dt = cg.TraerDatos(strQuery);

        //    if (dt.Rows.Count > 0)
        //    {
        //        hfIdAfiliado.Value = dt.Rows[0]["idAfiliado"].ToString();
        //    }
        //    dt.Dispose();

        //    CargarAgenda();
        //    //btnAsignar.Visible = true;
        //}
    }
}