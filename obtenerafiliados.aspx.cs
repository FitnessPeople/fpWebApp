using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;  

namespace fpWebApp
{
    public partial class obtenerafiliados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strQuery = "SELECT * FROM Afiliados " +
                "WHERE NombreAfiliado LIKE '%" + Request.QueryString["search"] + "%' " +
                "OR ApellidoAfiliado LIKE '%" + Request.QueryString["search"] + "%' " +
                "OR DocumentoAfiliado LIKE '%" + Request.QueryString["search"] + "%' " +
                "OR EmailAfiliado LIKE '%" + Request.QueryString["search"] + "%' " +
                "OR CelularAfiliado LIKE '%" + Request.QueryString["search"] + "%' " +
                "LIMIT 20";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            var lista = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new
                {
                    id = row["DocumentoAfiliado"],
                    nombre = row["NombreAfiliado"],
                    apellido = row["ApellidoAfiliado"],
                    celular = row["CelularAfiliado"],
                    correo = row["EmailAfiliado"],
                    direccion = row["DireccionAfiliado"]
                });
            }

            string json = JsonConvert.SerializeObject(lista);

            Response.Clear();
            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();
        }
    }
}
