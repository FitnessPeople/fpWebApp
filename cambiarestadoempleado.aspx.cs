using System;
using System.Data;
using System.Data.SqlClient;

namespace fpWebApp
{
    public partial class cambiarestadoempleado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strQuery = "SELECT Estado FROM Empleados WHERE DocumentoEmpleado = " + Request.QueryString["id"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (dt.Rows[0]["Estado"].ToString() == "Activo")
                    {
                        strQuery = "UPDATE Empleados SET " +
                            "Estado = 'En pausa' " +
                            "WHERE DocumentoEmpleado = " + Request.QueryString["id"].ToString();
                        string mensaje = cg.TraerDatosStr(strQuery);
                    }
                    if (dt.Rows[0]["Estado"].ToString() == "En pausa")
                    {
                        strQuery = "UPDATE Empleados SET " +
                            "Estado = 'Inactivo' " +
                            "WHERE DocumentoEmpleado = " + Request.QueryString["id"].ToString();
                        string mensaje = cg.TraerDatosStr(strQuery);
                    }
                    if (dt.Rows[0]["Estado"].ToString() == "Inactivo")
                    {
                        strQuery = "UPDATE Empleados SET " +
                            "Estado = 'Activo' " +
                            "WHERE DocumentoEmpleado = " + Request.QueryString["id"].ToString();
                        string mensaje = cg.TraerDatosStr(strQuery);
                    }
                }
                catch (SqlException ex)
                {
                    string mensaje = ex.Message;
                }
            }

            dt.Dispose();

            Response.Redirect("empleados");
        }
    }
}