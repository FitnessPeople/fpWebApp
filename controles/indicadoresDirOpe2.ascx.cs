using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresDirOpe2 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int mesActual = DateTime.Now.Month;
                int anioActual = DateTime.Now.Year;

                string nombreMes = new DateTime(anioActual, mesActual, 1)
                .ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));

                lblTituloMesAc.Text = "KPIs de " +
                                       char.ToUpper(nombreMes[0]) +
                                       nombreMes.Substring(1) +
                                       " " + anioActual;

                CargarIndicadoresOperativos(mesActual, anioActual);
            }
        }

        private void CargarIndicadoresOperativos(int mes, int annio)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarKpisDirOperacionMes(mes, annio);

            if (dt == null || dt.Rows.Count == 0)
                return;

            var lista = dt.AsEnumerable()
                .Select(r => new
                {
                    Area = ObtenerTextoSeguro(r, "Area"),
                    Kpi = ObtenerTextoSeguro(r, "KPI"),
                    ValorNumerico = ObtenerDecimalSeguro(r[2]),
                    ValorFormateado = FormatearValor(r[2]),
                    IconoTendencia = ObtenerIcono(r[2]),
                    ColorClase = ObtenerColor(r[2]),
                    TextoTendencia = ObtenerTextoTendencia(r[2])
                })
                .ToList();

            rptKpis.DataSource = lista;
            rptKpis.DataBind();
        }

        #region Métodos Auxiliares

        private string ObtenerTextoSeguro(DataRow row, string columna)
        {
            if (!row.Table.Columns.Contains(columna) || row[columna] == DBNull.Value)
                return string.Empty;

            return row[columna].ToString();
        }

        private decimal ObtenerDecimalSeguro(object valor)
        {
            decimal numero;
            if (valor == null || valor == DBNull.Value)
                return 0;

            if (!decimal.TryParse(valor.ToString(), out numero))
                return 0;

            return numero;
        }

        private string FormatearValor(object valor)
        {
            decimal numero = ObtenerDecimalSeguro(valor);

            // Si es porcentaje
            if (numero <= 100)
                return numero.ToString("N0") + "%";

            // Si es número grande
            return numero.ToString("N0");
        }

        private string ObtenerIcono(object valor)
        {
            decimal numero = ObtenerDecimalSeguro(valor);

            if (numero >= 80)
                return "fa fa-level-up";

            if (numero >= 50)
                return "fa fa-bolt";

            return "fa fa-level-down";
        }

        private string ObtenerColor(object valor)
        {
            decimal numero = ObtenerDecimalSeguro(valor);

            if (numero >= 80)
                return "text-success";

            if (numero >= 50)
                return "text-warning";

            return "text-danger";
        }

        private string ObtenerTextoTendencia(object valor)
        {
            decimal numero = ObtenerDecimalSeguro(valor);

            if (numero >= 80)
                return "Buen desempeño";

            if (numero >= 50)
                return "Desempeño medio";

            return "Bajo desempeño";
        }

        #endregion


    }
}