using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class reportespagoswompi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Pagos Wompi");
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
                        divBotonesLista.Visible = false;
                        //btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            //btnAgregar.Visible = true;
                        }
                    }
                    listaTransacciones();
                    string parametro = string.Empty;
                    if (Request.QueryString.Count > 0)
                        if (Request.QueryString["verid"] != null)
                        {
                            //Boton ver detalles
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarPagosWompiPorId(int.Parse(Request.QueryString["verid"].ToString()));
                            DataTable dti = cg.ConsultarUrl(1);

                            if (dt.Rows.Count > 0)
                            {
                                parametro = dt.Rows[0]["IdReferenciaWompi"].ToString();
                            }

                            string url = dti.Rows[0]["urlTest"].ToString() + parametro;
                            string rta = EnviarPeticion(url);
                            JToken token = JToken.Parse(rta);
                            string prettyJson = token.ToString(Formatting.Indented);
                            //txbPago.Text = prettyJson;
                            Console.WriteLine(prettyJson);

                            JObject jsonData = JObject.Parse(prettyJson); 

                            List<pagoswompidet> listaPagos = new List<pagoswompidet>
                            {
                                new pagoswompidet
                                {
                                    Id = jsonData["data"]["id"]?.ToString(),
                                    FechaCreacion = jsonData["data"]["created_at"]?.ToString(),
                                    FechaFinalizacion = jsonData["data"]["finalized_at"]?.ToString(),
                                    Valor = ((jsonData["data"]["amount_in_cents"]?.Value<int>() ?? 0) / 100).ToString("N0") + " " + jsonData["data"]["currency"]?.ToString(),
                                    Moneda = jsonData["data"]["currency"]?.ToString(),
                                    MetodoPago = jsonData["data"]["payment_method_type"]?.ToString(),
                                    Estado = jsonData["data"]["status"]?.ToString(),
                                    Referencia = jsonData["data"]["reference"]?.ToString(),
                                    NombreTarjeta = jsonData["data"]["payment_method"]["extra"]["name"]?.ToString(),
                                    UltimosDigitos = jsonData["data"]["payment_method"]["extra"]["last_four"]?.ToString(),
                                    MarcaTarjeta = jsonData["data"]["payment_method"]["extra"]["brand"]?.ToString(),
                                    TipoTarjeta = jsonData["data"]["payment_method"]["extra"]["card_type"]?.ToString(),
                                    NombreComercio = jsonData["data"]["merchant"]["name"]?.ToString(),
                                    ContactoComercio = jsonData["data"]["merchant"]["contact_name"]?.ToString(),
                                    TelefonoComercio = jsonData["data"]["merchant"]["phone_number"]?.ToString(),
                                    URLRedireccion = jsonData["data"]["redirect_url"]?.ToString(),
                                    PaymentLinkId = jsonData["data"]["payment_link_id"]?.ToString(),
                                    PublicKeyComercio = jsonData["data"]["merchant"]["public_key"]?.ToString(),
                                    EmailComercio = jsonData["data"]["merchant"]["email"]?.ToString(),
                                    Estado3DS = jsonData["data"]["payment_method"]["extra"]["three_ds_auth"]["three_ds_auth"]["current_step_status"]?.ToString()                                }
                            };

                            var consultaUnificada = from pago in listaPagos
                                                    join row in dt.AsEnumerable()
                                                    on pago.Id equals row["IdReferenciaWompi"]?.ToString() into detalles
                                                    from detalle in detalles.DefaultIfEmpty()
                                                    select new
                                                    {
                                                        IdTransaccion = pago.Id,
                                                        FechaCreacion = pago.FechaCreacion,
                                                        FechaFinalizacion = pago.FechaFinalizacion,
                                                        ValorPago = pago.Valor,
                                                        Moneda = pago.Moneda,
                                                        MetodoPago = pago.MetodoPago,
                                                        EstadoPago = pago.Estado,
                                                        ReferenciaPago = pago.Referencia,
                                                        NombreTarjeta = pago.NombreTarjeta,
                                                        UltimosDigitos = pago.UltimosDigitos,
                                                        MarcaTarjeta = pago.MarcaTarjeta,
                                                        TipoTarjeta = pago.TipoTarjeta,
                                                        NombreComercio = pago.NombreComercio,
                                                        ContactoComercio = pago.ContactoComercio,
                                                        TelefonoComercio = pago.TelefonoComercio,
                                                        URLRedireccion = pago.URLRedireccion,
                                                        PaymentLinkId = pago.PaymentLinkId,
                                                        PublicKeyComercio = pago.PublicKeyComercio,
                                                        EmailComercio = pago.EmailComercio,
                                                        Estado3DS = pago.Estado3DS,

                                                        //// Verificar valores antes de convertir
                                                        //IdAfiliadoPlan = detalle != null && detalle.Table.Columns.Contains("idAfiliadoPlan") && detalle["idAfiliadoPlan"] != DBNull.Value
                                                        //                 ? detalle["idAfiliadoPlan"].ToString()
                                                        //                 : "No disponible",

                                                        //NombreAfiliado = detalle != null && detalle.Table.Columns.Contains("NombreAfiliado") && detalle["NombreAfiliado"] != DBNull.Value
                                                        //                 ? detalle["NombreAfiliado"].ToString()
                                                        //                 : "No disponible",

                                                        //Valor = detalle != null && detalle.Table.Columns.Contains("Valor") && detalle["Valor"] != DBNull.Value
                                                        //        ? Convert.ToDecimal(detalle["Valor"], CultureInfo.InvariantCulture).ToString("N2")
                                                        //        : "0.00",

                                                        //IdReferenciaWompi = detalle != null && detalle.Table.Columns.Contains("IdReferenciaWompi") && detalle["IdReferenciaWompi"] != DBNull.Value
                                                        //                    ? detalle["IdReferenciaWompi"].ToString()
                                                        //                    : "No disponible",

                                                        //EntornoPago = detalle != null && detalle.Table.Columns.Contains("pa.env") && detalle["pa.env"] != DBNull.Value
                                                        //              ? detalle["pa.env"].ToString()
                                                        //              : "No disponible",

                                                        //FechaHoraPago = detalle != null && detalle.Table.Columns.Contains("pa.FechaHoraPago") && detalle["pa.FechaHoraPago"] != DBNull.Value
                                                        //               ? Convert.ToDateTime(detalle["pa.FechaHoraPago"]).ToString("yyyy-MM-dd HH:mm:ss")
                                                        //               : "No disponible",

                                                        //IdSede = detalle != null && detalle.Table.Columns.Contains("a.idSede") && detalle["a.idSede"] != DBNull.Value
                                                        //        ? detalle["a.idSede"].ToString()
                                                        //        : "No disponible"
                                                    };

                            GridView1.DataSource = consultaUnificada.ToList();
                            GridView1.DataBind();



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

        private void listaTransacciones()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosPlanAfiliados();
            rpPagosWompi.DataSource = dt;
            rpPagosWompi.DataBind();
            dt.Dispose();
        }


        protected void rpPagosWompi_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnVer = (HtmlAnchor)e.Item.FindControl("btnVer");
                    btnVer.Attributes.Add("href", "reportepagoswompi?verid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnVer.Visible = true;
                }
            }
        }

        private static string EnviarPeticion(string url)
        {
            string resultado = "";

            try
            {
                WebRequest oRequest = WebRequest.Create(url);
                oRequest.Method = "GET";
                oRequest.ContentType = "application/json;charset=UTF-8";

                WebResponse oResponse = oRequest.GetResponse();
                using (var oSr = new StreamReader(oResponse.GetResponseStream()))
                {
                    resultado = oSr.ReadToEnd().Trim();
                }

                return resultado;
            }
            catch (Exception ex)
            {
                return "Error al enviar la petición: " + ex.Message;
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {

        }




    }
}