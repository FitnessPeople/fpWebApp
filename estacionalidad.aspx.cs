﻿using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NPOI.OpenXmlFormats.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Services;

namespace fpWebApp
{
    public partial class estacionalidad : System.Web.UI.Page
    {
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
                        //CargarSedes();
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        DateTime dtHoy = DateTime.Now;
                        CargarAsesores();
                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        //btnEliminar.Visible = true;
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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static int GuardarEvento(string title, string start, bool allDay, string bgcolor)
        {
            string dtInicio = Convert.ToDateTime(start).ToString("yyyy-MM-dd");
            string dtFin = dtInicio;
            string strQuery = "INSERT INTO estacionalidad (Titulo, FechaInicio, FechaFin, TodoElDia, Color) " +
            "VALUES ('" + title + "', '" + dtInicio + "', '" + dtFin + "', " + allDay + ", '" + bgcolor + "')";
            clasesglobales cg = new clasesglobales();
            cg.TraerDatosStr(strQuery);

            strQuery = "SELECT LAST_INSERT_ID()";
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string EliminarEvento(string id)
        {
            string strQuery = "DELETE FROM estacionalidad WHERE idEstacionalidad = " + id;
            clasesglobales cg = new clasesglobales();
            cg.TraerDatosStr(strQuery);

            return "Ok";
        }

        [WebMethod]
        public static List<Feriado> ObtenerFeriados()
        {
            List<Feriado> lista = new List<Feriado>();

            string strQuery = "SELECT * FROM festivos";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lista.Add(new Feriado
                {
                    fecha = Convert.ToDateTime(dt.Rows[i]["Fecha"]).ToString("yyyy-MM-dd"),
                    descripcion = dt.Rows[i]["Titulo"].ToString()
                });
            }

            return lista;
        }

        public class Feriado
        {
            public string fecha { get; set; }
            public string descripcion { get; set; }
        }

        //private void CargarAgenda()
        //{
        //    clasesglobales cg = new clasesglobales();
        //    //DataTable dt = cg.ConsultaCargarAgenda(int.Parse(ddlSedes.SelectedItem.Value.ToString()));
        //    DataTable dt = new DataTable();

        //    _strEventos = "events: [\r\n";

        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            IFormatProvider provider = new CultureInfo("en-US");
        //            DateTime dtIni = Convert.ToDateTime(dt.Rows[i]["FechaHoraIni"].ToString(), provider);
        //            DateTime dtFin = Convert.ToDateTime(dt.Rows[i]["FechaHoraFin"].ToString(), provider);

        //            string strFechaHoraIni = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtIni);
        //            string strFechaHoraFin = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtFin);

        //            _strEventos += "{\r\n";
        //            _strEventos += "id: '" + dt.Rows[i]["idDisponibilidad"].ToString() + "',\r\n";
        //            //_strEventos += "title: '" + dt.Rows[i]["NombreEmpleado"].ToString() + "',\r\n";
        //            //_strEventos += "start: '" + dt.Rows[i]["FechaHoraIni"].ToString() + "',\r\n";
        //            _strEventos += "start: '" + strFechaHoraIni + "',\r\n";
        //            //_strEventos += "end: '" + dt.Rows[i]["FechaHoraFin"].ToString() + "',\r\n";
        //            _strEventos += "end: '" + strFechaHoraFin + "',\r\n";
        //            //_strEventos += "className: 'bg-primary',\r\n";

        //            if (dt.Rows[i]["idAfiliado"].ToString() != "")
        //            {
        //                if (dt.Rows[i]["Cancelada"].ToString() != "0")
        //                {
        //                    _strEventos += "color: '#ed5565',\r\n"; //danger
        //                    _strEventos += "title: '" + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
        //                    _strEventos += "description: 'Cita cancelada: " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
        //                    _strEventos += "icon: 'id-card',\r\n";
        //                    _strEventos += "btnEliminar: 'none',\r\n";
        //                }
        //                else
        //                {
        //                    _strEventos += "color: '#F8AC59',\r\n"; //warning
        //                    _strEventos += "title: '" + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
        //                    _strEventos += "description: 'Cita asignada: " + dt.Rows[i]["NombreAfiliado"].ToString() + " " + dt.Rows[i]["ApellidoAfiliado"].ToString() + "',\r\n";
        //                    _strEventos += "icon: 'id-card',\r\n";
        //                    _strEventos += "btnEliminar: 'none',\r\n";
        //                }
        //            }
        //            else
        //            {
        //                _strEventos += "title: '" + dt.Rows[i]["NombreEmpleado"].ToString() + "',\r\n";
        //                _strEventos += "color: '#1ab394',\r\n";
        //                _strEventos += "description: 'Cita disponible.',\r\n";
        //                _strEventos += "icon: 'user-doctor',\r\n";
        //                _strEventos += "btnEliminar: 'inline',\r\n";
        //            }

