using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class reportepagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Pagos");
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
                            //lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            //btnAgregar.Visible = true;
                        }
                    }
                    listaTransacciones();
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
            rpPagos.DataSource = dt;
            rpPagos.DataBind();
            dt.Dispose();
        }

        private string listarDetalle(int idAfiliadoPlan)
        {
            string parametro = string.Empty;
            string nomAfiliado = string.Empty;
            string mensaje = string.Empty;

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosWompiPorId(idAfiliadoPlan);
            DataTable dti = cg.ConsultarUrl(1);

            if (dt.Rows.Count > 0)
            {
                parametro = dt.Rows[0]["IdReferencia"].ToString();
                nomAfiliado = dt.Rows[0]["NombreAfiliado"].ToString();
            }

            string url = dti.Rows[0]["urlTest"].ToString() + parametro;
            string[] respuesta = cg.EnviarPeticionGet(url, out mensaje);
            JToken token = JToken.Parse(respuesta[0]);
            string prettyJson = token.ToString(Formatting.Indented);

            if (mensaje == "Ok") //Verifica respuesta ok
            {
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

                StringBuilder sb = new StringBuilder();

                sb.Append("<table class=\"table table-bordered table-striped\">");
                sb.Append("<tr>");
                sb.Append("<th>ID</th><th>Afiliado</th><th>Fecha Cre.</th><th>Fecha Fin.</th><th>Valor</th>");
                sb.Append("<th>Método Pago</th><th>Estado</th><th>Referencia</th><th>Tarjeta</th><th>Estado3DS</th>");
                sb.Append("</tr>");


                foreach (var pago in listaPagos)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{pago.Id}</td>");
                    sb.Append($"<td>{nomAfiliado}</td>");
                    sb.Append($"<td>" + String.Format("{0:dd MMM yyyy}", Convert.ToDateTime(pago.FechaCreacion)) + "</td>");
                    sb.Append($"<td>" + String.Format("{0:dd MMM yyyy}", Convert.ToDateTime(pago.FechaFinalizacion)) + "</td>");
                    sb.Append($"<td>{pago.Valor}</td>");
                    sb.Append($"<td>{pago.MetodoPago}</td>");
                    sb.Append($"<td>{pago.Estado}</td>");
                    sb.Append($"<td>{pago.Referencia}</td>");
                    sb.Append($"<td>{pago.NombreTarjeta}</td>");
                    sb.Append($"<td>{pago.Estado3DS}</td>");
                    sb.Append("</tr>");
                }

                sb.Append("</table>");

                return sb.ToString();
            }
            else
            {
                return prettyJson;
            }
        }

        protected void rpPagos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnVer = (HtmlAnchor)e.Item.FindControl("btnVer");
                    btnVer.Attributes.Add("href", "reportepagoswompi?verid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnVer.Visible = false;
                }
            }

        }

        private static string EnviarPeticion(string url, out bool mensaje)
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
                    mensaje = true;
                }

                return resultado;
            }
            catch (Exception ex)
            {
                string jsonError = JsonConvert.SerializeObject(new { error = "Error al enviar la petición: " + ex.Message });
                mensaje = false;
                resultado = jsonError;
                return resultado;
            }
        }


        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {

        }



        //protected void btnDetalle_Command(object sender, CommandEventArgs e)
        //{
        //    if (e.CommandName == "mostrarDetalle")
        //    {
        //        int idAfiliadoPlan = int.Parse(e.CommandArgument.ToString());
        //        RepeaterItem item = ((Control)sender).NamingContainer as RepeaterItem;

        //        if (item != null)
        //        {
        //            HiddenField hfDetalle = (HiddenField)this.FindControl("hfDetalle");
        //            Literal ltDetalleModal = (Literal)this.FindControl("ltDetalleModal");

        //            if (hfDetalle != null && ltDetalleModal != null)
        //            {
        //                string detalle = listarDetalle(idAfiliadoPlan);
        //                hfDetalle.Value = detalle;
        //                ltDetalleModal.Text = detalle;
        //            }

        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal",
        //                "setTimeout(function() { $('#ModalDetalle').modal('show'); }, 500);", true);
        //        }
        //    }
        //}

        protected void btnDetalle_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "mostrarDetalle")
            {
                int idAfiliadoPlan = int.Parse(e.CommandArgument.ToString());

                // Buscar el control en toda la página
                Literal ltDetalleModal = (Literal)Page.FindControl("ltDetalleModal");

                if (ltDetalleModal != null)
                {
                    ltDetalleModal.Text = listarDetalle(idAfiliadoPlan);
                }

                // Mostrar la modal con JavaScript
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal",
                   "setTimeout(function() { $('#ModalDetalle').modal('show'); }, 500);", true);
            }
        }


    }
}