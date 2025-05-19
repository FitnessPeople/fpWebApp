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

                    ltTitulo.Text = "Nuevo contacto";
                    Literal1.Text = "Empresas";
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
                                    txbValorPropuesta.Text = ValorPropuesta.ToString("C0", new CultureInfo("es-CO"));
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

                            //clasesglobales cg = new clasesglobales();
                            //DataTable dt = cg.ConsultarSedePorId(int.Parse(Request.QueryString["editid"].ToString()));
                            //if (dt.Rows.Count > 0)
                            //{
                            //    //string contenidoEditor = hiddenEditor.Value;
                            //    //txbSede.Text = dt.Rows[0]["NombreSede"].ToString();
                            //    //txbDireccion.Text = dt.Rows[0]["DireccionSede"].ToString();
                            //    //ddlCiudadSede.SelectedIndex = Convert.ToInt16(ddlCiudadSede.Items.IndexOf(ddlCiudadSede.Items.FindByValue(dt.Rows[0]["idCiudadSede"].ToString())));
                            //    //txbTelefono.Text = dt.Rows[0]["TelefonoSede"].ToString();
                            //    //hiddenEditor.Value = dt.Rows[0]["HorarioSede"].ToString();
                            //    //rblTipoSede.SelectedValue = dt.Rows[0]["TipoSede"].ToString();
                            //    //rblClaseSede.SelectedValue = dt.Rows[0]["ClaseSede"].ToString();
                            //    btnAgregar.Text = "Actualizar";
                            //    ltTitulo.Text = "Actualizar sede";
                            //}
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            //Borrar
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

            //ltValorTotal.Text = valorTotal.ToString("C0");
            dt.Dispose();
        }
        private void ListaEmpresasCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEmpresasCRM();

            ddlEmpresa.DataSource = dt;
            ddlEmpresa.DataBind();
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



        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            //string contenidoEditor = hiddenEditor.Value;

            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editid"] != null)
                {
                    //string strInitData = TraerData();
                    //try
                    //{
                    //    //string respuesta = cg.ActualizarSede(int.Parse(Request.QueryString["editid"].ToString()), txbSede.Text.ToString().Trim(), txbDireccion.Text.ToString().Trim(), int.Parse(ddlCiudadSede.SelectedItem.Value.ToString()), txbTelefono.Text.ToString().Trim(), contenidoEditor, rblTipoSede.SelectedValue.ToString(), rblClaseSede.SelectedValue.ToString());
                    //    string strNewData = TraerData();

                    //    //cg.InsertarLog(Session["idusuario"].ToString(), "sedes", "Modifica", "El usuario modificó datos a la sede " + txbSede.Text.ToString() + ".", strInitData, strNewData);
                    //}
                    //catch (Exception ex)
                    //{
                    //    string mensaje = ex.Message;
                    //}
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
                                    txaObservaciones.Value.Trim(), Convert.ToInt32(Session["idUsuario"] ), Convert.ToInt32(ddlObjetivos.SelectedItem.Value.ToString()),
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
                        text: '"+ mensaje.Replace("'", "\\'") + @"',
                        icon: 'error'
                    });
                ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                }

                //Response.Redirect("crmnuevocontacto");
                //}
                //else
                //{
                //    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                //        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                //        "Ya existe una sede con ese nombre." +
                //        "</div>";
                //}
            }
        }

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarSedePorId(int.Parse(Request.QueryString["editid"].ToString()));

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();
            return strData;
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
            if (ddlPlanes.SelectedValue == "0")
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
            }

            // Verificar si ya hay mes seleccionado
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



    }
}