        //            //_strEventos += "color: '#DBADFF',\r\n";
        //            //_strEventos += "todoeldia: 0,\r\n";
        //            _strEventos += "allDay: false,\r\n";
        //            _strEventos += "},\r\n";
        //        }
        //    }

        //    dt.Dispose();

        //    AgregarFestivos(_strEventos, "2025");

        //}

        /// <summary>
        /// Agrega los festivos del año al calendario
        /// </summary>
        /// <param name="eventos"></param>
        /// <param name="anho"></param>
        /// <returns></returns>
        //private string AgregarFestivos(string eventos, string anho)
        //{
        //    //https://www.festivos.com.co/calendario
        //    _strEventos = eventos;

        //    if (anho == "2025")
        //    {
        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-01-01',\r\n";
        //        _strEventos += "end: '2025-01-01',\r\n";
        //        _strEventos += "title: 'Año nuevo',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-01-06',\r\n";
        //        _strEventos += "end: '2025-01-06',\r\n";
        //        _strEventos += "title: 'Reyes magos',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-03-24',\r\n";
        //        _strEventos += "end: '2025-03-24',\r\n";
        //        _strEventos += "title: 'Día de San José',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-04-17',\r\n";
        //        _strEventos += "end: '2025-04-17',\r\n";
        //        _strEventos += "title: 'Jueves Santo',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-04-18',\r\n";
        //        _strEventos += "end: '2025-04-18',\r\n";
        //        _strEventos += "title: 'Viernes Santo',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-05-01',\r\n";
        //        _strEventos += "end: '2025-05-01',\r\n";
        //        _strEventos += "title: 'Día del Trabajo',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-06-02',\r\n";
        //        _strEventos += "end: '2025-06-02',\r\n";
        //        _strEventos += "title: 'Ascensión de Jesús',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-06-23',\r\n";
        //        _strEventos += "end: '2025-06-23',\r\n";
        //        _strEventos += "title: 'Corpus Christi',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-06-30',\r\n";
        //        _strEventos += "end: '2025-06-30',\r\n";
        //        _strEventos += "title: 'Sagrado Corazón de Jesús',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-07-01',\r\n";
        //        _strEventos += "end: '2025-07-01',\r\n";
        //        _strEventos += "title: '20%',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#009900',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-07-20',\r\n";
        //        _strEventos += "end: '2025-07-20',\r\n";
        //        _strEventos += "title: 'Día de la Independencia',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-08-07',\r\n";
        //        _strEventos += "end: '2025-08-07',\r\n";
        //        _strEventos += "title: 'Batalla de Boyacá',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-08-18',\r\n";
        //        _strEventos += "end: '2025-08-18',\r\n";
        //        _strEventos += "title: 'Asunción de la virgen',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-10-13',\r\n";
        //        _strEventos += "end: '2025-10-13',\r\n";
        //        _strEventos += "title: 'Día de la raza',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-11-03',\r\n";
        //        _strEventos += "end: '2025-11-03',\r\n";
        //        _strEventos += "title: 'Todos los santos',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-11-17',\r\n";
        //        _strEventos += "end: '2025-11-17',\r\n";
        //        _strEventos += "title: 'Independencia de Cartagena',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-12-08',\r\n";
        //        _strEventos += "end: '2025-12-08',\r\n";
        //        _strEventos += "title: 'Inmaculada concepción',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "{\r\n";
        //        _strEventos += "start: '2025-12-25',\r\n";
        //        _strEventos += "end: '2025-12-25',\r\n";
        //        _strEventos += "title: 'Navidad',\r\n";
        //        _strEventos += "rendering: 'background',\r\n";
        //        _strEventos += "color: '#ff9f89',\r\n";
        //        _strEventos += "allDay: true,\r\n";
        //        _strEventos += "display: 'background',\r\n";
        //        _strEventos += "},\r\n";

        //        _strEventos += "],\r\n";
        //    }

        //    if (anho == "2026")
        //    {

        //    }

        //    return eventos;
        //}

        private void CargarSedes()
        {
            //clasesglobales cg = new clasesglobales();
            //DataTable dt = cg.ConsultaCargarSedes("Gimnasio");

            //ddlSedes.Items.Clear();
            //ddlSedes.DataSource = dt;
            //ddlSedes.DataBind();

            ////ddlSedesCita.Items.Clear();
            ////ddlSedesCita.DataSource = dt;
            ////ddlSedesCita.DataBind();

            //dt.Dispose();

            //ltSede.Text = ddlSedes.SelectedItem.Text.ToString();
            //CargarAgenda();
        }

        private void CargarAsesores()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarAsesores();

            ///ddlAsesores.DataSource = dt;
            //ddlAsesores.DataBind();

            dt.Dispose();
        }

        //protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlSedes.SelectedItem.Value.ToString() != "")
        //    {
        //        ltSede.Text = ddlSedes.SelectedItem.Text.ToString();
        //        CargarAgenda();
        //    }
        //}
    }
}