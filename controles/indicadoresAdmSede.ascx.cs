using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresAdmSede : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarIndicadoresAdminSede();
            CargarKPIAsesores(Convert.ToInt32(Session["idCanalVenta"]));
        }

        protected void CargarIndicadoresAdminSede()
        {
            clasesglobales cg = new clasesglobales();

            DataTable dt = cg.ConsultarIndicadoresInicioAdminSede( Convert.ToInt32(Session["idcanalVenta"]));

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                lblVentasMes.Text = Convert.ToDecimal(row["VentasMes"]).ToString("N0");

                lblMetaMes.Text = Convert.ToDecimal(row["MetaMes"]).ToString("N0");

                lblCumplimientoMes.Text = Convert.ToDecimal(row["CumplimientoMes"]).ToString("N2") + "%";

                lblTicketPromedio.Text = Convert.ToDecimal(row["TicketPromedio"]).ToString("N2");
            }
        }

        protected void CargarKPIAsesores(int idCanalVenta)
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarIndicadoresKPIAsesoresenCanalVentaMesActual(idCanalVenta);

                if (dt != null && dt.Rows.Count > 0)
                {
                    rptKPIAsesores.DataSource = dt;
                    rptKPIAsesores.DataBind();
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