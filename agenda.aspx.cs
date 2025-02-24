using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class agenda : System.Web.UI.Page
    {
        OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
        private string _strEventos;
        protected string strEventos { get { return this._strEventos; } }
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
                        CargarSedes();
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        divCrear.Visible = true;
                        CargarSedes();
                        CargarEspecialistas();
                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        btnEliminar.Visible = true;
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
            ltSede.Text = ddlSedes.SelectedItem.Text.ToString();
            string strQuery = "SELECT *, DATE_FORMAT(FechaHoraInicio, \"%m/%d/%Y %r\") FechaHoraIni, " +
                "DATE_FORMAT(FechaHoraFinal, \"%m/%d/%Y %r\") FechaHoraFin " +
                "FROM DisponibilidadEspecialistas de, Especialistas e " +
                "WHERE de.idSede = " + ddlSedes.SelectedItem.Value.ToString() + " " +
                "AND de.idEspecialista = e.idEspecialista ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            _strEventos = "events: [\r\n";

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _strEventos += "{\r\n";
                    _strEventos += "id: '" + dt.Rows[i]["idDisponibilidad"].ToString() + "',\r\n";
                    _strEventos += "title: '" + dt.Rows[i]["NombreEspecialista"].ToString() + " " + dt.Rows[i]["ApellidoEspecialista"].ToString() + "',\r\n";
                    _strEventos += "description: 'Especialista',\r\n";
                    _strEventos += "start: '" + dt.Rows[i]["FechaHoraIni"].ToString() + "',\r\n";
                    _strEventos += "end: '" + dt.Rows[i]["FechaHoraFin"].ToString() + "',\r\n";
                    //_strEventos += "className: 'bg-primary',\r\n";
                    _strEventos += "color: '" + dt.Rows[i]["ColorEspecialista"].ToString() + "',\r\n";
                    //_strEventos += "color: '#DBADFF',\r\n";
                    //_strEventos += "todoeldia: 0,\r\n";
                    _strEventos += "allDay: false,\r\n";
                    _strEventos += "},\r\n";
                }
            }

            dt.Dispose();

            // Agregar festivos 2024

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-01-01',\r\n";
            _strEventos += "end: '2024-01-01',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-01-08',\r\n";
            _strEventos += "end: '2024-01-08',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-03-25',\r\n";
            _strEventos += "end: '2024-03-25',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-03-28',\r\n";
            _strEventos += "end: '2024-03-28',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-03-29',\r\n";
            _strEventos += "end: '2024-03-29',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-03-31',\r\n";
            _strEventos += "end: '2024-03-31',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-05-01',\r\n";
            _strEventos += "end: '2024-05-01',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-05-13',\r\n";
            _strEventos += "end: '2024-05-13',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-06-03',\r\n";
            _strEventos += "end: '2024-06-03',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-06-10',\r\n";
            _strEventos += "end: '2024-06-10',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-07-01',\r\n";
            _strEventos += "end: '2024-07-01',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-07-20',\r\n";
            _strEventos += "end: '2024-07-20',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-08-07',\r\n";
            _strEventos += "end: '2024-08-07',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-08-19',\r\n";
            _strEventos += "end: '2024-08-19',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-10-14',\r\n";
            _strEventos += "end: '2024-10-14',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-11-04',\r\n";
            _strEventos += "end: '2024-11-04',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-11-11',\r\n";
            _strEventos += "end: '2024-11-11',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-12-08',\r\n";
            _strEventos += "end: '2024-12-08',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "{\r\n";
            _strEventos += "start: '2024-12-25',\r\n";
            _strEventos += "end: '2024-12-25',\r\n";
            _strEventos += "rendering: 'background',\r\n";
            _strEventos += "color: '#ff9f89',\r\n";
            _strEventos += "allDay: true,\r\n";
            _strEventos += "},\r\n";

            _strEventos += "],\r\n";

        }

        private void CargarSedes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSedes("Gimnasio");

            ddlSedes.Items.Clear();
            ddlSedes.DataSource = dt;
            ddlSedes.DataBind();

            ddlSedesCita.Items.Clear();
            ddlSedesCita.DataSource = dt;
            ddlSedesCita.DataBind();

            dt.Dispose();

            CargarAgenda();
        }

        private void CargarEspecialistas()
        {
            string strQuery = "SELECT idEspecialista, CONCAT(NombreEspecialista, ' ', ApellidoEspecialista) AS NombreCompleto FROM Especialistas " +
                "WHERE EstadoEspecialista = 'Activo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlEspecialistas.DataSource = dt;
            ddlEspecialistas.DataBind();

            dt.Dispose();
        }

        protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSedes.SelectedItem.Value.ToString() != "")
            {
                CargarAgenda();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //Insertamos en la tabla DisponibilidadEspecialistas
            //string fechahorainicio = txbFechaIni.Value.ToString() + " " + txbHoraIni.Value.ToString();
            //string fechahorafin = txbFechaFin.Value.ToString() + " " + txbHoraFin.Value.ToString();
            
            //DateTime dtFechaIniCita = Convert.ToDateTime(fechahorainicio);
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
                            string strQuery = "SELECT * FROM DisponibilidadEspecialistas " +
                                "WHERE idSede = " + ddlSedesCita.SelectedItem.Value.ToString() + " " +
                                "AND (('" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "' > FechaHoraInicio AND '" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "' < FechaHoraFinal) " +
                                "OR ('" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "' > FechaHoraInicio AND '" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "' < FechaHoraFinal))";
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.TraerDatos(strQuery);

                            if (dt.Rows.Count == 0)
                            {
                                // Consulta si se cruza la cita del especialista con la fecha y hora de otra disponible
                                strQuery = "SELECT * FROM DisponibilidadEspecialistas " +
                                    "WHERE idEspecialista = " + ddlEspecialistas.SelectedItem.Value.ToString() + " " +
                                    "AND (('" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "' > FechaHoraInicio AND '" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "' < FechaHoraFinal) " +
                                    "OR ('" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "' > FechaHoraInicio AND '" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "' < FechaHoraFinal))";
                                DataTable dt1 = cg.TraerDatos(strQuery);

                                if (dt1.Rows.Count == 0)
                                {
                                    if (dtFechaIniCita.Hour < 6 || dtFechaIniCita.Hour >= 21)
                                    {
                                        ltMensaje.Text = "Horario fuera del intervalo del especialista";
                                        dtFechaIniCita = dtFechaFin;
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
                                                        strQuery = "INSERT INTO DisponibilidadEspecialistas " +
                                                            "(idEspecialista, idSede, FechaHoraInicio, FechaHoraFinal, idUsuario) " +
                                                            "VALUES (" + ddlEspecialistas.SelectedItem.Value.ToString() + ", " + ddlSedesCita.SelectedItem.Value.ToString() + ", " +
                                                            "'" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "', '" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                                            "" + Session["idusuario"].ToString() + ") ";
                                                        OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                                                        myConnection.Open();
                                                        command.ExecuteNonQuery();
                                                        command.Dispose();
                                                        myConnection.Close();
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            strQuery = "INSERT INTO DisponibilidadEspecialistas " +
                                                "(idEspecialista, idSede, FechaHoraInicio, FechaHoraFinal, idUsuario) " +
                                                "VALUES (" + ddlEspecialistas.SelectedItem.Value.ToString() + ", " + ddlSedesCita.SelectedItem.Value.ToString() + ", " +
                                                "'" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "', '" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                                "" + Session["idusuario"].ToString() + ") ";
                                            OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                                            myConnection.Open();
                                            command.ExecuteNonQuery();
                                            command.Dispose();
                                            myConnection.Close();
                                        }
                                        dtFechaIniCita = dtFechaFinCita;
                                    }
                                }
                                else
                                {
                                    ltMensaje.Text = "Ya esta ocupado este especialista en otra sede.";
                                    dtFechaIniCita = dtFechaFin;
                                }
                                dt1.Dispose();
                            }
                            else
                            {
                                ltMensaje.Text = "Ya esta ocupado este horario en la sede.";
                                dtFechaIniCita = dtFechaFin;
                            }
                            dt.Dispose();
                        }
                    }
                    catch (OdbcException ex)
                    {
                        string mensaje = ex.Message;
                        myConnection.Close();
                    }
                }
                else
                {
                    ltMensaje.Text = "Hora de inicio debe ser menor a hora final";
                }
            }
            Response.Redirect("agenda");
        }
    }
}