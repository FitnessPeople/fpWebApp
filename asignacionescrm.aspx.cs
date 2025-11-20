using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Wordprocessing;


namespace fpWebApp
{
    public partial class asignacionescrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    int idPerfil = Convert.ToInt32(Session["idPerfil"].ToString());
                    int idCanalVenta = Convert.ToInt32(Session["idCanalVenta"].ToString());
                    ValidarPermisos("Afiliados");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        
                        CargarCanalesVentaSedes();
                        if (idPerfil == 1 || idPerfil == 18 || idPerfil == 21 || idPerfil == 37) // Usuario Directivo
                        {
                            CargarAsesoresPorSede(idCanalVenta);
                            listaAfiliados("Todas");
                        }
                        else
                        {
                            CargarAsesoresPorSede(idCanalVenta);                           
                            if (idCanalVenta == 12 || idCanalVenta ==13 || idCanalVenta == 14)
                            {
                                listaAfiliados("Todas");
                            }
                            listaAfiliados(idCanalVenta.ToString());
                        }

                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            btnExportar.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            lnkAsignar.Visible = true;
                        }
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
        private void CargarCanalesVentaSedes()
        {
            ddlCanalVenta.Items.Clear();
            System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("Seleccione", "");
            ddlCanalVenta.Items.Add(li);

            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = new DataTable();

                if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
                {
                    dt = cg.ConsultarCanalesVentaSedes();
                }
                else
                {
                    dt = cg.ConsultarCanalesVentaSedesPorId(Convert.ToInt32(Session["idSede"].ToString()));
                }

                ddlCanalVenta.DataTextField = "NombreCanalVenta";
                ddlCanalVenta.DataValueField = "idCanalVenta";
                ddlCanalVenta.DataSource = dt;
                ddlCanalVenta.DataBind();

                dt.Dispose();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }

        }

        private void CargarAsesoresPorSede(int idCanalVenta)
        {
            clasesglobales cg = new clasesglobales();

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



        private void listaAfiliados(string strSede)
        {
            int dias = 0;
            try
            {
                if (ddlDias.SelectedItem != null && int.TryParse(ddlDias.SelectedItem.Value, out int parsed))
                    dias = parsed;

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarDiasFaltanPlanPregestionCRM(strSede, dias);

                lblTotalRegistros.Text = $"Registros totales: {dt.Rows.Count}";

                DataView dv = dt.DefaultView;
                dv.Sort = $"{SortExpression} {SortDirection}";

                gvAfiliados.DataSource = dv;
                gvAfiliados.DataBind();

                dt.Dispose();


                foreach (System.Web.UI.WebControls.ListItem item in rblPageSize.Items)
                {
                    item.Attributes["class"] = "btn btn-xs btn-white";
                }

                rblPageSize.RepeatLayout = RepeatLayout.Flow; // Para que se acomoden como botones

            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }

        }


        protected void gvAfiliados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAfiliados.PageIndex = e.NewPageIndex;

            CargarCanalesVentaSedes();
            if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
            {
                listaAfiliados("Todas");
            }
            else
            {
                listaAfiliados(Session["idCanalVenta"].ToString());
            }
        }

        protected void ddlDias_SelectedIndexChanged(object sender, EventArgs e)
        {
            listaAfiliados("Todas");
        }

        protected void gvAfiliados_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //CheckBox chk = (CheckBox)e.Row.FindControl("chkSeleccionarTodo");
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chkSeleccionarTodo");

                if (chk != null)
                {
                    chk.InputAttributes["onclick"] = "seleccionarTodos(this);";
                }

                foreach (DataControlField field in gvAfiliados.Columns)
                {
                    if (!string.IsNullOrEmpty(field.SortExpression))
                    {
                        // Restaurar texto original (para que no se repita el icono ▼ ni ▲) 
                        switch (field.SortExpression)
                        {
                            case "IdAfiliado":
                                field.HeaderText = "ID";
                                break;
                            case "NombreAfiliado":
                                field.HeaderText = "Nombres";
                                break;
                            case "ApellidoAfiliado":
                                field.HeaderText = "Apellidos";
                                break;
                            case "diasquefaltan":
                                field.HeaderText = "Días plan";
                                break;
                            case "NombreCanalVenta":
                                field.HeaderText = "Canal de venta";
                                break;
                            case "EstadoPlan":
                                field.HeaderText = "Estado";
                                break;
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


        protected void lnkAsignar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            string asesor = ddlAsesores.SelectedItem.Value;
            string tipoGestion = "1";
            bool haySeleccionados = false;
            int totalAgregados = 0;
            int totalErrores = 0;

            try
            {
                foreach (GridViewRow row in gvAfiliados.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        var chk = row.FindControl("chkSeleccionar") as System.Web.UI.WebControls.CheckBox;

                        if (chk != null && chk.Checked)
                        {
                            haySeleccionados = true;

                            string nombre = gvAfiliados.DataKeys[row.RowIndex]["NombreAfiliado"].ToString();
                            string apellido = gvAfiliados.DataKeys[row.RowIndex]["ApellidoAfiliado"].ToString();
                            string documento = gvAfiliados.DataKeys[row.RowIndex]["DocumentoAfiliado"].ToString();
                            string idTipoDocumento = gvAfiliados.DataKeys[row.RowIndex]["idTipoDocumento"].ToString();
                            string celular = gvAfiliados.DataKeys[row.RowIndex]["CelularAfiliado"].ToString();
                            int diasquefaltan = Convert.ToInt32(gvAfiliados.DataKeys[row.RowIndex]["diasquefaltan"].ToString());

                            if (diasquefaltan >= -30 && diasquefaltan < 30)
                                tipoGestion = "2";
                            else if (diasquefaltan < -30)
                                tipoGestion = "3";
                            else
                                tipoGestion = "1";

                            clasesglobales cg = new clasesglobales();
                            string respuesta = cg.InsertarPregestionAsesorCRM(nombre, apellido, documento, Convert.ToInt32(idTipoDocumento), celular, Convert.ToInt32(tipoGestion),
                                                Convert.ToInt32(Session["idCanalVenta"].ToString()), Convert.ToInt32(Session["idUsuario"].ToString()), Convert.ToInt32(asesor), "Pendiente");

                            if (respuesta == "OK")
                                totalAgregados++;
                            else
                                totalErrores++;
                        }
                    }
                }

                if (!haySeleccionados)
                {
                    string script = @"
                        Swal.fire({
                            title: 'Selecciona un registro',
                            text: 'Debes elegir al menos uno para poder asignarlo a un asesor.',
                            icon: 'warning'
                        });
                    ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "SeleccioneUno", script, true);
                    return;
                }

                string scriptOk = $@"
                    Swal.fire({{
                        title: '¡Registros asignados!',
                        text: 'Se agregaron {totalAgregados} registros correctamente.',
                        icon: 'success',
                        timer: 3000,
                        showConfirmButton: false,
                        timerProgressBar: true
                    }}).then(() => {{
                        window.location.href = 'asignacionescrm';
                    }});
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", scriptOk, true);
            }

            catch (Exception ex)
            {
                string script = @"
                    Swal.fire({
                        title: 'Error inesperado',
                        text: '" + ex.Message.Replace("'", "\\'") + @"',
                        icon: 'error'
                    });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
            }
        }


        protected void rblPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pageSize = int.Parse(rblPageSize.SelectedValue);

            // Si es 0 (Todos), desactivamos paginación
            if (pageSize == 0)
            {
                gvAfiliados.AllowPaging = false;
            }
            else
            {
                gvAfiliados.AllowPaging = true;
                gvAfiliados.PageSize = pageSize;
            }
            CargarCanalesVentaSedes();
            if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
            {
                listaAfiliados("Todas");
            }
            else
            {
                listaAfiliados(Session["idCanalVenta"].ToString());
            }
        }

        protected void gvAfiliados_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Alternar dirección
            if (SortExpression == e.SortExpression)
                SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
            else
            {
                SortExpression = e.SortExpression;
                SortDirection = "ASC";
            }
            string strSede = "";

            if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
            {
                strSede = "Todas";
            }
            else
            {
                strSede = Session["idSede"].ToString();
            }

            int dias = 0;
            if (ddlDias.SelectedItem != null && int.TryParse(ddlDias.SelectedItem.Value, out int parsed))
                dias = parsed;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarDiasFaltanPlanPregestionCRM(strSede, dias);

            DataView dv = dt.DefaultView;
            dv.Sort = $"{SortExpression} {SortDirection}";

            // Asignar al GridView
            gvAfiliados.DataSource = dv;
            gvAfiliados.DataBind();

            foreach (System.Web.UI.WebControls.ListItem item in rblPageSize.Items)
            {
                item.Attributes["class"] = "btn btn-xs btn-white";
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

        protected void gvAfiliados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Attributes["onclick"] = "seleccionarCheckbox(this, event)";
            //    e.Row.Attributes["style"] = "cursor:pointer;";
            //}

            Label lblEstado = (Label)e.Row.FindControl("lblEstado");
            if (lblEstado != null)
            {
                string estado = lblEstado.Text.Trim();

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
        protected void ddlCanalVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            listaAfiliados(ddlCanalVenta.SelectedItem.Value.ToString());
        }
    }
}