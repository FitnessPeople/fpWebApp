using System;
using System.Data;
using System.Data.SqlClient;
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
                        CargarSedes();
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        DateTime dtHoy = DateTime.Now;
                        txbFechaIni.Attributes.Add("type", "date");
                        txbFechaFin.Attributes.Add("type", "date");
                        txbFechaIni.Attributes.Add("min", dtHoy.Year.ToString() + "-" + String.Format("{0:MM}", dtHoy) + "-" + String.Format("{0:dd}", dtHoy));
                        txbFechaFin.Attributes.Add("min", dtHoy.Year.ToString() + "-" + String.Format("{0:MM}", dtHoy) + "-" + String.Format("{0:dd}", dtHoy));
                        divCrear.Visible = true;
                        CargarSedes();
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
                                string strQuery = "DELETE FROM DisponibilidadEspecialistas " +
                                    " WHERE idDisponibilidad = " + Request.QueryString["deleteid"].ToString();
                                clasesglobales cg = new clasesglobales();
                                string mensaje = cg.TraerDatosStr(strQuery);

                                if (mensaje == "OK")
                                {
                                    cg.InsertarLog(Session["idusuario"].ToString(), "DisponibilidadEspecialistas", "Elimina", "El usuario eliminó un nuevo espacio del especialista.", "", "");
                                }
                            }
                            catch (SqlException ex)
                            {
                                string mensaje = ex.Message;
                            }
                            Response.Redirect("agenda");
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

        private void CargarSedes()
        {
            int idSedeUsuario = Convert.ToInt32(Session["idSede"]);

            clasesglobales cg = new clasesglobales();

            int? idSede = (idSedeUsuario == 11) ? (int?)null : idSedeUsuario;

            DataTable dt = cg.ConsultaCargarSedesPorId(idSede, "Gimnasio");

            ddlSedes.Items.Clear();
            ddlSedesSesion.Items.Clear();
            ddlSedes.Items.Add(new ListItem("Todas", "0"));

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlSedes.DataSource = dt;
                ddlSedes.DataValueField = "idSede";
                ddlSedes.DataTextField = "NombreSede";
                ddlSedes.DataBind();

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
                                    if (dtFechaIniCita.Hour < 6 || dtFechaIniCita.Hour >= 21)
                                    {
                                        script = @"
                                            Swal.fire({
                                                title: 'Advertencia',
                                                text: 'Horario fuera del intervalo de la sesión.',
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
                                                            "(idClaseGrupal, idEntrenador, idSede, FechaInicio, FechaFinal, " +
                                                            "CupoMaximo, Estado) " +
                                                            "VALUES (" + ddlClasesGrupales.SelectedItem.Value.ToString() + ", " +
                                                            "" + ddlEntrenadores.SelectedItem.Value.ToString() + ", " +
                                                            "" + ddlSedesSesion.SelectedItem.Value.ToString() + ", " +
                                                            "'" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                                            "'" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                                            "" + txbCupo.Value.ToString() + " " +
                                                            "'Programado') ";

                                                        string mensaje = cg.TraerDatosStr(strQuery);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            strQuery = "INSERT INTO ProgramacionClasesGrupales " +
                                                "(idClaseGrupal, idEntrenador, idSede, FechaInicio, FechaFinal, " +
                                                "CupoMaximo, Estado) " +
                                                "VALUES (" + ddlClasesGrupales.SelectedItem.Value.ToString() + ", " +
                                                "" + ddlEntrenadores.SelectedItem.Value.ToString() + ", " +
                                                "" + ddlSedesSesion.SelectedItem.Value.ToString() + ", " +
                                                "'" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                                "'" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                                "" + txbCupo.Value.ToString() + " " +
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
                                                window.location.href = 'agenda';
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

        }
    }
}