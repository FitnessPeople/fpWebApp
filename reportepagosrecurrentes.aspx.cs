using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Globalization;
using NPOI.POIFS.Crypt.Agile;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Security.Cryptography;

namespace fpWebApp
{
	public partial class reportepagosrecurrentes : System.Web.UI.Page
	{
        static int idIntegracion = 1; // Pruebas
        //static int idIntegracion = 4; // Producción

        protected string Url
        {
            get { return ViewState["urlWompi"]?.ToString(); }
            set { ViewState["urlWompi"] = value; }
        }

        protected string IntegritySecret
        {
            get { return ViewState["integrity_secret"]?.ToString(); }
            set { ViewState["integrity_secret"] = value; }
        }

        protected string KeyPub
        {
            get { return ViewState["keyPub"]?.ToString(); }
            set { ViewState["keyPub"] = value; }
        }

        protected string KeyPriv
        {
            get { return ViewState["keyPriv"]?.ToString(); }
            set { ViewState["keyPriv"] = value; }
        }

        //

        protected string DataIdTransaccion
        {
            get { return ViewState["dataIdTransaccion"]?.ToString(); }
            set { ViewState["dataIdTransaccion"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
		{
            CultureInfo culture = new CultureInfo("es-CO");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Pagos");
                    ConsultarDatosWompi();

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
                            CargarPlanes();
                            //lbExportarExcel.Visible = false;
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
                            CargarPlanes();
                        }
                    }
                    listaTransacciones();
                    VentasWeb();
                    VentasCounter();
                }
                else
                {
                    Response.Redirect("logout.aspx");
                }
            }
        }

        private void ConsultarDatosWompi()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dtIntegracionWompi = cg.ConsultarIntegracionWompi(idIntegracion);

            Url = dtIntegracionWompi != null && dtIntegracionWompi.Rows.Count > 0 ? dtIntegracionWompi.Rows[0]["urlTest"].ToString() : null;
            IntegritySecret = dtIntegracionWompi != null && dtIntegracionWompi.Rows.Count > 0 ? dtIntegracionWompi.Rows[0]["integrity_secret"].ToString() : null;
            KeyPub = dtIntegracionWompi != null && dtIntegracionWompi.Rows.Count > 0 ? dtIntegracionWompi.Rows[0]["keyPub"].ToString() : null;
            KeyPriv = dtIntegracionWompi != null && dtIntegracionWompi.Rows.Count > 0 ? dtIntegracionWompi.Rows[0]["keyPriv"].ToString() : null;

            dtIntegracionWompi.Dispose();
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

        private void CargarPlanes()
        {
            ddlPlanes.Items.Clear();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanesWeb();

            ddlPlanes.DataSource = dt;
            ddlPlanes.DataBind();
            dt.Dispose();
        }

        private void listaTransacciones()
        {
            
            clasesglobales cg = new clasesglobales();

            string query = @"SELECT *
                            FROM PagosPlanAfiliado ppa
                            INNER JOIN (
                                SELECT DataIdFuente, MAX(fechaHoraPago) AS UltimoPago
                                FROM PagosPlanAfiliado
                                GROUP BY DataIdFuente
                            ) ult ON ppa.DataIdFuente = ult.DataIdFuente AND ppa.fechaHoraPago = ult.UltimoPago
                            INNER JOIN AfiliadosPlanes ap ON ap.idAfiliadoPlan = ppa.idAfiliadoPlan
                            INNER JOIN Afiliados a ON a.idAfiliado = ap.idAfiliado 
                            INNER JOIN Usuarios u ON u.idUsuario = ppa.idUsuario  
                            INNER JOIN Planes p ON p.idPlan = ap.idPlan
                            ORDER BY ppa.fechaHoraPago ASC;";

            DataTable dt = cg.TraerDatos(query);
            
            rpPagos.DataSource = dt;
            rpPagos.DataBind();
            dt.Dispose();
        }

