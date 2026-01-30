using System;
using System.Data;
using System.Globalization;

namespace fpWebApp
{
    public partial class calendariofpadmin : System.Web.UI.Page
    {
        private string _strEventos;
        protected string strEventos { get { return this._strEventos; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Usuarios");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        CargarCalendario();
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        DateTime dtHoy = DateTime.Now;
                        txbFechaIni.Attributes.Add("type", "date");
                        txbFechaIni.Attributes.Add("min", dtHoy.Year.ToString() + "-" + String.Format("{0:MM}", dtHoy) + "-" + String.Format("{0:dd}", dtHoy));
                        divCrear.Visible = true;
                        CargarCalendario();
                        DateTime fechaActual = DateTime.Now;
                        DateTime fechaDestino = new DateTime(2025, 8, 29);
                        TimeSpan diferencia = fechaDestino - fechaActual;
                        ltDias.Text = Convert.ToInt32(diferencia.TotalDays).ToString();
;                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        //btnEliminar.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("logout.aspx");
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

        private void CargarCalendario()
        {
            string strQuery = "SELECT a.*, u.NombreUsuario " +
                "FROM AvancesFP a " +
                "LEFT JOIN Usuarios u ON u.idUsuario = a.idUsuario";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            _strEventos = "events: [\r\n";

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IFormatProvider provider = new CultureInfo("en-US");
                    DateTime dtIni = Convert.ToDateTime(dt.Rows[i]["FechaHoraInicio"].ToString());
                    DateTime dtFin = Convert.ToDateTime(dt.Rows[i]["FechaHoraFinal"].ToString());

                    string strFechaHoraIni = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtIni);
                    //string strFechaHoraIni = String.Format("{0:yyyy-MM-dd}", dtIni);
                    string strFechaHoraFin = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtFin);
                    //string strFechaHoraFin = String.Format("{0:yyyy-MM-dd}", dtFin);

                    _strEventos += "{\r\n";
                    _strEventos += "id: '" + dt.Rows[i]["idAvance"].ToString() + "',\r\n";
                    _strEventos += "start: '" + strFechaHoraIni + "',\r\n";
                    _strEventos += "end: '" + strFechaHoraFin + "',\r\n";
                    //_strEventos += "className: 'bg-primary',\r\n";

                    if (dt.Rows[i]["Tipo"].ToString() == "Reunión")
                    {
                        _strEventos += "color: '#ed5565',\r\n"; //danger
                        _strEventos += "icon: 'users-between-lines',\r\n";
                    }
                    if (dt.Rows[i]["Tipo"].ToString() == "Avance")
                    {
                        _strEventos += "color: '#F8AC59',\r\n"; //warning
                        _strEventos += "icon: 'code-branch',\r\n";

                    }

                    _strEventos += "title: '" + dt.Rows[i]["NombreUsuario"].ToString() + "',\r\n";
                    _strEventos += "description: '" + dt.Rows[i]["Descripcion"].ToString() + "',\r\n";
                    _strEventos += "btnEliminar: 'none',\r\n";
                    _strEventos += "allDay: false,\r\n";
                    _strEventos += "},\r\n";
                }
            }

            dt.Dispose();

            AgregarFestivos(_strEventos, "2026");

            _strEventos += "],\r\n";

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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            DateTime dtFechaIni = Convert.ToDateTime(txbFechaIni.Value.ToString());
            DateTime dtFechaIniCita = Convert.ToDateTime(dtFechaIni.ToString("yyyy-MM-dd") + " " + txbHoraIni.Value.ToString());
            DateTime dtFechaFinCita = dtFechaIniCita.AddHours(1);

            string strQuery = "INSERT INTO AvancesFP " +
                "(idUsuario, FechaHoraInicio, FechaHoraFinal, Descripcion, Tipo) " +
                "VALUES (" + Session["idUsuario"].ToString() + ", '" + dtFechaIniCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                "'" + dtFechaFinCita.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                "'" + txbDescripcion.Text.ToString() + "', '" + ddlTipo.SelectedItem.Value.ToString() + "') ";
            
            clasesglobales cg = new clasesglobales();
            string mensaje = cg.TraerDatosStr(strQuery);

            Response.Redirect("calendariofpadmin");
        }
    }
}