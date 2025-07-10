using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class paginas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Páginas");
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
                        divBotonesLista.Visible = false;
                        btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = false;
                            CargarCategorias();
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                            CargarCategorias();
                        }
                    }

                    ListaPaginas();
                    ltTitulo.Text = "Agregar página";

                    if (Request.QueryString.Count > 0)
                    {
                        rpPaginas.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarPaginaPorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                txbPagina.Text = dt.Rows[0]["Pagina"].ToString();
                                txbAspx.Text = dt.Rows[0]["NombreAspx"].ToString();
                                ddlCategorias.SelectedIndex = Convert.ToInt16(ddlCategorias.Items.IndexOf(ddlCategorias.Items.FindByValue(dt.Rows[0]["idCategoria"].ToString())));

                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar Página";
                            }
                            dt.Dispose();
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            //Las paginas no se pueden borrar
                            ltMensaje.Text = "<div class=\"ibox-content\">" +
                                "<div class=\"alert alert-danger alert-dismissable\">" +
                                "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                "Esta página no se puede borrar." +
                                "</div></div>";

                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarPaginaPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                txbPagina.Text = dt.Rows[0]["Pagina"].ToString();
                                txbPagina.Enabled = false;
                                ddlCategorias.SelectedIndex = Convert.ToInt16(ddlCategorias.Items.IndexOf(ddlCategorias.Items.FindByValue(dt.Rows[0]["idCategoria"].ToString())));
                                ddlCategorias.Enabled = false;
                                btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                btnAgregar.Enabled = false;
                                ltTitulo.Text = "Borrar Página";
                            }
                            dt.Dispose();
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

        private void ListaPaginas()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPaginas();
            rpPaginas.DataSource = dt;
            rpPaginas.DataBind();
            dt.Dispose();
        }

        protected void rpPaginas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "paginas?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "paginas?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }

        private bool ValidarPagina(string strNombre)
        {
            bool bExiste = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPaginaPorNombre(strNombre);
            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }
            dt.Dispose();
            return bExiste;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            if (Request.QueryString.Count > 0)
            {
                string strInitData = TraerData();

                if (Request.QueryString["editid"] != null)
                {
                    string respuesta = cg.ActualizarPagina(int.Parse(Request.QueryString["editid"].ToString()), txbPagina.Text.ToString().Trim(), txbAspx.Text.ToString().Trim(), Convert.ToInt32(ddlCategorias.SelectedItem.Value));

                    string strNewData = TraerData();
                    cg.InsertarLog(Session["idusuario"].ToString(), "paginas", "Modifica", "El usuario modificó la página: " + txbPagina.Text.ToString() + ".", strInitData, strNewData);
                }

                if (Request.QueryString["deleteid"] != null)
                {
                    // Borrar
                }
                Response.Redirect("paginas");
            }
            else
            {
                if (!ValidarPagina(txbPagina.Text.ToString()))
                {
                    try
                    {
                        string respuesta = cg.InsertarPagina(txbPagina.Text.ToString().Trim(), txbAspx.Text.ToString().Trim(), Convert.ToInt32(ddlCategorias.SelectedItem.Value.ToString()));

                        cg.InsertarLog(Session["idusuario"].ToString(), "paginas", "Agrega", "El usuario agregó una nueva página: " + txbPagina.Text.ToString() + ".", "", "");

                        DataTable dt = cg.ConsultarUltimaPagina();
                        int IdPagina = int.Parse(dt.Rows[0]["idPagina"].ToString());
                        dt.Dispose();

                        DataTable dt1 = cg.ConsultarPerfiles();

                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            try
                            {
                                string respuesta2 = cg.InsertarPermisoPerfil(int.Parse(dt1.Rows[i]["idPerfil"].ToString()), IdPagina, 1, 0, 0, 0, 0);
                            }
                            catch (Exception ex)
                            {
                                string mensaje = ex.Message;
                            }
                        }
                        dt1.Dispose();
                    }
                    catch (Exception ex)
                    {
                        string mensajeExcepcionInterna = string.Empty;
                        Console.WriteLine(ex.Message);
                        if (ex.InnerException != null)
                        {
                            mensajeExcepcionInterna = ex.InnerException.Message;
                            Console.WriteLine("Mensaje de la excepción interna: " + mensajeExcepcionInterna);
                        }
                        ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Excepción interna." +
                        "</div>";
                    }
                    Response.Redirect("paginas");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Ya existe una Página con ese nombre." +
                    "</div>";
                }
            }
        }

        private void CargarCategorias()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCategoriasPaginas();

            ddlCategorias.Items.Clear();

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlCategorias.DataSource = dt;
                ddlCategorias.DataValueField = "idCategoria";
                ddlCategorias.DataTextField = "Nombre";
                ddlCategorias.DataBind();
            }

            dt.Dispose();
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarPaginas();
                string nombreArchivo = $"Paginas_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    cg.ExportarExcel(dt, nombreArchivo);
                }
                else
                {
                    Response.Write("<script>alert('No existen registros para esta consulta');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
            }
        }

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPaginaPorId(int.Parse(Request.QueryString["editid"].ToString()));

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();

            return strData;
        }
    }
}