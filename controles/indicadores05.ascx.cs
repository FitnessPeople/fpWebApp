using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadores05 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CuantasTablas();
            CuantosProcedimientosAlmacenados();
        }

        private void CuantasTablas()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM information_schema.TABLES WHERE TABLE_SCHEMA = 'fitnesspeople';";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos1.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosProcedimientosAlmacenados()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM information_schema.routines WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'fitnesspeople'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos2.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosPerfiles()
        {
            ltCuantos3.Text = "";
        }

        private void CuantasPaginas()
        {
            ltCuantos4.Text = "";
        }
    }
}