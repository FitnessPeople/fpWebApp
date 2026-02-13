using System;
using System.Data;

namespace fpWebApp.controles
{
    public partial class indicadoresmedico : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CuantosEspecialistasActivos();
            CuantosEspecialistasInactivos();
            CuantasEspecialidades();
            CuantasCitasHoy();
        }

        private void CuantosEspecialistasActivos()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Empleados WHERE idCargo in (18, 29, 30)";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos1.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosEspecialistasInactivos()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Empleados WHERE idCargo in (18, 29, 30)";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos2.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantasEspecialidades()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Profesiones WHERE area = 'Salud'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos3.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantasCitasHoy()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Empleados WHERE TipoContrato = 'Aprendiz'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            //ltCuantos4.Text = dt.Rows[0]["cuantos"].ToString();
            ltCuantos4.Text = "15";

            dt.Dispose();
        }
    }
}