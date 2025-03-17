using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class reportepagosmulticanal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("es-CO"); 
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Autorizaciones");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {                       

                        if (ViewState["CrearModificar"].ToString() == "1")
                        {                           
                            txbEfeFechaIni.Attributes.Add("type", "date");
                            txbEfeFechaIni.Value = DateTime.Now.ToString("yyyy-MM-01").ToString();
                            txbEfeFechaFin.Attributes.Add("type", "date");
                            txbEfeFechaFin.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                            listaTransaccionesEfectivo("Efectivo",(txbEfeFechaIni.Value.ToString()),(txbEfeFechaFin.Value.ToString()));

                            txbDataFechaIni.Attributes.Add("type", "date");
                            txbDataFechaIni.Value = DateTime.Now.ToString("yyyy-MM-01").ToString();
                            txbDataFechaFin.Attributes.Add("type", "date");
                            txbDataFechaFin.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                            listaTransaccionesDatafono("Datafono", (txbEfeFechaIni.Value.ToString()), (txbEfeFechaFin.Value.ToString()));

                            txbTransFechaIni.Attributes.Add("type", "date");
                            txbTransFechaIni.Value = DateTime.Now.ToString("yyyy-MM-01").ToString();
                            txbTransFechaFin.Attributes.Add("type", "date");
                            txbTransFechaFin.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                            listaTransaccionesTransferencia("Transferencia", (txbTransFechaIni.Value.ToString()), (txbTransFechaFin.Value.ToString()));

                            txbWompiFechaIni.Attributes.Add("type", "date");
                            txbWompiFechaIni.Value = DateTime.Now.ToString("yyyy-MM-01").ToString();
                            txbWompiFechaFin.Attributes.Add("type", "date");
                            txbWompiFechaFin.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                            listaTransaccionesWompi("Transferencia", (txbWompiFechaIni.Value.ToString()), (txbWompiFechaFin.Value.ToString()));

                        }
                    }
                }
                else
                {
                    Response.Redirect("logout");
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

        private void listaTransaccionesEfectivo(string tipoPago, string fechaIni, string fechaFin)
        {            
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosPorTipo(tipoPago, fechaIni, fechaFin, out decimal valorTotal);
            rpTipoEfectivo.DataSource = dt;
            rpTipoEfectivo.DataBind();
            ltValorTotalEfe.Text = valorTotal.ToString("C0");
            dt.Dispose();
        }

        private void listaTransaccionesDatafono(string tipoPago, string fechaIni, string fechaFin)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosPorTipo(tipoPago, fechaIni, fechaFin, out decimal valorTotal);
            rpTipoDatafono.DataSource = dt;
            rpTipoDatafono.DataBind();
            ltValorTotalData.Text = valorTotal.ToString("C0");
            dt.Dispose();
        }

        private void listaTransaccionesTransferencia(string tipoPago, string fechaIni, string fechaFin)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosPorTipo(tipoPago, fechaIni, fechaFin, out decimal valorTotal);
            rpTransferencia.DataSource = dt;
            rpTransferencia.DataBind();
            ltValorTotalTrans.Text = valorTotal.ToString("C0");
            dt.Dispose();
        }

        private void listaTransaccionesWompi(string tipoPago, string fechaIni, string fechaFin)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt1 = listarDetalle();
            foreach (DataRow row in dt1.Rows)
            {
                row["amount_in_cents"] = Convert.ToInt32(row["amount_in_cents"]) / 100;
            }
            DataTable dt = cg.ConsultarPagosPorTipo(tipoPago, fechaIni, fechaFin, out decimal valorTotal);
            rpWompi.DataSource = dt1;
            rpWompi.DataBind();
            ltValortotalWompi.Text = valorTotal.ToString("C0");
            dt.Dispose();
        }

        private DataTable listarDetalle()
        {
            string parametro = string.Empty;
            string tester = string.Empty;
            string mensaje = string.Empty;
            int idempresa = 4;
            clasesglobales cg = new clasesglobales();
            DataTable dti = cg.ConsultarUrl(idempresa);

            string strFechaInicioMes = string.Format("{0:yyyy-MM-01}", DateTime.Now);
            string strFechaHoy = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            //parametro = "?from_date=2025-01-01&until_date=2025-03-11&page=1&page_size=50&order_by=created_at&order=DESC";
            parametro = "?from_date=" + strFechaInicioMes + "&until_date=" + strFechaHoy + "&page=1&page_size=10&order_by=created_at&order=DESC";

            string url = dti.Rows[0]["urlTest"].ToString() + parametro;
            string[] respuesta = cg.EnviarPeticionGet(url, idempresa.ToString(), out mensaje);
            JToken token = JToken.Parse(respuesta[0]);
            string prettyJson = token.ToString(Formatting.Indented);
            DataTable respuestaWompi = cg.InsertarYObtenerTransaccionesWompi(prettyJson);

            return respuestaWompi;
        }

        public class Datum
        {
            public string id { get; set; }
            public string created_at { get; set; }
            public string finalized_at { get; set; }
            public string amount_in_cents { get; set; }
            public string reference { get; set; }
            public string customer_email { get; set; }
            public string currency { get; set; }
            public string payment_method_type { get; set; }
            public string status { get; set; }
            public string status_message { get; set; }
            public string device_id { get; set; }
            public string full_name { get; set; }
            public string phone_number { get; set; }
            public CustomerData customer_data { get; set; }
        }

        public class Root
        {
            public List<Datum> data { get; set; }
        }

        public class CustomerData
        {
            public string device_id { get; set; }
            public string full_name { get; set; }
            public string phone_number { get; set; }
        }
        protected void btnFiltrarEfe_Click(object sender, EventArgs e)
        {
            listaTransaccionesEfectivo("Efectivo", txbEfeFechaIni.Value.ToString(), txbEfeFechaFin.Value.ToString());
        }

        protected void btnFiltrarData_Click(object sender, EventArgs e)
        {
            listaTransaccionesDatafono("Datafono", txbDataFechaIni.Value.ToString(), txbDataFechaFin.Value.ToString());
        }

        protected void btnFiltrarTrans_Click(object sender, EventArgs e)
        {
            listaTransaccionesTransferencia("Transferencia", txbTransFechaIni.Value.ToString(), txbTransFechaFin.Value.ToString());

        }

        protected void btnFiltrarWompi_Click(object sender, EventArgs e)
        {
            listaTransaccionesWompi("Wompi", txbWompiFechaIni.Value.ToString(), txbWompiFechaFin.Value.ToString());
        }
    }
}