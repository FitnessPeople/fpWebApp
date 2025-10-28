using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Web.Services.Description;
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
                    ValidarPermisos("Prospectos");
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
                            ListaProspectos();
                            listaEmpresasAfiliadas();
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {

                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            ListaProspectos();
                            btnAgregar.Visible = true;
                          
                        }
                        if (ViewState["Borrar"].ToString() == "1")
                        {
                            lnkAsignar.Visible = true;
                        }
                    }

                    CargarTipoDocumento();

                    ltTitulo.Text = "Agregar prospecto";

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

        private void ListaProspectos()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarProspectosCRM();
            gvProspectos.DataSource = dt;
            gvProspectos.DataBind();
            dt.Dispose();
        }


        private void listaEmpresasAfiliadas()
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarEmpresasYProspectosCorporativos();

                ddlEmpresas.DataSource = dt;
                ddlEmpresas.DataBind();

                dt.Dispose();
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Ocurrió un error al cargar las empresas. Por favor intente nuevamente.";
                lblMensaje.CssClass = "text-danger";
            }
        }
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
            // Consultar si el prospecto existe en la tabla Afiliados.
            clasesglobales cg = new clasesglobales();
            DataTable dt1 = cg.ConsultarAfiliadoPorDocumento(Convert.ToInt32(txbDocumento.Text.ToString()));

            if (dt1.Rows.Count > 0)
            {
                string script = @"
                    Swal.fire({
                        title: 'Mensaje',
                        text: 'Ya existe un afiliado registrado con este documento.',
                        icon: 'error'
                    }).then((result) => {
                        if (result.isConfirmed) {
                                            
                        }
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
            }
            else
            {
                // Consultar si el prospecto existe en la tabla ContactosCRM.
                DataTable dt2 = cg.ConsultarContactosCRMPorDocumento(Convert.ToInt32(txbDocumento.Text.ToString()));

                if (dt2.Rows.Count > 0)
                {
                    string script = @"
                        Swal.fire({
                            title: 'Mensaje',
                            text: 'Ya existe un contacto en el CRM registrado con este documento.',
                            icon: 'error'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                            
                            }
                        });
                        ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                }
                else
                {
                    // Consultar si el prospecto existe en la tabla PregestionCRM.
                    DataTable dt3 = cg.ConsultarPregestionCRMPorDocumento(Convert.ToInt32(txbDocumento.Text.ToString()));

                    if (dt3.Rows.Count > 0)
                    {
                        string script = @"
                        Swal.fire({
                            title: 'Mensaje',
                            text: 'Ya existe este documento en PregestionCRM.',
                            icon: 'error'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                            
                            }
                        });
                        ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                    }
                    else
                    {
                        string nombre = txbNombreContacto.Text.ToString();
                        string apellido = txbApellidoContacto.Text.ToString();
                        string documento = txbDocumento.Text.ToString();
                        int idTipoDocumento = Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value.ToString());
                        string celular = txbCelular.Text.ToString();
                        int tipoGestion = 4;

                        string rta = cg.InsertarPregestionCRM(nombre, apellido,
                                        documento, idTipoDocumento, celular, tipoGestion,
                                        Convert.ToInt32(Session["idCanalVenta"].ToString()),
                                        Convert.ToInt32(Session["idUsuario"].ToString()));

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
                                    window.location.href = 'prospectoscrm';
                                });
                                ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                        }
                    }
                }
            }
        }

        protected void gvProspectos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProspectos.PageIndex = e.NewPageIndex;

            //CargarCanalesVenta();
            if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
            {
                ListaProspectos();
            }
            else
            {
                ListaProspectos();
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
    }
}