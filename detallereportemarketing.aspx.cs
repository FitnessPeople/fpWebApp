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
    public partial class detallereportemarketing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Cargos");
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
                        //divBotonesLista.Visible = false;
                        //btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            //divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            //divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            //btnAgregar.Visible = true;
                        }
                    }
                    listaEstrategias();
                    //ListaCargos();
                    //ltTitulo.Text = "Agregar cargo";

                    if (Request.QueryString.Count > 0)
                    {
                        //rpCargos.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarCargosPorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                //txbNombreCargo.Text = dt.Rows[0]["NombreCargo"].ToString();
                                //btnAgregar.Text = "Actualizar";
                                //ltTitulo.Text = "Actualizar cargo";
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ValidarCargoTablas(int.Parse(Request.QueryString["deleteid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                //ltMensaje.Text = "<div class=\"ibox-content\">" +
                                //    "<div class=\"alert alert-danger alert-dismissable\">" +
                                //    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                //    "Este cargo no se puede borrar, hay registros asociados a él." +
                                //    "</div></div>";

                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarCargosPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    //txbNombreCargo.Text = dt1.Rows[0]["NombreCargo"].ToString();
                                    //txbNombreCargo.Enabled = false;
                                    //btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    //btnAgregar.Enabled = false;
                                    //ltTitulo.Text = "Borrar cargo";
                                }
                                dt1.Dispose();
                            }
                            else
                            {
                                //Borrar
                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarCargosPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    //txbNombreCargo.Text = dt1.Rows[0]["NombreCargo"].ToString();
                                    //txbNombreCargo.Enabled = false;
                                    //btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    //ltTitulo.Text = "Borrar cargo";
                                }
                                dt1.Dispose();
                            }
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


        private void listaEstrategias()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstrategiasMarketing();

            //foreach (DataRow row in dt.Rows)
            //{
            //    row["Planes"] = ConvertirIdsANombres(row["Planes"].ToString(), dicPlanes);
            //    row["CanalesVenta"] = ConvertirIdsANombres(row["CanalesVenta"].ToString(), dicCanales);
            //}

            //rpEstrategias.DataSource = dt;
            //rpEstrategias.DataBind();

            dt.Dispose();
        }


        private Dictionary<int, string> dicPlanes;
        private Dictionary<int, string> dicCanales;

        //private string ConvertirIdsANombres(string ids, Dictionary<int, string> diccionario)
        //{
        //    if (string.IsNullOrEmpty(ids)) return "";

        //    var nombres = ids.Split(',')
        //                     .Select(id => int.TryParse(id.Trim(), out int parsedId) && diccionario.ContainsKey(parsedId)
        //                         ? diccionario[parsedId]
        //                         : $"(ID {id})") // por si el id no existe
        //                     .ToList();

        //    return string.Join(", ", nombres);
        //}

        private void ListaCargos()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCargos();
            //rpCargos.DataSource = dt;
            //rpCargos.DataBind();
            dt.Dispose();
        }

        protected void rpCargos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "cargos?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "cargos?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }



        private bool ValidarCargos(string strNombre)
        {
            bool bExiste = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstadoCivilPorNombre(strNombre);
            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }
            dt.Dispose();
            return bExiste;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            if (Request.QueryString.Count > 0)
            {
                string strInitData = TraerData();

                if (Request.QueryString["editid"] != null)
                {
                    //string respuesta = cg.ActualizarCargo(int.Parse(Request.QueryString["editid"].ToString()), txbNombreCargo.Text.ToString().Trim());

                    //string strNewData = TraerData();
                    //cg.InsertarLog(Session["idusuario"].ToString(), "cargos empleado", "Modifica", "El usuario modificó datos del cargo de empleado: " + txbNombreCargo.Text.ToString() + ".", strInitData, strNewData);
                }
                if (Request.QueryString["deleteid"] != null)
                {
                    string respuesta = cg.EliminarCargo(int.Parse(Request.QueryString["deleteid"].ToString()));
                }
                Response.Redirect("cargos");
            }
            else
            {

            }
        }

        //protected void lbExportarExcel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string consultaSQL = @"SELECT NombreCargo AS 'Nombre de Cargos'
		      //                         FROM cargos
		      //                         ORDER BY NombreCargo;";

        //        clasesglobales cg = new clasesglobales();
        //        DataTable dt = cg.TraerDatos(consultaSQL);
        //        string nombreArchivo = $"CargosEmpleados_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

        //        if (dt.Rows.Count > 0)
        //        {
        //            cg.ExportarExcel(dt, nombreArchivo);
        //        }
        //        else
        //        {
        //            Response.Write("<script>alert('No existen registros para esta consulta');</script>");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
        //    }
        //}

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCargosPorId(int.Parse(Request.QueryString["editid"].ToString()));

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