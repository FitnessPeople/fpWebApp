﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class tablasbd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Tablas BD");
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
                            lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {

                        }
                    }
                    listaTablasBD();
                    //ActualizarEstadoxFechaFinal();
                    //indicadores01.Visible = false;
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

        private void listaTablasBD()
        {
            string strQuery = "SELECT * FROM information_schema.TABLES WHERE TABLE_SCHEMA = 'fitnesspeople'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
            rpProcedimientos.DataSource = dt;
            rpProcedimientos.DataBind();
            dt.Dispose();
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string consultaSQL = @"SELECT * FROM information_schema.TABLES WHERE TABLE_SCHEMA = 'fitnesspeople'";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
                string nombreArchivo = $"TablasBD_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

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

        protected void rpProcedimientos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlButton btnDetalles = (HtmlButton)e.Item.FindControl("btnDetalles");
                btnDetalles.Attributes.Add("data-toggle", "modal");
                btnDetalles.Attributes.Add("data-target", "#myModal" + ((DataRowView)e.Item.DataItem).Row[2].ToString());
                btnDetalles.Visible = true;

                string strQuery = "DESCRIBE " + ((DataRowView)e.Item.DataItem).Row[2].ToString();
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);

                Literal ltModales = (Literal)e.Item.FindControl("ltModales");
                ltModales.Text += "<div class=\"modal inmodal\" id=\"myModal" + ((DataRowView)e.Item.DataItem).Row[2].ToString() + "\" tabindex=\"-1\" role=\"dialog\" aria-hidden=\"true\">";
                ltModales.Text += "<div class=\"modal-dialog modal-lg\">";
                ltModales.Text += "<div class=\"modal-content animated bounceInRight\">";

                ltModales.Text += "<div class=\"modal-header\">";
                ltModales.Text += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\"><span aria-hidden=\"true\">&times;</span><span class=\"sr-only\">Cerrar</span></button>";
                ltModales.Text += "<i class=\"fa fa-id-card modal-icon\" style=\"color: #1C84C6;\"></i>";
                ltModales.Text += "<h4 class=\"modal-title\">Datos de la tabla " + ((DataRowView)e.Item.DataItem).Row[2].ToString() + "</h4>";
                ltModales.Text += "</div>";

                ltModales.Text += "<div class=\"modal-body\">";

                ltModales.Text += "<table class=\"table table-striped\">";
                ltModales.Text += "<tr>";
                ltModales.Text += "<td class=\"small\"><b>Campo</b>";
                ltModales.Text += "</td>";
                ltModales.Text += "<td class=\"small\"><b>Tipo</b>";
                ltModales.Text += "</td>";
                ltModales.Text += "</tr>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ltModales.Text += "<tr>";
                    ltModales.Text += "<td class=\"small\">" + dt.Rows[i]["Field"].ToString();
                    ltModales.Text += "</td>";
                    ltModales.Text += "<td class=\"small\">" + dt.Rows[i]["Type"].ToString();
                    ltModales.Text += "</td>";
                    ltModales.Text += "</tr>";
                }

                ltModales.Text += "</table>";

                ltModales.Text += "</div>";

                ltModales.Text += "</div>";
                ltModales.Text += "</div>";
                ltModales.Text += "</div>";
            }
        }
    }
}