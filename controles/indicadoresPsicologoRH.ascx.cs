using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresPsicologoRH : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int mesActual = DateTime.Now.Month;
                int anioActual = DateTime.Now.Year;
                CargarIndicadores();
                string nombreMes = new DateTime(anioActual, mesActual, 1)
               .ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));

                lblTituloCumple.Text = "🎂 Cumpleaños de " +
                                       char.ToUpper(nombreMes[0]) +
                                       nombreMes.Substring(1) +
                                       " " + anioActual;
                CargarCumpleanosMes(mesActual, anioActual);
            }
        }

        private void CargarIndicadores()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.consultarIndicadoresInicioPsicologoRH();

                if (dt != null && dt.Rows.Count > 0)
                {
                    lblTotalActivos.Text = dt.Rows[0]["TotalEmpleadosActivos"].ToString();
                    lblIngresosMes.Text = dt.Rows[0]["IngresosMesActual"].ToString();
                    lblConUsuario.Text = dt.Rows[0]["IngresosConUsuarioMes"].ToString();
                    lblDigitalizacion.Text = Convert.ToDecimal(dt.Rows[0]["PorcentajeDigitalizacionMes"]).ToString("0.##");
                    lblSinUsuario.Text = dt.Rows[0]["IngresosSinUsuarioMes"].ToString();
                    lblCargos.Text = dt.Rows[0]["CargosOcupadosMes"].ToString();
                    lblCumpleanios.Text = dt.Rows[0]["CumpleaniosMesActual"].ToString();
                    lblRetirosMes.Text = dt.Rows[0]["RetirosMesActual"].ToString();
                }

            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }

        protected void CargarCumpleanosMes(int mes, int annio)
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                DataTable dt = cg.consultarCumpleanosEmpleadosMes(mes, annio);

                if (dt != null && dt.Rows.Count > 0)
                {
                    rptCumpleanos.DataSource = dt;
                    rptCumpleanos.DataBind();
                }
                else
                {
                    rptCumpleanos.DataSource = null;
                    rptCumpleanos.DataBind();
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso",
                    "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error: " + idLog,
                    "error");
            }
        }

        protected void rptCumpleanos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image img = (Image)e.Item.FindControl("imgEmpleado");

                string foto = DataBinder.Eval(e.Item.DataItem, "FotoEmpleado")?.ToString();

                if (!string.IsNullOrEmpty(foto))
                {
                    img.ImageUrl = foto;
                }
                else
                {
                    img.ImageUrl = "~/images/user-default.png";
                }
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