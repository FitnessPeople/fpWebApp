using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresusucmr : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarPermisos("Especialistas");
            if (ViewState["SinPermiso"].ToString() == "0")
            {
                CuantosEspecialistasActivos();
                CuantosEspecialistasInactivos();
                CuantasEspecialidades();
                CuantasCitasHoy();
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

        private void CuantosEspecialistasActivos()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM contactoscrm WHERE idEstadoCRM = 1 ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos1.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosEspecialistasInactivos()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM contactoscrm WHERE idEstadoCRM = 2 ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos2.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantasEspecialidades()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM contactoscrm WHERE idEstadoCRM = 3 ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos3.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantasCitasHoy()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM contactoscrm WHERE idEstadoCRM = 4 ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            //ltCuantos4.Text = dt.Rows[0]["cuantos"].ToString();
            ltCuantos4.Text = "15";

            dt.Dispose();
        }
    }
}