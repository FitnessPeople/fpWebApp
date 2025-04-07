using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace fpWebApp
{
    public partial class imprimirafiliados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strFechaHoy = DateTime.Now.ToString("MM") + "/01/" + DateTime.Now.Year.ToString();
                string strUltimoDiaMes = DateTime.Now.ToString("MM") + "/" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString() + "/" + DateTime.Now.Year.ToString();
                //txbInicio.Text = "01/01/2025";
                txbInicio.Text = strFechaHoy;
                //txbFinal.Text = "01/31/2025";
                txbFinal.Text = strUltimoDiaMes;
                CargarSedes();
            }
        }

        private void CargarSedes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSedes("Gimnasio");

            ddlSedes.DataSource = dt;
            ddlSedes.DataBind();

            dt.Dispose();
        }

        protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarAfiliados();
        }

        private void ListarAfiliados()
        {
            string strQueryAdd = "";
            if (txbInicio.Text.ToString() != "" && txbFinal.Text.ToString() != "")
            {
                strQueryAdd = "AND FechaAfiliacion BETWEEN '" + txbInicio.Text.Substring(6, 4) + "-" + txbInicio.Text.Substring(0, 2) + "-" + txbInicio.Text.Substring(3, 2) + "' " +
                    "AND '" + txbFinal.Text.Substring(6, 4) + "-" + txbFinal.Text.Substring(0, 2) + "-" + txbFinal.Text.Substring(3, 2) + "' ";
            }
            string strQuery = "SELECT *, TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) AS edad, " +
                    "IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 14,'baby',IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 60,'person','person-walking-with-cane')) age " +
                    "FROM Afiliados " +
                    "WHERE idSede = " + ddlSedes.SelectedItem.Value.ToString() + " " + strQueryAdd;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpAfiliados.DataSource = dt;
            rpAfiliados.DataBind();

            dt.Dispose();
        }

        protected void txbInicio_TextChanged(object sender, EventArgs e)
        {
            ListarAfiliados();
        }

        protected void txbFinal_TextChanged(object sender, EventArgs e)
        {
            ListarAfiliados();
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string strQueryAdd = "";
                if (txbInicio.Text.ToString() != "" && txbFinal.Text.ToString() != "")
                {
                    strQueryAdd = "AND FechaAfiliacion BETWEEN '" + txbInicio.Text.Substring(6, 4) + "-" + txbInicio.Text.Substring(0, 2) + "-" + txbInicio.Text.Substring(3, 2) + "' " +
                        "AND '" + txbFinal.Text.Substring(6, 4) + "-" + txbFinal.Text.Substring(0, 2) + "-" + txbFinal.Text.Substring(3, 2) + "' ";
                }
                string strQuery = "SELECT *, TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) AS edad " +
                    "FROM Afiliados " +
                    "WHERE idSede = " + ddlSedes.SelectedItem.Value.ToString() + " " + strQueryAdd;
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);
                string nombreArchivo = $"Afiliados_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("Afiliados");

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