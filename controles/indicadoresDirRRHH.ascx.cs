using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresDirRRHH : System.Web.UI.UserControl
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
                DataTable dt = cg.ConsultarIndicadoresInicioDirRRHH();

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    lblTotalActivos.Text = row["TotalEmpleadosActivos"].ToString();
                    lblRotacionMes.Text = row["RotacionMes"].ToString();
                    lblIngresosMes.Text = row["NuevasContratacionesMes"].ToString();
                    lblNomina.Text = Convert.ToDecimal(row["CostoNominaMensual"]).ToString("N0");
                    lblAntiguedad.Text = row["AntiguedadPromedioAnios"].ToString();
                    lblProfesionalizacion.Text = row["IndiceProfesionalizacion"].ToString();
                    lblEstabilidad.Text = row["IndiceEstabilidad"].ToString();
                    lblSedesActivas.Text = row["SedesConPersonal"].ToString();
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name,Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error: " + idLog, "error");
            }
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
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

            ScriptManager.RegisterStartupScript(this, GetType(),
                "SweetAlert", script, true);
        }
    }
}