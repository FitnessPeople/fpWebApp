using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace fpWebApp
{
    public partial class gympass : System.Web.UI.Page
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
                            lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            //btnAgregar.Visible = true;
                        }
                    }
                    listaInscritos();
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

        private void listaInscritos()
        {
            string strQuery = "SELECT * FROM GymPass";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
            rpInscritos.DataSource = dt;
            rpInscritos.DataBind();
            dt.Dispose();
        }

        //protected void rpInscritos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        if (ViewState["CrearModificar"].ToString() == "1")
        //        {
        //            HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
        //            btnEditar.Attributes.Add("href", "editarempleado?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
        //            btnEditar.Visible = false;
        //        }
        //        if (ViewState["Borrar"].ToString() == "1")
        //        {
        //            HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
        //            btnEliminar.Attributes.Add("href", "eliminarempleado?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
        //            btnEliminar.Visible = false;
        //        }
        //    }
        //}

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string consultaSQL = @"SELECT idGymPass AS 'ID', Nombres AS 'Nombres', Apellidos AS 'Apellidos', 
                    Email AS 'Email', Celular AS 'Celular', NroDocumento AS 'Nro de Documento', Ciudad AS 'Ciudad', 
                    Sede AS 'Sede', FechaAsistencia AS 'Fecha Asistencia', FechaInscripcion AS 'Fecha Inscripción' 
                    FROM GymPass 
                    ORDER BY FechaInscripcion DESC;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
                string nombreArchivo = $"Inscritos_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    cg.ExportarExcel(dt, nombreArchivo);
                }
                else
                {
                    Response.Write("<script>alert('No existen registros para esta consulta');</script>");
                    //Response.Redirect("gympass.aspx?mensaje=No existen registros para esta consulta");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
                //Response.Redirect("gympass.aspx?mensaje=" + Server.UrlEncode(ex.Message));
            }
        }
    }
}