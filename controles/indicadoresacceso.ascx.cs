using System;
using System.Data;

namespace fpWebApp.controles
{
    public partial class indicadoresacceso : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CuantosAccesosHoy();
            CuantosAccesosSemana();
            CuantosAccesosMes();
            CuantosAfiliadosActivosEnSede();
        }

        private void CuantosAccesosHoy()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM AccesoAfiliado WHERE DATE(FechaHoraIngreso) = DATE(NOW())";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos1.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosAccesosSemana()
        {
            string strHoy = DateTime.Now.ToString("yyyy-MM-dd");
            string strQuery = @"SELECT COUNT(*) AS cuantos 
                FROM AccesoAfiliado 
                WHERE FechaHoraIngreso BETWEEN (FechaHoraIngreso - INTERVAL (DAYOFWEEK(FechaHoraIngreso) - 2) DAY) AND FechaHoraIngreso";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos2.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosAccesosMes()
        {
            string strHoy = DateTime.Now.ToString("yyyy-MM-dd");
            string strQuery = @"SELECT COUNT(*) AS cuantos 
                FROM AccesoAfiliado 
                WHERE FechaHoraIngreso BETWEEN DATE_ADD(FechaHoraIngreso, INTERVAL -DAYOFMONTH(FechaHoraIngreso) + 1 DAY) AND FechaHoraIngreso";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos3.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosAfiliadosActivosEnSede()
        {
            //string strHoy = DateTime.Now.ToString("yyyy-MM-dd");
            string strQuery = @"SELECT COUNT(*) AS cuantos 
                FROM Afiliados WHERE idSede = " + Session["idSede"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos4.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }
    }
}