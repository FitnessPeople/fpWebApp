using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class programarpersonalizada : System.Web.UI.Page
    {
        private string _strEventos;
        protected string strEventos { get { return this._strEventos; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Programar sesión personalizada");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        CargarSedesCalendario();
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        DateTime dtHoy = DateTime.Now;
                        txbFechaIni.Attributes.Add("type", "date");
                        txbFechaIni.Attributes.Add("min", dtHoy.Year.ToString() + "-" + String.Format("{0:MM}", dtHoy) + "-" + String.Format("{0:dd}", dtHoy));
                        divCrear.Visible = true;
                        CargarSedesSesion();
                        CargarSedesCalendario();
                        CargarEntrenadores();
                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        btnEliminar.Visible = true;
                    }
                    if (Request.QueryString.Count > 0)
                    {
                        if (Request.QueryString["deleteid"] != null)
                        {
                            try
                            {
                                string strQuery = "DELETE FROM ProgramacionSesionPersonalizada " +
                                    "WHERE idProgramacionPer = " + Request.QueryString["deleteid"].ToString();
                                clasesglobales cg = new clasesglobales();
                                string mensaje = cg.TraerDatosStr(strQuery);

                                if (mensaje == "OK")
                                {
                                    cg.InsertarLog(Session["idusuario"].ToString(), "ProgramacionSesionPersonalizada", "Elimina", "El usuario eliminó una sesión personalizada.", "", "");
                                }
                            }
                            catch (SqlException ex)
                            {
                                string mensaje = ex.Message;
                            }
                            Response.Redirect("programarpersonalizada");
                        }
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

        private void CargarSedesCalendario()
        {
            int idSedeUsuario = Convert.ToInt32(Session["idSede"]);

            clasesglobales cg = new clasesglobales();

            int? idSede = (idSedeUsuario == 11) ? (int?)null : idSedeUsuario;

            DataTable dt = cg.ConsultaCargarSedesPorId(idSede, "Gimnasio");

            ddlSedes.Items.Clear();
            ddlSedes.Items.Add(new ListItem("Seleccione", ""));

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlSedes.DataSource = dt;
                ddlSedes.DataValueField = "idSede";
                ddlSedes.DataTextField = "NombreSede";
                ddlSedes.DataBind();
            }

            dt.Dispose();
        }

        private void CargarSedesSesion()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSedesPorId(null, "Gimnasio");

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlSedesSesion.DataSource = dt;
                ddlSedesSesion.DataBind();
            }

            dt.Dispose();
        }

        private void CargarEntrenadores()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarEntrenadores();

            ddlEntrenadores.DataSource = dt;
            ddlEntrenadores.DataBind();

            dt.Dispose();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            DateTime dtFechaFinCita;

            DateTime dtFechaIni = Convert.ToDateTime(txbFechaIni.Value.ToString());

            string script = string.Empty;

            DateTime dtFechaIniCita = Convert.ToDateTime(dtFechaIni.ToString("yyyy-MM-dd") + " " + txbHoraIni.Value.ToString());
            DateTime dtFechaFinCitaDia = dtFechaIniCita.AddHours(1);

            if (dtFechaFinCitaDia > dtFechaIniCita)
            {
                try
                {
                    while (dtFechaIniCita < dtFechaFinCitaDia)
                    {
                        dtFechaFinCita = dtFechaFinCitaDia;

                        // Consulta si se cruza la cita en la sede con la fecha y hora de otra disponible
                        string strQuery = "SELECT * FROM ProgramacionSesionPersonalizada " +
                            "WHERE (idEntrenador = " + ddlEntrenadores.SelectedItem.Value.ToString() + " " +
                            "OR idSede = " + ddlSedesSesion.SelectedItem.Value.ToString() + ") " +
                            "AND FechaHora = '" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "' ";
                        clasesglobales cg = new clasesglobales();
                        DataTable dt = cg.TraerDatos(strQuery);

                        if (dt.Rows.Count == 0)
                        {
                            
                            if (dtFechaIniCita.Hour < 5 || dtFechaIniCita.Hour >= 21)
                            {
                                script = @"
                                    Swal.fire({
                                        title: 'Advertencia',
                                        text: 'Horario fuera del intervalo permitido para la sesión.',
                                        icon: 'error'
                                    }).then(() => {
                                    });
                                    ";
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                                dtFechaIniCita = dtFechaFinCita;
                            }
                            else
                            {
                                string[] strDocumento = txbAfiliado.Text.ToString().Split('-');

                                strQuery = "INSERT INTO ProgramacionSesionPersonalizada " +
                                    "(idEntrenador, DocumentoAfiliado, idSede, FechaHora, Estado) " +
                                    "VALUES (" + ddlEntrenadores.SelectedItem.Value.ToString() + ", " +
                                    "'" + strDocumento[0].Trim() + "', " +
                                    "" + ddlSedesSesion.SelectedItem.Value.ToString() + ", " +
                                    "'" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                    "'Agendada') ";

                                string mensaje = cg.TraerDatosStr(strQuery);
                                
                                dtFechaIniCita = dtFechaFinCita;

                                script = @"
                                    Swal.fire({
                                        title: 'Sesión creada exitosamente.',
                                        text: '',
                                        icon: 'success',
                                        timer: 3000, // 3 segundos
                                        showConfirmButton: false,
                                        timerProgressBar: true
                                    }).then(() => {
                                        window.location.href = 'programarpersonalizada';
                                    });
                                    ";
                                ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                            }
                        }
                        else
                        {
                            script = @"
                                Swal.fire({
                                    title: 'Advertencia',
                                    text: 'Ya esta ocupado este horario en la sede.',
                                    icon: 'error'
                                }).then(() => {
                                });
                                ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                            dtFechaIniCita = dtFechaFinCitaDia;
                        }
                        dt.Dispose();
                    }
                }
                catch (SqlException ex)
                {
                    script = @"
                        Swal.fire({
                            title: 'Error',
                            text: 'SqlException: (" + ex.Message.ToString() + @").',
                            icon: 'error'
                        }).then(() => {
                        });
                        ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                }
            }
            else
            {
                script = @"
                    Swal.fire({
                        title: 'Advertencia',
                        text: 'Hora de inicio debe ser menor a hora final.',
                        icon: 'error'
                    }).then(() => {
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
            }
        }

        protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSesionesPersonalizadas(ddlSedes.SelectedItem.Value.ToString());
        }

        private void CargarSesionesPersonalizadas(string idSede)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSesionesPersonalizadas(int.Parse(idSede));

            _strEventos = "events: [\r\n";

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IFormatProvider provider = new CultureInfo("en-US");
                    DateTime dtIni = Convert.ToDateTime(dt.Rows[i]["FechaHora"].ToString(), provider);
                    DateTime dtFin = dtIni.AddHours(1);

                    string strFechaHoraIni = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtIni);
                    string strFechaHoraFin = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtFin);

                    _strEventos += "{\r\n";
                    _strEventos += "id: '" + dt.Rows[i]["idProgramacionPer"].ToString() + "',\r\n";
                    //_strEventos += "title: '" + dt.Rows[i]["NombreEmpleado"].ToString() + "',\r\n";
                    //_strEventos += "start: '" + dt.Rows[i]["FechaHoraIni"].ToString() + "',\r\n";
                    _strEventos += "start: '" + strFechaHoraIni + "',\r\n";
                    //_strEventos += "end: '" + dt.Rows[i]["FechaHoraFin"].ToString() + "',\r\n";
                    _strEventos += "end: '" + strFechaHoraFin + "',\r\n";
                    //_strEventos += "className: 'bg-primary',\r\n";

                    _strEventos += "title: '" + dt.Rows[i]["NombreEmpleado"].ToString() + "',\r\n";
                    _strEventos += "color: '#1ab394',\r\n";
                    _strEventos += "description: 'Entrenamiento personalizado para: " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                    _strEventos += "icon: 'handshake-angle',\r\n";
                    _strEventos += "btnEliminar: 'inline',\r\n";
                    //}

                    //_strEventos += "color: '#DBADFF',\r\n";
                    //_strEventos += "todoeldia: 0,\r\n";
                    _strEventos += "allDay: false,\r\n";
                    _strEventos += "},\r\n";
                }
            }

            dt.Dispose();

            AgregarFestivos(_strEventos, "2026");

            _strEventos += "],\r\n";

        }

        /// <summary>
        /// Agrega los festivos del año al calendario
        /// </summary>
        /// <param name="eventos"></param>
        /// <param name="anho"></param>
        /// <returns></returns>
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