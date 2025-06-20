using NPOI.OpenXmlFormats.Dml.Chart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class planesweb : System.Web.UI.Page
    {
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
                                btnAgregar.Visible = false;
                            }
                        }
                        ListaPlanes();
                        ltTitulo.Text = "Actualizar información web de los planes";

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
                                    txbTituloPlan.Text = dt.Rows[0]["TituloPlan"].ToString();
                                    txbDescripcion.Text = dt.Rows[0]["DescripcionPlanWeb"].ToString();
                                    btnAgregar.Text = "Actualizar";
                                    btnAgregar.Visible = true;
                                    ltTitulo.Text = "Actualizar Plan";

                                    if (dt.Rows[0]["BannerWeb"].ToString() != "")
                                    {
                                        ltBanner.Text = "<img src=\"img/banners/" + dt.Rows[0]["BannerWeb"].ToString() + "\" class=\"img responsive\" />";
                                    }
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
                                        txbTituloPlan.Text = dt1.Rows[0]["TituloPlan"].ToString();
                                        txbDescripcion.Text = dt1.Rows[0]["DescripcionPlan"].ToString();
                                        txbDescripcion.Enabled = false;
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
                                        txbTituloPlan.Text = dt1.Rows[0]["TituloPlan"].ToString();
                                        txbDescripcion.Text = dt1.Rows[0]["DescripcionPlan"].ToString();
                                        txbDescripcion.Enabled = false;
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

        /// <summary>
        /// Valida los permisos del usuario en la pagina visitada.
        /// Contiene un parametro.
        /// </summary>
        /// <param name="strPagina"></param>
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

        /// <summary>
        /// Lista todos los planes 
        /// </summary>
        private void ListaPlanes()
        {
            clasesglobales cg = new clasesglobales();
            //DataTable dt = cg.ConsultarPlanes();
            string strQuery = "SELECT *, IF(pm.EstadoPlan='Activo','primary','danger') AS label " +
                "FROM Planes pm " +
                "LEFT JOIN Usuarios u ON pm.idUsuario = u.idUsuario ";
            DataTable dt = cg.TraerDatos(strQuery);
            rpPlanes.DataSource = dt;

            if (!dt.Columns.Contains("TotalMeses"))
                dt.Columns.Add("TotalMeses", typeof(int));

            foreach (DataRow row in dt.Rows)
            {
                int meses = row["Meses"] != DBNull.Value ? Convert.ToInt32(row["Meses"]) : 0;
                int cortesia = row["MesesCortesia"] != DBNull.Value ? Convert.ToInt32(row["MesesCortesia"]) : 0;
                row["TotalMeses"] = meses + cortesia;
            }

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
                    btnEditar.Attributes.Add("href", "planesweb?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
            }
        }

        /// <summary>
        /// Valida si un plan ya existe con ese mismo nombre
        /// </summary>
        /// <param name="strNombre"></param>
        /// <returns>Devuelve 'true' si existe o 'false' si no existe.</returns>
        private bool ValidarPlan(string strNombre)
        {
            bool bExiste = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanPorNombre(strNombre);
            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }
            dt.Dispose();
            return bExiste;
        }

        /// <summary>
        /// Agrega un nuevo plan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            if (Request.QueryString.Count > 0)
            {
                string strInitData = TraerData();

                string strFilenameBanner = "";
                HttpPostedFile postedFileBanner = Request.Files["fileBanner"];

                if (postedFileBanner != null && postedFileBanner.ContentLength > 0)
                {
                    //Save the File.
                    string filePath = Server.MapPath("~//img//banners//") + Path.GetFileName(postedFileBanner.FileName);
                    postedFileBanner.SaveAs(filePath);
                    strFilenameBanner = postedFileBanner.FileName;
                }

                string strFilenameImagen = "";
                HttpPostedFile postedFileImagen = Request.Files["fileImagen"];

                if (postedFileImagen != null && postedFileImagen.ContentLength > 0)
                {
                    //Save the File.
                    string filePath = Server.MapPath("~//img//") + Path.GetFileName(postedFileImagen.FileName);
                    postedFileImagen.SaveAs(filePath);
                    strFilenameImagen = postedFileImagen.FileName;
                }

                if (Request.QueryString["editid"] != null)
                {
                    string respuesta = cg.ActualizarPlanWeb(int.Parse(Request.QueryString["editid"].ToString()),
                        txbTituloPlan.Text.ToString().Trim(),
                        txbDescripcion.Text.ToString(), "", "", "");

                    string strNewData = TraerData();

                    cg.InsertarLog(Session["idusuario"].ToString(), "planes", "Modifica", "El usuario modificó el plan: " + txbTituloPlan.Text.ToString() + ".", strInitData, strNewData);
                }
                if (Request.QueryString["deleteid"] != null)
                {
                    string respuesta = cg.EliminarPlan(int.Parse(Request.QueryString["deleteid"].ToString()));
                }
                Response.Redirect("planes");
            }
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string consultaSQL = @"SELECT NombrePlan AS 'Nombre de Plan', 
                    DescripcionPlan AS 'Descripción', 
                    PrecioBase AS 'Precio Base', 
                    PrecioTotal AS 'Precio Total', 
                    EstadoPlan AS 'Estado', 
                    Meses AS 'Meses', 
                    DiasCongelamientoMes AS 'Cantidad de Días de Congelamiento', 
                    FechaInicial AS 'Fecha de Inicio', 
                    FechaFinal AS 'Fecha de Terminación', 
                    NombreUsuario AS 'Nombre de Usuario Creador', 
                    EmailUsuario AS 'Correo de Usuario Creador'
                    FROM Planes p 
                    LEFT JOIN Usuarios u ON p.idusuario = u.idUsuario 
                    ORDER BY NombrePlan;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
                string nombreArchivo = $"Planes_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

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
            DataTable dt = cg.ConsultarPlanPorId(int.Parse(Request.QueryString["editid"].ToString()));

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