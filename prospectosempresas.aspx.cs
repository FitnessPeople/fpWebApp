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
    public partial class prospectosempresas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Prospectos empresas");
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
                    CargarTipoDocumento();
                    ListaProspectosEmpresas();
                    CargarCiudad();

                    ltTitulo.Text = "Agregar prospecto empresa";
                    if (Request.QueryString.Count > 0)
                    {
                        rpEmpresasCRM.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = new DataTable();
                            dt = cg.ConsultarEstadoCRMPorID(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                //txbNombreEstado.Text = dt.Rows[0]["NombreEstadoCRM"].ToString();
                                //ListItem item = ddlColores.Items.FindByText(dt.Rows[0]["ColorEstadoCRM"].ToString());
                                //if (item != null) ddlColores.SelectedIndex = ddlColores.Items.IndexOf(item);

                                //ListItem itemI = ddlIconos.Items.FindByText(dt.Rows[0]["ColorEstadoCRM"].ToString());
                                //if (itemI != null) ddlIconos.SelectedIndex = ddlIconos.Items.IndexOf(item);


                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar Estado CRM";
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ValidarCiudadSedesTablas(Request.QueryString["deleteid"].ToString());
                            if (dt.Rows.Count > 0)
                            {
                                ltMensaje.Text = "<div class=\"ibox-content\">" +
                                    "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                    "Este estado no se puede borrar, hay registros asociados a él." +
                                    "</div></div>";

                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarCiudadSedePorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt.Rows.Count > 0)
                                {
                                    //txbNombreEstado.Text = dt1.Rows[0]["NombreCiudadSede"].ToString();
                                    //txbNombreEstado.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    btnAgregar.Enabled = false;
                                    ltTitulo.Text = "Borrar Estado CRM";
                                }
                                dt1.Dispose();
                            }
                            else
                            {
                                //Borrar
                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarCiudadSedePorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    //txbNombreEstado.Text = dt1.Rows[0]["NombreCiudadSede"].ToString();
                                    //txbNombreEstado.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    ltTitulo.Text = "Borrar Estado CRM";
                                }
                                dt1.Dispose();
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

        private void CargarCiudad()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCiudadesCol();

            ddlCiudades.DataSource = dt;
            ddlCiudades.DataBind();

            dt.Dispose();
        }

        private bool ValidarEstado(string strNombre)
        {
            bool bExiste = false;
            DataTable dt = new DataTable();
            clasesglobales cg = new clasesglobales();
            dt = cg.ConsultarEstadoCRMPorNombre(strNombre);
            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }
            return bExiste;
        }

        private void ValidarPermisos(string strPagina)
        {
            ViewState["SinPermiso"] = "1";
            ViewState["Consulta"] = "0";
            ViewState["Exportar"] = "0";
            ViewState["CrearModificar"] = "0";
            ViewState["Borrar"] = "0";

            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ValidarPermisos(strPagina, Session["idPerfil"].ToString(), Session["idusuario"].ToString());

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
        private void CargarTipoDocumento()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultartiposDocumento();

            DataView dv = new DataView(dt);
            dv.RowFilter = "idTipoDoc = 7";

            ddlTipoDocumento.DataSource = dv;
            ddlTipoDocumento.DataBind();

            dt.Dispose();
        }



        private void ListaProspectosEmpresas()
        {
            try
            {
                DataTable dt = new DataTable();
                clasesglobales cg = new clasesglobales();
                dt = cg.ConsultarEmpresasCRM();
                rpEmpresasCRM.DataSource = dt;
                rpEmpresasCRM.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }

        }


        protected void rpEstadosCRM_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "estadoscrm?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "estadoscrm?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            bool salida = false;
            string mensaje = string.Empty;

            if (Request.QueryString.Count > 0)
            {
                string strInitData = TraerData();

                if (Request.QueryString["editid"] != null)
                {
                    string respuesta = cg.ActualizarCiudadSede(int.Parse(Request.QueryString["editid"].ToString()), "");

                    string strNewData = TraerData();
                    cg.InsertarLog(Session["idusuario"].ToString(), "prospecto empresa", "Modifica", "El usuario modificó  el prospecto empresa: " + "" + ".", strInitData, strNewData);
                }

                if (Request.QueryString["deleteid"] != null)
                {
                    string respuesta = cg.EliminarCiudadSede(int.Parse(Request.QueryString["deleteid"].ToString()));
                }
                Response.Redirect("estadoscrm");
            }
            else
            {
                //if (!ValidarEstado(""))
                //{

                    ////string iconoMin = ddlIconos.SelectedItem.Value;
                    //string htmlIconoMin = $"<i class=\"{iconoMin}\"></i>";

                    //string iconoMax = iconoMin.Contains("fa-5x") ? iconoMin : iconoMin + " fa-5x";
                    //string htmlIconoMax = $"<i class=\"{iconoMax}\"></i>";

                    try
                    {
                        string respuesta = cg.InsertarEmpresaCRM(txbNombreEmpresa.Value , Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value.ToString()),
                            txbDocumento.Text, txbCelularEmpresa.Value, txbCorreoEmpresa.Value, Convert.ToInt32(ddlCiudades.SelectedItem.Value.ToString()),
                            txaObservaciones.Value, Convert.ToInt32(Session["idUsuario".ToString()]),  out salida, out mensaje);

                        cg.InsertarLog(Session["idusuario"].ToString(), "prospectos empresas", "Agrega", "El usuario agregó un nuevo prospecto empresa crm: " + "" + ".", "", "");

                        if (salida)
                        {
                            string script = @"
                                Swal.fire({
                                    title: 'Empresa prospecto credada correctamente!',
                                    text: '" + mensaje.Replace("'", "\\'") + @"',
                                    icon: 'success',
                                    timer: 3000, // 3 segundos
                                    showConfirmButton: false,
                                    timerProgressBar: true
                                }).then(() => {
                                    window.location.href = 'prospectosempresas';
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
                                  window.location.href = 'prospectosempresas';
                                }
                            });
                        ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        string script = @"
                        Swal.fire({
                        title: 'Error',
                        text: 'Ha ocurrido un error inesperado." + ex.Message.ToString() + @"',
                        icon: 'error'
                    });
                    ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                    }
                    // Response.Redirect("estadoscrm");
                //}
                //else
                //{
                //    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                //    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                //    "Ya existe un prospecto empresa con ese nombre." +
                //    "</div>";
                //}
            }
        }
        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string consultaSQL = @"SELECT *
		                               FROM empresascrm
		                               ORDER BY NombreEmpresaCRM;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
                string nombreArchivo = $"CiudadesSedes_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

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
            DataTable dt = cg.ConsultarCiudadSedePorId(int.Parse(Request.QueryString["editid"].ToString()));

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();

            return strData;
        }

        protected void rpEmpresasCRM_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}