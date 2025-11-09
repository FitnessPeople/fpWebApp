using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class reporteefectividadcrm : System.Web.UI.Page
    {
        protected string Grafico1 = "{}";
        protected string Grafico2 = "{}";
        protected string Grafico3 = "{}";
        protected string Grafico4 = "{}";
        protected string Grafico5 = "{}";
        protected string Grafico6 = "{}";

        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("es-CO");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Efectividad gestión");
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
                        //btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            
                            ListaCanalesDeVenta();
                            listaAsesoresActivos();
                            listaGestionAesores();
                           
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            txbFechaIni.Attributes.Add("type", "date");
                            txbFechaIni.Value = DateTime.Now.ToString("yyyy-MM-01").ToString();
                            txbFechaFin.Attributes.Add("type", "date");
                            txbFechaFin.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();

                        }
                    }
                    listaContactos();                  
                    listaIndicadorPorGenero();
                    listaIndicadorPorEdades();
                    listaIndicadorPorPlanesSeleccionados();
                    listaIndicadorPorEstadosVentaCRM();
                    listaGestionAesores();
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


        private void listaGestionAesores()
        {
            clasesglobales cg = new clasesglobales();
            int idCanalVenta = Convert.ToInt32(ddlCanalesVenta.SelectedValue);
            int idUsuario = Convert.ToInt32(ddlAsesores.SelectedValue);
            ltCuantosAse.Text = "0";

           DateTime? FechaIni = null;
            DateTime? FechaFin = null;

            if (!string.IsNullOrEmpty(txbFechaIni.Value))
                FechaIni = Convert.ToDateTime(txbFechaIni.Value);

            if (!string.IsNullOrEmpty(txbFechaFin.Value))
                FechaFin = Convert.ToDateTime(txbFechaFin.Value);

            try
            {
                DataTable dt = cg.ConsultarIndicadorGestionAsesorCRM(idCanalVenta, FechaIni, FechaFin, idUsuario);
                rpGestionAsesores.DataSource = dt;
                rpGestionAsesores.DataBind();
                ltCuantosAse.Text = dt.Rows.Count.ToString();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }

        }

        private void ListaCanalesDeVenta()
        {
            ddlCanalesVenta.Items.Clear();
            ddlCanalesVenta.Items.Add(new ListItem("Todos los canales de venta", "0"));

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCanalesVenta();

            // Excluir canal con id = 1 ("Ninguno")
            DataRow[] filasFiltradas = dt.Select("idCanalVenta <> 1");
            dt = filasFiltradas.Length > 0 ? filasFiltradas.CopyToDataTable() : dt.Clone();

            ddlCanalesVenta.DataSource = dt;
            ddlCanalesVenta.DataBind();

            dt.Dispose();
        }

        protected void ddlCanalesVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCanalVenta = Convert.ToInt32(ddlCanalesVenta.SelectedValue);
            CargarAsesoresPorCanalVenta(idCanalVenta);
        }

        private void CargarAsesoresPorCanalVenta(int idCanalVenta)
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                DataTable dt = cg.ConsultaCargarAsesoresPorCanalVenta(idCanalVenta);

                if (idCanalVenta > 0)
                {
                    var filteredRows = dt.AsEnumerable()
                                         .Where(r => r.Field<int>("idCanalVenta") == idCanalVenta);

                    dt = filteredRows.Any() ? filteredRows.CopyToDataTable() : dt.Clone();
                }

                ddlAsesores.Items.Clear();
                ddlAsesores.Items.Add(new ListItem("Todos los asesores", "0"));
                ddlAsesores.DataSource = dt;
                ddlAsesores.DataBind();

                dt.Dispose();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }
        }

        private void listaAsesoresActivos()
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                DataTable dt = cg.ConsultaCargarAsesoresActivos();
                ddlAsesores.Items.Clear();
                ddlAsesores.Items.Add(new ListItem("Todos los asesores", "0"));
                ddlAsesores.DataSource = dt;
                ddlAsesores.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            listaContactos();
            listaIndicadorPorGenero();
            listaIndicadorPorEdades();
            listaIndicadorPorPlanesSeleccionados();
            listaIndicadorPorEstadosVentaCRM();
            listaGestionAesores();
        }

        private void listaContactos()
        {
            clasesglobales cg = new clasesglobales();

            int idCanalVenta = Convert.ToInt32(ddlCanalesVenta.SelectedValue);
            int idUsuario = Convert.ToInt32(ddlAsesores.SelectedValue);

            DateTime? FechaIni = null;
            DateTime? FechaFin = null;

            if (!string.IsNullOrEmpty(txbFechaIni.Value))
                FechaIni = Convert.ToDateTime(txbFechaIni.Value);

            if (!string.IsNullOrEmpty(txbFechaFin.Value))
                FechaFin = Convert.ToDateTime(txbFechaFin.Value);

            try
            {
                DataTable dt = cg.ConsultarEfectividadGestionCRM(idCanalVenta, FechaIni, FechaFin, idUsuario);

                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    rpContactos.DataSource = dt;
                    rpContactos.DataBind();

                    
                    decimal sumatoriaValor = 0;
                    if (dt.Columns.Contains("ValorPropuesta"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["ValorPropuesta"] != DBNull.Value)
                                sumatoriaValor += Convert.ToDecimal(row["ValorPropuesta"]);
                        }
                    }
                    
                    ltCantidadCon.Text = $"{dt.Rows.Count:N0} registros"; // Total de registros                    
                }
                else
                {
                    rpContactos.DataSource = null;
                    rpContactos.DataBind();

                    ltCantidadCon.Text = "0 registros";
                  
                }

                dt.Dispose();
            }
            catch (Exception ex)
            {
                ltCantidadCon.Text = "<p>Error al consultar los contactos.</p>";
               
            }
        }

        private void listaIndicadorPorGenero()
        {
            clasesglobales cg = new clasesglobales();

            int idCanalVenta = Convert.ToInt32(ddlCanalesVenta.SelectedValue);
            int idUsuario = Convert.ToInt32(ddlAsesores.SelectedValue);

            DateTime? FechaIni = null;
            DateTime? FechaFin = null;

            if (!string.IsNullOrEmpty(txbFechaIni.Value))
                FechaIni = Convert.ToDateTime(txbFechaIni.Value);

            if (!string.IsNullOrEmpty(txbFechaFin.Value))
                FechaFin = Convert.ToDateTime(txbFechaFin.Value);

            try
            {
                DataTable dt = cg.ConsultarIndicadorGeneroCRM(idCanalVenta, FechaIni, FechaFin, idUsuario);

                if (dt == null || dt.Rows.Count == 0)
                {
                    ltCuantos2.Text = "<p>No hay datos de género disponibles.</p>";
                    ltRegistros2.Text = "0";
                    return;
                }

                // Validar nombres de columnas (por si el SP devuelve otras columnas)
                string colGenero = dt.Columns.Contains("Genero") ? "Genero"
                                 : dt.Columns.Contains("genero") ? "genero"
                                 : dt.Columns.Contains("sexo") ? "sexo" : null;

                string colCantidad = dt.Columns.Contains("Cantidad") ? "Cantidad"
                                 : dt.Columns.Contains("cantidad") ? "cantidad"
                                 : dt.Columns.Contains("Total") ? "Total" : null;

                if (colGenero == null || colCantidad == null)
                {
                    // Si las columnas no coinciden, mostrar todo el datatable como fallback (útil para debug)
                    System.Text.StringBuilder sbfallback = new System.Text.StringBuilder();
                    sbfallback.Append("<table class='table table-sm'><thead><tr>");
                    foreach (DataColumn c in dt.Columns) sbfallback.AppendFormat("<th>{0}</th>", c.ColumnName);
                    sbfallback.Append("</tr></thead><tbody>");
                    foreach (DataRow r in dt.Rows)
                    {
                        sbfallback.Append("<tr>");
                        foreach (DataColumn c in dt.Columns) sbfallback.AppendFormat("<td>{0}</td>", r[c] ?? "");
                        sbfallback.Append("</tr>");
                    }
                    sbfallback.Append("</tbody></table>");
                    ltCuantos2.Text = "<p>Estructura de resultado inesperada:</p>" + sbfallback.ToString();
                    ltRegistros2.Text = dt.Rows.Count.ToString();
                    dt.Dispose();
                    return;
                }

                // Construir la tabla con los nombres esperados
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<table class='table table-condensed table-sm' style='font-size:13px;margin-bottom:0;'>");
                sb.Append("<thead><tr><th>Género</th><th class='text-right'>Cantidad</th></tr></thead>");
                sb.Append("<tbody>");

                foreach (DataRow row in dt.Rows)
                {
                    string genero = row[colGenero].ToString().Trim();
                    int cantidad = 0;
                    int.TryParse(row[colCantidad].ToString(), out cantidad);

                    sb.AppendFormat("<tr><td>{0}</td><td style='text-align:right'>{1}</td></tr>", HttpUtility.HtmlEncode(genero), cantidad.ToString("N0"));
                }

                sb.Append("</tbody></table>");

                ltCuantos2.Text = sb.ToString();
                ltRegistros2.Text = dt.Rows.Count.ToString();

                dt.Dispose();
            }
            catch (Exception ex)
            {
                // Para debugging podrías escribir ex.Message en algún literal de desarrollo
                ltCuantos2.Text = "<p>Error al consultar los datos.</p>";
                ltRegistros2.Text = "0";
            }
        }

        private void listaIndicadorPorEdades()
        {
            clasesglobales cg = new clasesglobales();

            int idCanalVenta = Convert.ToInt32(ddlCanalesVenta.SelectedValue);
            int idUsuario = Convert.ToInt32(ddlAsesores.SelectedValue);

            DateTime? FechaIni = null;
            DateTime? FechaFin = null;

            if (!string.IsNullOrEmpty(txbFechaIni.Value))
                FechaIni = Convert.ToDateTime(txbFechaIni.Value);

            if (!string.IsNullOrEmpty(txbFechaFin.Value))
                FechaFin = Convert.ToDateTime(txbFechaFin.Value);

            try
            {
                DataTable dt = cg.ConsultarIndicadorEdadesCRM(idCanalVenta, FechaIni, FechaFin, idUsuario);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Construir tabla HTML con los resultados
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append("<table class='table table-striped table-sm' style='font-size:13px;'>");
                    sb.Append("<thead><tr><th>Rango de Edad</th><th>Cantidad</th></tr></thead>");
                    sb.Append("<tbody>");

                    foreach (DataRow row in dt.Rows)
                    {
                        string rango = row["RangoEdad"].ToString().Trim();
                        int cantidad = Convert.ToInt32(row["Cantidad"]);

                        sb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", rango, cantidad);
                    }

                    sb.Append("</tbody></table>");

                    // Mostrar tabla dentro del literal correspondiente
                    ltCuantos3.Text = sb.ToString();
                    ltRegistros3.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    ltCuantos3.Text = "<p>No hay datos de rangos de edad disponibles.</p>";
                    ltRegistros3.Text = "0";
                }

                dt.Dispose();
            }
            catch (Exception ex)
            {
                ltCuantos3.Text = "<p>Error al consultar los datos.</p>";
                ltRegistros3.Text = "0";
            }
        }

        private void listaIndicadorPorPlanesSeleccionados()
        {
            clasesglobales cg = new clasesglobales();

            int idCanalVenta = Convert.ToInt32(ddlCanalesVenta.SelectedValue);
            int idUsuario = Convert.ToInt32(ddlAsesores.SelectedValue);
            int limite = 3;

            DateTime? FechaIni = null;
            DateTime? FechaFin = null;

            if (!string.IsNullOrEmpty(txbFechaIni.Value))
                FechaIni = Convert.ToDateTime(txbFechaIni.Value);

            if (!string.IsNullOrEmpty(txbFechaFin.Value))
                FechaFin = Convert.ToDateTime(txbFechaFin.Value);

            try
            {
                DataTable dt = cg.ConsultarIndicadorPlanesSeleccionados(idCanalVenta, FechaIni, FechaFin, idUsuario, limite);

                if (dt == null || dt.Rows.Count == 0)
                {
                    ltCuantos4.Text = "<p>No hay datos de planes disponibles.</p>";
                    ltRegistros4.Text = "0";
                    return;
                }

                // Se asume que el SP devuelve: Plan | CantidadConsultas
                string colPlan = dt.Columns.Contains("Plan") ? "Plan"
                                 : dt.Columns.Contains("NombrePlan") ? "NombrePlan" : null;

                string colCantidad = dt.Columns.Contains("CantidadConsultas") ? "CantidadConsultas"
                                      : dt.Columns.Contains("Cantidad") ? "Cantidad"
                                      : dt.Columns.Contains("Total") ? "Total" : null;

                if (colPlan == null || colCantidad == null)
                {
                    // Mostrar estructura de datos (debug)
                    System.Text.StringBuilder sbfallback = new System.Text.StringBuilder();
                    sbfallback.Append("<table class='table table-sm'><thead><tr>");
                    foreach (DataColumn c in dt.Columns) sbfallback.AppendFormat("<th>{0}</th>", c.ColumnName);
                    sbfallback.Append("</tr></thead><tbody>");
                    foreach (DataRow r in dt.Rows)
                    {
                        sbfallback.Append("<tr>");
                        foreach (DataColumn c in dt.Columns) sbfallback.AppendFormat("<td>{0}</td>", r[c] ?? "");
                        sbfallback.Append("</tr>");
                    }
                    sbfallback.Append("</tbody></table>");
                    ltCuantos4.Text = "<p>Estructura inesperada del resultado:</p>" + sbfallback.ToString();
                    ltRegistros4.Text = dt.Rows.Count.ToString();
                    dt.Dispose();
                    return;
                }

                // Construir tabla HTML
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<table class='table table-condensed table-sm' style='font-size:13px;margin-bottom:0;'>");
                sb.Append("<thead><tr><th>Plan</th><th class='text-right'>Cantidad</th></tr></thead>");
                sb.Append("<tbody>");

                foreach (DataRow row in dt.Rows)
                {
                    string plan = row[colPlan].ToString().Trim();
                    int cantidad = 0;
                    int.TryParse(row[colCantidad].ToString(), out cantidad);

                    sb.AppendFormat("<tr><td>{0}</td><td style='text-align:right'>{1}</td></tr>",
                        HttpUtility.HtmlEncode(plan), cantidad.ToString("N0"));
                }

                sb.Append("</tbody></table>");

                ltCuantos4.Text = sb.ToString();
                ltRegistros4.Text = dt.Rows.Count.ToString();

                dt.Dispose();
            }
            catch (Exception)
            {
                ltCuantos4.Text = "<p>Error al consultar los datos.</p>";
                ltRegistros4.Text = "0";
            }
        }

        private void listaIndicadorPorEstadosVentaCRM()
        {
            clasesglobales cg = new clasesglobales();

            int idCanalVenta = Convert.ToInt32(ddlCanalesVenta.SelectedValue);
            int idUsuario = Convert.ToInt32(ddlAsesores.SelectedValue);

            DateTime? FechaIni = null;
            DateTime? FechaFin = null;

            if (!string.IsNullOrEmpty(txbFechaIni.Value))
                FechaIni = Convert.ToDateTime(txbFechaIni.Value);

            if (!string.IsNullOrEmpty(txbFechaFin.Value))
                FechaFin = Convert.ToDateTime(txbFechaFin.Value);

            try
            {
                DataTable dt = cg.ConsultarIndicadorEstadosVentaCRM(idCanalVenta, FechaIni, FechaFin, idUsuario);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Buscar columnas sin importar mayúsculas/minúsculas
                    string colEstado = dt.Columns.Cast<DataColumn>()
                        .FirstOrDefault(c => c.ColumnName.Equals("EstadoVenta", StringComparison.OrdinalIgnoreCase))?.ColumnName;

                    string colCantidad = dt.Columns.Cast<DataColumn>()
                        .FirstOrDefault(c => c.ColumnName.Equals("Cantidad", StringComparison.OrdinalIgnoreCase))?.ColumnName;

                    string colValor = dt.Columns.Cast<DataColumn>()
                        .FirstOrDefault(c => c.ColumnName.Equals("Valor", StringComparison.OrdinalIgnoreCase))?.ColumnName;

                    if (colEstado == null || colCantidad == null || colValor == null)
                    {
                        // Mostrar estructura inesperada (debug visual)
                        System.Text.StringBuilder sbFallback = new System.Text.StringBuilder();
                        sbFallback.Append("<table class='table table-sm'><thead><tr>");
                        foreach (DataColumn c in dt.Columns)
                            sbFallback.AppendFormat("<th>{0}</th>", c.ColumnName);
                        sbFallback.Append("</tr></thead><tbody>");
                        foreach (DataRow r in dt.Rows)
                        {
                            sbFallback.Append("<tr>");
                            foreach (DataColumn c in dt.Columns)
                                sbFallback.AppendFormat("<td>{0}</td>", r[c] ?? "");
                            sbFallback.Append("</tr>");
                        }
                        sbFallback.Append("</tbody></table>");
                        ltCuantosTemp.Text = "<p>Estructura inesperada del resultado:</p>" + sbFallback.ToString();
                        ltRegistrosTemp.Text = dt.Rows.Count.ToString();
                        dt.Dispose();
                        return;
                    }

                    // Construir tabla HTML con los resultados correctos
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<table class='table table-condensed table-sm' style='font-size:13px;margin-bottom:0;'>");
                    sb.Append("<thead><tr><th>Estado de Venta</th><th class='text-right'>Cantidad</th><th class='text-right'>Valor</th></tr></thead>");
                    sb.Append("<tbody>");

                    foreach (DataRow row in dt.Rows)
                    {
                        string estado = row[colEstado].ToString().Trim();
                        int cantidad = 0;
                        decimal valor = 0;

                        int.TryParse(row[colCantidad].ToString(), out cantidad);
                        decimal.TryParse(row[colValor].ToString(), out valor);

                        sb.AppendFormat(
                            "<tr><td>{0}</td><td style='text-align:right'>{1:N0}</td><td style='text-align:right'>$ {2:N0}</td></tr>",
                            HttpUtility.HtmlEncode(estado), cantidad, valor
                        );
                    }

                    sb.Append("</tbody></table>");

                    // ✅ Mostrar en los literales correctos
                    ltCuantosTemp.Text = sb.ToString();
                    ltRegistrosTemp.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    ltCuantosTemp.Text = "<p>No hay datos de estados de venta disponibles.</p>";
                    ltRegistrosTemp.Text = "0";
                }

                dt.Dispose();
            }
            catch (Exception ex)
            {
                ltCuantosTemp.Text = "<p>Error al consultar los datos.</p>";
                ltRegistrosTemp.Text = "0";
            }
        }


        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {

        }


    }
}