using MySql.Data.MySqlClient;
using NPOI.OpenXmlFormats.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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
                    ValidarPermisos("Afiliados");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        CargarAsesores();

                        CargarCanalesVenta();
                        if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
                        {
                            listaAfiliados("Todas");
                        }
                        else
                        {
                            listaAfiliados(Session["idSede"].ToString());
                        }

                        //listaAfiliados(ddlSedes.SelectedItem.Value.ToString());

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


        private void CargarCanalesVenta()
        {
            ddlCanalVenta.Items.Clear();
            clasesglobales cg = new clasesglobales();
            DataTable dt = new DataTable();

            if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
            {
                dt = cg.ConsultarCanalesVenta();
            }
            else
            {
                dt = cg.ConsultarCanalesVentaPorId(Convert.ToInt32(Session["idCanalVenta"].ToString()));
            }

            ddlCanalVenta.DataSource = dt;
            ddlCanalVenta.DataBind();

            dt.Dispose();
        }

        private void CargarAsesores()
        {
            string strQuery = "SELECT * " +
                "FROM Usuarios u, Empleados e " +
                "WHERE u.idEmpleado = e.DocumentoEmpleado " +
                "AND u.EstadoUsuario = 'Activo' " +
                "AND e.idSede = " + Session["idSede"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlAsesores.DataSource = dt;
            ddlAsesores.DataBind();

            dt.Dispose();
        }

        private void listaAfiliados(string strSede)
        {
            string strQueryAdd = "";
            string strQueryAdd2 = "";
            string strLimit = "5000";

            if (strSede != "Todas")
            {
                strQueryAdd = "AND a.idSede = " + strSede;
            }

            if (ddlDias.SelectedItem.Value.ToString() == "-30")
            {
                strQueryAdd2 = "AND DATEDIFF(FechaFinalPlan, CURDATE()) <= -30 ";
            }

            if (ddlDias.SelectedItem.Value.ToString() == "30")
            {
                strQueryAdd2 = "AND DATEDIFF(FechaFinalPlan, CURDATE()) > -30 AND DATEDIFF(FechaFinalPlan, CURDATE()) < 30 ";
            }

            if (ddlDias.SelectedItem.Value.ToString() == "31")
            {
                strQueryAdd2 = "AND DATEDIFF(FechaFinalPlan, CURDATE()) > 31 ";
            }

            string strQuery = "SELECT *, DATEDIFF(FechaFinalPlan, CURDATE()) AS diasquefaltan " +
                "FROM Afiliados a " +
                "LEFT JOIN sedes s ON s.idSede = a.idSede " +
                "LEFT JOIN AfiliadosPlanes ap ON ap.idAfiliado = a.idAfiliado " +
                "WHERE 1=1 " + strQueryAdd + " " + strQueryAdd2 + " " +
                "AND a.DocumentoAfiliado NOT IN (SELECT documentoContacto FROM pregestioncrm) " +
                "LIMIT " + strLimit + "";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            lblTotalRegistros.Text = $"Registros totales: {dt.Rows.Count}";

            DataView dv = dt.DefaultView;
            dv.Sort = $"{SortExpression} {SortDirection}";

            gvAfiliados.DataSource = dv;
            gvAfiliados.DataBind();

            dt.Dispose();

            foreach (ListItem item in rblPageSize.Items)
            {
                item.Attributes["class"] = "btn btn-xs btn-white";
            }
            rblPageSize.RepeatLayout = RepeatLayout.Flow; // Para que se acomoden como botones
        }

        protected void gvAfiliados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAfiliados.PageIndex = e.NewPageIndex;

            CargarCanalesVenta();
            if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
            {
                listaAfiliados("Todas");
            }
            else
            {
                listaAfiliados(Session["idSede"].ToString());
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
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSeleccionarTodo");
                if (chk != null)
                {
                    chk.InputAttributes["onclick"] = "seleccionarTodos(this);";
                }

                foreach (DataControlField field in gvAfiliados.Columns)
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

        protected void lnkAsignar_Click(object sender, EventArgs e)
        {
            string strQuery = @"INSERT INTO pregestioncrm 
                (FechaHoraPregestion, NombreContacto, ApellidoContacto, DocumentoContacto, 
                idTipoDocumentoContacto, CelularContacto, idTipoGestion, idCanalVenta, idUsuarioAsigna, idAsesor) 
                VALUES (NOW(), @Nombre, @Apellido, @Documento, 
                @TipoDoc, @Celular, @TipoGestion, @IdCanalVenta, @IdUsuarioAsigna, @idAsesor)";

            string asesor = ddlAsesores.SelectedItem.Value;
            string connString = ConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
            string tipoGestion = "1";
            bool haySeleccionados = false;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                foreach (GridViewRow row in gvAfiliados.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                    if (chk != null && chk.Checked)
                    {
                        haySeleccionados = true;

                        string id = gvAfiliados.DataKeys[row.RowIndex]["IdAfiliado"].ToString();
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
                            tipoGestion = "1"; // valor por defecto

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
                            cmd.Parameters.AddWithValue("@idAsesor", asesor);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            // Si no hubo seleccionados, mostrar alerta
            if (!haySeleccionados)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Debe seleccionar al menos un registro.');", true);
                string script = @"
                    Swal.fire({
                        title: 'Seleccione al menos 1 registro',
                        text: 'Debes seleccionar al menos un registro para asignarlo a un asesor.',
                        icon: 'warning'
                    });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "SeleccioneUno", script, true);
                return;
            }
            else
            {
                Response.Redirect("asignacionescrm");
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

            // Recargar datos
            CargarCanalesVenta();
            if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
            {
                listaAfiliados("Todas");
            }
            else
            {
                listaAfiliados(Session["idSede"].ToString());
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

            if (ddlDias.SelectedItem.Value.ToString() == "-30")
            {
                strQueryAdd2 = "AND DATEDIFF(FechaFinalPlan, CURDATE()) <= -30 ";
            }

            if (ddlDias.SelectedItem.Value.ToString() == "30")
            {
                strQueryAdd2 = "AND DATEDIFF(FechaFinalPlan, CURDATE()) > -30 AND DATEDIFF(FechaFinalPlan, CURDATE()) < 30 ";
            }

            if (ddlDias.SelectedItem.Value.ToString() == "31")
            {
                strQueryAdd2 = "AND DATEDIFF(FechaFinalPlan, CURDATE()) > 31 ";
            }

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
            gvAfiliados.DataSource = dv;
            gvAfiliados.DataBind();

            foreach (ListItem item in rblPageSize.Items)
            {
                item.Attributes["class"] = "btn btn-xs btn-white";
            }
            rblPageSize.RepeatLayout = RepeatLayout.Flow; // Para que se acomoden como botones
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

        protected void ddlCanalVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
            {
                listaAfiliados("Todas");
            }
            else
            {
                listaAfiliados(Session["idSede"].ToString());
            }
        }
    }
}