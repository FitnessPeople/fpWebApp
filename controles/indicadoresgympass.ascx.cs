using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
	public partial class indicadoresgympass : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            ValidarPermisos("Páginas");
            if (ViewState["SinPermiso"].ToString() == "0")
            {
                CantidadGymPassAgendados();
                CantidadGymPassQueAsistieron();
                CantidadGymPassQueNoAsistieron();
                CantidadGymPassCancelados();
            }
        }

        private void ValidarPermisos(string strPagina)
        {
            ViewState["SinPermiso"] = "1";
            ViewState["Consulta"] = "0";
            ViewState["Exportar"] = "0";
            ViewState["CrearModificar"] = "0";
            ViewState["Borrar"] = "0";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ValidarPermisos(strPagina, Session["idPerfil"].ToString(), Session["idusuario"].ToString());

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

        private void CantidadGymPassAgendados()
        {
            string strQuery = "SELECT COUNT(*) AS Cantidad FROM GymPassAgenda WHERE Estado = 'Agendado'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCantidadAgendados.Text = dt.Rows[0]["Cantidad"].ToString();

            dt.Dispose();
        }

        private void CantidadGymPassQueAsistieron()
        {
            string strQuery = "SELECT COUNT(*) AS Cantidad FROM GymPassAgenda WHERE Estado = 'Asistió'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCantidadAsistieron.Text = dt.Rows[0]["Cantidad"].ToString();

            dt.Dispose();
        }

        private void CantidadGymPassQueNoAsistieron()
        {
            string strQuery = "SELECT COUNT(*) AS Cantidad FROM GymPassAgenda WHERE Estado = 'No Asistió'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCantidadNoAsistieron.Text = dt.Rows[0]["Cantidad"].ToString();

            dt.Dispose();
        }

        private void CantidadGymPassCancelados()
        {
            string strQuery = "SELECT COUNT(*) AS Cantidad FROM GymPassAgenda WHERE Estado = 'Cancelado'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCantidadCancelados.Text = dt.Rows[0]["Cantidad"].ToString();

            dt.Dispose();
        }
    }
}