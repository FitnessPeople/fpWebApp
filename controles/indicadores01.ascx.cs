using System;
using System.Data;

namespace fpWebApp.controles
{
    public partial class indicadores01 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CuantosEmpleadosActivos();
            CuantosEmpleadosFijos();
            CuantosEmpleadosOPS();
            CuantosEmpleadosAprendiz();
        }

        private void CuantosEmpleadosActivos()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Empleados WHERE Estado = 'Activo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos1.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosEmpleadosFijos()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Empleados WHERE TipoContrato = 'Fijo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos2.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosEmpleadosOPS()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Empleados WHERE TipoContrato = 'OPS'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos3.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosEmpleadosAprendiz()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Empleados WHERE TipoContrato = 'Aprendiz'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos4.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }
    }
}