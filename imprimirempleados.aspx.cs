using System;
using System.Data;

namespace fpWebApp
{
    public partial class imprimirempleados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strQuery = "SELECT *, IF(Estado='Activo','primary','danger') AS label, " +
                "DATEDIFF(CURDATE(), FechaInicio) diastrabajados, " +
                "DATEDIFF(FechaFinal, CURDATE()) diasporterminar " +
                "FROM Empleados";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpEmpleados.DataSource = dt;
            rpEmpleados.DataBind();

            dt.Dispose();
        }
    }
}