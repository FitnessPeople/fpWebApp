using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresAdmSede : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarIndicadoresAdminSede();
        }

        protected void CargarIndicadoresAdminSede()
        {
            clasesglobales cg = new clasesglobales();
       
            
            DataTable dt = cg.ConsultarIndicadoresInicioAdminSede(Convert.ToInt32(Session["idcanalVenta"]));

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                lblVentasMes.Text = Convert.ToDecimal(row["VentasMes"])
                                        .ToString("N0");

                lblMetaMes.Text = Convert.ToDecimal(row["MetaMes"])
                                        .ToString("N0");

                lblCumplimientoMes.Text = Convert.ToDecimal(row["CumplimientoMes"])
                                        .ToString("N2");

                lblTicketPromedio.Text = Convert.ToDecimal(row["VentaEsperadaHoy"])
                                        .ToString("N0");

            }

        }
    }
}