using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresAseCom : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarIndicadoresAsesor();
        }
        protected void CargarIndicadoresAsesor()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                if (Session["idUsuario"] == null)
                    return;

                int idUsuario = Convert.ToInt32(Session["idUsuario"]);               

                DataTable dt = cg.ConsultarIndicadoresInicioAsesor(idUsuario);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    lblRitmoReal.Text = row["RitmoReal"].ToString();
                    lblRitmoEsperado.Text = row["RitmoEsperado"].ToString();

                    lblProyeccionCierre.Text = row["ProyeccionCierre"].ToString();
                    lblMetaMes.Text = row["MetaMes"].ToString();

                    lblDiasHabiles.Text = row["DiasRestantes"].ToString();
                    lblVentaNecesariaDia.Text = row["VentaNecesariaDia"].ToString();

                    lblPresion.Text = row["Presion"].ToString();

                    lblRankingActual.Text = row["RankingActual"].ToString();

                    lblVentasAcumuladas.Text = row["VentasAcumuladas"].ToString();
                    lblDiferenciaMeta.Text = row["DiferenciaMeta"].ToString();

                    lblPromedioDiario.Text = row["PromedioDiario"].ToString();
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            clasesglobales cg = new clasesglobales();

            // tipo puede ser: 'success', 'error', 'warning', 'info', 'question'
            string script = $@"
                Swal.hideLoading();
                Swal.fire({{
                title: '{titulo}',
                text: '{mensaje}',
                icon: '{tipo}', 
                allowOutsideClick: false, 
                showCloseButton: false, 
                confirmButtonText: 'Aceptar'
            }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
        }
    }
}