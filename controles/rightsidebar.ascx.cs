using System;
using System.Data;

namespace fpWebApp.controles
{
    public partial class rightsidebar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Cargamos proyectos/tareas pendientes por usuario
            string strQuery = "SELECT * FROM tareas WHERE idUsuario = " + Session["idUsuario"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpTareas.DataSource = dt;
            rpTareas.DataBind();

            dt.Dispose();
        }
    }
}