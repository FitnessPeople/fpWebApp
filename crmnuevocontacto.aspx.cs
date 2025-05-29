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
                    ValidarPermisos("Sedes");
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
                        txbFechaPrim.Attributes.Add("min", DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"));
                        txbFechaPrim.Value = DateTime.Now.ToString("yyyy-MM-dd");
                        txbFechaProx.Attributes.Add("type", "date");
                        txbFechaProx.Value = DateTime.Now.ToString("yyyy-MM-dd");
                        txbCorreoContacto.Attributes.Add("type", "email");

                        divBotonesLista.Visible = false;
                        btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                    ListaEmpresasCRM();
                    ListaEstadosCRM();
                    ListaContactos();
                    ListaTiposAfiliadosCRM();
                    CargarPlanes();
                    ListaCanalesMarketingCRM();
                    ListaObjetivosfiliadoCRM();
                    CargarCiudad();

                    //ltTitulo.Text = "Nuevo contacto";
                    //Literal1.Text = "Empresas";
                    if (Request.QueryString.Count > 0)
                    {
                        rpContactosCRM.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar

                            bool respuesta = false;
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarContactosCRMPorId(int.Parse(Request.QueryString["editid"].ToString()), out respuesta);
                            Session["contactoId"] = int.Parse(Request.QueryString["editid"].ToString());

                            if (respuesta)
                            {

                                if (dt.Rows.Count > 0)
                                {
                                    DataRow row = dt.Rows[0];

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
                                        ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(dt.Rows[0]["idEmpresaCRM"].ToString())));
                                    else
                                        ddlEmpresa.SelectedItem.Value = "0";

                                    ddlStatusLead.SelectedIndex = Convert.ToInt32(ddlStatusLead.Items.IndexOf(ddlStatusLead.Items.FindByValue(dt.Rows[0]["idEstadoCRM"].ToString())));
                                    txbFechaPrim.Value = Convert.ToDateTime(row["FechaPrimerCon"]).ToString("yyyy-MM-dd");
                                    txbFechaProx.Value = Convert.ToDateTime(row["FechaProximoCon"]).ToString("yyyy-MM-dd");
                                    int ValorPropuesta = Convert.ToInt32(dt.Rows[0]["ValorPropuesta"]);
                                    int ValorMes = Convert.ToInt32(dt.Rows[0]["ValorBase"]);
                                    txbValorPropuesta.Text = ValorPropuesta.ToString("C0", new CultureInfo("es-CO"));
                                    txbValorMes.Text = ValorMes.ToString("C0", new CultureInfo("es-CO"));
                                    //txaObservaciones.Value = row["observaciones"].ToString();
                                    ddlObjetivos.SelectedIndex = Convert.ToInt32(ddlObjetivos.Items.IndexOf(ddlObjetivos.Items.FindByValue(dt.Rows[0]["idObjetivo"].ToString())));
                                    ddlTipoPago.SelectedIndex = ddlTipoPago.Items.IndexOf(ddlTipoPago.Items.FindByValue(dt.Rows[0]["TipoPago"].ToString()));
                                    ddlTiposAfiliado.SelectedIndex = Convert.ToInt32(ddlTiposAfiliado.Items.IndexOf(ddlTiposAfiliado.Items.FindByValue(dt.Rows[0]["idTipoAfiliado"].ToString())));
                                    ddlCanalesMarketing.SelectedIndex = Convert.ToInt32(ddlCanalesMarketing.Items.IndexOf(ddlCanalesMarketing.Items.FindByValue(dt.Rows[0]["idCanalMarketing"].ToString())));
                                    ddlPlanes.SelectedIndex = Convert.ToInt32(ddlPlanes.Items.IndexOf(ddlPlanes.Items.FindByValue(dt.Rows[0]["idPlan"].ToString())));
                                    rblMesesPlan.SelectedIndex = Convert.ToInt32(rblMesesPlan.Items.IndexOf(rblMesesPlan.Items.FindByValue(dt.Rows[0]["MesesPlan"].ToString())));

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
                            DataTable dt = cg.ValidarArlEmpleados(int.Parse(Request.QueryString["deleteid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                ltMensaje.Text = "<div class=\"ibox-content\">" +
                                    "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                    "Este Contacto no se puede borrar, hay registros asociados a él" +
                                    "</div></div>";

                                DataTable dt1 = cg.ConsultarArlPorId(int.Parse(Request.QueryString["deleteid"].ToString()));

                                if (dt1.Rows.Count > 0)
                                {
                                    txbNombreContacto.Value = dt.Rows[0]["NombreContacto"].ToString();
                                    txbNombreContacto.Disabled = true;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    btnAgregar.Enabled = false;
                                    //ltTitulo.Text = "Borrar Contacto CRM";
                                }
                                dt1.Dispose();
                            }
                            else
                            {
                                //Borrar
                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarContactosCRMPorId(int.Parse(Request.QueryString["deleteid"].ToString()), out respuesta);
                                if (dt1.Rows.Count > 0)
                                {
                                    DataRow row = dt1.Rows[0];

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
                                    int ValorMes = Convert.ToInt32(dt.Rows[0]["ValorBase"]);
                                    txbValorMes.Text = ValorMes.ToString("C0", new CultureInfo("es-CO"));
                                    //txaObservaciones.Value = row["observaciones"].ToString();
                                    ddlObjetivos.SelectedIndex = Convert.ToInt32(ddlObjetivos.Items.IndexOf(ddlObjetivos.Items.FindByValue(dt1.Rows[0]["idObjetivo"].ToString())));
                                    ddlTipoPago.SelectedIndex = ddlTipoPago.Items.IndexOf(ddlTipoPago.Items.FindByValue(dt1.Rows[0]["TipoPago"].ToString()));
                                    ddlTiposAfiliado.SelectedIndex = Convert.ToInt32(ddlTiposAfiliado.Items.IndexOf(ddlTiposAfiliado.Items.FindByValue(dt1.Rows[0]["idTipoAfiliado"].ToString())));
                                    ddlCanalesMarketing.SelectedIndex = Convert.ToInt32(ddlCanalesMarketing.Items.IndexOf(ddlCanalesMarketing.Items.FindByValue(dt1.Rows[0]["idCanalMarketing"].ToString())));
                                    ddlPlanes.SelectedIndex = Convert.ToInt32(ddlPlanes.Items.IndexOf(ddlPlanes.Items.FindByValue(dt1.Rows[0]["idPlan"].ToString())));
                                    rblMesesPlan.SelectedIndex = Convert.ToInt32(rblMesesPlan.Items.IndexOf(rblMesesPlan.Items.FindByValue(dt1.Rows[0]["MesesPlan"].ToString())));

                                    //Inactivar controles
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
                                    rblMesesPlan.Enabled = false;
                                    txaObservaciones.Disabled = true;
                                    ArchivoPropuesta.Disabled = true;

                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    //ltTitulo.Text = "Borrar contacto CRM";
                                }
                                dt1.Dispose();
                            }
                        }
                        if (Request.QueryString["editidEmp"] != null)
                        {
                            //Editar

                            bool respuesta = false;
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarEmpresaCRMPorId(int.Parse(Request.QueryString["editidEmp"].ToString()), out respuesta);
                            Session["empresaCRMId"] = int.Parse(Request.QueryString["editidEmp"].ToString());
                            btnAgregarEmp.Text = "Actualizar";
                            if (respuesta)
                            {

                                if (dt.Rows.Count > 0)
                                {
                                    DataRow row = dt.Rows[0];

                                    txbNombreEmpresaCRM.Value = row["NombreEmpresaCRM"].ToString();
                                    txbPaginaWeb.Value = row["paginaWeb"].ToString();
                                    if (row["idEmpresaCRM"].ToString() != "")
                                        ddlContactos.SelectedIndex = Convert.ToInt32(ddlContactos.Items.IndexOf(ddlContactos.Items.FindByValue(dt.Rows[0]["idContacto"].ToString())));
                                    else
                                        ddlContactos.SelectedItem.Value = "0";

                                    txaObservacionesEmp.Value = row["observacionesEmp"].ToString();
                                    rblEstado.SelectedIndex = Convert.ToInt32(rblEstado.Items.IndexOf(rblEstado.Items.FindByValue(dt.Rows[0]["EstadoEmpresaCRM"].ToString())));
                                    ddlCiudad.SelectedIndex = Convert.ToInt32(ddlCiudad.Items.IndexOf(ddlCiudad.Items.FindByValue(dt.Rows[0]["idCiudad"].ToString())));
                                }
                            }
                            else
                            {
                                DataRow row = dt.Rows[0];
                                txbNombreContacto.Value = row["Error"].ToString(); ;
                            }
                        }
                        if (Request.QueryString["deleteidEmp"] != null)
                        {
                            //Borrar
                            bool respuesta = false;
                            clasesglobales cg = new clasesglobales();
                            DataTable dt1 = new DataTable();
                            dt1 = cg.ConsultarEmpresaCRMPorId(int.Parse(Request.QueryString["deleteidEmp"].ToString()), out respuesta);
                            if (dt1.Rows.Count > 0)
                            {
                                DataRow row = dt1.Rows[0];

                                txbNombreEmpresaCRM.Value = row["NombreEmpresaCRM"].ToString();
                                txbPaginaWeb.Value = row["paginaWeb"].ToString();


                                if (row["idContacto"].ToString() != "")
                                    ddlContactos.SelectedIndex = Convert.ToInt32(ddlContactos.Items.IndexOf(ddlContactos.Items.FindByValue(dt1.Rows[0]["idContacto"].ToString())));
                                else
                                    ddlContactos.SelectedItem.Value = "0";

                                if (row["EstadoEmpresaCRM"].ToString() != "")
                                    rblEstado.SelectedIndex = Convert.ToInt32(rblEstado.Items.IndexOf(rblEstado.Items.FindByValue(dt1.Rows[0]["EstadoEmpresaCRM"].ToString())));
                                else
                                    rblEstado.SelectedItem.Value = "0";

                                if (row["idCiudad"].ToString() != "")
                                    ddlCiudad.SelectedIndex = Convert.ToInt32(ddlCiudad.Items.IndexOf(ddlCiudad.Items.FindByValue(dt1.Rows[0]["idCiudad"].ToString())));
                                else
                                    ddlCiudad.SelectedItem.Value = "0";

                                txaObservacionesEmp.Value = row["ObservacionesEmp"].ToString();

                                //Inactivar controles
                                txbNombreEmpresaCRM.Disabled = true;
                                txbPaginaWeb.Disabled = true;
                                ddlCiudad.Enabled = false;
                                rblEstado.Enabled = false;
                                ddlContactos.Enabled = false;
                                txaObservacionesEmp.Disabled = true;

                                btnAgregarEmp.Text = "⚠ Confirmar borrado ❗";
                                //ltTitulo.Text = "Borrar empresa CRM";
                            }
                            dt1.Dispose();
                        }
                    }
                }

                else
                {
                    Response.Redirect("logout");
                }
            }
        }


        private void ListaContactos()
        {
            decimal valorTotal = 0;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarContactosCRM(out valorTotal);

            rpContactosCRM.DataSource = dt;
            rpContactosCRM.DataBind();
            ddlContactos.DataSource = dt;
            ddlContactos.DataBind();

            //ltValorTotal.Text = valorTotal.ToString("C0");
            dt.Dispose();
        }
        private void ListaEmpresasCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEmpresasCRM();

            ddlEmpresa.DataSource = dt;
            ddlEmpresa.DataBind();
            rpEmpresasCRM.DataSource = dt;
            rpEmpresasCRM.DataBind();
            dt.Dispose();
        }

        private void ListaEstadosCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstadossCRM();

            ddlStatusLead.DataSource = dt;
            ddlStatusLead.DataBind();
            dt.Dispose();
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
        private void CargarCiudad()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCiudadesCol();

            ddlCiudad.DataSource = dt;
            ddlCiudad.DataBind();

            dt.Dispose();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            //string contenidoEditor = hiddenEditor.Value;

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

                    //clasesglobales cg = new clasesglobales();
                    try
                    {
                        // Obtener y limpiar valores
                        string nombre = txbNombreContacto.Value?.ToString().Trim();
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


                        // Validar campos requeridos
                        if (string.IsNullOrWhiteSpace(nombre) ||
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
                            string.IsNullOrWhiteSpace(plan)
                            )
                        {
                            mensajeValidacion = "Todos los campos son obligatorios.";

                            //ltMensajeVal.Text = "<div class='alert alert-danger'>Todos los campos son obligatorios.</div>";
                            //MostrarModalEditar(Convert.ToInt32(Session["contactoId"]));
                            return;
                        }
                        else
                        {
                            respuesta = cg.ActualizarContactoCRM(Convert.ToInt32(Session["contactoId"].ToString()), txbNombreContacto.Value.ToString().Trim().ToUpper(),
                                    Regex.Replace(txbTelefonoContacto.Value.ToString().Trim(), @"\D", ""), txbCorreoContacto.Value.ToString().Trim().ToLower(),
                                    Convert.ToInt32(ddlEmpresa.SelectedItem.Value.ToString()), Convert.ToInt32(ddlStatusLead.SelectedItem.Value.ToString()),
                                    txbFechaPrim.Value.ToString(), txbFechaProx.Value.ToString(), Convert.ToInt32(Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "")), "",
                                    txaObservaciones.Value.Trim(), Convert.ToInt32(Session["idUsuario"]), Convert.ToInt32(ddlObjetivos.SelectedItem.Value.ToString()),
                                    ddlTipoPago.SelectedItem.Value.ToString(), Convert.ToInt32(ddlTiposAfiliado.SelectedItem.Value.ToString()),
                                    Convert.ToInt32(ddlCanalesMarketing.SelectedItem.Value.ToString()), Convert.ToInt32(ddlPlanes.SelectedItem.Value.ToString()),
                                    Convert.ToInt32(rblMesesPlan.SelectedValue), out salida, out mensaje);

                            if (salida)
                            {
                                string script = @"
                                Swal.fire({
                                    title: 'El contacto se actualizó de forma exitosa',
                                    text: '" + mensaje.Replace("'", "\\'") + @"',
                                    icon: 'success',
                                    timer: 3000, // 3 segundos
                                    showConfirmButton: false,
                                    timerProgressBar: true
                                }).then(() => {
                                    window.location.href = 'nuevocontactocrm';
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
                    Response.Redirect("crmnuevocontacto");
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
                                string tipoMensaje = respuesta ? "Éxito" : "Error";
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

                try
                {
                    respuesta = cg.InsertarContactoCRM(txbNombreContacto.Value.ToString().Trim().ToUpper(), Regex.Replace(txbTelefonoContacto.Value.ToString().Trim(), @"\D", ""),
                    txbCorreoContacto.Value.ToString().Trim().ToLower(), Convert.ToInt32(ddlEmpresa.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlStatusLead.SelectedItem.Value.ToString()), txbFechaPrim.Value.ToString(),
                    fechaHoraMySQL.ToString(), Convert.ToInt32(Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "")), "",
                    txaObservaciones.Value.Trim(), Convert.ToInt32(Session["idUsuario"]), Convert.ToInt32(ddlObjetivos.SelectedItem.Value.ToString()),
                    ddlTipoPago.SelectedItem.Value.ToString(), Convert.ToInt32(ddlTiposAfiliado.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlCanalesMarketing.SelectedItem.Value.ToString()), Convert.ToInt32(ddlPlanes.SelectedItem.Value.ToString()), Convert.ToInt32(rblMesesPlan.SelectedValue), out salida, out mensaje);

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

        protected void rblClaseSede_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void rpContactosCRM_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
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
            }
        }

        protected void ddlPlanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPlanes.SelectedValue) || ddlPlanes.SelectedValue == "0")
            {
                txbValorPropuesta.Text = "Por favor selecciona un plan válido.";
                ViewState["precioBase"] = null;
                ViewState["descuentoMensual"] = null;
                return;
            }

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanes();
            var fila = dt.Select("IdPlan = " + ddlPlanes.SelectedValue);
            if (fila.Length > 0)
            {
                ViewState["precioBase"] = fila[0]["PrecioBase"];
                ViewState["descuentoMensual"] = fila[0]["DescuentoMensual"];

                int ValorMes = Convert.ToInt32(fila[0]["PrecioBase"]);
                txbValorMes.Text = ValorMes.ToString("C0", new CultureInfo("es-CO"));
                txbValorMes.Enabled = false;

                string observaciones = fila[0]["DescripcionPlan"].ToString();
                txaObservaciones.InnerText = observaciones;

                // Verificar si el plan es permanente
                bool esPermanente = Convert.ToBoolean(fila[0]["Permanente"]);
                if (esPermanente)
                {
                    int mesesPlan = Convert.ToInt32(fila[0]["MesesMaximo"]); // Asegúrate que esta columna está en tu tabla

                    // Buscar índice del valor y seleccionarlo
                    int index = rblMesesPlan.Items.IndexOf(rblMesesPlan.Items.FindByValue(mesesPlan.ToString()));
                    if (index >= 0)
                    {
                        rblMesesPlan.ClearSelection();
                        rblMesesPlan.SelectedIndex = index;
                    }
                }
            }

            // Calcular propuesta si ya hay un valor seleccionado en el radio
            if (!string.IsNullOrEmpty(rblMesesPlan.SelectedValue))
            {
                CalcularPropuesta();
            }
            else
            {
                txbValorPropuesta.Text = "Primero selecciona los meses del plan.";
            }
        }


        protected void rblMesesPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar si ya hay un plan seleccionado
            if (ViewState["precioBase"] == null || ViewState["descuentoMensual"] == null)
            {
                txbValorPropuesta.Text = "Primero selecciona un plan.";
                return;
            }

            CalcularPropuesta();
        }

        private void CalcularPropuesta()
        {
            if (ViewState["precioBase"] == null || ViewState["descuentoMensual"] == null || string.IsNullOrEmpty(rblMesesPlan.SelectedValue))
            {
                txbValorPropuesta.Text = "Faltan datos para calcular.";
                return;
            }

            if (ddlPlanes.SelectedValue == "0")
            {
                txbValorPropuesta.Text = "Debes seleccionar un plan válido.";
                return;
            }

            int precioBase = Convert.ToInt32(ViewState["precioBase"]);
            double descuentoMensual = Convert.ToDouble(ViewState["descuentoMensual"]);
            int meses = Convert.ToInt32(rblMesesPlan.SelectedValue);

            double descuento = (meses - 1) * descuentoMensual;
            double total = (precioBase - ((precioBase * descuento) / 100)) * meses;

            txbValorPropuesta.Text = $"${total:N0}";
        }

        protected void btnAgregarEmp_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();

            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editidEmp"] != null)
                {
                    bool salida = false;
                    string mensaje = string.Empty;
                    string respuesta = string.Empty;
                    string mensajeValidacion = string.Empty;

                    if (ddlCiudad.SelectedItem.Value != "")
                        ddlCiudad.SelectedIndex = Convert.ToInt32(ddlCiudad.Items.IndexOf(ddlCiudad.Items.FindByValue(ddlCiudad.SelectedItem.Value)));
                    else
                        ddlCiudad.SelectedItem.Value = "0";

                    if (ddlContactos.SelectedItem.Value != "")
                        ddlContactos.SelectedIndex = Convert.ToInt32(ddlContactos.Items.IndexOf(ddlContactos.Items.FindByValue(ddlContactos.SelectedItem.Value)));
                    else
                        ddlContactos.SelectedItem.Value = "0";

                    try
                    {
                        // Obtener y limpiar valores
                        string nombreEmp = txbNombreEmpresaCRM.Value?.ToString().Trim();
                        string paginaWeb = txbPaginaWeb.Value?.ToString().Trim();
                        string contacto = ddlContactos.SelectedItem?.Value;
                        string observaciones = txaObservacionesEmp.Value?.ToString().Trim();
                        string ciudad = ddlCiudad.SelectedItem?.Value;
                        string estado = rblEstado.SelectedItem?.Value;


                        // Validar campos requeridos
                        if (string.IsNullOrWhiteSpace(nombreEmp) ||
                            string.IsNullOrWhiteSpace(paginaWeb) ||
                            string.IsNullOrWhiteSpace(contacto) ||
                            string.IsNullOrWhiteSpace(observaciones) ||
                            string.IsNullOrWhiteSpace(ciudad) ||
                            string.IsNullOrWhiteSpace(estado)
                            )
                        {
                            mensajeValidacion = "Todos los campos son obligatorios.";
                            return;
                        }
                        else
                        {
                            respuesta = cg.ActualizarEmpresaCRM(Convert.ToInt32(Request.QueryString["editidEmp"].ToString()), txbNombreEmpresaCRM.Value.ToString().Trim(),
                                        txbPaginaWeb.Value.ToString().Trim().ToLower(), Convert.ToInt32(ddlContactos.SelectedItem.Value.ToString()),
                                        Convert.ToInt32(Session["idUsuario"]), txaObservacionesEmp.Value.Trim(), rblEstado.SelectedItem.Value.ToString(), Convert.ToInt32(ddlCiudad.SelectedValue),
                                        out salida, out mensaje);

                            if (salida)
                            {
                                string script = @"
                                Swal.fire({
                                    title: 'La empresa CRM se actualizó de forma exitosa',
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
                                });
                                ";
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        mensaje = ex.Message.Replace("'", "\\'").Replace("\n", "").Replace("\r", "");
                        string script = @"
                        Swal.fire({
                            title: 'Error',
                            text: '" + mensaje + @"',
                            icon: 'error'
                        });
                        ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                    }

                    //Response.Redirect("crmnuevocontacto");
                }
                if (Request.QueryString["deleteidEmp"] != null)
                {
                    bool respuesta = false;
                    bool _respuesta = false;
                    string mensaje = string.Empty;
                    int idempresaCRM = Convert.ToInt32(Request.QueryString["deleteidEmp"].ToString());
                    int idUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
                    string Usuario = Session["NombreUsuario"].ToString();

                    try
                    {
                        DataTable dt = cg.ConsultarEmpresaCRMPorId(idempresaCRM, out _respuesta);
                        Session["contactoId"] = idempresaCRM;

                        if (idempresaCRM > 0)
                        {
                            cg.EliminarEmpresaCRM(idempresaCRM, idUsuario, Usuario, out respuesta, out mensaje);

                            if (respuesta)
                            {
                                string tipoMensaje = respuesta ? "Éxito" : "Error";
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
                        mensaje = ex.Message.Replace("'", "\\'").Replace("\n", "").Replace("\r", "");
                        string script = @"
                        Swal.fire({
                            title: 'Error',
                            text: '" + mensaje + @"',
                            icon: 'error'
                        });
                        ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                    }
                }
            }
            else
            {
                if (ddlContactos.SelectedItem.Value != "")
                    ddlContactos.SelectedIndex = Convert.ToInt32(ddlContactos.Items.IndexOf(ddlContactos.Items.FindByValue(ddlContactos.SelectedItem.Value)));
                else
                    ddlContactos.SelectedItem.Value = "0";

                bool salida = false;
                string mensaje = string.Empty;
                string mensajeValidacion = string.Empty;
                string respuesta = string.Empty;
                string nombreEmp = txbNombreEmpresaCRM.ToString().Trim();

                try
                {
                    respuesta = cg.InsertarEmpresaCRM(txbNombreEmpresaCRM.Value.ToString().Trim(), txbPaginaWeb.Value.ToString().Trim().ToUpper(),
                        Convert.ToInt32(ddlContactos.SelectedItem.Value.ToString()), Convert.ToInt32(Session["idUsuario"]), txaObservacionesEmp.Value.Trim(),
                        rblEstado.SelectedItem.Value.ToString(), Convert.ToInt32(ddlCiudad.SelectedValue), out salida, out mensaje);

                    if (salida)
                    {
                        string script = @"
                        Swal.fire({
                            title: 'la empresa se creó para CRM de forma exitosa ',
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

        protected void rpEmpresasCRM_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditarEmp = (HtmlAnchor)e.Item.FindControl("btnEditarEmp");
                    btnEditarEmp.Attributes.Add("href", "crmnuevocontacto?editidEmp=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditarEmp.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminarEmp = (HtmlAnchor)e.Item.FindControl("btnEliminarEmp");
                    btnEliminarEmp.Attributes.Add("href", "crmnuevocontacto?deleteidEmp=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminarEmp.Visible = true;
                }
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrarAcordeon", "$('#acordeonZonaLateralIzqSup').collapse('hide');", true);

            }
        }
    }
}