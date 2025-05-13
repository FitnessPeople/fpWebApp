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
    public partial class crmnuevocontacto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Sedes");
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
                        txbFechaPrim.Attributes.Add("type", "date");
                        txbFechaPrim.Attributes.Add("min", DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"));
                        txbFechaPrim.Value = DateTime.Now.ToString("yyyy-MM-dd");
                        txbFechaProx.Attributes.Add("type", "date");
                        txbFechaProx.Value = DateTime.Now.ToString("yyyy-MM-dd");
                        txbCorreoContacto.Attributes.Add("type", "email");

                        divBotonesLista.Visible = false;
                        btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                    ListaEmpresasCRM();
                    ListaEstadosCRM();
                    ListaContactos();
                    ListaTiposAfiliadosCRM();
                    CargarPlanes();
                    ListaCanalesMarketingCRM();
                    ListaObjetivosfiliadoCRM();

                    ltTitulo.Text = "Contactos";
                    Literal1.Text = "Empresas";
                    if (Request.QueryString.Count > 0)
                    {
                        rpContactosCRM.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarSedePorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                //string contenidoEditor = hiddenEditor.Value;
                                //txbSede.Text = dt.Rows[0]["NombreSede"].ToString();
                                //txbDireccion.Text = dt.Rows[0]["DireccionSede"].ToString();
                                //ddlCiudadSede.SelectedIndex = Convert.ToInt16(ddlCiudadSede.Items.IndexOf(ddlCiudadSede.Items.FindByValue(dt.Rows[0]["idCiudadSede"].ToString())));
                                //txbTelefono.Text = dt.Rows[0]["TelefonoSede"].ToString();
                                //hiddenEditor.Value = dt.Rows[0]["HorarioSede"].ToString();
                                //rblTipoSede.SelectedValue = dt.Rows[0]["TipoSede"].ToString();
                                //rblClaseSede.SelectedValue = dt.Rows[0]["ClaseSede"].ToString();
                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar sede";
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            //Borrar
                        }
                    }
                }
                else
                {
                    Response.Redirect("logout");
                }
            }
        }


        private void ListaContactos()
        {
            decimal valorTotal = 0;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarContactosCRM(out valorTotal);

            rpContactosCRM.DataSource = dt;
            rpContactosCRM.DataBind();

            //ltValorTotal.Text = valorTotal.ToString("C0");
            dt.Dispose();
        }
        private void ListaEmpresasCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEmpresasCRM();

            ddlEmpresa.DataSource = dt;
            ddlEmpresa.DataBind();
            dt.Dispose();
        }

        private void ListaEstadosCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstadossCRM();

            ddlStatusLead.DataSource = dt;
            ddlStatusLead.DataBind();
            dt.Dispose();
        }

        private void ListaTiposAfiliadosCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarTipoAfiliadCRM();

            ddlTiposAfiliado.DataSource = dt;
            ddlTiposAfiliado.DataBind();
            dt.Dispose();
        }

        private void CargarPlanes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanes();

            ddlPlanes.DataSource = dt;
            ddlPlanes.DataBind();
            dt.Dispose();
        }

        private void ListaObjetivosfiliadoCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarObjetivosAfiliados();

            ddlObjetivos.DataSource = dt;
            ddlObjetivos.DataBind();
            dt.Dispose();
        }

        private void ListaCanalesMarketingCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCanalesMarketingCRM();

            ddlCanalesMarketing.DataSource = dt;
            ddlCanalesMarketing.DataBind();
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



        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            //string contenidoEditor = hiddenEditor.Value;

            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editid"] != null)
                {
                    string strInitData = TraerData();
                    try
                    {
                        //string respuesta = cg.ActualizarSede(int.Parse(Request.QueryString["editid"].ToString()), txbSede.Text.ToString().Trim(), txbDireccion.Text.ToString().Trim(), int.Parse(ddlCiudadSede.SelectedItem.Value.ToString()), txbTelefono.Text.ToString().Trim(), contenidoEditor, rblTipoSede.SelectedValue.ToString(), rblClaseSede.SelectedValue.ToString());
                        string strNewData = TraerData();

                        //cg.InsertarLog(Session["idusuario"].ToString(), "sedes", "Modifica", "El usuario modificó datos a la sede " + txbSede.Text.ToString() + ".", strInitData, strNewData);
                    }
                    catch (Exception ex)
                    {
                        string mensaje = ex.Message;
                    }

                    Response.Redirect("sedes");
                }
            }
            else
            {
                //    if (!ValidarSede(txbSede.Text.ToString()))
                //    {
                //        try
                //        {
                //            string respuesta = cg.InsertarSede(txbSede.Text.ToString().Trim(), txbDireccion.Text.ToString().Trim(), int.Parse(ddlCiudadSede.SelectedItem.Value.ToString()), txbTelefono.Text.ToString().Trim(), contenidoEditor, "", rblTipoSede.SelectedValue.ToString(), rblClaseSede.SelectedValue.ToString());
                //            cg.InsertarLog(Session["idusuario"].ToString(), "sedes", "Agrega", "El usuario agregó una nueva sede: " + txbSede.Text.ToString() + ".", "", "");
                //        }
                //        catch (Exception ex)
                //        {
                //            string mensaje = ex.Message;
                //        }

                //        Response.Redirect("sedes");
                //    }
                //    else
                //    {
                //        ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                //            "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                //            "Ya existe una sede con ese nombre." +
                //            "</div>";
                //    }
            }
        }

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarSedePorId(int.Parse(Request.QueryString["editid"].ToString()));

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();
            return strData;
        }

        protected void rblClaseSede_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        protected void rpContactosCRM_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "sedes?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "sedes?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }


        protected void ddlPlanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPlanes.SelectedValue == "0")
            {
                txbValorPropuesta.Text = "Por favor selecciona un plan válido.";
                ViewState["precioBase"] = null;
                ViewState["descuentoMensual"] = null;
                return;
            }

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanes();
            var fila = dt.Select("IdPlan = " + ddlPlanes.SelectedValue);
            if (fila.Length > 0)
            {
                ViewState["precioBase"] = fila[0]["PrecioBase"];
                ViewState["descuentoMensual"] = fila[0]["DescuentoMensual"];
            }

            // Verificar si ya hay mes seleccionado
            if (!string.IsNullOrEmpty(rblMesesPlan.SelectedValue))
            {
                CalcularPropuesta();
            }
            else
            {
                txbValorPropuesta.Text = "Primero selecciona los meses del plan.";
            }
        }


        protected void rblMesesPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar si ya hay un plan seleccionado
            if (ViewState["precioBase"] == null || ViewState["descuentoMensual"] == null)
            {
                txbValorPropuesta.Text = "Primero selecciona un plan.";
                return;
            }

            CalcularPropuesta();
        }

        private void CalcularPropuesta()
        {
            if (ViewState["precioBase"] == null || ViewState["descuentoMensual"] == null || string.IsNullOrEmpty(rblMesesPlan.SelectedValue))
            {
                txbValorPropuesta.Text = "Faltan datos para calcular.";
                return;
            }

            if (ddlPlanes.SelectedValue == "0")
            {
                txbValorPropuesta.Text = "Debes seleccionar un plan válido.";
                return;
            }

            int precioBase = Convert.ToInt32(ViewState["precioBase"]);
            double descuentoMensual = Convert.ToDouble(ViewState["descuentoMensual"]);
            int meses = Convert.ToInt32(rblMesesPlan.SelectedValue);

            double descuento = (meses - 1) * descuentoMensual;
            double total = (precioBase - ((precioBase * descuento) / 100)) * meses;

            txbValorPropuesta.Text = $"${total:N0}";
        }



    }
}