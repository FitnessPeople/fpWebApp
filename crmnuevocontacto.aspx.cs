using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Presentation;

namespace fpWebApp
{
    public partial class crmnuevocontacto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


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
                        txbFechaPrim.Attributes.Add("type", "date");
                        txbFechaPrim.Attributes.Add("min", DateTime.Now.ToString("yyyy-MM-dd"));
                        txbFechaPrim.Value = DateTime.Now.ToString("yyyy-MM-dd");
                        txbFechaProx.Attributes.Add("type", "date");
                        txbFechaProx.Value = DateTime.Now.ToString("yyyy-MM-dd");
                        txbCorreoContacto.Attributes.Add("type", "email");

                        btnAgregar.Text = "Agregar";

                        txbAfiliado.Enabled = true;
                        txbNombreContacto.Disabled = false;
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

                    ListaEmpresasCRM();
                    ListaEstadosCRM();
                    rpContactosCRM.ItemDataBound += rpContactosCRM_ItemDataBound;
                    ListaContactosPorUsuario();
                    ListaTiposAfiliadosCRM();
                    CargarPlanes();
                    ListaCanalesMarketingCRM();
                    ListaObjetivosfiliadoCRM();
                    CargarTipoDocumento();
                    ListaMediosDePago();


                    if (Request.QueryString.Count > 0)
                    {
                        rpContactosCRM.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            txbAfiliado.Enabled = false;
                            txbNombreContacto.Disabled = false;
                            txbApellidoContacto.Disabled = false;
                            txbDocumento.Enabled = false;
                            ddlTipoDocumento.Enabled = false;
                            txbTelefonoContacto.Disabled = true;
                            txbCorreoContacto.Disabled = true;
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
                                    txbNombreContacto.Value = row["NombreContacto"].ToString();
                                    txbApellidoContacto.Value = row["ApellidoContacto"].ToString();
                                    ltNombreContacto.Text = row["NombreContacto"].ToString() + " " + row["ApellidoContacto"].ToString();
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
                                    else
                                    {
                                        ltProximoContacto.Text = "El próximo " + fecha.ToString("dddd dd MMM yyyy hh:mm tt", cultura);
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
                                    ltPlan.Text = row["NombrePlan"].ToString();
                                    //rblMesesPlan.SelectedIndex = Convert.ToInt32(rblMesesPlan.Items.IndexOf(rblMesesPlan.Items.FindByValue(dt.Rows[0]["MesesPlan"].ToString())));
                                }
                            }
                            else
                            {
                                DataRow row = dt.Rows[0];
                                txbNombreContacto.Value = row["Error"].ToString(); ;
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            bool respuesta = false;
                            clasesglobales cg = new clasesglobales();
                            //DataTable dt = cg.validarco(int.Parse(Request.QueryString["deleteid"].ToString()));
                            //if (dt.Rows.Count > 0)
                            //{
                            //    ltMensaje.Text = "<div class=\"ibox-content\">" +
                            //        "<div class=\"alert alert-danger alert-dismissable\">" +
                            //        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                            //        "Este Contacto no se puede borrar, hay registros asociados a él" +
                            //        "</div></div>";

                            //    DataTable dt1 = cg.ConsultarContactosCRMPorId(int.Parse(Request.QueryString["deleteid"].ToString()),out respuesta);

                            //    if (dt1.Rows.Count > 0)
                            //    {
                            //        txbNombreContacto.Value = dt.Rows[0]["NombreContacto"].ToString();
                            //        txbNombreContacto.Disabled = true;
                            //        btnAgregar.Text = "⚠ Confirmar borrado ❗";
                            //        btnAgregar.Enabled = false;
                            //        //ltTitulo.Text = "Borrar Contacto CRM";
                            //    }
                            //    dt1.Dispose();
                            //}
                            // else
                            //{
                            //Borrar
                            DataTable dt1 = new DataTable();
                            dt1 = cg.ConsultarContactosCRMPorId(int.Parse(Request.QueryString["deleteid"].ToString()), out respuesta);
                            if (dt1.Rows.Count > 0)
                            {
                                DataRow row = dt1.Rows[0];

                                ddlTipoDocumento.SelectedIndex = Convert.ToInt32(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt1.Rows[0]["idTipoDoc"].ToString())));
                                txbDocumento.Text = row["DocumentoAfiliado"].ToString();

