using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace fpWebApp
{
    public partial class planes : System.Web.UI.Page
    {
        OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    if (Session["idUsuario"] != null)
                    {
                        ValidarPermisos("Planes");
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
                        ListaPlanes();
                        ltTitulo.Text = "Agregar un plan";
                        txbPrecio.Attributes.Add("type", "number");
                        txbDiasCongelamiento.Attributes.Add("type", "number");
                        txbDiasCongelamiento.Attributes.Add("step", "0.1");
                        txbFechaInicio.Attributes.Add("type", "date");
                        txbFechaFinal.Attributes.Add("type", "date");

                        if (Request.QueryString.Count > 0)
                        {
                            rpPlanes.Visible = false;
                            if (Request.QueryString["editid"] != null)
                            {
                                //Editar
                                clasesglobales cg = new clasesglobales();
                                DataTable dt = cg.ConsultarPlanPorId(int.Parse(Request.QueryString["editid"].ToString()));
                                if (dt.Rows.Count > 0)
                                {
                                    txbPlan.Text = dt.Rows[0]["NombrePlan"].ToString();
                                    txbDescripcion.Text = dt.Rows[0]["DescripcionPlan"].ToString();
                                    txbPrecio.Text = dt.Rows[0]["PrecioBase"].ToString();
                                    txbDiasCongelamiento.Text = dt.Rows[0]["DiasCongelamientoMes"].ToString().Replace(',','.');
                                    txbFechaInicio.Text = dt.Rows[0]["FechaInicial"].ToString();
                                    txbFechaFinal.Text = dt.Rows[0]["FechaFinal"].ToString();
                                    btnAgregar.Text = "Actualizar";
                                    ltTitulo.Text = "Actualizar Plan";
                                }
                            }
                            if (Request.QueryString["deleteid"] != null)
                            {
                                clasesglobales cg = new clasesglobales();
                                DataTable dt = cg.ValidarPlanAfiliados(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt.Rows.Count > 0)
                                {
                                    ltMensaje.Text = "<div class=\"ibox-content\">" +
                                        "<div class=\"alert alert-danger alert-dismissable\">" +
                                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                        "Este Plan no se puede borrar, hay afiliados asociados a éste." +
                                        "</div></div>";

                                    DataTable dt1 = new DataTable();
                                    dt1 = cg.ConsultarPlanPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                    if (dt1.Rows.Count > 0)
                                    {
                                        txbPlan.Text = dt1.Rows[0]["NombrePlan"].ToString();
                                        txbDescripcion.Text = dt1.Rows[0]["DescripcionPlan"].ToString();
                                        txbPrecio.Text = dt1.Rows[0]["PrecioBase"].ToString();
                                        txbDiasCongelamiento.Text = dt1.Rows[0]["DiasCongelamientoMes"].ToString().Replace(',', '.');
                                        txbFechaInicio.Text = dt1.Rows[0]["FechaInicial"].ToString();
                                        txbFechaFinal.Text = dt1.Rows[0]["FechaFinal"].ToString();
                                        txbPlan.Enabled = false;
                                        txbDescripcion.Enabled = false;
                                        txbPrecio.Enabled = false;
                                        txbDiasCongelamiento.Enabled = false;
                                        txbFechaInicio.Enabled = false;
                                        txbFechaFinal.Enabled = false;
                                        btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                        btnAgregar.Enabled = false;
                                        ltTitulo.Text = "Borrar Plan";
                                    }
                                    dt1.Dispose();
                                }
                                else
                                {
                                    //Borrar
                                    DataTable dt1 = new DataTable();
                                    dt1 = cg.ConsultarPlanPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                    if (dt1.Rows.Count > 0)
                                    {
                                        txbPlan.Text = dt1.Rows[0]["NombrePlan"].ToString();
                                        txbDescripcion.Text = dt1.Rows[0]["DescripcionPlan"].ToString();
                                        txbPrecio.Text = dt1.Rows[0]["PrecioBase"].ToString();
                                        txbDiasCongelamiento.Text = dt1.Rows[0]["DiasCongelamientoMes"].ToString().Replace(',', '.');
                                        txbFechaInicio.Text = dt1.Rows[0]["FechaInicial"].ToString();
                                        txbFechaFinal.Text = dt1.Rows[0]["FechaFinal"].ToString();
                                        txbPlan.Enabled = false;
                                        txbDescripcion.Enabled = false;
                                        txbPrecio.Enabled = false;
                                        txbDiasCongelamiento.Enabled = false;
                                        txbFechaInicio.Enabled = false;
                                        txbFechaFinal.Enabled = false;
                                        btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                        ltTitulo.Text = "Borrar Plan";
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

        private void ListaPlanes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanes();
            rpPlanes.DataSource = dt;
            rpPlanes.DataBind();
            dt.Dispose();
        }

        protected void rpPlanes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "planes?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "planes?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editid"] != null)
                {
                    myConnection.Open();
                    string strQuery = "UPDATE Planes " +
                        "SET NombrePlan = '" + txbPlan.Text.ToString().Replace("'", "") + "', " +
                        "DescripcionPlan = '" + txbDescripcion.Text.ToString().Replace("'", "") + "', " +
                        "PrecioBase = " + txbPrecio.Text.ToString() + ", " +
                        "DiasCongelamientoMes = " + txbDiasCongelamiento.Text.ToString() + ", " +
                        "WHERE idPlan = " + Request.QueryString["editid"].ToString();
                    OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    myConnection.Close();

                    Response.Redirect("planes");
                }
            }
            else
            {
                myConnection.Open();
                string strQuery = "INSERT INTO Planes " +
                    "(NombrePlan, DescripcionPlan, Preciobase, EstadoPlan, idUsuario) " +
                    "VALUES ('" + txbPlan.Text.ToString().Replace("'", "") + "', '" + txbDescripcion.Text.ToString().Replace("'", "") + "', " +
                    "" + txbPrecio.Text.ToString() + ", 1, " + Session["idUsuario"].ToString() + ") ";
                OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                command1.ExecuteNonQuery();
                command1.Dispose();
                myConnection.Close();

                Response.Redirect("planes");   
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("planes");
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {

        }
    }
}