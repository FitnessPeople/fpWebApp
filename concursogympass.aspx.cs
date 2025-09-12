using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
	public partial class concursogympass : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Estudiafit");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        //No tiene acceso a esta página
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    else
                    {
                        //Si tiene acceso a esta página
                        divBotonesLista.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            CargarGraficas();
                        }
                    }

                    listaInscritos();
                }
                else
                {
                    Response.Redirect("logout.aspx");
                }
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

        private void listaInscritos()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarRegistrosConcursoGymPass();
            rpConcursoGymPass.DataSource = dt;
            rpConcursoGymPass.DataBind();
            dt.Dispose();
        }

        private void CargarGraficas()
        {
            string query = @"SELECT e.NombreEmb AS NombreEmbajador, COUNT(*) AS Cantidad 
                             FROM Embajadores e 
                             INNER JOIN concursogympass cgp 
                               ON e.CodigoEmb = cgp.CodigoEmb 
                             GROUP BY e.NombreEmb 
                             ORDER BY Cantidad DESC;";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(query);

            // columnas: array de arrays -> [["Emb1", 10], ["Emb2", 5], ...]
            var columnas = new List<List<object>>();
            // colores: objeto name->color -> { "Emb1":"#ff0000", "Emb2":"#00ff00", ... }
            var coloresMap = new Dictionary<string, string>();

            int total = dt.Rows.Count;
            for (int i = 0; i < total; i++)
            {
                DataRow row = dt.Rows[i];
                string embajador = row["NombreEmbajador"].ToString();
                int cantidad = Convert.ToInt32(row["Cantidad"]);

                columnas.Add(new List<object> { embajador, cantidad });

                string color = GenerateColor(i, Math.Max(1, total)); // genera color dinámico
                coloresMap[embajador] = color;
            }

            var serializer = new JavaScriptSerializer();
            string columnasJson = serializer.Serialize(columnas);   // [["Emb1",10],["Emb2",5],...]
            string coloresJson = serializer.Serialize(coloresMap);  // {"Emb1":"#...","Emb2":"#..."}
            string categoriasJson = serializer.Serialize(new string[] { "" }); // tal como tenías

            string script = $@"
                var columnasJS = {columnasJson};
                var coloresJS = {coloresJson};
                var categoriasJS = {categoriasJson};
            ";
            ClientScript.RegisterStartupScript(this.GetType(), "chartVars", script, true);
        }

        // Generador HSL -> HEX
        private string GenerateColor(int index, int total)
        {
            double hue = (index * 360.0) / Math.Max(1, total);
            double s = 0.65;
            double l = 0.55;

            double c = (1 - Math.Abs(2 * l - 1)) * s;
            double hPrime = hue / 60.0;
            double x = c * (1 - Math.Abs(hPrime % 2 - 1));
            double r1 = 0, g1 = 0, b1 = 0;

            if (0 <= hPrime && hPrime < 1) { r1 = c; g1 = x; b1 = 0; }
            else if (1 <= hPrime && hPrime < 2) { r1 = x; g1 = c; b1 = 0; }
            else if (2 <= hPrime && hPrime < 3) { r1 = 0; g1 = c; b1 = x; }
            else if (3 <= hPrime && hPrime < 4) { r1 = 0; g1 = x; b1 = c; }
            else if (4 <= hPrime && hPrime < 5) { r1 = x; g1 = 0; b1 = c; }
            else { r1 = c; g1 = 0; b1 = x; }

            double m = l - c / 2.0;
            int r = (int)Math.Round((r1 + m) * 255);
            int g = (int)Math.Round((g1 + m) * 255);
            int b = (int)Math.Round((b1 + m) * 255);

            r = Math.Max(0, Math.Min(255, r));
            g = Math.Max(0, Math.Min(255, g));
            b = Math.Max(0, Math.Min(255, b));

            return $"#{r:X2}{g:X2}{b:X2}";
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarConcursoGymPassExcel();

                string nombreArchivo = $"ConcursoGymPass_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    cg.ExportarExcel(dt, nombreArchivo);
                }
                else
                {
                    Response.Write("<script>alert('No existen registros para esta consulta');</script>");
                    //Response.Redirect("gympass.aspx?mensaje=No existen registros para esta consulta");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
                //Response.Redirect("gympass.aspx?mensaje=" + Server.UrlEncode(ex.Message));
            }
        }
    }
}