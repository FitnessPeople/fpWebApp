using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresDirCom : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarIndicadores();
        }

        protected void CargarIndicadores()
        {
            clasesglobales cg = new clasesglobales();

            /* =========================================================
               INDICADORES PRINCIPALES
            ========================================================= */
            DataTable dt = cg.ConsultarIndicadoresInicioDirComercial();

            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow row = dt.Rows[0];

            // ================== KPIs CABECERA ==================

            // 1️⃣ Ritmo vs tiempo
            lblRitmoReal.Text = Convert.ToDecimal(row["PorcentajeCumplimiento"]).ToString("N2");

            decimal diasMes = Convert.ToDecimal(row["DiasHabilesMes"]);
            decimal diasTrans = Convert.ToDecimal(row["DiasHabilesTranscurridos"]);

            decimal ritmoEsperado = diasMes > 0 ? (diasTrans / diasMes) * 100 : 0;
            lblRitmoEsperado.Text = ritmoEsperado.ToString("N2");

            // 2️⃣ Proyección cierre de mes
            decimal ventaAcumulada = Convert.ToDecimal(row["VentaAcumulada"]);
            decimal metaMensual = Convert.ToDecimal(row["MetaMensual"]);

            decimal proyeccionCierre = diasTrans > 0
                ? (ventaAcumulada / diasTrans) * diasMes
                : 0;

            lblProyeccionCierre.Text = proyeccionCierre.ToString("C0");
            lblMetaMes.Text = metaMensual.ToString("C0");

            // 3️⃣ Días hábiles restantes
            lblDiasHabiles.Text = row["DiasHabilesRestantes"].ToString();

            decimal faltante = Convert.ToDecimal(row["VentaFaltante"]);
            decimal diasRestantes = Convert.ToDecimal(row["DiasHabilesRestantes"]);

            decimal ventaNecesariaDia = diasRestantes > 0
                ? faltante / diasRestantes
                : 0;

            lblVentaNecesariaDia.Text = ventaNecesariaDia.ToString("C0");

            // 4️⃣ Presión comercial
            decimal promedioDiarioActual = diasTrans > 0
                ? ventaAcumulada / diasTrans
                : 0;

            decimal presion = promedioDiarioActual > 0
                ? ventaNecesariaDia / promedioDiarioActual
                : 0;

            lblPresion.Text = presion.ToString("N2");

            // ================== SEGUNDA FILA ==================

            // 5️⃣ Presupuesto total
            lblPresupuestoTotal.Text = metaMensual.ToString("C0");

            // 6️⃣ Variación vs mes anterior (placeholder)
            lblVariacionMes.Text = "0.00";

            /* =========================================================
               CONCENTRACIÓN DE METAS (SP INDEPENDIENTE)
            ========================================================= */
            DataTable dtCon = cg.ConsultarIndicadoresInicioDirComercialConcetracionMetas();

            if (dtCon != null && dtCon.Rows.Count > 0)
            {
                DataRow rCon = dtCon.Rows[0];

                // 7️⃣ Canal con mayor meta
                lblCanalTop.Text = rCon["CanalTop"].ToString();
                lblMetaCanalTop.Text = Convert.ToDecimal(rCon["MetaCanalTop"]).ToString("C0");

                // 8️⃣ Concentración Top 3
                lblConcentracion.Text =
                    Convert.ToDecimal(rCon["ConcentracionTop3"]).ToString("N2") + "%";
            }
            else
            {
                lblCanalTop.Text = "—";
                lblMetaCanalTop.Text = "—";
                lblConcentracion.Text = "—";
            }
        }


    }
}