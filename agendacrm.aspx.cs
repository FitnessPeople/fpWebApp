using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class agendacrm : System.Web.UI.Page
    {
        private string _strEventos;
        public string EstadosCRM_Json { get; set; }
        protected string strEventos { get { return this._strEventos; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarPlanes();
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
                        ListaEstadosCRM();
                        CargarAgenda();
                        CargarDatosContacto(0);


                        //txbFechaInicio.Attributes.Add("type", "date");

                        DateTime dtHoy = DateTime.Now;
                        DateTime dt60 = DateTime.Now.AddMonths(2);
                        //txbFechaInicio.Attributes.Add("min", dtHoy.Year.ToString() + "-" + String.Format("{0:MM}", dtHoy) + "-" + String.Format("{0:dd}", dtHoy));
                        //txbFechaInicio.Attributes.Add("max", dt60.Year.ToString() + "-" + String.Format("{0:MM}", dt60) + "-" + String.Format("{0:dd}", dt60));
                        //txbFechaInicio.Text = String.Format("{0:yyyy-MM-dd}", dtHoy);

                        //txbWompi.Attributes.Add("type", "number");
                        //txbWompi.Attributes.Add("min", "0");
                        //txbWompi.Attributes.Add("max", "10000000");
                        //txbWompi.Attributes.Add("step", "100");
                        //txbWompi.Text = "0";

                        //txbDatafono.Attributes.Add("type", "number");
                        //txbDatafono.Attributes.Add("min", "0");
                        //txbDatafono.Attributes.Add("max", "10000000");
                        //txbDatafono.Attributes.Add("step", "100");
                        //txbDatafono.Text = "0";

                        //txbEfectivo.Attributes.Add("type", "number");
                        //txbEfectivo.Attributes.Add("min", "0");
                        //txbEfectivo.Attributes.Add("max", "10000000");
                        //txbEfectivo.Attributes.Add("step", "100");
                        //txbEfectivo.Text = "0";

                        //txbTransferencia.Attributes.Add("type", "number");
                        //txbTransferencia.Attributes.Add("min", "0");
                        //txbTransferencia.Attributes.Add("max", "10000000");
                        //txbTransferencia.Attributes.Add("step", "100");
                        //txbTransferencia.Text = "0";

                        //ViewState.Add("precioBase", 0);
                        //ltPrecioBase.Text = "$0";
                        //ltDescuento.Text = "";
                        //ltPrecioFinal.Text = "$0";
                        //ltAhorro.Text = "$0";
                        ///ltConDescuento.Text = "$0";

                        //ltNombrePlan.Text = "Nombre del plan";

                        //btnMes1.Attributes.Add("style", "padding: 6px 9px;");

                        //CargarPlanesAfiliado();
                        // MesesDisabled();

                        //string strData = listarDetalle();
                        //ltDetalleWompi.Text = strData;


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
        protected void btnMes_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string mes = btn.Text;
            // Aquí haces lo que necesites con el número del mes seleccionado
            // Por ejemplo:
            Response.Write("Mes seleccionado: " + mes);
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
        private void ListaEstadosCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstadossCRM();

            var lista = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                lista.Add(new { id = dr["idEstadoCRM"].ToString(), nombre = dr["NombreEstadoCRM"].ToString() });
            }

            EstadosCRM_Json = JsonConvert.SerializeObject(lista);

            dt.Dispose();
        }
        private void CargarDatosContacto(int idContacto)
        {
            bool respuesta = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarContactosCRMPorId(idContacto, out respuesta);

            rptContenido.DataSource = dt;
            rptContenido.DataBind();
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
                    _strEventos += "doc: '" + dt.Rows[i]["DocumentoAfiliado"].ToString() + "',\r\n";
                    _strEventos += "title: '" + dt.Rows[i]["NombreContacto"].ToString() + "',\r\n";
                    _strEventos += "start: '" + dt.Rows[i]["FechaProximoCon1"].ToString() + "',\r\n";
                    _strEventos += "end: '" + dt.Rows[i]["FechaProximoCon1"].ToString() + "',\r\n";
                    _strEventos += "idEstadoCRM: '" + dt.Rows[i]["idEstadoCRM"].ToString() + "',\r\n";


                    if (dt.Rows[i]["idContacto"].ToString() != "")
                    {
                        _strEventos += "color: '" + dt.Rows[i]["ColorHexaCRM"].ToString() + "',\r\n";
                        _strEventos += "btnAsignar: 'none',\r\n";
                    }
                    else
                    {
                        _strEventos += "color: '##198754',\r\n";
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

        #region zona modal
        private void CargarPlanes()
        {

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanes();

            if (dt.Rows.Count > 0)
            {
                PlaceHolder ph = ((PlaceHolder)this.FindControl("phPlanes"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Button btn = new Button();
                    btn.Text = dt.Rows[i]["NombrePlan"].ToString();
                    btn.CssClass = "btn btn-" + dt.Rows[i]["NombreColorPlan"].ToString() + " btn-outline btn-block small font-bold";
                    btn.ToolTip = dt.Rows[i]["NombrePlan"].ToString();
                    btn.Command += new CommandEventHandler(btn_Click);
                    btn.CommandArgument = dt.Rows[i]["idPlan"].ToString();
                    btn.ID = dt.Rows[i]["idPlan"].ToString();
                    //ph.Controls.Add(btn);
                }
            }
            dt.Dispose();
        }

        private void btn_Click(object sender, CommandEventArgs e)
        {
            string strQuery = "SELECT * " +
                "FROM planes " +
                "WHERE idPlan = " + e.CommandArgument;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ViewState["idPlan"] = dt.Rows[0]["idPlan"].ToString();
            ViewState["nombrePlan"] = dt.Rows[0]["NombrePlan"].ToString();
            ViewState["precioBase"] = Convert.ToInt32(dt.Rows[0]["PrecioBase"].ToString());
            ViewState["descuentoMensual"] = "";
            ViewState["mesesMaximo"] = Convert.ToDouble(dt.Rows[0]["MesesMaximo"].ToString());

            //divPanelResumen.Attributes.Remove("class");
            //divPanelResumen.Attributes.Add("class", "panel panel-" + dt.Rows[0]["NombreColorPlan"].ToString());

            //ltPrecioBase.Text = "$" + String.Format("{0:N0}", ViewState["precioBase"]);
            //ltPrecioFinal.Text = ltPrecioBase.Text;

            //CalculoPrecios("1");
            //ActivarBotones("1");
            ////ActivarCortesia("0");

            //ltDescuento.Text = "0%";
            //ltAhorro.Text = "$0";
            //ltConDescuento.Text = "$0";
            //ltDescripcion.Text = "<b>Características</b>: " + dt.Rows[0]["DescripcionPlan"].ToString() + "<br />";

            //ltNombrePlan.Text = "<b>Plan " + ViewState["nombrePlan"].ToString() + "</b>";

            //MesesEnabled();
            ScriptManager.RegisterStartupScript(upAsesorCRM, upAsesorCRM.GetType(), "abrirModal", @"
    $('.modal').modal('hide'); // Oculta cualquier otra modal
    setTimeout(function() {
        $('#modal-view-event').modal('show');
    }, 300);", true);


        }

    //    private void MesesEnabled()
    //    {
    //        MesesDisabled();
    //        if (Convert.ToInt32(btnMes1.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
    //        {
    //            btnMes1.Enabled = true;
    //        }
    //        if (Convert.ToInt32(btnMes2.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
    //        {
    //            btnMes2.Enabled = true;
    //        }
    //        if (Convert.ToInt32(btnMes3.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
    //        {
    //            btnMes3.Enabled = true;
    //        }
    //        if (Convert.ToInt32(btnMes4.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
    //        {
    //            btnMes4.Enabled = true;
    //        }
    //        if (Convert.ToInt32(btnMes5.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
    //        {
    //            btnMes5.Enabled = true;
    //        }
    //        if (Convert.ToInt32(btnMes6.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
    //        {
    //            btnMes6.Enabled = true;
    //        }
    //        if (Convert.ToInt32(btnMes7.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
    //        {
    //            btnMes7.Enabled = true;
    //        }
    //        if (Convert.ToInt32(btnMes8.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
    //        {
    //            btnMes8.Enabled = true;
    //        }
    //        if (Convert.ToInt32(btnMes9.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
    //        {
    //            btnMes9.Enabled = true;
    //        }
    //        if (Convert.ToInt32(btnMes10.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
    //        {
    //            btnMes10.Enabled = true;
    //        }
    //        if (Convert.ToInt32(btnMes11.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
    //        {
    //            btnMes11.Enabled = true;
    //        }
    //        if (Convert.ToInt32(btnMes12.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
    //        {
    //            btnMes12.Enabled = true;
    //        }

    //    }

    //    private void MesesDisabled()
    //    {
    //        btnMes1.Enabled = false;
    //        btnMes2.Enabled = false;
    //        btnMes3.Enabled = false;
    //        btnMes4.Enabled = false;
    //        btnMes5.Enabled = false;
    //        btnMes6.Enabled = false;
    //        btnMes7.Enabled = false;
    //        btnMes8.Enabled = false;
    //        btnMes9.Enabled = false;
    //        btnMes10.Enabled = false;
    //        btnMes11.Enabled = false;
    //        btnMes12.Enabled = false;
    //    }

    //    protected void btnMes1_Click(object sender, EventArgs e)
    //    {
    //        CalculoPrecios(btnMes1.Text.ToString());
    //        ActivarBotones(btnMes1.Text.ToString());
    //        //ActivarCortesia("1");
    //        //ActivarRegalo("0");
    //        //LimpiarFormulario();
    //        ScriptManager.RegisterStartupScript(upAsesorCRM, upAsesorCRM.GetType(), "abrirModal", @"
    //$('.modal').modal('hide'); // Oculta cualquier otra modal
    //setTimeout(function() {
    //    $('#modal-view-event').modal('show');
    //}, 300);", true);


    //    }

    //    protected void btnMes2_Click(object sender, EventArgs e)
    //    {
    //        CalculoPrecios(btnMes2.Text.ToString());
    //        ActivarBotones(btnMes2.Text.ToString());
    //        //ActivarCortesia("1");
    //        //ActivarRegalo("0");
    //        //LimpiarFormulario();
    //        ScriptManager.RegisterStartupScript(upAsesorCRM, upAsesorCRM.GetType(), "abrirModal", @"
    //$('.modal').modal('hide'); // Oculta cualquier otra modal
    //setTimeout(function() {
    //    $('#modal-view-event').modal('show');
    //}, 300);", true);


    //    }

    //    protected void btnMes3_Click(object sender, EventArgs e)
    //    {
    //        CalculoPrecios(btnMes3.Text.ToString());
    //        ActivarBotones(btnMes3.Text.ToString());
    //        //ActivarCortesia("2");
    //        //ActivarRegalo("0");
    //        //LimpiarFormulario();
    //        ScriptManager.RegisterStartupScript(upAsesorCRM, upAsesorCRM.GetType(), "abrirModal", @"
    //$('.modal').modal('hide'); // Oculta cualquier otra modal
    //setTimeout(function() {
    //    $('#modal-view-event').modal('show');
    //}, 300);", true);


    //    }

    //    protected void btnMes4_Click(object sender, EventArgs e)
    //    {
    //        CalculoPrecios(btnMes4.Text.ToString());
    //        ActivarBotones(btnMes4.Text.ToString());
    //        //ActivarCortesia("3");
    //        //ActivarRegalo("1");
    //        //LimpiarFormulario();
    //        ScriptManager.RegisterStartupScript(upAsesorCRM, upAsesorCRM.GetType(), "abrirModal", @"
    //$('.modal').modal('hide'); // Oculta cualquier otra modal
    //setTimeout(function() {
    //    $('#modal-view-event').modal('show');
    //}, 300);", true);


    //    }

    //    protected void btnMes5_Click(object sender, EventArgs e)
    //    {
    //        CalculoPrecios(btnMes5.Text.ToString());
    //        ActivarBotones(btnMes5.Text.ToString());
    //        //ActivarCortesia("3");
    //        //ActivarRegalo("1");
    //        //LimpiarFormulario();
    //        ScriptManager.RegisterStartupScript(upAsesorCRM, upAsesorCRM.GetType(), "abrirModal", @"
    //$('.modal').modal('hide'); // Oculta cualquier otra modal
    //setTimeout(function() {
    //    $('#modal-view-event').modal('show');
    //}, 300);", true);


    //    }

    //    protected void btnMes6_Click(object sender, EventArgs e)
    //    {
    //        CalculoPrecios(btnMes6.Text.ToString());
    //        ActivarBotones(btnMes6.Text.ToString());
    //        //ActivarCortesia("3");
    //        //ActivarRegalo("1");
    //        //LimpiarFormulario();
    //        ScriptManager.RegisterStartupScript(upAsesorCRM, upAsesorCRM.GetType(), "abrirModal", @"
    //$('.modal').modal('hide'); // Oculta cualquier otra modal
    //setTimeout(function() {
    //    $('#modal-view-event').modal('show');
    //}, 300);", true);


    //    }

    //    protected void btnMes7_Click(object sender, EventArgs e)
    //    {
    //        CalculoPrecios(btnMes7.Text.ToString());
    //        ActivarBotones(btnMes7.Text.ToString());
    //        //ActivarCortesia("3");
    //        //ActivarRegalo("1");
    //        //LimpiarFormulario();
    //        ScriptManager.RegisterStartupScript(upAsesorCRM, upAsesorCRM.GetType(), "abrirModal", @"
    //$('.modal').modal('hide'); // Oculta cualquier otra modal
    //setTimeout(function() {
    //    $('#modal-view-event').modal('show');
    //}, 300);", true);


    //    }

    //    protected void btnMes8_Click(object sender, EventArgs e)
    //    {
    //        CalculoPrecios(btnMes8.Text.ToString());
    //        ActivarBotones(btnMes8.Text.ToString());
    //        //ActivarCortesia("3");
    //        //ActivarRegalo("2");
    //        //LimpiarFormulario();
    //        ScriptManager.RegisterStartupScript(upAsesorCRM, upAsesorCRM.GetType(), "abrirModal", @"
    //$('.modal').modal('hide'); // Oculta cualquier otra modal
    //setTimeout(function() {
    //    $('#modal-view-event').modal('show');
    //}, 300);", true);


    //    }

    //    protected void btnMes9_Click(object sender, EventArgs e)
    //    {
    //        CalculoPrecios(btnMes9.Text.ToString());
    //        ActivarBotones(btnMes9.Text.ToString());
    //        //ActivarCortesia("3");
    //        //ActivarRegalo("2");
    //        //LimpiarFormulario();
    //        ScriptManager.RegisterStartupScript(upAsesorCRM, upAsesorCRM.GetType(), "abrirModal", @"
    //$('.modal').modal('hide'); // Oculta cualquier otra modal
    //setTimeout(function() {
    //    $('#modal-view-event').modal('show');
    //}, 300);", true);


    //    }

    //    protected void btnMes10_Click(object sender, EventArgs e)
    //    {
    //        CalculoPrecios(btnMes10.Text.ToString());
    //        ActivarBotones(btnMes10.Text.ToString());
    //        ///ActivarCortesia("4");
    //        //ActivarRegalo("2");
    //        //LimpiarFormulario();
    //        ScriptManager.RegisterStartupScript(upAsesorCRM, upAsesorCRM.GetType(), "abrirModal", @"
    //$('.modal').modal('hide'); // Oculta cualquier otra modal
    //setTimeout(function() {
    //    $('#modal-view-event').modal('show');
    //}, 300);", true);


    //    }

    //    protected void btnMes11_Click(object sender, EventArgs e)
    //    {
    //        CalculoPrecios(btnMes11.Text.ToString());
    //        ActivarBotones(btnMes11.Text.ToString());
    //        //ActivarCortesia("4");
    //        //ActivarRegalo("2");
    //        //LimpiarFormulario();
    //        ScriptManager.RegisterStartupScript(upAsesorCRM, upAsesorCRM.GetType(), "abrirModal", @"
    //$('.modal').modal('hide'); // Oculta cualquier otra modal
    //setTimeout(function() {
    //    $('#modal-view-event').modal('show');
    //}, 300);", true);


    //    }

    //    protected void btnMes12_Click(object sender, EventArgs e)
    //    {
    //        CalculoPrecios(btnMes12.Text.ToString());
    //        ActivarBotones(btnMes12.Text.ToString());
    //        //ActivarCortesia("4");
    //        //ActivarRegalo("3");
    //        //LimpiarFormulario();
    //        ScriptManager.RegisterStartupScript(upAsesorCRM, upAsesorCRM.GetType(), "abrirModal", @"
    //$('.modal').modal('hide'); // Oculta cualquier otra modal
    //setTimeout(function() {
    //    $('#modal-view-event').modal('show');
    //}, 300);", true);
    //    }

    //    private void CalculoPrecios(string strMes)
    //    {
    //        int intPrecioBase = Convert.ToInt32(ViewState["precioBase"]);
    //        double dobDescuento = (Convert.ToInt32(strMes) - 1) * Convert.ToDouble(ViewState["descuentoMensual"]);
    //        int intMeses = Convert.ToInt32(strMes);
    //        ViewState["meses"] = intMeses;
    //        double dobTotal = (intPrecioBase - ((intPrecioBase * dobDescuento) / 100)) * intMeses;
    //        ViewState["precio"] = Convert.ToString(Convert.ToInt32(dobTotal));
    //        double dobAhorro = ((intPrecioBase * dobDescuento) / 100) * intMeses;
    //        double dobConDescuento = (intPrecioBase - ((intPrecioBase * dobDescuento) / 100));

    //        ltPrecioBase.Text = "$" + String.Format("{0:N0}", intPrecioBase);
    //        ltDescuento.Text = dobDescuento.ToString() + "%";
    //        //ltPrecioFinal.Text = String.Format("{0:C0}", intTotal);
    //        ltPrecioFinal.Text = "$" + String.Format("{0:N0}", dobTotal);
    //        //ltAhorro.Text = String.Format("{0:C0}", intAhorro);
    //        ltAhorro.Text = "$" + String.Format("{0:N0}", dobAhorro);
    //        //ltConDescuento.Text = String.Format("{0:C0}", intConDescuento);
    //        //ltConDescuento.Text = "$" + String.Format("{0:N0}", dobConDescuento);

    //        //ltObservaciones.Text = "Valor sin descuento: $" + string.Format("{0:N0}", intPrecioBase) + "<br /><br />";
    //        //ltObservaciones.Text += "<b>Meses</b>: " + intMeses.ToString() + ".<br />";
    //        //ltObservaciones.Text += "<b>Descuento</b>: " + dobDescuento.ToString() + "%.<br />";
    //        //ltObservaciones.Text += "<b>Valor del mes con descuento</b>: $" + string.Format("{0:N0}", dobConDescuento) + ".<br />";
    //        //ltObservaciones.Text += "<b>Ahorro</b>: $" + string.Format("{0:N0}", dobAhorro) + ".<br />";
    //        //ltObservaciones.Text += "<b>Valor Total</b>: $" + string.Format("{0:N0}", dobTotal) + ".<br />";

    //        //ViewState["observaciones"] = ltObservaciones.Text.ToString().Replace("<b>", "").Replace("</b>", "").Replace("<br />", "\r\n");
    //        //ltValorTotal.Text = "($" + string.Format("{0:N0}", dobTotal) + ")";

    //        //string strDataWompi = Convert.ToBase64String(Encoding.Unicode.GetBytes(ViewState["DocumentoAfiliado"].ToString() + "_" + ViewState["precio"].ToString()));
    //        ////lbEnlaceWompi.Text = "https://fitnesspeoplecolombia.com/wompiplan?code=" + strDataWompi;
    //        //lbEnlaceWompi.Text = "<b>Enlace de pago Wompi:</b> <br />";
    //        //lbEnlaceWompi.Text += MakeTinyUrl("https://fitnesspeoplecolombia.com/wompiplan?code=" + strDataWompi);
    //        //hdEnlaceWompi.Value = MakeTinyUrl("https://fitnesspeoplecolombia.com/wompiplan?code=" + strDataWompi);
    //        //btnPortapaleles.Visible = true;
    //    }

    //    private void ActivarBotones(string strMes)
    //    {
    //        btnMes1.CssClass = btnMes1.CssClass.Replace("active", "");
    //        btnMes2.CssClass = btnMes2.CssClass.Replace("active", "");
    //        btnMes3.CssClass = btnMes3.CssClass.Replace("active", "");
    //        btnMes4.CssClass = btnMes4.CssClass.Replace("active", "");
    //        btnMes5.CssClass = btnMes5.CssClass.Replace("active", "");
    //        btnMes6.CssClass = btnMes6.CssClass.Replace("active", "");
    //        btnMes7.CssClass = btnMes7.CssClass.Replace("active", "");
    //        btnMes8.CssClass = btnMes8.CssClass.Replace("active", "");
    //        btnMes9.CssClass = btnMes9.CssClass.Replace("active", "");
    //        btnMes10.CssClass = btnMes10.CssClass.Replace("active", "");
    //        btnMes11.CssClass = btnMes11.CssClass.Replace("active", "");
    //        btnMes12.CssClass = btnMes12.CssClass.Replace("active", "");

    //        switch (strMes)
    //        {
    //            case "1":
    //                btnMes1.CssClass += " active";
    //                break;
    //            case "2":
    //                btnMes2.CssClass += " active";
    //                break;
    //            case "3":
    //                btnMes3.CssClass += " active";
    //                break;
    //            case "4":
    //                btnMes4.CssClass += " active";
    //                break;
    //            case "5":
    //                btnMes5.CssClass += " active";
    //                break;
    //            case "6":
    //                btnMes6.CssClass += " active";
    //                break;
    //            case "7":
    //                btnMes7.CssClass += " active";
    //                break;
    //            case "8":
    //                btnMes8.CssClass += " active";
    //                break;
    //            case "9":
    //                btnMes9.CssClass += " active";
    //                break;
    //            case "10":
    //                btnMes10.CssClass += " active";
    //                break;
    //            case "11":
    //                btnMes11.CssClass += " active";
    //                break;
    //            case "12":
    //                btnMes12.CssClass += " active";
    //                break;
    //            default:
    //                break;
    //        }
    //    }

        private void LimpiarFormulario()
        {
            //ltCortesias.Text = "";
            //ltRegalos.Text = "";
            //btn7dias.CssClass += btn7dias.CssClass.Replace("active", "");
            //btn15dias.CssClass = btn15dias.CssClass.Replace("active", "");
            //btn30dias.CssClass = btn30dias.CssClass.Replace("active", "");
            //btn60dias.CssClass = btn60dias.CssClass.Replace("active", "");
            //btnRegalo1.CssClass = btnRegalo1.CssClass.Replace("active", "");
            //btnRegalo2.CssClass = btnRegalo2.CssClass.Replace("active", "");
            //btnRegalo3.CssClass = btnRegalo3.CssClass.Replace("active", "");
        }

        #endregion
    }
}