                                txbNombreContacto.Value = row["NombreContacto"].ToString();
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
                                txbFechaPrim.Value = Convert.ToDateTime(row["FechaPrimerCon"]).ToString("yyyy-MM-dd");
                                txbFechaProx.Value = Convert.ToDateTime(row["FechaProximoCon"]).ToString("yyyy-MM-dd");
                                int ValorPropuesta = Convert.ToInt32(dt1.Rows[0]["ValorPropuesta"]);
                                txbValorPropuesta.Text = ValorPropuesta.ToString("C0", new CultureInfo("es-CO"));
                                //int ValorMes = Convert.ToInt32(dt.Rows[0]["ValorBase"]);
                                //txbValorMes.Text = ValorMes.ToString("C0", new CultureInfo("es-CO"));
                                //txaObservaciones.Value = row["observaciones"].ToString();
                                ddlObjetivos.SelectedIndex = Convert.ToInt32(ddlObjetivos.Items.IndexOf(ddlObjetivos.Items.FindByValue(dt1.Rows[0]["idObjetivo"].ToString())));
                                ddlTipoPago.SelectedIndex = ddlTipoPago.Items.IndexOf(ddlTipoPago.Items.FindByValue(dt1.Rows[0]["idMedioPago"].ToString()));
                                ddlTiposAfiliado.SelectedIndex = Convert.ToInt32(ddlTiposAfiliado.Items.IndexOf(ddlTiposAfiliado.Items.FindByValue(dt1.Rows[0]["idTipoAfiliado"].ToString())));
                                ddlCanalesMarketing.SelectedIndex = Convert.ToInt32(ddlCanalesMarketing.Items.IndexOf(ddlCanalesMarketing.Items.FindByValue(dt1.Rows[0]["idCanalMarketing"].ToString())));
                                ddlPlanes.SelectedIndex = Convert.ToInt32(ddlPlanes.Items.IndexOf(ddlPlanes.Items.FindByValue(dt1.Rows[0]["idPlan"].ToString())));
                                //rblMesesPlan.SelectedIndex = Convert.ToInt32(rblMesesPlan.Items.IndexOf(rblMesesPlan.Items.FindByValue(dt1.Rows[0]["MesesPlan"].ToString())));

                                //Inactivar controles
                                txbDocumento.Enabled = false;
                                ddlTipoDocumento.Enabled = false;
                                txbNombreContacto.Disabled = true;
                                txbTelefonoContacto.Disabled = true;
                                txbCorreoContacto.Disabled = true;
                                txbFechaPrim.Disabled = true;
                                txbFechaProx.Disabled = true;
                                txbValorPropuesta.Enabled = false;
                                ddlEmpresa.Enabled = false;
                                ddlStatusLead.Enabled = false;
                                ddlTiposAfiliado.Enabled = false;
                                txbHoraIni.Disabled = true;
                                ddlTipoPago.Enabled = false;
                                ddlObjetivos.Enabled = false;
                                ddlCanalesMarketing.Enabled = false;
                                ddlPlanes.Enabled = false;
                                //rblMesesPlan.Enabled = false;
                                txaObservaciones.Disabled = true;
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
            int idUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
            decimal valorTotal = 0;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarContactosCRMPorUsuario(idUsuario, out valorTotal);

            rpContactosCRM.DataSource = dt;
            rpContactosCRM.DataBind();

