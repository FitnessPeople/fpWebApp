using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class embajadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Empleados");
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
                    listaEmbajadores();
                    CargarTipoDocumento();

                    ltTitulo.Text = "Agregar sede";

                    if (Request.QueryString.Count > 0)
                    {
                        rpEmbajadores.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            string strQuery = "SELECT * FROM Embajadores WHERE idEmbajador = " + Request.QueryString["editid"].ToString();
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.TraerDatos(strQuery);
                            if (dt.Rows.Count > 0)
                            {
                                txbNombre.Text = dt.Rows[0]["NombreEmb"].ToString();
                                txbCodigo.Text = dt.Rows[0]["CodigoEmb"].ToString();
                                ddlTipoDocumento.SelectedIndex = Convert.ToInt16(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt.Rows[0]["idTipoDocumento"].ToString())));
                                txbDocumento.Text = dt.Rows[0]["NroDocumentoEmb"].ToString();
                                txbCelular.Text = dt.Rows[0]["CelularEmb"].ToString();
                                txbInstagram.Text = dt.Rows[0]["InstagramEmb"].ToString();
                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar embajador";
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

        private void listaEmbajadores()
        {
            string strQuery = "SELECT *, " +
                "IF(EstadoEmb = 'Activo','success','danger') AS Status " +
                "FROM Embajadores";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
            rpEmbajadores.DataSource = dt;
            rpEmbajadores.DataBind();
            dt.Dispose();
        }

        private void CargarTipoDocumento()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultartiposDocumento();

            ddlTipoDocumento.DataSource = dt;
            ddlTipoDocumento.DataBind();

            dt.Dispose();
        }

        protected void rpEmbajadores_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor cambiaestado = (HtmlAnchor)e.Item.FindControl("cambiaestado");
                    cambiaestado.Attributes.Add("href", "cambiaestadoembajador?id=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    cambiaestado.Visible = true;
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "embajadores?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "embajadores?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();

            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editid"] != null)
                {
                    string strInitData = TraerData();
                    try
                    {
                        string strQuery = "UPDATE Embajadores SET " +
                            "idTipoDocumento = " + ddlTipoDocumento.SelectedItem.Value.ToString() + ", " +
                            "NroDocumentoEmb = '" + txbDocumento.Text.ToString() + "', " +
                            "NombreEmb = '" + txbNombre.Text.ToString() + "', " +
                            "CelularEmb = '" + txbCelular.Text.ToString() + "', " +
                            "CodigoEmb = '" + txbCodigo.Text.ToString() + "', " +
                            "InstagramEmb = '" + txbInstagram.Text.ToString() + "' " +
                            "WHERE idEmbajador = " + Request.QueryString["editid"].ToString();
                        //string respuesta = cg.ActualizarSede(int.Parse(Request.QueryString["editid"].ToString()), txbSede.Text.ToString().Trim(), txbDireccion.Text.ToString().Trim(), int.Parse(ddlCiudadSede.SelectedItem.Value.ToString()), txbTelefono.Text.ToString().Trim(), contenidoEditor, rblTipoSede.SelectedValue.ToString(), rblClaseSede.SelectedValue.ToString());
                        string respuesta = cg.TraerDatosStr(strQuery);
                        string strNewData = TraerData();

                        cg.InsertarLog(Session["idusuario"].ToString(), "embajadores", "Modifica", "El usuario modificó datos al embajador " + txbNombre.Text.ToString() + ".", strInitData, strNewData);
                    }
                    catch (Exception ex)
                    {
                        string mensaje = ex.Message;
                    }

                    Response.Redirect("embajadores");
                }
            }
            else
            {
                if (!ValidarDocumento(txbDocumento.Text.ToString()))
                {
                    try
                    {
                        //string respuesta = cg.InsertarSede(txbSede.Text.ToString().Trim(), txbDireccion.Text.ToString().Trim(), int.Parse(ddlCiudadSede.SelectedItem.Value.ToString()), txbTelefono.Text.ToString().Trim(), contenidoEditor, "", rblTipoSede.SelectedValue.ToString(), rblClaseSede.SelectedValue.ToString());
                        string strQuery = "INSERT INTO Embajadores (idTipoDocumento, NroDocumentoEmb, NombreEmb, " +
                            "CelularEmb, CodigoEmb, InstagramEmb, EstadoEmb) " +
                            "VALUES (" + ddlTipoDocumento.SelectedItem.Value.ToString() + ", " +
                            "'" + txbDocumento.Text.ToString() + "', " +
                            "'" + txbNombre.Text.ToString() + "', " +
                            "'" + txbCelular.Text.ToString() + "', " +
                            "'" + txbCodigo.Text.ToString() + "', " +
                            "'" + txbInstagram.Text.ToString() + "', " +
                            "'Activo') ";
                        string respuesta = cg.TraerDatosStr(strQuery);
                        cg.InsertarLog(Session["idusuario"].ToString(), "embajadores", "Agrega", "El usuario agregó un nuevo embajador: " + txbNombre.Text.ToString() + ".", "", "");
                    }
                    catch (Exception ex)
                    {
                        string mensaje = ex.Message;
                    }

                    Response.Redirect("embajadores");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Ya existe un embajador con ese documento." +
                        "</div>";
                }
            }
        }

        private bool ValidarDocumento(string strDocumento)
        {
            bool bExiste = false;
            string strQuery = "SELECT * FROM Embajadores WHERE NroDocumentoEmb = " + strDocumento;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }

            return bExiste;
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
    }
}