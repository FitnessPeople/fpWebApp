using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class cambiaestadoembajador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strQuery = "SELECT EstadoEmb FROM Embajadores WHERE idEmbajador = " + Request.QueryString["id"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (dt.Rows[0]["EstadoEmb"].ToString() == "Activo")
                    {
                        strQuery = "UPDATE Embajadores SET " +
                            "EstadoEmb = 'Inactivo' " +
                            "WHERE idEmbajador = " + Request.QueryString["id"].ToString();
                        string mensaje = cg.TraerDatosStr(strQuery);
                    }
                    if (dt.Rows[0]["EstadoEmb"].ToString() == "Inactivo")
                    {
                        strQuery = "UPDATE Embajadores SET " +
                            "EstadoEmb = 'Activo' " +
                            "WHERE idEmbajador = " + Request.QueryString["id"].ToString();
                        string mensaje = cg.TraerDatosStr(strQuery);
                    }
                }
                catch (SqlException ex)
                {
                    string mensaje = ex.Message;
                }
            }

            dt.Dispose();

            Response.Redirect("embajadores");
        }
    }
}