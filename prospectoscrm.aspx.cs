using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class prospectoscrm : System.Web.UI.Page
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

                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {

                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                    ListaProspectos();
                    CargarTipoDocumento();

                    ltTitulo.Text = "Agregar prospecto";

                    if (Request.QueryString.Count > 0)
                    {
                        //rpProspectos.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarEpsPorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                txbNombreContacto.Text = dt.Rows[0]["NombreEps"].ToString();
                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar EPS";
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ValidarEpsTablas(int.Parse(Request.QueryString["deleteid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                ltMensaje.Text = "<div class=\"ibox-content\">" +
                                    "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                    "Esta EPS no se puede borrar, hay empleados asociados a ella." +
                                    "</div></div>";

                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarEpsPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    txbNombreContacto.Text = dt1.Rows[0]["NombreEps"].ToString();
                                    txbNombreContacto.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    btnAgregar.Enabled = false;
                                    ltTitulo.Text = "Borrar EPS";
                                }
                                dt1.Dispose();
                            }
                            else
                            {
                                //Borrar
                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarEpsPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    txbNombreContacto.Text = dt1.Rows[0]["NombreEps"].ToString();
                                    txbNombreContacto.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    ltTitulo.Text = "Borrar EPS";
                                }
                                dt1.Dispose();
                            }
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

        private void ListaProspectos()
        {
            clasesglobales cg = new clasesglobales();
            //DataTable dt = cg.ConsultarEpss();

            string strQuery = "SELECT *, DATEDIFF(FechaHoraPregestion, CURDATE()) AS hacecuanto " +
                "FROM pregestioncrm pg, tiposgestioncrm tg " +
                "WHERE pg.idTipoGestion = 4 " +
                "AND pg.idTipoGestion = tg.idTipoGestionCRM ";
            DataTable dt = cg.TraerDatos(strQuery);

            gvProspectos.DataSource = dt;
            gvProspectos.DataBind();
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {


            string strQuery = @"INSERT INTO pregestioncrm 
                (FechaHoraPregestion, NombreContacto, ApellidoContacto, DocumentoContacto, 
                idTipoDocumentoContacto, CelularContacto, idTipoGestion, idCanalVenta, idUsuarioAsigna) 
                VALUES (NOW(), @Nombre, @Apellido, @Documento, 
                @TipoDoc, @Celular, @TipoGestion, @IdCanalVenta, @IdUsuarioAsigna)";

            string connString = ConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
            string tipoGestion = "4";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string nombre = txbNombreContacto.Text.ToString();
                string apellido = txbApellidoContacto.Text.ToString();
                string documento = txbDocumento.Text.ToString();
                string idTipoDocumento = ddlTipoDocumento.SelectedItem.Value.ToString();
                string celular = txbCelular.Text.ToString();

                using (MySqlCommand cmd = new MySqlCommand(strQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellido", apellido);
                    cmd.Parameters.AddWithValue("@Documento", documento);
                    cmd.Parameters.AddWithValue("@TipoDoc", idTipoDocumento);
                    cmd.Parameters.AddWithValue("@Celular", celular);
                    cmd.Parameters.AddWithValue("@TipoGestion", tipoGestion);
                    cmd.Parameters.AddWithValue("@IdCanalVenta", Session["idCanalVenta"].ToString());
                    cmd.Parameters.AddWithValue("@IdUsuarioAsigna", Session["idUsuario"].ToString());

                    cmd.ExecuteNonQuery();

                    Response.Redirect("prospectoscrm");
                }
            }
        }

        protected void gvProspectos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProspectos.PageIndex = e.NewPageIndex;

            //CargarCanalesVenta();
            if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
            {
                //listaAfiliados("Todas");
            }
            else
            {
                //listaAfiliados(Session["idSede"].ToString());
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

        protected void gvProspectos_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvProspectos.PageIndex = e.NewPageIndex;

            //CargarCanalesVenta();
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

            // Obtener y ordenar datos
            string strQueryAdd = "";
            string strQueryAdd2 = "";
            string strLimit = "5000";
            string strSede = "";

            if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
            {
                strSede = "Todas";
            }
            else
            {
                strSede = Session["idSede"].ToString();
            }

            if (strSede != "Todas")
            {
                strQueryAdd = "AND a.idSede = " + strSede;
            }

            //if (ddlDias.SelectedItem.Value.ToString() == "-30")
            //{
            //    strQueryAdd2 = "AND DATEDIFF(FechaFinalPlan, CURDATE()) <= -30 ";
            //}

            //if (ddlDias.SelectedItem.Value.ToString() == "30")
            //{
            //    strQueryAdd2 = "AND DATEDIFF(FechaFinalPlan, CURDATE()) > -30 AND DATEDIFF(FechaFinalPlan, CURDATE()) < 30 ";
            //}

            //if (ddlDias.SelectedItem.Value.ToString() == "31")
            //{
            //    strQueryAdd2 = "AND DATEDIFF(FechaFinalPlan, CURDATE()) > 31 ";
            //}

            string strQuery = "SELECT *, DATEDIFF(FechaFinalPlan, CURDATE()) AS diasquefaltan " +
                "FROM Afiliados a " +
                "LEFT JOIN sedes s ON s.idSede = a.idSede " +
                "LEFT JOIN AfiliadosPlanes ap ON ap.idAfiliado = a.idAfiliado " +
                "WHERE 1=1 " + strQueryAdd + " " + strQueryAdd2 + " " +
                "AND a.DocumentoAfiliado NOT IN (SELECT documentoContacto FROM pregestioncrm) " +
                "ORDER BY DATEDIFF(FechaFinalPlan, CURDATE()) DESC " +
                "LIMIT " + strLimit + "";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
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
                        // Restaurar texto original (puedes usar un diccionario si son dinámicos)
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

        protected void gvProspectos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Añade un atributo onclick a cada fila
                e.Row.Attributes["onclick"] = "seleccionarCheckbox(this, event)";
                // Opcional: cambia el cursor al pasar
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