        private void listaTransaccionesPorFecha(int tipoPago, int idPlan, string fechaIni, string fechaFin)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosPorTipo(tipoPago, idPlan, fechaIni, fechaFin, out decimal valorTotal);
            rpPagos.DataSource = dt;
            rpPagos.DataBind();
            //ltValortotalWompi.Text = valorTotal.ToString("C0");
            dt.Dispose();

            decimal sumatoriaValor = 0;

            if (dt.Rows.Count > 0)
            {
                object suma = dt.Compute("SUM(Valor)", "");
                sumatoriaValor = suma != DBNull.Value ? Convert.ToDecimal(suma) : 0;
            }
        }

        private void VentasWeb()
        {
            string strQuery = "";
            //if (rblValor.SelectedValue.ToString() == "")
            //{
            strQuery = @"SELECT  
                SUM(pa.Valor) sumatoria 
                FROM pagosplanafiliado pa
                    INNER JOIN afiliadosplanes ap ON ap.idAfiliadoPlan = pa.idAfiliadoPlan
                    INNER JOIN afiliados a ON a.idAfiliado = ap.idAfiliado    
                    INNER JOIN usuarios u ON u.idUsuario = pa.idUsuario  
                    INNER JOIN mediosdepago mp ON mp.idMedioPago = pa.idMedioPago 
                    INNER JOIN planes p ON p.idPlan = ap.idPlan 
                WHERE pa.idMedioPago = 4 
                AND DATE(pa.FechaHoraPago) 
                BETWEEN IFNULL(NULLIF('" + txbFechaIni.Value.ToString() + @"', ''), DATE_FORMAT(CURDATE(), '%Y-%m-01')) 
                AND IFNULL(NULLIF('" + txbFechaFin.Value.ToString() + @"', ''), CURDATE()) 
                AND u.idUsuario = 156 
                AND ap.idPlan = " + ddlPlanes.SelectedValue.ToString();
            //}
            //else
            //{
            //    strQuery = @"SELECT  
            //    SUM(pa.Valor) sumatoria 
            //    FROM pagosplanafiliado pa
            //        INNER JOIN afiliadosplanes ap ON ap.idAfiliadoPlan = pa.idAfiliadoPlan
            //        INNER JOIN afiliados a ON a.idAfiliado = ap.idAfiliado    
            //        INNER JOIN usuarios u ON u.idUsuario = pa.idUsuario  
            //        INNER JOIN mediosdepago mp ON mp.idMedioPago = pa.idMedioPago 
            //    WHERE pa.idMedioPago = 4 
            //    AND DATE(pa.FechaHoraPago) 
            //    BETWEEN IFNULL(NULLIF('" + txbFechaIni.Value.ToString() + @"', ''), DATE_FORMAT(CURDATE(), '%Y-%m-01')) 
            //    AND IFNULL(NULLIF('" + txbFechaFin.Value.ToString() + @"', ''), CURDATE()) 
            //    AND u.idUsuario = 156 
            //    AND pa.Valor = " + rblValor.SelectedValue.ToString();
            //}

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            dt.Dispose();

            int valor = 0;
            int.TryParse(dt.Rows[0]["sumatoria"]?.ToString(), out valor);
        }

        private void VentasCounter()
        {
            string strQuery = "";
            //if (rblValor.SelectedValue.ToString() == "")
            //{
            strQuery = @"SELECT  
                SUM(pa.Valor) sumatoria 
                FROM pagosplanafiliado pa
                    INNER JOIN afiliadosplanes ap ON ap.idAfiliadoPlan = pa.idAfiliadoPlan
                    INNER JOIN afiliados a ON a.idAfiliado = ap.idAfiliado    
                    INNER JOIN usuarios u ON u.idUsuario = pa.idUsuario  
                    INNER JOIN mediosdepago mp ON mp.idMedioPago = pa.idMedioPago 
                    INNER JOIN planes p ON p.idPlan = ap.idPlan 
                WHERE pa.idMedioPago = 4 
                AND DATE(pa.FechaHoraPago) 
                BETWEEN IFNULL(NULLIF('" + txbFechaIni.Value.ToString() + @"', ''), DATE_FORMAT(CURDATE(), '%Y-%m-01')) 
                AND IFNULL(NULLIF('" + txbFechaFin.Value.ToString() + @"', ''), CURDATE()) 
                AND u.idUsuario = 152 
                AND ap.idPlan = " + ddlPlanes.SelectedValue.ToString();
            //}
            //else
            //{
            //    strQuery = @"SELECT  
            //    SUM(pa.Valor) sumatoria 
            //    FROM pagosplanafiliado pa
            //        INNER JOIN afiliadosplanes ap ON ap.idAfiliadoPlan = pa.idAfiliadoPlan
            //        INNER JOIN afiliados a ON a.idAfiliado = ap.idAfiliado    
            //        INNER JOIN usuarios u ON u.idUsuario = pa.idUsuario  
            //        INNER JOIN mediosdepago mp ON mp.idMedioPago = pa.idMedioPago 
            //    WHERE pa.idMedioPago = 4 
            //    AND DATE(pa.FechaHoraPago) 
            //    BETWEEN IFNULL(NULLIF('" + txbFechaIni.Value.ToString() + @"', ''), DATE_FORMAT(CURDATE(), '%Y-%m-01')) 
            //    AND IFNULL(NULLIF('" + txbFechaFin.Value.ToString() + @"', ''), CURDATE()) 
            //    AND u.idUsuario = 152
            //    AND pa.Valor = " + rblValor.SelectedValue.ToString();
            //}

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            dt.Dispose();

            int valor = 0;
            int.TryParse(dt.Rows[0]["sumatoria"]?.ToString(), out valor);
        }

        private string listarDetalle(int idAfiliadoPlan)
        {
            string parametro = string.Empty;
            string nomAfiliado = string.Empty;
            string mensaje = string.Empty;
            string tipoPago = string.Empty;
            StringBuilder sb = new StringBuilder();
            clasesglobales cg = new clasesglobales();
            //int idempresa = 1; //Wompi pruebas 
            int idempresa = 4; //Wompi produccion

            try
            {
                DataTable dt = cg.ConsultarPagosPorId(idAfiliadoPlan);
                tipoPago = dt.Rows[0]["NombreMedioPago"].ToString();


                switch (tipoPago)
                {

                    case "Efectivo":
                        sb.Append("<table class=\"table table-bordered table-striped\">");
                        sb.Append("<tr>");
                        sb.Append("<th>ID</th><th>Documento afiliado</th><th>Nombre afiliado</th><th>Tipo pago</th><th>Valor</th><th>Fecha Cre.</th>");
                        sb.Append("<th>Estado</th><th>Usuario</th><th>Canal de Venta</th>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append($"<td>{dt.Rows[0]["idAfiliadoPlan"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["DocumentoAfiliado"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["NombreAfiliado"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["NombreMedioPago"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["Valor"].ToString()}</td>");
                        sb.Append($"<td>" + String.Format("{0:dd MMM yyyy}", Convert.ToDateTime(dt.Rows[0]["FechaHoraPago"].ToString())) + "</td>");
                        sb.Append($"<td>{"Aprobado"}</td>");
                        sb.Append($"<td>{dt.Rows[0]["Usuario"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["CanalVenta"].ToString()}</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        break;
                    case "Transferencia":
                        sb.Append("<table class=\"table table-bordered table-striped\">");
                        sb.Append("<tr>");
                        sb.Append("<th>ID</th><th>Documento afiliado</th><th>Nombre afiliado</th><th>Tipo pago</th><th>Valor</th><th>Fecha Cre.</th>");
                        sb.Append("<th>Banco</th><th>Estado</th><th>Usuario</th><th>Canal de Venta</th>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append($"<td>{dt.Rows[0]["idAfiliadoPlan"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["DocumentoAfiliado"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["NombreAfiliado"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["NombreMedioPago"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["Valor"].ToString()}</td>");
                        sb.Append($"<td>" + String.Format("{0:dd MMM yyyy}", Convert.ToDateTime(dt.Rows[0]["FechaHoraPago"].ToString())) + "</td>");
                        sb.Append($"<td>{dt.Rows[0]["Banco"].ToString()}</td>");
                        sb.Append($"<td>{"Aprobado"}</td>");
                        sb.Append($"<td>{dt.Rows[0]["Usuario"].ToString()}</td>");
                        sb.Append($"<td>{dt.Rows[0]["CanalVenta"].ToString()}</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");

                        break;
                    case "Datafono":

                        break;
                    case "Pago en línea":

                        DataTable dti = cg.ConsultarUrl(idempresa);

                        if (dt.Rows.Count > 0)
                        {
                            //parametro = dt.Rows[0]["IdReferencia"].ToString();
                            parametro = dt.Rows[0]["DataIdTransaction"].ToString();
                            nomAfiliado = dt.Rows[0]["NombreAfiliado"].ToString();
                        }

                        string url = dti.Rows[0]["urlTest"].ToString() + "/transactions/" + parametro;
                        string[] respuesta = cg.EnviarPeticionGet(url, idempresa.ToString(), out mensaje);
                        JToken token = JToken.Parse(respuesta[0]);
                        string prettyJson = token.ToString(Formatting.Indented);

                        if (mensaje == "Ok") //Verifica respuesta ok
                        {
                            JObject jsonData = JObject.Parse(prettyJson);

                            List<pagoswompidet> listaPagos = new List<pagoswompidet>
                        {
                            new pagoswompidet
                            {
                                Id = jsonData["data"]["id"]?.ToString(),
                                FechaCreacion = jsonData["data"]["created_at"]?.ToString(),
                                FechaFinalizacion = jsonData["data"]["finalized_at"]?.ToString(),
                                Valor = ((jsonData["data"]["amount_in_cents"]?.Value<int>() ?? 0) / 100).ToString("N0") + " " + jsonData["data"]["currency"]?.ToString(),
                                Moneda = jsonData["data"]["currency"]?.ToString(),
                                MetodoPago = jsonData["data"]["payment_method_type"]?.ToString(),
                                Estado = jsonData["data"]["status"]?.ToString(),
                                Referencia = jsonData["data"]["reference"]?.ToString(),
                                NombreTarjeta = jsonData["data"]["payment_method"]["extra"]["name"]?.ToString(),
                                UltimosDigitos = jsonData["data"]["payment_method"]["extra"]["last_four"]?.ToString(),
                                MarcaTarjeta = jsonData["data"]["payment_method"]["extra"]["brand"]?.ToString(),
                                TipoTarjeta = jsonData["data"]["payment_method"]["extra"]["card_type"]?.ToString(),
                                NombreComercio = jsonData["data"]["merchant"]["name"]?.ToString(),
                                ContactoComercio = jsonData["data"]["merchant"]["contact_name"]?.ToString(),
                                TelefonoComercio = jsonData["data"]["merchant"]["phone_number"]?.ToString(),
                                URLRedireccion = jsonData["data"]["redirect_url"]?.ToString(),
                                PaymentLinkId = jsonData["data"]["payment_link_id"]?.ToString(),
                                PublicKeyComercio = jsonData["data"]["merchant"]["public_key"]?.ToString(),
                                EmailComercio = jsonData["data"]["merchant"]["email"]?.ToString() }
                                //Estado3DS = jsonData["data"]["payment_method"]["extra"]["three_ds_auth"]["three_ds_auth"]["current_step_status"]?.ToString()                                }
                            };

                            sb.Append("<table class=\"table table-bordered table-striped\">");
                            sb.Append("<tr>");
                            sb.Append("<th>ID</th><th>Afiliado</th><th>Fecha Cre.</th><th>Fecha Fin.</th><th>Valor</th>");
                            sb.Append("<th>Método Pago</th><th>Estado</th><th>Referencia</th><th>Usuario</th><th>Canal de Venta</th>");
                            sb.Append("</tr>");

                            foreach (var pago in listaPagos)
                            {
                                sb.Append("<tr>");
                                sb.Append($"<td>{pago.Id}</td>");
                                sb.Append($"<td>{nomAfiliado}</td>");
                                sb.Append($"<td>" + String.Format("{0:dd MMM yyyy}", Convert.ToDateTime(pago.FechaCreacion)) + "</td>");
                                sb.Append($"<td>" + String.Format("{0:dd MMM yyyy}", Convert.ToDateTime(pago.FechaFinalizacion)) + "</td>");
                                sb.Append($"<td>{pago.Valor}</td>");
                                sb.Append($"<td>{pago.MetodoPago}</td>");
                                sb.Append($"<td>{pago.Estado}</td>");
                                sb.Append($"<td>{pago.Referencia}</td>");
                                sb.Append($"<td>{dt.Rows[0]["Usuario"].ToString()}</td>");
                                sb.Append($"<td>{dt.Rows[0]["CanalVenta"].ToString()}</td>");
                                sb.Append("</tr>");
                            }

                            sb.Append("</table>");

                            //return sb.ToString();
                        }
                        else
                        {
                            return prettyJson;
                        }
                        break;
                    default:
                        prettyJson = JsonConvert.SerializeObject(new { error = "Sin datos del tipo de pago: " });
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Error");
            }

            return sb.ToString();
        }

        protected void rpPagos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnVer = (HtmlAnchor)e.Item.FindControl("btnVer");
                    btnVer.Attributes.Add("href", "reportepagoswompi?verid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnVer.Visible = false;
                }
            }

        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Arreglar datos quemados
                string consultaSQL = @"SELECT a.DocumentoAfiliado AS 'Documento de Afiliado', CONCAT_WS(' ', a.NombreAfiliado, a.ApellidoAfiliado) AS 'Nombre de Afiliado', 
                        pa.Valor AS 'Valor', pa.idReferencia AS 'Nro. de Referencia', mp.NombreMedioPago AS 'Tipo de Pago', 
                        pa.Banco AS 'Entidad Bancaría', pa.FechaHoraPago AS 'Fecha de Pago', pa.estadoPago AS 'Estado', 
                        u.NombreUsuario AS 'Nombre de Usuario', cv.NombreCanalVenta AS 'Canal de Venta' 
                        FROM pagosplanafiliado pa
                        INNER JOIN afiliadosplanes ap ON ap.idAfiliadoPlan = pa.idAfiliadoPlan
                        INNER JOIN afiliados a ON a.idAfiliado = ap.idAfiliado    
                        INNER JOIN usuarios u ON u.idUsuario = pa.idUsuario  
                        INNER JOIN empleados e ON e.DocumentoEmpleado = u.idEmpleado
                        INNER JOIN canalesventa cv ON cv.idCanalVenta = e.idCanalVenta 
                        INNER JOIN mediosdepago mp ON mp.idMedioPago = pa.idMedioPago 
                        WHERE DATE(pa.FechaHoraPago) 
                            BETWEEN '" + txbFechaIni.Value.ToString() + @"' 
                            AND '" + txbFechaFin.Value.ToString() + @"'  
                        ORDER BY pa.FechaHoraPago DESC;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
                string nombreArchivo = $"ReportesPagos_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

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


            //try
            //{
            //    // se instaló NPOI en nuguet
            //    clasesglobales cg = new clasesglobales();
            //    DataTable dt = cg.ConsultarPagosPorTipo(ddlTipoPago.SelectedValue.ToString(), txbFechaIni.Value.ToString(), txbFechaFin.Value.ToString(), out decimal valortotal);
            //    if (dt.Rows.Count == 0)
            //    {
            //        dt = cg.ConsultarPagosRecientes(out decimal valorTotal, out int totalRegistros);

            //    }
            //    string nombreArchivo = $"{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

            //    if (dt.Rows.Count > 0)
            //    {
            //        IWorkbook workbook = new XSSFWorkbook();
            //        ISheet sheet = workbook.CreateSheet("Pagos");

            //        IRow headerRow = sheet.CreateRow(0);
            //        for (int i = 0; i < dt.Columns.Count; i++)
            //        {
            //            ICell cell = headerRow.CreateCell(i);
            //            cell.SetCellValue(dt.Columns[i].ColumnName);
            //        }

            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            IRow row = sheet.CreateRow(i + 1);
            //            for (int j = 0; j < dt.Columns.Count; j++)
            //            {
            //                object value = dt.Rows[i][j];
            //                row.CreateCell(j).SetCellValue(value != DBNull.Value ? value.ToString() : "");
            //            }
            //        }

            //        for (int i = 0; i < dt.Columns.Count; i++)
            //        {
            //            sheet.AutoSizeColumn(i);
            //        }

            //        using (MemoryStream memoryStream = new MemoryStream())
            //        {
            //            workbook.Write(memoryStream);
            //            workbook.Close();

            //            byte[] byteArray = memoryStream.ToArray();

            //            Response.Clear();
            //            Response.Buffer = true;
            //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //            Response.AddHeader("Content-Disposition", $"attachment; filename={nombreArchivo}.xlsx");
            //            Response.BinaryWrite(byteArray);
            //            Response.Flush();
            //            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
            //}
        }


        protected void btnDetalle_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "mostrarDetalle")
            {
                int idAfiliadoPlan = int.Parse(e.CommandArgument.ToString());

                Literal ltDetalleModal = (Literal)Page.FindControl("ltDetalleModal");

                if (ltDetalleModal != null)
                {
                    ltDetalleModal.Text = listarDetalle(idAfiliadoPlan);
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal",
                   "setTimeout(function() { $('#ModalDetalle').modal('show'); }, 500);", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //if (rblValor.SelectedItem != null)
            //{
            listaTransaccionesPorFecha(
                4,
                Convert.ToInt32(ddlPlanes.SelectedValue.ToString()),
                txbFechaIni.Value.ToString(),
                txbFechaFin.Value.ToString());
            VentasCounter();
            VentasWeb();
            //}
            //else
            //{
            //    listaTransaccionesPorFecha(
            //        Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()),
            //        0,
            //        txbFechaIni.Value.ToString(),
            //        txbFechaFin.Value.ToString());
            //    VentasCounter();
            //    VentasWeb();
            //}
        }


        protected async void btnCobrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtenemos la fila
                Button btn = (Button)sender;
                RepeaterItem item = (RepeaterItem)btn.NamingContainer;

                // Recuperamos los datos ocultos
                HiddenField hfIdAfiliadoPlan = (HiddenField)item.FindControl("hfIdAfiliadoPlan");
                HiddenField hfIdVendedor = (HiddenField)item.FindControl("hfIdVendedor");
                HiddenField hfValor = (HiddenField)item.FindControl("hfValor");
                HiddenField hfFuentePago = (HiddenField)item.FindControl("hfFuentePago");
                HiddenField hfEmail = (HiddenField)item.FindControl("hfEmail");
                HiddenField hfNombrePlan = (HiddenField)item.FindControl("hfNombrePlan");
                HiddenField hfDocumentoAfiliado = (HiddenField)item.FindControl("hfDocumentoAfiliado");

                int idAfiliadoPlan = Convert.ToInt32(hfIdAfiliadoPlan.Value);
                int idVendedor = Convert.ToInt32(hfIdVendedor.Value);
                int valor = Convert.ToInt32(hfValor.Value);
                string fuentePago = hfFuentePago.Value;
                string correo = hfEmail.Value;
                string nombrePlan = hfNombrePlan.Value;
                string documentoAfiliado = hfDocumentoAfiliado.Value;

                // Validaciones básicas
                if (string.IsNullOrEmpty(fuentePago))
                {
                    MostrarAlerta("Error", "Este afiliado no tiene una fuente de pago registrada.", "error");
                    return;
                }

                int monto = Convert.ToInt32($"{valor}00");
                string moneda = "COP";

                string plan = hfNombrePlan.Value;
                string referencia = $"{hfDocumentoAfiliado.Value}-{DateTime.Now.ToString("yyyyMMddHHmmss")}";


                bool pagoExitoso = await CrearTransaccionRecurrenteAsync(
                    amount_in_cents: monto,
                    currency: moneda,
                    customer_email: correo, 
                    reference: referencia,
                    payment_source_id: Convert.ToInt32(fuentePago),
                    descripcion: $"Cobro recurrente del plan {plan}"
                );

                // Si fue exitoso, registramos el pago
                if (pagoExitoso)
                {
                    RegistrarPago(
                        idAfiliadoPlan, 
                        valor,
                        referencia,
                        idVendedor,
                        fuentePago
                    );

                    MostrarAlerta("Éxito", "El cobro recurrente se procesó correctamente.", "success");
                    listaTransacciones(); // refresca la tabla
                }
                else
                {
                    MostrarAlerta("Error", "El cobro no fue aprobado por la pasarela.", "error");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error inesperado", "No fue posible realizar el cobro. Revisa los logs para más detalles.", "error");
                System.Diagnostics.Debug.WriteLine($"[btnCobrar_Click] Error: {ex}");
            }
        }

        private void RegistrarPago(int idAfiliadoPlan, int valor, string referencia, int idVendedor, string idFuentePago)
        {
            try
            {
                clasesglobales cg = new clasesglobales();

                string idSiigoFactura = null;

                cg.InsertarPagoPlanAfiliadoWeb(
                    idAfiliadoPlan,
                    valor,
                    4,
                    referencia,
                    "Ninguno",
                    idVendedor, // TODO: Cambiar cuando se realice lógica [Validar que si la persona que intenta comprar un plan por la página, PERO tiene un registro en el CRM del mismo plan que está comprando por web, no queda la compra por web, sino, tiene en cuenta el CRM realizado anteriormente]
                    "Aprobado",
                    idSiigoFactura,
                    null,
                    idFuentePago,
                    DataIdTransaccion,
                    null,
                    null,
                    null
                );

            } catch(Exception ex)
            {
                MostrarAlerta("Error inesperado", "No fue posible realizar el cobro. Revisa los logs para más detalles.", "error");
                System.Diagnostics.Debug.WriteLine($"[btnCobrar_Click] Error: {ex}");
            }
        }

        // ==============================
        // MÉTODO PRINCIPAL DE COBRO RECURRENTE
        // ==============================
        public async Task<bool> CrearTransaccionRecurrenteAsync(int amount_in_cents, string currency, string customer_email, string reference, int payment_source_id, string descripcion)
        {
            try
            {
                string url = $"{Url}transactions";

                var transaccion = new
                {
                    amount_in_cents = amount_in_cents,
                    currency = currency,
                    customer_email = customer_email,
                    reference = reference,
                    description = descripcion,
                    payment_source_id = payment_source_id,
                    payment_method = new
                    {
                        installments = 1
                    }
                };

                string json = JsonConvert.SerializeObject(transaccion);

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", KeyPriv);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    string result = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MostrarAlerta("Error", $"No se pudo crear la transacción. Respuesta: {result}", "error");
                        return false;
                    }

                    // Deserializa la respuesta
                    Root3 rObjetc = JsonConvert.DeserializeObject<Root3>(result);

                    if (rObjetc?.data == null || string.IsNullOrEmpty(rObjetc.data.id))
                    {
                        MostrarAlerta("Error", "No se recibió un ID válido para la transacción recurrente.", "error");
                        return false;
                    }

                    // ==============================
                    // CONSULTA DEL ESTADO
                    // ==============================
                    DataIdTransaccion = rObjetc.data.id;

                    // Consultar estado (igual que en tu función actual)
                    string estado = null;
                    string estadoMensaje = null;
                    int maxIntentos = 15;
                    int intentos = 0;

                    do
                    {
                        await Task.Delay(1000);
                        (estado, estadoMensaje) = await ConsultarTransaccionPorReferencia(reference);
                        intentos++;
                    }
                    while (estado == "PENDING" && intentos < maxIntentos);

                    if (estado != "APPROVED")
                    {
                        MostrarAlerta("Transacción rechazada", $"{estadoMensaje ?? "Error desconocido"}", "error");
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error inesperado", "No pudimos procesar la transacción recurrente.<br>Por favor, intenta más tarde.", "error");
                System.Diagnostics.Debug.WriteLine("Error en CrearTransaccionRecurrenteAsync: " + ex);
                return false;
            }
        }

        // ==============================
        // CONSULTA DE TRANSACCIÓN POR REFERENCIA
        // ==============================
        private async Task<(string Estado, string EstadoMensaje)> ConsultarTransaccionPorReferencia(string referencia)
        {
            try
            {
                string respuesta = await GetPostConsultaTransaccionAsync(referencia);

                // Deserializar respuesta de Wompi
                var json = JsonConvert.DeserializeObject<dynamic>(respuesta);

                if (json.status == "ERROR")
                {
                    MostrarAlerta("Error al consultar", (string)json.message, "error");
                    return (null, null);
                }

                var data = json.data;
                if (data == null || data.Count == 0)
                {
                    MostrarAlerta("Sin resultados", "No se encontraron transacciones con esta referencia.", "info");
                    return (null, null);
                }

                string estado = data[0].status;
                string estadoMensaje = data[0].status_message;
                return (estado, estadoMensaje); // Ejemplo: "APPROVED", "DECLINED", "PENDING"
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error inesperado", "No se pudo consultar el estado de la transacción.", "error");
                System.Diagnostics.Debug.WriteLine("Error en ConsultarTransaccionPorReferencia: " + ex.ToString());
                return (null, null);
            }
        }

        // ==============================
        // CONSULTA HTTP GET A WOMPI
        // ==============================
        public async Task<string> GetPostConsultaTransaccionAsync(string idReferencia)
        {
            string url = $"{Url}transactions?reference={idReferencia}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", KeyPriv);

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    string result = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        return $"{{\"status\":\"ERROR\",\"message\":\"{result}\"}}";
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    return $"{{\"status\":\"ERROR\",\"message\":\"{ex.Message}\"}}";
                }
            }
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            string script = $@"
                Swal.fire({{
                    title: '{titulo}',
                    text: '{mensaje}',
                    icon: '{tipo}',
                    confirmButtonText: 'Aceptar',
                    background: '#3C3C3C'
                }});";

            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), script, true);
        }

        // ==============================
        // MODELOS NECESARIOS
        // ==============================
        public class Data3
        {
            public string id { get; set; }
            public DateTime created_at { get; set; }
            public int amount_in_cents { get; set; }
            public string reference { get; set; }
            public string customer_email { get; set; }
            public string currency { get; set; }
            public string payment_method_type { get; set; }
            public string status { get; set; }
            public string status_message { get; set; }
            public int payment_source_id { get; set; }
        }

        public class Meta { }

        public class Root3
        {
            public Data3 data { get; set; }
            public Meta meta { get; set; }
        }
    }
}