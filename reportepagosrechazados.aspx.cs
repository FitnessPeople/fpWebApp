using System;
using System.Data;
using System.Globalization;
using System.Threading;

namespace fpWebApp
{
    public partial class reportepagosrechazados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("es-CO");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Débitos rechazados");
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
                        //btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            //lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            //lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            divPagosRechazados.Visible = true;
                            HistorialCobrosRechazados();
                        }
                    }
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

        protected void lkbExcel1_Click(object sender, EventArgs e)
        {
            try
            {
                string strQuery = @"
                    SELECT ap.idAfiliadoPlan, a.DocumentoAfiliado, CONCAT(a.NombreAfiliado, "" "", a.ApellidoAfiliado) AS NombreCompletoAfiliado, 
                    COUNT(a.idAfiliado) AS Intentos, MAX(hcr.FechaIntento) AS UltimoIntento, MAX(hcr.MensajeEstado) AS Mensaje 
                    FROM HistorialCobrosRechazados AS hcr 
                    INNER JOIN AfiliadosPlanes AS ap ON ap.idAfiliadoPlan = hcr.idAfiliadoPlan 
                    INNER JOIN Afiliados AS a ON a.idAfiliado = ap.idAfiliado 
                    GROUP BY ap.idAfiliadoPlan, a.DocumentoAfiliado, NombreCompletoAfiliado;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);
                string nombreArchivo = $"cobrosrechazados_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    cg.ExportarExcel(dt, nombreArchivo);
                }
                else
                {
                    Response.Write("<script>alert('No existen registros para esta consulta');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
            }
        }

        private void HistorialCobrosRechazados()
        {
            clasesglobales cg = new clasesglobales();

            string strQuery = @"
                SELECT ap.idAfiliadoPlan, a.DocumentoAfiliado, CONCAT(a.NombreAfiliado, "" "", a.ApellidoAfiliado) AS NombreCompletoAfiliado, 
                COUNT(a.idAfiliado) AS Intentos, MAX(hcr.FechaIntento) AS UltimoIntento, MAX(hcr.MensajeEstado) AS Mensaje, p.PrecioBase 
                FROM HistorialCobrosRechazados AS hcr 
                INNER JOIN AfiliadosPlanes AS ap ON ap.idAfiliadoPlan = hcr.idAfiliadoPlan 
                INNER JOIN Afiliados AS a ON a.idAfiliado = ap.idAfiliado 
                INNER JOIN Planes AS p ON p.idPlan = ap.idPlan 
                GROUP BY ap.idAfiliadoPlan, a.DocumentoAfiliado, NombreCompletoAfiliado";


            DataTable dt = cg.TraerDatos(strQuery);

            decimal sumatoriaValor = 0;

            if (dt.Rows.Count > 0)
            {
                object suma = dt.Compute("SUM(PrecioBase)", "");
                sumatoriaValor = suma != DBNull.Value ? Convert.ToDecimal(suma) : 0;
            }

            ltCuantos.Text = dt.Rows.Count.ToString();
            ltTotalPorRecuadar.Text = String.Format("{0:C0}", sumatoriaValor);

            rpHistorialCobrosRechazados.DataSource = dt;
            rpHistorialCobrosRechazados.DataBind();
        }
    }
}