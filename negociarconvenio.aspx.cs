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
using static NPOI.HSSF.Util.HSSFColor;

namespace fpWebApp
{
    public partial class negociarconvenio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["editid"] != null)
                {
                    btnAgregar.Text = "Actualizar";
                }
                try
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
                            ddlProspectos.Enabled = true;
                            ddlEmpresas.Enabled = true;
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
                            //lblMensaje.Visible = false ;
                            txbFechaIni.Attributes.Add("type", "date");
                            txbFechaIni.Value = DateTime.Now.ToString("yyyy-MM-dd");
                            txbFechaFin.Attributes.Add("type", "date");
                            DateTime hoy = DateTime.Today;
                            DateTime ultimoDiaMes = new DateTime(hoy.Year, hoy.Month, DateTime.DaysInMonth(hoy.Year, hoy.Month));
                            txbFechaFin.Value = ultimoDiaMes.ToString("yyyy-MM-dd");
                        }


                        listaEmpresasAfiliadas();
                        CargarPlanes();
                        CargarNegociaciones(Convert.ToInt32(Session["idUsuario"].ToString()));

                        ltTitulo.Text = "Establecer condiciones";
                        string descifradoEdit = null;
                        string descifradoDelete = null;

                        clasesglobales cg = new clasesglobales();


                        if (Request.QueryString.Count > 0)
                        {
                            //  Editar
                            if (!string.IsNullOrEmpty(Request.QueryString["editid"]))
                            {
                                try
                                {
                                    string cifrado = Request.QueryString["editid"];
                                    descifradoEdit = cg.Decrypt(cifrado);
                                }
                                catch
                                {
                                    MostrarAlerta(
                                        "Error",
                                        "El enlace de edición no es válido.",
                                        "error"
                                    );
                                    return;
                                }
                            }

                            //  Eliminar
                            if (!string.IsNullOrEmpty(Request.QueryString["deleteid"]))
                            {
                                try
                                {
                                    string cifrado1 = Request.QueryString["deleteid"];
                                    descifradoDelete = cg.Decrypt(cifrado1);
                                }
                                catch
                                {
                                    MostrarAlerta(
                                        "Error",
                                        "El enlace de eliminación no es válido.",
                                        "error"
                                    );
                                    return;
                                }
                            }

                            rpNegociaciones.Visible = false;
                            if (descifradoEdit != null)
                            {
                                //Editar                                
                                DataTable dt = cg.ConsultarNegociacionPorId(int.Parse(descifradoEdit));
                                if (dt.Rows.Count > 0)
                                {
                                    CargarPlanes();
                                    ddlEmpresas.Enabled = false;
                                    ddlProspectos.Enabled = false;
                                    
                                    hfModo.Value = "edicion";// edicion ó lectura
                                    hfIdPlan.Value = dt.Rows[0]["idPlan"].ToString();
                                    hfDescuento.Value = dt.Rows[0]["Descuento"].ToString();
                                    hfValorNegociacion.Value = dt.Rows[0]["ValorNegociacion"].ToString();
                                    ddlEmpresas.SelectedIndex = ddlEmpresas.Items.IndexOf(ddlEmpresas.Items.FindByValue(dt.Rows[0]["DocumentoEmpresa"].ToString()));

                                    ListaProspectos(ddlEmpresas.SelectedValue);
                                    ddlProspectos.SelectedValue = dt.Rows[0]["idPregestion"].ToString();  
                                    hiddenEditor.Value = dt.Rows[0]["Descripcion"].ToString();
                                    txbFechaIni.Value = Convert.ToDateTime(dt.Rows[0]["FechaIni"]).ToString("yyyy-MM-dd");
                                    txbFechaFin.Value = Convert.ToDateTime(dt.Rows[0]["FechaFin"]).ToString("yyyy-MM-dd");
                                    btnAgregar.Text = "Actualizar";
                                    hfAccion.Value = "editar";
                                    ltTitulo.Text = "Actualizar negociación";
                                }
                            }

                            if (descifradoDelete != null)
                            {
                                
                                    //Borrar
                                    DataTable dt1 = new DataTable();
                                    dt1 = cg.ConsultarNegociacionPorId(int.Parse(descifradoDelete));
                                    if (dt1.Rows.Count > 0)
                                    {
                                        CargarPlanes();
                                        hfModo.Value = "edicion";// edicion ó lectura
                                        hfIdPlan.Value = dt1.Rows[0]["idPlan"].ToString();
                                        hfDescuento.Value = dt1.Rows[0]["Descuento"].ToString();
                                        hfValorNegociacion.Value = dt1.Rows[0]["ValorNegociacion"].ToString();
                                        ddlEmpresas.SelectedIndex = ddlEmpresas.Items.IndexOf(ddlEmpresas.Items.FindByValue(dt1.Rows[0]["DocumentoEmpresa"].ToString()));
                                        ddlEmpresas.Enabled = false;
                                        ListaProspectos(ddlEmpresas.SelectedValue);
                                        ddlProspectos.SelectedValue = dt1.Rows[0]["idPregestion"].ToString();
                                        ddlProspectos.Enabled = false;
                                        hiddenEditor.Value = dt1.Rows[0]["Descripcion"].ToString();                                    
                                        txbFechaIni.Value = Convert.ToDateTime(dt1.Rows[0]["FechaIni"]).ToString("yyyy-MM-dd");
                                        txbFechaIni.Disabled = true;
                                        txbFechaFin.Value = Convert.ToDateTime(dt1.Rows[0]["FechaFin"]).ToString("yyyy-MM-dd");
                                        txbFechaFin.Disabled = true;
                                        btnAgregar.Text = "Eliminar";
                                        hfAccion.Value = "eliminar";
                                        ltTitulo.Text = "Eliminar negociación";
                                }
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("logout");
                    }
                }
                catch (Exception ex)
                {
                    clasesglobales cg = new clasesglobales();
                    int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                    MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
                }
            }
        }



        private void CargarPlanes()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarPlanesVigentesVisibleCRM();
                rpPlanesVigentes.DataSource = dt;
                rpPlanesVigentes.DataBind();

                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void CargarNegociaciones(int idUsuario)
        {
            clasesglobales cg = new clasesglobales();
            try
            {                
                DataTable dt = cg.ConsultarNegociacionesPorUsuario(idUsuario);
                rpNegociaciones.DataSource = dt;
                rpNegociaciones.DataBind();

                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }

        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            // tipo puede ser: 'success', 'error', 'warning', 'info', 'question'
            string script = $@"
            Swal.hideLoading();
            Swal.fire({{
                title: '{titulo}',
                text: '{mensaje}',
                icon: '{tipo}', 
                allowOutsideClick: false, 
                showCloseButton: false, 
                confirmButtonText: 'Aceptar'
            }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
        }


        private void listaEmpresasAfiliadas()
        {
            clasesglobales cg = new clasesglobales();
            try
            {               
                DataTable dt = cg.ConsultarEmpresasYProspectosCorporativos();

                ddlEmpresas.DataSource = dt;
                ddlEmpresas.DataValueField = "DocumentoEmpresa";
                ddlEmpresas.DataTextField = "NombreEmpresa";
                ddlEmpresas.DataBind();

                 ddlEmpresas.Items.Insert(0, new ListItem("Seleccione", ""));

                 foreach (ListItem item in ddlEmpresas.Items)
                {
                    if (!string.IsNullOrEmpty(item.Value))  
                    {
                        
                        DataRow[] row = dt.Select($"DocumentoEmpresa = '{item.Value}'");

                        if (row.Length > 0)
                        {
                            string estado = row[0]["Origen"].ToString();   
                            item.Text = $"{item.Text} ({estado})";       
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                string documento = ddlEmpresas.SelectedValue;

                if (!string.IsNullOrEmpty(documento))
                {
                    ListaProspectos(documento);
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }

        }


        private void ListaProspectos(string documentoEmpresa)
        {
            clasesglobales cg = new clasesglobales();
            try
            {               
                DataTable dt = cg.ConsultarProspectoClienteCorporativo(documentoEmpresa);

                dt.Columns.Add("NombreCompleto", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    row["NombreCompleto"] = row["NombreContacto"].ToString() + " " +
                                            row["ApellidoContacto"].ToString();
                }

                ddlProspectos.DataSource = dt;
                ddlProspectos.DataTextField = "NombreCompleto"; // ajusta según tu tabla
                ddlProspectos.DataValueField = "IdPregestion";    // ajusta según tu tabla
                ddlProspectos.DataBind();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
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



        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            string contenidoEditor = hiddenEditor.Value;
            string mensaje = string.Empty;
            int codigo = 0;
            string planesObtenidos = string.Empty;
            string canalesObtenidos = string.Empty;
            contenidoEditor = hiddenEditor.Value;

            int idPlan = Convert.ToInt32(hfIdPlan.Value);
            decimal descuento = Convert.ToDecimal(hfDescuento.Value);
            decimal valorFinal = Convert.ToDecimal(hfValorNegociacion.Value);

            string descifradoEdit = null;
            string descifradoDelete = null;

            //  Editar
            if (!string.IsNullOrEmpty(Request.QueryString["editid"]))
            {
                try
                {
                    string cifrado = Request.QueryString["editid"];
                    descifradoEdit = cg.Decrypt(cifrado);
                }
                catch
                {                    
                    MostrarAlerta(
                        "Error",
                        "El enlace de edición no es válido.",
                        "error"
                    );
                    return;
                }
            }

            //  Eliminar
            if (!string.IsNullOrEmpty(Request.QueryString["deleteid"]))
            {
                try
                {
                    string cifrado1 = Request.QueryString["deleteid"];
                    descifradoDelete = cg.Decrypt(cifrado1);
                }
                catch
                {
                    MostrarAlerta(
                        "Error",
                        "El enlace de eliminación no es válido.",
                        "error"
                    );
                    return;
                }
            }

            if (Request.QueryString.Count > 0)
            {
                if (descifradoEdit != null)
                {
                    List<int> planesSeleccionados = new List<int>();

                    DataTable dt1 = cg.ConsultarNegociacionPorId(int.Parse(descifradoEdit));
                    ListaProspectos(ddlEmpresas.SelectedValue);
                    ddlProspectos.SelectedValue = dt1.Rows[0]["idPregestion"].ToString();

                    string strInitData = TraerData();
                    try
                    {
                        string respuesta = cg.ActualizarNegociacionCorporativa(Convert.ToInt32(descifradoEdit),ddlEmpresas.SelectedItem.Value,  
                        Convert.ToInt32(ddlProspectos.SelectedValue),contenidoEditor, txbFechaIni.Value.ToString(), txbFechaFin.Value.ToString(), idPlan, descuento, valorFinal, out codigo, out mensaje);

                        if (codigo==1)
                        {
                            string script = @"
                                    Swal.fire({
                                        title: '«¡Actualizado correctamente!»',
                                        text: '" + mensaje.Replace("'", "\\'") + @"',
                                        icon: 'success',
                                        timer: 3000, // 3 segundos
                                        showConfirmButton: false,
                                        timerProgressBar: true
                                    }).then(() => {
                                        window.location.href = 'negociarconvenio';
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
                                          window.location.href = 'negociarconvenio';
                                        }
                                    });
                                ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                        MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
                    }
                    string strNewData = TraerData();
                    cg.InsertarLog(Session["idusuario"].ToString(), "negociarconvenio", "Modifica", "El usuario modificó datos a la negocición " + "" + ".", strInitData, strNewData);

                }
                if (descifradoDelete != null)
                {
                    try
                    {
                        string respuesta = cg.EliminarNegociacionCorporativa(int.Parse(descifradoDelete));
                        string script;
                        if (respuesta == "OK")
                        {
                            script = @"
                                Swal.fire({
                                    title: '¡Eliminada!',
                                    text: 'La negociación fue eliminada correctamente.',
                                    icon: 'success',
                                    timer: 3000,
                                    showConfirmButton: false
                                }).then(() => {
                                    window.location.href = 'negociarconvenio';
                                });
                            ";
                        }
                        else
                        {
                            script = @"
                                Swal.fire({
                                    title: 'No se puede eliminar',
                                    text: '" + respuesta.Replace("'", "\\'") + @"',
                                    icon: 'warning'
                                }).then(() => {
                                    window.location.href = 'negociarconvenio';
                                });
                            ";
                        }

                        ScriptManager.RegisterStartupScript(this, GetType(), "EliminarMensaje", script, true);
                    }
                    catch (Exception ex)
                    {
                        int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                        MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
                    }
                }

            }
            else
            {
                hfAccion.Value = "agregar";
                btnAgregar.Text = "Agregar";
                // 1. Buscar la FILA seleccionada
                foreach (RepeaterItem item in rpPlanesVigentes.Items)
                {
                    HtmlInputRadioButton rd = (HtmlInputRadioButton)item.FindControl("rdDescuento");
                    if (rd != null && rd.Checked)
                    {
                        // 2. Obtener idPlan
                        // Se encuentra en el primer <td> de la fila
                        Label lblIdPlan = item.FindControl("lblIdPlan") as Label;

                        // Si prefieres sin Label: buscar el TD
                        var tdIdPlan = item.FindControl("tdIdPlan") as System.Web.UI.HtmlControls.HtmlTableCell;

                        if (tdIdPlan != null)
                            idPlan = Convert.ToInt32(tdIdPlan.InnerText.Trim());

                        // 3. Obtener porcentaje de descuento
                        HtmlInputGenericControl txtDesc = (HtmlInputGenericControl)item.FindControl("inputDescuento");
                        if (txtDesc != null)
                            descuento = Convert.ToDecimal(txtDesc.Value);

                        // 4. Obtener valor final (ya calculado en JS)
                        HtmlTableCell tdValorDesc = (HtmlTableCell)item.FindControl("valorConDescuento");
                        if (tdValorDesc != null)
                            valorFinal = ParseCOP(tdValorDesc.InnerText);

                        break; 
                    }
                }

                try
                {
                    contenidoEditor = hiddenEditor.Value;
                    idPlan = Convert.ToInt32(hfIdPlan.Value);
                    descuento = Convert.ToDecimal(hfDescuento.Value);
                    valorFinal = Convert.ToDecimal(hfValorNegociacion.Value);
                    codigo = 0;

                    string respuesta = cg.InsertarNegociacionCorporativo(ddlEmpresas.SelectedItem.Value, Convert.ToInt32(ddlProspectos.SelectedValue.ToString()), contenidoEditor, 
                    txbFechaIni.Value.ToString(), txbFechaFin.Value.ToString(), idPlan, descuento, valorFinal, Convert.ToInt32(Session["idUsuario"]),out codigo, out  mensaje);
                    if (respuesta=="OK")
                    {
                        string script = @"
                                    Swal.fire({
                                        title: '«¡Negociación creada correctamente!»',
                                        text: '" + mensaje.Replace("'", "\\'") + @"',
                                        icon: 'success',
                                        timer: 3000, // 3 segundos
                                        showConfirmButton: false,
                                        timerProgressBar: true
                                    }).then(() => {
                                        window.location.href = 'negociarconvenio';
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
                                          window.location.href = 'negociarconvenio';
                                        }
                                    });
                                ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                    }
                }
                catch (Exception ex)
                {
                    int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                    MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
                }
            }

        }

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarEstrategiaMarketingPorId(int.Parse(Request.QueryString["editid"].ToString()));

                string strData = "";
                foreach (DataColumn column in dt.Columns)
                {
                    strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
                }
                dt.Dispose();
                return strData;
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
                return "";
            }
        }


        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarNegociacionesPorUsuario(Convert.ToInt32(Session["idusuario"]));
                string nombreArchivo = $"{this.GetType().Name}_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

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
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }


        private decimal ParseCOP(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return 0;
            string limpio = new string(valor.Where(char.IsDigit).ToArray());
            return string.IsNullOrEmpty(limpio) ? 0 : Convert.ToDecimal(limpio);
        }


        protected void rpPlanesVigentes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }




        protected void rpNegociaciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        string valor = ((DataRowView)e.Item.DataItem).Row[0].ToString();
                        string cifrado = HttpUtility.UrlEncode(cg.Encrypt(valor).Replace("+", "-").Replace("/", "_").Replace("=", ""));

                        HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                        btnEditar.Attributes.Add("href", "negociarconvenio?editid=" + cifrado);
                        btnEditar.Visible = true;
                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        string valor = ((DataRowView)e.Item.DataItem).Row[0].ToString();
                        string cifrado = HttpUtility.UrlEncode(cg.Encrypt(valor).Replace("+", "-").Replace("/", "_").Replace("=", ""));

                        HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                        btnEliminar.Attributes.Add("href", "negociarconvenio?deleteid=" + cifrado);
                        btnEliminar.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }

        }
    }
}