using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZstdSharp.Unsafe;

namespace fpWebApp
{
    public partial class negociarconvenio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Negociar convenio");
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
                        lblMensaje.Visible = false ;
                        txbFechaIni.Attributes.Add("type", "date");
                        txbFechaIni.Value = DateTime.Now.ToString("yyyy-MM-dd");
                        txbFechaFin.Attributes.Add("type", "date");
                        txbFechaFin.Attributes.Add("min", DateTime.Now.ToString("yyyy-MM-dd"));
                        txbFechaFin.Value = DateTime.Now.ToString("yyyy-MM-dd");
                    }

                    CargarDiccionarios();
                    listaEstrategias();
                    //CargartiposEstrategias();
                    CargarPlanes();
                    ListaCanalesDeVenta();

                    ltTitulo.Text = "Establecer condiciones";

                    if (Request.QueryString.Count > 0)
                    {
                        rpEstrategias.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarEstrategiaMarketingPorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                //txbNombreEstrategia.Text = dt.Rows[0]["NombreEstrategia"].ToString();
                                ddlEmpresas.SelectedIndex = ddlEmpresas.Items.IndexOf(ddlEmpresas.Items.FindByValue(dt.Rows[0]["idTipoEstrategia"].ToString()));

                                hiddenEditor.Value = dt.Rows[0]["DescripcionEstrategia"].ToString();

                                decimal ValorPresupuesto = Convert.ToDecimal(dt.Rows[0]["ValorPresupuesto"]);
                                txbValorPresupuesto.Text = ValorPresupuesto.ToString("C0", new CultureInfo("es-CO"));

                                txbFechaIni.Value = Convert.ToDateTime(dt.Rows[0]["FechaInicio"]).ToString("yyyy-MM-dd");
                                txbFechaFin.Value = Convert.ToDateTime(dt.Rows[0]["FechaFin"]).ToString("yyyy-MM-dd");

                                // SELECCIONAR PLANES
                                string[] planesSeleccionados = dt.Rows[0]["Planes"].ToString().Split(',');
                                foreach (string planId in planesSeleccionados)
                                {
                                    ListItem item = chblPlanes.Items.FindByValue(planId.Trim());
                                    if (item != null)
                                    {
                                        item.Selected = true;
                                    }
                                }

                                // SELECCIONAR CANALES
                                //string[] canalesSeleccionados = dt.Rows[0]["CanalesVenta"].ToString().Split(',');
                                //foreach (string canalId in canalesSeleccionados)
                                //{
                                //    ListItem item = chblCanales.Items.FindByValue(canalId.Trim());
                                //    if (item != null)
                                //    {
                                //        item.Selected = true;
                                //    }
                                //}

                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar estrategia";
                            }
                        }

                        if (Request.QueryString["deleteid"] != null)
                        {
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ValidarEstrategiaMarketingTablas(int.Parse(Request.QueryString["deleteid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                lblMensaje.Text = "<div class=\"ibox-content\">" +
                                    "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                    "Esta estrategia no se puede borrar, hay registros asociados a ella." +
                                    "</div></div>";

                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarEstrategiaMarketingPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    //txbNombreEstrategia.Text = dt1.Rows[0]["NombreEstrategia"].ToString();
                                    //txbNombreEstrategia.Enabled = false;

                                    ddlEmpresas.SelectedIndex = ddlEmpresas.Items.IndexOf(ddlEmpresas.Items.FindByValue(dt1.Rows[0]["idTipoEstrategia"].ToString()));
                                    ddlEmpresas.Enabled = false;

                                    hiddenEditor.Value = dt1.Rows[0]["DescripcionEstrategia"].ToString();

                                    txbFechaIni.Value = Convert.ToDateTime(dt1.Rows[0]["FechaInicio"]).ToString("yyyy-MM-dd");
                                    txbFechaIni.Disabled = true;

                                    txbFechaFin.Value = Convert.ToDateTime(dt1.Rows[0]["FechaFin"]).ToString("yyyy-MM-dd");
                                    txbFechaFin.Disabled = true;

                                    // SELECCIONAR PLANES
                                    string[] planesSeleccionados = dt1.Rows[0]["Planes"].ToString().Split(',');
                                    foreach (string planId in planesSeleccionados)
                                    {
                                        ListItem item = chblPlanes.Items.FindByValue(planId.Trim());
                                        if (item != null)
                                        {
                                            item.Selected = true;
                                        }
                                    }
                                    chblPlanes.Enabled = false;

                                    // SELECCIONAR CANALES
                                    //string[] canalesSeleccionados = dt1.Rows[0]["CanalesVenta"].ToString().Split(',');
                                    //foreach (string canalId in canalesSeleccionados)
                                    //{
                                    //    ListItem item = chblCanales.Items.FindByValue(canalId.Trim());
                                    //    if (item != null)
                                    //    {
                                    //        item.Selected = true;
                                    //    }
                                    //}
                                    //chblCanales.Enabled = false;

                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    btnAgregar.Enabled = false;
                                    ltTitulo.Text = "Borrar Estrategia";
                                }
                                dt1.Dispose();
                            }
                            else
                            {
                                //Borrar
                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarEstrategiaMarketingPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    //txbNombreEstrategia.Text = dt1.Rows[0]["NombreEstrategia"].ToString();
                                    //txbNombreEstrategia.Enabled = false;

                                    ddlEmpresas.SelectedIndex = ddlEmpresas.Items.IndexOf(ddlEmpresas.Items.FindByValue(dt1.Rows[0]["idTipoEstrategia"].ToString()));
                                    ddlEmpresas.Enabled = false;

                                    hiddenEditor.Value = dt1.Rows[0]["DescripcionEstrategia"].ToString();

                                    txbFechaIni.Value = Convert.ToDateTime(dt1.Rows[0]["FechaInicio"]).ToString("yyyy-MM-dd");
                                    txbFechaIni.Disabled = true;

                                    txbFechaFin.Value = Convert.ToDateTime(dt1.Rows[0]["FechaFin"]).ToString("yyyy-MM-dd");
                                    txbFechaFin.Disabled = true;

                                    // SELECCIONAR PLANES
                                    string[] planesSeleccionados = dt1.Rows[0]["Planes"].ToString().Split(',');
                                    foreach (string planId in planesSeleccionados)
                                    {
                                        ListItem item = chblPlanes.Items.FindByValue(planId.Trim());
                                        if (item != null)
                                        {
                                            item.Selected = true;
                                        }
                                    }
                                    chblPlanes.Enabled = false;

                                    // SELECCIONAR CANALES
                                    //string[] canalesSeleccionados = dt1.Rows[0]["CanalesVenta"].ToString().Split(',');
                                    //foreach (string canalId in canalesSeleccionados)
                                    //{
                                    //    ListItem item = chblCanales.Items.FindByValue(canalId.Trim());
                                    //    if (item != null)
                                    //    {
                                    //        item.Selected = true;
                                    //    }
                                    //}
                                    //chblCanales.Enabled = false;
                                    //btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    //btnAgregar.Enabled = true;
                                    //ltTitulo.Text = "Borrar Estrategia";

                                }
                                dt1.Dispose();

                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("logout");
                }
            }
        }

        private void listaEstrategias()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstrategiasMarketing();

            foreach (DataRow row in dt.Rows)
            {
                row["Planes"] = ConvertirIdsANombres(row["Planes"].ToString(), dicPlanes);
                row["CanalesVenta"] = ConvertirIdsANombres(row["CanalesVenta"].ToString(), dicCanales);
            }

            rpEstrategias.DataSource = dt;
            rpEstrategias.DataBind();

            dt.Dispose();
        }

        private void CargarPlanes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanesVigentes();

            chblPlanes.DataSource = dt;
            chblPlanes.DataTextField = "NombrePlan";
            chblPlanes.DataValueField = "idPlan";
            chblPlanes.DataBind();

            dt.Dispose();
        }
        private void ListaCanalesDeVenta()
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

            //chblCanales.DataSource = dt;
            //chblCanales.DataTextField = "NombreCanalVenta";
            //chblCanales.DataValueField = "idCanalVenta";
            //chblCanales.DataBind();

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
        private string ConvertirIdsANombres(string ids, Dictionary<int, string> diccionario)
        {
            if (string.IsNullOrEmpty(ids)) return "";

            var nombres = ids.Split(',')
                             .Select(id => int.TryParse(id.Trim(), out int parsedId) && diccionario.ContainsKey(parsedId)
                                 ? diccionario[parsedId]
                                 : $"(ID {id})") // por si el id no existe
                             .ToList();

            return string.Join(", ", nombres);
        }


        private Dictionary<int, string> dicPlanes;
        private Dictionary<int, string> dicCanales;

        private void CargarDiccionarios()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dtCanales = cg.ConsultarCanalesVenta();

            DataTable dtPlanes = cg.ConsultarPlanesVigentes();

            dicCanales = dtCanales.AsEnumerable()
                .ToDictionary(row => Convert.ToInt32(row["idCanalVenta"]), row => row["NombreCanalVenta"].ToString());


            dicPlanes = dtPlanes.AsEnumerable()
                .ToDictionary(row => Convert.ToInt32(row["idPlan"]), row => row["NombrePlan"].ToString());
        }


        private void CargartiposEstrategias()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarTiposEstrategiasMarketing();
            ddlEmpresas.DataSource = dt;
            ddlEmpresas.DataBind();
            dt.Dispose();
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

        private bool ValidarEstrategia(string strNombre)
        {
            bool bExiste = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstrategiaMarketingPorNombre(strNombre.ToString().Trim());

            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }

            return bExiste;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            string contenidoEditor = hiddenEditor.Value;
            bool salida = false;
            string mensaje = string.Empty;
            string planesObtenidos = string.Empty;
            string canalesObtenidos = string.Empty;

            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editid"] != null)
                {
                    List<int> planesSeleccionados = new List<int>();

                    foreach (ListItem item in chblPlanes.Items)
                    {
                        if (item.Selected)
                        {
                            int idPlan = int.Parse(item.Value);
                            planesSeleccionados.Add(idPlan);
                        }
                    }
                    planesObtenidos = string.Join(",", planesSeleccionados);

                    //List<int> canalesSeleccionados = new List<int>();

                    //foreach (ListItem item in chblCanales.Items)
                    //{
                    //    if (item.Selected)
                    //    {
                    //        int idCanalVenta = int.Parse(item.Value);
                    //        canalesSeleccionados.Add(idCanalVenta);
                    //    }
                    //}
                    //canalesObtenidos = string.Join(",", canalesSeleccionados);

                    string strInitData = TraerData();
                    try
                    {
                        string respuesta = cg.ActualizarEstrategiaMarketing(Convert.ToInt32(Request.QueryString["editid"].ToString()), "",
                        contenidoEditor, txbFechaIni.Value, txbFechaFin.Value, canalesObtenidos, Convert.ToInt32(ddlEmpresas.SelectedItem.Value.ToString()),
                        planesObtenidos, Convert.ToDecimal(Regex.Replace(txbValorPresupuesto.Text, @"[^\d]", "")), out salida, out mensaje);

                        if (salida)
                        {
                            string script = @"
                                    Swal.fire({
                                        title: '«¡Actualizada correctamente!»',
                                        text: '" + mensaje.Replace("'", "\\'") + @"',
                                        icon: 'success',
                                        timer: 3000, // 3 segundos
                                        showConfirmButton: false,
                                        timerProgressBar: true
                                    }).then(() => {
                                        window.location.href = 'estrategiasmarketing';
                                    });
                                    ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                        }
                        else
                        {
                            string script = @"
                                    Swal.fire({
                                        title: 'Error',
                                        text: '" + mensaje.Replace("'", "\\'") + @"',
                                        icon: 'error'
                                    }).then((result) => {
                                        if (result.isConfirmed) {
                                          window.location.href = 'estrategiasmarketing';
                                        }
                                    });
                                ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        mensaje = ex.Message.ToString();
                        string script = @"
                            Swal.fire({
                                title: 'Error',
                                text: '" + mensaje.Replace("'", "\\'") + @"',
                                icon: 'error'
                            });
                        ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                    }
                    string strNewData = TraerData();
                    cg.InsertarLog(Session["idusuario"].ToString(), "estrategiaasmarketing", "Modifica", "El usuario modificó datos a la estrategia " + "" + ".", strInitData, strNewData);

                }
                if (Request.QueryString["deleteid"] != null)
                {
                    try
                    {
                        string respuesta = cg.EliminarEstrategiaMarketing(int.Parse(Request.QueryString["deleteid"].ToString()));

                        if (respuesta == "OK")
                        {
                            string script = @"
                                    Swal.fire({
                                        title: '«¡Eliminada correctamente!»',
                                        text: '" + mensaje.Replace("'", "\\'") + @"',
                                        icon: 'success',
                                        timer: 3000, // 3 segundos
                                        showConfirmButton: false,
                                        timerProgressBar: true
                                    }).then(() => {
                                        window.location.href = 'estrategiasmarketing';
                                    });
                                    ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                        }
                        else
                        {
                            string script = @"
                                    Swal.fire({
                                        title: 'Error',
                                        text: '" + mensaje.Replace("'", "\\'") + @"',
                                        icon: 'error'
                                    }).then((result) => {
                                        if (result.isConfirmed) {
                                          window.location.href = 'estrategiasmarketing';
                                        }
                                    });
                                ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        mensaje = ex.Message.ToString();
                        string script = @"
                            Swal.fire({
                                title: 'Error',
                                text: '" + mensaje.Replace("'", "\\'") + @"',
                                icon: 'error'
                            });
                        ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                    }
                }

            }
            else
            {
                //if (!ValidarEstrategia(txbNombreEstrategia.Text.ToString()))
                //{
                //    List<int> planesSeleccionados = new List<int>();

                //    foreach (ListItem item in chblPlanes.Items)
                //    {
                //        if (item.Selected)
                //        {
                //            int idPlan = int.Parse(item.Value);
                //            planesSeleccionados.Add(idPlan);
                //        }
                //    }
                //    planesObtenidos = string.Join(",", planesSeleccionados);

                //    List<int> canalesSeleccionados = new List<int>();

                //    foreach (ListItem item in chblCanales.Items)
                //    {
                //        if (item.Selected)
                //        {
                //            int idCanalVenta = int.Parse(item.Value);
                //            canalesSeleccionados.Add(idCanalVenta);
                //        }
                //    }
                //    canalesObtenidos = string.Join(",", canalesSeleccionados);

                //    try
                //    {
                //        string respuesta = cg.InsertarEstrategiaMarketing(txbNombreEstrategia.Text, contenidoEditor, txbFechaIni.Value, txbFechaFin.Value, canalesObtenidos,
                //             Convert.ToInt32(ddlTipoEstrategias.SelectedItem.Value.ToString()), planesObtenidos, Convert.ToInt32(Session["idUsuario"].ToString()),
                //             Convert.ToDecimal(Regex.Replace(txbValorPresupuesto.Text, @"[^\d]", "")), out salida, out mensaje);
                //        if (salida)
                //        {
                //            string script = @"
                //                    Swal.fire({
                //                        title: '«¡Creada correctamente!»',
                //                        text: '" + mensaje.Replace("'", "\\'") + @"',
                //                        icon: 'success',
                //                        timer: 3000, // 3 segundos
                //                        showConfirmButton: false,
                //                        timerProgressBar: true
                //                    }).then(() => {
                //                        window.location.href = 'estrategiasmarketing';
                //                    });
                //                    ";
                //            ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                //        }
                //        else
                //        {
                //            string script = @"
                //                    Swal.fire({
                //                        title: 'Error',
                //                        text: '" + mensaje.Replace("'", "\\'") + @"',
                //                        icon: 'error'
                //                    }).then((result) => {
                //                        if (result.isConfirmed) {
                //                          window.location.href = 'estrategiasmarketing';
                //                        }
                //                    });
                //                ";
                //            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        mensaje = ex.Message.ToString();
                //        string script = @"
                //            Swal.fire({
                //                title: 'Error',
                //                text: '" + mensaje.Replace("'", "\\'") + @"',
                //                icon: 'error'
                //            });
                //        ";
                //        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                //    }
                //}
                //else
                //{
                //    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                //        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                //        "Ya existe una estrategia con ese nombre." +
                //        "</div>";
                //}
            }

        }

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstrategiaMarketingPorId(int.Parse(Request.QueryString["editid"].ToString()));

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();
            return strData;
        }

        protected void cvPlanes_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Verifica que al menos un ítem esté seleccionado
            args.IsValid = chblPlanes.Items.Cast<ListItem>().Any(li => li.Selected);
        }


        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string consultaSQL = @"SELECT s.NombreSede AS 'Nombre de Sede', s.DireccionSede AS 'Dirección', 
                                       cs.NombreCiudadSede AS 'Ciudad', s.TelefonoSede AS 'Teléfono', 
                                       s.HorarioSede AS 'Horarios', s.TipoSede AS 'Tipo de Sede', s.ClaseSede AS 'Clase de Sede'
                                       FROM Sedes s, CiudadesSedes cs 
                                       WHERE s.idCiudadSede = cs.idCiudadSede 
                                       ORDER BY s.NombreSede;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
                string nombreArchivo = $"Sedes_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

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

        protected void rpEstrategias_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "estrategiasmarketing?editid=" + ((DataRowView)e.Item.DataItem).Row["idEstrategia"].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "estrategiasmarketing?deleteid=" + ((DataRowView)e.Item.DataItem).Row["idEstrategia"].ToString());
                    btnEliminar.Visible = true;
                }

            }
        }

        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}