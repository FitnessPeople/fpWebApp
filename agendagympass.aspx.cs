using System;
using System.Data;
using System.Globalization;
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

                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {

                    }
                    if (ViewState["Consulta"].ToString() == "1" || ViewState["CrearModificar"].ToString() == "1")
                    {
                        if (Session["idSede"].ToString() != "")
                        {
                            int idSedeUsuario = Convert.ToInt32(Session["idSede"]);

                            if (idSedeUsuario != 11)
                            {
                                clasesglobales cg = new clasesglobales();

                                int? idSede = (idSedeUsuario == 11) ? (int?)null : idSedeUsuario;

                                DataTable dt = cg.ConsultaCargarSedesPorId(idSede, "Gimnasio");

                                ltSede.Text = $"Sede {dt.Rows[0]["NombreSede"]}";

                                dt.Dispose();
                            }
                            else
                            {
                                ltSede.Text = "de todas las sedes";
                            }

                            divFiltroSede.Visible = (idSedeUsuario == 11);
                            CargarSedes();
                            CargarEstados();
                            CargarAgenda();
                        }
                        else
                        {
                            Response.Redirect("gympass");
                        }
                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        btnNoAsistencia.Visible = true;
                        btnAsistencia.Visible = true;
                        btnCancelar.Visible = true;
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
            int idSedeUsuario = Convert.ToInt32(Session["idSede"]);

            clasesglobales cg = new clasesglobales();

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

                    string strFechaHora = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtFecha);

                    _strEventos += "{\r\n";
                    _strEventos += "id: '" + dt.Rows[i]["idAgenda"].ToString() + "',\r\n";
                    _strEventos += "start: '" + strFechaHora + "',\r\n";

                    string nombreEstado = dt.Rows[i]["Estado"].ToString();
                    string nombreCompleto = dt.Rows[i]["Nombres"].ToString() + " " + dt.Rows[i]["Apellidos"].ToString();

                    _strEventos += $"title: '{nombreCompleto}',\r\n";

                    if (nombreEstado == "Cancelado")
                    {
                        _strEventos += "color: '#F8AC59',\r\n"; // Warning
                        _strEventos += "description: 'El usuario Canceló la cita de Gym Pass.',\r\n";
                        _strEventos += "icon: 'user',\r\n";
                        _strEventos += "btnAsistencia: 'none',\r\n";
                        _strEventos += "btnNoAsistencia: 'none',\r\n";
                        _strEventos += "btnCancelar: 'none',\r\n";
                    } 
                    else if (nombreEstado == "Asistió")
                    {
                        _strEventos += "color: '#1C84C6',\r\n"; // Success
                        _strEventos += "description: 'El usuario Asistió a la cita de Gym Pass.',\r\n";
                        _strEventos += "icon: 'user',\r\n";
                        _strEventos += "btnAsistencia: 'none',\r\n";
                        _strEventos += "btnNoAsistencia: 'none',\r\n";
                        _strEventos += "btnCancelar: 'none',\r\n";
                    }
                    else if (nombreEstado == "No Asistió")
                    {
                        _strEventos += "color: '#DC3545',\r\n"; // Danger
                        _strEventos += "description: 'El usuario No Asistió a la cita de Gym Pass.',\r\n";
                        _strEventos += "icon: 'user',\r\n";
                        _strEventos += "btnAsistencia: 'none',\r\n";
                        _strEventos += "btnNoAsistencia: 'none',\r\n";
                        _strEventos += "btnCancelar: 'none',\r\n";
                    }
                    else
                    {
                        _strEventos += "color: '#1ab394',\r\n"; // Primary
                        _strEventos += "description: 'La cita de Gym Pass ha sido Agendada.',\r\n";
                        _strEventos += "icon: 'user',\r\n";
                        _strEventos += "btnAsistencia: 'inline',\r\n";
                        _strEventos += "btnNoAsistencia: 'inline',\r\n";
                        _strEventos += "btnCancelar: 'inline',\r\n";
                    }

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

        private void CargarSedes()
        {
            int idSedeUsuario = Convert.ToInt32(Session["idSede"]);

            clasesglobales cg = new clasesglobales();

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
            if (ddlSedes.SelectedItem.Text == "Todas")
            {
                ltSede.Text = "de todas las sedes";
            }
            else
            {
                ltSede.Text = $"Sede {ddlSedes.SelectedItem.Text}";
            }

            int? nuevoIdSede = ddlSedes.SelectedItem.Text == "Todas" || ddlSedes.SelectedItem.Value == "0"
                ? (int?)null
                : Convert.ToInt32(ddlSedes.SelectedItem.Value);

            int? idSedeAnterior = ViewState["FiltroSede"] as int?;

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

        protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nuevoEstado = ddlEstados.SelectedValue == "Todos" ? null : ddlEstados.SelectedValue;
            string estadoAnterior = ViewState["FiltroEstado"] as string;

            if (nuevoEstado != estadoAnterior)
            {
                CargarAgenda();
            }
        }
    }
}