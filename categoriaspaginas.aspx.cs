using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class categoriaspaginas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Categorías páginas");
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

                    //ListaPaginas();
                    ltTitulo.Text = "Agregar categoría";

                    if (Request.QueryString.Count > 0)
                    {
                        rpCategorias.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarCategoriasPaginasPorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                txbCategoria.Text = dt.Rows[0]["NombreCategoriaPagina"].ToString();
                                txbIconoFA.Text = dt.Rows[0]["IconoFA"].ToString();
                                txbIdentificador.Text = dt.Rows[0]["Identificador"].ToString();
                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar Categoría página";
                            }
                            dt.Dispose();
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            //Las categorías no se pueden borrar
                            ltMensaje.Text = "<div class=\"ibox-content\">" +
                                "<div class=\"alert alert-danger alert-dismissable\">" +
                                "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                "Esta categoría no se puede borrar." +
                                "</div></div>";

                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarPaginaPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                txbCategoria.Text = dt.Rows[0]["NombreCategoriaPagina"].ToString();
                                txbCategoria.Enabled = false;
                                txbIdentificador.Text = dt.Rows[0]["Identificador"].ToString();
                                txbIdentificador.Enabled = false;
                                txbIconoFA.Text = dt.Rows[0]["IconoFA"].ToString();
                                txbIconoFA.Enabled = false;
                                btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                btnAgregar.Enabled = false;
                                ltTitulo.Text = "Borrar Categoría";
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

        private void ValidarPermisos(string strCategoria)
        {
            ViewState["SinPermiso"] = "1";
            ViewState["Consulta"] = "0";
            ViewState["Exportar"] = "0";
            ViewState["CrearModificar"] = "0";
            ViewState["Borrar"] = "0";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ValidarPermisos(strCategoria, Session["idPerfil"].ToString(), Session["idusuario"].ToString());

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


        private bool ValidarCategoria(string strNombre)
        {
            bool bExiste = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCategoriaPaginaPorNombre(strNombre);
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
                    string respuesta = cg.ActualizarCategoriaPagina(int.Parse(Request.QueryString["editid"].ToString()), txbCategoria.Text.ToString().Trim(), txbIconoFA.Text.ToString(), txbIdentificador.Text.ToString());

                    string strNewData = TraerData();
                    cg.InsertarLog(Session["idusuario"].ToString(), "Categorias paginas", "Modifica", "El usuario modificó la página: " + txbCategoria.Text.ToString() + ".", strInitData, strNewData);
                }

                if (Request.QueryString["deleteid"] != null)
                {
                    // Borrar
                }
                Response.Redirect("categoriaspaginas");
            }
            else
            {
                if (!ValidarCategoria(txbCategoria.Text.ToString()))
                {
                    try
                    {
                        string respuesta = cg.InsertarCategoriaPagina(txbCategoria.Text.ToString().Trim(), txbIconoFA.Text.ToString(), txbIdentificador.Text.ToString());

                        cg.InsertarLog(Session["idusuario"].ToString(), "categorias paginas", "Agrega", "El usuario agregó una nueva categoría página: " + txbCategoria.Text.ToString() + ".", "", "");

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
                    Response.Redirect("categoriaspaginas");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Ya existe una Categoría de página con ese nombre." +
                    "</div>";
                }
            }
        }

        private void CargarCategorias()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCategoriasPaginas();
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    rpCategorias.DataSource = dt;
                    rpCategorias.DataBind();
                }

                dt.Dispose();
            }
            catch (Exception ex)
            {
                ltMensaje.Text =
                   "<div class='alert alert-danger alert-dismissable'>" +
                   "<button aria-hidden='true' data-dismiss='alert' class='close' type='button'>×</button>" +
                   "<strong>Error:</strong> " + ex.Message.ToString() +
                   "</div>";
            }
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarCategoriasPaginas();
                string nombreArchivo = $"Paginas_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    cg.ExportarExcelOk(dt, nombreArchivo);
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
            DataTable dt = cg.ConsultarCategoriasPaginasPorId(int.Parse(Request.QueryString["editid"].ToString()));

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();

            return strData;
        }

        protected void rpCategorias_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "categoriaspaginas?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "categoriaspaginas?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }
    }
}