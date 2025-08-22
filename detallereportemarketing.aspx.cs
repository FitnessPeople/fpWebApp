using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Cache;
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
                    ValidarPermisos("Reporte estrategias");
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
                    //ListaCargos();
                    //ltTitulo.Text = "Agregar cargo";

                    if (Request.QueryString.Count > 0)
                    {
                        if (Request.QueryString["idEstrategia"] != null)
                        {
                            Session["idEstrategia"] = Request.QueryString["idEstrategia"].ToString();
                            listaEstrategia(Convert.ToInt32(Session["idEstrategia"].ToString()));
                            ListaContactosPorUsuario();
                            CargarGraficaBarraCanalesVenta(Convert.ToInt32(Session["idEstrategia"].ToString()));
                            CargarGraficaPiePlanesEstrategia(Convert.ToInt32(Session["idEstrategia"].ToString()));
                        }


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


        private void listaEstrategia( int idEstrategia)
        {

            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarEstrategiaMarketingPorId(idEstrategia);
                DataTable dt1 = cg.ConsultarCuantosLeadsEstrategiaAceptados();

                DataTable dt2 = cg.ConsultarEstrategiaasMarketingEncabezado();

                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    DataRow[] rows = dt2.Select("IdEstrategia = " + idEstrategia);

                    if (rows.Length > 0)
                        ltEstadoEstrategia.Text = rows[0]["Estado"].ToString();
                    else
                        ltEstadoEstrategia.Text = "No encontrado";
                }

                ltNombreEstrategia.Text = dt2.Rows[0]["NombreEstrategia"].ToString();
                ltNombreUsuario.Text = dt.Rows[0]["NombreUsuario"].ToString();
                DataTable dt3 = cg.ConsultarEstrategiaMarketingCuantos(idEstrategia);

                if (dt3 != null && dt3.Rows.Count > 0)
                {
                    int eficienciaInt = (int)Math.Round(Convert.ToDecimal(dt3.Rows[0]["PorcentajeEfectividad"]));
                    ltEficiencia.Text = eficienciaInt.ToString();
                    progressEficiencia.Attributes["style"] = "width:" + eficienciaInt + "%;";
                }
                else
                {
                    ltEficiencia.Text = "0";
                    progressEficiencia.Attributes["style"] = "width:0%;";
                }

                DateTime fechaCreacion = Convert.ToDateTime(dt.Rows[0]["FechaCreacion"]);
                ltFechaCreacion.Text = fechaCreacion.ToString("dd.MM.yyyy");

                ltCantidadLeadsEstrategia.Text = dt3.Rows[0]["TotalContactos"].ToString();
                ltCantidadLeadsAprobados.Text = dt3.Rows[0]["ContactosConPagoAprobado"].ToString();

                DateTime fechaInicio = Convert.ToDateTime(dt.Rows[0]["FechaInicio"]);
                DateTime fechaFin = Convert.ToDateTime(dt.Rows[0]["FechaFin"]);

                ltFechaIni.Text = fechaInicio.ToString("dd.MM.yyyy");
                ltFechaFin.Text = fechaFin.ToString("dd.MM.yyyy");
                ltDescripcionEstrategia.Text = dt.Rows[0]["DescripcionEstrategia"].ToString();

                dt.Dispose();

            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }
        }

        private void CargarGraficaBarraCanalesVenta(int idEstrategia)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarRankingCanalesVentaPorIdEstrategia(Convert.ToInt32(idEstrategia));
            
            var dataList = new List<object>();
            int index = 1;

            foreach (DataRow row in dt.Rows)
            {
                string canal = row["CanalVenta"].ToString();
                decimal ventas = Convert.ToDecimal(row["Ventas"]);

                // Estructura: [x, y, label]
                dataList.Add(new object[] { index, ventas, canal });
                index++;
            }

            // Serializar a JSON
            string jsonData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(dataList);

            // Guardar en un hiddenfield o literal para usar en JS
            hiddenGrafica.Value = jsonData;

            dt.Dispose();
        }

        private void CargarGraficaPiePlanesEstrategia(int idEstrategia)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarRankingPlanesPorIdEstrategia(idEstrategia);

            // En el DataTable deben venir: NombrePlan, NumeroPlanes, ValorAcumulado
            var lista = dt.AsEnumerable().Select(r => new
            {
                label = r["NombrePlan"].ToString(),
                cantidad = Convert.ToInt32(r["NumeroPlanes"]),
                valor = Convert.ToDouble(r["ValorAcumulado"])
            }).ToList();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(lista);

            // Deja disponible en JS
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "dataPiePlanes",
                $"window.planesRanking = {json};",
                true
            );

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
        private void ListaContactosPorUsuario()
        {

            int idUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
            decimal valorTotal = 0;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarContactosCRMPorUsuario(idUsuario, out valorTotal);

            rpContactosCRM.DataSource = dt;
            rpContactosCRM.DataBind();

            //ltValorTotal.Text = valorTotal.ToString("C0");
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
        protected void rpContactosCRM_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;

                HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");

                // Obtener documento del afiliado desde el campo del Repeater
                int documentoAfiliado;
                if (int.TryParse(row["DocumentoAfiliado"].ToString(), out documentoAfiliado))
                {
                    clasesglobales cg = new clasesglobales();
                    DataTable dtAfiliado = cg.ConsultarAfiliadoPorDocumento(documentoAfiliado);

                    if (dtAfiliado.Rows.Count > 0)
                    {
                        int idAfiliado = Convert.ToInt32(dtAfiliado.Rows[0]["idAfiliado"]);
                        DataTable dtEstadoActivo = cg.ConsultarAfiliadoEstadoActivo(idAfiliado);

                        // Encontrar los tres botones
                        //HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                        //HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                        HtmlAnchor btnNuevoAfiliado = (HtmlAnchor)e.Item.FindControl("btnNuevoAfiliado");

                        // Si el afiliado tiene plan activo, ocultar todos los botones
                        if (dtEstadoActivo.Rows.Count > 0)
                        {
                            if (btnEditar != null) btnEditar.Visible = false;
                            if (btnEliminar != null) btnEliminar.Visible = false;
                            if (btnNuevoAfiliado != null) btnNuevoAfiliado.Visible = false;
                        }
                        else
                        {
                            // Mostrar botones solo si no tiene plan activo y según permisos
                            if (ViewState["CrearModificar"].ToString() == "1" && btnEditar != null)
                            {
                                btnEditar.Attributes.Add("href", "crmnuevocontacto?editid=" + row.Row[0].ToString());
                                btnEditar.Visible = true;
                            }

                            if (ViewState["Borrar"].ToString() == "1" && btnEliminar != null)
                            {
                                btnEliminar.Attributes.Add("href", "crmnuevocontacto?deleteid=" + row.Row[0].ToString());
                                btnEliminar.Visible = true;
                            }

                            if (btnNuevoAfiliado != null)
                            {
                                // Este botón se muestra sin permisos adicionales
                                btnNuevoAfiliado.Visible = true;
                            }
                        }
                    }
                }
                
                if (row["FechaCreacion"] != DBNull.Value)
                {
                    DateTime fechaPrimerContacto = Convert.ToDateTime(row["FechaCreacion"]);
                    TimeSpan diferencia = DateTime.Now - fechaPrimerContacto;

                    string leyenda = "";
                    if (diferencia.TotalMinutes < 1)
                        leyenda = "Hace menos de un minuto";
                    else if (diferencia.TotalMinutes < 60)
                        leyenda = $"Hace {(int)diferencia.TotalMinutes} minuto{((int)diferencia.TotalMinutes == 1 ? "" : "s")}";
                    else if (diferencia.TotalHours < 24)
                        leyenda = $"Hace {(int)diferencia.TotalHours} hora{((int)diferencia.TotalHours == 1 ? "" : "s")}";
                    else
                        leyenda = $"Hace {(int)diferencia.TotalDays} día{((int)diferencia.TotalDays == 1 ? "" : "s")}";

                    Literal ltTiempo = (Literal)e.Item.FindControl("ltTiempoTranscurrido");
                    if (ltTiempo != null)
                        ltTiempo.Text = leyenda;
                }
            }
        }
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