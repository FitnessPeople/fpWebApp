using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresconcursogympass : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarPermisos("Páginas");
            if (ViewState["SinPermiso"].ToString() == "0")
            {
                CantidadTotalPersonasRegistradas();
                CantidadEmbajadorCodigosMasRegistrados();
                CantidadSedesMasPersonasRegistradas();
                FechaMasPersonasRegistradas();
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

        private void CantidadTotalPersonasRegistradas()
        {
            string strQuery = @"SELECT COUNT(*) AS 'CantidadTotal' 
                                FROM ConcursoGymPass;";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
            ltCantidadTotalPersonasRegistradas.Text = dt.Rows[0]["CantidadTotal"].ToString();
            dt.Dispose();
        }

        private void CantidadEmbajadorCodigosMasRegistrados()
        {
            string strQuery = @"SELECT e.NombreEmb AS 'NombreEmbajador', COUNT(*) AS Cantidad 
                                FROM Embajadores e 
                                INNER JOIN ConcursoGymPass cgp 
                                ON e.CodigoEmb = cgp.CodigoEmb 
                                GROUP BY e.NombreEmb 
                                ORDER BY Cantidad DESC 
                                LIMIT 1;";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
            ltNombreEmbajador.Text = dt.Rows[0]["NombreEmbajador"].ToString();
            ltCantidadTotalEmbajador.Text = dt.Rows[0]["Cantidad"].ToString();
            dt.Dispose();
        }

        private void CantidadSedesMasPersonasRegistradas()
        {
            string strQuery = @"SELECT sede AS 'Sede', COUNT(*) AS Cantidad 
                                FROM ConcursoGymPass 
                                GROUP BY sede 
                                ORDER BY Cantidad DESC 
                                LIMIT 1;";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
            ltNombreSede.Text = dt.Rows[0]["Sede"].ToString();
            ltCantidadTotalSede.Text = dt.Rows[0]["Cantidad"].ToString();
            dt.Dispose();
        }

        private void FechaMasPersonasRegistradas()
        {
            string strQuery = @"SELECT fechaRegistro AS 'FechaRegistro', COUNT(*) AS Cantidad 
                                FROM ConcursoGymPass 
                                GROUP BY fechaRegistro 
                                ORDER BY Cantidad DESC 
                                LIMIT 1;";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
            DateTime fecha = Convert.ToDateTime(dt.Rows[0]["FechaRegistro"].ToString());
            ltFechaMasRegistros.Text = fecha.ToString("dd/MM/yyyy");
            ltCantidadTotalFecha.Text = dt.Rows[0]["Cantidad"].ToString();
            dt.Dispose();
        }   
    }
}