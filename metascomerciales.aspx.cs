using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class metascomerciales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Metas comerciales");
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
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                    ListaMetasComerciales();
                    CargarCanalesVenta();
                    ltTitulo.Text = "Agregar meta comercial";

                    if (Request.QueryString.Count > 0)
                    {
                        rpMetasComerciales.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarMetaComercialPorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                txbPresupuesto.Text = dt.Rows[0]["Valor"].ToString();
                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar meta comercial";
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
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    btnAgregar.Enabled = false;
                                    ltTitulo.Text = "Borrar meta comercial";
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
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    ltTitulo.Text = "Borrar meta comercial";
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

        private void ListaMetasComerciales()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarMetasComerciales();
            rpMetasComerciales.DataSource = dt;
            rpMetasComerciales.DataBind();
            dt.Dispose();
        }

        protected string ObtenerNombreMes(object mes)
        {
            int numeroMes = Convert.ToInt32(mes);
            DateTime fecha = new DateTime(2025, numeroMes, 1);
            return fecha.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
        }

        private void CargarCanalesVenta()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCanalesVenta();

            DataRow[] filasFiltradas = dt.Select("idCanalVenta <> 1"); // Se excluye la opción 1 Ninguno

            if (filasFiltradas.Length > 0)
            {
                dt = filasFiltradas.CopyToDataTable();
            }
            else
            {
                dt.Clear();
            }

            ddlCanalVenta.DataSource = dt;
            ddlCanalVenta.DataBind();
            dt.Dispose();
        }

        protected void rpMetasComerciales_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "metascomerciales?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "metascomerciales?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }

        private bool ValidarMetaComercial()
        {
            int CanalVenta = Convert.ToInt32(ddlCanalVenta.SelectedItem.Value.ToString());
            int mes = Convert.ToInt32(ddlMes.SelectedItem.Value.ToString());
            int annio = Convert.ToInt32(ddlAnnio.SelectedItem.Value.ToString());

            bool bExiste = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarMetaComercial(CanalVenta, mes, annio);
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
                    string respuesta = cg.ActualizarMetaComercial(
                        Convert.ToInt32(Request.QueryString["editid"].ToString()),
                        Convert.ToInt32(ddlCanalVenta.SelectedItem.Value.ToString()),
                        Convert.ToInt32(ddlMes.SelectedItem.Value.ToString()),
                        Convert.ToInt32(ddlAnnio.SelectedItem.Value.ToString()),
                        Convert.ToInt32(txbPresupuesto.Text.ToString()),
                        Convert.ToInt32(Session["idUsuario"].ToString())
                        );

                    string strNewData = TraerData();
                    cg.InsertarLog(Session["idusuario"].ToString(), "MetasComerciales", "Modifica", "El usuario modificó la nueva meta comercial: " +
                            "Canal de venta: " + ddlCanalVenta.SelectedItem.Text.ToString() + ", Mes: " + ddlMes.SelectedItem.Text.ToString() + ", " +
                            "Año: " + ddlAnnio.SelectedItem.Text.ToString() + ", Valor: $ " + txbPresupuesto.Text.ToString(), strInitData, strNewData);
                }
                if (Request.QueryString["deleteid"] != null)
                {
                    //string respuesta = cg.EliminarMetaComercial(int.Parse(Request.QueryString["deleteid"].ToString()));
                }
                Response.Redirect("metascomerciales");
            }
            else
            {
                if (!ValidarMetaComercial())
                {
                    try
                    {
                        string respuesta = cg.InsertarMetaComercial(
                            Convert.ToInt32(ddlCanalVenta.SelectedItem.Value.ToString()), 
                            Convert.ToInt32(ddlMes.SelectedItem.Value.ToString()), 
                            Convert.ToInt32(ddlAnnio.SelectedItem.Value.ToString()), 
                            Convert.ToInt32(txbPresupuesto.Text.ToString()),
                            Convert.ToInt32(Session["idUsuario"].ToString())
                            );

                        cg.InsertarLog(Session["idusuario"].ToString(), "MetasComerciales", "Agrega", "El usuario agregó una nueva meta comercial: " +
                            "Canal de venta: " + ddlCanalVenta.SelectedItem.Text.ToString() + ", Mes: " + ddlMes.SelectedItem.Text.ToString() + ", " +
                            "Año: " + ddlAnnio.SelectedItem.Text.ToString() + ", Valor: $ " + txbPresupuesto.Text.ToString(), "", "");
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
                    Response.Redirect("metascomerciales");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Ya existe una meta comercial para ese canal de venta en el mismo mes y año." +
                        "</div>";
                }
            }
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string consultaSQL = @"SELECT mc.idMeta, mc.idCanalVenta, cv.NombreCanalVenta, mc.Mes, mc.Annio, mc.Valor  
	                                   FROM metascomerciales mc, canalesventa cv 
                                       WHERE mc.idCanalVenta = cv.idCanalVenta 
	                                   ORDER BY idCanalVenta, Annio, Mes;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
                string nombreArchivo = $"METAS_COMERCIALES_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

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
            DataTable dt = cg.ConsultarMetaComercialPorId(int.Parse(Request.QueryString["editid"].ToString()));

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