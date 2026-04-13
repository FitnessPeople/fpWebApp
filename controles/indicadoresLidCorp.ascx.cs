using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresLidCorp : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int mesActual = DateTime.Now.Month;
                int anioActual = DateTime.Now.Year;
                CargarIndicadores(mesActual, anioActual,70000000);
                string nombreMes = new DateTime(anioActual, mesActual, 1)
               .ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));


            }
        }

        private void CargarIndicadores(int mes, int annio, int metames)
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                DataTable dt = cg.ConsultarIndicadoresInicioLidCoporativo(mes, annio, metames);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    lblVentaTotal.Text = Convert.ToDecimal(row["VentaTotalMes"]).ToString("N0");
                    lblVentaMesAnterior.Text = Convert.ToDecimal(row["VentaMesAnterior"]).ToString("N0");
                    lblVariacion.Text = Convert.ToDecimal(row["Variacion"]).ToString("0.##");
                    lblProyeccion.Text = Convert.ToDecimal(row["Proyeccion"]).ToString("N0");
                    lblRitmoReal.Text = Convert.ToDecimal(row["RitmoReal"]).ToString("0.##");
                    lblRitmoEsperado.Text = Convert.ToDecimal(row["RitmoEsperado"]).ToString("0.##");
                    lblDiasRestantes.Text = row["DiasRestantes"].ToString();
                    lblPresion.Text = Convert.ToDecimal(row["Presion"]).ToString("0.##");
                    lblRecuperacion.Text = Convert.ToDecimal(row["Recuperacion"]).ToString("N0");
                    lblVisitasEfectivas.Text = row["VisitasEfectivas"].ToString();
                    lblTasaEfectividad.Text = Convert.ToDecimal(row["TasaEfectividad"]).ToString("0.##");
                    lblVentaDirecta.Text = Convert.ToDecimal(row["VentaDirecta"]).ToString("N0");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", ex.Message, "error");
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