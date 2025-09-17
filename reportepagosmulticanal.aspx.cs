using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;

namespace fpWebApp
{
    public partial class reportepagosmulticanal : System.Web.UI.Page
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
                    ValidarPermisos("Pagos multicanal");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {

                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            txbFechaIni.Attributes.Add("type", "date");
                            txbFechaIni.Value = DateTime.Now.ToString("yyyy-MM-01").ToString();
                            txbFechaFin.Attributes.Add("type", "date");
                            txbFechaFin.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();

                            listaTransaccionesEfectivo(1, (txbFechaIni.Value.ToString()), (txbFechaFin.Value.ToString()));

                            listaTransaccionesDatafono(4, (txbFechaIni.Value.ToString()), (txbFechaFin.Value.ToString()));

                            listaTransaccionesTransferencia(2, (txbFechaIni.Value.ToString()), (txbFechaFin.Value.ToString()));

                            listaTransaccionesWompi(5, (txbFechaIni.Value.ToString()), (txbFechaFin.Value.ToString()));

                        }
                    }
                }
                else
                {
                    Response.Redirect("logout");
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

        private void listaTransaccionesEfectivo(int tipoPago, string fechaIni, string fechaFin)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosPorTipo(tipoPago, fechaIni, fechaFin, out decimal valorTotal);
            rpTipoEfectivo.DataSource = dt;
            rpTipoEfectivo.DataBind();
            ltValorTotalEfe.Text = valorTotal.ToString("C0");
            dt.Dispose();
        }

        private void listaTransaccionesDatafono(int tipoPago, string fechaIni, string fechaFin)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosPorTipo(tipoPago, fechaIni, fechaFin, out decimal valorTotal);
            rpTipoDatafono.DataSource = dt;
            rpTipoDatafono.DataBind();
            ltValorTotalData.Text = valorTotal.ToString("C0");
            dt.Dispose();
        }

        private void listaTransaccionesTransferencia(int tipoPago, string fechaIni, string fechaFin)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosPorTipo(tipoPago, fechaIni, fechaFin, out decimal valorTotal);
            rpTransferencia.DataSource = dt;
            rpTransferencia.DataBind();
            ltValorTotalTrans.Text = valorTotal.ToString("C0");
            dt.Dispose();
        }

        private void listaTransaccionesWompi(int tipoPago, string fechaIni, string fechaFin)
        {
            clasesglobales cg = new clasesglobales();
            bool rtaStatus;
            DataTable dt1 = listarDetalle(out rtaStatus);

            if (rtaStatus)
            {
                foreach (DataRow row in dt1.Rows)
                {
                    row["amount_in_cents"] = Convert.ToInt32(row["amount_in_cents"]) / 100;
                    string paymentMethod = row["payment_method_type"].ToString().ToLower();
                    row["payment_method_type"] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(paymentMethod);
                    string status = row["status"].ToString().ToLower();
                    row["status"] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(status);
                }

                DataTable dt = cg.ConsultarPagosTransaccWompi(txbFechaIni.Value.ToString(), txbFechaFin.Value.ToString(), out decimal valorTotal);
                rpWompi.DataSource = dt1;
                rpWompi.DataBind();
                ltValortotalWompi.Text = valorTotal.ToString("C0");
                dt1.Dispose();
            }
            else
            {
                if (dt1.Columns.Contains("Error") && dt1.Rows.Count > 0)
                {
                    string mensajeError = dt1.Rows[0]["Error"].ToString();
                    ltError.Text = mensajeError;
                    trError.Visible = true;
                }
                else
                {
                    ltError.Text = "Ocurrió un error desconocido.";
                    trError.Visible = true;
                }

                rpWompi.DataSource = new DataTable();
                rpWompi.DataBind();
            }
        }

        private DataTable listarDetalle(out bool rtaStatus)
        {
            int idempresa = 4; //Wompi

            clasesglobales cg = new clasesglobales();
            DataTable dti = cg.ConsultarUrl(idempresa);
            DataTable respuestaWompi = new DataTable();

            string cadena = dti.Rows[0]["urlServicioAd3"].ToString(); //string de parámetro
            string parametro = cadena
                .Replace("{from}", txbFechaIni.Value)
                .Replace("{until}", txbFechaFin.Value)
                .Replace("{page}", "1")
                .Replace("{size}", "50")
                .Replace("{order_by}", "created_at")
                .Replace("{order}", "DESC")
                .Trim('"');

            string url = dti.Rows[0]["urlTest"].ToString() + "transactions" +  parametro;
            string mensaje;
            string[] respuesta = cg.EnviarPeticionGet(url, idempresa.ToString(), out mensaje);

            JToken token = JToken.Parse(respuesta[0]);
            string prettyJson = token.ToString(Formatting.Indented);

            if (mensaje == "Ok")
            {
                bool verificar = VerificarRespuetsJson(prettyJson);
                if (verificar)
                    respuestaWompi = cg.InsertarYObtenerTransaccionesWompi(prettyJson);
                rtaStatus = true;
            }
            else
            {
                JObject jsonError = JObject.Parse(prettyJson);
                string mensajeError = jsonError.ContainsKey("error") && jsonError["error"] != null ? jsonError["error"].ToString() : "Error desconocido";
                rtaStatus = false;
                respuestaWompi = new DataTable();
                respuestaWompi.Columns.Add("Error", typeof(string));
                respuestaWompi.Rows.Add(mensajeError);
            }
            return respuestaWompi;
        }

        private bool VerificarRespuetsJson(string respuesta)
        {
            bool rta = true;
            if (respuesta.Length > 0 && !string.IsNullOrEmpty(respuesta))
            {
                JObject jsonObject = JObject.Parse(respuesta);
                int totalResults = (int)jsonObject["meta"]["total_results"];

                if (totalResults == 0)
                    rta = false;
            }
            else
                rta = false;

            return rta;
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            listaTransaccionesEfectivo(1, txbFechaIni.Value.ToString(), txbFechaFin.Value.ToString());
            listaTransaccionesDatafono(4, txbFechaIni.Value.ToString(), txbFechaFin.Value.ToString());
            listaTransaccionesTransferencia(2, txbFechaIni.Value.ToString(), txbFechaFin.Value.ToString());
            listaTransaccionesWompi(5, txbFechaIni.Value.ToString(), txbFechaFin.Value.ToString());
        }
        protected void btnExportarEfe_Click(object sender, EventArgs e)
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarPagosPorTipo(1, txbFechaIni.Value.ToString(), txbFechaFin.Value.ToString(), out decimal valortotal);
                string nombreArchivo = $"Efectivo_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("Pagos");

                    IRow headerRow = sheet.CreateRow(0);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        ICell cell = headerRow.CreateCell(i);
                        cell.SetCellValue(dt.Columns[i].ColumnName);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet.CreateRow(i + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            object value = dt.Rows[i][j];
                            row.CreateCell(j).SetCellValue(value != DBNull.Value ? value.ToString() : "");
                        }
                    }

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        workbook.Write(memoryStream);
                        workbook.Close();

                        byte[] byteArray = memoryStream.ToArray();

                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("Content-Disposition", $"attachment; filename={nombreArchivo}.xlsx");
                        Response.BinaryWrite(byteArray);
                        Response.Flush();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
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

        protected void btnExportarData_Click(object sender, EventArgs e)
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarPagosPorTipo(4, txbFechaIni.Value.ToString(), txbFechaFin.Value.ToString(), out decimal valortotal);
                string nombreArchivo = $"Datafono_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("Pagos");

                    IRow headerRow = sheet.CreateRow(0);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        ICell cell = headerRow.CreateCell(i);
                        cell.SetCellValue(dt.Columns[i].ColumnName);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet.CreateRow(i + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            object value = dt.Rows[i][j];
                            row.CreateCell(j).SetCellValue(value != DBNull.Value ? value.ToString() : "");
                        }
                    }

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        workbook.Write(memoryStream);
                        workbook.Close();

                        byte[] byteArray = memoryStream.ToArray();

                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("Content-Disposition", $"attachment; filename={nombreArchivo}.xlsx");
                        Response.BinaryWrite(byteArray);
                        Response.Flush();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
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

        protected void btnExportarTrans_Click(object sender, EventArgs e)
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarPagosPorTipo(2, txbFechaIni.Value.ToString(), txbFechaFin.Value.ToString(), out decimal valortotal);
                string nombreArchivo = $"Transferencia_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("Pagos");

                    IRow headerRow = sheet.CreateRow(0);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        ICell cell = headerRow.CreateCell(i);
                        cell.SetCellValue(dt.Columns[i].ColumnName);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet.CreateRow(i + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            object value = dt.Rows[i][j];
                            row.CreateCell(j).SetCellValue(value != DBNull.Value ? value.ToString() : "");
                        }
                    }

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        workbook.Write(memoryStream);
                        workbook.Close();

                        byte[] byteArray = memoryStream.ToArray();

                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("Content-Disposition", $"attachment; filename={nombreArchivo}.xlsx");
                        Response.BinaryWrite(byteArray);
                        Response.Flush();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
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

        protected void btnExportarWompi_Click(object sender, EventArgs e)
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarPagosTransaccWompi(txbFechaIni.Value.ToString(), txbFechaFin.Value.ToString(), out decimal valortotal);
                string nombreArchivo = $"Wompi_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("Pagos");

                    IRow headerRow = sheet.CreateRow(0);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        ICell cell = headerRow.CreateCell(i);
                        cell.SetCellValue(dt.Columns[i].ColumnName);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet.CreateRow(i + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            object value = dt.Rows[i][j];
                            row.CreateCell(j).SetCellValue(value != DBNull.Value ? value.ToString() : "");
                        }
                    }

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        workbook.Write(memoryStream);
                        workbook.Close();

                        byte[] byteArray = memoryStream.ToArray();

                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("Content-Disposition", $"attachment; filename={nombreArchivo}.xlsx");
                        Response.BinaryWrite(byteArray);
                        Response.Flush();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
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
    }
}