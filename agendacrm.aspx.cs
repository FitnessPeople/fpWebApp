using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class agendacrm : System.Web.UI.Page
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
                        CargarAgenda();

                        //CargarSedes();
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        CargarEstadosLead();
                        CargarAgenda();
                        CargarDatosContacto(6);

                        //CargarSedes();
                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        //btnAsignar.Visible = true;
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

        public string strOptionsStatus = string.Empty;
        private void CargarEstadosLead()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstadossCRM();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    strOptionsStatus += $"<option value='{dr["idEstadoCRM"]}'>{dr["NombreEstadoCRM"]}</option>";
                }
            }

            dt.Dispose();
        }

        private string EstadosCRM_Json = "";

        private void ListaEstadosCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstadossCRM();

            var lista = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                lista.Add(new { id = dr["idEstadoCRM"], nombre = dr["NombreEstadoCRM"] });
            }
            EstadosCRM_Json = Newtonsoft.Json.JsonConvert.SerializeObject(lista);

            ddlStatusLead.DataSource = dt;
            ddlStatusLead.DataBind();

            dt.Dispose();
        }


        private void CargarDatosContacto(int idContacto)
        {
            bool respuesta = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarContactosCRMPorId(idContacto, out respuesta);
            Session["contactoId"] = idContacto;
            //rptContenido.DataSource = dt;
            //rptContenido.DataBind();

            if (respuesta)
            {

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    //txbNombreContacto.Value = row["NombreContacto"].ToString();
                    //txbNombreContacto.Disabled = true;
                    //txbTelefonoContacto.Disabled = true;
                    //ddlEmpresa.Enabled = false;
                    //txbFechaPrim.Disabled = true;
                    //txbCorreoContacto.Disabled = true;
                    //txbValorPropuesta.Enabled = false;
                    //ArchivoPropuesta.Disabled = true;
                    //string telefono = Convert.ToString(row["TelefonoContacto"]);
                    //if (!string.IsNullOrEmpty(telefono) && telefono.Length == 10)
                    //{
                    //    txbTelefonoContacto.Value = $"{telefono.Substring(0, 3)} {telefono.Substring(3, 3)} {telefono.Substring(6, 4)}";
                    //}
                    //else
                    //{
                    //    txbTelefonoContacto.Value = row["TelefonoContacto"].ToString();
                    //}
                    //txbCorreoContacto.Value = row["EmailContacto"].ToString();
                    //if (row["idEmpresaCRM"].ToString() != "")
                    //    ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(dt.Rows[0]["idEmpresaCRM"].ToString())));
                    //else
                    //    ddlEmpresa.SelectedItem.Value = "0";

                    //ddlStatusLead.SelectedIndex = Convert.ToInt32(ddlStatusLead.Items.IndexOf(ddlStatusLead.Items.FindByValue(dt.Rows[0]["idEstadoCRM"].ToString())));
                    //txbFechaPrim.Value = Convert.ToDateTime(row["FechaPrimerCon"]).ToString("yyyy-MM-dd");
                    //txbFechaProx.Value = Convert.ToDateTime(row["FechaProximoCon"]).ToString("yyyy-MM-dd");
                    //int ValorPropuesta = Convert.ToInt32(dt.Rows[0]["ValorPropuesta"]);
                    //txbValorPropuesta.Text = ValorPropuesta.ToString("C0", new CultureInfo("es-CO"));
                    ////txaObservaciones.Value = row["observaciones"].ToString().Trim();
                }
            }
            else
            {
                DataRow row = dt.Rows[0];
                //txbNombreContacto.Value = row["Error"].ToString(); ;
            }
        }



        private void CargarAgenda()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarAgendaCRM();

            _strEventos = "events: [\r\n";

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _strEventos += "{\r\n";
                    _strEventos += "id: '" + dt.Rows[i]["idContacto"].ToString() + "',\r\n";
                    _strEventos += "title: '" + dt.Rows[i]["NombreContacto"].ToString() + "',\r\n";
                    _strEventos += "start: '" + dt.Rows[i]["FechaProximoCon1"].ToString() + "',\r\n";
                    _strEventos += "end: '" + dt.Rows[i]["FechaProximoCon1"].ToString() + "',\r\n";
                    _strEventos += "idEstadoCRM: '" + dt.Rows[i]["idEstadoCRM"].ToString() + "',\r\n";
                    //_strEventos += "className: 'bg-primary',\r\n";

                    if (dt.Rows[i]["idContacto"].ToString() != "")
                    {
                        _strEventos += "color: '#F8AC59',\r\n";
                        _strEventos += "description: 'Cita asignada: " + dt.Rows[i]["NombreContacto"].ToString() + " (" + dt.Rows[i]["TelefonoContacto"].ToString() + ")',\r\n";
                        _strEventos += "btnAsignar: 'none',\r\n";
                    }
                    else
                    {
                        _strEventos += "color: '#1ab394',\r\n";
                        _strEventos += "description: 'Agenda disponible.',\r\n";
                        _strEventos += "btnAsignar: 'inline',\r\n";
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

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarAgenda();
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