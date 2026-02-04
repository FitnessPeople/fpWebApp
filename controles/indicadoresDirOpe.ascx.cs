using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresDirOpe : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarIndicadoresOperativos();
            }
        }

        protected void CargarIndicadoresOperativos()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarIndicadoresInicioDirOperativo();

            if (dt == null || dt.Rows.Count == 0)
            {
                LimpiarIndicadores();
                return;
            }

            DataRow row = dt.Rows[0];

            lblIngresos.Text =
                GetDecimal(row, "IngresosReales").ToString("C0");

            lblMetaMensual.Text =
                GetDecimal(row, "MetaMensual").ToString("C0");

            lblCumplimiento.Text =
                GetPorcentaje(row, "CumplimientoIngresos");

            lblRetencion.Text =
                GetPorcentaje(row, "TasaRetencion");

            lblChurn.Text =
                GetPorcentaje(row, "Churn");

            lblOcupacion.Text =
                GetPorcentaje(row, "OcupacionPromedio");

            lblTicketPromedio.Text =
                GetDecimal(row, "TicketPromedio").ToString("C0");

            lblCumplimientoSede.Text =
                GetPorcentaje(row, "CumplimientoSede");
        }

        private void LimpiarIndicadores()
        {
            lblIngresos.Text = "$0";
            lblMetaMensual.Text = "$0";
            lblCumplimiento.Text = "0";
            lblRetencion.Text = "0";
            lblChurn.Text = "0";
            lblOcupacion.Text = "0";
            lblTicketPromedio.Text = "$0";
            lblCumplimientoSede.Text = "0";
        }

        private decimal GetDecimal(DataRow row, string column)
        {
            if (row[column] == DBNull.Value)
                return 0;

            string valor = row[column].ToString()
                .Replace("%", "")
                .Replace("$", "")
                .Replace(",", "")
                .Trim();

            if (string.IsNullOrEmpty(valor))
                return 0;

            decimal resultado;
            return decimal.TryParse(valor, out resultado) ? resultado : 0;
        }


        private string GetString(DataRow row, string column)
        {
            if (row[column] == DBNull.Value)
                return "0";

            string valor = row[column].ToString().Trim();
            return string.IsNullOrEmpty(valor) ? "0" : valor;
        }


        private string GetPorcentaje(DataRow row, string column)
        {
            if (row[column] == DBNull.Value)
                return "0";

            string valor = row[column].ToString().Replace("%", "").Trim();
            return string.IsNullOrEmpty(valor) ? "0" : valor;
        }


    }
}