using System;
using System.Data;

namespace fpWebApp.controles
{
    public partial class indicadoresreportespagos : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TotalVentas();
            //TotalVentasWeb();
            //TotalVentasCounter();
            //TotalVentasPlanEasy();
        }

        private void TotalVentas()
        {
            // OJO pasar a Procedimiento Almacenado
            string strQuery = @"SELECT SUM(pa.Valor) AS totalventas 
                FROM pagosplanafiliado pa
                WHERE DATE(pa.FechaHoraPago) 
                    BETWEEN '" + Session["fechaIni"].ToString() + @"' 
                    AND '" + Session["fechaFin"].ToString() + @"' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos1.Text = String.Format("{0:C0}", dt.Rows[0]["totalventas"].ToString());

            dt.Dispose();
        }
    }
}