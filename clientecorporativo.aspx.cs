using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class clientecorporativo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Cliente corporativo");
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
                        btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            ListaClienteCorporativo();
                            listaEmpresasAfiliadas();
                            CargarCanalesAsesores();
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {

                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            ListaClienteCorporativo();
                            btnAgregar.Visible = true;
                          
                        }
                        if (ViewState["Borrar"].ToString() == "1")
                        {
                            lnkAsignar.Visible = true;
                        }
                    }

                    CargarTipoDocumento();

                    ltTitulo.Text = "Agregar cliente corporativo";

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

        private void ListaClienteCorporativo()
        {
            clasesglobales cg = new clasesglobales();
            try
            {                
                DataTable dt = cg.ConsultarProspectosCRM();
                gvProspectos.DataSource = dt;
                gvProspectos.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }

        }
        private void MostrarAlerta(string titulo, string mensaje, string tipo)
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


        private void listaEmpresasAfiliadas()
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarEmpresasYProspectosCorporativos();

                ddlEmpresas.DataSource = dt;
                ddlEmpresas.DataValueField = "DocumentoEmpresa";  // Ahora SÍ existe
                ddlEmpresas.DataTextField = "NombreEmpresa";
                ddlEmpresas.DataBind();             

            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Ocurrió un error al cargar las empresas. Por favor intente nuevamente. " + ex.Message.ToString();
                lblMensaje.CssClass = "text-danger";
            }
        }

        //private void listaEmpresasAfiliadas()
        //{
        //    try
        //    {
        //        clasesglobales cg = new clasesglobales();
        //        DataTable dt = cg.ConsultarEmpresasYProspectosCorporativos();

        //        ddlEmpresas.DataSource = dt;

        //        ddlEmpresas.DataValueField = "DocumentoEmpresa";  // ← Valor que necesitas insertar
        //        ddlEmpresas.DataTextField = "NombreEmpresa";      // ← Nombre visible

        //        ddlEmpresas.DataBind();

        //        ddlEmpresas.Items.Insert(0, new ListItem("Seleccione", "")); // opcional

        //        dt.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensaje.Visible = true;
        //        lblMensaje.Text = "Ocurrió un error al cargar las empresas. Por favor intente nuevamente. " + ex.ToString();
        //        lblMensaje.CssClass = "text-danger";
        //    }
        //}

        private void CargarTipoDocumento()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultartiposDocumento();

            ddlTipoDocumento.DataSource = dt;
            ddlTipoDocumento.DataBind();

            dt.Dispose();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
          
            clasesglobales cg = new clasesglobales();
            DataTable dt1 = cg.ConsultarAfiliadoPorDocumento(Convert.ToInt32(txbDocumento.Text.ToString()));

            //if (dt1.Rows.Count > 0)
            //{
            //    string script = @"
            //        Swal.fire({
            //            title: 'Mensaje',
            //            text: 'Ya existe un afiliado registrado con este documento.',
            //            icon: 'error'
            //        }).then((result) => {
            //            if (result.isConfirmed) {
                                            
            //            }
            //        });
            //        ";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
            //}
            //else
            //{
                // Consultar si el prospecto existe en la tabla ContactosCRM.
                DataTable dt2 = cg.ConsultarContactosCRMPorDocumento(Convert.ToInt32(txbDocumento.Text.ToString()));

                //if (dt2.Rows.Count > 0)
                //{
                //    string script = @"
                //        Swal.fire({
                //            title: 'Mensaje',
                //            text: 'Ya existe un contacto en el CRM registrado con este documento.',
                //            icon: 'error'
                //        }).then((result) => {
                //            if (result.isConfirmed) {
                                            
                //            }
                //        });
                //        ";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                //}
                //else
                //{
                    // Consultar si el prospecto existe en la tabla PregestionCRM.
                    DataTable dt3 = cg.ConsultarPregestionCRMPorDocumento(Convert.ToInt32(txbDocumento.Text.ToString()));

                    //if (dt3.Rows.Count > 0)
                    //{
                    //    string script = @"
                    //    Swal.fire({
                    //        title: 'Mensaje',
                    //        text: 'Ya existe este documento en PregestionCRM.',
                    //        icon: 'error'
                    //    }).then((result) => {
                    //        if (result.isConfirmed) {
                                            
                    //        }
                    //    });
                    //    ";
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                    //}
                    //else
                    //{
                        string nombre = txbNombreContacto.Text.ToString();
                        string apellido = txbApellidoContacto.Text.ToString();
                        string documento = txbDocumento.Text.ToString();
                        int idTipoDocumento = Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value.ToString());
                        string celular = txbCelular.Text.ToString();
                        int tipoGestion = 4;



                        string rta = cg.InsertarPregestionAsesorCRM(nombre, apellido, documento, Convert.ToInt32(idTipoDocumento), celular, Convert.ToInt32(tipoGestion),
                                           Convert.ToInt32(Session["idCanalVenta"].ToString()), Convert.ToInt32(Session["idUsuario"].ToString()), 0, "Pendiente", ddlEmpresas.SelectedValue.ToString());

            if (rta == "OK")
                        {
                            string script = @"
                                Swal.fire({
                                    title: '¡Registro exitoso!',
                                    text: 'Registrado en la tabla PregestionCRM.',
                                    icon: 'success',
                                    timer: 3000, // 3 segundos
                                    showConfirmButton: false,
                                    timerProgressBar: true
                                }).then(() => {
                                    window.location.href = 'clientecorporativo';
                                });
                                ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                        }
                    //}
                //}
            //}
        }

        protected void gvProspectos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProspectos.PageIndex = e.NewPageIndex;

            //CargarCanalesVenta();
            if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
            {
                ListaClienteCorporativo();
            }
            else
            {
                ListaClienteCorporativo();
            }
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAgregarDatos_Click(object sender, EventArgs e)
        {

        }

        protected void rblPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pageSize = int.Parse(rblPageSize.SelectedValue);

            // Si es 0 (Todos), desactivamos paginación
            if (pageSize == 0)
            {
                gvProspectos.AllowPaging = false;
            }
            else
            {
                gvProspectos.AllowPaging = true;
                gvProspectos.PageSize = pageSize;
            }
            //CargarSedes();
            if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
            {
                //listaAfiliados("Todas");
            }   
            else
            {
                //listaAfiliados(Session["idSede"].ToString());
            }
        }

        protected void gvProspectos_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Alternar dirección
            if (SortExpression == e.SortExpression)
                SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
            else
            {
                SortExpression = e.SortExpression;
                SortDirection = "ASC";
            } 

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarProspectosCRM();
            DataView dv = dt.DefaultView;
            dv.Sort = $"{SortExpression} {SortDirection}";

            // Asignar al GridView
            gvProspectos.DataSource = dv;
            gvProspectos.DataBind();

            foreach (ListItem item in rblPageSize.Items)
            {
                item.Attributes["class"] = "btn btn-xs btn-white";
            }
            rblPageSize.RepeatLayout = RepeatLayout.Flow; // Para que se acomoden como botones
        }

        protected void gvProspectos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSeleccionarTodo");
                if (chk != null)
                {
                    chk.InputAttributes["onclick"] = "seleccionarTodos(this);";
                }

                foreach (DataControlField field in gvProspectos.Columns)
                {
                    if (!string.IsNullOrEmpty(field.SortExpression))
                    {
                        // Restaurar texto original (para que no se repita el icono ▼ ni ▲) 
                        switch (field.SortExpression)
                        {
                            case "idPregestion":
                                field.HeaderText = "ID";
                                break;
                            case "NombreContacto":
                                field.HeaderText = "Nombres";
                                break;
                            case "ApellidoContacto":
                                field.HeaderText = "Apellidos";
                                break;
                            case "DocumentoContacto":
                                field.HeaderText = "Documento";
                                break;
                            case "CelularContacto":
                                field.HeaderText = "Celular";
                                break;
                            case "hacecuanto":
                                field.HeaderText = "Días plan";
                                break;
                                //case "EstadoPlan":
                                //    field.HeaderText = "Estado";
                                //    break;
                        }

                        // Si esta es la columna ordenada, agregar el ícono
                        if (field.SortExpression == SortExpression)
                        {
                            string arrow = SortDirection == "ASC" ? " ▲" : " ▼";
                            field.HeaderText += arrow;
                        }
                    }
                }
            }
        }

        private void CargarCanalesAsesores()
        {
            int idPerfil = Convert.ToInt32(Session["idPerfil"].ToString());
            int idCanalVenta = Convert.ToInt32(Session["idCanalVenta"].ToString());

            //CargarCanalesVentaSedes();
            if (idPerfil == 1 || idPerfil == 18 || idPerfil == 21 || idPerfil == 37) // Usuario Directivo
            {
                CargarAsesoresPorSede(idCanalVenta);
               // listaAfiliados("Todas");
            }
            else
            {
                CargarAsesoresPorSede(idCanalVenta);
                if (idCanalVenta == 12 || idCanalVenta == 13 || idCanalVenta == 14)
                {
                   // listaAfiliados("Todas");
                }
               // listaAfiliados(idCanalVenta.ToString());
            }
        }

        private void CargarAsesoresPorSede(int idCanalVenta)
        {
            clasesglobales cg = new clasesglobales();

            ddlAsesores.Items.Clear();
            try
            {
                int idSede = Convert.ToInt32(Session["idSede"].ToString());
                DataTable dt = cg.ConsultaCargarAsesoresPorSede(idSede);

                if (idCanalVenta > 0)
                {
                    var filteredRows = dt.AsEnumerable()
                                         .Where(r => r.Field<int>("idCanalVenta") == idCanalVenta);

                    if (filteredRows.Any())
                    {
                        dt = filteredRows.CopyToDataTable();
                    }
                    else
                    {
                        dt = dt.Clone();
                    }
                }
                ddlAsesores.DataSource = dt;
                ddlAsesores.DataTextField = "NombreUsuario";
                ddlAsesores.DataValueField = "idUsuario";
                ddlAsesores.DataBind();

                dt.Dispose();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }
        }

        //private void CargarCanalesVentaSedes()
        //{
        //    ddlCanalVenta.Items.Clear();
        //    System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        //    ddlCanalVenta.Items.Add(li);

        //    try
        //    {
        //        clasesglobales cg = new clasesglobales();
        //        DataTable dt = new DataTable();

        //        if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
        //        {
        //            dt = cg.ConsultarCanalesVentaSedes();
        //        }
        //        else
        //        {
        //            dt = cg.ConsultarCanalesVentaSedesPorId(Convert.ToInt32(Session["idSede"].ToString()));
        //        }

        //        ddlCanalVenta.DataTextField = "NombreCanalVenta";
        //        ddlCanalVenta.DataValueField = "idCanalVenta";
        //        ddlCanalVenta.DataSource = dt;
        //        ddlCanalVenta.DataBind();

        //        dt.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = ex.Message.ToString();
        //    }

        //}

        protected void gvProspectos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes["onclick"] = "seleccionarCheckbox(this, event)";
           
                e.Row.Attributes["style"] = "cursor:pointer;";
            }

            Label lblEstado = (Label)e.Row.FindControl("lblEstado");
            if (lblEstado != null)
            {
                string estado = lblEstado.Text.Trim();

                // Aplica clases Bootstrap según el estado
                switch (estado.ToLower())
                {
                    case "activo":
                        lblEstado.CssClass = "badge badge-info"; // verde
                        break;
                    case "inactivo":
                        lblEstado.CssClass = "badge badge-danger"; // rojo
                        break;
                    default:
                        lblEstado.CssClass = "badge badge-warning"; // gris
                        break;
                }
            }
        }

        private string SortExpression
        {
            get { return ViewState["SortExpression"] as string ?? "diasquefaltan"; }
            set { ViewState["SortExpression"] = value; }
        }

        private string SortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        //protected void lnkAsignar_Click(object sender, EventArgs e)
        //{
        //    string mensaje = string.Empty;
        //    string asesor = ddlAsesores.SelectedItem.Value;
        //    bool haySeleccionados = false;
        //    int totalAgregados = 0;
        //    int totalErrores = 0;
        //    clasesglobales cg = new clasesglobales();
        //    try
        //    {
        //        DataTable dtCorporativo = new DataTable();
        //        dtCorporativo = cg.ConsultarClientecorporativo(.ToString());

        //        foreach (GridViewRow row in gvProspectos.Rows)
        //        {
        //            if (row.RowType == DataControlRowType.DataRow)
        //            {
        //                var chk = row.FindControl("chkSeleccionar") as System.Web.UI.WebControls.CheckBox;

        //                if (chk != null && chk.Checked)
        //                {
        //                    haySeleccionados = true;

        //                    string idPregestion = gvProspectos.DataKeys[row.RowIndex]["idPregestion"].ToString();    

        //                    string respuesta = cg.ActualizarAsesorPregestionCorporativo( Convert.ToInt32(idPregestion), Convert.ToInt32(asesor));

        //                    if (respuesta == "OK")
        //                        totalAgregados++;
        //                    else
        //                        totalErrores++;
        //                }
        //            }
        //        }

        //        if (!haySeleccionados)
        //        {
        //            string script = @"
        //                Swal.fire({
        //                    title: 'Selecciona un registro',
        //                    text: 'Debes elegir al menos uno para poder asignarlo a un asesor.',
        //                    icon: 'warning'
        //                });
        //            ";
        //            ScriptManager.RegisterStartupScript(this, GetType(), "SeleccioneUno", script, true);
        //            return;
        //        }

        //        string scriptOk = $@"
        //            Swal.fire({{
        //                title: '¡Registros asignados!',
        //                text: 'Se agregaron {totalAgregados} registros correctamente.',
        //                icon: 'success',
        //                timer: 3000,
        //                showConfirmButton: false,
        //                timerProgressBar: true
        //            }}).then(() => {{
        //                window.location.href = 'clientecorporativo';
        //            }});
        //        ";
        //        ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", scriptOk, true);
        //    }

        //    catch (Exception ex)
        //    {
        //        int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
        //        MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
        //    }
        //}

        protected void lnkAsignar_Click(object sender, EventArgs e)
        {
            string asesor = ddlAsesores.SelectedItem.Value;
            bool haySeleccionados = false;
            bool haySinAcuerdo = false;

            int totalAgregados = 0;
            int totalErrores = 0;

            clasesglobales cg = new clasesglobales();

            try
            {
                foreach (GridViewRow row in gvProspectos.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = row.FindControl("chkSeleccionar") as CheckBox;

                        if (chk != null && chk.Checked)
                        {
                            haySeleccionados = true;

                            // 🔑 Obtener valores desde DataKeys
                            string idPregestion = gvProspectos.DataKeys[row.RowIndex]["idPregestion"].ToString();
                            string estadoNegociacion = gvProspectos.DataKeys[row.RowIndex]["EstadoNegociacion"].ToString();

                            // 🔴 VALIDACIÓN DE ACUERDO
                            if (string.IsNullOrEmpty(estadoNegociacion) || estadoNegociacion != "ACUERDO")
                            {
                                haySinAcuerdo = true;
                                break;
                            }
                        }
                    }
                }

                // ⚠️ No seleccionó nada
                if (!haySeleccionados)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "SeleccioneUno", @"
                Swal.fire({
                    title: 'Selecciona un registro',
                    text: 'Debes elegir al menos uno para poder asignarlo.',
                    icon: 'warning'
                });
            ", true);
                    return;
                }

                // 🚫 Tiene registros sin acuerdo
                if (haySinAcuerdo)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "SinAcuerdo", @"
                Swal.fire({
                    title: 'Sin negociación',
                    text: 'Uno o más registros no tienen acuerdo. Debe realizar una negociación para poder asignar.',
                    icon: 'info'
                });
            ", true);
                    return;
                }

                // ✅ ASIGNACIÓN (solo si todos cumplen)
                foreach (GridViewRow row in gvProspectos.Rows)
                {
                    CheckBox chk = row.FindControl("chkSeleccionar") as CheckBox;

                    if (chk != null && chk.Checked)
                    {
                        string idPregestion = gvProspectos.DataKeys[row.RowIndex]["idPregestion"].ToString();

                        string respuesta = cg.ActualizarAsesorPregestionCorporativo(
                            Convert.ToInt32(idPregestion),
                            Convert.ToInt32(asesor)
                        );

                        if (respuesta == "OK")
                            totalAgregados++;
                        else
                            totalErrores++;
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", $@"
                    Swal.fire({{
                        title: '¡Registros asignados!',
                        text: 'Se agregaron {totalAgregados} registros correctamente.',
                        icon: 'success',
                        timer: 3000,
                        showConfirmButton: false
                    }}).then(() => {{
                        window.location.href = 'clientecorporativo';
                    }});
                ", true);
                    }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta(
                    "Error de proceso",
                    "Ocurrió un inconveniente. Código de error: " + idLog,
                    "error"
                );
            }
        }

    }
}