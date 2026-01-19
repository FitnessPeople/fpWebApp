using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class agendacorporativo : System.Web.UI.Page
    {
        private string _strEventos;
        private string _strVistaInicial;
        protected string strEventos { get { return this._strEventos; } }
        protected string strVistaInicial { get { return this._strVistaInicial; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Agenda");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        CargarDatos();
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        CargarDatos();
                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        //btnEliminar.Visible = true;
                    }
                    if (Request.QueryString.Count > 0)
                    {
                        if (Request.QueryString["deleteid"] != null)
                        {
                            try
                            {
                                string strQuery = "DELETE FROM AgendaAsesoresCorporativos " +
                                    " WHERE idAgendaCorp = " + Request.QueryString["deleteid"].ToString();
                                clasesglobales cg = new clasesglobales();
                                string mensaje = cg.TraerDatosStr(strQuery);
                            }
                            catch (SqlException ex)
                            {
                                string mensaje = ex.Message;
                            }
                            Response.Redirect("agendacorporativo");
                        }
                        
                        if (Request.QueryString["atendida"] != null)
                        {
                            try
                            {
                                string strQuery = "UPDATE AgendaAsesoresCorporativos SET Atendida = 1 " +
                                    " WHERE idAgendaCorp = " + Request.QueryString["atendida"].ToString();
                                clasesglobales cg = new clasesglobales();
                                string mensaje = cg.TraerDatosStr(strQuery);
                            }
                            catch (SqlException ex)
                            {
                                string mensaje = ex.Message;
                            }
                            Response.Redirect("agendacorporativo");
                        }

                        if (Request.QueryString["negociada"] != null)
                        {
                            try
                            {
                                string strQuery = "UPDATE AgendaAsesoresCorporativos SET Atendida = 1, Negociada = 1 " +
                                    " WHERE idAgendaCorp = " + Request.QueryString["negociada"].ToString();
                                clasesglobales cg = new clasesglobales();
                                string mensaje = cg.TraerDatosStr(strQuery);
                            }
                            catch (SqlException ex)
                            {
                                string mensaje = ex.Message;
                            }
                            Response.Redirect("agendacorporativo");
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

        private void CargarAgenda(string idUsuario)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaAgendaPorAsesor(int.Parse(idUsuario));

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
                    _strEventos += "id: '" + dt.Rows[i]["idAgendaCorp"].ToString() + "',\r\n";
                    _strEventos += "start: '" + strFechaHoraIni + "',\r\n";
                    _strEventos += "end: '" + strFechaHoraFin + "',\r\n";

                    if (dt.Rows[i]["idUsuario"].ToString() != "")
                    {
                        if (dt.Rows[i]["Atendida"].ToString() == "0" && dt.Rows[i]["Negociada"].ToString() == "0") // Cita agendada
                        {
                            _strEventos += "color: '#1ab394',\r\n"; // primary
                            _strEventos += "title: '" + dt.Rows[i]["NombreUsuario"].ToString() + "',\r\n";
                            _strEventos += "description: '<h4>Cita agendada.</h4><br />" +
                                "<i class=\"fa fa-building m-r-sm\"></i>" + dt.Rows[i]["NombreEmpresaCRM"].ToString() + "<br />" +
                                "<i class=\"fa fa-pen-to-square m-r-sm\"></i>" + dt.Rows[i]["Contexto"].ToString() + "',\r\n";
                            _strEventos += "icon: 'calendar-plus',\r\n";
                            _strEventos += "btnEliminar: 'inline',\r\n";
                            _strEventos += "btnAtendida: 'inline',\r\n";
                            _strEventos += "btnNegociada: 'inline',\r\n";
                        }

                        if (dt.Rows[i]["Atendida"].ToString() == "1" && dt.Rows[i]["Negociada"].ToString() == "0") // Cita atendida
                        {
                            _strEventos += "color: '#F8AC59',\r\n"; // warning
                            _strEventos += "title: '" + dt.Rows[i]["NombreUsuario"].ToString() + "',\r\n";
                            _strEventos += "description: '<h4>Cita agendada.</h4><br />" +
                                "<i class=\"fa fa-building m-r-sm\"></i>" + dt.Rows[i]["NombreEmpresaCRM"].ToString() + "<br />" +
                                "<i class=\"fa fa-pen-to-square m-r-sm\"></i>" + dt.Rows[i]["Contexto"].ToString() + "',\r\n";
                            _strEventos += "icon: 'calendar-check',\r\n";
                            _strEventos += "btnEliminar: 'none',\r\n";
                            _strEventos += "btnAtendida: 'none',\r\n";
                            _strEventos += "btnNegociada: 'inline',\r\n";
                        }

                        if (dt.Rows[i]["Negociada"].ToString() == "1") // Venta negociada
                        {
                            _strEventos += "color: '#1a7bb9',\r\n"; // success
                            _strEventos += "title: '" + dt.Rows[i]["NombreUsuario"].ToString() + "',\r\n";
                            _strEventos += "description: '<h4>Cita agendada.</h4><br />" +
                                "<i class=\"fa fa-building m-r-sm\"></i>" + dt.Rows[i]["NombreEmpresaCRM"].ToString() + "<br />" +
                                "<i class=\"fa fa-pen-to-square m-r-sm\"></i>" + dt.Rows[i]["Contexto"].ToString() + "',\r\n";
                            _strEventos += "icon: 'trophy',\r\n";
                            _strEventos += "btnEliminar: 'none',\r\n";
                            _strEventos += "btnAtendida: 'none',\r\n";
                            _strEventos += "btnNegociada: 'none',\r\n";
                        }
                    }

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

        private void listaEmpresasAfiliadas()
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarEmpresasYProspectosCorporativos();

                ddlEmpresas.Items.Clear();
                ListItem li = new ListItem("Seleccione", "");
                ddlEmpresas.Items.Add(li);
                ddlEmpresas.DataSource = dt;
                ddlEmpresas.DataBind();

                dt.Dispose();
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", "Ocurrió un error al cargar las empresas. Por favor intente nuevamente." + ex.Message.ToString(), "error");
            }
        }
        
        private void CargarDatos()
        {
            DateTime dtHoy = DateTime.Now;
            txbFechaIni.Attributes.Add("type", "date");
            txbFechaIni.Attributes.Add("min", dtHoy.Year.ToString() + "-" + String.Format("{0:MM}", dtHoy) + "-" + String.Format("{0:dd}", dtHoy));
            
            listaEmpresasAfiliadas();

            if (Session["idPerfil"].ToString() == "36" || Session["idPerfil"].ToString() == "1" || Session["idPerfil"].ToString() == "37") // Líder Corporativo, CEO o Director operativo
            {
                // Cargar lista de asesores corporativos
                string strQuery = @"
                    SELECT u.* 
                    FROM usuarios u 
                    INNER JOIN perfiles p ON p.idPerfil = u.idPerfil 
                    WHERE p.idPerfil = 10 "; // Perfil 10: Asesor corporativo

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);

                ddlAsesores.Items.Clear();
                ListItem li = new ListItem("Seleccione", "");
                ddlAsesores.Items.Add(li);
                ddlAsesores.DataSource = dt;
                ddlAsesores.DataBind();
                divAsesor.Visible = true;

                //Cargar agenda de todos los asesores corporativos
                CargarAgenda("0");
                ltAsesor.Text = "todos los asesores";
                _strVistaInicial = "dayGridMonth";
            }
            else
            {
                if (Session["idPerfil"].ToString() == "10") // Asesor Corporativo
                {
                    //Cargar agenda de asesor
                    CargarAgenda(Session["idUsuario"].ToString());
                    ltAsesor.Text = Session["NombreUsuario"].ToString();
                    divAsesor.Visible = false;
                    _strVistaInicial = "timeGridWeek";
                }
            }
        }

        /// <summary>
        /// Inserta en la tabla AgendaAsesoresCorporativos
        /// </summary>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //string fechahorainicio = txbFechaIni.Value.ToString() + " " + txbHoraIni.Value.ToString();
            //string fechahorafin = txbFechaFin.Value.ToString() + " " + txbHoraFin.Value.ToString();

            //DateTime dtFechaIniCita = Convert.ToDateTime(fechahorainicio);
            DateTime dtFechaFinCita;

            DateTime dtFechaIni = Convert.ToDateTime(txbFechaIni.Value.ToString());
            DateTime dtFechaFin = Convert.ToDateTime(txbFechaIni.Value.ToString());

            int nroDias = (dtFechaFin - dtFechaIni).Days + 1;

            string script = string.Empty;

            int idUsuario;
            if (Session["idPerfil"].ToString() == "10") // Asesor Corporativo
            {
                //Cargar agenda de asesor
                idUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
            }
            else
            {
                idUsuario = Convert.ToInt32(ddlAsesores.SelectedItem.Value.ToString());
            }

            for (int i = 0; i < nroDias; i++)
            {
                DateTime dtFechaIniCita = Convert.ToDateTime(dtFechaIni.AddDays(i).ToString("yyyy-MM-dd") + " " + txbHoraIni.Value.ToString());
                DateTime dtFechaFinCitaDia = dtFechaIniCita.AddMinutes(60);

                try
                {
                    while (dtFechaIniCita < dtFechaFinCitaDia)
                    {
                        dtFechaFinCita = dtFechaIniCita.AddMinutes(60);

                        // Consulta si se cruza la cita en la sede con la fecha y hora de otra disponible
                        string strQuery = "SELECT * FROM AgendaAsesoresCorporativos " +
                            "WHERE (idEmpresa = " + ddlEmpresas.SelectedItem.Value.ToString() + " " +
                            "OR idUsuario = " + idUsuario.ToString() + ") " +
                            "AND (('" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "' > FechaHoraInicio AND '" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "' < FechaHoraFinal) " +
                            "OR ('" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "' > FechaHoraInicio AND '" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "' < FechaHoraFinal))";
                        clasesglobales cg = new clasesglobales();
                        DataTable dt = cg.TraerDatos(strQuery);

                        if (dt.Rows.Count == 0)
                        {
                            strQuery = @"
                                SELECT * FROM AgendaAsesoresCorporativos " +
                                "WHERE idUsuario = " + idUsuario.ToString() + " " +
                                "AND TIMESTAMPDIFF(MINUTE, '" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "', FechaHoraInicio) <= 60 " +
                                "AND '" + dtFechaIniCita.ToString("yyyy-MM-dd") + "' = DATE(FechaHoraInicio) ";
                            DataTable dt1 = cg.TraerDatos(strQuery);

                            if (dt1.Rows.Count == 0)
                            {
                                if (dtFechaIniCita.Hour < 7 || dtFechaIniCita.Hour >= 18)
                                {
                                    script = @"
                                        Swal.fire({
                                            title: 'Advertencia',
                                            text: 'Horario fuera del intervalo de visitas.',
                                            icon: 'error'
                                        }).then(() => {
                                        });
                                        ";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                                    //ltMensaje.Text = "Horario fuera del intervalo del especialista";
                                    dtFechaIniCita = dtFechaFinCita;
                                }
                                else
                                {
                                    strQuery = "INSERT INTO AgendaAsesoresCorporativos " +
                                        "(idUsuario, idEmpresa, FechaHoraInicio, FechaHoraFinal, Contexto) " +
                                        "VALUES (" + idUsuario.ToString() + ", " +
                                        "" + ddlEmpresas.SelectedItem.Value.ToString() + ", " +
                                        "'" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                        "'" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                        "'" + txbObservaciones.Value.ToString() + "') ";

                                    string mensaje = cg.TraerDatosStr(strQuery);
                                    
                                    dtFechaIniCita = dtFechaFinCita;

                                    script = @"
                                        Swal.fire({
                                            title: 'Agenda creada exitosamente.',
                                            text: '',
                                            icon: 'success',
                                            timer: 3000, // 3 segundos
                                            showConfirmButton: false,
                                            timerProgressBar: true
                                        }).then(() => {
                                            window.location.href = 'agendacorporativo';
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
                                        text: 'Ya esta ocupado este espacio con la misma empresa.',
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
        }

        //protected void ddlAsesores_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlAsesores.SelectedItem.Value.ToString() != "")
        //    {
        //        ltAsesor.Text = ddlAsesores.SelectedItem.Text.ToString();
        //        CargarAgenda(ddlAsesores.SelectedItem.Value.ToString());
        //    }
        //}

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            // tipo puede ser: 'success', 'error', 'warning', 'info', 'question'
            string script = $@"
                Swal.hideLoading();
                Swal.fire({{
                    title: '{titulo}',
                    text: '{mensaje}',
                    icon: '{tipo}', 
                    allowOutsideClick: false, 
                    showCloseButton: false, 
                    confirmButtonText: 'Aceptar'
                }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
        }
    }
}