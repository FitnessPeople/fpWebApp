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

        protected void lkbExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string strQuery = @"SELECT 
	                                    a.documentoAfiliado, CONCAT(a.NombreAfiliado, ' ', a.ApellidoAfiliado) AS NombreCompletoAfiliado, 
	                                    hcr.idAfiliadoPlan, COUNT(hcr.idCobro) AS Intentos, MAX(hcr.FechaIntento) AS UltimoIntento, MAX(hcr.MensajeEstado) AS Mensaje, 
	                                    p.idPlan, 
	                                    ap.valor, ap.fechaProximoCobro, ap.meses 
                                    FROM HistorialCobrosRechazados AS hcr 
                                    INNER JOIN AfiliadosPlanes AS ap ON ap.idAfiliadoPlan = hcr.idAfiliadoPlan 
                                    INNER JOIN Afiliados AS a ON a.idAfiliado = ap.idAfiliado 
                                    INNER JOIN Planes AS p ON p.idPlan = ap.idPlan 
                                    INNER JOIN (
	                                    SELECT idAfiliadoPlan, IFNULL(SUM(mesesPagados), 0) AS totalMesesPagados 
	                                    FROM PagosPlanAfiliado 
	                                    GROUP BY idAfiliadoPlan 
                                    ) pagos ON pagos.idAfiliadoPlan = ap.idAfiliadoPlan 
                                    WHERE p.debitoAutomatico = 1 
                                    AND ap.fechaProximoCobro <= CURDATE() 
                                    AND ap.fechaProximoCobro <= ap.fechaFinalPlan 
                                    AND pagos.totalMesesPagados < ap.meses
                                    AND ap.EstadoPlan IN ('Activo', 'Pendiente') 
                                    GROUP BY hcr.idAfiliadoPlan 
                                    ORDER BY Intentos ASC;";


                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);
                string nombreArchivo = $"CobrosRechazados_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    cg.ExportarExcelOk(dt, nombreArchivo);
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
            string strQuery = @"SELECT 
	                                a.documentoAfiliado, CONCAT(a.NombreAfiliado, ' ', a.ApellidoAfiliado) AS NombreCompletoAfiliado, 
	                                hcr.idAfiliadoPlan, COUNT(hcr.idCobro) AS Intentos, MAX(hcr.FechaIntento) AS UltimoIntento, MAX(hcr.MensajeEstado) AS Mensaje, 
	                                p.idPlan, 
	                                ap.valor, ap.fechaProximoCobro, ap.meses 
                                FROM HistorialCobrosRechazados AS hcr 
                                INNER JOIN AfiliadosPlanes AS ap ON ap.idAfiliadoPlan = hcr.idAfiliadoPlan 
                                INNER JOIN Afiliados AS a ON a.idAfiliado = ap.idAfiliado 
                                INNER JOIN Planes AS p ON p.idPlan = ap.idPlan 
                                INNER JOIN (
	                                SELECT idAfiliadoPlan, IFNULL(SUM(mesesPagados), 0) AS totalMesesPagados 
	                                FROM PagosPlanAfiliado 
	                                GROUP BY idAfiliadoPlan 
                                ) pagos ON pagos.idAfiliadoPlan = ap.idAfiliadoPlan 
                                WHERE p.debitoAutomatico = 1 
                                AND ap.fechaProximoCobro <= CURDATE() 
                                AND ap.fechaProximoCobro <= ap.fechaFinalPlan 
                                AND pagos.totalMesesPagados < ap.meses
                                AND ap.EstadoPlan IN ('Activo', 'Pendiente') 
                                GROUP BY hcr.idAfiliadoPlan 
                                ORDER BY Intentos ASC;";


            DataTable dt = cg.TraerDatos(strQuery);

            if (!dt.Columns.Contains("DeudaActual")) dt.Columns.Add("DeudaActual", typeof(int));

            int deudaTotalGeneral = 0;

            foreach (DataRow row in dt.Rows)
            {
                int idPlan = Convert.ToInt32(row["idPlan"]);
                int idAfiliadoPlan = Convert.ToInt32(row["idAfiliadoPlan"]);
                int valorBase = Convert.ToInt32(row["valor"]);
                int mesesPlan = Convert.ToInt32(row["meses"]);
                DateTime fechaProximoCobro = Convert.ToDateTime(row["fechaProximoCobro"]);
                DateTime fechaActual = DateTime.Now;

                int mesesAtraso = ((fechaActual.Year - fechaProximoCobro.Year) * 12) + fechaActual.Month - fechaProximoCobro.Month;

                if (fechaActual.Day >= fechaProximoCobro.Day) mesesAtraso++;

                int mesesPagados = cg.ConsultarCantidadMesesPagadosPorIdAfiliadoPlan(idAfiliadoPlan);

                int mesesRestantesPlan = Math.Max(0, mesesPlan - mesesPagados);

                int mesesACobrar = mesesAtraso > 0 ? mesesAtraso : 1;

                mesesACobrar = Math.Min(mesesACobrar, mesesRestantesPlan);

                // Monto Acumulado
                int montoTotal = 0;

                for (int i = 0; i < mesesACobrar; i++)
                {
                    int mesSimulado = mesesPagados + i;

                    int valorMes = cg.ObtenerValorMesPlanSimulado(idPlan, mesSimulado, valorBase);

                    montoTotal += valorMes;
                }

                row["DeudaActual"] = montoTotal;

                deudaTotalGeneral += montoTotal;
            }

            ltCuantos.Text = dt.Rows.Count.ToString();
            ltTotalPorRecuadar.Text = String.Format("{0:C0}", deudaTotalGeneral);

            rpHistorialCobrosRechazados.DataSource = dt;
            rpHistorialCobrosRechazados.DataBind();
            dt.Dispose();
        }
    }
}