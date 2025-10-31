using System;
using System.Data;

namespace fpWebApp.controles
{
    public partial class indicadores02 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CuantosAfiliadosActivos();
            CuantosAfiliadosInactivos();
            CuantasSedes();
            CuantosNuevosUltimoMes();
        }

        private void CuantosAfiliadosActivos()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Afiliados WHERE EstadoAfiliado = 'Activo' ";

            if (Session["idSede"].ToString() != "11")
                strQuery += "AND idSede = " + Session["idSede"].ToString();

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos1.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosAfiliadosInactivos()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Afiliados WHERE EstadoAfiliado = 'Inactivo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos2.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantasSedes()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Sedes WHERE idSede <> 11";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos3.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosNuevosUltimoMes()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Afiliados WHERE EstadoAfiliado = 'Activo' AND MONTH(FechaAfiliacion) = " + DateTime.Now.Month.ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos4.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }
    }
}