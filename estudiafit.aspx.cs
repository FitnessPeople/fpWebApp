using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
	public partial class estudiafit : System.Web.UI.Page
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
                            CargarGraficaBarras();
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
            DataTable dt = cg.ConsultarEstudiafit();
            rpEstudiafit.DataSource = dt;
            rpEstudiafit.DataBind();
            dt.Dispose();
        }

        private void CargarGraficaBarras()
        {
            string query = @"SELECT eu.nombre AS 'NombreUniversidad', COUNT(*) AS Cantidad
                    FROM Estudiafit e 
                    INNER JOIN EstudiafitUni eu 
                    ON e.idUni = eu.idUni 
                    GROUP BY eu.nombre;";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(query);

            var columnas = new List<string>();
            var coloresUsados = new Dictionary<string, string>();

            foreach (DataRow row in dt.Rows)
            {
                string universidad = row["NombreUniversidad"].ToString();
                int cantidad = Convert.ToInt32(row["Cantidad"]);

                columnas.Add($"['{universidad}', {cantidad}]");

                if (coloresFijos.ContainsKey(universidad))
                {
                    coloresUsados[universidad] = coloresFijos[universidad];
                }
                else
                {
                    // Color por defecto si no está en la lista
                    coloresUsados[universidad] = "#7f7f7f";
                }
            }

            string columnasJS = string.Join(",", columnas);
            string coloresJS = string.Join(",", coloresUsados.Select(kv => $"'{kv.Key}': '{kv.Value}'"));

            ClientScript.RegisterStartupScript(this.GetType(), "setVars",
                $"var columnasJS = [{columnasJS}]; var coloresJS = {{{coloresJS}}};", true);


            // Gráfica de chartjs
            string labelsJS = string.Join(",", dt.Rows.Cast<DataRow>()
                .Select(row => $"'{row["NombreUniversidad"].ToString()}'"));

            string dataJS = string.Join(",", dt.Rows.Cast<DataRow>()
                .Select(row => row["Cantidad"].ToString()));

            string backgroundColorsJS = string.Join(",", dt.Rows.Cast<DataRow>()
                .Select(row =>
                {
                    string universidad = row["NombreUniversidad"].ToString();
                    return $"'{(coloresFijos.ContainsKey(universidad) ? coloresFijos[universidad] : "#7f7f7f")}'";
                }));

            ClientScript.RegisterStartupScript(this.GetType(), "chartjsVars", $@"
                var chartLabels = [{labelsJS}];
                var chartData = [{dataJS}];
                var chartColors = [{backgroundColorsJS}];
            ", true);
        }

        Dictionary<string, string> coloresFijos = new Dictionary<string, string>
        {
            { "Universidad 1", "#1f77b4" },
            { "Universidad 2", "#ff7f0e" },
            { "Universidad 3", "#2ca02c" },
            { "Universidad 4", "#d62728" },
            { "Universidad 5", "#7401a0" },
            { "Universidad 6", "#8c564b" },
            { "Universidad 7", "#e377c2" }
        };

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarEstudiafitExcel();

                string nombreArchivo = $"Estudiafit_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

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