using System;
using System.Data;

namespace fpWebApp
{
    public partial class imprimirusuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strQuery = "SELECT *, " +
                "IF(NombreEmpleado is null,'-Sin asociar-',NombreEmpleado) AS Empleado " +
                "FROM Usuarios u " +
                "LEFT JOIN Empleados e ON u.idEmpleado = e.DocumentoEmpleado " +
                "INNER JOIN Perfiles pf ON u.idPerfil = pf.idPerfil";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpUsuarios.DataSource = dt;
            rpUsuarios.DataBind();

            dt.Dispose();
        }
    }
}