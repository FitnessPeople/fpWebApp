using System;
using System.Data;

namespace fpWebApp.controles
{
    public partial class indicadores04 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CuantosUsuariosActivos();
            CuantosusuariosInactivos();
            CuantosPerfiles();
            CuantasPaginas();
        }

        private void CuantosUsuariosActivos()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Usuarios WHERE EstadoUsuario = 'Activo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos1.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosusuariosInactivos()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Usuarios WHERE EstadoUsuario = 'Inactivo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos2.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosPerfiles()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Perfiles";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos3.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantasPaginas()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Paginas";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos4.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }
    }
}