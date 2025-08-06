using NPOI.OpenXmlFormats.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
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
                        string strParam = "";
                        if (Session["idSede"].ToString() == "11")
                        {
                            CargarSedes(11, "Todas");
                        }
                        else
                        {
                            CargarSedes(Convert.ToInt32(Session["idSede"].ToString()), "Gimnasio");
                        }
                        listaAfiliados(strParam, ddlSedes.SelectedItem.Value.ToString());

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

        private void CargarSedes(int idSede, string clase)
        {
            ddlSedes.Items.Clear();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSedesPorId(Convert.ToInt32(idSede), clase);

            if (clase == "Todas")
            {
                ListItem li = new ListItem("Todas", "Todas");
                ddlSedes.Items.Add(li);
            }

            ddlSedes.DataSource = dt;
            ddlSedes.DataBind();

            dt.Dispose();
        }

        private void listaAfiliados(string strParam, string strSede)
        {
            string strQueryAdd = "";
            string strQueryAdd2 = "";
            string strLimit = "5000";
            if (strSede != "Todas")
            {
                strQueryAdd = "AND a.idSede = " + strSede;
            }
            if (strParam != "")
            {
                strLimit = "5000";
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

            string strQuery = "SELECT *, " +
                "IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) IS NOT NULL, CONCAT('(',TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()),')'),'<i class=\"fa fa-circle-question m-r-lg m-l-lg\"></i>') AS edad, " +
                "IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 14,'danger',IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 14,'success',IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 60,'info','warning'))) AS badge, " +
                "IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 14,'baby',IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) >= 60,'person-walking-with-cane','')) AS age, " +
                "IF(EstadoAfiliado='Activo','info',IF(EstadoAfiliado='Inactivo','danger','warning')) AS badge2, " +
                "IF(EstadoPlan='Activo','info',IF(EstadoAfiliado='Inactivo','danger','warning')) AS badge3, " +
                "DATEDIFF(FechaFinalPlan, CURDATE()) AS diasquefaltan, " +
                "IF(DATEDIFF(FechaFinalPlan, CURDATE()) < 30 AND DATEDIFF(FechaFinalPlan, CURDATE()) > -30,'1',IF(DATEDIFF(FechaFinalPlan, CURDATE()) < -30,'2','')) AS TipoGestion " +
                "FROM Afiliados a " +
                "LEFT JOIN generos g ON g.idGenero = a.idGenero " +
                "LEFT JOIN sedes s ON s.idSede = a.idSede " +
                "LEFT JOIN ciudadessedes cs ON s.idCiudadSede = cs.idCiudadSede " +
                "LEFT JOIN estadocivil ec ON ec.idEstadoCivil = a.idEstadoCivilAfiliado " +
                "LEFT JOIN AfiliadosPlanes ap ON ap.idAfiliado = a.idAfiliado " +
                "LEFT JOIN profesiones p ON p.idProfesion = a.idProfesion " +
                "LEFT JOIN eps ON eps.idEps = a.idEps " +
                "LEFT JOIN ciudades ON ciudades.idCiudad = a.idCiudadAfiliado " +
                "WHERE (DocumentoAfiliado like '%" + strParam + "%' " +
                "OR NombreAfiliado like '%" + strParam + "%' " +
                "OR EmailAfiliado like '%" + strParam + "%' " +
                "OR CelularAfiliado like '%" + strParam + "%') " + strQueryAdd + " " + strQueryAdd2 + " " +
                "ORDER BY a.idAfiliado DESC " +
                "LIMIT " + strLimit + "";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            lblTotalRegistros.Text = $"Total de registros: {dt.Rows.Count}";

            DataView dv = dt.DefaultView;
            dv.Sort = $"{SortExpression} {SortDirection}";

            gvAfiliados.DataSource = dv;
            gvAfiliados.DataBind();

            //rpAfiliados.DataSource = dt;
            //rpAfiliados.DataBind();

            dt.Dispose();
        }

        protected void gvAfiliados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAfiliados.PageIndex = e.NewPageIndex;
            string strParam = "";
            if (Session["idSede"].ToString() == "11")
            {
                CargarSedes(11, "Todas");
            }
            else
            {
                CargarSedes(Convert.ToInt32(Session["idSede"].ToString()), "Gimnasio");
            }
            listaAfiliados(strParam, ddlSedes.SelectedItem.Value.ToString());
        }

        protected void AsignarAfiliados()
        {
            string strQuery = "";
            clasesglobales cg = new clasesglobales();
            foreach (GridViewRow row in gvAfiliados.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                if (chk != null && chk.Checked)
                {
                    string id = gvAfiliados.DataKeys[row.RowIndex].Value.ToString();
                    // Aquí puedes procesar ese ID seleccionado
                    strQuery = "INSERT INTO pregestioncrm (NombreContacto) VALUES ('CARLOS')";
                    cg.TraerDatosStr(strQuery);
                }
            }
        }

        protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strParam = txbBuscar.Value.ToString();
            listaAfiliados(strParam, ddlSedes.SelectedItem.Value.ToString());
        }

        protected void ddlDias_SelectedIndexChanged(object sender, EventArgs e)
        {
            listaAfiliados("", "Todas");
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
            AsignarAfiliados();
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
            string strParam = "";
            if (Session["idSede"].ToString() == "11")
            {
                CargarSedes(11, "Todas");
            }
            else
            {
                CargarSedes(Convert.ToInt32(Session["idSede"].ToString()), "Gimnasio");
            }
            listaAfiliados(strParam, ddlSedes.SelectedItem.Value.ToString());
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
            string strParam = "";
            string strQueryAdd = "";
            string strQueryAdd2 = "";
            string strLimit = "5000";
            if (ddlSedes.SelectedItem.Value.ToString() != "Todas")
            {
                strQueryAdd = "AND a.idSede = " + ddlSedes.SelectedItem.Value.ToString();
            }
            if (strParam != "")
            {
                strLimit = "5000";
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

            string strQuery = "SELECT *, " +
                "IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) IS NOT NULL, CONCAT('(',TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()),')'),'<i class=\"fa fa-circle-question m-r-lg m-l-lg\"></i>') AS edad, " +
                "IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 14,'danger',IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 14,'success',IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 60,'info','warning'))) AS badge, " +
                "IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 14,'baby',IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) >= 60,'person-walking-with-cane','')) AS age, " +
                "IF(EstadoAfiliado='Activo','info',IF(EstadoAfiliado='Inactivo','danger','warning')) AS badge2, " +
                "IF(EstadoPlan='Activo','info',IF(EstadoAfiliado='Inactivo','danger','warning')) AS badge3, " +
                "DATEDIFF(FechaFinalPlan, CURDATE()) AS diasquefaltan, " +
                "IF(DATEDIFF(FechaFinalPlan, CURDATE()) < 30 AND DATEDIFF(FechaFinalPlan, CURDATE()) > -30,'1',IF(DATEDIFF(FechaFinalPlan, CURDATE()) < -30,'2','')) AS TipoGestion " +
                "FROM Afiliados a " +
                "LEFT JOIN generos g ON g.idGenero = a.idGenero " +
                "LEFT JOIN sedes s ON s.idSede = a.idSede " +
                "LEFT JOIN ciudadessedes cs ON s.idCiudadSede = cs.idCiudadSede " +
                "LEFT JOIN estadocivil ec ON ec.idEstadoCivil = a.idEstadoCivilAfiliado " +
                "LEFT JOIN AfiliadosPlanes ap ON ap.idAfiliado = a.idAfiliado " +
                "LEFT JOIN profesiones p ON p.idProfesion = a.idProfesion " +
                "LEFT JOIN eps ON eps.idEps = a.idEps " +
                "LEFT JOIN ciudades ON ciudades.idCiudad = a.idCiudadAfiliado " +
                "WHERE (DocumentoAfiliado like '%" + strParam + "%' " +
                "OR NombreAfiliado like '%" + strParam + "%' " +
                "OR EmailAfiliado like '%" + strParam + "%' " +
                "OR CelularAfiliado like '%" + strParam + "%') " + strQueryAdd + " " + strQueryAdd2 + " " +
                "ORDER BY a.idAfiliado DESC " +
                "LIMIT " + strLimit + "";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
            DataView dv = dt.DefaultView;
            dv.Sort = $"{SortExpression} {SortDirection}";

            // Asignar al GridView
            gvAfiliados.DataSource = dv;
            gvAfiliados.DataBind();
        }

        private string SortExpression
        {
            get { return ViewState["SortExpression"] as string ?? "IdAfiliado"; }
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
        }
    }
}