using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class crmvista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Historias clinicas");

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
                        //btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            //lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            //lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            //btnAgregar.Visible = true;
                        }
                    }

                    clasesglobales cg = new clasesglobales();
                    DataTable dt = cg.ConsultarContactosCMR();
                    txbFechaPrim.Attributes.Add("type", "date");
                    txbFechaPrim.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                    txbFechaProx.Attributes.Add("type", "date");
                    txbFechaProx.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                    int valorPropuesta = Convert.ToInt32("0");
                    txbValorPropuesta.Text = valorPropuesta.ToString("C0", new CultureInfo("es-CO"));
                    //ListaHistorias();
                    ListaContactos();

                    
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

        private void ListaHistorias()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarContactosCMR();

            rpContactosCMR.DataSource = dt;
            rpContactosCMR.DataBind();

            dt.Dispose();
        }

        private void ListaContactos()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarContactosCMR();

            rpContactosCMR.DataSource = dt;
            rpContactosCMR.DataBind();

            dt.Dispose();
        }

        protected void rpHistoriasClinicas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlButton btnEditar = (HtmlButton)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("onClick", "window.location.href='editarhistoria?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                    btnEditar.Visible = true;

                    //HtmlButton btnAgregar = (HtmlButton)e.Item.FindControl("btnAgregar");
                    //btnAgregar.Attributes.Add("onClick", "window.location.href='agregarcontrol?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                    //btnAgregar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlButton btnEliminar = (HtmlButton)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("onClick", "window.location.href='eliminarhistoria?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {

        }
    }
}