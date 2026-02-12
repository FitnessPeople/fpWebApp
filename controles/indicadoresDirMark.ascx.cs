using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresDirMark : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarIndicadores();
            }
        }

        protected void CargarIndicadores()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarIndicadoresInicioDirMarketing();

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    lblVentasMarketingMes.Text = "$ " + dt.Rows[0]["VentasMarketingMes"].ToString();
                    lblCampaniaTop.Text = dt.Rows[0]["CampaniaTop"].ToString();
                    lblRoiMarketingMes.Text = dt.Rows[0]["RoiMarketingMes"].ToString() + " %";
                    lblCrecimientoMarketing.Text = dt.Rows[0]["CrecimientoVsMesAnterior"].ToString() + " %";
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