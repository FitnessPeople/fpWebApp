using DocumentFormat.OpenXml.Presentation;
using K4os.Compression.LZ4.Internal;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Drawing;
using System.Web.Services.Description;

namespace fpWebApp
{
    public class clasesglobales
    {
        #region Otros

        public DataTable CargarPlanesAfiliado(string idAfiliado, string Estado)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CARGAR_PLANES_AFILIADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_afiliado", idAfiliado);
                        cmd.Parameters.AddWithValue("@p_estado", Estado);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ValidarPermisos(string strPagina, string idPerfil, string idUsuario)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_PERMISOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_pagina", strPagina);
                        cmd.Parameters.AddWithValue("@p_id_perfil", Convert.ToInt32(idPerfil));
                        cmd.Parameters.AddWithValue("@p_id_usuario", Convert.ToInt32(idUsuario));

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable TraerDatos(string strQuery)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, mysqlConexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string TraerDatosStr(string strQuery)
        {
            string respuesta = string.Empty;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, mysqlConexion))
                    {
                        mysqlConexion.Open();
                        cmd.CommandType = CommandType.Text;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            cmd.ExecuteNonQuery();
                            respuesta = "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }
            return respuesta;
        }

        public DataTable TraerDatosArmatura(string strQuery)
        {
            DataTable dt = new DataTable();

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["PSIPlatformBoot"].ConnectionString;
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand(strQuery, conn))
                    {
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        try
                        {
                            dt = ds.Tables[0];
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Error: ---> " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarLog(string idUsuario, string tabla, string accion, string descripcion, string datosAnteriores, string datosNuevos)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_LOG", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_usuario", Convert.ToInt32(idUsuario));
                        cmd.Parameters.AddWithValue("@p_tabla", tabla);
                        cmd.Parameters.AddWithValue("@p_accion", accion);
                        cmd.Parameters.AddWithValue("@p_datos_anteriores", datosAnteriores);
                        cmd.Parameters.AddWithValue("@p_datos_nuevos", datosNuevos);
                        cmd.Parameters.AddWithValue("@p_descripcion_log", descripcion);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghkmnpqrstuvwxyzABCDEFGHKMNPQRSTUVWXYZ123456789*%$_-@#&?";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                var unused = res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public string GenerarCodigo(int longitud)
        {
            //const string caracteresPermitidos = "ABCDEFGHJKMNPQRSTUVWXYZ23456789";
            const string caracteresPermitidos = "1234567890";
            StringBuilder resultado = new StringBuilder(longitud);
            Random random = new Random();

            for (int i = 0; i < longitud; i++)
            {
                resultado.Append(caracteresPermitidos[random.Next(caracteresPermitidos.Length)]);
            }

            return resultado.ToString();
        }

        public void EnviarCorreo(string strRemitente, string strDestinatario, string strAsunto, string strMensaje)
        {
            MailMessage objeto_mail = new MailMessage();
            objeto_mail.From = new MailAddress(strRemitente);
            MailAddress maTo = new MailAddress(strDestinatario);
            objeto_mail.To.Add(maTo);
            objeto_mail.Subject = strAsunto;
            objeto_mail.Body = strMensaje;

            //SmtpClient client = new SmtpClient();
            //client.Host = "localhost";
            //client.Port = 25;
            //client.UseDefaultCredentials = false;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.Credentials = new System.Net.NetworkCredential("afiliaciones@fitnesspeoplecolombia.com", "i18Jo%5b3");

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); // o 465 con SSL
            client.EnableSsl = true; // o false si estás usando 465
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("sistemas@fitnesspeoplecmd.com", "rfhw romt jngp pmpo"); // Reemplaza con tu correo y contraseña de aplicación

            try
            {
                client.Send(objeto_mail);
                objeto_mail.Dispose();
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
            }

        }

        public void ExportarExcel(DataTable dt, string nombreArchivo)
        {
            if (dt.Rows.Count > 0)
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Datos");

                // Estilo para encabezados (negrita, centrado y bordes)
                ICellStyle headerStyle = workbook.CreateCellStyle();
                headerStyle.Alignment = HorizontalAlignment.Center;
                headerStyle.VerticalAlignment = VerticalAlignment.Center;
                headerStyle.BorderTop = BorderStyle.Thin;
                headerStyle.BorderBottom = BorderStyle.Thin;
                headerStyle.BorderLeft = BorderStyle.Thin;
                headerStyle.BorderRight = BorderStyle.Thin;
                IFont font = workbook.CreateFont();
                font.IsBold = true;
                headerStyle.SetFont(font);

                // Estilo para datos (bordes)
                ICellStyle borderStyle = workbook.CreateCellStyle();
                borderStyle.BorderTop = BorderStyle.Thin;
                borderStyle.BorderBottom = BorderStyle.Thin;
                borderStyle.BorderLeft = BorderStyle.Thin;
                borderStyle.BorderRight = BorderStyle.Thin;

                // Encabezados
                IRow headerRow = sheet.CreateRow(0);
                headerRow.HeightInPoints = 20; // Altura de la fila
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ICell cell = headerRow.CreateCell(i);
                    cell.SetCellValue(dt.Columns[i].ColumnName);
                    cell.CellStyle = headerStyle;
                }

                // Datos
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        object value = dt.Rows[i][j];
                        ICell cell = row.CreateCell(j);
                        cell.SetCellValue(value != DBNull.Value ? value.ToString() : "");
                        cell.CellStyle = borderStyle;
                    }
                }

                // Autoajustar columnas
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }

                // Descargar el archivo
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.Write(memoryStream);
                    workbook.Close();
                    byte[] byteArray = memoryStream.ToArray();
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", $"attachment; filename={nombreArchivo}.xlsx");
                    HttpContext.Current.Response.BinaryWrite(byteArray);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }

        // Exportar Excel Mejorado
        public void ExportarExcelEPPlus(DataTable dt, string nombreArchivo)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Datos");

                // Cargar la DataTable en la hoja (con encabezados)
                ws.Cells["A1"].LoadFromDataTable(dt, true);

                int totalRows = dt.Rows.Count + 1; // +1 por los encabezados
                int totalCols = dt.Columns.Count;

                // 1. Estilos de encabezados (primera fila)
                using (ExcelRange range = ws.Cells[1, 1, 1, totalCols])
                {
                    range.Style.Font.Bold = true; // Negrita
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Centrado
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray); // Fondo gris
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }
                ws.Row(1).Height = 20; // Altura fila encabezados

                // 2. Estilos de datos (bordes en todo el documento con info)
                using (ExcelRange range = ws.Cells[2, 1, totalRows, totalCols])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                // 3. Aplicar formato a todas las columnas de tipo fecha
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].DataType == typeof(DateTime))
                    {
                        using (ExcelRange range = ws.Cells[2, i + 1, totalRows, i + 1])
                        {
                            range.Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
                        }
                    }
                }

                // 4. Autoajustar columnas
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                // Descargar el archivo
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    package.SaveAs(memoryStream);
                    byte[] byteArray = memoryStream.ToArray();
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", $"attachment; filename={nombreArchivo}.xlsx");
                    HttpContext.Current.Response.BinaryWrite(byteArray);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }

        public string[] EnviarPeticionGet(string url, string idempresa, out string mensaje)
        {
            string[] strConjuntoResultados = new string[2];
            string resultado = "";
            string resultadoj = "";
            string auth = string.Empty;
            DataTable dt = new DataTable();

            if (idempresa == "4")
            {
                dt = ConsultarUrl(Convert.ToInt32(idempresa));
                auth = dt.Rows[0]["urlServicioAd2"].ToString();
            }

            try
            {
                WebRequest oRequest = WebRequest.Create(url);
                oRequest.Method = "GET";
                oRequest.ContentType = "application/json;charset=UTF-8";
                oRequest.Headers.Add("Authorization", auth);

                WebResponse oResponse = oRequest.GetResponse();
                using (var oSr = new StreamReader(oResponse.GetResponseStream()))
                {
                    resultado = oSr.ReadToEnd().Trim();
                    strConjuntoResultados[0] = resultado;
                    JObject jsonObj = JObject.Parse(resultado);
                    if (jsonObj["message"] != null)
                    {
                        resultadoj = jsonObj["message"].ToString();
                    }
                    else
                    {
                        resultadoj = "";
                    }
                    strConjuntoResultados[1] = resultadoj;
                    mensaje = "Ok";
                }

                return strConjuntoResultados;
            }
            catch (Exception ex)
            {
                string jsonError = JsonConvert.SerializeObject(new { error = "Error al enviar la petición: " + ex.Message });
                mensaje = "error";
                strConjuntoResultados[0] = jsonError;
                return strConjuntoResultados;
            }
        }

        public string ComputeSha256Hash(string strData)
        {
            // Crea un SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - devuelve una matriz de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(strData));

                // Convierte una matriz de bytes en una cadena
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Generador HSL -> HEX
        /// </summary>
        /// <param name="index"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public string GenerateColor(int index, int total)
        {
            double hue = (index * 360.0) / Math.Max(1, total);
            double s = 0.65;
            double l = 0.55;

            double c = (1 - Math.Abs(2 * l - 1)) * s;
            double hPrime = hue / 60.0;
            double x = c * (1 - Math.Abs(hPrime % 2 - 1));
            double r1 = 0, g1 = 0, b1 = 0;

            if (0 <= hPrime && hPrime < 1) { r1 = c; g1 = x; b1 = 0; }
            else if (1 <= hPrime && hPrime < 2) { r1 = x; g1 = c; b1 = 0; }
            else if (2 <= hPrime && hPrime < 3) { r1 = 0; g1 = c; b1 = x; }
            else if (3 <= hPrime && hPrime < 4) { r1 = 0; g1 = x; b1 = c; }
            else if (4 <= hPrime && hPrime < 5) { r1 = x; g1 = 0; b1 = c; }
            else { r1 = c; g1 = 0; b1 = x; }

            double m = l - c / 2.0;
            int r = (int)Math.Round((r1 + m) * 255);
            int g = (int)Math.Round((g1 + m) * 255);
            int b = (int)Math.Round((b1 + m) * 255);

            r = Math.Max(0, Math.Min(255, r));
            g = Math.Max(0, Math.Min(255, g));
            b = Math.Max(0, Math.Min(255, b));

            return $"#{r:X2}{g:X2}{b:X2}";
        }


        #endregion

        #region Ciudades
        public DataTable ConsultarCiudades()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CIUDADES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCiudadesPorId(int codigoCiudad)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CIUDAD", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_ciudad", codigoCiudad);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCiudadesPorNombre(string nombreCiudad)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CIUDAD_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_ciudad", nombreCiudad);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarCiudad(int idCiudad, string nombreCiudad, string nombreEstado, string codigoEstado)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_CIUDAD", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_ciudad", nombreCiudad);
                        cmd.Parameters.AddWithValue("@p_id_ciudad", idCiudad);
                        cmd.Parameters.AddWithValue("@p_nombre_estado", nombreEstado);
                        cmd.Parameters.AddWithValue("@p_codigo_estado", codigoEstado);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarCiudad(int idCiudad)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_CIUDAD", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_ciudad", idCiudad);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarCiudad(string nombreCiudad, string codigoCiudad, string nombreEstado, string codigoEstado, string nombrePais, string CodigoPais)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_CIUDAD", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_ciudad", nombreCiudad);
                        cmd.Parameters.AddWithValue("@p_codigo_ciudad", codigoCiudad);
                        cmd.Parameters.AddWithValue("@p_nombre_estado", nombreEstado);
                        cmd.Parameters.AddWithValue("@p_codigo_estado", codigoEstado);
                        cmd.Parameters.AddWithValue("@p_nombre_pais", nombrePais);
                        cmd.Parameters.AddWithValue("@p_codigo_pais", CodigoPais);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarCiudadTablas(string idCiudad)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_CIUDAD_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_ciudad", Convert.ToInt32(idCiudad));

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ValidarCiudadSedesTablas(string idCiudadSede)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_CIUDAD_SEDES_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_ciudad_sede", Convert.ToInt32(idCiudadSede));

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }



        public DataTable ConsultarDepartamentos()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_DEPARTAMENTOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCiudadesCol()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CIUDADES_COL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        #endregion

        #region Arls
        public DataTable ConsultarArls()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ARLS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarArlPorNombre(string nombreArl)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ARL_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_arl", nombreArl);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarArlPorId(int idArl)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ARL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_arl", idArl);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarArl(int idArl, string nombreArl)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_ARL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_arl", nombreArl);
                        cmd.Parameters.AddWithValue("@p_id_arl", idArl);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarArl(int idArl)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_ARL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_arl", idArl);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarArl(string nombreArl)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_ARL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_arl", nombreArl);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarArlEmpleados(int idArl)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_ARL_EMPLEADOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_arl", idArl);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        #endregion

        #region Profesiones
        public DataTable ConsultarProfesiones()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PROFESIONES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarProfesionPorNombre(string Profesion)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PROFESION_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_profesion", Profesion);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarProfesionPorId(int idProfesion)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PROFESION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_profesion", idProfesion);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarProfesion(int idProfesion, string Profesion, string Area)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_PROFESION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_profesion", idProfesion);
                        cmd.Parameters.AddWithValue("@p_profesion", Profesion);
                        cmd.Parameters.AddWithValue("@p_area", Area);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarProfesion(int idProfesion)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_PROFESION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_profesion", idProfesion);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarProfesion(string Profesion, string Area)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PROFESION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_profesion", Profesion);
                        cmd.Parameters.AddWithValue("@p_area", Area);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarProfesionTablas(int idProfesion)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_PROFESION_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_profesion", idProfesion);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        #endregion

        #region Eps
        public DataTable ConsultarEpss()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EPSS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEpsPorNombre(string nombreEps)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EPS_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_eps", nombreEps);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEpsPorId(int idEps)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EPS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_eps", idEps);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarEps(int idEps, string nombreEps)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_EPS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_eps", nombreEps);
                        cmd.Parameters.AddWithValue("@p_id_eps", idEps);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarEps(int idEps)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_EPS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_eps", idEps);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarEps(string nombreEps)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_EPS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_eps", nombreEps);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarEpsTablas(int idEps)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_EPS_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_eps", idEps);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        #endregion

        #region Fondos de Pension
        public DataTable ConsultarPensiones()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PENSIONES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPensionPorNombre(string nombrePension)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PENSION_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_pension", nombrePension);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPensionPorId(int idPension)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PENSION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_pension", idPension);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarPension(int idPension, string nombrePension)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_PENSION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_pension", nombrePension);
                        cmd.Parameters.AddWithValue("@p_id_pension", idPension);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarPension(int idPension)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_PENSION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_pension", idPension);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarPension(string nombrePension)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PENSION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_pension", nombrePension);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarPensionEmpleados(int idPension)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_PENSION_EMPLEADOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_pension", idPension);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        #endregion

        #region Fondos de Cesantias
        public DataTable ConsultarCesantias()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CESANTIAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCesantiaPorNombre(string nombreCesantia)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CESANTIA_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_cesantia", nombreCesantia);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCesantiaPorId(int idCesantia)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CESANTIA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_cesantia", idCesantia);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarCesantia(int idCesantia, string nombreCesantia)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_CESANTIA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_cesantia", nombreCesantia);
                        cmd.Parameters.AddWithValue("@p_id_cesantia", idCesantia);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarCesantia(int idCesantia)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_CESANTIA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_cesantia", idCesantia);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarCesantia(string nombreCesantia)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_CESANTIA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_cesantia", nombreCesantia);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarCesantiaEmpleados(int idCesantia)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_CESANTIA_EMPLEADOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_cesantia", idCesantia);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        #endregion

        #region Cajas de Compensacion
        public DataTable ConsultarCajasComp()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CAJASCOMP", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCajaCompPorNombre(string nombreCajaComp)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CAJACOMP_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_cajacomp", nombreCajaComp);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCajaCompPorId(int idCajaComp)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CAJACOMP", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_cajacomp", idCajaComp);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarCajaComp(int idCajaComp, string nombreCajaComp)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_CAJACOMP", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_cajacomp", nombreCajaComp);
                        cmd.Parameters.AddWithValue("@p_id_cajacomp", idCajaComp);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarCajaComp(int idCajaComp)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_CAJACOMP", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_cajacomp", idCajaComp);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarCajaComp(string nombreCajaComp)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_CAJACOMP", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_cajacomp", nombreCajaComp);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarCajaCompEmpleados(int idCajaComp)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_CAJACOMP_EMPLEADOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_cajacomp", idCajaComp);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        #endregion

        #region Paginas

        public string InsertarPagina(string Pagina, string Aspx, int idCategoria, string IconoFA)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PAGINA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_pagina", Pagina);
                        cmd.Parameters.AddWithValue("@p_pagina_aspx", Aspx);
                        cmd.Parameters.AddWithValue("@p_id_categoria", idCategoria);
                        cmd.Parameters.AddWithValue("@p_icono_pagina", IconoFA);
                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ConsultarUltimaPagina()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ULTIMA_PAGINA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public string ActualizarPagina(int idPagina, string nombrePagina, string nombreAspx, int idCategoria, string iconoPagina)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_PAGINA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_pagina", nombrePagina);
                        cmd.Parameters.AddWithValue("@p_pagina_aspx", nombreAspx);
                        cmd.Parameters.AddWithValue("@p_id_categoria", idCategoria);
                        cmd.Parameters.AddWithValue("@p_id_pagina", idPagina);
                        cmd.Parameters.AddWithValue("@p_icono_pagina", iconoPagina);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ConsultarPaginaPorNombre(string nombre_pagina)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PAGINA_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_pagina", nombre_pagina);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarPaginas()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PAGINAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarPaginaPorId(int idPagina)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PAGINA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_pagina", idPagina);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string EliminarPagina(int idPagina)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_PAGINA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_pagina", idPagina);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        #endregion

        #region Categorias Paginas

        public DataTable ConsultarCategoriasPaginas()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CATEGORIAS_PAGINAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        #endregion

        #region Perfiles
        public DataTable ConsultarPerfiles()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PERFILES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPerfilPorNombre(string nombrePerfil)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PERFIL_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_perfil", nombrePerfil);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPerfilPorId(int idPerfil)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PERFIL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_perfil", idPerfil);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarPerfil(int idPerfil, string nombrePerfil)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_PERFIL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_perfil", nombrePerfil);
                        cmd.Parameters.AddWithValue("@p_id_perfil", idPerfil);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarPerfil(int idPerfil)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_PERFIL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_perfil", idPerfil);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarPerfil(string nombrePerfil)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PERFIL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_perfil", nombrePerfil);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarPerfil(int idPerfil)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_PERFIL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_perfil", idPerfil);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarUltimoPerfil()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ULTIMO_PERFIL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        #endregion

        #region Permisos perfiles

        public DataTable ConsultarPermisosPerfiles(string nombre_pagina, int id_perfil, int id_usuario)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PERMISOS_PERFILES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_pagina", nombre_pagina);
                        cmd.Parameters.AddWithValue("@p_id_perfil", id_perfil);
                        cmd.Parameters.AddWithValue("@p_id_usuario", id_usuario);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPermisosPerfilesPorPerfil(int idPerfil)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PERMISOS_PERFILES_POR_PERFIL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_perfil", Convert.ToInt32(idPerfil));

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarPermisoPerfil(int idPerfil, int idPagina, int SinPermiso, int Consultar, int Exporta, int CreaModifica, int Borra)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PERMISOS_PERFILES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_idPerfil", idPerfil);
                        cmd.Parameters.AddWithValue("@p_idPagina", idPagina);
                        cmd.Parameters.AddWithValue("@p_SinPermiso", SinPermiso);
                        cmd.Parameters.AddWithValue("@p_Consulta", Consultar);
                        cmd.Parameters.AddWithValue("@p_Exportar", Exporta);
                        cmd.Parameters.AddWithValue("@p_CrearModificar", CreaModifica);
                        cmd.Parameters.AddWithValue("@p_Borrar", Borra);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }


        #endregion

        #region Ciudades sedes

        public DataTable ConsultarCiudadesSedes()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CIUDADES_SEDES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarCiudadSedePorId(int idCiudadSede)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CIUD_SEDE_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_ciudad_sede", idCiudadSede);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarCiudadSede(string nombreCiudadSede)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_CIUDAD_SEDE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_ciu_sede", nombreCiudadSede);
                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarCiudadSede(int idCiudadSede)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_CIUDAD_SEDE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_ciudad_sede", idCiudadSede);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarCiudadSede(int idCiudadSede, string nombreCiudadSede)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_CIUDAD_SEDE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_ciudad_sede", idCiudadSede);
                        cmd.Parameters.AddWithValue("@p_nombre_ciudad_sede", nombreCiudadSede);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }
            return respuesta;
        }

        public DataTable ConsultarCiudadSedePorNombre(string nombreCiudadSede)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CIUD_SEDE_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_ciudad_sede", nombreCiudadSede);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        #endregion

        #region Sedes

        public DataTable ConsultarSedes()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_SEDES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarSedePorId(int idSede)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_SEDES_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarSedePorNombre(string nombreSede)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_SEDES_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_sede", nombreSede);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultaCargarSedesPorId(int? idSede, string p_clase_sede)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CARGAR_SEDES_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede.HasValue ? idSede.Value : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_clase_sede", p_clase_sede);// Gimnasio / Oficina / Todos 

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultaCargarSedes(string p_clase_sede)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CARGAR_SEDES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_clase_sede", p_clase_sede);// Gimnasio / Oficina / Todos 

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public string InsertarSede(string nombreSede, string direccionSede, int idCiudadSede, string telefonoSede, string horarioSede, string googleMaps, string tipoSede, string claseSede)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_SEDE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_sede", nombreSede);
                        cmd.Parameters.AddWithValue("@p_direccion_sede", direccionSede);
                        cmd.Parameters.AddWithValue("@p_id_ciudad_sede", idCiudadSede);
                        cmd.Parameters.AddWithValue("@p_telefono_sede", telefonoSede);
                        cmd.Parameters.AddWithValue("@p_horario_sede", horarioSede);
                        cmd.Parameters.AddWithValue("@p_google_maps", googleMaps);
                        cmd.Parameters.AddWithValue("@p_tipo_sede", tipoSede);
                        cmd.Parameters.AddWithValue("@p_clase_sede", claseSede);
                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarSede(int idSede, string nombreSede, string direccionSede, int idCiudadSede, string telefonoSede, string horarioSede, string tipoSede, string claseSede)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_SEDE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);
                        cmd.Parameters.AddWithValue("@p_nombre_sede", nombreSede);
                        cmd.Parameters.AddWithValue("@p_direccion_sede", direccionSede);
                        cmd.Parameters.AddWithValue("@p_id_ciudad_sede", idCiudadSede);
                        cmd.Parameters.AddWithValue("@p_telefono_sede", telefonoSede);
                        cmd.Parameters.AddWithValue("@p_horario_sede", horarioSede);
                        cmd.Parameters.AddWithValue("@p_tipo_sede", tipoSede);
                        cmd.Parameters.AddWithValue("@p_clase_sede", claseSede);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }
            return respuesta;
        }

        public string EliminarSede(int idSede)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_SEDE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }


        #endregion

        #region Tipos de documento

        public DataTable ConsultartiposDocumento()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_TIPOS_DOCUMENTO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultartiposDocumentoPorId(int idTipoDocumento)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_TIPO_DOC_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_tipo_doc", idTipoDocumento);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarTiposDocumentoPorNombre(string nombreTipoDocumento)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_TIPO_DOC_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_tipo_documento", nombreTipoDocumento);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }
        public string InsertarTipoDocumento(string tipoDocumento, string siglaDocumento)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_TIPO_DOCUMENTO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_tipo_documento", tipoDocumento);
                        cmd.Parameters.AddWithValue("@p_sigla_documento", siglaDocumento);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarTipoDocumento(int idTipoDocumento, string tipoDocumento, string sigladocumento)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_TIPO_DOCUMENTO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_tipo_documento", idTipoDocumento);
                        cmd.Parameters.AddWithValue("@p_tipo_documento", tipoDocumento);
                        cmd.Parameters.AddWithValue("@p_sigla_documento", sigladocumento);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarTipoDocumento(int idTipoDocumento)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_TIPO_DOCUMENTO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_tipo_documento", idTipoDocumento);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarTiposDocumentoTablas(int idTipoDocumento)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_TIPO_DOCUMENTO_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_tipo_documento", idTipoDocumento);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        #endregion

        #region Genero

        public DataTable ConsultarGeneros()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GENEROS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarGeneroPorId(int idGenero)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GENERO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_genero", idGenero);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarGenerosPorNombre(string genero)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GENERO_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_genero", genero);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarGenero(string genero)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_GENERO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_genero", genero);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarGenero(int idGenero)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_GENERO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_genero", idGenero);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarGeneroTablas(string idGenero)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_GENERO_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_genero", Convert.ToInt32(idGenero));

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarGenero(int idGenero, string genero)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_GENERO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_genero", idGenero);
                        cmd.Parameters.AddWithValue("@p_genero", genero);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        #endregion

        #region Estado civil

        public DataTable ConsultarEstadosCiviles()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTADOS_CIVILES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEstadoCivilPorId(int idEstadoCivil)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTADO_CIVIL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_estado_civil", idEstadoCivil);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEstadoCivilPorNombre(string estadoCivil)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTADO_CIVIL_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p__estado_civil", estadoCivil);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarEstadoCivil(string estadoCivil)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_ESTADO_CIVIL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_estado_civil", estadoCivil);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarEstadoCivil(int idEstadoCivil, string estadoCivil)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_ESTADO_CIVIL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_estado_civil", idEstadoCivil);
                        cmd.Parameters.AddWithValue("@p_estado_civil", estadoCivil);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarEstadoCivil(int idEstadoCivil)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_ESTADO_CIVIL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_estado_civil", idEstadoCivil);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarEstadoCivilTablas(string idEstadoCivil)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_ESTADO_CIVIL_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_estado_civil", Convert.ToInt32(idEstadoCivil));

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        #endregion

        #region Objetivos del afiliado

        public DataTable ConsultarObjetivosAfiliados()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_OBJETIVOS_AFILIADOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarObjetivoAfiliadoPorId(int idObjetivoAfiliadol)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_OBJETIVO_AFILIADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_objetivo", idObjetivoAfiliadol);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarObjetivoAfiliadoPorNombre(string objetivo)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_OBJETIVO_AFILIADO_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_objetivo", objetivo);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarObjetivoAfiliado(string objetivo)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_OBJETIVO_AFILIADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_objetivo", objetivo);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarObjetivoAfiliado(int idObjetivo, string objetivo)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_OBJETIVO_AFILIADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_objetivo", idObjetivo);
                        cmd.Parameters.AddWithValue("@p_objetivo", objetivo);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarObjetivoAfiliado(int idObjetivoAfiliado)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_OBJETIVO_AFILIADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_objetivo", idObjetivoAfiliado);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarObjetivoAfiliadoTablas(string idObjetivo)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_OBJETIVO_AFILIADO_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_objetivo", Convert.ToInt32(idObjetivo));

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        #endregion

        #region Preguntas ParQ

        public DataTable ConsultarPreguntasParq()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PREGUNTAS_PARQ", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPreguntaParQPorId(int idParq)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PREGUNTA_PARQ", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_parq", idParq);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPreguntaParQPorNombre(string preguntaParq)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PREGUNTA_PARQ_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_pregunta_parq", preguntaParq);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarPreguntaParQ(string preguntaParQ, out int _idParQ)
        {
            string respuesta = string.Empty;
            _idParQ = 0;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PREGUNTA_PARQ", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_pregunta_parq", preguntaParQ);

                        // Parámetro de salida
                        MySqlParameter idParQ = new MySqlParameter("@p_pregunta_id", MySqlDbType.Int32);
                        idParQ.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(idParQ);

                        cmd.ExecuteNonQuery();
                        _idParQ = Convert.ToInt32(idParQ.Value);

                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarPreguntaParQ(int idParQ, string preguntaParQ, string estadoParQ, int orden)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_PREGUNTA_PARQ", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_parq", idParQ);
                        cmd.Parameters.AddWithValue("@p_pregunta_parq", preguntaParQ);
                        cmd.Parameters.AddWithValue("@p_estado_parq", estadoParQ);
                        cmd.Parameters.AddWithValue("@p_orden", orden);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarPreguntaParQ(int idParQ)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_PREGUNTA_PARQ", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_parq", idParQ);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarPreguntaParQTablas(int idparQ)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_PREGUNTAS_PARQ_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_parq", idparQ);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        #endregion

        #region Tipos de Incapacidad

        public DataTable ConsultarTiposIncapacidades()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_TIPOS_INCAPACIDAD", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarTipoIncapacidadPorId(int idTipoIncapacidad)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_TIPO_INCAPACIDAD", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_tipo_incapacidad", idTipoIncapacidad);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarTipoIncapacidadPorNombre(string tipoIncapacidad)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_TIPO_INCAPACIDAD_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_tipo_incapacidad", tipoIncapacidad);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarTipoIncapacidad(string tipoIncapacidad)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_TIPO_INCAPACIDAD", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_tipo_incapacidad", tipoIncapacidad);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarTipoIncapacidad(int idTipoIncapacidad, string tipoIncapacidad)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_TIPO_INCAPACIDAD", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_tipo_incapacidad", idTipoIncapacidad);
                        cmd.Parameters.AddWithValue("@p_tipo_incapacidad", tipoIncapacidad);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarTipoIncapacidad(int idTipoIncapacidad)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_TIPO_INCAPACIDAD", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_tipo_incapacidad", idTipoIncapacidad);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarTipoIncapacidadTablas(int idTipoIncapacidad)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_TIPO_INCAPACIDAD_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_tipo_incapacidad", idTipoIncapacidad);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        #endregion

        #region Reportes

        public DataTable ConsultarPagosPlanAfiliados()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PAGOS_PLAN_AFILIADOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        //public DataTable ConsultarPagosRecientes()
        //{
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
        //        using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
        //        {
        //            using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PAGOS_RECIENTES", mysqlConexion))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
        //                {
        //                    mysqlConexion.Open();
        //                    dataAdapter.Fill(dt);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        dt = new DataTable();
        //        dt.Columns.Add("Error", typeof(string));
        //        dt.Rows.Add(ex.Message);
        //    }

        //    return dt;
        //}

        public DataTable ConsultarPagosRecientes(out decimal valorTotal, out int totalRegistros)
        {
            DataTable dt = new DataTable();
            valorTotal = 0;
            totalRegistros = 0;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PAGOS_RECIENTES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        MySqlParameter ValorTotal = new MySqlParameter("@p_total_valor", MySqlDbType.Decimal);
                        MySqlParameter TotalRegistros = new MySqlParameter("@v_total_registros", MySqlDbType.Int32);

                        ValorTotal.Direction = ParameterDirection.Output;
                        TotalRegistros.Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(ValorTotal);
                        cmd.Parameters.Add(TotalRegistros);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);

                            if (ValorTotal.Value != DBNull.Value)
                            {
                                valorTotal = Convert.ToDecimal(ValorTotal.Value);
                            }
                            if (TotalRegistros.Value != DBNull.Value)
                            {
                                totalRegistros = Convert.ToInt32(TotalRegistros.Value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPagosRecientes()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PAGOS_RECIENTES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        public DataTable ConsultarPagosPorId(int idAfiliadoPlan)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PAGOS_TRANSAC_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_pago", idAfiliadoPlan);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPagosPorTipo(int idCanalVenta, int tipoPago, int idPlan, string fechaIni, string fechaFin, out decimal valorTotal)
        {
            DataTable dt = new DataTable();
            valorTotal = 0;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PAGOS_POR_TIPO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_tipo_pago", tipoPago);
                        cmd.Parameters.AddWithValue("@p_id_plan", idPlan);
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_fecha_ini", fechaIni);
                        cmd.Parameters.AddWithValue("@p_fecha_fin", fechaFin);

                        // Parámetro de salida
                        MySqlParameter ValorTotal = new MySqlParameter("@p_total_valor", MySqlDbType.Decimal);
                        ValorTotal.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(ValorTotal);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);

                            if (ValorTotal.Value != DBNull.Value)
                            {
                                valorTotal = Convert.ToDecimal(ValorTotal.Value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPagosTransaccWompi(string fechaIni, string fechaFin, out decimal valorTotal)
        {
            DataTable dt = new DataTable();
            valorTotal = 0;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_TRANSAC_WOMPI", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_fecha_ini", fechaIni);
                        cmd.Parameters.AddWithValue("@p_fecha_fin", fechaFin);
                        // Parámetro de salida
                        MySqlParameter ValorTotal = new MySqlParameter("@p_total_sum", MySqlDbType.Decimal);
                        ValorTotal.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(ValorTotal);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);

                            if (ValorTotal.Value != DBNull.Value)
                            {
                                valorTotal = Convert.ToDecimal(ValorTotal.Value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        public DataTable ConsultarUrl(int idIntegracion)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_URL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_integracion", idIntegracion);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable InsertarYObtenerTransaccionesWompi(string json)
        {
            DataTable dataTable = new DataTable();
            try
            {
                var data = JsonConvert.DeserializeObject<Root>(json);
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(strConexion))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_DATOS_JSON_WOMPI", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var item in data.data)
                        {
                            cmd.Parameters.Clear();


                            //Validación de fecha/hora ss
                            DateTime createdAt, finalizedAt;

                            string normalizedCreatedAt = NormalizeDate(item.created_at);
                            string normalizedFinalizedAt = NormalizeDate(item.finalized_at);

                            bool createdAtValid = DateTime.TryParseExact(normalizedCreatedAt,
                                "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out createdAt);

                            bool finalizedAtValid = DateTime.TryParseExact(normalizedFinalizedAt,
                                "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out finalizedAt);

                            cmd.Parameters.AddWithValue("@p_id", item.id);
                            cmd.Parameters.AddWithValue("@p_created_at", createdAtValid ? (object)createdAt : DBNull.Value);
                            cmd.Parameters.AddWithValue("@p_finalized_at", finalizedAtValid ? (object)finalizedAt : DBNull.Value);
                            cmd.Parameters.AddWithValue("@p_amount_in_cents", item.amount_in_cents);
                            cmd.Parameters.AddWithValue("@p_reference", item.reference);
                            cmd.Parameters.AddWithValue("@p_customer_email", item.customer_email);
                            cmd.Parameters.AddWithValue("@p_currency", item.currency);
                            cmd.Parameters.AddWithValue("@p_payment_method_type", item.payment_method_type);
                            cmd.Parameters.AddWithValue("@p_status", item.status);
                            cmd.Parameters.AddWithValue("@p_status_message", (object)item.status_message ?? DBNull.Value);

                            cmd.Parameters.AddWithValue("@p_device_id", string.IsNullOrEmpty(item.customer_data?.device_id) ? DBNull.Value : (object)item.customer_data.device_id);
                            cmd.Parameters.AddWithValue("@p_full_name", string.IsNullOrEmpty(item.customer_data?.full_name) ? DBNull.Value : (object)item.customer_data.full_name);
                            cmd.Parameters.AddWithValue("@p_phone_number", string.IsNullOrEmpty(item.customer_data?.phone_number) ? DBNull.Value : (object)item.customer_data.phone_number);

                            var extra = item.payment_method?.extra;
                            cmd.Parameters.AddWithValue("@p_card_bin", extra?.bin ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@p_card_name", extra?.name ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@p_card_brand", extra?.brand ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@p_card_exp_year", extra?.exp_year ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@p_card_exp_month", extra?.exp_month ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@p_card_last_four", extra?.last_four ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@p_card_holder", extra?.card_holder ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@p_card_type", extra?.card_type ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@p_is_three_ds", extra?.is_three_ds ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@p_three_ds_step", extra?.three_ds_auth?.three_ds_auth?.current_step ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@p_three_ds_status", extra?.three_ds_auth?.three_ds_auth?.current_step_status ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@p_external_identifier", extra?.external_identifier ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@p_processor_response_code", extra?.processor_response_code ?? (object)DBNull.Value);

                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                            {
                                adapter.Fill(dataTable);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }

            return dataTable;
        }

        string NormalizeDate(string date)
        {
            if (string.IsNullOrEmpty(date))
                return date;

            // Eliminar la 'Z' si está presente y recortar milisegundos si existen
            int dotIndex = date.IndexOf('.');
            if (dotIndex > 0)
            {
                date = date.Substring(0, dotIndex); // Recortar desde el punto en adelante
            }
            else if (date.EndsWith("Z"))
            {
                date = date.TrimEnd('Z'); // Si no tiene milisegundos pero tiene 'Z', la quitamos
            }

            return date;
        }

        #endregion

        #region Planes

        public DataTable ConsultarPlanes()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PLANES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPlanPorId(int idPlan)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PLAN", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_plan", idPlan);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ValidarPlanAfiliados(int idPlan)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_PLAN_AFILIADOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_plan", idPlan);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPlanPorNombre(string NombrePlan)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PLAN_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_plan", NombrePlan);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarPlan(string nombrePlan, string descripcionPlan, int precioTotal, int precioBase, int meses,
            int mesesCortesia, string color, int idUsuario, double diasCongelamiento, string fechaInicio,
            string fechaFinal, int permanente, int debitoAutomatico)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PLAN", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre", nombrePlan);
                        cmd.Parameters.AddWithValue("@p_descripcion", descripcionPlan);
                        cmd.Parameters.AddWithValue("@p_precio_total", precioTotal);
                        cmd.Parameters.AddWithValue("@p_precio_base", precioBase);
                        cmd.Parameters.AddWithValue("@p_meses", meses);
                        cmd.Parameters.AddWithValue("@p_meses_cortesia", mesesCortesia);
                        cmd.Parameters.AddWithValue("@p_color_plan", color);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_dias_congelamiento", diasCongelamiento);
                        cmd.Parameters.AddWithValue("@p_fecha_inicial", fechaInicio);
                        cmd.Parameters.AddWithValue("@p_fecha_final", fechaFinal);
                        cmd.Parameters.AddWithValue("@p_permanente", permanente);
                        cmd.Parameters.AddWithValue("@p_debito_automatico", debitoAutomatico);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarPlan(int idPlan, string nombre, string descripcion, int precioTotal, int precioBase, int meses, int mesesCortesia,
            string color, int idUsuario, int diasCongelamiento, string fechaInicial, string fechaFinal, int permanente, int debitoAutomatico)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_PLAN", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_plan", idPlan);
                        cmd.Parameters.AddWithValue("@p_nombre", nombre);
                        cmd.Parameters.AddWithValue("@p_descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@p_precio_total", precioTotal);
                        cmd.Parameters.AddWithValue("@p_precio_base", precioBase);
                        cmd.Parameters.AddWithValue("@p_meses", meses);
                        cmd.Parameters.AddWithValue("@p_meses_cortesia", mesesCortesia);
                        cmd.Parameters.AddWithValue("@p_color_plan", color);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_dias_congelamiento", diasCongelamiento);
                        cmd.Parameters.AddWithValue("@p_fecha_inicial", fechaInicial);
                        cmd.Parameters.AddWithValue("@p_fecha_final", fechaFinal);
                        cmd.Parameters.AddWithValue("@p_permanente", permanente);
                        cmd.Parameters.AddWithValue("@p_debito_automatico", debitoAutomatico);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarPlanWeb(int idPlan, string tituloPlan, string descripcionPlanWeb,
            string bannerWeb, string imagenMarketing, string enlacePago)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_PLAN_WEB", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_plan", idPlan);
                        cmd.Parameters.AddWithValue("@p_titulo_plan", tituloPlan);
                        cmd.Parameters.AddWithValue("@p_descripcion_plan_web", descripcionPlanWeb);
                        cmd.Parameters.AddWithValue("@p_banner_web", bannerWeb);
                        cmd.Parameters.AddWithValue("@p_imagen_marketing", imagenMarketing);
                        cmd.Parameters.AddWithValue("@p_enlace_pago", enlacePago);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarPlan(int idPlan)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_PLAN", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_plan", idPlan);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ConsultarPlanesWeb()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PLANES_WEB", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPlanesVigentesVisibleCRM()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PLANES_VIGENTES_VISIBLESCRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPlanesVigentes()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PLANES_VIGENTES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarUltimoAfilEnAfiliadosPlan()

        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ULTIMO_AFILIADO_EN_AFILIADOS_PLAN", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        //public string InsertarAfiliadoPlan(int idAfiliado, int idPlan, string fechaInicio, string fechaFin, int meses,
        //    int valor, string observaciones, string estado, out int ultimoRegistro)
        //{
        //    string mensaje = "";
        //    ultimoRegistro = 0; // Inicializamos

        //    try
        //    {
        //        string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

        //        using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
        //        {
        //            mysqlConexion.Open();

        //            using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_AFILIADO_PLAN", mysqlConexion))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                // Parámetros de entrada
        //                cmd.Parameters.AddWithValue("@p_id_afiliado", idAfiliado);
        //                cmd.Parameters.AddWithValue("@p_id_plan", idPlan);
        //                cmd.Parameters.AddWithValue("@p_fecha_inicio", fechaInicio);
        //                cmd.Parameters.AddWithValue("@p_fecha_fin", fechaFin);
        //                cmd.Parameters.AddWithValue("@p_meses", meses);
        //                cmd.Parameters.AddWithValue("@p_valor", valor);
        //                cmd.Parameters.AddWithValue("@p_observaciones", observaciones);
        //                cmd.Parameters.AddWithValue("@p_estado", estado);

        //                // Parámetros de salida
        //                var pUltimoRegistro = new MySqlParameter("@p_ultimo_registro", MySqlDbType.Int32)
        //                {
        //                    Direction = ParameterDirection.Output
        //                };
        //                cmd.Parameters.Add(pUltimoRegistro);

        //                var pMensaje = new MySqlParameter("@p_mensaje", MySqlDbType.VarChar, 300)
        //                {
        //                    Direction = ParameterDirection.Output
        //                };
        //                cmd.Parameters.Add(pMensaje);

        //                // Ejecutar procedimiento
        //                cmd.ExecuteNonQuery();

        //                // Leer valores de salida
        //                ultimoRegistro = Convert.ToInt32(pUltimoRegistro.Value);
        //                mensaje = pMensaje.Value?.ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        mensaje = "ERROR: " + ex.Message;
        //    }

        //    return mensaje;
        //}


        public string InsertarAfiliadoPlan(int idAfiliado, int idPlan, string fechaInicio, string fechaFin, int meses,
        int valor, string observaciones, string estado)
        {
            string mensaje = "";
            int idAfiliadoPlan = 0;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_AFILIADO_PLAN", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_afiliado", idAfiliado);
                        cmd.Parameters.AddWithValue("@p_id_plan", idPlan);
                        cmd.Parameters.AddWithValue("@p_fecha_inicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@p_fecha_fin", fechaFin);
                        cmd.Parameters.AddWithValue("@p_meses", meses);
                        cmd.Parameters.AddWithValue("@p_valor", valor);
                        cmd.Parameters.AddWithValue("@p_observaciones", observaciones);
                        cmd.Parameters.AddWithValue("@p_estado", estado);
                        
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idAfiliadoPlan = Convert.ToInt32(reader["idAfiliadoPlan"]);
                            }
                        }
                    }
                }  
                mensaje = $"OK|{idAfiliadoPlan}";
            }
            catch (Exception ex)
            {
                mensaje = "ERROR: " + ex.Message;
            }
            return mensaje;
        }


        public DataTable ConsultarAfiliadoPlanPorDocumento(string nroDocumento)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_AFILIADO_PLAN_POR_DOCUMENTO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_documento_afiliado", nroDocumento);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarPreguntasPARQporIdAfiliado(int idAfiliado)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PREGUNTAS_PARQ_POR_ID_AFILIADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_afiliado", idAfiliado);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarEmpresasAfiliadas()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EMPRESAS_AFILIADAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarExisteDocdAfiliado(string docAfiliado)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EXISTE_DOCUMENTO_AFILIADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_documento_afiliado", docAfiliado);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }


        #endregion

        #region Agenda

        public DataTable ConsultaCargarAgenda(int idSede)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CARGAR_AGENDA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultaCargarAgendaPorSedePorEspecialidad(int idSede, int idEspecialidad)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CARGAR_AGENDA_POR_SEDE_POR_ESPECIALIDAD", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);
                        cmd.Parameters.AddWithValue("@p_id_especialidad", idEspecialidad);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultaCargarAgendaPorSedePorEspecialista(int idSede, int idEspecialista)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CARGAR_AGENDA_POR_SEDE_POR_ESPECIALISTA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);
                        cmd.Parameters.AddWithValue("@p_id_especialista", idEspecialista);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultaCargarAgendaPorEspecialista(int idUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CARGAR_AGENDA_ESPECIALISTA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultaCargarEspecialistas()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CARGAR_ESPECIALISTA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        #endregion

        #region Asesores
        public DataTable ConsultaCargarAsesores()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CARGAR_ASESORES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultaCargarAsesoresActivos()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CARGAR_ASESORES_ACTIVOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEfectividadGestionCRM(int idCanalVenta, DateTime? FechaIni, DateTime? FechaFin, int idUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EFECTIVIDAD_GESTION_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_fecha_inicio", FechaIni);
                        cmd.Parameters.AddWithValue("@p_fecha_fin", FechaFin);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarIndicadorGeneroCRM(int idCanalVenta, DateTime? FechaIni, DateTime? FechaFin, int idUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INDICADOR_GENERO_GESTION_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_fecha_inicio", FechaIni);
                        cmd.Parameters.AddWithValue("@p_fecha_fin", FechaFin);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarIndicadorEdadesCRM(int idCanalVenta, DateTime? FechaIni, DateTime? FechaFin, int idUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INDICADOR_EDADES_GESTION_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_fecha_inicio", FechaIni);
                        cmd.Parameters.AddWithValue("@p_fecha_fin", FechaFin);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarIndicadorPlanesSeleccionados(int idCanalVenta, DateTime? FechaIni, DateTime? FechaFin, int idUsuario, int limite)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INDICADOR_PLANES_MAS_CONSULTADOS_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_fecha_inicio", FechaIni);
                        cmd.Parameters.AddWithValue("@p_fecha_fin", FechaFin);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_limite", limite);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        public DataTable ConsultarIndicadorEstadosVentaCRM(int idCanalVenta, DateTime? FechaIni, DateTime? FechaFin, int idUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INDICADOR_ESTADOVENTA_GESTION_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_fecha_inicio", FechaIni);
                        cmd.Parameters.AddWithValue("@p_fecha_fin", FechaFin);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        public DataTable ConsultarIndicadorGestionAsesorCRM(int idCanalVenta, DateTime? FechaIni, DateTime? FechaFin, int idUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INDICADOR_ASESOR_GESTION_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_fecha_inicio", FechaIni);
                        cmd.Parameters.AddWithValue("@p_fecha_fin", FechaFin);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        #endregion

        #region Congelaciones

        public DataTable ConsultaCargarCongelaciones(int idAfiliado)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CARGAR_CONGELACIONES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_afiliado", idAfiliado);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        #endregion

        #region Canales de venta

        public DataTable ConsultarMetasComerciales()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_METAS_COMERCIALES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarMetaComercial(int CanalVenta, int mes, int annio)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_META_COMERCIAL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", CanalVenta);
                        cmd.Parameters.AddWithValue("@p_mes", mes);
                        cmd.Parameters.AddWithValue("@p_annio", annio);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarMetaComercialPorId(int idMeta)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_META_COMERCIAL_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_meta", idMeta);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarMetaComercial(int idCanalVenta, int Mes, int Annio,
            int Valor, int Senior, int Junior, int Elite, int Director, int idUsuario)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_META_COMERCIAL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_mes", Mes);
                        cmd.Parameters.AddWithValue("@p_annio", Annio);
                        cmd.Parameters.AddWithValue("@p_valor", Valor);
                        cmd.Parameters.AddWithValue("@p_senior", Senior);
                        cmd.Parameters.AddWithValue("@p_junior", Junior);
                        cmd.Parameters.AddWithValue("@p_elite", Elite);
                        cmd.Parameters.AddWithValue("@p_director", Director);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarMetaComercial(int idMeta, int idCanalVenta, int Mes, int Annio, 
            int Valor, int Senior, int Junior, int Elite, int Director, int idUsuario)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_META_COMERCIAL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_meta", idMeta);
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_mes", Mes);
                        cmd.Parameters.AddWithValue("@p_annio", Annio);
                        cmd.Parameters.AddWithValue("@p_valor", Valor);
                        cmd.Parameters.AddWithValue("@p_senior", Senior);
                        cmd.Parameters.AddWithValue("@p_junior", Junior);
                        cmd.Parameters.AddWithValue("@p_elite", Elite);
                        cmd.Parameters.AddWithValue("@p_director", Director);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarMetaComercial(int idMeta)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_META_COMERCIAL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_meta", idMeta);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ConsultarCanalesVenta()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CANALES_VENTA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCanalesVentaPorId(int idCanalVenta)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CANALES_VENTA_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCanalesVentaPorNombre(string nombreCanalVenta)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CANALES_VENTA_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_canal_venta", nombreCanalVenta);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarCanalVenta(string nombreCanalVenta)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_CANAL_VENTA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_canal_venta", nombreCanalVenta);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarCanalVenta(int idCanalVEnta, string nombreCanalVenta)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_CANAL_VENTA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVEnta);
                        cmd.Parameters.AddWithValue("@p_nombre_canal_venta", nombreCanalVenta);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarCanalVenta(int idCanalVenta)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_CANAL_VENTA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarCanalVentaTablas(int idCanalVenta)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_CANAL_VENTA_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        #endregion

        #region Cargos


        public DataTable ConsultarCargos()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CARGOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCargosPorId(int idCargo)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CARGOS_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_cargo", idCargo);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCargoPorNombre(string nombreCargo)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CARGOS_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p__estado_civil", nombreCargo);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarCargo(string nombreCargo)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_CARGO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_cargo", nombreCargo);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarCargo(int idCargo, string nombreCargo)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_CARGO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_cargo", idCargo);
                        cmd.Parameters.AddWithValue("@p_nombre_cargo", nombreCargo);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarCargo(int idCargo)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_CARGO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_cargo", idCargo);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarCargoTablas(int idCargo)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_CARGOS_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_cargo", idCargo);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        #endregion

        #region Empleados

        public DataTable ConsultarEmpleados()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EMPLEADOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEmpleadosPorSede(string strSede)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EMPLEADOS_POR_SEDE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_idSede", strSede);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarEstadoEmpleadoPorFechaFinal()
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_ESTADO_EMPLE_POR_FECHA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ConsultarExisteDocEmpleado(string documento)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EXISTE_DOCUMENTO_EMPLEADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_documento_empleado", documento);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        public DataTable ConsultarExisteEmailEmpleado(string email)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EXISTE_EMAIL_EMPLEADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_email_empleado", email);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarExisteTelEmpleado(string telefono)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EXISTE_TELEFONO_EMPLEADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_telefono_empleado", telefono);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }



        public string InsertarNuevoEmpleado(string documentoEmpleado, int tipoDocumento, string nombreEmpleado, string telEmpleado, 
            string telEmpleadoCorp, string emailEmpleado, string emailEmpleadoCorp, string dirEmpleado, int idCiudadEmpleado, 
            string fechaNacEmpleado, string fotoEmpleado, string nroContrato, string tipoContrato, int idEmpresaFP, 
            int idSede, string fechaIni, string fechaFin, int sueldo, string grupoNomina, int idEps, 
            int idFondo, int idArl, int idCajaCompensa, int idCesantias, string estadoEmpleado, int idGenero, 
            int idEstadoCivil, int idCanalVenta, int idCargo, int idProfesion, string nivelEstudio, 
            int estratoSocial, string tipoVivienda, int nroPersonas, string actividadExtra, 
            string consumeLicor, string medioTransporte, string tipoSangre)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_EMPLEADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_documento_empleado", documentoEmpleado);
                        cmd.Parameters.AddWithValue("@p_tipo_doc_empleado", tipoDocumento);
                        cmd.Parameters.AddWithValue("@p_nombre_empleado", nombreEmpleado);
                        cmd.Parameters.AddWithValue("@p_tel_empleado", telEmpleado);
                        cmd.Parameters.AddWithValue("@p_tel_empleado_corp", telEmpleadoCorp);
                        cmd.Parameters.AddWithValue("@p_email_empleado", emailEmpleado);
                        cmd.Parameters.AddWithValue("@p_email_empleado_corp", emailEmpleadoCorp);
                        cmd.Parameters.AddWithValue("@p_dir_empleado", dirEmpleado);
                        cmd.Parameters.AddWithValue("@p_id_ciu_empleado", idCiudadEmpleado);
                        cmd.Parameters.AddWithValue("@p_fecha_nac_empleado", fechaNacEmpleado);
                        cmd.Parameters.AddWithValue("@p_foto_empleado", fotoEmpleado);
                        cmd.Parameters.AddWithValue("@p_nro_contrato", nroContrato);
                        cmd.Parameters.AddWithValue("@p_tipo_contrato", tipoContrato);
                        cmd.Parameters.AddWithValue("@p_id_empresa_fp", idEmpresaFP);
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);
                        cmd.Parameters.AddWithValue("@p_fecha_inicio", fechaIni);
                        cmd.Parameters.AddWithValue("@p_fecha_fin", fechaFin);
                        cmd.Parameters.AddWithValue("@p_sueldo", sueldo);
                        cmd.Parameters.AddWithValue("@p_grupo_nomina", grupoNomina);
                        cmd.Parameters.AddWithValue("@p_id_eps", idEps);
                        cmd.Parameters.AddWithValue("@p_id_fondo_pension", idFondo);
                        cmd.Parameters.AddWithValue("@p_id_arl", idArl);
                        cmd.Parameters.AddWithValue("@p_id_caja_comp", idCajaCompensa);
                        cmd.Parameters.AddWithValue("@p_cesantias", idCesantias);
                        cmd.Parameters.AddWithValue("@p_estado", estadoEmpleado);
                        cmd.Parameters.AddWithValue("@p_id_genero", idGenero);
                        cmd.Parameters.AddWithValue("@p_estado_civil", idEstadoCivil);
                        cmd.Parameters.AddWithValue("@p_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_id_cargo", idCargo);
                        cmd.Parameters.AddWithValue("@p_id_profesion", idProfesion);
                        cmd.Parameters.AddWithValue("@p_nivel_estudio", nivelEstudio);
                        cmd.Parameters.AddWithValue("@p_estrato_social", estratoSocial);
                        cmd.Parameters.AddWithValue("@p_tipo_vivienda", tipoVivienda);
                        cmd.Parameters.AddWithValue("@p_nro_personas", nroPersonas);
                        cmd.Parameters.AddWithValue("@p_actividad_extra", actividadExtra);
                        cmd.Parameters.AddWithValue("@p_consume_licor", consumeLicor);
                        cmd.Parameters.AddWithValue("@p_medio_transporte", medioTransporte);
                        cmd.Parameters.AddWithValue("@p_tipo_sangre", tipoSangre);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ConsultarEmpresasFP()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EMPRESAS_FP", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarEmpleadoNuevo(string documentoEmpleado, int tipoDocumento, string nombreEmpleado,
            string telEmpleado, string telEmpleadoCorp, string emailEmpleado, string emailEmpleadoCorp,
            string dirEmpleado, int idCiudadEmpleado, string fechaNacEmpleado, string fotoEmpleado, int idSede, int idEps,
            int idFondo, int idArl, int idCajaCompensa, int idCesantias, int idGenero, int idEstadoCivil, int idCargo, string claveUsuario, 
            int idProfesion, string nivelEstudio, int estratoSocial, string tipoVivienda, int nroPersonas,
            string actividadExtra, string consumeLicor, string medioTransporte, string tipoSangre)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_EMPLEADO_NUEVO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_documento_empleado", documentoEmpleado);
                        cmd.Parameters.AddWithValue("@p_tipo_doc_empleado", tipoDocumento);
                        cmd.Parameters.AddWithValue("@p_nombre_empleado", nombreEmpleado);
                        cmd.Parameters.AddWithValue("@p_tel_empleado", telEmpleado);
                        cmd.Parameters.AddWithValue("@p_email_empleado", emailEmpleado);
                        cmd.Parameters.AddWithValue("@p_tel_empleado_corp", telEmpleadoCorp);
                        cmd.Parameters.AddWithValue("@p_email_empleado_corp", emailEmpleadoCorp);
                        cmd.Parameters.AddWithValue("@p_dir_empleado", dirEmpleado);
                        cmd.Parameters.AddWithValue("@p_id_ciu_empleado", idCiudadEmpleado);
                        cmd.Parameters.AddWithValue("@p_fecha_nac_empleado", fechaNacEmpleado);
                        cmd.Parameters.AddWithValue("@p_foto_empleado", fotoEmpleado);
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);
                        cmd.Parameters.AddWithValue("@p_id_eps", idEps);
                        cmd.Parameters.AddWithValue("@p_id_fondo_pension", idFondo);
                        cmd.Parameters.AddWithValue("@p_id_arl", idArl);
                        cmd.Parameters.AddWithValue("@p_id_caja_comp", idCajaCompensa);
                        cmd.Parameters.AddWithValue("@p_cesantias", idCesantias);
                        cmd.Parameters.AddWithValue("@p_id_genero", idGenero);
                        cmd.Parameters.AddWithValue("@p_estado_civil", idEstadoCivil);
                        cmd.Parameters.AddWithValue("@p_id_cargo", idCargo);
                        cmd.Parameters.AddWithValue("@p_clave_usuario", claveUsuario);
                        cmd.Parameters.AddWithValue("@p_id_profesion", idProfesion);
                        cmd.Parameters.AddWithValue("@p_nivel_estudio", nivelEstudio);
                        cmd.Parameters.AddWithValue("@p_estrato_social", estratoSocial);
                        cmd.Parameters.AddWithValue("@p_tipo_vivienda", tipoVivienda);
                        cmd.Parameters.AddWithValue("@p_nro_personas", nroPersonas);
                        cmd.Parameters.AddWithValue("@p_actividad_extra", actividadExtra);
                        cmd.Parameters.AddWithValue("@p_consume_licor", consumeLicor);
                        cmd.Parameters.AddWithValue("@p_medio_transporte", medioTransporte);
                        cmd.Parameters.AddWithValue("@p_tipo_sangre", tipoSangre);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarEmpleado(string documentoEmpleado, int tipoDocumento, string nombreEmpleado,
            string telEmpleado, string telEmpleadoCorp, string emailEmpleado, string emailEmpleadoCorp,
            string dirEmpleado, int idCiudadEmpleado, string fechaNacEmpleado, string fotoEmpleado, string nroContrato,
            string tipoContrato, int idEmpresaFP, int idSede, string fechaIni, string fechaFin, int sueldo, string grupoNomina, int idEps,
            int idFondo, int idArl, int idCajaCompensa, int idCesantias, string estadoEmpleado, int idGenero, int idEstadoCivil, 
            int idCanalVenta, int idCargo, int idProfesion, string nivelEstudio, int estratoSocial, string tipoVivienda, int nroPersonas, 
            string actividadExtra, string consumeLicor, string medioTransporte, string tipoSangre)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_EMPLEADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_documento_empleado", documentoEmpleado);
                        cmd.Parameters.AddWithValue("@p_tipo_doc_empleado", tipoDocumento);
                        cmd.Parameters.AddWithValue("@p_nombre_empleado", nombreEmpleado);
                        cmd.Parameters.AddWithValue("@p_tel_empleado", telEmpleado);
                        cmd.Parameters.AddWithValue("@p_email_empleado", emailEmpleado);
                        cmd.Parameters.AddWithValue("@p_tel_empleado_corp", telEmpleadoCorp);
                        cmd.Parameters.AddWithValue("@p_email_empleado_corp", emailEmpleadoCorp);
                        cmd.Parameters.AddWithValue("@p_dir_empleado", dirEmpleado);
                        cmd.Parameters.AddWithValue("@p_id_ciu_empleado", idCiudadEmpleado);
                        cmd.Parameters.AddWithValue("@p_fecha_nac_empleado", fechaNacEmpleado);
                        cmd.Parameters.AddWithValue("@p_foto_empleado", fotoEmpleado);
                        cmd.Parameters.AddWithValue("@p_nro_contrato", nroContrato);
                        cmd.Parameters.AddWithValue("@p_tipo_contrato", tipoContrato);
                        cmd.Parameters.AddWithValue("@p_id_empresa_fp", idEmpresaFP);
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);
                        cmd.Parameters.AddWithValue("@p_fecha_inicio", fechaIni);
                        cmd.Parameters.AddWithValue("@p_fecha_fin", fechaFin);
                        cmd.Parameters.AddWithValue("@p_sueldo", sueldo);
                        cmd.Parameters.AddWithValue("@p_grupo_nomina", grupoNomina);
                        cmd.Parameters.AddWithValue("@p_id_eps", idEps);
                        cmd.Parameters.AddWithValue("@p_id_fondo_pension", idFondo);
                        cmd.Parameters.AddWithValue("@p_id_arl", idArl);
                        cmd.Parameters.AddWithValue("@p_id_caja_comp", idCajaCompensa);
                        cmd.Parameters.AddWithValue("@p_cesantias", idCesantias);
                        cmd.Parameters.AddWithValue("@p_estado", estadoEmpleado);
                        cmd.Parameters.AddWithValue("@p_id_genero", idGenero);
                        cmd.Parameters.AddWithValue("@p_estado_civil", idEstadoCivil);
                        cmd.Parameters.AddWithValue("@p_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_id_cargo", idCargo);
                        cmd.Parameters.AddWithValue("@p_id_profesion", idProfesion);
                        cmd.Parameters.AddWithValue("@p_nivel_estudio", nivelEstudio);
                        cmd.Parameters.AddWithValue("@p_estrato_social", estratoSocial);
                        cmd.Parameters.AddWithValue("@p_tipo_vivienda", tipoVivienda);
                        cmd.Parameters.AddWithValue("@p_nro_personas", nroPersonas);
                        cmd.Parameters.AddWithValue("@p_actividad_extra", actividadExtra);
                        cmd.Parameters.AddWithValue("@p_consume_licor", consumeLicor);
                        cmd.Parameters.AddWithValue("@p_medio_transporte", medioTransporte);
                        cmd.Parameters.AddWithValue("@p_tipo_sangre", tipoSangre);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarEstadoUsuario(string documentoEmpleado)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_ESTADO_USUARIO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_documento_empleado", documentoEmpleado);
                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ConsultarEmpleado(string documentoEmpleado)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EMPLEADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_doc_empleado", documentoEmpleado);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable CargarEmpleados(string docEmpleado)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CARGAR_EMPLEADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_doc_empleado", docEmpleado);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        #endregion

        #region Categorias tienda
        public DataTable ConsultarCategorias()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CATEGORIAS_TIENDA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCategoriaPorNombre(string nombreCategoria)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CATEGORIAS_TIENDA_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_categoria", nombreCategoria);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCategoriaPorId(int idCategoria)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CATEGORIA_TIENDA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_categoria", idCategoria);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarCategoria(int idCategoria, string nombreCategoria)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_CATEGORIA_TIENDA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_categoria", nombreCategoria);
                        cmd.Parameters.AddWithValue("@p_id_categoria", idCategoria);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarCategoria(int idCategoria)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_CATEGORIA_TIENDA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_categoria", idCategoria);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarCategoria(string nombreCategoria)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_CATEGORIA_TIENDA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_categoria", nombreCategoria);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ValidarCategoriaTienda(int idCategoria)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_CATEGORIA_TIENDA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_categoria", idCategoria);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        #endregion

        #region CRM

        public DataTable ConsultarContactosCRM(out decimal valorTotal)
        {
            DataTable dt = new DataTable();
            valorTotal = 0;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CONTACTOS_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Parámetro de salida
                        MySqlParameter ValorTotal = new MySqlParameter("@Total_Valor_Propuesta", MySqlDbType.Decimal);
                        ValorTotal.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(ValorTotal);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);

                            if (ValorTotal.Value != DBNull.Value)
                            {
                                valorTotal = Convert.ToDecimal(ValorTotal.Value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarContactosCRMPorId(int idContacto, out bool respuesta)
        {
            DataTable dt = new DataTable();
            respuesta = false;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CONTACTOS_CRM_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_contacto", idContacto);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
                respuesta = false;
            }

            return dt;
        }

        public DataTable ConsultarContactosCRMPorDocumento(int nroDocumento)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CONTACTOS_CRM_POR_DOCUMENTO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_documento_afiliado", nroDocumento);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarPregestionCRMPorDocumento(int nroDocumento)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PREGESTION_CRM_POR_DOCUMENTO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_documento_afiliado", nroDocumento);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarEmpresasCRM()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EMPRESAS_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEmpresaCRMPorId(int idEmpresCRM, out bool respuesta)
        {
            DataTable dt = new DataTable();
            respuesta = false;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EMPRESA_CRM_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_empresa_CRM", idEmpresCRM);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
                respuesta = false;
            }

            return dt;
        }

        public DataTable ValidarEmpresaCRMPorId(int idEmpresCRM, out bool respuesta, out string mensaje)
        {
            DataTable dt = new DataTable();
            mensaje = string.Empty;
            respuesta = false;


            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_EMPRESA_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_empresa_CRM", idEmpresCRM);

                        MySqlParameter pMensaje = new MySqlParameter("@p_mensaje", MySqlDbType.VarChar, 300);
                        pMensaje.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pMensaje);

                        cmd.ExecuteNonQuery();
                        mensaje = pMensaje.Value?.ToString();

                        if (mensaje == "OK") respuesta = true;
                        else respuesta = false;

                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
                respuesta = false;
            }

            return dt;
        }

        public DataTable ConsultarEmpresasCRMPorNombre(string nombreEmpresaCRM)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EMPRESA_CRM_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_empresa_crm", nombreEmpresaCRM);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEmpresasCRMPorUsuario(int  idUsuario)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EMPRESA_CRM_POR_USUARIO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEstadossCRM()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTADOS_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarPregestionCRM(string nombreContacto, string apellidoContacto, string documentoContacto, 
            int idTipoDocumentoContacto, string celularContacto, int idTipoGestion, int idCanalVenta, int idUsuarioAsigna)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PREGESTION_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_nombre_contacto", nombreContacto);
                        cmd.Parameters.AddWithValue("@p_apellido_contacto", apellidoContacto);
                        cmd.Parameters.AddWithValue("@p_documento_contacto", documentoContacto);
                        cmd.Parameters.AddWithValue("@p_id_tipo_documento_contacto", idTipoDocumentoContacto);
                        cmd.Parameters.AddWithValue("@p_celular_contacto", celularContacto);
                        cmd.Parameters.AddWithValue("@p_id_tipo_gestion", idTipoGestion);
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_id_usuario_asigna", idUsuarioAsigna);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarContactoCRM(string nombreContacto, string apellidoContacto, string telefonoContacto, string emailContacto, int idEmpresaCMR,
        int idEstado, string fechaPrimerCon, string fechaProxCon, int valorPropuesta, string archivoPropuesta, string observaciones,
        int idUsuario, int idObjetivo, int tipoPago, int idTipoAfiliado, int idCanalMarketing, int idPlan, int mesesPlan, int idTipoDoc,
        string DocumentoAfiliado, string tiempoAtencion, int idGenero, int edad, string fechaNac, int idEstadoVenta, int idEstrategia, int idPregestion,
        out bool respuesta, out string mensaje)
        {
            mensaje = string.Empty;
            respuesta = false;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_CONTACTO_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_contacto", nombreContacto);
                        cmd.Parameters.AddWithValue("@p_apellido_contacto", apellidoContacto);
                        cmd.Parameters.AddWithValue("@p_telefono_contacto", telefonoContacto);
                        cmd.Parameters.AddWithValue("@p_email_contacto", emailContacto);
                        cmd.Parameters.AddWithValue("@p_id_empresa", idEmpresaCMR);
                        cmd.Parameters.AddWithValue("@p_id_estado", idEstado);
                        cmd.Parameters.AddWithValue("@p_fecha_primer_con", fechaPrimerCon);
                        cmd.Parameters.AddWithValue("@p_fecha_proximo_con", fechaProxCon);
                        cmd.Parameters.AddWithValue("@p_valor_propuesta", valorPropuesta);
                        cmd.Parameters.AddWithValue("@p_archivo_propuesta", archivoPropuesta);
                        cmd.Parameters.AddWithValue("@p_observaciones", observaciones);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_id_objetivo", idObjetivo);
                        cmd.Parameters.AddWithValue("@p_id_medio_pago", tipoPago);
                        cmd.Parameters.AddWithValue("@p_id_tipo_afiliado", idTipoAfiliado);
                        cmd.Parameters.AddWithValue("@p_id_canal_marketing", idCanalMarketing);
                        cmd.Parameters.AddWithValue("@p_id_plan", idPlan);
                        cmd.Parameters.AddWithValue("@p_meses_plan", mesesPlan);
                        cmd.Parameters.AddWithValue("@p_id_tipo_doc", idTipoDoc);
                        cmd.Parameters.AddWithValue("@p_documento_afiliado", DocumentoAfiliado);
                        cmd.Parameters.AddWithValue("@p_tiempo_atencion", tiempoAtencion);
                        cmd.Parameters.AddWithValue("@p_genero", idGenero);
                        cmd.Parameters.AddWithValue("@p_edad", edad);
                        cmd.Parameters.AddWithValue("@p_fec_nac", fechaNac);
                        cmd.Parameters.AddWithValue("@p_estado_venta", idEstadoVenta);
                        cmd.Parameters.AddWithValue("@p_estrategia", idEstrategia);
                        cmd.Parameters.AddWithValue("@p_id_pregestion", idPregestion);


                        // Parámetro de salida
                        MySqlParameter pMensaje = new MySqlParameter("@p_mensaje", MySqlDbType.VarChar, 300);
                        pMensaje.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pMensaje);

                        cmd.ExecuteNonQuery();
                        mensaje = pMensaje.Value?.ToString();

                        if (mensaje == "OK") respuesta = true;
                        else respuesta = false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = "ERROR: " + ex.Message;
            }

            return mensaje;
        }

        public string ActualizarContactoCRM(int idContactoCMR, string nombreContacto, string apellidoContacto, string telefonoContacto, string emailContacto, int idEmpresaCMR,
        int idEstado, string fechaPrimerCon, string fechaProxCon, int valorPropuesta, string archivoPropuesta, string observaciones,
        int idUsuario, int idObjetivo, int tipoPago, int idTipoAfiliado, int idCanalMarketing, int idPlan, int mesesPlan, int idTipoDoc, string DocumentoAfiliado,
        int idGenero, int edad, string fechaNac, int idEstadoVenta, int idEstrategia, out bool respuesta, out string mensaje)
        {
            mensaje = string.Empty;
            respuesta = false;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_CONTACTO_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_contacto_cmr", idContactoCMR);
                        cmd.Parameters.AddWithValue("@p_nombre_contacto", nombreContacto);
                        cmd.Parameters.AddWithValue("@p_apellido_contacto", apellidoContacto);
                        cmd.Parameters.AddWithValue("@p_telefono_contacto", telefonoContacto);
                        cmd.Parameters.AddWithValue("@p_email_contacto", emailContacto);
                        cmd.Parameters.AddWithValue("@p_id_empresa", idEmpresaCMR);
                        cmd.Parameters.AddWithValue("@p_id_estado", idEstado);
                        cmd.Parameters.AddWithValue("@p_fecha_primer_con", fechaPrimerCon);
                        cmd.Parameters.AddWithValue("@p_fecha_proximo_con", fechaProxCon);
                        cmd.Parameters.AddWithValue("@p_valor_propuesta", valorPropuesta);
                        cmd.Parameters.AddWithValue("@p_archivo_propuesta", archivoPropuesta);
                        cmd.Parameters.AddWithValue("@p_observaciones", observaciones);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_id_objetivo", idObjetivo);
                        cmd.Parameters.AddWithValue("@p_id_medio_pago", tipoPago);
                        cmd.Parameters.AddWithValue("@p_id_tipo_afiliado", idTipoAfiliado);
                        cmd.Parameters.AddWithValue("@p_id_canal_marketing", idCanalMarketing);
                        cmd.Parameters.AddWithValue("@p_id_plan", idPlan);
                        cmd.Parameters.AddWithValue("@p_meses_plan", mesesPlan);
                        cmd.Parameters.AddWithValue("@p_id_tipo_doc", idTipoDoc);
                        cmd.Parameters.AddWithValue("@p_documento_afiliado", DocumentoAfiliado);
                        cmd.Parameters.AddWithValue("@p_genero", idGenero);
                        cmd.Parameters.AddWithValue("@p_edad", edad);
                        cmd.Parameters.AddWithValue("@p_fec_nac", fechaNac);
                        cmd.Parameters.AddWithValue("@p_estado_venta", idEstadoVenta);
                        cmd.Parameters.AddWithValue("@p_estrategia", idEstrategia);

                        cmd.ExecuteNonQuery();
                        respuesta = true;
                        mensaje = "Ok";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = "ERROR: " + ex.Message;
            }

            return mensaje;
        }


        public string EliminarContactoCRM(int idContactoCMR, int idUsuario, string Usuario, out bool respuesta, out string mensaje)
        {
            mensaje = string.Empty;
            respuesta = false;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_CONTACTO_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_id_contacto_cmr", idContactoCMR);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_usuario", Usuario);

                        // ParámetroS de salida
                        MySqlParameter pMensaje = new MySqlParameter("@p_mensaje", MySqlDbType.VarChar, 300);
                        pMensaje.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pMensaje);

                        MySqlParameter pRespuesta = new MySqlParameter("@p_respuesta", MySqlDbType.Bit);
                        pRespuesta.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pRespuesta);

                        cmd.ExecuteNonQuery();

                        mensaje = pMensaje.Value?.ToString();
                        respuesta = Convert.ToBoolean(pRespuesta.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = "ERROR: " + ex.Message;
            }

            return mensaje;
        }

        public DataTable ConsultarCuantosPorEstadosCRM(int id_estadoCRM)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CUANTOS_ESTADOS_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_estado_crm", id_estadoCRM);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarHistorialPorContactoCMR(int idContacto, out bool respuesta)
        {
            DataTable dt = new DataTable();
            respuesta = false;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_HISTORICO_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_contacto", idContacto);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
                respuesta = false;
            }

            return dt;
        }

        public DataTable ConsultarAgendaCRM(int idUsuario)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CONTACTOS_CRM_POR_PROX_FECHA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarTipoAfiliadCRM()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_TIPO_AFILIADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarTipoAfiliadoBasico()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_TIPO_AFILIADO_BASICO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        public DataTable ConsultarCanalesMarketingCRM()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CANALES_MARKETING_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarEmpresaCRM(string nombreEmpresaCRM, int idTipoDoc , string docEmpresa, string digitoVerificacion, string celularEmpresa,
         string correoEmpresa, int idCiudad, string observacionesEmp, int idUsuario, string nombreComercial , string nombreContacto , string nombreCargo , 
         out bool respuesta, out string mensaje)
        {
            mensaje = string.Empty;
            respuesta = false;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_EMPRESA_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_empresa_crm", nombreEmpresaCRM);
                        cmd.Parameters.AddWithValue("@p_id_tipo_doc", idTipoDoc);
                        cmd.Parameters.AddWithValue("@p_doc_empresa", docEmpresa);
                        cmd.Parameters.AddWithValue("@p_digito", digitoVerificacion);
                        cmd.Parameters.AddWithValue("@p_celular_empresa", celularEmpresa);
                        cmd.Parameters.AddWithValue("@p_correo_empresa", correoEmpresa);
                        cmd.Parameters.AddWithValue("@p_id_ciudad", idCiudad);
                        cmd.Parameters.AddWithValue("@p_observaciones_emp", observacionesEmp);          
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);          
                        cmd.Parameters.AddWithValue("@p_nombre_comercial", nombreComercial);          
                        cmd.Parameters.AddWithValue("@p_nombre_contacto", nombreContacto);          
                        cmd.Parameters.AddWithValue("@p_cargo_contacto", nombreCargo); 

                        // Parámetro de salida
                        MySqlParameter pMensaje = new MySqlParameter("@p_mensaje", MySqlDbType.VarChar, 300);
                        pMensaje.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pMensaje);

                        cmd.ExecuteNonQuery();
                        mensaje = pMensaje.Value?.ToString();

                        if (mensaje == "OK") respuesta = true;
                        else respuesta = false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = "ERROR: " + ex.Message;
            }

            return mensaje;
        }

        public string ActualizarEmpresaCRM(int idEmpresaCRM, string nombreEmpresaCRM, int idTipoDoc, string docEmpresa, string digitoVerificacion,
         string celularEmpresa, string correoEmpresa, int idCiudad, string observacionesEmp, int idUsuario, string nombreComercial, string nombreContacto, 
         string nombreCargo, out bool respuesta, out string mensaje)
        {
            mensaje = string.Empty;
            respuesta = false;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_EMPRESA_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_empresa_crm", idEmpresaCRM);
                        cmd.Parameters.AddWithValue("@p_nombre_empresa_crm", nombreEmpresaCRM);
                        cmd.Parameters.AddWithValue("@p_id_tipo_doc", idTipoDoc);
                        cmd.Parameters.AddWithValue("@p_doc_empresa", docEmpresa);
                        cmd.Parameters.AddWithValue("@p_digito", digitoVerificacion);
                        cmd.Parameters.AddWithValue("@p_celular_empresa", celularEmpresa);
                        cmd.Parameters.AddWithValue("@p_correo_empresa", correoEmpresa);
                        cmd.Parameters.AddWithValue("@p_id_ciudad", idCiudad);
                        cmd.Parameters.AddWithValue("@p_observaciones_emp", observacionesEmp);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_nombre_comercial", nombreComercial);
                        cmd.Parameters.AddWithValue("@p_nombre_contacto", nombreContacto);
                        cmd.Parameters.AddWithValue("@p_cargo_contacto", nombreCargo);

                        // Parámetro de salida
                        MySqlParameter pMensaje = new MySqlParameter("@p_mensaje", MySqlDbType.VarChar, 300);
                        pMensaje.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pMensaje);

                        cmd.ExecuteNonQuery();
                        mensaje = pMensaje.Value?.ToString();

                        if (mensaje == "OK") respuesta = true;
                        else respuesta = false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = "ERROR: " + ex.Message;
            }

            return mensaje;
        }

        public string EliminarEmpresaCRM(int idEmpresaCMR, int idUsuario, string Usuario, out bool respuesta, out string mensaje)
        {
            mensaje = string.Empty;
            respuesta = false;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_EMPRESA_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_id_empresa_crm", idEmpresaCMR);

                        // Parámetro de salida
                        MySqlParameter pMensaje = new MySqlParameter("@p_mensaje", MySqlDbType.VarChar, 300);
                        pMensaje.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pMensaje);

                        cmd.ExecuteNonQuery();
                        mensaje = pMensaje.Value?.ToString();

                        if (mensaje == "OK") respuesta = true;
                        else respuesta = false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = "ERROR: " + ex.Message;
            }

            return mensaje;
        }

        public string InsertarEstadoCRM(string nombreEstadoCRM, string colorEstadoCRM, string iconoMaxEstadoCRM, string iconoMinEstadoCRM,
        string colorHexaCRM, out bool respuesta, out string mensaje)
        {
            mensaje = string.Empty;
            respuesta = false;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_ESTADO_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_estado_crm", nombreEstadoCRM);
                        cmd.Parameters.AddWithValue("@p_color_estado_crm", colorEstadoCRM);
                        cmd.Parameters.AddWithValue("@p_icono_max_estado_crm", iconoMaxEstadoCRM);
                        cmd.Parameters.AddWithValue("@p_icono_min_estado_crm", iconoMinEstadoCRM);
                        cmd.Parameters.AddWithValue("@p_color_hexa_crm", colorHexaCRM);

                        // Parámetro de salida
                        MySqlParameter pMensaje = new MySqlParameter("@p_mensaje", MySqlDbType.VarChar, 300);
                        pMensaje.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pMensaje);

                        cmd.ExecuteNonQuery();
                        mensaje = pMensaje.Value?.ToString();

                        if (mensaje == "OK") respuesta = true;
                        else respuesta = false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = "ERROR: " + ex.Message;
            }

            return mensaje;
        }


        public string ActualizarEstadoCRM(int idEstadoCRM, string nombreEstadoCRM, string colorEstadoCRM, int iconoMaxEstadoCRM,
        int iconoMinEstadoCRM, string colorHexaCRM, out bool respuesta, out string mensaje)
        {
            mensaje = string.Empty;
            respuesta = false;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_ESTADO_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_estado_crm", idEstadoCRM);
                        cmd.Parameters.AddWithValue("@p_nombre_estado_crm", nombreEstadoCRM);
                        cmd.Parameters.AddWithValue("@p_color_estado_crm", colorEstadoCRM);
                        cmd.Parameters.AddWithValue("@p_icono_max_estado_crm", iconoMaxEstadoCRM);
                        cmd.Parameters.AddWithValue("@p_icono_min_estado_crm", iconoMinEstadoCRM);
                        cmd.Parameters.AddWithValue("@p_color_hexa_crm", colorHexaCRM);


                        // Parámetro de salida
                        MySqlParameter pMensaje = new MySqlParameter("@p_mensaje", MySqlDbType.VarChar, 300);
                        pMensaje.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pMensaje);

                        cmd.ExecuteNonQuery();
                        mensaje = pMensaje.Value?.ToString();

                        if (mensaje == "OK") respuesta = true;
                        else respuesta = false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = "ERROR: " + ex.Message;
            }

            return mensaje;
        }

        public string EliminarEstadoCRM(int idEstadoCRM, int idUsuario, string Usuario, out bool respuesta, out string mensaje)
        {
            mensaje = string.Empty;
            respuesta = false;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_ESTADO_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_id_estado_crm", idEstadoCRM);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_usuario", Usuario);

                        // ParámetroS de salida
                        MySqlParameter pMensaje = new MySqlParameter("@p_mensaje", MySqlDbType.VarChar, 300);
                        pMensaje.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pMensaje);

                        MySqlParameter pRespuesta = new MySqlParameter("@p_respuesta", MySqlDbType.Bit);
                        pRespuesta.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pRespuesta);

                        cmd.ExecuteNonQuery();

                        mensaje = pMensaje.Value?.ToString();
                        respuesta = Convert.ToBoolean(pRespuesta.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = "ERROR: " + ex.Message;
            }

            return mensaje;
        }

        public DataTable ConsultarEstadoCRMPorID(int idEstadoCRM)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTADOS_CRM_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_estado_CRM", idEstadoCRM);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        public DataTable ConsultarEstadoCRMPorNombre(string nombreEstadoCRM)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTADO_CRM_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_estado_CRM", nombreEstadoCRM);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarContactosCRMPorUsuario(int idUsuario, out decimal valorTotal)
        {
            DataTable dt = new DataTable();
            valorTotal = 0;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CONTACTOS_CRM_POR_USUARIO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

                        // Parámetro de salida
                        MySqlParameter ValorTotal = new MySqlParameter("@Total_Valor_Propuesta", MySqlDbType.Decimal);
                        ValorTotal.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(ValorTotal);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);

                            if (ValorTotal.Value != DBNull.Value)
                            {
                                valorTotal = Convert.ToDecimal(ValorTotal.Value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarEstadoCRMPagoPlan(int idcrm, string nombrePlan, int precioTotal, int idUsuario, int idEstadoCRM)
        {

            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_ESTADO_CRM_PAGO_PLAN", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_contacto", idcrm);
                        cmd.Parameters.AddWithValue("@p_nompre_plan", nombrePlan);
                        cmd.Parameters.AddWithValue("@p_precio_total", precioTotal);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_id_estado", idEstadoCRM);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ConsultarEstrategiasMarketing()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTRATEGIAS_MARKETING", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEstrategiasMarketingVigentes()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTRATEGIAS_MARKETING_VIGENTES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarTiposEstrategiasMarketing()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_TIPOS_ESTRATEGIAS_MARKETING", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarEstrategiaMarketing(string nombreEstrategia, string descripcionEstrategia, string fechaInicio, string fechaFin,
        string canalesVenta, int idTipoEstrategia, string planes, int idUsuario, decimal valorPresupuesto, out bool respuesta, out string mensaje)
        {
            mensaje = string.Empty;
            respuesta = false;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_ESTRATEGIA_MARKETING", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_estrategia", nombreEstrategia);
                        cmd.Parameters.AddWithValue("@p_descripcion_estrategia", descripcionEstrategia);
                        cmd.Parameters.AddWithValue("@p_fecha_inicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@p_fecha_fin", fechaFin);
                        cmd.Parameters.AddWithValue("@p_canales_venta", canalesVenta);
                        cmd.Parameters.AddWithValue("@p_id_tipo_estrategia", idTipoEstrategia);
                        cmd.Parameters.AddWithValue("@p_planes", planes);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_valor_presupuesto", valorPresupuesto);

                        // Parámetro de salida
                        MySqlParameter pMensaje = new MySqlParameter("@p_mensaje", MySqlDbType.VarChar, 300);
                        pMensaje.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pMensaje);

                        cmd.ExecuteNonQuery();
                        mensaje = pMensaje.Value?.ToString();

                        if (mensaje == "OK") respuesta = true;
                        else respuesta = false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = "ERROR: " + ex.Message;
            }

            return mensaje;
        }

        public string ActualizarEstrategiaMarketing(int idEstrategia, string nombreEstrategia, string descripcionEstrategia, string fechaInicio, string fechaFin,
        string canalesVenta, int idTipoEstrategia, string planes, decimal ValorPresupuesto, out bool respuesta, out string mensaje)
        {
            mensaje = string.Empty;
            respuesta = false;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_ESTRATEGIA_MARKETING", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_estrategia", idEstrategia);
                        cmd.Parameters.AddWithValue("@p_nombre_estrategia", nombreEstrategia);
                        cmd.Parameters.AddWithValue("@p_descripcion_estrategia", descripcionEstrategia);
                        cmd.Parameters.AddWithValue("@p_fecha_inicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@p_fecha_fin", fechaFin);
                        cmd.Parameters.AddWithValue("@p_canales", canalesVenta);
                        cmd.Parameters.AddWithValue("@p_id_tipo_estrategia", idTipoEstrategia);
                        cmd.Parameters.AddWithValue("@p_planes", planes);
                        cmd.Parameters.AddWithValue("@p_valor_presupuesto", ValorPresupuesto);

                        // Parámetro de salida
                        MySqlParameter pMensaje = new MySqlParameter("@p_mensaje", MySqlDbType.VarChar, 300);
                        pMensaje.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pMensaje);

                        cmd.ExecuteNonQuery();
                        mensaje = pMensaje.Value?.ToString();

                        if (mensaje == "OK") respuesta = true;
                        else respuesta = false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = "ERROR: " + ex.Message;
            }

            return mensaje;
        }

        public DataTable ConsultarEstrategiaMarketingPorId(int idEstrategia)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTRATEGIA_MARKETING_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_estrategia", idEstrategia);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ValidarEstrategiaMarketingTablas(int idEstrategia)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_ESTRATEGIA_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_estrategia", idEstrategia);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string EliminarEstrategiaMarketing(int idEstategia)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_ESTRATEGIA_MARKETING", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_estrategia", idEstategia);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ConsultarEstrategiaMarketingPorNombre(string nombreEstategia)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTRATEGIA_MARKETING_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_estrategia", nombreEstategia);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            dataAdapter.Fill(dt); // ← ¡ya no se vuelve a abrir la conexión!
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEstadosVenta()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTADOS_VENTA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarResumenEstrategiasUltimoMes()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_RESUMEN_ESTRATEGIA_ULT_MES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCuantosLeadsEstrategiaAceptados()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CUANTOS_LEADS_ESTRATEGIA_ACEPTADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEstrategiaasMarketingEncabezado()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTRATEGIAS_MARKETING_ENCABEZADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarRankingAsesoresMesPasado()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_RANKING_ASESORES_MES_PASADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarRankingCanalesVentaPorVentaPorIdCanal(int idCanalvventa)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_RANKING_ASESORES_MES_VIGENTE_CRM_POR_CANAL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalvventa);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEstrategiasMarketingValorMes()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTRATEGIAS_MARK_POR_VALOR_MES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarRankingAsesoresMesVigente()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_RANKING_ASESORES_MES_VIGENTE_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCantidadLeadsPorEstadosVenta()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTADOS_VENTA_LEADS_TODOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarRankingCanalesVentaPorVenta()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_RANKING_CANALES_VENTA_MES_VIGENTE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCuantossLeadsDiaPorAsesor(int idEstrategia)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("PA_CONSULTAR_CUANTOS_LEADS_DIA_DIA_POR_ASESOR_ESTRATEGIA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_estrategia", idEstrategia);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarRankingPlanesPorIdEstrategia(int idEstrategia)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_RANKING_PLANES_POR_ESTRATEGIA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_estrategia", idEstrategia);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarRankingCanalesVentaPorIdEstrategia(int idEstrategia)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("PA_CONSULTAR_RANKING_CANALES_VENTA_POR_ESTRATEGIA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_estrategia", idEstrategia);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEstrategiaMarketingCuantos(int idEstrategia)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTRATEGIA_MARKETING_CUANTOS_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_estrategia", idEstrategia);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCanalesVentaSedes()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CANALES_VENTA_SEDES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarDiasFaltanPlanPregestionCRM(string idCanalVenta, int dias)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_DIAS_FALTAN_PLAN_PREGESTION_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_sede", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_dias", dias);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCanalesVentaSedesPorId(int idsede)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CANALES_VENTA_SEDES_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_sede", idsede);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultaCargarAsesoresPorSede(int idSede)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("PA_CARGAR_ASESORES_POR_SEDE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultaCargarAsesoresPorCanalVenta(int idCanalVenta)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("PA_CARGAR_ASESORES_POR_CANAL_DE_VENTA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable CargarPlanesAfiliadoPregestionCRM(string idAfiliado, string Estado)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PLAN_AFILIADO_PREGESTION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_afiliado", idAfiliado);
                        //cmd.Parameters.AddWithValue("@p_estado", Estado);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultaCargarPregestionPorIdAsesor(int idUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PREGESTION_POR_ASESOR", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string InsertarPregestionAsesorCRM(string nombre, string apellido, string documento, int idTipoDocumento, string celular,
        int tipoGestion, int idCanalVenta, int idUsuarioAsigna, int asesor, string estado)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PREGESTION_ASESOR_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_contacto", nombre);
                        cmd.Parameters.AddWithValue("@p_apellido_contacto", apellido);
                        cmd.Parameters.AddWithValue("@p_documento_contacto", documento);
                        cmd.Parameters.AddWithValue("@p_tipo_doc_contacto", idTipoDocumento);
                        cmd.Parameters.AddWithValue("@p_celular_contacto", celular);
                        cmd.Parameters.AddWithValue("@p_id_tipo_gestion", tipoGestion);
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuarioAsigna);
                        cmd.Parameters.AddWithValue("@p_id_asesor", asesor);
                        cmd.Parameters.AddWithValue("@p_estado", estado);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarIdAsesorPregestion(int idPregestion, int idAsesor, out string respuesta)
        {
            respuesta = string.Empty;
            string _mensaje = string.Empty;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_ID_ASESOR_PREGESTION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_pregestion", idPregestion);
                        cmd.Parameters.AddWithValue("@p_id_asesor", idAsesor);

                        // Parámetro de salida
                        MySqlParameter mensaje = new MySqlParameter("@p_mensaje", MySqlDbType.VarChar, 300);
                        mensaje.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(mensaje);

                        cmd.ExecuteNonQuery();
                        _mensaje = mensaje.Value.ToString();

                        respuesta = _mensaje;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ConsultarVentasAsesorMesVigente(int idUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_VENTAS_ASESOR_MES_VIGENTE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarVentasAsesorAnnioVigente(int idUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("PA_CONSULTAR_VENTAS_ASESOR_ANNO_VIGENTE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarVentasAsesorDiaVigente(int idUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_VENTAS_ASESOR_DIA_VIGENTE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarUsuarioSedePerfilPorId(int idUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_USUARIO_SEDE_PERFIL_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarMetaComercialMensual(int idCanalVenta, int mes, int annio)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_META_COMERCIAL_MENSUAL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_mes", mes);
                        cmd.Parameters.AddWithValue("@p_annio", annio);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarMetaComercialMensualTodos(int mes, int annio)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_META_COMERCIAL_MENSUAL_TODOS_CANALES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_mes", mes);
                        cmd.Parameters.AddWithValue("@p_annio", annio);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEstacionalidadPorDia(int mes, int annio)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTACIONALIDAD_POR_DIA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_mes", mes);
                        cmd.Parameters.AddWithValue("@p_annio", annio);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarVentasVsMetasPorUusuarioCRM(int idCanalVenta, int mes, int annio, int idUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GRAFICA_METAS_VS_VENTAS_USU", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_mes", mes);
                        cmd.Parameters.AddWithValue("@p_annio", annio);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        public DataTable ConsultarVentasVsMetasPorCanalVenta(int idCanalVenta, int mes, int annio)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GRAFICA_METAS_VS_VENTAS_CANAL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_mes", mes);
                        cmd.Parameters.AddWithValue("@p_annio", annio);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarVentasVsMetasGlobal(int mes, int annio)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GRAFICA_METAS_VS_VENTAS_GLOBAL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_mes", mes);
                        cmd.Parameters.AddWithValue("@p_annio", annio);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPoliticaTiempoLeadCRM(string documentoAfiliado,int dias)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_POLITICA_TIEMPO_LEAD_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_documento_afiliado", documentoAfiliado);
                        cmd.Parameters.AddWithValue("@p_dias", dias);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarUsuarioGestionaCRM(int idContacto, int idUsuarioAsignado)
        {
            string respuesta = string.Empty;
            string _mensaje = string.Empty;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_USUARIO_GESTIONA_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_contacto", idContacto);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuarioAsignado);

                        // Parámetro de salida
                        MySqlParameter mensaje = new MySqlParameter("@p_mensaje", MySqlDbType.VarChar,300);
                        mensaje.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(mensaje);

                        cmd.ExecuteNonQuery();
                        _mensaje = mensaje.Value.ToString();

                        respuesta = _mensaje;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ConsultarProspectosCRM()
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PROSPECTOS_CRM", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }


        public DataTable CargarProspectosCRM()
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CARGAR_PROSPECTOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarProspectoClienteCorporativo(string doc_empresa)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PROSPECTO_POR__EMPRESA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_doc_empresa", doc_empresa);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }
        #endregion

        #region GymPass

        public DataTable ConsultarGymPass()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GYM_PASS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarGymPassPorId(int idGymPass)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GYM_PASS_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_gym_pass", idGymPass);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarGymPassPorDocumento(string nroDocumento)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GYM_PASS_POR_DOCUMENTO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_doc_gym_pass", nroDocumento);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        #endregion

        #region GymPass Agenda

        public DataTable ConsultarGymPassAgendaPorId(int idGymPass)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GYM_PASS_AGENDA_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_gym_pass", idGymPass);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarGymPassAgendaPorDocumento(string nroDocumento)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GYM_PASS_AGENDA_POR_DOCUMENTO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_doc_gym_pass", nroDocumento);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarGymPassAgendaPorSede(int idSede)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GYM_PASS_AGENDA_POR_SEDE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarGymPassAgendaPorEstado(string estado)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GYM_PASS_AGENDA_POR_ESTADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_estado", estado);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarGymPassAgenda(int? idSede, string estado)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GYM_PASS_AGENDA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede.HasValue ? idSede.Value : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@p_estado", string.IsNullOrEmpty(estado) ? (object)DBNull.Value : estado);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarEstadosGymPassAgenda()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_GYM_PASS_AGENDA_ESTADOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable EliminarGymPassAgenda(int idAgenda)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_AGENDA_GYM_PASS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_agenda_gym_pass", idAgenda);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        #endregion

        #region Afiliados

        public DataTable ConsultarAfiliados()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_AFILIADOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }
        public DataTable ConsultarAfiliadoPorId(int idAfiliado)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_AFILIADO_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_afiliado", idAfiliado);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarAfiliadoParaContrato(int idAfiliado)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_AFILIADO_PARA_CONTRATO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_afiliado", idAfiliado);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarAfiliadoPorDocumento(int nroDocumento)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_AFILIADO_POR_DOCUMENTO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_documento_afiliado", nroDocumento);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public string EliminarAfiliado(int idAfiliado, string estado)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_AFILIADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_afiliado", idAfiliado);
                        cmd.Parameters.AddWithValue("@p_estado", estado);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarAfiliado(int idAfiliado, int idTipoDocumento, string nombreAfiliado, string apellidoAfiliado,
        string celularAfiliado, string emailAfiliado, string dirAfiliado, int idCiudadAfiliado, string fechaNacAfiliado, string fotoAfiliado,
        int idGenero, int idEstadoCivilAfiliado, int idProfesion, int idEmpresaAfil, int idEps, int idSede, string responsableAfil,
        string parentesco, string contactoAfiliado)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_AFILIADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada (coinciden con el procedimiento en MySQL)
                        cmd.Parameters.AddWithValue("@p_id_afiliado", idAfiliado);
                        cmd.Parameters.AddWithValue("@p_id_tipo_documento", idTipoDocumento);
                        cmd.Parameters.AddWithValue("@p_nombre_afiliado", nombreAfiliado);
                        cmd.Parameters.AddWithValue("@p_apellido_afiliado", apellidoAfiliado);
                        cmd.Parameters.AddWithValue("@p_celular_afiliado", celularAfiliado);
                        cmd.Parameters.AddWithValue("@p_email_afiliado", emailAfiliado);
                        cmd.Parameters.AddWithValue("@p_dir_afiliado", dirAfiliado);
                        cmd.Parameters.AddWithValue("@p_id_ciudad_afiliado", idCiudadAfiliado);
                        cmd.Parameters.AddWithValue("@p_fecha_nac_afiliado", fechaNacAfiliado);
                        cmd.Parameters.AddWithValue("@p_foto_afiliado", fotoAfiliado);
                        cmd.Parameters.AddWithValue("@p_id_genero", idGenero);
                        cmd.Parameters.AddWithValue("@p_id_etado_civil_afiliado", idEstadoCivilAfiliado);
                        cmd.Parameters.AddWithValue("@p_id_profesion", idProfesion);
                        cmd.Parameters.AddWithValue("@p_id_empresa_afil", idEmpresaAfil);
                        cmd.Parameters.AddWithValue("@p_id_eps", idEps);
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);
                        cmd.Parameters.AddWithValue("@p_responsable_afil", responsableAfil);
                        cmd.Parameters.AddWithValue("@p_parentesco", parentesco);
                        cmd.Parameters.AddWithValue("@p_contacto_afiliado", contactoAfiliado);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarAfiliado(string documentoAfiliado, int idTipoDocumento, string nombreAfiliado, string apellidoAfiliado, string celularAfiliado,
        string emailAfiliado, string claveAfiliado, string dirAfiliado, int idCiudadAfiliado, string fechaNacAfiliado, string fotoAfiliado, int idGenero,
        int idEstadoCivilAfiliado, int idProfesion, int idEmpresaAfil, int idEps, int idSede, string responsableAfil, string parentesco, string contactoAfiliado,
        int idUsuario)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_AFILIADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_documento_afiliado", documentoAfiliado);
                        cmd.Parameters.AddWithValue("@p_id_tipo_documento", idTipoDocumento);
                        cmd.Parameters.AddWithValue("@p_nombre_afiliado", nombreAfiliado);
                        cmd.Parameters.AddWithValue("@p_apellido_afiliado", apellidoAfiliado);
                        cmd.Parameters.AddWithValue("@p_celular_afiliado", celularAfiliado);
                        cmd.Parameters.AddWithValue("@p_email_afiliado", emailAfiliado);
                        cmd.Parameters.AddWithValue("@p_clave_afiliado", claveAfiliado);
                        cmd.Parameters.AddWithValue("@p_dir_afiliado", dirAfiliado);
                        cmd.Parameters.AddWithValue("@p_id_ciudad_afiliado", idCiudadAfiliado);
                        cmd.Parameters.AddWithValue("@p_fecha_nac_afiliado", fechaNacAfiliado);
                        cmd.Parameters.AddWithValue("@p_foto_afiliado", fotoAfiliado);
                        cmd.Parameters.AddWithValue("@p_id_genero", idGenero);
                        cmd.Parameters.AddWithValue("@p_id_etado_civil", idEstadoCivilAfiliado);
                        cmd.Parameters.AddWithValue("@p_id_profesion", idProfesion);
                        cmd.Parameters.AddWithValue("@p_id_empresa_afil", idEmpresaAfil);
                        cmd.Parameters.AddWithValue("@p_id_eps", idEps);
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);
                        cmd.Parameters.AddWithValue("@p_responsable_afil", responsableAfil);
                        cmd.Parameters.AddWithValue("@p_parentesco", parentesco);
                        cmd.Parameters.AddWithValue("@p_contacto_afiliado", contactoAfiliado);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarPagoPlanAfiliado(int idAfiliadoPlan, int valor, int idMedioPago, string idReferencia, string banco,
        int idUsuario, string estado, string idSiigoFactura, int idCanalVenta, int idcrm)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PAGO_PLAN_AFILIADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_id_afiliado_plan", idAfiliadoPlan);
                        cmd.Parameters.AddWithValue("@p_valor", valor);
                        cmd.Parameters.AddWithValue("@p_id_medio_pago", idMedioPago);
                        cmd.Parameters.AddWithValue("@p_id_referencia", idReferencia);
                        cmd.Parameters.AddWithValue("@p_banco", banco);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_estado", estado);
                        cmd.Parameters.AddWithValue("@p_id_siigo_factura", idSiigoFactura);
                        cmd.Parameters.AddWithValue("@p_id_canal_venta", idCanalVenta);
                        cmd.Parameters.AddWithValue("@p_id_contacto", idcrm);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ConsultarAfiliadoEstadoActivo(int idAfiliado)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_AFILIADO_CON_PLAN_ACTIVO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_afiliado", idAfiliado);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarAfiliadoSede(int idAfiliado)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_AFILIADO_SEDE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_afiliado", idAfiliado);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }
        public DataTable ConsultarAfiliadoPorParametros(string parametro)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_AFILIADO_POR_PARAMETROS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_afiliado", parametro);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }
        #endregion

        #region Medios de pago
        public DataTable ConsultarMediosDePago()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_MEDIOS_DE_PAGO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        #endregion

        #region Activos Fijos

        public DataTable ConsultarActivoPorId(int idActivo)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ACTIVO_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_activo", idActivo);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public string EliminarActivo(int idActivo, string estado)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_ACTIVO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_activo", idActivo);
                        cmd.Parameters.AddWithValue("@p_estado", estado);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarActivo(int idActivo, int idSede, int idCategoria, string nombreActivo,
        string estadoActivo, string codigoInterno, string marca, string proveedor, string fechaIngreso, string imagenActivo)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_ACTIVO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada (coinciden con el procedimiento en MySQL)
                        cmd.Parameters.AddWithValue("@p_id_activo", idActivo);
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);
                        cmd.Parameters.AddWithValue("@p_id_categoria", idCategoria);
                        cmd.Parameters.AddWithValue("@p_nombre_activo", nombreActivo);
                        cmd.Parameters.AddWithValue("@p_estado_activo", estadoActivo);
                        cmd.Parameters.AddWithValue("@p_codigo_interno", codigoInterno);
                        cmd.Parameters.AddWithValue("@p_marca", marca);
                        cmd.Parameters.AddWithValue("@p_proveedor", proveedor);
                        cmd.Parameters.AddWithValue("@p_fecha_ingreso", fechaIngreso);
                        cmd.Parameters.AddWithValue("@p_imagen_activo", imagenActivo);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarActivo(int idSede, int idCategoria, string nombreActivo,
        string estadoActivo, string codigoInterno, string marca, string proveedor, string fechaIngreso, string imagenActivo)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_ACTIVO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);
                        cmd.Parameters.AddWithValue("@p_id_categoria", idCategoria);
                        cmd.Parameters.AddWithValue("@p_nombre_activo", nombreActivo);
                        cmd.Parameters.AddWithValue("@p_estado_activo", estadoActivo);
                        cmd.Parameters.AddWithValue("@p_codigo_interno", codigoInterno);
                        cmd.Parameters.AddWithValue("@p_marca", marca);
                        cmd.Parameters.AddWithValue("@p_proveedor", proveedor);
                        cmd.Parameters.AddWithValue("@p_fecha_ingreso", fechaIngreso);
                        cmd.Parameters.AddWithValue("@p_imagen_activo", imagenActivo);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        #endregion

        #region Web

        public DataTable ConsultarRegistrosConcursoGymPass()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_REGISTROS_CONCURSO_GYM_PASS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarConcursoGymPassExcel()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CONCURSO_GYM_PASS_EXCEL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        // Estudiafit
        public DataTable ConsultarEstudiafit()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTUDIAFIT", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarEstudiafitExcel()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ESTUDIAFIT_EXCEL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        // Siigo API
        public DataTable ConsultarCodigoSiigoPorDocumento(string docAfiliado)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_COD_SIIGO_POR_DOCUMENTO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_documento_afiliado", docAfiliado);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarIdSiigoFacturaDePagoPlanAfiliado(int idPago, string idSiigoFactura)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_ID_SIIGO_FACTURA_DE_PAGO_PLAN_AFILIADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_pago", idPago);
                        cmd.Parameters.AddWithValue("@p_id_siigo_factura", idSiigoFactura);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ConsultarIntegracion(int idSede)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_INTEGRACION_POR_ID_SEDE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_sede", idSede);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        // Pagos en la Web

        public DataTable ConsultarIntegracionWompi(int idIntegracion)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_INTEGRACION_WOMPI", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_integracion", idIntegracion);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarPlanPromocionPorId(int idPlan)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PROMOCION_PLAN_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_plan", idPlan);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public int ConsultarCantidadPagosPorIdAfiliadoPlan(int idAfiliadoPlan)
        {
            int cantidadPagos = 0;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CANTIDAD_PAGOS_POR_ID_AFILIADO_PLAN", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_afiliado_plan", idAfiliadoPlan);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cantidadPagos = Convert.ToInt32(reader["CantidadPagos"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en InsertarAfiliadoPlan: " + ex.Message);
                cantidadPagos = -1; // -1 indica error
            }

            return cantidadPagos;
        }

        public int ObtenerValorPlanConPromocion(int idPlan, int idAfiliadoPlan)
        {
            try
            {
                DataTable promo = ConsultarPlanPromocionPorId(idPlan);

                // Si hubo error en el procedimiento
                if (promo == null || promo.Rows.Count == 0 || promo.Columns.Contains("Error"))
                {
                    System.Diagnostics.Debug.WriteLine($"[ObtenerValorPlanConPromocion] No se encontró promoción para plan {idPlan}");
                    return 0; // valor por defecto, el que luego puedes reemplazar por PrecioTotal
                }

                DataRow row = promo.Rows[0];

                // Validar campos con TryParse para evitar excepciones
                int.TryParse(row["MesesDuracion"]?.ToString(), out int mesesPromo);
                int.TryParse(row["PrecioProm"]?.ToString(), out int precioPromo);
                int.TryParse(row["PrecioTotal"]?.ToString(), out int precioNormal);

                // Si por alguna razón viene vacío, ponemos 0 para no romper
                if (mesesPromo == 0 || (precioPromo == 0 && precioNormal == 0))
                {
                    System.Diagnostics.Debug.WriteLine($"[ObtenerValorPlanConPromocion] Datos de promoción incompletos para plan {idPlan}");
                    return 0;
                }

                int cantidadPagos = ConsultarCantidadPagosPorIdAfiliadoPlan(idAfiliadoPlan);

                if (cantidadPagos == -1)
                {
                    // Error al consultar los pagos
                    return 0;
                }

                // Regla: si aún está dentro del número de meses promocionales, cobra precio promo
                int valorFinal = (cantidadPagos < mesesPromo) ? precioPromo : precioNormal;
                return valorFinal;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ObtenerValorPlanConPromocion] Error: {ex.Message}");
                return 0;
            }
        }

        public int InsertarPagoPlanAfiliadoWebYDevolverId(int idAfiliadoPlan, int valor, int idMedioPago, string idReferencia, string banco, int idUsuario, string estado, string idSiigoFactura, string idDataToken, string idDataFuente, string idDataTransaccion, string codDatafono, string idTransaccionRRN, string numFacturaDatafono)
        {
            int idPago = 0;

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PAGO_PLAN_AFILIADO_WEB", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_afiliado_plan", idAfiliadoPlan);
                        cmd.Parameters.AddWithValue("@p_valor", valor);
                        cmd.Parameters.AddWithValue("@p_id_medio_pago", idMedioPago);
                        cmd.Parameters.AddWithValue("@p_id_referencia", idReferencia);
                        cmd.Parameters.AddWithValue("@p_banco", banco);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_estado", estado);
                        cmd.Parameters.AddWithValue("@p_id_siigo_factura", idSiigoFactura);
                        cmd.Parameters.AddWithValue("@p_id_data_token", idDataToken);
                        cmd.Parameters.AddWithValue("@p_id_data_fuente", idDataFuente);
                        cmd.Parameters.AddWithValue("@p_id_data_transaction", idDataTransaccion);
                        cmd.Parameters.AddWithValue("@p_cod_datafono", codDatafono);
                        cmd.Parameters.AddWithValue("@p_id_transaccion_RRN", idTransaccionRRN);
                        cmd.Parameters.AddWithValue("@p_num_recibo_datafono", numFacturaDatafono);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idPago = Convert.ToInt32(reader["idPago"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en InsertarAfiliadoPlan: " + ex.Message);
                idPago = -1; // -1 indica error
            }

            return idPago;
        }

        public string InsertarPagoPlanAfiliadoWeb(int idAfiliadoPlan, int valor, int idMedioPago, string idReferencia, string banco, int idUsuario, string estado, string idSiigoFactura, string idDataToken, string idDataFuente, string idDataTransaccion, string codDatafono, string idTransaccionRRN, string numFacturaDatafono)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PAGO_PLAN_AFILIADO_WEB", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_afiliado_plan", idAfiliadoPlan);
                        cmd.Parameters.AddWithValue("@p_valor", valor);
                        cmd.Parameters.AddWithValue("@p_id_medio_pago", idMedioPago);
                        cmd.Parameters.AddWithValue("@p_id_referencia", idReferencia);
                        cmd.Parameters.AddWithValue("@p_banco", banco);
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_estado", estado);
                        cmd.Parameters.AddWithValue("@p_id_siigo_factura", idSiigoFactura);
                        cmd.Parameters.AddWithValue("@p_id_data_token", idDataToken);
                        cmd.Parameters.AddWithValue("@p_id_data_fuente", idDataFuente);
                        cmd.Parameters.AddWithValue("@p_id_data_transaction", idDataTransaccion);
                        cmd.Parameters.AddWithValue("@p_cod_datafono", codDatafono);
                        cmd.Parameters.AddWithValue("@p_id_transaccion_RRN", idTransaccionRRN);
                        cmd.Parameters.AddWithValue("@p_num_recibo_datafono", numFacturaDatafono);
                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string ActualizarFechaProximoCobro(int idAfiliadoPlan)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_FECHA_PROXIMO_COBRO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_afiliado_plan", idAfiliadoPlan);
                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        // Cobros recurrentes rechazados
        public string InsertarCobroRechazado(int idAfiliadoPlan, string mensajeEstado)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_COBRO_RECHAZADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_afiliado_plan", idAfiliadoPlan);
                        cmd.Parameters.AddWithValue("@p_mensaje_estado", mensajeEstado);
                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarHistorialCobrosRechazados(int idAfiliadoPlan)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_COBROS_RECHAZADOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_afiliado_plan", idAfiliadoPlan);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        #endregion

        #region Usuarios

        public DataTable ValidarUsuario(string username, string password)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_USUARIO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_usuario", username);
                        cmd.Parameters.AddWithValue("@p_password", password);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable RevisarCodigo(int idUsuario, string codigo)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CODIGO_USUARIO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_codigo", codigo);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarFechaCodigo(int idUsuario)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_FECHA_CODIGO_USUARIO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public string ActualizarCodigoUsuario(int idUsuario, string codigo)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_CODIGO_USUARIO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@p_codigo_ingreso", codigo);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        #endregion

        #region Corporativo

        public DataTable ConsultarEmpresasYProspectosCorporativos()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EMPRESAS_Y_PROSPECTOS_CORPORATIVOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        #endregion

    }

    #region Modelos


    public class pagoswompidet
    {
        public string Id { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaFinalizacion { get; set; }
        public string Valor { get; set; }
        public string Moneda { get; set; }
        public string MetodoPago { get; set; }
        public string Estado { get; set; }
        public string Referencia { get; set; }
        public string NombreTarjeta { get; set; }
        public string UltimosDigitos { get; set; }
        public string MarcaTarjeta { get; set; }
        public string TipoTarjeta { get; set; }
        public string NombreComercio { get; set; }
        public string ContactoComercio { get; set; }
        public string TelefonoComercio { get; set; }
        public string URLRedireccion { get; set; }
        public string PaymentLinkId { get; set; }
        public string PublicKeyComercio { get; set; }
        public string EmailComercio { get; set; }
        public string Estado3DS { get; set; }
        public CustomerData customer_data { get; set; }
    }
    public class Datum
    {
        public string id { get; set; }
        public string created_at { get; set; }
        public string finalized_at { get; set; }
        public long amount_in_cents { get; set; }
        public string reference { get; set; }
        public string customer_email { get; set; }
        public string currency { get; set; }
        public string payment_method_type { get; set; }
        public string status { get; set; }
        public string status_message { get; set; }
        public CustomerData customer_data { get; set; }
        public PaymentMethod payment_method { get; set; }
    }

    public class Root
    {
        public List<Datum> data { get; set; }
    }

    public class CustomerData
    {
        public string device_id { get; set; }
        public string full_name { get; set; }
        public string phone_number { get; set; }
    }

    public class PaymentMethod
    {
        public string type { get; set; }
        public Extra extra { get; set; }
        public int installments { get; set; }
    }

    public class Extra
    {
        public string bin { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public string exp_year { get; set; }
        public string card_type { get; set; }
        public string exp_month { get; set; }
        public string last_four { get; set; }
        public string card_holder { get; set; }
        public bool is_three_ds { get; set; }
        public ThreeDsAuth three_ds_auth { get; set; }
        public string external_identifier { get; set; }
        public string processor_response_code { get; set; }
    }

    public class ThreeDsAuth
    {
        public ThreeDsAuthStep three_ds_auth { get; set; }
    }

    public class ThreeDsAuthStep
    {
        public string current_step { get; set; }
        public string current_step_status { get; set; }
    }


    #endregion
}