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
    public partial class estrategiasmarketing : System.Web.UI.Page
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

                    listaEstrategias();
                    CargartiposEstrategias();
                    //listaCiudades();
                    ltTitulo.Text = "Agregar estrategia";

                    if (Request.QueryString.Count > 0)
                    {
                        rpEstrategias.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarSedePorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                string contenidoEditor = hiddenEditor.Value;
                                txbSede.Text = dt.Rows[0]["NombreSede"].ToString();
                                //txbDireccion.Text = dt.Rows[0]["DireccionSede"].ToString();
                               // ddlCiudadSede.SelectedIndex = Convert.ToInt16(ddlCiudadSede.Items.IndexOf(ddlCiudadSede.Items.FindByValue(dt.Rows[0]["idCiudadSede"].ToString())));
                                //txbTelefono.Text = dt.Rows[0]["TelefonoSede"].ToString();
                                hiddenEditor.Value = dt.Rows[0]["HorarioSede"].ToString();
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

        private void listaEstrategias()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstrategiasMarketing();
            rpEstrategias.DataSource = dt;
            rpEstrategias.DataBind();

            dt.Dispose();
        }

        private void CargarPlanes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanesVigencias();

            //ddlPlanes.DataSource = dt;
            //ddlPlanes.DataBind();
            dt.Dispose();
        }

        //private void listaCiudades()
        //{
        //    clasesglobales cg = new clasesglobales();
        //    DataTable dt = cg.ConsultarCiudadesSedes();
        //    ddlCiudadSede.DataSource = dt;
        //    ddlCiudadSede.DataBind();
        //    dt.Dispose();
        //}

        private void CargartiposEstrategias()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarTiposEstrategiasMarketing();
            ddlTipoEstrategias.DataSource = dt;
            ddlTipoEstrategias.DataBind();
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

        private bool ValidarSede(string strNombre)
        {
            bool bExiste = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarSedePorNombre(strNombre.ToString().Trim());

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

            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editid"] != null)
                {
                    //string strInitData = TraerData();
                    //try
                    //{
                    //    string respuesta = cg.ActualizarSede(int.Parse(Request.QueryString["editid"].ToString()), txbSede.Text.ToString().Trim(), txbDireccion.Text.ToString().Trim(), int.Parse(ddlCiudadSede.SelectedItem.Value.ToString()), txbTelefono.Text.ToString().Trim(), contenidoEditor, rblTipoSede.SelectedValue.ToString(), rblClaseSede.SelectedValue.ToString());
                    //    string strNewData = TraerData();

                    //    cg.InsertarLog(Session["idusuario"].ToString(), "sedes", "Modifica", "El usuario modificó datos a la sede " + txbSede.Text.ToString() + ".", strInitData, strNewData);
                    //}
                    //catch (Exception ex)
                    //{
                    //    string mensaje = ex.Message;
                    //}

                    //Response.Redirect("sedes");
                }
            }
            else
            {
                if (!ValidarSede(txbSede.Text.ToString()))
                {
                    //try
                    //{
                    //    string respuesta = cg.InsertarSede(txbSede.Text.ToString().Trim(), txbDireccion.Text.ToString().Trim(), int.Parse(ddlCiudadSede.SelectedItem.Value.ToString()), txbTelefono.Text.ToString().Trim(), contenidoEditor, "", rblTipoSede.SelectedValue.ToString(), rblClaseSede.SelectedValue.ToString());
                    //    cg.InsertarLog(Session["idusuario"].ToString(), "sedes", "Agrega", "El usuario agregó una nueva sede: " + txbSede.Text.ToString() + ".", "", "");
                    //}
                    //catch (Exception ex)
                    //{
                    //    string mensaje = ex.Message;
                    //}

                    //Response.Redirect("sedes");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Ya existe una sede con ese nombre." +
                        "</div>";
                }
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

        protected void rpSedes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "sedes?editid=" + ((DataRowView)e.Item.DataItem).Row["idSede"].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "sedes?deleteid=" + ((DataRowView)e.Item.DataItem).Row["idSede"].ToString());
                    btnEliminar.Visible = true;
                }
            }
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

        }
    }
}