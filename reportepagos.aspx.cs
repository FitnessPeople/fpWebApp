using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Globalization;
using DocumentFormat.OpenXml.InkML;
using OfficeOpenXml;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Formula.Functions;

namespace fpWebApp
{
    public partial class reportepagos : System.Web.UI.Page
    {
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
                        }
                    }
                    listaTransacciones();
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

        private void listaTransacciones()
        {
            decimal valorTotal = 0;
            int totalRegistros = 0;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosRecientes(out valorTotal, out totalRegistros);
            rpPagos.DataSource = dt;
            rpPagos.DataBind();
            dt.Dispose();
        }

        private void listaTransaccionesPorFecha(string tipoPago, string fechaIni, string fechaFin)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosPorTipo(tipoPago, fechaIni, fechaFin, out decimal valorTotal);
            rpPagos.DataSource = dt;
            rpPagos.DataBind();
            //ltValortotalWompi.Text = valorTotal.ToString("C0");
            dt.Dispose();
        }

        private string listarDetalle(int idAfiliadoPlan)
        {
            string parametro = string.Empty;
            string nomAfiliado = string.Empty;
            string mensaje = string.Empty;
            string tipoPago = string.Empty;
            StringBuilder sb = new StringBuilder();
            clasesglobales cg = new clasesglobales();
            int idempresa = 1;

            try
            {
                DataTable dt = cg.ConsultarPagosPorId(idAfiliadoPlan);
                tipoPago = dt.Rows[0]["TipoPago"].ToString();


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
                        sb.Append($"<td>{dt.Rows[0]["TipoPago"].ToString()}</td>");
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
                        sb.Append($"<td>{dt.Rows[0]["TipoPago"].ToString()}</td>");
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
                    case "Wompi":

                        DataTable dti = cg.ConsultarUrl(idempresa);//1-Wompi 2-Armatura 

                        if (dt.Rows.Count > 0)
                        {
                            parametro = dt.Rows[0]["IdReferencia"].ToString();
                            nomAfiliado = dt.Rows[0]["NombreAfiliado"].ToString();
                        }

                        string url = dti.Rows[0]["urlTest"].ToString() + parametro;
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
                                EmailComercio = jsonData["data"]["merchant"]["email"]?.ToString(),
                                Estado3DS = jsonData["data"]["payment_method"]["extra"]["three_ds_auth"]["three_ds_auth"]["current_step_status"]?.ToString()                                }
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
                                       pa.Valor AS 'Valor', pa.idReferencia AS 'Nro. de Referencia', pa.TipoPago AS 'Tipo de Pago', 
                                       pa.Banco AS 'Entidad Bancaría', pa.FechaHoraPago AS 'Fecha de Pago', pa.estadoPago AS 'Estado', 
                                       u.NombreUsuario AS 'Nombre de Usuario', cv.NombreCanalVenta AS 'Canal de Venta'
                                       FROM pagosplanafiliado pa
                                       INNER JOIN afiliadosplanes ap ON ap.idAfiliadoPlan = pa.idAfiliadoPlan
                                       INNER JOIN afiliados a ON a.idAfiliado = ap.idAfiliado    
                                       INNER JOIN usuarios u ON u.idUsuario = pa.idUsuario  
                                       INNER JOIN empleados e ON e.DocumentoEmpleado = u.idEmpleado
                                       INNER JOIN canalesventa cv ON cv.idCanalVenta = e.idCanalVenta
                                       WHERE DATE(pa.FechaHoraPago) BETWEEN '2025-02-28' AND '2025-02-28' 
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
            listaTransaccionesPorFecha(ddlTipoPago.SelectedValue.ToString(), txbFechaIni.Value.ToString(), txbFechaFin.Value.ToString());
        }
    }
}