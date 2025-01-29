using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class obtenerafiliados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strQuery = "SELECT * " +
                "FROM Afiliados " +
                "WHERE NombreAfiliado LIKE '%" + Request.QueryString["search"].ToString() + "%' " +
                "OR ApellidoAfiliado LIKE '%" + Request.QueryString["search"].ToString() + "%' " +
                "OR DocumentoAfiliado like '%" + Request.QueryString["search"].ToString() + "%' " +
                "OR EmailAfiliado like '%" + Request.QueryString["search"].ToString() + "%' " +
                "OR CelularAfiliado like '%" + Request.QueryString["search"].ToString() + "%' " +
                "LIMIT 20";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
            string strJson = "[\r\n";
            int intCuantasFilas = dt.Rows.Count;

            if (intCuantasFilas > 0)
            {
                for (int i = 0; i < intCuantasFilas; i++)
                {
                    strJson += "{\r\n";

                    strJson += "\"id\":\"" + dt.Rows[i]["DocumentoAfiliado"] + "\",\r\n";
                    strJson += "\"nombre\":\"" + dt.Rows[i]["NombreAfiliado"] + "\",\r\n";
                    strJson += "\"apellido\":\"" + dt.Rows[i]["ApellidoAfiliado"] + "\",\r\n";
                    strJson += "\"celular\":\"" + dt.Rows[i]["CelularAfiliado"] + "\",\r\n";
                    strJson += "\"correo\":\"" + dt.Rows[i]["EmailAfiliado"] + "\",\r\n";
                    strJson += "\"direccion\":\"" + dt.Rows[i]["DireccionAfiliado"] + "\"\r\n";

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