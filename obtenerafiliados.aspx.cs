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
            string parametro = Request.QueryString["search"].ToString();
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarAfiliadoPorParametros(parametro);

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
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }

        }
    }
}
