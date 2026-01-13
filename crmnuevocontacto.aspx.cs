using DocumentFormat.OpenXml.Presentation;
using Microsoft.Ajax.Utilities;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class crmnuevocontacto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListaMediosDePago();
                CargarPlanes();
                
                txbDocumento.Attributes.Add("autocomplete", "off");
                txbNombreContacto.Attributes.Add("autocomplete", "off");
                txbApellidoContacto.Attributes.Add("autocomplete", "off");
                txbCorreoContacto.Attributes.Add("autocomplete", "off");               
                txbTelefonoContacto.Attributes.Add("autocomplete", "off");

                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Nuevo CRM");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        //No tiene acceso a esta página
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    else
                    {
                        //Si tiene acceso a esta página
                        Session["idPregestion"] = "0";
                        txbFechaPrim.Attributes.Add("type", "date");
                        txbFechaPrim.Attributes.Add("min", DateTime.Now.ToString("yyyy-MM-dd"));
                        txbFechaPrim.Value = DateTime.Now.ToString("yyyy-MM-dd");
                        txbFechaProx.Attributes.Add("type", "date");
                        txbFechaProx.Value = DateTime.Now.ToString("yyyy-MM-dd");
                        txbCorreoContacto.Attributes.Add("type", "email");

                        btnAgregar.Text = "Agregar";

                        ddlAfiliadoOrigen.Enabled = true;
                        txbNombreContacto.Enabled = true;
                        txbApellidoContacto.Disabled = false;
                        txbDocumento.Enabled = true;
                        ddlTipoDocumento.Enabled = true;
                        txbTelefonoContacto.Disabled = false;
                        txbCorreoContacto.Disabled = false;
                        txbFechaPrim.Disabled = false;

                        divBotonesLista.Visible = false;
                        btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;

                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }

                    listaEmpresasAfiliadas();
                    ListaEstadosCRM();
                    rpContactosCRM.ItemDataBound += rpContactosCRM_ItemDataBound;
                    ListaContactosPorUsuario();
                    ConsultarTipoAfiliado();                    
                    ListaCanalesMarketingCRM();
                    ListaObjetivosfiliadoCRM();
                    CargarTipoDocumento();                   
                    CargarPregestion();
                    CargarGeneros();
                    CargarEstadosVentas();
                    CargarEstrategiasMarketing();



                    if (Request.QueryString.Count > 0)
                    {
                        rpContactosCRM.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            ddlAfiliadoOrigen.Enabled = false;
                            txbNombreContacto.Enabled = true;
                            txbApellidoContacto.Disabled = false;
                            txbDocumento.Enabled = false;
                            //ddlTipoDocumento.Enabled = false;
                            //txbTelefonoContacto.Disabled = true;
                            //txbCorreoContacto.Disabled = true;
                            txbFechaPrim.Disabled = true;
                            btnAgregar.Text = "Actualizar";

                            bool respuesta = false;
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarContactosCRMPorId(int.Parse(Request.QueryString["editid"].ToString()), out respuesta);
                            Session["contactoId"] = int.Parse(Request.QueryString["editid"].ToString());
                            litHistorialHTML.Text = dt.Rows[0]["HistorialHTML2"].ToString();

                            if (respuesta)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    DataRow row = dt.Rows[0];
                                    ddlTipoDocumento.SelectedIndex = Convert.ToInt32(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt.Rows[0]["idTipoDoc"].ToString())));
                                    txbDocumento.Text = row["DocumentoAfiliado"].ToString();
                                    txbNombreContacto.Text = row["NombreContacto"].ToString();
                                    txbApellidoContacto.Value = row["ApellidoContacto"].ToString();
                                    ltNombreContacto.Text = row["NombreContacto"].ToString() + " " + row["ApellidoContacto"].ToString();
                                    imgFoto.ImageUrl = row["Foto"].ToString();

                                    if (row["idGenero"].ToString() != "")
                                        ddlGenero.SelectedIndex = Convert.ToInt32(ddlGenero.Items.IndexOf(ddlGenero.Items.FindByValue(dt.Rows[0]["idGenero"].ToString())));
                                    else
                                        ddlGenero.SelectedItem.Value = "0";

                                    DateTime fechaNacimiento = Convert.ToDateTime(dt.Rows[0]["FecNacAfiliado"]);
                                    DateTime hoy = DateTime.Today;

                                    if (fechaNacimiento == new DateTime(1900, 1, 1))
                                    {
                                        txbFecNac.Text = string.Empty;
                                        txbEdad.Text = string.Empty;
                                    }
                                    else
                                    {
                                        hoy = DateTime.Today;
                                        int edad = hoy.Year - fechaNacimiento.Year;

                                        if (fechaNacimiento.Date > hoy.AddYears(-edad))
                                            edad--;

                                        txbFecNac.Text = fechaNacimiento.ToString("dd/MM/yyyy");
                                        txbEdad.Text = edad.ToString();
                                    }

                                    string telefono = Convert.ToString(row["TelefonoContacto"]);
                                    if (!string.IsNullOrEmpty(telefono) && telefono.Length == 10)
                                    {
                                        txbTelefonoContacto.Value = $"{telefono.Substring(0, 3)} {telefono.Substring(3, 3)} {telefono.Substring(6, 4)}";
                                    }
                                    else
                                    {
                                        txbTelefonoContacto.Value = row["TelefonoContacto"].ToString();
                                    }
                                    ltTelefono.Text = txbTelefonoContacto.Value.ToString();
                                    txbCorreoContacto.Value = row["EmailContacto"].ToString();
                                    if (row["idEmpresaCRM"].ToString() != "")
                                        ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(dt.Rows[0]["idEmpresaCRM"].ToString())));
                                    else
                                        ddlEmpresa.SelectedItem.Value = "0";

                                    ddlStatusLead.SelectedIndex = Convert.ToInt32(ddlStatusLead.Items.IndexOf(ddlStatusLead.Items.FindByValue(dt.Rows[0]["idEstadoCRM"].ToString())));
                                    ddlEstadoVenta.SelectedIndex = Convert.ToInt32(ddlEstadoVenta.Items.IndexOf(ddlEstadoVenta.Items.FindByValue(dt.Rows[0]["idEstadoVenta"].ToString())));
                                    ddlEstrategia.SelectedIndex = Convert.ToInt32(ddlEstrategia.Items.IndexOf(ddlEstrategia.Items.FindByValue(dt.Rows[0]["idEstrategia"].ToString())));

                                    CultureInfo cultura = new CultureInfo("es-ES");
                                    txbFechaPrim.Value = Convert.ToDateTime(row["FechaPrimerCon"]).ToString("yyyy-MM-dd");

                                    DateTime fechaPC = Convert.ToDateTime(row["FechaCreacion"]);
                                    ltPrimerContacto.Text = fechaPC.ToString("dddd dd MMM yyyy hh:mm tt", cultura);

                                    txbFechaProx.Value = Convert.ToDateTime(row["FechaProximoCon"]).ToString("yyyy-MM-dd");
                                    DateTime fecha = Convert.ToDateTime(row["FechaProximoCon"]);

                                    if (fecha.Date == DateTime.Today)
                                    {
                                        ltProximoContacto.Text = "Hoy a las " + fecha.ToString("hh:mm tt", cultura);
                                    }
                                    else if (fecha.Date == DateTime.Today.AddDays(1))
                                    {
                                        string hora = fecha.ToString("hh:mm", cultura);
                                        string ampm = fecha.ToString("tt", CultureInfo.InvariantCulture).ToLower();

                                        ltProximoContacto.Text = "Mañana " + fecha.ToString("d 'de' MMMM", cultura)
                                            + " a las " + hora + " " + ampm;
                                    }
                                    else if (fecha.Date > DateTime.Today.AddDays(1))
                                    {
                                        ltProximoContacto.Text = "El próximo " + fecha.ToString("dddd dd MMM yyyy hh:mm tt", cultura);
                                    }
                                    else
                                    {
                                        ltProximoContacto.Text = fecha.ToString("dddd dd MMM yyyy hh:mm tt", cultura);
                                    }


                                    int ValorPropuesta = Convert.ToInt32(dt.Rows[0]["ValorPropuesta"]);
                                    //int ValorMes = Convert.ToInt32(dt.Rows[0]["ValorBase"]);
                                    txbValorPropuesta.Text = ValorPropuesta.ToString("C0", new CultureInfo("es-CO"));
                                    //txbValorMes.Text = ValorMes.ToString("C0", new CultureInfo("es-CO"));
                                    //txaObservaciones.Value = row["observaciones"].ToString();
                                    ddlObjetivos.SelectedIndex = Convert.ToInt32(ddlObjetivos.Items.IndexOf(ddlObjetivos.Items.FindByValue(dt.Rows[0]["idObjetivo"].ToString())));
                                    ListItem item = ddlObjetivos.Items.FindByValue(dt.Rows[0]["idObjetivo"].ToString());
                                    if (item != null) ltObjetivo.Text = item.Text;
                                    else
                                        ltObjetivo.Text = "sin objetivo asignado";

                                    ddlTipoPago.SelectedIndex = ddlTipoPago.Items.IndexOf(ddlTipoPago.Items.FindByValue(dt.Rows[0]["idMedioPago"].ToString()));
                                    ddlTiposAfiliado.SelectedIndex = Convert.ToInt32(ddlTiposAfiliado.Items.IndexOf(ddlTiposAfiliado.Items.FindByValue(dt.Rows[0]["idTipoAfiliado"].ToString())));

                                    ListItem itemTipAfil = ddlTiposAfiliado.Items.FindByValue(dt.Rows[0]["idTipoAfiliado"].ToString());
                                    if (itemTipAfil != null) ltTipoAfiliado.Text = itemTipAfil.Text;
                                    else
                                        ltTipoAfiliado.Text = "sin Tipo afiliado asignado";

                                    ddlCanalesMarketing.SelectedIndex = Convert.ToInt32(ddlCanalesMarketing.Items.IndexOf(ddlCanalesMarketing.Items.FindByValue(dt.Rows[0]["idCanalMarketing"].ToString())));
                                    ddlPlanes.SelectedIndex = Convert.ToInt32(ddlPlanes.Items.IndexOf(ddlPlanes.Items.FindByValue(dt.Rows[0]["idPlan"].ToString())));
                                    decimal precio = Convert.ToDecimal(row["PrecioTotal"]);

                                    ltPlan.Text = row["NombrePlan"].ToString() + " de " + precio.ToString("C0", new System.Globalization.CultureInfo("es-CO"));


                                    //rblMesesPlan.SelectedIndex = Convert.ToInt32(rblMesesPlan.Items.IndexOf(rblMesesPlan.Items.FindByValue(dt.Rows[0]["MesesPlan"].ToString())));
                                }
                            }
                            else
                            {
                                DataRow row = dt.Rows[0];
                                txbNombreContacto.Text = row["Error"].ToString(); ;
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            bool respuesta = false;
                            clasesglobales cg = new clasesglobales();
                            //Borrar
                            DataTable dt1 = new DataTable();
                            dt1 = cg.ConsultarContactosCRMPorId(int.Parse(Request.QueryString["deleteid"].ToString()), out respuesta);
                            if (dt1.Rows.Count > 0)
                            {
                                DataRow row = dt1.Rows[0];

                                ddlTipoDocumento.SelectedIndex = Convert.ToInt32(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt1.Rows[0]["idTipoDoc"].ToString())));
                                txbDocumento.Text = row["DocumentoAfiliado"].ToString();

                                txbNombreContacto.Text = row["NombreContacto"].ToString();

                                txbApellidoContacto.Value = row["ApellidoContacto"].ToString();
                                if (row["idGenero"].ToString() != "")
                                    ddlGenero.SelectedIndex = Convert.ToInt32(ddlGenero.Items.IndexOf(ddlGenero.Items.FindByValue(dt1.Rows[0]["idGenero"].ToString())));
                                else
                                    ddlGenero.SelectedItem.Value = "0";

                                DateTime fechaNacimiento = Convert.ToDateTime(dt1.Rows[0]["FecNacAfiliado"]);
                                DateTime hoy = DateTime.Today;

                                if (fechaNacimiento == new DateTime(1900, 1, 1))
                                {
                                    txbFecNac.Text = string.Empty;
                                    txbEdad.Text = string.Empty;
                                }
                                else
                                {
                                    hoy = DateTime.Today;
                                    int edad = hoy.Year - fechaNacimiento.Year;

                                    if (fechaNacimiento.Date > hoy.AddYears(-edad))
                                        edad--;

                                    txbFecNac.Text = fechaNacimiento.ToString("dd/MM/yyyy");
                                    txbEdad.Text = edad.ToString();
                                }

                                string telefono = Convert.ToString(row["TelefonoContacto"]);
                                if (!string.IsNullOrEmpty(telefono) && telefono.Length == 10)
                                {
                                    txbTelefonoContacto.Value = $"{telefono.Substring(0, 3)} {telefono.Substring(3, 3)} {telefono.Substring(6, 4)}";
                                }
                                else
                                {
                                    txbTelefonoContacto.Value = row["TelefonoContacto"].ToString();
                                }
                                txbCorreoContacto.Value = row["EmailContacto"].ToString();
                                if (row["idEmpresaCRM"].ToString() != "")
                                    ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(dt1.Rows[0]["idEmpresaCRM"].ToString())));
                                else
                                    ddlEmpresa.SelectedItem.Value = "0";

                                ddlStatusLead.SelectedIndex = Convert.ToInt32(ddlStatusLead.Items.IndexOf(ddlStatusLead.Items.FindByValue(dt1.Rows[0]["idEstadoCRM"].ToString())));
                                ddlEstadoVenta.SelectedIndex = Convert.ToInt32(ddlEstadoVenta.Items.IndexOf(ddlEstadoVenta.Items.FindByValue(dt1.Rows[0]["idEstadoVenta"].ToString())));
                                ddlEstrategia.SelectedIndex = Convert.ToInt32(ddlEstrategia.Items.IndexOf(ddlEstrategia.Items.FindByValue(dt1.Rows[0]["idEstrategia"].ToString())));
                                txbFechaPrim.Value = Convert.ToDateTime(row["FechaPrimerCon"]).ToString("yyyy-MM-dd");
                                txbFechaProx.Value = Convert.ToDateTime(row["FechaProximoCon"]).ToString("yyyy-MM-dd");
                                int ValorPropuesta = Convert.ToInt32(dt1.Rows[0]["ValorPropuesta"]);
                                txbValorPropuesta.Text = ValorPropuesta.ToString("C0", new CultureInfo("es-CO"));
                                //int ValorMes = Convert.ToInt32(dt.Rows[0]["ValorBase"]);
                                //txbValorMes.Text = ValorMes.ToString("C0", new CultureInfo("es-CO"));
                                txaObservaciones.Value = row["observaciones"].ToString();
                                ddlObjetivos.SelectedIndex = Convert.ToInt32(ddlObjetivos.Items.IndexOf(ddlObjetivos.Items.FindByValue(dt1.Rows[0]["idObjetivo"].ToString())));
                                ddlTipoPago.SelectedIndex = ddlTipoPago.Items.IndexOf(ddlTipoPago.Items.FindByValue(dt1.Rows[0]["idMedioPago"].ToString()));
                                ddlTiposAfiliado.SelectedIndex = Convert.ToInt32(ddlTiposAfiliado.Items.IndexOf(ddlTiposAfiliado.Items.FindByValue(dt1.Rows[0]["idTipoAfiliado"].ToString())));
                                ddlCanalesMarketing.SelectedIndex = Convert.ToInt32(ddlCanalesMarketing.Items.IndexOf(ddlCanalesMarketing.Items.FindByValue(dt1.Rows[0]["idCanalMarketing"].ToString())));
                                ddlPlanes.SelectedIndex = Convert.ToInt32(ddlPlanes.Items.IndexOf(ddlPlanes.Items.FindByValue(dt1.Rows[0]["idPlan"].ToString())));
                                //rblMesesPlan.SelectedIndex = Convert.ToInt32(rblMesesPlan.Items.IndexOf(rblMesesPlan.Items.FindByValue(dt1.Rows[0]["MesesPlan"].ToString())));

                                //Inactivar controles
                                txbDocumento.Enabled = false;
                                ddlTipoDocumento.Enabled = false;
                                txbNombreContacto.Enabled = false;
                                txbApellidoContacto.Disabled = true;
                                ddlGenero.Enabled = false;
                                txbTelefonoContacto.Disabled = true;
                                txbCorreoContacto.Disabled = true;
                                txbFechaPrim.Disabled = true;
                                txbFechaProx.Disabled = true;
                                txbValorPropuesta.Enabled = false;
                                ddlEmpresa.Enabled = false;
                                ddlStatusLead.Enabled = false;
                                ddlEstadoVenta.Enabled = false;
                                ddlEstrategia.Enabled = false;
                                ddlTiposAfiliado.Enabled = false;
                                txbHoraIni.Disabled = true;
                                ddlTipoPago.Enabled = false;
                                ddlObjetivos.Enabled = false;
                                ddlCanalesMarketing.Enabled = false;
                                ddlPlanes.Enabled = false;
                                //rblMesesPlan.Enabled = false;
                                txaObservaciones.Disabled = true;
                                rfvObservaciones.Enabled = false;
                                ArchivoPropuesta.Disabled = true;

                                btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                //ltTitulo.Text = "Borrar contacto CRM";
                            }
                            dt1.Dispose();
                            //}
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

        #region Métodos cargue de datos
        private void ListaContactosPorUsuario()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                int idUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
                decimal valorTotal = 0;

                DataTable dt = cg.ConsultarContactosCRMPorUsuario(idUsuario, out valorTotal);

                rpContactosCRM.DataSource = dt;
                rpContactosCRM.DataBind();

                //ltValorTotal.Text = valorTotal.ToString("C0");
                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void ListaMediosDePago()
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                DataTable dt = cg.ConsultarMediosDePago();

                ddlTipoPago.DataSource = dt;
                ddlTipoPago.DataTextField = "NombreMedioPago";
                ddlTipoPago.DataValueField = "idMedioPago";
                ddlTipoPago.DataBind();

                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso",
                    "Ocurrió un inconveniente. Código: " + idLog,
                    "error");
            }
        }



        private void listaEmpresasAfiliadas()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarEmpresasYProspectosCorporativos();

                ddlEmpresa.DataSource = dt;
                ddlEmpresa.DataValueField = "DocumentoEmpresa";
                ddlEmpresa.DataTextField = "NombreEmpresa";
                ddlEmpresa.DataBind();

               // ddlEmpresas.Items.Insert(0, new ListItem("Seleccione", ""));

                foreach (ListItem item in ddlEmpresa.Items)
                {
                    if (!string.IsNullOrEmpty(item.Value))
                    {

                        DataRow[] row = dt.Select($"DocumentoEmpresa = '{item.Value}'");

                        if (row.Length > 0)
                        {
                            string estado = row[0]["Origen"].ToString();
                            item.Text = $"{item.Text} ({estado})";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void ListaEstadosCRM()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarEstadossCRM();
                ddlStatusLead.Items.Clear();

                ddlStatusLead.Items.Add(new ListItem("Seleccione", ""));

                foreach (DataRow row in dt.Rows)
                {
                    ListItem item = new ListItem
                    {
                        Text = row["NombreEstadoCRM"].ToString(),
                        Value = row["idEstadoCRM"].ToString()
                    };
                    ddlStatusLead.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void CargarGeneros()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarGeneros();
                ddlGenero.DataSource = dt;
                ddlGenero.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void CargarEstadosVentas()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarEstadosVenta();
                ddlEstadoVenta.DataSource = dt;
                ddlEstadoVenta.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void CargarEstrategiasMarketing()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarEstrategiasMarketingVigentes();
                ddlEstrategia.DataSource = dt;
                ddlEstrategia.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void ConsultarTipoAfiliado()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarTipoAfiliadoBasico();

                ddlTiposAfiliado.DataSource = dt;
                ddlTiposAfiliado.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }
        private void CargarPlanes()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarPlanesVigentesVisibleCRM();

                dt.DefaultView.Sort = "PrecioMinimo ASC, FechaFinal ASC";
                dt = dt.DefaultView.ToTable();

                dt.Columns.Add("TextoPlan", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    string nombre = row["NombrePlan"].ToString().PadRight(25);

                    decimal precioMinimo = row["PrecioMinimo"] != DBNull.Value
                        ? Convert.ToDecimal(row["PrecioMinimo"])
                        : 0;

                    string valor = precioMinimo
                        .ToString("$#,0", System.Globalization.CultureInfo.CreateSpecificCulture("es-CO"))
                        .PadLeft(10);

                    string fechaFinal = row["FechaFinal"] != DBNull.Value
                        ? Convert.ToDateTime(row["FechaFinal"]).ToString("dd/MM/yy")
                        : "";

                    row["TextoPlan"] = $"{nombre}  Min: {valor}  Vence: {fechaFinal}";
                }

                ddlPlanes.DataSource = dt;
                ddlPlanes.DataTextField = "TextoPlan";
                ddlPlanes.DataValueField = "idPlan";
                ddlPlanes.DataBind();

                ddlPlanes.Items.Insert(0, new ListItem("Seleccione", ""));

            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            clasesglobales cg = new clasesglobales();
            try
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
            catch (Exception ex)
            {

            }
        }

        private void CargarPlanesAfiliadPregestion(string strIdAfiliado)
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.CargarPlanesAfiliadoPregestionCRM(strIdAfiliado, "Activo");

                if (dt.Rows.Count > 0)
                {
                    rpPlanesAfiliado.DataSource = dt;
                    rpPlanesAfiliado.DataBind();
                    pnlPlanesAfiliado.Visible = true;
                }
                else
                {
                    pnlPlanesAfiliado.Visible = false;
                }

                dt.Dispose();

            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }
        private void ListaObjetivosfiliadoCRM()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarObjetivosAfiliados();

                ddlObjetivos.DataSource = dt;
                ddlObjetivos.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }
        private void ListaCanalesMarketingCRM()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarCanalesMarketingCRM();

                ddlCanalesMarketing.DataSource = dt;
                ddlCanalesMarketing.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }
        private void CargarTipoDocumento()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultartiposDocumento();
                ddlTipoDocumento.DataSource = dt;
                ddlTipoDocumento.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void CargarPregestion()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultaCargarPregestionPorIdAsesor(Convert.ToInt32(Session["idUsuario"]));

                ddlAfiliadoOrigen.DataSource = dt;
                ddlAfiliadoOrigen.DataValueField = "DocumentoContacto";
                ddlAfiliadoOrigen.DataTextField = "NombreCompleto";
                ddlAfiliadoOrigen.DataBind();

                // Se crea un diccionario con idPregestion y el documento contacto
                var map = dt.AsEnumerable()
                            .ToDictionary(r => r["DocumentoContacto"].ToString(),
                                          r => Convert.ToInt32(r["idPregestion"]));
                ViewState["DocToIdPreg"] = map;
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }



        #endregion

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            try
            {               
                Session["idcrm"] = string.Empty;
                string idcrm = string.Empty;

                if (Request.QueryString.Count > 0)
                {
                    idcrm = Request.QueryString["editid"];
                    Session["idcrm"] = idcrm;
                    string evento = Request.QueryString["evento"];
                    string documento = Request.QueryString["documento"];

                    if (Request.QueryString["editid"] != null)
                    {
                        bool salida = false;
                        string mensaje = string.Empty;
                        string respuesta = string.Empty;
                        string mensajeValidacion = string.Empty;

                        DateTime fecNacCli;

                        // --- LIMPIEZA DE EDAD ---
                        System.Text.RegularExpressions.Match match = Regex.Match(txbEdad.Text, @"\d+");
                        int edad;

                        if (match.Success && int.TryParse(match.Value, out edad))
                        {
                            txbEdad.Text = edad.ToString();
                        }
                        else
                        {
                            edad = 0;
                            txbEdad.Text = "0";
                        }

                        // --- VALIDACIÓN FECHA ---
                        string fechaTexto = txbFecNac.Text.Trim();

                        // Si está vacía
                        if (string.IsNullOrEmpty(fechaTexto))
                        {
                            fecNacCli = new DateTime(1900, 1, 1);
                        }
                        else
                        {
                            // Todos los formatos posibles (con y sin hora)
                            string[] formatos = {
                                                "dd/MM/yyyy",
                                                "MM/dd/yyyy",
                                                "yyyy-MM-dd",
                                                "dd-MM-yyyy",
                                                "MM-dd-yyyy",
                                                "dd/MM/yyyy HH:mm:ss",
                                                "MM/dd/yyyy HH:mm:ss",
                                                "yyyy-MM-dd HH:mm:ss",
                                                "dd-MM-yyyy HH:mm:ss",
                                                "MM-dd-yyyy HH:mm:ss",
                                                "dd/MM/yyyy hh:mm tt",
                                                "MM/dd/yyyy hh:mm tt",
                                                "yyyy-MM-dd hh:mm tt",
                                                "dd-MM-yyyy hh:mm tt",
                                                "MM-dd-yyyy hh:mm tt"
                                            };

                            // Intentar convertir con ParseExact
                            if (!DateTime.TryParseExact(
                                    fechaTexto,
                                    formatos,
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.AllowWhiteSpaces,
                                    out fecNacCli))
                            {
                              
                                if (!DateTime.TryParse(fechaTexto, out fecNacCli))
                                {
                                    throw new Exception($"La fecha '{fechaTexto}' no es válida.");
                                }
                            }
                        }

                        // Formato final
                        txbFecNac.Text = fecNacCli.ToString("yyyy-MM-dd");

                        if (ddlEmpresa.SelectedItem.Value != "")
                            ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(ddlEmpresa.SelectedItem.Value)));
                        else
                            ddlEmpresa.SelectedItem.Value = "0";

                        try
                        {
                            respuesta = cg.ActualizarContactoCRM(Convert.ToInt32(Session["contactoId"].ToString()), 
                                txbNombreContacto.Text.ToString().Trim().ToUpper(),
                                txbApellidoContacto.Value.ToString().Trim().ToUpper(), 
                                Regex.Replace(txbTelefonoContacto.Value.ToString().Trim(), @"\D", ""),
                                txbCorreoContacto.Value.ToString().Trim().ToLower(), 
                                Convert.ToInt32(ddlEmpresa.SelectedItem.Value.ToString()),
                                Convert.ToInt32(ddlStatusLead.SelectedItem.Value.ToString()), 
                                txbFechaPrim.Value.ToString(), 
                                txbFechaProx.Value.ToString(),
                                Convert.ToInt32(Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "")), "", 
                                txaObservaciones.Value.Trim(),
                                Convert.ToInt32(Session["idUsuario"]), 
                                Convert.ToInt32(ddlObjetivos.SelectedItem.Value.ToString()),
                                Convert.ToInt32(ddlTipoPago.SelectedItem.Value.ToString()), 
                                Convert.ToInt32(ddlTiposAfiliado.SelectedItem.Value.ToString()),
                                Convert.ToInt32(ddlCanalesMarketing.SelectedItem.Value.ToString()), 
                                Convert.ToInt32(ddlPlanes.SelectedItem.Value.ToString()), 0,
                                Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value.ToString()), 
                                txbDocumento.Text, 
                                Convert.ToInt32(ddlGenero.SelectedItem.Value.ToString()),
                                Convert.ToInt32(txbEdad.Text), txbFecNac.Text, 
                                Convert.ToInt32(ddlEstadoVenta.SelectedItem.Value.ToString()),
                                Convert.ToInt32(ddlEstrategia.SelectedItem.Value.ToString()), 
                                out salida, 
                                out mensaje);

                            if (salida)
                            {
                                string urlRedirect = (evento == "1") ? "agendacrm" : "crmnuevocontacto";

                                string script = @"
                                Swal.fire({
                                    title: 'El contacto se actualizó correctamente',
                                    text: '" + mensaje.Replace("'", "\\'") + @"',
                                    icon: 'success',
                                    timer: 3000, // 3 segundos
                                    showConfirmButton: false,
                                    timerProgressBar: true
                                }).then(() => {
                                    window.location.href = '" + urlRedirect + @"';
                                });
                                ";

                                ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                            }
                            else
                            {
                                string urlRedirect = (evento == "1") ? "agendacrm" : "crmnuevocontacto";
                                string script = @"
                                Swal.fire({
                                    title: 'Error',
                                    text: '" + mensaje.Replace("'", "\\'") + @"',
                                    icon: 'error'
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                       window.location.href = '"" + urlRedirect + @""';
                                    }
                                });
                                ";
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                            }

                        }
                        catch (Exception ex)
                        {
                            //string urlRedirect = (evento == "1") ? "agendacrm" : "crmnuevocontacto";
                            string script = @"
                        Swal.fire({
                        title: 'Error',
                        text: 'Ha ocurrido un error inesperado. " + ex.Message.ToString() + @"',
                        icon: 'error'
                    });
                ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                        }

                    }
                    if (Request.QueryString["deleteid"] != null)
                    {

                        bool respuesta = false;
                        bool _respuesta = false;
                        string mensaje = string.Empty;
                        int idContacto = Convert.ToInt32(Request.QueryString["deleteid"].ToString());
                        int idUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
                        string Usuario = Session["NombreUsuario"].ToString();

                        try
                        {
                            DataTable dt = cg.ConsultarContactosCRMPorId(idContacto, out _respuesta);
                            Session["contactoId"] = idContacto;

                            if (idContacto > 0)
                            {
                                cg.EliminarContactoCRM(idContacto, idUsuario, Usuario, out respuesta, out mensaje);

                                if (respuesta)
                                {
                                    string tipoMensaje = respuesta ? "Fitness People" : "Error";
                                    string tipoIcono = respuesta ? "success" : "error";
                                    string script = @"
                                        Swal.fire({
                                            title: '" + tipoMensaje + @"',
                                            text: '" + mensaje + @"',
                                            icon: '" + tipoIcono + @"'
                                        }).then((result) => {
                                            if (result.isConfirmed) {
                                                window.location.href = 'crmnuevocontacto';
                                            }
                                        });
                                    ";

                                    ScriptManager.RegisterStartupScript(this, GetType(), "EliminarYAlerta", script, true);
                                }
                                else
                                {
                                    string script = @"
                                            Swal.fire({
                                            title: 'Error',
                                            text: '" + mensaje.Replace("'", "\\'") + @"',
                                            icon: 'error',
                                            timer: 3000,
                                            timerProgressBar: true,
                                            showConfirmButton: true
                                        }).then(() => {
                                            Response.Redirect(Request.RawUrl);
                                        });
                                    ";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            mensaje = ex.Message;
                            string script = @"
                        Swal.fire({
                        title: 'Error',                       
                        text: '"" + mensaje.Replace(""'"", ""\\'"") + @""',
                        icon: 'error'
                    });
                ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                        }
                    }
                }
                else
                {
                    int segundosPasados = 0;
                    //if (!ValidarSede(txbSede.Text.ToString()))
                    //{
                    bool salida = false;
                    //ViewState["AbrirModal"] = true;
                    string mensaje = string.Empty;
                    string mensajeValidacion = string.Empty;
                    string respuesta = string.Empty;
                    DateTime fecNacCli;
                    string textoEdad = txbEdad.Text.Trim();
                    System.Text.RegularExpressions.Match match = Regex.Match(textoEdad, @"\d+");

                    int edad;

                    if (match.Success && int.TryParse(match.Value, out edad))
                    {
                        txbEdad.Text = edad.ToString(); // Deja solo el número limpio en la caja
                    }
                    else
                    {
                        edad = 0;
                        txbEdad.Text = "0";
                    }

                    if (string.IsNullOrEmpty(txbFecNac.Text))
                    {
                        fecNacCli = new DateTime(1900, 1, 1);
                        txbFecNac.Text = fecNacCli.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        fecNacCli = DateTime.Parse(txbFecNac.Text);
                    }

                    txbFecNac.Text = fecNacCli.ToString("yyyy-MM-dd");

                    // Parseamos la fecha y la hora
                    DateTime fecha = DateTime.Parse(txbFechaProx.Value);
                    TimeSpan hora = TimeSpan.Parse(txbHoraIni.Value);
                    DateTime fechaHora = fecha.Date + hora;
                    string fechaHoraMySQL = fechaHora.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    TimeSpan tiempo = TimeSpan.Zero;
                    string observaciones = Request.Unvalidated.Form["txaObservaciones"];
                    observaciones = Regex.Replace(observaciones, "<.*?>", string.Empty);


                    if (int.TryParse(hfContador.Value, out segundosPasados))
                    {
                        int minutos = segundosPasados / 60;
                        int segundos = segundosPasados % 60;
                        string tiempoFormateado = $"00:{minutos:D2}:{segundos:D2}";
                        tiempo = TimeSpan.Parse(tiempoFormateado);
                    }

                    try
                    {
                        respuesta = cg.InsertarContactoCRM(txbNombreContacto.Text.ToString().Trim().ToUpper(), txbApellidoContacto.Value.ToString().Trim().ToUpper(),
                        Regex.Replace(txbTelefonoContacto.Value.ToString().Trim(), @"\D", ""), txbCorreoContacto.Value.ToString().Trim().ToLower(),
                        Convert.ToInt32(ddlEmpresa.SelectedItem.Value.ToString()), Convert.ToInt32(ddlStatusLead.SelectedItem.Value.ToString()), txbFechaPrim.Value.ToString(),
                        fechaHoraMySQL.ToString(), Convert.ToInt32(Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "")), "",
                        txaObservaciones.Value.Trim(), Convert.ToInt32(Session["idUsuario"]), Convert.ToInt32(ddlObjetivos.SelectedItem.Value.ToString()),
                        Convert.ToInt32(ddlTipoPago.SelectedItem.Value.ToString()), Convert.ToInt32(ddlTiposAfiliado.SelectedItem.Value.ToString()),
                        Convert.ToInt32(ddlCanalesMarketing.SelectedItem.Value.ToString()), Convert.ToInt32(ddlPlanes.SelectedItem.Value.ToString()), 0,
                        Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value.ToString()), txbDocumento.Text, tiempo.ToString(), Convert.ToInt32(ddlGenero.SelectedItem.Value.ToString()),
                        Convert.ToInt32(txbEdad.Text), txbFecNac.Text, Convert.ToInt32(ddlEstadoVenta.SelectedItem.Value.ToString()), Convert.ToInt32(ddlEstrategia.SelectedItem.Value.ToString()),
                        Convert.ToInt32(Session["idPregestion"].ToString()), out salida, out mensaje);

                        if (salida)
                        {
                            string script = @"
                        Swal.fire({
                            title: '«¡Creado correctamente!»',
                            text: '" + mensaje.Replace("'", "\\'") + @"',
                            icon: 'success',
                            timer: 3000, // 3 segundos
                            showConfirmButton: false,
                            timerProgressBar: true
                        }).then(() => {
                            window.location.href = 'crmnuevocontacto';
                        });
                    ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                        }
                        else
                        {
                            string script = @"
                            Swal.fire({
                                title: 'Error',
                                text: '" + mensaje.Replace("'", "\\'") + @"',
                                icon: 'error'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                  window.location.href = 'crmnuevocontacto';
                                }
                            });
                        ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        mensaje = ex.Message.ToString();
                        string script = @"
                    Swal.fire({
                        title: 'Error',
                        text: '" + mensaje.Replace("'", "\\'") + @"',
                        icon: 'error'
                    });
                ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                    }
                }

            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        protected void rpContactosCRM_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            try
            {                
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    Literal ltInfoAfiliado = (Literal)e.Item.FindControl("ltInfoAfiliado");

                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    HtmlAnchor btnNuevoAfiliado = (HtmlAnchor)e.Item.FindControl("btnNuevoAfiliado");

                    int documentoAfiliado;

                    if (int.TryParse(row["DocumentoAfiliado"].ToString(), out documentoAfiliado))
                    {
                        
                        DataTable dtAfiliado = cg.ConsultarAfiliadoPorDocumento(documentoAfiliado);

                        if (dtAfiliado.Rows.Count > 0)
                        {
                            int idAfiliado = Convert.ToInt32(dtAfiliado.Rows[0]["idAfiliado"]);
                            DataTable dtEstadoActivo = cg.ConsultarAfiliadoEstadoActivo(idAfiliado);

                            // Siempre se mostrará la información de planes anteriores
                            if (dtEstadoActivo.Rows.Count > 0)
                            {
                                string nombrePlan = dtEstadoActivo.Rows[0]["NombrePlan"].ToString();

                                DateTime fechaFinal;
                                string fechaFormateada = "";
                                if (DateTime.TryParse(dtEstadoActivo.Rows[0]["FechaFinalPlan"].ToString(), out fechaFinal))
                                {
                                    fechaFormateada = fechaFinal.ToString("dd 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-ES"));
                                }

                                decimal valorPlan;
                                string valorFormateado = "";
                                if (decimal.TryParse(dtEstadoActivo.Rows[0]["Valor"].ToString(), out valorPlan))
                                {
                                    valorFormateado = valorPlan.ToString("C0", new System.Globalization.CultureInfo("es-CO"));
                                }

                                string infoExtra =
                                    $"El cliente ha tenido planes anteriores <b>{nombrePlan}</b> " +
                                    $"con fecha final el día <b>{fechaFormateada}</b> " +
                                    $"por un valor de <b>{valorFormateado}</b>.";

                                ltInfoAfiliado.Text = $"<span style='display:block; text-align:justify;'>{infoExtra}</span>";
                            }
                            else
                            {
                                ltInfoAfiliado.Text = "No se encontraron planes anteriores para este usuario.";
                            }

                            if (ViewState["CrearModificar"].ToString() == "1" && btnEditar != null)
                            {
                                btnEditar.Attributes.Add("href", "crmnuevocontacto?editid=" + row.Row[0].ToString());
                                btnEditar.Visible = true;
                            }

                            if (ViewState["Borrar"].ToString() == "1" && btnEliminar != null)
                            {
                                btnEliminar.Attributes.Add("href", "crmnuevocontacto?deleteid=" + row.Row[0].ToString());
                                btnEliminar.Visible = true;
                            }

                            if (btnNuevoAfiliado != null)
                            {
                                btnNuevoAfiliado.Visible = true;
                            }

                        }
                        else
                        {
                            //  si no es  afiliado 
                            ltInfoAfiliado.Text = "No se encontraron planes anteriores para este usuario.";

                            if (ViewState["CrearModificar"].ToString() == "1" && btnEditar != null)
                            {
                                btnEditar.Attributes.Add("href", "crmnuevocontacto?editid=" + row.Row[0].ToString());
                                btnEditar.Visible = true;
                            }

                            if (ViewState["Borrar"].ToString() == "1" && btnEliminar != null)
                            {
                                btnEliminar.Attributes.Add("href", "crmnuevocontacto?deleteid=" + row.Row[0].ToString());
                                btnEliminar.Visible = true;
                            }

                            if (btnNuevoAfiliado != null)
                            {
                                btnNuevoAfiliado.Visible = true;
                            }
                        }
                    }

                    ///////////////////// Calcular tiempo transcurrido /////////////////////

                    DataTable dt1  = new DataTable();
                    dt1 = cg.ConsultarDiasFestivos();

                    if (row["FechaGestion"] != DBNull.Value)
                    {
                        DateTime fechaPrimerContacto = Convert.ToDateTime(row["FechaGestion"]);
                        DateTime hoy = DateTime.Now;

                        // Lista de festivos
                        List<DateTime> festivos = dt1.AsEnumerable()
                            .Select(r => r.Field<DateTime>("Fecha").Date)
                            .ToList();

                    
                        int diasHabiles = ObtenerDiasHabiles(fechaPrimerContacto, hoy, festivos);              
                        TimeSpan diferencia = hoy - fechaPrimerContacto;
                        int totalMeta = 6;

                        // Tamaño real de días hábiles
                        int diaHabilReal = diasHabiles;

                        // Mostrar máximo 6 de 6
                        int diaHabilMostrar = Math.Min(diasHabiles, totalMeta);

                        // Días restantes
                        int diasRestantes = totalMeta - diasHabiles;
                        if (diasRestantes < 0)
                            diasRestantes = 0;

                        // Texto base
                        string leyendaBase = diasHabiles <= 0
                            ? (diferencia.TotalMinutes < 1 ? "Hace menos de un minuto"
                               : diferencia.TotalMinutes < 60 ? $"Hace {(int)diferencia.TotalMinutes} minutos"
                               : diferencia.TotalHours < 24 ? $"Hace {(int)diferencia.TotalHours} horas"
                               : "Hoy")
                            : $"Hace {diasHabiles} días";

                        string fechaInicial = Convert.ToDateTime(row["FechaGestion"]).ToString("dd/MM/yyyy");

                        string tooltip = $"Día inicial: {fechaInicial}&#10;" +
                                         $"Días calendario: {diferencia.Days}&#10;" +
                                         $"Día hábil real: {diaHabilReal}&#10;" +
                                         $"Día hábil mostrado: {diaHabilMostrar} de {totalMeta}&#10;";
                                         //$"Días restantes: {diasRestantes}";

                        // Icono reloj
                        string icono = "<i class='fa fa-clock-o'></i>";

                        // Texto visible
                        string textoVisible = diasRestantes == 0
                            ? $"<b>Quedan 0 días</b>"
                            : $"Quedan {diasRestantes} días";

                        // Construcción final
                        string leyendaFinal =
                            //$"{icono} {leyendaBase} • Día hábil {diaHabilMostrar} de {totalMeta} • " +
                            $"<span title='{tooltip}' style='cursor:pointer;'>{textoVisible}</span>";

                        HtmlGenericControl ltTiempo = (HtmlGenericControl)e.Item.FindControl("ltTiempoTranscurrido");
                        if (ltTiempo != null)
                        {
                            ltTiempo.InnerHtml = leyendaFinal;       
                            ltTiempo.Attributes["title"] = tooltip;                           }
                    }
                    ////////////////////////////////////////////////////////////////////////
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private int ObtenerDiasHabiles(DateTime inicio, DateTime fin, List<DateTime> festivos)
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                int diasHabiles = 0;

                DateTime fechaActual = inicio.Date;

                while (fechaActual.Date <= fin.Date)
                {
                    bool esFinDeSemana = (fechaActual.DayOfWeek == DayOfWeek.Saturday ||
                                          fechaActual.DayOfWeek == DayOfWeek.Sunday);

                    bool esFestivo = festivos.Contains(fechaActual.Date);

                    if (!esFinDeSemana && !esFestivo)
                        diasHabiles++;

                    fechaActual = fechaActual.AddDays(1);
                }

                return diasHabiles - 1; // no contar el día de inicio
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
                return 0;
            }
        }


        protected void ddlPlanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                if (string.IsNullOrEmpty(ddlPlanes.SelectedValue) || ddlPlanes.SelectedValue == "0")
                {
                    txbValorPropuesta.Text = "Por favor selecciona un plan válido.";
                    ViewState["precioBase"] = null;
                    ViewState["Meses"] = null;
                    ViewState["MesesCortesia"] = null;
                    ViewState["DebitoAutomatico"] = null;
                    return;
                }

                DataTable dt = cg.ConsultarPlanes();
                var fila = dt.Select("IdPlan = " + ddlPlanes.SelectedValue);
                if (fila.Length > 0)
                {
                    ViewState["precioBase"] = fila[0]["PrecioBase"];
                    ViewState["precioTotal"] = fila[0]["precioTotal"];
                    ViewState["Meses"] = fila[0]["Meses"];
                    ViewState["MesesCortesia"] = fila[0]["MesesCortesia"];
                    ViewState["DebitoAutomatico"] = fila[0]["DebitoAutomatico"];

                    int meses = Convert.ToInt32(ViewState["Meses"]);
                    int cortesia = Convert.ToInt32(ViewState["MesesCortesia"]);
                    int totalMeses = meses + cortesia;
                    double total = Convert.ToDouble(ViewState["precioTotal"]);
                    int precioBase = Convert.ToInt32(ViewState["precioBase"]);
                    int DebitoAutomatico = Convert.ToInt32(ViewState["DebitoAutomatico"]);

                    int ValorMes = Convert.ToInt32(fila[0]["PrecioBase"]);
                    txbValorMes.Text = ValorMes.ToString("C0", new CultureInfo("es-CO"));
                    txbValorMes.Enabled = false;

                    string observaciones = fila[0]["DescripcionPlan"].ToString();
                    //txaObservaciones.InnerText = observaciones;

                    int mesesPlan = Convert.ToInt32(fila[0]["Meses"]); // Asegúrate que esta columna está en tu tabla

                    if (ViewState["DebitoAutomatico"] != null && int.TryParse(ViewState["DebitoAutomatico"].ToString(), out DebitoAutomatico))
                    {
                        if (DebitoAutomatico == 1)
                        {
                            total = precioBase * 12;
                        }
                    }
                    txbValorPropuesta.Text = $"${total:N0}";
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        public class EstadoCRM
        {
            public int idEstadoCRM { get; set; }
            public string NombreEstadoCRM { get; set; }
            public string ColorHexaCRM { get; set; }
            public string IconoMinEstadoCRM { get; set; }
        }

        public class verificarafiliado : IHttpHandler
        {
            public void ProcessRequest(HttpContext context)
            {
                string docStr = context.Request.QueryString["documento"];
                bool existe = false;

                if (!string.IsNullOrEmpty(docStr))
                {
                    clasesglobales cg = new clasesglobales();
                    DataTable dt = cg.ConsultarAfiliadoPorDocumento(Convert.ToInt32(docStr));
                    existe = dt.Rows.Count > 0;
                }

                context.Response.ContentType = "application/json";
                context.Response.Write("{\"existe\": " + existe.ToString().ToLower() + "}");
            }

            public bool IsReusable => false;
        }

        public string GetTelefonoHTML(object telefonoObj)
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                if (telefonoObj == null) return "";

                // 1. Limpiar el número (quitar espacios, guiones, paréntesis, etc.)
                string telefonoLimpio = Regex.Replace(telefonoObj.ToString(), @"\D", "");

                // 2. Validar longitud y aplicar formato visual
                string telefonoFormateado = telefonoLimpio;
                if (telefonoLimpio.Length == 10)
                {
                    telefonoFormateado = $"{telefonoLimpio.Substring(0, 3)} {telefonoLimpio.Substring(3, 3)} {telefonoLimpio.Substring(6, 4)}";
                }

                bool esCelular = telefonoLimpio.StartsWith("3");
                bool esFijo = telefonoLimpio.StartsWith("60");
                string icono = esCelular ? "fab fa-whatsapp" : "fas fa-phone";
                string color = esCelular ? "forestgreen" : "#007bff";
                string enlace = esCelular ? $"https://wa.me/57{telefonoLimpio}" : $"tel:{telefonoLimpio}";

                return $"<a href='{enlace}' target='_blank'>" +
                       $"<i class='{icono} m-r-xs font-bold' style='color:{color};'></i> {telefonoFormateado}</a>";

            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
                return "Error";
            }
        }

        protected void btnActualizarYRedirigir_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            try
            {

                bool rta = false;
                int idAfil = 0;
                int idcrm = Convert.ToInt32(Request.QueryString["editid"]);
                Session["idcrm"] = idcrm;
                string evento = Request.QueryString["evento"];
                string documento = Request.QueryString["documento"];

                DataTable dt1 = cg.ConsultarContactosCRMPorId(idcrm, out rta);
                DataTable dt2 = cg.ConsultarAfiliadoPorDocumento(Convert.ToInt32(dt1.Rows[0]["DocumentoAfiliado"].ToString()));
                if (dt2.Rows.Count > 0)
                    idAfil = Convert.ToInt32(dt2.Rows[0]["IdAfiliado"].ToString());

                // VALIDACIÓN: si ya tiene plan activo, no continuar
                if (idAfil > 0)
                {
                    DataTable dtActivo = cg.ConsultarAfiliadoEstadoActivo(idAfil);
                    if (dtActivo.Rows.Count > 0)
                    {
                        btnAgregar.Enabled = false;
                        txaObservaciones.Disabled = true;
                        txbFechaProx.Disabled = true;
                        txbHoraIni.Disabled = true;
                        ddlStatusLead.Enabled = false;
                        hfContador.Visible = false;

                        string script = @"
                        Swal.fire({
                            title: 'Afiliado ya tiene plan activo',
                            text: 'No se puede redirigir al proceso de afiliación.',
                            icon: 'warning',
                            confirmButtonText: 'Entendido'
                        });";
                        ScriptManager.RegisterStartupScript(this, GetType(), "AfiliadoActivo", script, true);
                        //  return; // 
                    }
                }

                if (idcrm > 0)
                {
                    bool salida = false;
                    string mensaje = string.Empty;
                    string respuesta = string.Empty;
                    string mensajeValidacion = string.Empty;
                    if (txaObservaciones.Value == "") txaObservaciones.Value = "Se redirige a proceso de afiliaciones";


                    if (ddlEmpresa.SelectedItem.Value != "")
                        ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(ddlEmpresa.SelectedItem.Value)));
                    else
                        ddlEmpresa.SelectedItem.Value = "0";

                    DateTime fecNacCli;

                    // 1. EXTRAER SOLO EL NÚMERO DE LA EDAD
                    System.Text.RegularExpressions.Match match = Regex.Match(txbEdad.Text, @"\d+");
                    int edad;

                    if (match.Success && int.TryParse(match.Value, out edad))
                    {
                        txbEdad.Text = edad.ToString();
                    }
                    else
                    {
                        edad = 0;
                        txbEdad.Text = "0";
                    }

                    string fechaTexto = txbFecNac.Text.Trim();

                    // Si viene vacía → asignar 1900-01-01
                    if (string.IsNullOrEmpty(fechaTexto))
                    {
                        fecNacCli = new DateTime(1900, 1, 1);
                    }
                    else
                    {
                        // Formatos permitidos
                        string[] formatosAceptados = {
                        "dd/MM/yyyy",   // Colombia
                        "MM/dd/yyyy",   // USA
                        "yyyy-MM-dd",   // ISO (input date HTML)
                        "dd-MM-yyyy",
                        "MM-dd-yyyy"
                    };

                        bool ok = DateTime.TryParseExact(fechaTexto, formatosAceptados, CultureInfo.InvariantCulture, DateTimeStyles.None, out fecNacCli);

                        if (!ok)
                        {
                            // Si no se pudo interpretar, evita reventar el sistema
                            throw new Exception($"La fecha '{fechaTexto}' no es válida.");
                        }
                    }

                    // 3. Formato final estándar
                    txbFecNac.Text = fecNacCli.ToString("yyyy-MM-dd");

                    try
                    {
                        respuesta = cg.ActualizarContactoCRM(Convert.ToInt32(Session["contactoId"].ToString()), txbNombreContacto.Text.Trim().ToUpper(),
                            txbApellidoContacto.Value.Trim().ToUpper(), Regex.Replace(txbTelefonoContacto.Value.Trim(), @"\D", ""),
                            txbCorreoContacto.Value.Trim().ToLower(), Convert.ToInt32(ddlEmpresa.SelectedItem.Value),
                            Convert.ToInt32(ddlStatusLead.SelectedItem.Value), txbFechaPrim.Value, txbFechaProx.Value,
                            Convert.ToInt32(Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "")), "", txaObservaciones.Value.Trim(),
                            Convert.ToInt32(Session["idUsuario"]), Convert.ToInt32(ddlObjetivos.SelectedItem.Value),
                            Convert.ToInt32(ddlTipoPago.SelectedItem.Value), Convert.ToInt32(ddlTiposAfiliado.SelectedItem.Value),
                            Convert.ToInt32(ddlCanalesMarketing.SelectedItem.Value), Convert.ToInt32(ddlPlanes.SelectedItem.Value), 0,
                            Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value), txbDocumento.Text, Convert.ToInt32(ddlGenero.SelectedItem.Value.ToString()),
                            Convert.ToInt32(txbEdad.Text), txbFecNac.Text, Convert.ToInt32(ddlEstadoVenta.SelectedItem.Value.ToString()),
                            Convert.ToInt32(ddlEstrategia.SelectedItem.Value.ToString()), out salida, out mensaje);

                        if (salida)
                        {
                            bool existe = false;

                            if (!string.IsNullOrEmpty(documento))
                            {
                                DataTable dt = cg.ConsultarAfiliadoPorDocumento(Convert.ToInt32(documento));
                                existe = dt.Rows.Count > 0;
                            }

                            string urlRedirect = (existe) ? "editarafiliado" : "nuevoafiliado";
                            string formulario = (existe) ? "de edición" : "nuevo afiliado";

                            string script = @"
                            Swal.fire({
                                title: 'Serás redirigido al formulario " + formulario + @"',
                                text: 'Espera un momento...',
                                icon: 'success',
                                timer: 4000,
                                showConfirmButton: false,
                                timerProgressBar: true
                            }).then(() => {
                                window.location.href = '" + urlRedirect + @"?idcrm=" + idcrm + @"';
                            });";

                            ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                        }
                        else
                        {
                            string urlRedirect = (evento == "1") ? "agendacrm" : "crmnuevocontacto";
                            string script = @"
                            Swal.fire({
                                title: 'Error',
                                text: '" + mensaje.Replace("'", "\\'") + @"',
                                icon: 'error'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    window.location.href = '" + urlRedirect + @"';
                                }
                            });";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        //string urlRedirect = (evento == "1") ? "agendacrm" : "crmnuevocontacto";
                        string script = @"
                        Swal.fire({
                            title: 'Error',
                            text: 'Ha ocurrido un error inesperado." + ex.Message.ToString() + @"',
                            icon: 'error'
                        });";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                    }
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }


        protected void ddlAfiliadoOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                if (ddlAfiliadoOrigen.SelectedItem.Value.ToString() != "")
                {
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();
                    DataTable dtCorporativo = new DataTable();

                    var map = ViewState["DocToIdPreg"] as Dictionary<string, int>;
                    if (map != null && map.TryGetValue(ddlAfiliadoOrigen.SelectedValue, out int idPregestion))
                    {
                        Session["idPregestion"] = idPregestion;
                    }

                    bool esAfiliado = false;
                    Session["esAfiliado"] = esAfiliado.ToString();


                    int documento = 0;
                    string[] strDocumento = ddlAfiliadoOrigen.SelectedItem.Value.ToString().Split('-');
                    if (int.TryParse(strDocumento[0], out documento))
                    {
                        dt = cg.ConsultarAfiliadoPorDocumento(documento);
                    }

                    dt1 = cg.ConsultarTipoAfiliadoBasico();
                    dtCorporativo = cg.ConsultarClientecorporativo(documento.ToString());

                    DataTable dt2 = cg.ConsultarProspectosCRMAsignados();
                    DataRow[] filas = dt2.Select($"DocumentoContacto = '{documento}'");


                    try
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if(dtCorporativo.Rows.Count > 0)
                            {

                                if (filas.Length > 0)
                                {
                                    DataRow row = filas[0];

                                    if (Convert.ToInt32(row["idAsesor"]) != 0)
                                    {
                                        ViewState["Cargando"] = true;

                                        txbDocumento.Text = documento.ToString();

                                        ddlTipoDocumento.SelectedValue = row["idTipoDocumentoContacto"].ToString();
                                        txbNombreContacto.Text = row["NombreContacto"].ToString();
                                        txbApellidoContacto.Value = row["ApellidoContacto"].ToString();
                                        txbTelefonoContacto.Value = row["CelularContacto"].ToString();

                                        string genero;
                                        DateTime? fechaNacimiento;
                                        int? edad;

                                        ConsultarApiAdres(documento, out genero, out fechaNacimiento, out edad);

                                        if (!string.IsNullOrEmpty(genero) && ddlGenero.Items.FindByValue(genero) != null)
                                        {
                                            ddlGenero.SelectedValue = genero;
                                        }

                                        if (fechaNacimiento.HasValue)
                                        {
                                            txbFecNac.Text = fechaNacimiento.Value.ToString("yyyy-MM-dd");
                                        }

                                        if (edad.HasValue)
                                        {
                                            txbEdad.Text = edad.Value + " años";
                                        }

                                        ddlCanalesMarketing.SelectedValue = "15";
                                        ddlEmpresa.SelectedValue = row["DocumentoEmpresa"].ToString();
                                        if (ddlTipoPago.Items.FindByValue("7") != null)
                                        {
                                            ddlTipoPago.ClearSelection();
                                            ddlTipoPago.SelectedValue = "7";
                                        }
                                        ddlEstrategia.SelectedValue = "0";
                                        ddlStatusLead.SelectedValue = "2";
                                        ddlEstadoVenta.SelectedValue = "1";
                                        //Correo
                                        txbCorreoContacto.Value = dt.Rows[0]["EmailAfiliado"].ToString();

                                        string idPlan = row["idPlan"].ToString().Trim();

                                        if (ddlPlanes.Items.FindByValue(idPlan) == null)
                                        {
                                            ddlPlanes.Items.Add(new ListItem("Plan negociación", idPlan));
                                        }
                                        ddlPlanes.SelectedValue = idPlan;

                                        int valorPropuesta = 0;

                                        if (row["ValorNegociacion"] != DBNull.Value)
                                        {
                                            int.TryParse(row["ValorNegociacion"].ToString(), out valorPropuesta);
                                        }

                                        txbValorPropuesta.Text = valorPropuesta.ToString("C0", new CultureInfo("es-CO"));

                                        //int ValorPropuesta = Convert.ToInt32(row["ValorNegociacion"]);
                                        //txbValorPropuesta.Text = ValorPropuesta.ToString("C0", new CultureInfo("es-CO"));

                                        txaObservaciones.Value = row["EstadoNegociacion"] +
                                                                     " Descuento: " + row["Descuento"] + "% " +
                                                                     row["Descripcion"];

                                        // Elimina todas las etiquetas HTML
                                        txaObservaciones.Value = Regex.Replace(txaObservaciones.Value, "<.*?>", string.Empty);

                                        ViewState.Remove("Cargando");
                                        upPlanes.Update();
                                    }
                                }

                            }
                            else
                            {
                                esAfiliado = true;
                                Session["esAfiliado"] = esAfiliado.ToString();
                                txbDocumento.Text = documento.ToString();
                                ddlTipoDocumento.SelectedIndex = Convert.ToInt32(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt.Rows[0]["idTipoDocumento"].ToString())));
                                txbNombreContacto.Text = dt.Rows[0]["NombreAfiliado"].ToString();
                                txbApellidoContacto.Value = dt.Rows[0]["ApellidoAfiliado"].ToString();
                                if (dt.Rows[0]["idGenero"].ToString() != "")
                                    ddlGenero.SelectedIndex = Convert.ToInt32(ddlGenero.Items.IndexOf(ddlGenero.Items.FindByValue(dt.Rows[0]["idGenero"].ToString())));
                                else
                                    ddlGenero.SelectedItem.Value = "0";

                                DateTime fechaNacimiento = Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"]);
                                DateTime hoy = DateTime.Today;

                                int edad = hoy.Year - fechaNacimiento.Year;
                                if (fechaNacimiento.Date > hoy.AddYears(-edad))
                                {
                                    edad--;
                                }

                                txbFecNac.Text = fechaNacimiento.ToString("dd/MM/yyyy");
                                txbEdad.Text = edad.ToString() + " años";
                                txbTelefonoContacto.Value = dt.Rows[0]["CelularAfiliado"].ToString();
                                txbCorreoContacto.Value = dt.Rows[0]["EmailAfiliado"].ToString();

                                ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(dt.Rows[0]["idEmpresaAfil"].ToString())));
                                ddlTiposAfiliado.SelectedValue = "2";//Afiliado en renovación

                                CargarPlanesAfiliadPregestion(dt.Rows[0]["idAfiliado"].ToString());
                            }
                        }
                        else //si no es afiliado
                        { 
                            if (filas.Length > 0)
                            {
                                DataRow row = filas[0];

                                if (Convert.ToInt32(row["idAsesor"]) != 0)
                                {
                                    ViewState["Cargando"] = true;

                                    txbDocumento.Text = documento.ToString();

                                    ddlTipoDocumento.SelectedValue = row["idTipoDocumentoContacto"].ToString();
                                    txbNombreContacto.Text = row["NombreContacto"].ToString();
                                    txbApellidoContacto.Value = row["ApellidoContacto"].ToString();
                                    txbTelefonoContacto.Value = row["CelularContacto"].ToString();

                                    string genero;
                                    DateTime? fechaNacimiento;
                                    int? edad;

                                    ConsultarApiAdres(documento, out genero, out fechaNacimiento, out edad);

                                    if (!string.IsNullOrEmpty(genero) && ddlGenero.Items.FindByValue(genero) != null)
                                    {
                                        ddlGenero.SelectedValue = genero;
                                    }

                                    if (fechaNacimiento.HasValue)
                                    {
                                        txbFecNac.Text = fechaNacimiento.Value.ToString("yyyy-MM-dd");
                                    }

                                    if (edad.HasValue)
                                    {
                                        txbEdad.Text = edad.Value + " años";
                                    }

                                    ddlCanalesMarketing.SelectedValue = "15";
                                    ddlEmpresa.SelectedValue = row["DocumentoEmpresa"].ToString();
                                    if (ddlTipoPago.Items.FindByValue("7") != null)
                                    {
                                        ddlTipoPago.ClearSelection();
                                        ddlTipoPago.SelectedValue = "7";
                                    }
                                    ddlEstrategia.SelectedValue = "0";
                                    ddlStatusLead.SelectedValue = "2";
                                    ddlEstadoVenta.SelectedValue = "1";                                


                                    string idPlan = row["idPlan"].ToString().Trim();

                                    if (ddlPlanes.Items.FindByValue(idPlan) == null)
                                    {
                                        ddlPlanes.Items.Add(new ListItem("Plan negociación", idPlan));
                                    }
                                    ddlPlanes.SelectedValue = idPlan;

                                    int ValorPropuesta = Convert.ToInt32(row["ValorNegociacion"]);
                                    txbValorPropuesta.Text = ValorPropuesta.ToString("C0", new CultureInfo("es-CO"));
                                    
                                    txaObservaciones.Value = row["EstadoNegociacion"] +
                                                                 " Descuento: " + row["Descuento"] + "% " +
                                                                 row["Descripcion"];

                                    // Elimina todas las etiquetas HTML
                                    txaObservaciones.Value = Regex.Replace(txaObservaciones.Value, "<.*?>", string.Empty);

                                    ViewState.Remove("Cargando");
                                    upPlanes.Update();
                                }
                            }
                        }
                        dt.Dispose();
                         

                    }
                    catch (Exception ex)
                    {
                        int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                        MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
                    }
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void ConsultarApiAdres(int documento, out string genero, out DateTime? fechaNacimiento, out int? edad )
        {
            genero = null;
            fechaNacimiento = null;
            edad = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);

                    var response = client.GetAsync(
                        $"https://pqrdsuperargo.supersalud.gov.co/api/api/adres/0/{documento}"
                    ).Result;

                    if (!response.IsSuccessStatusCode)
                        return;

                    var json = response.Content.ReadAsStringAsync().Result;
                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                    if (data.sexo != null)
                        genero = data.sexo.ToString();

                    if (data.fecha_nacimiento != null)
                        fechaNacimiento = Convert.ToDateTime(data.fecha_nacimiento.ToString());

                    if (data.edad != null)
                        edad = Convert.ToInt32(data.edad);
                }
            }
            catch
            {
                // 
            }
        }


        public class RespuestaContacto
        {
            public string status { get; set; }
            public string mensaje { get; set; }
            public string fecha { get; set; }
            public string nombrePlan { get; set; }
        }

        [System.Web.Services.WebMethod]
        public static RespuestaContacto ValidarContacto(string documento, int idUsuario)
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt2 = cg.ConsultarPoliticaTiempoLeadCRM(documento, 6);

                if (dt2.Rows.Count > 0)
                {
                    //bool tienePlanVendido = false;
                    //bool esPropio = false;
                    //bool bloqueadoPorOtro = false;

                    foreach (DataRow row in dt2.Rows)
                    {
                        int idEstadoCRM = Convert.ToInt32(row["idEstadoCRM"]);
                        int dias = Convert.ToInt32(row["DiasTranscurridos"]);
                        int idUsuarioCreaCRM = Convert.ToInt32(row["idUsuario"]);

                        if (idEstadoCRM == 3)
                        {
                            return new RespuestaContacto
                            {
                                status = "planVendido",
                                mensaje = "El contacto ya tiene un plan vendido o una negociación aceptada.",
                                fecha = "",
                                nombrePlan = ""
                            };
                        }
                        if (idUsuarioCreaCRM == idUsuario)
                        {
                            return new RespuestaContacto
                            {
                                status = "propio",
                                mensaje = "Ya tienes un contacto creado para este documento.",
                                fecha = "",
                                nombrePlan = ""
                            };
                        }
                        if (idUsuarioCreaCRM != idUsuario && dias <= 6)
                        {
                            return new RespuestaContacto
                            {
                                status = "bloqueado",
                                mensaje = "El contacto está siendo gestionado por otro asesor.",
                                fecha = idUsuarioCreaCRM.ToString(),
                                nombrePlan = ""
                            };
                        }
                    }
                }

                DataTable dt3 = cg.CargarPlanesAfiliado("0", documento, "Activo");

                if (dt3.Rows.Count > 0)
                {
                    //bool tienePlanVendido = false;

                    foreach (DataRow row in dt3.Rows)
                    {
                        int idDebitoAutomatico = Convert.ToInt32(row["DebitoAutomatico"]);

                        if (idDebitoAutomatico == 1)
                        {
                            return new RespuestaContacto
                            {
                                status = "planVendido",
                                mensaje = "El contacto ya tiene un plan vendido o una negociación aceptada.",
                                fecha = row["FechaInicioPlan"].ToString() + " - " + row["FechaFinalPlan"].ToString(),
                                nombrePlan = row["NombrePlan"].ToString()
                            };
                        }
                    }
                }

                return new RespuestaContacto { status = "ok" };
            }

            catch (Exception ex)
            {
                return new RespuestaContacto
                {
                    status = "error",
                    mensaje = "Contacta al administrador del sistema.",
                    fecha = "",
                    nombrePlan = ""
                };
                //return "Error, contácte al administrador del sistema - " + ex.Message;
            }
        }

    }
}