using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadores04 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarPermisos("Páginas");
            if (ViewState["SinPermiso"].ToString() == "0")
            {
                CuantosUsuariosActivos();
                CuantosusuariosInactivos();
                CuantosPerfiles();
                CuantasPaginas();
            }
        }

        private void ValidarPermisos(string strPagina)
        {
            ViewState["SinPermiso"] = "1";
            string strQuery = "SELECT SinPermiso, Consulta, Exportar, CrearModificar, Borrar " +
                "FROM permisos_perfiles pp, paginas p, usuarios u " +
                "WHERE pp.idPagina = p.idPagina " +
                "AND p.Pagina = '" + strPagina + "' " +
                "AND pp.idPerfil = " + Session["idPerfil"].ToString() + " " +
                "AND u.idPerfil = pp.idPerfil " +
                "AND u.idUsuario = " + Session["idusuario"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                ViewState["SinPermiso"] = dt.Rows[0]["SinPermiso"].ToString();
                ViewState["Consulta"] = dt.Rows[0]["Consulta"].ToString();
                ViewState["Exportar"] = dt.Rows[0]["Exportar"].ToString();
                ViewState["CrearModificar"] = dt.Rows[0]["CrearModificar"].ToString();
                ViewState["Borrar"] = dt.Rows[0]["Borrar"].ToString();
            }

            dt.Dispose();
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