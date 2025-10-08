using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class listacontactoscrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idContacto = 0;
                int idEmpresa = 0;
                bool hayContacto = int.TryParse(Request.QueryString["idContacto"], out idContacto);
                bool hayEmpresa = int.TryParse(Request.QueryString["empresaId"], out idEmpresa);

                if (hayContacto)
                {
                    Session["contactoId"] = idContacto;
                    Session["empresaId"] = null;
                    pnlContacto.Visible = true;
                    pnlEmpresa.Visible = false;
                    CargarDatosContacto(idContacto);
                }
                else if (hayEmpresa)
                {
                    Session["empresaId"] = idEmpresa;
                    Session["contactoId"] = null;
                    pnlEmpresa.Visible = true;
                    pnlContacto.Visible = false;
                    CargarDatosEmpresaCRM(idEmpresa);

                    // Activar tab-2 (Empresas) visualmente con JS
                    ScriptManager.RegisterStartupScript(this, GetType(), "activarTab", "$('a[href=\"#tab-2\"]').tab('show');", true);
                }

                // Si no hay ninguno en la URL, puedes mostrar el último contacto por defecto
                if (!hayContacto && !hayEmpresa)
                {
                    clasesglobales cg = new clasesglobales();
                    decimal valorT = 0;
                    DataTable dtContactos = cg.ConsultarContactosCRM(out valorT);

                    if (dtContactos.Rows.Count > 0)
                    {
                        int ultimoIdContacto = Convert.ToInt32(dtContactos.Rows[0]["IdContacto"]);
                        Session["contactoId"] = ultimoIdContacto;
                        Session["empresaId"] = null;
                        pnlContacto.Visible = true;
                        pnlEmpresa.Visible = false;
                        CargarDatosContacto(ultimoIdContacto);
                    }
                }

                // Mostrar modal si está seteado
                if (ViewState["AbrirModal"] != null && (bool)ViewState["AbrirModal"] == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "$('#ModalContacto').modal('show');", true);
                    ViewState["AbrirModal"] = null;
                }

                // Validar usuario y permisos
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Lista contactos CRM");

                    if (ViewState["SinPermiso"]?.ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                    }
                    else
                    {
                        if (ViewState["CrearModificar"]?.ToString() == "1")
                        {
                            txbFechaPrim.Attributes.Add("type", "date");
                            txbFechaPrim.Attributes.Add("min", DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"));
                            txbFechaPrim.Value = DateTime.Now.ToString("yyyy-MM-dd");
                            txbFechaProx.Attributes.Add("type", "date");
                            txbFechaProx.Value = DateTime.Now.ToString("yyyy-MM-dd");
                            txbCorreoContacto.Attributes.Add("type", "email");
                        }

                        rpContactosCRM.ItemDataBound += rpContactosCRM_ItemDataBound;

                        ConsultarEmpresasCRM();
                        ListaEstadosCRM();
                        ListaContactos();
                        ltFechaHoy.Text = DateTime.Now.ToString("h:mm tt - dd.MM.yyyy").ToLower();
                    }
                }
                else
                {
                    Response.StatusCode = 401;
                    Response.End();
                    Response.Redirect("logout.aspx");
                }

                // Activar validación botón si aplica
                ScriptManager.RegisterStartupScript(this, GetType(), "activarBoton", "setTimeout(validarBotonActualizar, 100);", true);
            }

            // Forzar visibilidad en caso de PostBack
            pnlContacto.Visible = Session["contactoId"] != null;
            pnlEmpresa.Visible = Session["empresaId"] != null && Session["contactoId"] == null;
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

        private void ListaContactos()
        {
            try
            {
                decimal valorTotal = 0;
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarContactosCRM(out valorTotal);
                if(dt.Rows.Count > 0)
                {
                    rpContactosCRM.DataSource = dt;
                    rpContactosCRM.DataBind();

                    //ltValorTotal.Text = valorTotal.ToString("C0");
                    dt.Dispose();
                }


            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
               
            }

        }
        private void ConsultarEmpresasCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEmpresasCRM();

            ddlEmpresa.DataSource = dt;
            ddlEmpresa.DataBind();
            rpEmpresaCRM.DataSource = dt;
            rpEmpresaCRM.DataBind();
            dt.Dispose();
        }
        private void CargarDatosEmpresaCRM(int idEmpresaCMR)
        {
            try
            {
                bool respuesta = false;
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarEmpresaCRMPorId(idEmpresaCMR, out respuesta);
                rpContenidoEmpresaCRM.DataSource = dt;
                rpContenidoEmpresaCRM.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }

        }
        private void ListaEstadosCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstadossCRM();

            ddlStatusLead.DataSource = dt;
            ddlStatusLead.DataBind();
            dt.Dispose();
        }

        protected void rpContactosCRM_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            bool salida = false;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                int idContacto = Convert.ToInt32(drv["IdContacto"]);

                // Lógica para obtener historial por contacto
                DataTable historial = cg.ConsultarHistorialPorContactoCMR(idContacto, out salida); // tu función personalizada

                // Buscar el repeater hijo
                Repeater rptHistorial = (Repeater)e.Item.FindControl("rptHistorial");

                if (historial != null && rptHistorial != null)
                {
                    rptHistorial.DataSource = historial;
                    rptHistorial.DataBind();
                }
            }
        }



        private void CargarDatosContacto(int idContacto)
        {
            bool respuesta = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarContactosCRMPorId(idContacto, out respuesta);
            Session["contactoId"] = idContacto;
            rptContenido.DataSource = dt;
            rptContenido.DataBind();

            if (respuesta)
            {

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    txbNombreContacto.Value = row["NombreContacto"].ToString();
                    txbNombreContacto.Disabled = true;
                    txbTelefonoContacto.Disabled = true;
                    ddlEmpresa.Enabled = false;
                    txbFechaPrim.Disabled = true;
                    txbCorreoContacto.Disabled = true;
                    //txbValorPropuesta.Enabled = false;
                    //ArchivoPropuesta.Disabled = true;
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
                    //txbValorPropuesta.Text = ValorPropuesta.ToString("C0", new CultureInfo("es-CO"));
                    //txaObservaciones.Value = row["observaciones"].ToString().Trim();
                }
            }
            else
            {
                DataRow row = dt.Rows[0];
                txbNombreContacto.Value = row["Error"].ToString(); ;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            bool salida = false;
            ViewState["AbrirModal"] = true;
            string mensaje = string.Empty;
            string mensajeValidacion = string.Empty;
            string respuesta = string.Empty;

            if (ddlEmpresa.SelectedItem.Value != "")
                ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(ddlEmpresa.SelectedItem.Value)));
            else
                ddlEmpresa.SelectedItem.Value = "0";

            clasesglobales cg = new clasesglobales();
            try
            {
                //respuesta = cg.InsertarContactoCRM(txbNombreContacto.Value.ToString().Trim().ToUpper(), Regex.Replace(txbTelefonoContacto.Value.ToString().Trim(), @"\D", ""),
                //txbCorreoContacto.Value.ToString().Trim().ToLower(), Convert.ToInt32(ddlEmpresa.SelectedItem.Value.ToString()),
                //Convert.ToInt32(ddlStatusLead.SelectedItem.Value.ToString()), txbFechaPrim.Value.ToString(),
                //fechaHoraMySQL.ToString(), Convert.ToInt32(Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "")), "",
                //txaObservaciones.Value.Trim(), Convert.ToInt32(Session["idUsuario"]), Convert.ToInt32(ddlObjetivos.SelectedItem.Value.ToString()),
                //ddlTipoPago.SelectedItem.Value.ToString(), Convert.ToInt32(ddlTiposAfiliado.SelectedItem.Value.ToString()),
                //Convert.ToInt32(ddlCanalesMarketing.SelectedItem.Value.ToString()), Convert.ToInt32(ddlPlanes.SelectedItem.Value.ToString()),
                //Convert.ToInt32(rblMesesPlan.SelectedValue), out salida, out mensaje);

                if (salida)
                {
                    string script = @"
                        $('#ModalContacto').modal('hide');
                        $('.modal-backdrop').remove();
                        Swal.fire({
                            title: 'El contacto se creó de forma exitosa',
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
                            $('#ModalContacto').modal('hide');
                            $('.modal-backdrop').remove();
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
            catch (Exception ex)
            {
                string script = @"
                    $('#ModalContacto').modal('hide');
                    $('.modal-backdrop').remove();
                    Swal.fire({
                        title: 'Error',
                        text: '" + ex.Message.Replace("'", "\\'") + @"',
                        icon: 'error'
                    });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
            }
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

        protected string GetEnlaceWeb(object url)
        {
            string pagina = url as string;
            if (!string.IsNullOrEmpty(pagina))
            {
                return $"<a href='{pagina}' target='_blank'>Visitar sitio</a>";
            }
            return "";
        }

        protected string FormatearUbicacion(object ciudad, object estado)
        {
            string c = ciudad as string ?? "";
            string e = estado as string ?? "";
            if (!string.IsNullOrEmpty(c) && !string.IsNullOrEmpty(e))
                return $"{c} - {e}";
            else
                return c + e; // Si uno está vacío, solo muestra el otro
        }

        protected string FormatearCOP(object valor)
        {
            if (valor == null || valor == DBNull.Value) return "";

            decimal monto = Convert.ToDecimal(valor);
            CultureInfo culturaCol = new CultureInfo("es-CO");
            return monto.ToString("C0", culturaCol); // C0 = sin decimales
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            btnActualizar.Visible = true;
            btnAgregar.Visible = false;
            Button btnEditar = (Button)sender;
            int idContacto = Convert.ToInt32(btnEditar.CommandArgument);

            if (idContacto > 0)
            {
                CargarDatosContacto(idContacto);
                upModal.Update();
                ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", "$('#ModalContacto').modal('show');", true);
            }
        }

        private void MostrarModalEditar(int idContacto)
        {
            CargarDatosContacto(idContacto);
            upModal.Update();
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", "$('#ModalContacto').modal('show');", true);
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            bool salida = false;
            string mensaje = string.Empty;
            string respuesta = string.Empty;
            string mensajeValidacion = string.Empty;

            if (ddlEmpresa.SelectedItem.Value != "")
                ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(ddlEmpresa.SelectedItem.Value)));
            else
                ddlEmpresa.SelectedItem.Value = "0";

            clasesglobales cg = new clasesglobales();
            try
            {
                // Obtener y limpiar valores
                string nombre = txbNombreContacto.Value?.ToString().Trim();
                string telefono = Regex.Replace(txbTelefonoContacto.Value?.ToString().Trim(), @"\D", "");
                string correo = txbCorreoContacto.Value?.ToString().Trim();
                string fechaPrim = txbFechaPrim?.Value?.ToString().Trim();
                string fechaProx = txbFechaProx?.Value?.ToString().Trim();
               //string valorPropuestaTexto = Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "");
                string empresa = ddlEmpresa.SelectedItem?.Value;
                string statusLead = ddlStatusLead.SelectedItem?.Value;

                // Validar campos requeridos
                if (string.IsNullOrWhiteSpace(nombre) ||
                    string.IsNullOrWhiteSpace(telefono) ||
                    string.IsNullOrWhiteSpace(correo) ||
                    string.IsNullOrWhiteSpace(empresa) ||
                    string.IsNullOrWhiteSpace(statusLead) ||
                    string.IsNullOrWhiteSpace(fechaPrim) ||
                    string.IsNullOrWhiteSpace(fechaProx) )
                    //string.IsNullOrWhiteSpace(valorPropuestaTexto))
                {
                    mensajeValidacion = "Todos los campos son obligatorios.";

                    ltMensajeVal.Text = "<div class='alert alert-danger'>Todos los campos son obligatorios.</div>";
                    MostrarModalEditar(Convert.ToInt32(Session["contactoId"]));
                    return;
                }
                else
                {
                    //respuesta = cg.ActualizarContactoCRM(Convert.ToInt32(Session["contactoId"].ToString()), txbNombreContacto.Value.ToString().Trim().ToUpper(),
                    //        Regex.Replace(txbTelefonoContacto.Value.ToString().Trim(), @"\D", ""), txbCorreoContacto.Value.ToString().Trim().ToLower(),
                    //        Convert.ToInt32(ddlEmpresa.SelectedItem.Value.ToString()), Convert.ToInt32(ddlStatusLead.SelectedItem.Value.ToString()),
                    //        txbFechaPrim.Value.ToString(), txbFechaProx.Value.ToString(), Convert.ToInt32(Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "")), "",
                    //        txaObservaciones.Value.Trim(), Convert.ToInt32(Session["idUsuario"]), Convert.ToInt32(ddlObjetivos.SelectedItem.Value.ToString()),
                    //        ddlTipoPago.SelectedItem.Value.ToString(), Convert.ToInt32(ddlTiposAfiliado.SelectedItem.Value.ToString()),
                    //        Convert.ToInt32(ddlCanalesMarketing.SelectedItem.Value.ToString()), Convert.ToInt32(ddlPlanes.SelectedItem.Value.ToString()),
                    //        Convert.ToInt32(rblMesesPlan.SelectedValue), out salida, out mensaje);

                    if (salida)
                    {
                        string script = @"
                            $('#ModalContacto').modal('hide');
                            $('.modal-backdrop').remove();
                            Swal.fire({
                                title: 'El contacto se actualizó de forma exitosa',
                                text: '" + mensaje.Replace("'", "\\'") + @"',
                                icon: 'success',
                                timer: 3000, // 3 segundos
                                showConfirmButton: false,
                                timerProgressBar: true
                            }).then(() => {
                                window.location.href = 'listacontactoscrm';
                            });
                        ";

                        ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                    }
                    else
                    {
                        string script = @"
                            $('#ModalContacto').modal('hide');
                            $('.modal-backdrop').remove();
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
                    $('#ModalContacto').modal('hide');
                    $('.modal-backdrop').remove();
                    Swal.fire({
                        title: 'Error',
                        text: '" + ex.Message.Replace("'", "\\'") + @"',
                        icon: 'error'
                    });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
            }

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            bool respuesta = false;
            clasesglobales cg = new clasesglobales();

            int idContacto = Convert.ToInt32(btnEliminar.CommandArgument);

            DataTable dt = cg.ConsultarContactosCRMPorId(idContacto, out respuesta);
            Session["Contacto"] = dt.Rows[0]["NombreContacto"].ToString();


            //if (idContacto > 0)
            //{
            //    Session["contactoId"] = idContacto;

            //    string nombreContacto = Session["Contacto"].ToString().Trim();
            //    ltEliminar.Text = $@"
            //    <div style='color: #b30000; font-weight: bold; '>
            //        ⚠ ¿Está seguro de que desea eliminar el contacto <span style='text-decoration: underline;'>{nombreContacto}</span>?
            //    </div>";

            //    upEliminar.Update();
            //    ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", "$('#Modaleliminar').modal('show');", true);
            //}
        }

        protected void btnAccionEliminar_Click(object sender, EventArgs e)
        {
            //ltEliminar.Text = string.Empty;
            bool respuesta = false;
            bool _respuesta = false;
            string mensaje = string.Empty;
            int idContacto = Convert.ToInt32(Session["contactoId"]);
            int idUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
            string Usuario = Session["NombreUsuario"].ToString();
            clasesglobales cg = new clasesglobales();

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
                                $('#Modaleliminar').modal('hide');
                                $('.modal-backdrop').remove();
                                Swal.fire({
                                    title: '" + tipoMensaje + @"',
                                    text: '" + mensaje + @"',
                                    icon: '" + tipoIcono + @"'
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        location.reload();
                                    }
                                });
                            ";

                        ScriptManager.RegisterStartupScript(this, GetType(), "EliminarYAlerta", script, true);
                    }
                    else
                    {
                        string script = @"
                            $('#ModalContacto').modal('hide');
                            $('.modal-backdrop').remove();
                            Swal.fire({
                                title: 'Error',
                                text: '" + mensaje.Replace("'", "\\'") + @"',
                                icon: 'error',
                                timer: 3000,
                                timerProgressBar: true,
                                showConfirmButton: false
                            }).then(() => {
                                location.reload();
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
                    $('#ModalContacto').modal('hide');
                    $('.modal-backdrop').remove();
                    Swal.fire({
                        title: 'Error',                       
                        text: '"" + mensaje.Replace(""'"", ""\\'"") + @""',
                        icon: 'error'
                    });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
            }
        }

        protected void btnGestionarContacto_Command(object sender, CommandEventArgs e)
        {
            string respuesta = string.Empty;
            try
            {
                clasesglobales cg = new clasesglobales();
                     
                int idContacto = Convert.ToInt32(e.CommandArgument);               
                int idUsuario = Convert.ToInt32(Session["idUsuario"]);               
                respuesta =  cg.ActualizarUsuarioGestionaCRM(idContacto, idUsuario);
               
                Response.Redirect("crmnuevocontacto.aspx");
            }
            catch (Exception ex)            
            {              
                Console.WriteLine("Error: " + ex.Message);
            }
        }





    }
}