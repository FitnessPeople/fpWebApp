using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresLidAsis : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarIndicadores();
            }
        }

        private void CargarIndicadores()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = new DataTable();
                dt = cg.ConsultarIndicadoresInicioCoordAsistencial();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    lblTotalAfiliados.Text = Convert.ToInt32(row["TotalAfiliadosActivos"]).ToString("N0");
                    lblHistoriasMes.Text = Convert.ToInt32(row["HistoriasClinicasMes"]).ToString("N0");
                    lblCobertura.Text = Convert.ToDecimal(row["PorcentajeCoberturaClinica"]).ToString("N2");
                    lblIMC.Text = Convert.ToDecimal(row["PromedioIMC"]).ToString("N2");
                    lblAptos.Text = Convert.ToDecimal(row["PorcentajeAptos"]).ToString("N2");
                    lblIncapacidades.Text = Convert.ToInt32(row["IncapacidadesActivas"]).ToString("N0");
                    lblRiesgo.Text = Convert.ToInt32(row["AfiliadosEnRiesgo"]).ToString("N0");
                    lblOcupacion.Text = Convert.ToDecimal(row["PorcentajeOcupacionAgenda"]).ToString("N2");
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