            //ltValorTotal.Text = valorTotal.ToString("C0");
            dt.Dispose();
        }
        private void ListaMediosDePago()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarMediosDePago();

            ddlTipoPago.DataSource = dt;
            ddlTipoPago.DataBind();
            dt.Dispose();
        }
        private void ListaEmpresasCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEmpresasCRM();

            ddlEmpresa.DataSource = dt;
            ddlEmpresa.DataBind();
            //rpEmpresasCRM.DataSource = dt;
            //rpEmpresasCRM.DataBind();
            dt.Dispose();
        }
        private void ListaEstadosCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstadossCRM();
            foreach (DataRow row in dt.Rows)
            {
                ListItem item = new ListItem
                {
                    Text = $"<i class='{row["IconoMinEstadoCRM"]}'></i>{row["NombreEstadoCRM"]}",
                    Value = row["idEstadoCRM"].ToString()
                };

                item.Attributes["style"] = $"color: {row["ColorHexaCRM"]};";
                item.Attributes["data-icon"] = $"{row["IconoMinEstadoCRM"]}";
                item.Attributes["data-color"] = row["ColorHexaCRM"].ToString();

                ddlStatusLead.Items.Add(item);
            }
        }
        private void ListaTiposAfiliadosCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarTipoAfiliadCRM();

            ddlTiposAfiliado.DataSource = dt;
            ddlTiposAfiliado.DataBind();
            dt.Dispose();
        }
        private void CargarPlanes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanes();

            ddlPlanes.DataSource = dt;
            ddlPlanes.DataBind();
            dt.Dispose();
        }
        private void ListaObjetivosfiliadoCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarObjetivosAfiliados();

            ddlObjetivos.DataSource = dt;
            ddlObjetivos.DataBind();
            dt.Dispose();
        }
        private void ListaCanalesMarketingCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCanalesMarketingCRM();

            ddlCanalesMarketing.DataSource = dt;
            ddlCanalesMarketing.DataBind();
            dt.Dispose();
        }
        private void CargarTipoDocumento()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultartiposDocumento();
            ddlTipoDocumento.DataSource = dt;
            ddlTipoDocumento.DataBind();
            dt.Dispose();
        }

        #endregion

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editid"] != null)
                {
                    bool salida = false;
                    string mensaje = string.Empty;
                    string respuesta = string.Empty;
                    string mensajeValidacion = string.Empty;

                    if (ddlEmpresa.SelectedItem.Value != "")
                        ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(ddlEmpresa.SelectedItem.Value)));
                    else
                        ddlEmpresa.SelectedItem.Value = "0";

                    try
                    {
                        // Obtener y limpiar valores
                        string nombre = txbNombreContacto.Value?.ToString().Trim();
                        string apellido = txbApellidoContacto.Value?.ToString().Trim();
                        string telefono = Regex.Replace(txbTelefonoContacto.Value?.ToString().Trim(), @"\D", "");
                        string correo = txbCorreoContacto.Value?.ToString().Trim();
                        string fechaPrim = txbFechaPrim?.Value?.ToString().Trim();
                        string fechaProx = txbFechaProx?.Value?.ToString().Trim();
                        string valorPropuestaTexto = Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "");
                        string empresa = ddlEmpresa.SelectedItem?.Value;
                        string statusLead = ddlStatusLead.SelectedItem?.Value;
                        string objetivo = ddlObjetivos.SelectedItem?.Value;
                        string tipoPago = ddlTipoPago.SelectedItem?.Value;
                        string tipoAfiliado = ddlTiposAfiliado.SelectedItem?.Value;
                        string canalMarketing = ddlCanalesMarketing.SelectedItem?.Value;
                        string plan = ddlPlanes.SelectedItem?.Value;
                        string observaciones = txaObservaciones.Value.ToString().Trim();


                        // Validar campos requeridos
                        if (string.IsNullOrWhiteSpace(nombre) ||
                            string.IsNullOrWhiteSpace(apellido) ||
                            string.IsNullOrWhiteSpace(telefono) ||
                            string.IsNullOrWhiteSpace(correo) ||
                            string.IsNullOrWhiteSpace(empresa) ||
                            string.IsNullOrWhiteSpace(statusLead) ||
                            string.IsNullOrWhiteSpace(fechaPrim) ||
                            string.IsNullOrWhiteSpace(fechaProx) ||
                            string.IsNullOrWhiteSpace(valorPropuestaTexto) ||
                            string.IsNullOrWhiteSpace(statusLead) ||
                            string.IsNullOrWhiteSpace(objetivo) ||
                            string.IsNullOrWhiteSpace(tipoPago) ||
                            string.IsNullOrWhiteSpace(tipoAfiliado) ||
                            string.IsNullOrWhiteSpace(canalMarketing) ||
                            string.IsNullOrWhiteSpace(observaciones) ||
                            string.IsNullOrWhiteSpace(plan)
                            )
                        {
                            mensajeValidacion = "Todos los campos son obligatorios.";

                            ltMensaje.Text = "<div class=\"ibox-content\">" +
                             "<div class=\"alert alert-danger alert-dismissable\">" +
                             "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                             "Todos los campos son obligatorios." +
                             "</div></div>";
                            return;
                        }
                        else
                        {
                            respuesta = cg.ActualizarContactoCRM(Convert.ToInt32(Session["contactoId"].ToString()), txbNombreContacto.Value.ToString().Trim().ToUpper(),
                                    txbApellidoContacto.Value.ToString().Trim().ToUpper(), Regex.Replace(txbTelefonoContacto.Value.ToString().Trim(), @"\D", ""),
                                    txbCorreoContacto.Value.ToString().Trim().ToLower(), Convert.ToInt32(ddlEmpresa.SelectedItem.Value.ToString()),
                                    Convert.ToInt32(ddlStatusLead.SelectedItem.Value.ToString()), txbFechaPrim.Value.ToString(), txbFechaProx.Value.ToString(),
                                    Convert.ToInt32(Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "")), "", txaObservaciones.Value.Trim(),
                                    Convert.ToInt32(Session["idUsuario"]), Convert.ToInt32(ddlObjetivos.SelectedItem.Value.ToString()),
                                    Convert.ToInt32(ddlTipoPago.SelectedItem.Value.ToString()), Convert.ToInt32(ddlTiposAfiliado.SelectedItem.Value.ToString()),
                                    Convert.ToInt32(ddlCanalesMarketing.SelectedItem.Value.ToString()), Convert.ToInt32(ddlPlanes.SelectedItem.Value.ToString()), 0,
                                    Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value.ToString()), txbDocumento.Text, out salida, out mensaje);

                            if (salida)
                            {
                                string script = @"
                                Swal.fire({
                                    title: 'El contacto CRM se actualizó de forma exitosa',
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
                                        $('#ModalContacto').modal('show');
                                    }
                                });
                                ";
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string script = @"
                        Swal.fire({
                        title: 'Error',
                        text: 'Ha ocurrido un error inesperado.',
                        icon: 'error'
                    });
                ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                    }
                    //Response.Redirect("crmnuevocontacto");
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

                // Parseamos la fecha y la hora
                DateTime fecha = DateTime.Parse(txbFechaProx.Value);
                TimeSpan hora = TimeSpan.Parse(txbHoraIni.Value);
                DateTime fechaHora = fecha.Date + hora;
                string fechaHoraMySQL = fechaHora.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                TimeSpan tiempo = TimeSpan.Zero;

                if (int.TryParse(hfContador.Value, out segundosPasados))
                {
                    int minutos = segundosPasados / 60;
                    int segundos = segundosPasados % 60;
                    string tiempoFormateado = $"00:{minutos:D2}:{segundos:D2}";
                    tiempo = TimeSpan.Parse(tiempoFormateado);
                }

                try
                {
                    respuesta = cg.InsertarContactoCRM(txbNombreContacto.Value.ToString().Trim().ToUpper(), txbApellidoContacto.Value.ToString().Trim().ToUpper(),
                    Regex.Replace(txbTelefonoContacto.Value.ToString().Trim(), @"\D", ""), txbCorreoContacto.Value.ToString().Trim().ToLower(),
                    Convert.ToInt32(ddlEmpresa.SelectedItem.Value.ToString()), Convert.ToInt32(ddlStatusLead.SelectedItem.Value.ToString()), txbFechaPrim.Value.ToString(),
                    fechaHoraMySQL.ToString(), Convert.ToInt32(Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "")), "",
                    txaObservaciones.Value.Trim(), Convert.ToInt32(Session["idUsuario"]), Convert.ToInt32(ddlObjetivos.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlTipoPago.SelectedItem.Value.ToString()), Convert.ToInt32(ddlTiposAfiliado.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlCanalesMarketing.SelectedItem.Value.ToString()), Convert.ToInt32(ddlPlanes.SelectedItem.Value.ToString()), 0,
                    Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value.ToString()), txbDocumento.Text, tiempo.ToString(), out salida, out mensaje);

                    if (salida)
                    {
                        string script = @"
                        Swal.fire({
                            title: 'El contacto se creó de forma exitosa',
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


        protected void rpContactosCRM_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "crmnuevocontacto?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "crmnuevocontacto?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }

                //  ltTiempoTranscurrido: Hace X minutos
                if (row["FechaCreacion"] != DBNull.Value)
                {
                    DateTime fechaPrimerContacto = Convert.ToDateTime(row["FechaCreacion"]);
                    TimeSpan diferencia = DateTime.Now - fechaPrimerContacto;

                    string leyenda = "";
                    if (diferencia.TotalMinutes < 1)
                    {
                        leyenda = "Hace menos de un minuto";
                    }
                    else if (diferencia.TotalMinutes < 60)
                    {
                        int min = (int)Math.Floor(diferencia.TotalMinutes);
                        leyenda = $"Hace {min} minuto" + (min == 1 ? "" : "s");
                    }
                    else if (diferencia.TotalHours < 24)
                    {
                        int hrs = (int)Math.Floor(diferencia.TotalHours);
                        leyenda = $"Hace {hrs} hora" + (hrs == 1 ? "" : "s");
                    }
                    else
                    {
                        int dias = (int)Math.Floor(diferencia.TotalDays);
                        leyenda = $"Hace {dias} día" + (dias == 1 ? "" : "s");
                    }

                    Literal ltTiempo = (Literal)e.Item.FindControl("ltTiempoTranscurrido");
                    if (ltTiempo != null)
                    {
                        ltTiempo.Text = leyenda;
                    }
                }

            }
        }

        protected void ddlPlanes_SelectedIndexChanged(object sender, EventArgs e)
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

            clasesglobales cg = new clasesglobales();
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
                txaObservaciones.InnerText = observaciones;

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

        public class EstadoCRM
        {
            public int idEstadoCRM { get; set; }
            public string NombreEstadoCRM { get; set; }
            public string ColorHexaCRM { get; set; }
            public string IconoMinEstadoCRM { get; set; }
        }

        protected void btnAfiliado_Click(object sender, EventArgs e)
        {

            clasesglobales cg = new clasesglobales();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            bool esAfiliado = false;
            Session["esAfiliado"] = esAfiliado.ToString();
            int documento = 0;
            string[] strDocumento = txbAfiliado.Text.ToString().Split('-');
            if (int.TryParse(strDocumento[0], out documento))
            {
                dt = cg.ConsultarAfiliadoPorDocumento(documento);
            }

            dt1 = cg.ConsultarTipoAfiliadCRM();

            try
            {
                if (dt.Rows.Count > 0)
                {
                    esAfiliado = true;
                    Session["esAfiliado"] = esAfiliado.ToString();
                    txbDocumento.Text = documento.ToString();
                    ddlTipoDocumento.SelectedIndex = Convert.ToInt32(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt.Rows[0]["idTipoDocumento"].ToString())));
                    txbNombreContacto.Value = dt.Rows[0]["NombreAfiliado"].ToString();
                    txbApellidoContacto.Value = dt.Rows[0]["ApellidoAfiliado"].ToString();
                    txbTelefonoContacto.Value = dt.Rows[0]["CelularAfiliado"].ToString();
                    txbCorreoContacto.Value = dt.Rows[0]["EmailAfiliado"].ToString();
                    ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(dt.Rows[0]["idEmpresaAfil"].ToString())));
                    ddlTiposAfiliado.SelectedValue = "2";//Afiliado en renovación

                    ListaEmpresasCRM();
                    ListaMediosDePago();
                }
                dt.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //protected void ddlTiposAfiliado_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // Validar si seleccionaron la opción "Afiliado Nuevo"
        //    if (ddlTiposAfiliado.SelectedValue == "1")
        //    {
        //        string documento = txbDocumento.Text.Trim();
        //        string tipoDocumento = ddlTipoDocumento.SelectedValue;
        //        string nombreContacto = txbNombreContacto.Value.Trim();

        //        // Verificar que todos los campos requeridos tengan datos
        //        if (!string.IsNullOrEmpty(documento) &&
        //            !string.IsNullOrEmpty(tipoDocumento) &&
        //            !string.IsNullOrEmpty(nombreContacto))
        //        {
        //            // Construir URL con los parámetros
        //            string url = $"nuevoafiliado.aspx?doc={HttpUtility.UrlEncode(documento)}&tipo={HttpUtility.UrlEncode(tipoDocumento)}&nombre={HttpUtility.UrlEncode(nombreContacto)}";

        //            // Mostrar y configurar el enlace
        //            lnkNuevoAfiliado.NavigateUrl = url;
        //            lnkNuevoAfiliado.Visible = true;
        //        }
        //        else
        //        {
        //            // Si falta algún dato, no mostrar el enlace
        //            lnkNuevoAfiliado.Visible = false;
        //        }
        //    }
        //    else
        //    {
        //        // Si no se selecciona "Afiliado Nuevo", ocultar el enlace
        //        lnkNuevoAfiliado.Visible = false;
        //    }
        //}
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

            // 4. Devolver HTML
            return $"<a href='{enlace}' target='_blank'>" +
                   $"<i class='{icono} m-r-xs font-bold' style='color:{color};'></i> {telefonoFormateado}</a>";
        }

        protected void btnActualizarYRedirigir_Click(object sender, EventArgs e)
        {
            //// 1️⃣ Obtén los valores necesarios: ID, fecha, estado, observaciones.
            //int idContacto = Convert.ToInt32(hdnIdContacto.Value); // Por ejemplo en un HiddenField
            //DateTime nuevaFechaProx = DateTime.Now.AddDays(1); // o lo que tengas en un input
            //int nuevoEstadoId = Convert.ToInt32(ddlEstado.SelectedValue); // ejemplo: un DropDownList
            //string nuevasObservaciones = txtObservaciones.Text.Trim(); // ejemplo: un TextBox

            //// 2️⃣ Ejecuta la actualización
            //ActualizarContacto(idContacto, nuevaFechaProx, nuevoEstadoId, nuevasObservaciones);

            //// 3️⃣ Redirige a la URL deseada
            //Response.Redirect("crmnuevocontacto.aspx?editid=" + idContacto);
        }
    }
}