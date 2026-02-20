using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class programarsesion : System.Web.UI.Page
    {
        private string _strEventos;
        protected string strEventos { get { return this._strEventos; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Programar sesión");
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
                        txbFechaFin.Attributes.Add("type", "date");
                        txbFechaIni.Attributes.Add("min", dtHoy.Year.ToString() + "-" + String.Format("{0:MM}", dtHoy) + "-" + String.Format("{0:dd}", dtHoy));
                        txbFechaFin.Attributes.Add("min", dtHoy.Year.ToString() + "-" + String.Format("{0:MM}", dtHoy) + "-" + String.Format("{0:dd}", dtHoy));
                        divCrear.Visible = true;
                        CargarSedesSesion();
                        CargarSedesCalendario();
                        CargarEntrenadores();
                        CargarClasesGrupales();
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
                                string strQuery = "DELETE FROM ProgramacionClasesGrupales " +
                                    "WHERE idProgramacion = " + Request.QueryString["deleteid"].ToString();
                                clasesglobales cg = new clasesglobales();
                                string mensaje = cg.TraerDatosStr(strQuery);

                                if (mensaje == "OK")
                                {
                                    cg.InsertarLog(Session["idusuario"].ToString(), "ProgramacionClasesGrupales", "Elimina", "El usuario eliminó un nueva sesión de clase grupal.", "", "");
                                }
                            }
                            catch (SqlException ex)
                            {
                                string mensaje = ex.Message;
                            }
                            Response.Redirect("programarsesion");
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

        private void CargarClasesGrupales()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarClasesGrupales();

            ddlClasesGrupales.DataSource = dt;
            ddlClasesGrupales.DataBind();

            dt.Dispose();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            DateTime dtFechaFinCita;

            DateTime dtFechaIni = Convert.ToDateTime(txbFechaIni.Value.ToString());
            DateTime dtFechaFin = Convert.ToDateTime(txbFechaFin.Value.ToString());

            int intCountItemsChecked = 0;
            foreach (ListItem item in cbDiasRepite.Items)
            {
                if (item.Selected)
                {
                    intCountItemsChecked += 1;
                }
            }

            int nroDias = (dtFechaFin - dtFechaIni).Days + 1;

            string script = string.Empty;

            for (int i = 0; i < nroDias; i++)
            {
                DateTime dtFechaIniCita = Convert.ToDateTime(dtFechaIni.AddDays(i).ToString("yyyy-MM-dd") + " " + txbHoraIni.Value.ToString());
                DateTime dtFechaFinCitaDia = Convert.ToDateTime(dtFechaIni.AddDays(i).ToString("yyyy-MM-dd") + " " + txbHoraFin.Value.ToString());

                if (dtFechaFinCitaDia > dtFechaIniCita)
                {
                    try
                    {
                        while (dtFechaIniCita < dtFechaFinCitaDia)
                        {
                            dtFechaFinCita = dtFechaIniCita.AddMinutes(Convert.ToDouble(ddlDuracion.SelectedItem.Value.ToString()));

                            // Consulta si se cruza la cita en la sede con la fecha y hora de otra disponible
                            string strQuery = "SELECT * FROM ProgramacionClasesGrupales " +
                                "WHERE (idClaseGrupal = " + ddlClasesGrupales.SelectedItem.Value.ToString() + " " +
                                "OR idEntrenador = " + ddlEntrenadores.SelectedItem.Value.ToString() + " " +
                                "OR idSede = " + ddlSedesSesion.SelectedItem.Value.ToString() + ") " +
                                "AND (('" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "' > FechaInicio AND '" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "' < FechaFin) " +
                                "OR ('" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "' > FechaInicio AND '" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "' < FechaFin))";
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.TraerDatos(strQuery);

                            if (dt.Rows.Count == 0)
                            {
                                // Consulta si se cruza la cita del entrenador con la fecha y hora de otra disponible
                                strQuery = "SELECT * FROM ProgramacionClasesGrupales " +
                                    "WHERE idEntrenador = " + ddlEntrenadores.SelectedItem.Value.ToString() + " " +
                                    "AND idClaseGrupal != " + ddlClasesGrupales.SelectedItem.Value.ToString() + " " +
                                    "AND idSede = " + ddlSedesSesion.SelectedItem.Value.ToString() + " " +
                                    "AND TIMESTAMPDIFF(MINUTE, '" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "', FechaInicio) <= 60 " +
                                    "AND '" + dtFechaIniCita.ToString("yyyy-MM-dd") + "' = DATE(FechaInicio) ";
                                DataTable dt1 = cg.TraerDatos(strQuery);

                                if (dt1.Rows.Count == 0)
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
                                        if (intCountItemsChecked > 0)
                                        {
                                            foreach (ListItem item in cbDiasRepite.Items)
                                            {
                                                if (item.Selected)
                                                {
                                                    if (Convert.ToInt16(dtFechaIniCita.DayOfWeek) == Convert.ToInt16(item.Value.ToString()))
                                                    {
                                                        strQuery = "INSERT INTO ProgramacionClasesGrupales " +
                                                            "(idClaseGrupal, idEntrenador, idSede, FechaInicio, FechaFin, " +
                                                            "CupoMaximo, Estado) " +
                                                            "VALUES (" + ddlClasesGrupales.SelectedItem.Value.ToString() + ", " +
                                                            "" + ddlEntrenadores.SelectedItem.Value.ToString() + ", " +
                                                            "" + ddlSedesSesion.SelectedItem.Value.ToString() + ", " +
                                                            "'" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                                            "'" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                                            "" + txbCupo.Value.ToString() + ", " +
                                                            "'Programado') ";

                                                        string mensaje = cg.TraerDatosStr(strQuery);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            strQuery = "INSERT INTO ProgramacionClasesGrupales " +
                                                "(idClaseGrupal, idEntrenador, idSede, FechaInicio, FechaFin, " +
                                                "CupoMaximo, Estado) " +
                                                "VALUES (" + ddlClasesGrupales.SelectedItem.Value.ToString() + ", " +
                                                "" + ddlEntrenadores.SelectedItem.Value.ToString() + ", " +
                                                "" + ddlSedesSesion.SelectedItem.Value.ToString() + ", " +
                                                "'" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                                "'" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                                "" + txbCupo.Value.ToString() + ", " +
                                                "'Programado') ";

                                            string mensaje = cg.TraerDatosStr(strQuery);
                                        }
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
                                                window.location.href = 'programarsesion';
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
                                            text: 'Ya esta ocupado este especialista en otra sede.',
                                            icon: 'error'
                                        }).then(() => {
                                        });
                                        ";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                                    dtFechaIniCita = dtFechaFinCitaDia;
                                }
                                dt1.Dispose();
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
        }

        protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSesiones(ddlSedes.SelectedItem.Value.ToString());
        }

        private void CargarSesiones(string idSede)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSesiones(int.Parse(idSede));

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
                    _strEventos += "id: '" + dt.Rows[i]["idProgramacion"].ToString() + "',\r\n";
                    //_strEventos += "title: '" + dt.Rows[i]["NombreEmpleado"].ToString() + "',\r\n";
                    //_strEventos += "start: '" + dt.Rows[i]["FechaHoraIni"].ToString() + "',\r\n";
                    _strEventos += "start: '" + strFechaHoraIni + "',\r\n";
                    //_strEventos += "end: '" + dt.Rows[i]["FechaHoraFin"].ToString() + "',\r\n";
                    _strEventos += "end: '" + strFechaHoraFin + "',\r\n";
                    //_strEventos += "className: 'bg-primary',\r\n";

                    //if (dt.Rows[i]["idAfiliado"].ToString() != "")
                    //{
                    //    if (dt.Rows[i]["Cancelada"].ToString() != "0")
                    //    {
                    //        _strEventos += "color: '#ed5565',\r\n"; //danger
                    //        _strEventos += "title: '" + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                    //        _strEventos += "description: 'Cita cancelada: " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                    //        _strEventos += "icon: 'id-card',\r\n";
                    //        _strEventos += "btnEliminar: 'none',\r\n";
                    //    }
                    //    else
                    //    {
                    //        _strEventos += "color: '#F8AC59',\r\n"; //warning
                    //        _strEventos += "title: '" + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                    //        _strEventos += "description: 'Cita asignada: " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                    //        _strEventos += "icon: 'id-card',\r\n";
                    //        _strEventos += "btnEliminar: 'none',\r\n";
                    //    }
                    //}
                    //else
                    //{
                        _strEventos += "title: '" + dt.Rows[i]["Modalidad"].ToString() + " - " + dt.Rows[i]["NombreEmpleado"].ToString() + "',\r\n";
                        _strEventos += "color: '#1ab394',\r\n";
                        _strEventos += "description: 'Cupo total: " + dt.Rows[i]["CupoMaximo"].ToString() + "',\r\n";
                        _strEventos += "icon: 'user-doctor',\r\n";
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