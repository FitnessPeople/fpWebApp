using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
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
                                if (dt.Rows[0]["idCanalVenta"].ToString() != "")
                                {
                                    ddlCanalVenta.SelectedIndex = Convert.ToInt16(ddlCanalVenta.Items.IndexOf(ddlCanalVenta.Items.FindByValue(dt.Rows[0]["idCanalVenta"].ToString())));
                                }
                                else
                                {
                                    ddlCanalVenta.SelectedItem.Value = "";
                                }
                                ddlMes.SelectedIndex = Convert.ToInt16(ddlMes.Items.IndexOf(ddlMes.Items.FindByValue(dt.Rows[0]["mes"].ToString())));
                                ddlAnnio.SelectedIndex = Convert.ToInt16(ddlAnnio.Items.IndexOf(ddlAnnio.Items.FindByValue(dt.Rows[0]["annio"].ToString())));
                                int presupuesto = (dt.Rows[0]["Presupuesto"].ToString() != "") ? Convert.ToInt32(dt.Rows[0]["Presupuesto"]) : 0;
                                int metaAsesorDeluxe = (dt.Rows[0]["MetaAsesorDeluxe"].ToString() != "") ? Convert.ToInt32(dt.Rows[0]["MetaAsesorDeluxe"]) : 0;
                                int metaAsesorPremium  = (dt.Rows[0]["MetaAsesorPremium"].ToString() != "") ? Convert.ToInt32(dt.Rows[0]["MetaAsesorPremium"]) : 0;
                                int metaAsesorElite = (dt.Rows[0]["MetaAsesorElite"].ToString() != "") ? Convert.ToInt32(dt.Rows[0]["MetaAsesorElite"]) : 0;
                                int metaDirectorSede = (dt.Rows[0]["MetaDirectorSede"].ToString() != "") ? Convert.ToInt32(dt.Rows[0]["MetaDirectorSede"]) : 0;
                                int metaAsesorOnline = (dt.Rows[0]["MetaAsesorOnline"].ToString() != "") ? Convert.ToInt32(dt.Rows[0]["MetaAsesorOnline"]) : 0;
                                txbPresupuesto.Text = presupuesto.ToString("C0", new CultureInfo("es-CO"));
                                txbAsesorDeluxe.Text = metaAsesorDeluxe.ToString("C0", new CultureInfo("es-CO"));
                                txbAsesorPremium.Text = metaAsesorPremium.ToString("C0", new CultureInfo("es-CO"));
                                txbAsesorElite.Text = metaAsesorElite.ToString("C0", new CultureInfo("es-CO"));
                                txbDirectorSede.Text = metaDirectorSede.ToString("C0", new CultureInfo("es-CO"));
                                txbAsesorOnline.Text = metaAsesorOnline.ToString("C0", new CultureInfo("es-CO"));
                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar meta comercial";
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            // Eliminar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = new DataTable();
                            dt = cg.ConsultarMetaComercialPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["idCanalVenta"].ToString() != "")
                                {
                                    ddlCanalVenta.SelectedIndex = Convert.ToInt16(ddlCanalVenta.Items.IndexOf(ddlCanalVenta.Items.FindByValue(dt.Rows[0]["idCanalVenta"].ToString())));
                                }
                                else
                                {
                                    ddlCanalVenta.SelectedItem.Value = "";
                                }
                                ddlCanalVenta.Enabled = false;
                                txbPresupuesto.Text = dt.Rows[0]["Presupuesto"].ToString();
                                txbPresupuesto.Enabled = false;
                                ddlMes.Enabled = false;
                                ddlAnnio.Enabled = false;
                                btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                ltTitulo.Text = "Eliminar meta";
                            }
                            dt.Dispose();
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
            if (Request.QueryString.Count > 0)  // Viene a editar o a borrar el registro
            {
                //if (!ValidarMetaComercial())
                //{
                    string strInitData = TraerData();

                    if (Request.QueryString["editid"] != null)
                    {
                        string respuesta = cg.ActualizarMetaComercial(
                            Convert.ToInt32(Request.QueryString["editid"].ToString()),
                            Convert.ToInt32(ddlCanalVenta.SelectedItem.Value.ToString()),
                            Convert.ToInt32(ddlMes.SelectedItem.Value.ToString()),
                            Convert.ToInt32(ddlAnnio.SelectedItem.Value.ToString()),
                            Convert.ToInt32(Regex.Replace(txbPresupuesto.Text, @"[^\d]", "")),
                            Convert.ToInt32(Regex.Replace(txbAsesorDeluxe.Text, @"[^\d]", "")), 
                            Convert.ToInt32(Regex.Replace(txbAsesorPremium.Text, @"[^\d]", "")), 
                            Convert.ToInt32(Regex.Replace(txbAsesorElite.Text, @"[^\d]", "")), 
                            Convert.ToInt32(Regex.Replace(txbDirectorSede.Text, @"[^\d]", "")), 
                            Convert.ToInt32(Regex.Replace(txbAsesorOnline.Text, @"[^\d]", "")), 
                            Convert.ToInt32(Session["idUsuario"].ToString())
                            );

                        string strNewData = TraerData();
                        cg.InsertarLog(Session["idusuario"].ToString(), "MetasComerciales", "Modifica", "El usuario modificó la nueva meta comercial: " +
                                "Canal de venta: " + ddlCanalVenta.SelectedItem.Text.ToString() + ", Mes: " + ddlMes.SelectedItem.Text.ToString() + ", " +
                                "Año: " + ddlAnnio.SelectedItem.Text.ToString() + ", Presupuesto: $ " + Regex.Replace(txbPresupuesto.Text, @"[^\d]", ""), strInitData, strNewData);
                    }
                    if (Request.QueryString["deleteid"] != null)
                    {
                        string respuesta = cg.EliminarMetaComercial(int.Parse(Request.QueryString["deleteid"].ToString()));
                    }
                    Response.Redirect("metascomerciales");
                //}
                //else
                //{
                //    MostrarAlerta("Error", "Ya existe un registro para misma fecha y el mismo canal de venta.", "error");
                //    //ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                //    //"<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                //    //"Ya existe un registro para misma fecha y el mismo canal de venta." +
                //    //"</div>";
                //}

                
            }
            else // Viene a agregar un registro
            {
                if (chbTodoElAnnio.Checked)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        try
                        {
                            string respuesta = cg.InsertarMetaComercial(
                                Convert.ToInt32(ddlCanalVenta.SelectedItem.Value.ToString()),
                                i+1,
                                Convert.ToInt32(ddlAnnio.SelectedItem.Value.ToString()),
                                Convert.ToInt32(Regex.Replace(txbPresupuesto.Text, @"[^\d]", "")),
                                Convert.ToInt32(Regex.Replace(txbAsesorDeluxe.Text, @"[^\d]", "")),
                                Convert.ToInt32(Regex.Replace(txbAsesorPremium.Text, @"[^\d]", "")),
                                Convert.ToInt32(Regex.Replace(txbAsesorElite.Text, @"[^\d]", "")),
                                Convert.ToInt32(Regex.Replace(txbDirectorSede.Text, @"[^\d]", "")),
                                Convert.ToInt32(Regex.Replace(txbAsesorOnline.Text, @"[^\d]", "")),
                                Convert.ToInt32(Session["idUsuario"].ToString())
                                );
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
                            MostrarAlerta("Error", ex.Message.ToString(), "error");
                            //ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                            //"<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                            //"Excepción interna." +
                            //"</div>";
                        }
                    }
                    cg.InsertarLog(Session["idusuario"].ToString(), "MetasComerciales", "Agrega", "El usuario agregó una nueva meta comercial: " +
                        "Canal de venta: " + ddlCanalVenta.SelectedItem.Text.ToString() + ", todo el año, " +
                        "Año: " + ddlAnnio.SelectedItem.Text.ToString() + ", Presupuesto: $ " + Regex.Replace(txbPresupuesto.Text, @"[^\d]", ""), "", "");

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
                                Convert.ToInt32(Regex.Replace(txbPresupuesto.Text, @"[^\d]", "")),
                                Convert.ToInt32(Regex.Replace(txbAsesorDeluxe.Text, @"[^\d]", "")),
                                Convert.ToInt32(Regex.Replace(txbAsesorPremium.Text, @"[^\d]", "")),
                                Convert.ToInt32(Regex.Replace(txbAsesorElite.Text, @"[^\d]", "")),
                                Convert.ToInt32(Regex.Replace(txbDirectorSede.Text, @"[^\d]", "")),
                                Convert.ToInt32(Regex.Replace(txbAsesorOnline.Text, @"[^\d]", "")),
                                Convert.ToInt32(Session["idUsuario"].ToString())
                                );

                            cg.InsertarLog(Session["idusuario"].ToString(), "MetasComerciales", "Agrega", "El usuario agregó una nueva meta comercial: " +
                                "Canal de venta: " + ddlCanalVenta.SelectedItem.Text.ToString() + ", Mes: " + ddlMes.SelectedItem.Text.ToString() + ", " +
                                "Año: " + ddlAnnio.SelectedItem.Text.ToString() + ", Presupuesto: $ " + Regex.Replace(txbPresupuesto.Text, @"[^\d]", ""), "", "");
                        }
                        catch (Exception ex)
                        {
                            string mensajeExcepcionInterna = string.Empty;
                            //Console.WriteLine(ex.Message);
                            if (ex.InnerException != null)
                            {
                                mensajeExcepcionInterna = ex.InnerException.Message;
                                //Console.WriteLine("Mensaje de la excepción interna: " + mensajeExcepcionInterna);
                            }
                            MostrarAlerta("Error", ex.Message.ToString(), "error");
                            //ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                            //"<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                            //"Excepción interna." +
                            //"</div>";
                        }
                        Response.Redirect("metascomerciales");
                    }
                    else
                    {
                        MostrarAlerta("Error", "Ya existe una meta comercial para ese canal de venta en el mismo mes y año.", "error");
                        //ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        //    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        //    "Ya existe una meta comercial para ese canal de venta en el mismo mes y año." +
                        //    "</div>";
                    }
                }
            }
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            // tipo puede ser: 'success', 'error', 'warning', 'info', 'question'
            string script = $@"
            Swal.fire({{
                title: '{titulo}',
                text: '{mensaje}',
                icon: '{tipo}', 
                background: '#3C3C3C', 
                showCloseButton: true, 
                confirmButtonText: 'Aceptar', 
                customClass: {{
                    popup: 'alert',
                    confirmButton: 'btn-confirm-alert'
                }},
            }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string consultaSQL = @"SELECT mc.idMeta, mc.idCanalVenta, cv.NombreCanalVenta, mc.Mes, mc.Annio, mc.Presupuesto  
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