using System;
using System.Data;

namespace fpWebApp
{
    public partial class obtenerciudades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strQuery = "SELECT * " +
                "FROM Ciudades " +
                "WHERE CodigoPais = 'Co' " +
                "AND (NombreEstado LIKE '%" + Request.QueryString["search"].ToString() + "%' " +
                "OR NombreCiudad LIKE '%" + Request.QueryString["search"].ToString() + "%') ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
            string strJson = "[\r\n";
            int intCuantasFilas = dt.Rows.Count;

            if (intCuantasFilas > 0)
            {
                for (int i = 0; i < intCuantasFilas; i++)
                {
                    strJson += "{\r\n";

                    strJson += "\"id\":\"" + dt.Rows[i]["idCiudad"] + "\",\r\n";
                    strJson += "\"ciudad\":\"" + dt.Rows[i]["NombreCiudad"] + "\",\r\n";
                    strJson += "\"estado\":\"" + dt.Rows[i]["NombreEstado"] + "\",\r\n";
                    strJson += "\"pais\":\"" + dt.Rows[i]["nombrePais"] + "\"\r\n";

                    strJson += "},\r\n";
                }
            }
            strJson = strJson.Remove(strJson.Length - 3);
            strJson += "]\r\n";
            Response.Write(strJson);
            dt.Dispose();
        }
    }
}