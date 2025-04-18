﻿using System;
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
        private string _strEventos;
        protected string strEventos { get { return this._strEventos; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Administrar agenda");
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
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarAgenda(int.Parse(ddlSedes.SelectedItem.Value.ToString()));

            _strEventos = "events: [\r\n";

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _strEventos += "{\r\n";
                    _strEventos += "id: '" + dt.Rows[i]["idDisponibilidad"].ToString() + "',\r\n";
                    _strEventos += "title: '" + dt.Rows[i]["NombreEmpleado"].ToString() + "',\r\n";
                    //_strEventos += "description: 'Especialista',\r\n";
                    _strEventos += "start: '" + dt.Rows[i]["FechaHoraIni"].ToString() + "',\r\n";
                    _strEventos += "end: '" + dt.Rows[i]["FechaHoraFin"].ToString() + "',\r\n";
                    //_strEventos += "className: 'bg-primary',\r\n";

                    if (dt.Rows[i]["idAfiliado"].ToString() != "")
                    {
                        _strEventos += "color: '#F8AC59',\r\n";
                        _strEventos += "description: 'Cita asignada: " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
                        _strEventos += "btnEliminar: 'none',\r\n";
                    }
                    else
                    {
                        _strEventos += "color: '#1ab394',\r\n";
                        _strEventos += "description: 'Cita disponible.',\r\n";
                        _strEventos += "btnEliminar: 'inline',\r\n";
                    }

                    //_strEventos += "color: '#DBADFF',\r\n";
                    //_strEventos += "todoeldia: 0,\r\n";
                    _strEventos += "allDay: false,\r\n";
                    _strEventos += "},\r\n";
                }
            }

            dt.Dispose();

            AgregarFestivos(_strEventos, "2025");

        }

        private string AgregarFestivos(string eventos, string anho)
        {
            _strEventos = eventos;

            if (anho == "2025")
            {
                _strEventos += "{\r\n";
                _strEventos += "start: '2025-01-01',\r\n";
                _strEventos += "end: '2025-01-01',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-01-06',\r\n";
                _strEventos += "end: '2025-01-06',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-03-24',\r\n";
                _strEventos += "end: '2025-03-24',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-04-17',\r\n";
                _strEventos += "end: '2025-04-17',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-04-18',\r\n";
                _strEventos += "end: '2025-04-18',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-05-01',\r\n";
                _strEventos += "end: '2025-05-01',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-06-02',\r\n";
                _strEventos += "end: '2025-06-02',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-06-23',\r\n";
                _strEventos += "end: '2025-06-23',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-06-30',\r\n";
                _strEventos += "end: '2025-06-23',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-07-20',\r\n";
                _strEventos += "end: '2025-07-20',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-08-07',\r\n";
                _strEventos += "end: '2025-08-07',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-08-18',\r\n";
                _strEventos += "end: '2025-08-18',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-10-13',\r\n";
                _strEventos += "end: '2025-10-13',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-11-03',\r\n";
                _strEventos += "end: '2025-11-03',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-11-17',\r\n";
                _strEventos += "end: '2025-11-17',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-12-08',\r\n";
                _strEventos += "end: '2025-12-08',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
                _strEventos += "},\r\n";

                _strEventos += "{\r\n";
                _strEventos += "start: '2025-12-25',\r\n";
                _strEventos += "end: '2025-12-25',\r\n";
                _strEventos += "rendering: 'background',\r\n";
                _strEventos += "color: '#ff9f89',\r\n";
                _strEventos += "allDay: true,\r\n";
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
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSedes("Gimnasio");

            ddlSedes.Items.Clear();
            ddlSedes.DataSource = dt;
            ddlSedes.DataBind();

            ddlSedesCita.Items.Clear();
            ddlSedesCita.DataSource = dt;
            ddlSedesCita.DataBind();

            dt.Dispose();

            ltSede.Text = ddlSedes.SelectedItem.Text.ToString();
            CargarAgenda();
        }

        private void CargarEspecialistas()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarEspecialistas();

            ddlEspecialistas.DataSource = dt;
            ddlEspecialistas.DataBind();

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
                                "WHERE (idSede = " + ddlSedesCita.SelectedItem.Value.ToString() + " " +
                                "OR idEspecialista  = " + ddlEspecialistas.SelectedItem.Value.ToString() + ") " +
                                "AND (('" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "' > FechaHoraInicio AND '" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "' < FechaHoraFinal) " +
                                "OR ('" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "' > FechaHoraInicio AND '" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "' < FechaHoraFinal))";
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.TraerDatos(strQuery);

                            if (dt.Rows.Count == 0)
                            {
                                // Consulta si se cruza la cita del especialista con la fecha y hora de otra disponible
                                //strQuery = "SELECT * FROM DisponibilidadEspecialistas " +
                                //    "WHERE idEspecialista = " + ddlEspecialistas.SelectedItem.Value.ToString() + " " +
                                //    "AND (('" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "' > FechaHoraInicio AND '" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "' < FechaHoraFinal) " +
                                //    "OR ('" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "' > FechaHoraInicio AND '" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "' < FechaHoraFinal))";
                                strQuery = "SELECT * FROM DisponibilidadEspecialistas " +
                                    "WHERE idEspecialista = " + ddlEspecialistas.SelectedItem.Value.ToString() + " " +
                                    "AND idSede != " + ddlSedes.SelectedItem.Value.ToString() + " " +
                                    "AND TIMESTAMPDIFF(MINUTE, '" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "', FechaHoraInicio) <= 60 " +
                                    "AND '" + dtFechaIniCita.ToString("yyyy-MM-dd") + "' = DATE(FechaHoraInicio) ";
                                DataTable dt1 = cg.TraerDatos(strQuery);

                                if (dt1.Rows.Count == 0)
                                {
                                    if (dtFechaIniCita.Hour < 6 || dtFechaIniCita.Hour >= 21)
                                    {
                                        ltMensaje.Text = "Horario fuera del intervalo del especialista";
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
                                                        strQuery = "INSERT INTO DisponibilidadEspecialistas " +
                                                            "(idEspecialista, idSede, FechaHoraInicio, FechaHoraFinal, idUsuarioCrea) " +
                                                            "VALUES (" + ddlEspecialistas.SelectedItem.Value.ToString() + ", " + ddlSedesCita.SelectedItem.Value.ToString() + ", " +
                                                            "'" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "', '" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                                            "" + Session["idusuario"].ToString() + ") ";
                                                        
                                                        string mensaje = cg.TraerDatosStr(strQuery);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            strQuery = "INSERT INTO DisponibilidadEspecialistas " +
                                                "(idEspecialista, idSede, FechaHoraInicio, FechaHoraFinal, idUsuarioCrea) " +
                                                "VALUES (" + ddlEspecialistas.SelectedItem.Value.ToString() + ", " + ddlSedesCita.SelectedItem.Value.ToString() + ", " +
                                                "'" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "', '" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                                "" + Session["idusuario"].ToString() + ") ";

                                            string mensaje = cg.TraerDatosStr(strQuery);
                                        }
                                        dtFechaIniCita = dtFechaFinCita;
                                    }
                                }
                                else
                                {
                                    ltMensaje.Text = "Ya esta ocupado este especialista en otra sede.";
                                    dtFechaIniCita = dtFechaFinCitaDia;
                                }
                                dt1.Dispose();
                            }
                            else
                            {
                                ltMensaje.Text = "Ya esta ocupado este horario en la sede.";
                                dtFechaIniCita = dtFechaFinCitaDia;
                            }
                            dt.Dispose();
                        }
                    }
                    catch (OdbcException ex)
                    {
                        string mensaje = ex.Message;
                    }
                }
                else
                {
                    ltMensaje.Text = "Hora de inicio debe ser menor a hora final";
                }
            }
            //Response.Redirect("agenda");
        }
    }
}