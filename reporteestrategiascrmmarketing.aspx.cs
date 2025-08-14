using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace fpWebApp
{
    public partial class reporteestrategiascrmmarketing : System.Web.UI.Page
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
                        //divContenido.Visible = false;
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



                    ListaRankingAsesores();
                    ListaEstadosVentaLeads();
                    ListaRankingCanalesVentaMesVigente();
                    ListaRankingMejorAsesorMesPasado();
                    ObtenerGraficaEstrategiasPorMes();
                    ListaResumenEstrategiaUltimoMes();
                    ListaCuantosLeadsEstrategiaAceptados();

                    clasesglobales cg = new clasesglobales();

                    

                    if (Request.QueryString.Count > 0)
                    {
                        //rpCargos.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            
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
                            //clasesglobales cg = new clasesglobales();
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


        private void ListaRankingAsesores()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarRankingAsesoresMesVigente();
            
            DateTime fechaActual = DateTime.Now;
            string mesActualConAnio = System.Globalization.CultureInfo
                .GetCultureInfo("es-ES")
                .DateTimeFormat
                .GetMonthName(fechaActual.Month)
                + " " + fechaActual.Year;

            ltMesActual.Text = mesActualConAnio.ToString();

            if (dt != null && dt.Rows.Count > 0)
            {
                rptRankingAsesores.DataSource = dt;
                rptRankingAsesores.DataBind();
            }
            else
            {
                rptRankingAsesores.DataSource = null;
                rptRankingAsesores.DataBind();
            }

            dt.Dispose();
        }

        private void ListaResumenEstrategiaUltimoMes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarResumenEstrategiasUltimoMes();

            if (dt != null && dt.Rows.Count > 0)
            {
                string topEstrategia = dt.Rows[0]["TopEstrategia"].ToString();
                string diferencia = dt.Rows[0]["DiferenciaVsPresupuesto"].ToString();
                string presupuesto = dt.Rows[0]["PresupuestoTotal"].ToString();
                string ventas = dt.Rows[0]["VentasTotales"].ToString();
                               
                string html = $@"
                            <span class='pull-right text-right'>
                                <small>Resumen del último mes: <strong>Top: {topEstrategia} ({diferencia})</strong></small><br />
                                <small>Presupuesto: {presupuesto} | Ventas: {ventas}</small>
                            </span>";

                litResumen.Text = html; 
            }
            else
            {
                string html = @"
                            <span class='pull-right text-right'>
                                <small>Resumen del último mes: <strong>Top: Sin datos ($0)</strong></small><br />
                                <small>Presupuesto: $0 | Ventas: $0</small>
                            </span>";

                litResumen.Text = html;
            }

            dt.Dispose();
        }

        private void ListaRankingMejorAsesorMesPasado()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarRankingAsesoresMesPasado();

            DateTime fechaAnterior = DateTime.Now.AddMonths(-1);
            string mesAnteriorConAnio = System.Globalization.CultureInfo
                .GetCultureInfo("es-ES")
                .DateTimeFormat
                .GetMonthName(fechaAnterior.Month)
                + " " + fechaAnterior.Year;

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                ltNomAsesorMesPasado.Text = row["Asesor"].ToString();
                ltMes.Text = mesAnteriorConAnio;
                ltCanalVenta.Text = row["CanalVenta"].ToString();
                ltCantidadPlanes.Text = row["CantidadPlanesVendidos"].ToString() + " Planes vendidos";
                ltValorVendido.Text = "$" + string.Format("{0:N0}", row["TotalVendido"]) + " vendido";

                string archivoFoto = row["Foto"]?.ToString();
                if (!string.IsNullOrWhiteSpace(archivoFoto))
                {
                    // Ruta completa hacia carpeta empleados
                    imgAsesor.ImageUrl = "img/empleados/" + archivoFoto;
                }
                // Si no viene nada, no se asigna imagen
            }
            else
            {
                ltNomAsesorMesPasado.Text = "Sin datos";
                ltMes.Text = mesAnteriorConAnio;
                ltCanalVenta.Text = "N/A";
                ltCantidadPlanes.Text = "0";
                ltValorVendido.Text = "$0";
                imgAsesor.ImageUrl = "img/a4.jpg";
                // No se asigna imagen
            }

            dt?.Dispose();
        }

        private void ListaEstadosVentaLeads()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCantidadLeadsPorEstadosVenta();
            DateTime fechaActual = DateTime.Now;
            string mesActualConAnio = System.Globalization.CultureInfo
             .GetCultureInfo("es-ES")
             .DateTimeFormat
             .GetMonthName(fechaActual.Month)
             + " " + fechaActual.Year;
            ltMesActualEV.Text = mesActualConAnio.ToString();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string estado = row["nombreEstado"].ToString();

                    if (estado == "Caliente")
                    {
                        lblCalienteLeads.Text = row["cantidadLeads"].ToString();
                        lblCalientePorcentaje.Text = row["porcentajeLeads"].ToString() + "%";
                        lblCalienteVentas.Text = "$" + row["monto"].ToString();
                    }
                    else if (estado == "Tibio")
                    {
                        lblTibioLeads.Text = row["cantidadLeads"].ToString();
                        lblTibioPorcentaje.Text = row["porcentajeLeads"].ToString() + "%";
                        lblTibioVentas.Text = "$" + row["monto"].ToString();
                    }
                    else if (estado == "Frío")
                    {
                        lblFrioLeads.Text = row["cantidadLeads"].ToString();
                        lblFrioPorcentaje.Text = row["porcentajeLeads"].ToString() + "%";
                        lblFrioVentas.Text = "$" + row["monto"].ToString();
                    }
                }
            }
        }

        private void ListaRankingCanalesVentaMesVigente()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarRankingCanalesVentaPorVenta();  

            if (dt.Rows.Count > 0)
            {
                DataRow canal = dt.Rows[0];
                ltTeam.Text ="Equipo " + dt.Rows[0]["CanalVenta"].ToString();
                int idCanalVenta = Convert.ToInt32(dt.Rows[0]["idCanalVenta"].ToString());

                DataTable asesores = cg.ConsultarRankingCanalesVentaPorVentaPorIdCanal(idCanalVenta);
                var sb = new StringBuilder();

                if (asesores != null && asesores.Rows.Count > 0)
                {
                    foreach (DataRow row in asesores.Rows)
                    {
                        string foto = row["Foto"]?.ToString().Trim();
                        string asesor = row["Asesor"]?.ToString().Trim();

                        if (string.IsNullOrEmpty(foto))
                        {
                            sb.AppendFormat(
                                "<a href='#'><img alt='{0}' title='{0}' class='img-circle' src='{1}' /></a>",
                                HttpUtility.HtmlEncode(asesor),
                                ResolveUrl("~/img/a4.jpg")
                            );
                            continue;
                        }

                        foto = foto.TrimStart('~', '/', '\\');

                        string imageUrl = foto.StartsWith("/img/empleados/", StringComparison.OrdinalIgnoreCase)
                            ? ResolveUrl("~/" + HttpUtility.UrlPathEncode(foto))
                            : ResolveUrl("~/img/empleados/" + HttpUtility.UrlPathEncode(foto));

                        sb.AppendFormat(
                            "<a href='#'><img alt='{0}' title='{0}' class='img-circle' src='{1}' /></a>",
                            HttpUtility.HtmlEncode(asesor),
                            imageUrl
                        );
                    }
                }

                ltAsesores.Text = sb.ToString();

                decimal ventas = Convert.ToDecimal(dt.Rows[0]["Ventas"], CultureInfo.InvariantCulture);
                
                int estrategias = Convert.ToInt32(canal["Estrategias"]);
                string ranking = canal["Ranking"].ToString();

                decimal meta = 1000000m; // Meta de ejemplo
                decimal porcentaje = ventas > 0 ? (ventas / meta * 100) : 0;

                lblEstadoVentas.InnerText = $"{porcentaje:0}%";
                progressBar.Style["width"] = $"{porcentaje:0}%";
                lblEstrategias.Text = estrategias.ToString();
                lblRanking.Text = string.IsNullOrEmpty(ranking) ? "-" : ranking;
                lblVentas.Text = ventas.ToString("C0", CultureInfo.CurrentCulture);

                ltDescripcion.Text =
                $"Enfocados en resultados, el {ltTeam.Text} ha logrado posicionarse en el {ranking} lugar, " +
                $"impulsando {estrategias} estrategias activas con una excelente gestión de recursos.";

            }
            else
            {
                lblEstadoVentas.InnerText = "0%";
                progressBar.Style["width"] = "0%";
                lblEstrategias.Text = "0";
                lblRanking.Text = "-";
                lblVentas.Text = "$0";
            }

        }

        private void ListaCuantosLeadsEstrategiaAceptados()
        {
            try
            {
                ltCantidadLeadsAceptados.Text = "0";
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarCuantosLeadsEstrategiaAceptados();
                if (dt.Rows.Count > 0)
                {
                    
                    ltCantidadLeadsAceptados.Text = Convert.ToInt32(dt.Rows[0]["TotalContactosMes"]).ToString("N0");
                    ltVentasTotales.Text = Convert.ToDecimal(dt.Rows[0]["TotalVentasAnio"]).ToString("C0", new System.Globalization.CultureInfo("es-CO"));
                    ltVentasTotalesMesActual.Text = Convert.ToDecimal(dt.Rows[0]["TotalVentasMes"]).ToString("C0", new System.Globalization.CultureInfo("es-CO"));


                    decimal ventasTotalesMesActual = Convert.ToDecimal(dt.Rows[0]["TotalVentasMes"].ToString());
                    int totalPresupuestoMes = Convert.ToInt32(dt.Rows[0]["TotalPresupuestoMes"].ToString());


                    decimal mediaVentasMes = ventasTotalesMesActual / totalPresupuestoMes;

                    int porcentaje = (int)Math.Round(mediaVentasMes * 100, MidpointRounding.AwayFromZero);

                    ltMediaVentasMesActual.Text = porcentaje + "%";
                    progressBarVentasMesActual.Attributes["style"] = "width: " + porcentaje + "%;";


                }
                dt.Dispose();
            }
            catch (Exception ex)
            {
               string mensaje = ex.Message;
            }

        }

        public string labelsJson { get; set; }
        public string presupuestoJson { get; set; }
        public string ventasJson { get; set; }
        private void ObtenerGraficaEstrategiasPorMes()
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarEstrategiasMarketingValorMes();

                if(dt.Rows.Count > 0)
                {
                    //Etiquetas para las fechas
                    DateTime fechaActual = DateTime.Now;
                    string mesActualConAnio = System.Globalization.CultureInfo
                     .GetCultureInfo("es-ES")
                     .DateTimeFormat
                     .GetMonthName(fechaActual.Month)
                     + " " + fechaActual.Year;
                    ltMesActualGraf.Text = mesActualConAnio.ToString();

                    string annioActual = fechaActual.Year.ToString();
                    ltAnnioActual.Text = annioActual;
                    //

                    DataTable dt1 = cg.ConsultarCuantosLeadsEstrategiaAceptados();
                    if (dt1.Rows.Count > 0)
                    {
                        decimal totalVentasAnio = Convert.ToDecimal(dt1.Rows[0]["TotalVentasAnio"].ToString());
                        decimal TotalContactosVentasAnio = Convert.ToDecimal(dt1.Rows[0]["TotalContactosVentasAnio"].ToString());
                        int TotalContactosAnio = Convert.ToInt32(dt1.Rows[0]["TotalcontactosAnio"].ToString());

                        decimal mediaCuantosAnio = TotalContactosVentasAnio / TotalContactosAnio;

                        int porcentaje = (int)Math.Round(mediaCuantosAnio * 100, MidpointRounding.AwayFromZero);

                        ltMediaCuantosAnio.Text = porcentaje + "%";
                        progressBarAnio.Attributes["style"] = "width: " + porcentaje + "%;";
                    }                    


                    var labels = new List<string>();
                    var presupuestos = new List<decimal>();
                    var ventas = new List<decimal>();

                    var datosPorMes = dt.AsEnumerable()
                        .GroupBy(r => Convert.ToDateTime(r["Mes"]).Month)
                        .ToDictionary(
                            g => g.Key,
                            g => new
                            {
                                Presupuesto = g.Sum(x => Convert.ToDecimal(x["Presupuesto"])),
                                Ventas = g.Sum(x => Convert.ToDecimal(x["Ventas"]))
                            }
                        );


                    // Rellenar los 12 meses
                    for (int mes = 1; mes <= 12; mes++)
                    {
                        string abreviado = new DateTime(DateTime.Now.Year, mes, 1)
                            .ToString("MMM", new System.Globalization.CultureInfo("es-CO"));

                        labels.Add(abreviado);

                        if (datosPorMes.ContainsKey(mes))
                        {
                            presupuestos.Add(datosPorMes[mes].Presupuesto);
                            ventas.Add(datosPorMes[mes].Ventas);
                        }
                        else
                        {
                            presupuestos.Add(0);
                            ventas.Add(0);
                        }
                    }

                    labelsJson = Newtonsoft.Json.JsonConvert.SerializeObject(labels);
                    presupuestoJson = Newtonsoft.Json.JsonConvert.SerializeObject(presupuestos);
                    ventasJson = Newtonsoft.Json.JsonConvert.SerializeObject(ventas);

                }

            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();               
            }
        }




        public class VentasCanal
        {
            public string CanalVenta { get; set; }
            public int Estrategias { get; set; }
            public decimal Ventas { get; set; }
            public int Ranking { get; set; }
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
                //if (!ValidarCargos(txbNombreCargo.Text.ToString()))
                //{
                //    try
                //    {
                //        string respuesta = cg.InsertarCargo(txbNombreCargo.Text.ToString().Trim());

                //        cg.InsertarLog(Session["idusuario"].ToString(), "cargos empleado", "Agrega", "El usuario agregó un nuevo cargo de empleado: " + txbNombreCargo.Text.ToString() + ".", "", "");
                //    }
                //    catch (Exception ex)
                //    {
                //        string mensajeExcepcionInterna = string.Empty;
                //        Console.WriteLine(ex.Message);
                //        if (ex.InnerException != null)
                //        {
                //            mensajeExcepcionInterna = ex.InnerException.Message;
                //            Console.WriteLine("Mensaje de la excepción interna: " + mensajeExcepcionInterna);
                //        }
                //        ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                //        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                //        "Excepción interna." +
                //        "</div>";
                //    }
                //    Response.Redirect("cargos");
                //}
                //else
                //{
                //    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                //        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                //        "Ya existe un cargo con ese nombre." +
                //        "</div>";
                //}
            }
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string consultaSQL = @"SELECT NombreCargo AS 'Nombre de Cargos'
		                               FROM cargos
		                               ORDER BY NombreCargo;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
                string nombreArchivo = $"CargosEmpleados_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

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