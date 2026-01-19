using DocumentFormat.OpenXml.Wordprocessing;
using fpWebApp.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPage.Services;
using static fpWebApp.Services.SiigoClient;

namespace fpWebApp
{
    public partial class planesAfiliado : System.Web.UI.Page
    {
        // PRUEBAS
        //static int idIntegracionSiigo = 3; // SIIGO
        // PRODUCCIÓN
        static int idIntegracionSiigo = 6; // SIIGO
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(),
                "SweetAlert", "https://cdn.jsdelivr.net/npm/sweetalert2@11/sweetalert2.all.min.js");
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Afiliados");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        txbFechaInicio.Attributes.Add("type", "date");

                        DateTime dtHoy = DateTime.Now;
                        DateTime dt60 = DateTime.Now.AddMonths(2);
                        txbFechaInicio.Attributes.Add("min", dtHoy.Year.ToString() + "-" + String.Format("{0:MM}", dtHoy) + "-" + String.Format("{0:dd}", dtHoy));
                        txbFechaInicio.Attributes.Add("max", dt60.Year.ToString() + "-" + String.Format("{0:MM}", dt60) + "-" + String.Format("{0:dd}", dt60));
                        txbFechaInicio.Text = String.Format("{0:yyyy-MM-dd}", dtHoy);

                        ViewState.Add("precioTotal", 0);
                        ltPrecioBase.Text = "$0";
                        ltDescuento.Text = "0%";
                        ltPrecioFinal.Text = "$0";
                        ltAhorro.Text = "$0";
                        ltConDescuento.Text = "$0";

                        ltNombrePlan.Text = "Nombre del plan";

                        ViewState["DiasCortesia"] = 0;
                        ListaPlanes();
                        CargarAfiliado();
                        CargarPlanesAfiliado();
                        ConsultarCodDatafono();

                        txbWompi.Enabled = false;
                        txbDatafono.Enabled = false;
                        txbEfectivo.Enabled = false;
                        txbTransferencia.Enabled = false;
                        txbNroAprobacion.Enabled = false;
                        cbPagaCounter.Enabled = false;

                    }
                    else
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
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

        private void ListaPlanes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanesVigentesVisibleCRM();
            rpPlanes.DataSource = dt;
            rpPlanes.DataBind();
            dt.Dispose();
        }

        private void CargarAfiliado()
        {
            string idcrm = string.Empty;

            string parametro = string.Empty;
            Session["IdAfiliado"] = string.Empty;
            Session["IdCRM"] = "0";
            lbAgregarPlan.Visible = true;
            btnCancelar.Visible = true;
            btnVolver.Visible = false;
            clasesglobales cg = new clasesglobales();

            string editid = Request.QueryString["id"];
            string idAfil = Request.QueryString["idAfil"];
            idcrm = Request.QueryString["idcrm"];

            if (Request.QueryString.Count > 0)
            {

                if (!string.IsNullOrEmpty(idAfil))
                {

                    parametro = idAfil;
                    btnCancelar.Visible = false;
                    btnVolver.Visible = true;
                    Session["IdAfiliado"] = parametro.ToString();
                    Session["idcrm"] = idcrm;
                }
                else if (!string.IsNullOrEmpty(editid))
                {
                    parametro = editid;
                }
                Session["IdAfiliado"] = parametro.ToString();

                DataTable dt = cg.ConsultarAfiliadoSede(Convert.ToInt32(parametro));

                if (dt.Rows.Count > 0)
                {
                    ViewState["DocumentoAfiliado"] = dt.Rows[0]["DocumentoAfiliado"].ToString();
                    ltNombre.Text = dt.Rows[0]["NombreAfiliado"].ToString();
                    ltApellido.Text = dt.Rows[0]["ApellidoAfiliado"].ToString();
                    ltEmail.Text = "(" + dt.Rows[0]["SiglaDocumento"].ToString() + ": " + dt.Rows[0]["DocumentoAfiliado"].ToString() + "), " + dt.Rows[0]["EmailAfiliado"].ToString();
                    ViewState["EmailAfiliado"] = dt.Rows[0]["EmailAfiliado"].ToString();
                    ltCelular.Text = "<a href=\"https://wa.me/57" + dt.Rows[0]["CelularAfiliado"].ToString() + "\" target=\"_blank\">" + dt.Rows[0]["CelularAfiliado"].ToString() + "</a>";
                    ltSede.Text = dt.Rows[0]["NombreSede"].ToString();
                    ltDireccion.Text = dt.Rows[0]["DireccionAfiliado"].ToString();
                    ltCiudad.Text = dt.Rows[0]["NombreCiudad"].ToString();
                    ltCumple.Text = String.Format("{0:dd MMM}", Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"]));
                    ltEstado.Text = "<span class=\"label label-" + dt.Rows[0]["label"].ToString() + "\">" + dt.Rows[0]["EstadoAfiliado"].ToString() + "</span>";
                    ViewState["EstadoAfiliado"] = dt.Rows[0]["EstadoAfiliado"].ToString();

                    if (dt.Rows[0]["FechaNacAfiliado"].ToString() != "1900-01-00")
                    {
                        ltCumple.Text = String.Format("{0:dd MMM}", Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"]));
                    }
                    else
                    {
                        ltCumple.Text = "-";
                    }

                    if (dt.Rows[0]["FotoAfiliado"].ToString() != "")
                    {
                        ltFoto.Text = "<img src=\"img/afiliados/" + dt.Rows[0]["FotoAfiliado"].ToString() + "\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                    }
                    else
                    {
                        if (dt.Rows[0]["idGenero"].ToString() == "1" || dt.Rows[0]["idGenero"].ToString() == "3")
                        {
                            ltFoto.Text = "<img src=\"img/afiliados/avatar_male.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                        }
                        if (dt.Rows[0]["idGenero"].ToString() == "2")
                        {
                            ltFoto.Text = "<img src=\"img/afiliados/avatar_female.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                        }
                    }
                }
                dt.Dispose();
            }
        }

        private void CargarPlanesAfiliado()
        {
            if (Request.QueryString.Count > 0)
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.CargarPlanesAfiliado(Session["IdAfiliado"].ToString(), "0", "all");
                rpPlanesAfiliado.DataSource = dt;
                rpPlanesAfiliado.DataBind();
                dt.Dispose();
            }
        }

        private void ConsultarCodDatafono()
        {
            //if (Request.QueryString.Count > 0)
            //{
            clasesglobales cg = new clasesglobales();

            //string codDatafonoQS = Request.QueryString["codDatafono"];
            string codDatafonoQS = "LM9ZZ702";

            DataTable dt = cg.ConsultarDatafonoPorCodigo(codDatafonoQS);

            string codDatafono = dt != null && dt.Rows.Count > 0 ? dt.Rows[0]["codDatafono"].ToString() : "";

            if (codDatafono != codDatafonoQS || codDatafono == "")
            {
                Response.Redirect("default");
                return;
            }

            Session["codDatafono"] = codDatafono;
            //}
            //else
            //{
            //    Response.Redirect("default");

            //}
        }

        private string ListarDetalle()
        {
            string parametro = string.Empty;
            //string tester = string.Empty;
            string mensaje = string.Empty;
            int idempresa = 4;//Wompi
            clasesglobales cg = new clasesglobales();
            DataTable dti = cg.ConsultarUrl(idempresa);

            string strFechaHoy = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            //parametro = "?from_date=2025-01-01&until_date=2025-03-11&page=1&page_size=50&order_by=created_at&order=DESC";
            parametro = "?from_date=" + strFechaHoy + "&until_date=" + strFechaHoy + "&page=1&page_size=50&order_by=created_at&order=DESC";

            //string url = dti.Rows[0]["urlTest"].ToString() + "transactions" + parametro + "&reference=" + ViewState["DocumentoAfiliado"].ToString();
            string url = dti.Rows[0]["url"].ToString() + "transactions" + parametro + "&reference=" + ViewState["DocumentoAfiliado"].ToString();
            string[] respuesta = cg.EnviarPeticionGet(url, idempresa.ToString(), out mensaje);
            JToken token = JToken.Parse(respuesta[0]);
            string prettyJson = token.ToString(Formatting.Indented);

            if (mensaje == "Ok") //Verifica respuesta ok
            {
                JObject jsonData = JObject.Parse(prettyJson);

                List<Datum> listaDatos = new List<Datum>();

                foreach (var item in jsonData["data"])
                {
                    var customerData = item["customer_data"] as JObject;
                    listaDatos.Add(new Datum
                    {
                        //id = item["id"]?.ToString(),
                        created_at = item["created_at"]?.ToString(),
                        //finalized_at = item["finalized_at"]?.ToString(),
                        amount_in_cents = ((item["amount_in_cents"]?.Value<int>() ?? 0) / 100).ToString("N0"),
                        reference = item["reference"]?.ToString(),
                        customer_email = item["customer_email"]?.ToString(),
                        currency = item["currency"]?.ToString(),
                        payment_method_type = item["payment_method_type"]?.ToString(),
                        status = item["status"]?.ToString(),
                        status_message = item["status_message"]?.ToString(),

                        device_id = customerData?["device_id"]?.ToString(),
                        full_name = customerData?["full_name"]?.ToString(),
                        phone_number = customerData?["phone_number"]?.ToString()
                    });
                }

                StringBuilder sb = new StringBuilder();

                sb.Append("<table class=\"table table-bordered table-striped\">");
                sb.Append("<tr>");
                sb.Append("<th>Afiliado</th><th>Teléfono</th><th>Fecha creación</th><th>Valor</th>");
                sb.Append("<th>Método pago</th><th>Estado</th><th>Referencia</th>");
                sb.Append("</tr>");

                foreach (var pago in listaDatos)
                {
                    string strStatus = string.Empty;
                    sb.Append("<tr>");
                    sb.Append($"<td>{pago.full_name}</td>");
                    sb.Append($"<td>{pago.phone_number}</td>");
                    sb.Append($"<td>" + String.Format("{0:dd MMM yyyy HH:mm}", Convert.ToDateTime(pago.created_at)) + "</td>");
                    sb.Append($"<td>{pago.amount_in_cents}</td>");
                    sb.Append($"<td>{pago.payment_method_type}</td>");

                    if (pago.status.ToString() == "APPROVED")
                    {
                        strStatus = "<span class=\"badge badge-info\">Aprobado</span>";
                    }
                    else
                    {
                        strStatus = "<span class=\"badge badge-danger\">Rechazado</span>";
                    }

                    sb.Append($"<td>" + strStatus + "</td>");
                    sb.Append($"<td>{pago.reference}</td>");
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
        }

        protected void rpPlanesAfiliado_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater rptPlanes = sender as Repeater;

            // Si el repeater no contiene datos
            if (rpPlanesAfiliado.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    // Muestra la etiqueta de error (si no hay datos)
                    Label lblSinRegistros = e.Item.FindControl("lblSinRegistros") as Label;
                    if (lblSinRegistros != null)
                    {
                        lblSinRegistros.Visible = true;
                    }
                }
            }
        }

        protected void txbWompi_TextChanged(object sender, EventArgs e)
        {
            string strData = ListarDetalle();
            ltDetalleWompi.Text = strData;

            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                int intTotal = Convert.ToInt32(Regex.Replace(txbWompi.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbDatafono.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbEfectivo.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbTransferencia.Text, @"[^\d]", ""));
                txbTotal.Text = intTotal.ToString("C0", new CultureInfo("es-CO"));

                // Llamar funcion que muestra la URL de pago de Wompi
                MostrarURLWompi();

                //if (Convert.ToInt32(ViewState["precioTotal"].ToString()) < Convert.ToInt32(Regex.Replace(txbTotal.Text, @"[^\d]", "")) ||
                //    Convert.ToInt32(ViewState["precioMinimo"].ToString()) > Convert.ToInt32(Regex.Replace(txbTotal.Text, @"[^\d]", "")))
                //{
                //    string script = @"
                //        Swal.fire({
                //            title: 'Precio total',
                //            text: 'Precio total incorrecto. Proporcione los valores adecuados para el total.',
                //            icon: 'error'
                //        }).then(() => {
                //        });
                //        ";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                //    return;
                //}

            }
        }

        protected void txbDatafono_TextChanged(object sender, EventArgs e)
        {
            MostrarAlertaProcesando();
            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                int intTotal = Convert.ToInt32(Regex.Replace(txbWompi.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbDatafono.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbEfectivo.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbTransferencia.Text, @"[^\d]", ""));
                txbTotal.Text = intTotal.ToString("C0", new CultureInfo("es-CO"));

                // Inicia el proceso de compra
                IniciarPago();

                //if (Convert.ToInt32(ViewState["precioTotal"].ToString()) < Convert.ToInt32(Regex.Replace(txbTotal.Text, @"[^\d]", "")) ||
                //    Convert.ToInt32(ViewState["precioMinimo"].ToString()) > Convert.ToInt32(Regex.Replace(txbTotal.Text, @"[^\d]", "")))
                //{
                //    string script = @"
                //        Swal.fire({
                //            title: 'Precio total',
                //            text: 'Precio total incorrecto. Proporcione los valores adecuados para el total.',
                //            icon: 'error'
                //        }).then(() => {
                //        });
                //        ";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                //    return;
                //}
            }
        }

        /// <summary>
        /// Función para iniciar el pago por datafono
        /// </summary>
        private async void IniciarPago()
        {
            string urlRedirect = $"planesAfiliado?id=" + Request.QueryString["id"].ToString();

            try
            {
                int precioPlan = Convert.ToInt32(Regex.Replace(txbDatafono.Text, @"[^\d]", ""));

                // Realizar compra de plan por datáfono Redeban
                bool pagoIniciado = await RealizarPagoAsync(precioPlan);

                if (!pagoIniciado)
                {
                    LimpiarPago();
                    MostrarAlerta("Error de Pago", "No se pudo iniciar el proceso de pago.", "error");
                    return;
                }

                tmrRespuesta.Enabled = true;
            }
            catch (Exception ex)
            {
                LimpiarPago();
                MostrarAlerta("Error", "Ha ocurrido un error inesperado: " + ex.Message, "error");
            }
        }

        protected async void tmrRespuesta_Tick(object sender, EventArgs e)
        {
            string urlRedirect = $"planesAfiiliado?id={Session["IdAfiliado"]}";
            int intentos = (int)(Session["intentos"] ?? 0);

            string idTransaccion = Session["idTransaccion"]?.ToString();
            string token = Session["token"]?.ToString();
            var redebanClient = CrearRedebanClient();

            if (intentos >= 15)
            {
                tmrRespuesta.Enabled = false;

                if (!string.IsNullOrEmpty(idTransaccion) && !string.IsNullOrEmpty(token))
                {
                    // Intentar borrar la transacción pendiente para evitar Cod:06
                    string resultadoBorrar = await redebanClient.BorrarTransaccionAsync(idTransaccion, token);

                    // Registrar el resultado para depuración
                    System.Diagnostics.Debug.WriteLine($"BorrarTransaccion: {resultadoBorrar}");
                }

                LimpiarPago();
                MostrarAlerta("Tiempo excedido", "No se recibió respuesta del datáfono. Por favor, intente nuevamente.", "warning");
                return;
            }

            Session["intentos"] = intentos + 1;

            if (string.IsNullOrEmpty(idTransaccion) || string.IsNullOrEmpty(token))
            {
                tmrRespuesta.Enabled = false;
                LimpiarPago();
                MostrarAlerta("Error", "No hay datos de transacción para consultar.", "error");
                return;
            }

            string respuesta = await redebanClient.ConsultarRespuestaAsync(idTransaccion, token);

            if ((respuesta.Contains("Cod:00") && (respuesta.Contains("Msj:0") || respuesta.Contains("Msj:00"))))
            {
                tmrRespuesta.Enabled = false;

                string[] partesRespuesta = respuesta.Split(',');

                Session["idTransaccionRRN"] = partesRespuesta[12];
                Session["numReciboDatafono"] = partesRespuesta[10];

                txbNroAprobacion.Text = Session["numReciboDatafono"].ToString();

                //await ProcesarPagoExitosoAsync();
            }
            else if ((respuesta.Contains("Cod:00") && (respuesta.Contains("Msj:1") || respuesta.Contains("Msj:01"))))
            {
                tmrRespuesta.Enabled = false;
                LimpiarPago();
                MostrarAlerta("Pago rechazado", "La transacción fue rechazada.", "error");
            }
        }

        private async Task<(bool ok, string idSiigoFactura)> ProcesarPagoExitosoAsync(int idAfiliadoPlan, string codSiigoPlan, string nombrePlan, string precioPlan)

        {
            string idAfiliado = Session["IdAfiliado"].ToString();
            string urlRedirect = $"planesAfiliado?id={idAfiliado}";

            try
            {
                string observaciones = $"Pago correspondiente del plan {nombrePlan} por valor de ${precioPlan}.";

                string fechaActual = DateTime.Now.ToString("yyyy-MM-dd");

                clasesglobales cg = new clasesglobales();

                DataTable dtIntegracion = cg.ConsultarIntegracionPorId(idIntegracionSiigo);
                string url = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? dtIntegracion.Rows[0]["url"].ToString() : null;
                string username = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? dtIntegracion.Rows[0]["username"].ToString() : null;
                string accessKey = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? dtIntegracion.Rows[0]["accessKey"].ToString() : null;
                string partnerId = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? dtIntegracion.Rows[0]["partnerId"].ToString() : null;

                int idDocumentType = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? Convert.ToInt32(dtIntegracion.Rows[0]["idDocumentType"].ToString()) : 0;
                int idSellerUser = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? Convert.ToInt32(dtIntegracion.Rows[0]["idSellerUser"].ToString()) : 0;
                int idPayment = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? Convert.ToInt32(dtIntegracion.Rows[0]["idPayment"].ToString()) : 0;
                dtIntegracion.Dispose();


                // 1. Creación de factura en Siigo
                var siigoClient = new SiigoClient(
                    new HttpClient(),
                    url,
                    username,
                    accessKey,
                    partnerId
                );

                DataTable dtAfi = cg.ConsultarAfiliadoPorId(Convert.ToInt32(idAfiliado));

                if (dtAfi.Rows.Count == 0) return (false, null);

                // Obtener datos del afiliado
                string nroDoc = dtAfi.Rows[0]["DocumentoAfiliado"].ToString();
                string strNombre = dtAfi.Rows[0]["NombreAfiliado"].ToString();
                string strApellido = dtAfi.Rows[0]["ApellidoAfiliado"].ToString();
                string strCelular = dtAfi.Rows[0]["CelularAfiliado"].ToString();
                string strEmail = dtAfi.Rows[0]["EmailAfiliado"].ToString();
                int idSede = Convert.ToInt32(dtAfi.Rows[0]["idSede"].ToString());
                dtAfi.Dispose();

                DataTable dtCodSiigoDocumento = cg.ConsultarCodigoSiigoPorDocumento(nroDoc);
                string idTipoDocSiigo = dtCodSiigoDocumento.Rows[0]["CodSiigo"].ToString();
                dtCodSiigoDocumento.Dispose();

                DataTable dtSede = cg.ConsultarSedePorId(idSede);
                string direccion = dtSede.Rows[0]["DireccionSede"].ToString();
                int idCiudad = Convert.ToInt32(dtSede.Rows[0]["idCiudadSede"].ToString());
                dtSede.Dispose();

                DataTable dtCiudad = cg.ConsultarCiudadSedeSiigoPorId(idCiudad);
                string codEstado = dtCiudad.Rows[0]["CodigoEstado"].ToString();
                string codCiudad = dtCiudad.Rows[0]["CodigoCiudad"].ToString();
                dtCiudad.Dispose();

                await siigoClient.ManageCustomerAsync(idTipoDocSiigo, nroDoc, strNombre, strApellido, direccion, codEstado, codCiudad, strCelular, strEmail);

                DataTable dtSedeCostCenter = cg.ConsultarSedePorId(idSede);
                int idCostCenter = dtSedeCostCenter != null && dtSedeCostCenter.Rows.Count > 0 ? Convert.ToInt32(dtSedeCostCenter.Rows[0]["idCostCenterSiigo"].ToString()) : 0;
                dtSedeCostCenter.Dispose();

                string _codSiigoPlan;
                string _nombrePlan;
                int _precioPlan;

                //  PRUEBAS
                if (idIntegracionSiigo == 3)
                {
                    idCostCenter = 621;
                    _codSiigoPlan = "COD2433";
                    _nombrePlan = "Pago de suscripción";
                    _precioPlan = 10000;
                }
                //  PRODUCCIÓN
                else if (idIntegracionSiigo == 6)
                {
                    _codSiigoPlan = codSiigoPlan;
                    _nombrePlan = nombrePlan;
                    _precioPlan = int.Parse(precioPlan);
                }
                else
                {
                    throw new Exception("Id de integración Siigo no válido.");
                }
                string idSiigoFactura = await siigoClient.RegisterInvoiceAsync(
                    nroDoc,
                    _codSiigoPlan,
                    _nombrePlan,
                    _precioPlan,
                    observaciones, 
                    idSellerUser,
                    idDocumentType,
                    fechaActual,
                    idCostCenter,
                    idPayment
                );                
                
                Session["idAfiliadoPlan"] = idAfiliadoPlan;
                string referencia = Session["documentoAfiliado"].ToString() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string codDatafono = Session["codDatafono"].ToString();

                return (true, idSiigoFactura);
            }
            catch (Exception ex)
            {
                //MostrarAlerta("Error", "El pago fue aprobado, pero ocurrió un error en el registro interno. Por favor, comunicarse con el área de sistemas.", "error");
                System.Diagnostics.Debug.WriteLine("Error en ProcesarPagoExitosoAsync: " + ex.ToString());
                return (false, null);
            }
        }

        private void LimpiarPago()
        {
            Session.Remove("idTransaccion");
            Session.Remove("idTransaccionAnterior");
            Session.Remove("token");
            Session.Remove("intentos");
            Session.Remove("idTransaccionRRN");
            Session.Remove("numReciboDatafono");
        }

        private void LimpiarTodo()
        {
            Session.Clear();
        }

        protected void txbEfectivo_TextChanged(object sender, EventArgs e)
        {
            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                int intTotal = Convert.ToInt32(Regex.Replace(txbWompi.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbDatafono.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbEfectivo.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbTransferencia.Text, @"[^\d]", ""));
                txbTotal.Text = intTotal.ToString("C0", new CultureInfo("es-CO"));

                //if (Convert.ToInt32(ViewState["precioTotal"].ToString()) < Convert.ToInt32(Regex.Replace(txbTotal.Text, @"[^\d]", "")) ||
                //    Convert.ToInt32(ViewState["precioMinimo"].ToString()) > Convert.ToInt32(Regex.Replace(txbTotal.Text, @"[^\d]", "")))
                //{
                //    // tipo puede ser: 'success', 'error', 'warning', 'info', 'question'
                //    //MostrarAlerta("Precio total", "Precio total incorrecto. Proporcione los valores adecuados para el total.", "error", "");

                //    string script = @"
                //        Swal.fire({
                //            title: 'Precio total',
                //            text: 'Precio total incorrecto. Proporcione los valores adecuados para el total.',
                //            icon: 'error'
                //        }).then(() => {
                //        });
                //        ";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                //    return;
                //}
            }
        }

        protected void txbTransferencia_TextChanged(object sender, EventArgs e)
        {
            ValidarBanco();
            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                int intTotal = Convert.ToInt32(Regex.Replace(txbWompi.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbDatafono.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbEfectivo.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbTransferencia.Text, @"[^\d]", ""));
                txbTotal.Text = intTotal.ToString("C0", new CultureInfo("es-CO"));

                //if (Convert.ToInt32(ViewState["precioTotal"].ToString()) < Convert.ToInt32(Regex.Replace(txbTotal.Text, @"[^\d]", "")) ||
                //    Convert.ToInt32(ViewState["precioMinimo"].ToString()) > Convert.ToInt32(Regex.Replace(txbTotal.Text, @"[^\d]", "")))
                //{
                //    // tipo puede ser: 'success', 'error', 'warning', 'info', 'question'
                //    //MostrarAlerta("Precio total", "Precio total incorrecto. Proporcione los valores adecuados para el total.", "error", "");
                //    //return;

                //    string script = @"
                //        Swal.fire({
                //            title: 'Precio total',
                //            text: 'Precio total incorrecto. Proporcione los valores adecuados para el total.',
                //            icon: 'error'
                //        }).then(() => {
                //        });
                //        ";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                //    return;
                //}
            }
        }

        protected bool ValidarBanco()
        {
            bool rta = false;
            if (Convert.ToInt32(Regex.Replace(txbTransferencia.Text, @"[^\d]", "")) > 0)
            {
                if (ViewState["Banco"] == null)
                {
                    MostrarAlerta("Banco", "Por favor, seleccione un banco", "error");
                }
                else
                {
                    rta = true;
                }
            }
            return rta;
        }

        /// <summary>
        /// Función para realizar el pago asincrónico con Redeban
        /// </summary>
        /// <param name="precioPlan"></param>
        /// <returns></returns>
        private async Task<bool> RealizarPagoAsync(int precioPlan)
        {
            string urlRedirect = $"planesAfiliado?id=" + Request.QueryString["id"].ToString();

            try
            {
                var redebanClient = CrearRedebanClient();

                // 1. Obtener el token
                string token = await redebanClient.ObtenerTokenAsync();

                if (string.IsNullOrEmpty(token))
                {
                    LimpiarPago();
                    MostrarAlerta("Error", "No se pudo obtener el token de Redeban.", "error");
                    return false;
                }

                // 2. Borrar transacción anterior si existe
                if (Session["idTransaccionAnterior"] != null)
                {
                    string idAnterior = Session["idTransaccionAnterior"].ToString();
                    string resultadoBorrar = await redebanClient.BorrarTransaccionAsync(idAnterior, token);

                    System.Diagnostics.Debug.WriteLine($"BorrarTransaccion Anterior: {resultadoBorrar}");
                }

                // 3. Generar un nuevo IdTransaccion único
                string idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmss");

                // Guardar en sesión para usar luego
                Session["idTransaccion"] = idTransaccion;
                Session["idTransaccionAnterior"] = idTransaccion;
                Session["token"] = token;
                Session["intentos"] = 0;
                string codDatafono = Session["codDatafono"].ToString();

                // 4. Enviar la solicitud de compra
                string resultado = await redebanClient.EnviarDatosCompraAsync(idTransaccion, token, precioPlan, codDatafono);

                // 5. Extraer el código de la respuesta con Regex
                var match = System.Text.RegularExpressions.Regex.Match(resultado, @"Cod:(\d+),Msj:(.*)");

                if (match.Success)
                {
                    string cod = match.Groups[1].Value;
                    string msj = match.Groups[2].Value;

                    if (cod == "00")
                    {
                        return true;
                    }
                    else
                    {
                        LimpiarPago();
                        MostrarAlerta("Error en pago", "No se pudo iniciar la transacción. Detalle: " + resultado, "error");
                        return false;
                    }
                }
                else
                {
                    LimpiarPago();
                    MostrarAlerta("Error en pago", "Respuesta inesperada del servicio Redeban: " + resultado, "error");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LimpiarPago();
                MostrarAlerta("Error inesperado", "Ocurrió un error al procesar el pago.", "error");
                System.Diagnostics.Debug.WriteLine("Error en RealizarCompra: " + ex.ToString());
                return false;
            }
        }

        private RedebanClient CrearRedebanClient()
        {
            return new RedebanClient(
                new HttpClient(),
                "https://sipserviceclientetestv52.azurewebsites.net/sipservice.asmx",
                "http://tempuri.org/",
                "0020304050",
                "sistemas@fitnesspeoplecmd.com",
                "idJ089J3Fm"
            );
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
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

        private void CalculoPrecios()
        {
            double intPrecio = Convert.ToInt32(ViewState["precioTotal"]);
            double intPrecioBase = Convert.ToInt32(ViewState["precioBase"]);
            double intMeses = Convert.ToInt32(ViewState["meses"]);
            double dobDescuento = (1 - (intPrecio / intMeses) / intPrecioBase) * 100;
            double intConDescuento = (intPrecioBase * intMeses) - intPrecio;
            double dobPrecioMesDescuento = intPrecio / intMeses;

            ltPrecioBase.Text = "$" + string.Format("{0:N0}", intPrecioBase);
            ltDescuento.Text = string.Format("{0:N2}", dobDescuento) + "%";
            ltPrecioFinal.Text = "$" + string.Format("{0:N0}", intPrecio);
            ltAhorro.Text = "$" + string.Format("{0:N0}", intConDescuento);
            ltConDescuento.Text = "$" + string.Format("{0:N0}", dobPrecioMesDescuento) + " / mes";

            //Ojo poner tipo plan
            if (ViewState["precioTotal"].ToString() == "99000")
            {
                ltTipoPlan.Text = "Debito automático";
            }
            else
            {
                ltTipoPlan.Text = "Único pago";
            }

            ltObservaciones.Text = "Valor sin descuento: $" + string.Format("{0:N0}", intPrecioBase) + "<br /><br />";
            ltObservaciones.Text += "<b>Meses</b>: " + intMeses.ToString() + ".<br />";
            ltObservaciones.Text += "<b>Descuento</b>: " + string.Format("{0:N2}", dobDescuento) + "%.<br />";
            ltObservaciones.Text += "<b>Valor del mes con descuento</b>: $" + string.Format("{0:N0}", dobPrecioMesDescuento) + "<br />";
            ltObservaciones.Text += "<b>Valor Total</b>: $" + string.Format("{0:N0}", intPrecio) + ".<br />";

            ViewState["observaciones"] = ltObservaciones.Text.ToString().Replace("<b>", "").Replace("</b>", "").Replace("<br />", "\r\n");
            ltValorTotal.Text = "($" + string.Format("{0:N0}", intPrecio) + ")";
        }

        public void MostrarURLWompi()
        {
            //double intPrecio = Convert.ToInt32(ViewState["precioTotal"]);
            double intPrecio = Convert.ToInt32(Regex.Replace(txbWompi.Text, @"[^\d]", ""));
            string strDataWompi = Convert.ToBase64String(Encoding.Unicode.GetBytes(ViewState["DocumentoAfiliado"].ToString() + "_" + intPrecio.ToString()));

            if (ViewState["idPlan"] == null)
            {
                // tipo puede ser: 'success', 'error', 'warning', 'info', 'question'
                MostrarAlerta("Elije un plan", "Debes seleccionar un plan de la lista.", "error");
                txbWompi.Text = "";
                return;
            }
            else
            {
                string payload = $"code={HttpUtility.UrlEncode(ViewState["DocumentoAfiliado"].ToString() + "_" + intPrecio.ToString() + "_" + ViewState["idPlan"].ToString())}";

                TimeSpan ttl = TimeSpan.FromMinutes(40); // Token válido 10 minutos
                string token = UrlEncryptor.Encrypt(payload, ttl);

                if (ViewState["DebitoAutomatico"].ToString() == "1")
                {
                    lbEnlaceWompi.Text = AcortarURL($"https://fitnesspeoplecmdcolombia.com/register?idPlan=" + ViewState["idPlan"].ToString() + @"&idVendedor=" + Session["idUsuario"].ToString() + @"");
                    hdEnlaceWompi.Value = AcortarURL($"https://fitnesspeoplecmdcolombia.com/register?idPlan=" + ViewState["idPlan"].ToString() + @"&idVendedor=" + Session["idUsuario"].ToString() + @"");
                    btnPortapaleles.Visible = true;
                }
                else
                {
                    //lbEnlaceWompi.Text = "https://fitnesspeoplecolombia.com/wompiplan?code=" + strDataWompi;
                    //lbEnlaceWompi.Text += AcortarURL("https://fitnesspeoplecmdcolombia.com/wompiplan?code=" + strDataWompi);
                    lbEnlaceWompi.Text = AcortarURL($"https://fitnesspeoplecmdcolombia.com/wompiplan?data={HttpUtility.UrlEncode(token)}");
                    //hdEnlaceWompi.Value = AcortarURL("https://fitnesspeoplecmdcolombia.com/wompiplan?code=" + strDataWompi);
                    hdEnlaceWompi.Value = AcortarURL($"https://fitnesspeoplecmdcolombia.com/wompiplan?data={HttpUtility.UrlEncode(token)}");
                    btnPortapaleles.Visible = true;
                }
            }
        }


        public static string AcortarURL(string url)
        {
            try
            {
                if (url.Length <= 30)
                {
                    return url;
                }

                string longUrl = url;
                string apiUrl = "https://is.gd/create.php?format=simple&url=" + HttpUtility.UrlEncode(longUrl);

                using (WebClient client = new WebClient())
                {
                    string shortUrl = client.DownloadString(apiUrl);
                    return shortUrl;
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        private void ActivarCortesia(string strCortesia)
        {
            switch (strCortesia)
            {
                case "0":
                    btn7dias.Enabled = true;
                    btn15dias.Enabled = true;
                    btn30dias.Enabled = false;
                    btn60dias.Enabled = false;
                    btn90dias.Enabled = false;
                    break;
                case "1":
                    btn7dias.Enabled = true;
                    btn15dias.Enabled = true;
                    btn30dias.Enabled = true;
                    btn60dias.Enabled = false;
                    btn90dias.Enabled = false;
                    break;
                case "2":
                    btn7dias.Enabled = true;
                    btn15dias.Enabled = true;
                    btn30dias.Enabled = true;
                    btn60dias.Enabled = true;
                    btn90dias.Enabled = false;
                    break;
                case "3":
                    btn7dias.Enabled = true;
                    btn15dias.Enabled = true;
                    btn30dias.Enabled = true;
                    btn60dias.Enabled = true;
                    btn90dias.Enabled = true;
                    break;
                default:
                    break;
            }
        }
        protected void btn7dias_Click(object sender, EventArgs e)
        {
            btn7dias.CssClass += " active";
            btn15dias.CssClass = btn15dias.CssClass.Replace("active", "");
            btn30dias.CssClass = btn30dias.CssClass.Replace("active", "");
            btn60dias.CssClass = btn60dias.CssClass.Replace("active", "");
            btn90dias.CssClass = btn90dias.CssClass.Replace("active", "");
            ltCortesias.Text = "<b>Cortesía: </b>7 días adicionales al plan.<br />";
            ViewState["DiasCortesia"] = 7;
        }

        protected void btn15dias_Click(object sender, EventArgs e)
        {
            btn15dias.CssClass += " active";
            btn7dias.CssClass = btn7dias.CssClass.Replace("active", "");
            btn30dias.CssClass = btn30dias.CssClass.Replace("active", "");
            btn60dias.CssClass = btn60dias.CssClass.Replace("active", "");
            btn90dias.CssClass = btn90dias.CssClass.Replace("active", "");
            ltCortesias.Text = "<b>Cortesía: </b>15 días adicionales al plan.<br />";
            ViewState["DiasCortesia"] = 15;
        }

        protected void btn30dias_Click(object sender, EventArgs e)
        {
            btn30dias.CssClass += " active";
            btn7dias.CssClass = btn7dias.CssClass.Replace("active", "");
            btn15dias.CssClass = btn15dias.CssClass.Replace("active", "");
            btn60dias.CssClass = btn60dias.CssClass.Replace("active", "");
            btn90dias.CssClass = btn90dias.CssClass.Replace("active", "");
            ltCortesias.Text = "<b>Cortesía: </b>30 días adicionales al plan.<br />";
            ViewState["DiasCortesia"] = 30;
        }

        protected void btn60dias_Click(object sender, EventArgs e)
        {
            btn60dias.CssClass += " active";
            btn7dias.CssClass = btn7dias.CssClass.Replace("active", "");
            btn15dias.CssClass = btn15dias.CssClass.Replace("active", "");
            btn30dias.CssClass = btn30dias.CssClass.Replace("active", "");
            btn90dias.CssClass = btn90dias.CssClass.Replace("active", "");
            ltCortesias.Text = "<b>Cortesía: </b>60 días adicionales al plan.<br />";
            ViewState["DiasCortesia"] = 60;
        }

        protected void btn90dias_Click(object sender, EventArgs e)
        {
            btn90dias.CssClass += " active";
            btn7dias.CssClass = btn7dias.CssClass.Replace("active", "");
            btn15dias.CssClass = btn15dias.CssClass.Replace("active", "");
            btn30dias.CssClass = btn30dias.CssClass.Replace("active", "");
            btn60dias.CssClass = btn60dias.CssClass.Replace("active", "");
            ltCortesias.Text = "<b>Cortesía: </b>90 días adicionales al plan.<br />";
            ViewState["DiasCortesia"] = 90;
        }
        private bool ValidarSesion()
        {
            return Session["IdAfiliado"] != null &&
                   Session["idcrm"] != null &&
                   Session["idusuario"] != null;
        }


        protected async void lbAgregarPlan_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                Response.Redirect("Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (ViewState["nombrePlan"] == null)
            {
                MostrarAlerta("Validación", "Debes seleccionar un tipo de plan para continuar.", "warning");
                return;
            }

            if (string.IsNullOrWhiteSpace(txbFechaInicio.Text))
            {
                MostrarAlerta("Validación", "Por favor, selecciona la fecha de inicio del plan.", "error");
                return;
            }

            if (string.IsNullOrWhiteSpace(txbTotal.Text))
            {
                MostrarAlerta("Error", "El valor total no puede estar vacío.", "error");
                return;
            }

            int totalIngresado = Convert.ToInt32(Regex.Replace(txbTotal.Text, @"[^\d]", ""));
            int precioMax = Convert.ToInt32(ViewState["precioTotal"]);
            int precioMin = Convert.ToInt32(ViewState["precioMinimo"]);

            if (totalIngresado > precioMax || totalIngresado < precioMin)
            {
                MostrarAlerta("Error", "Precio incorrecto. Intenta nuevamente.", "error");
                return;
            }

            if (Convert.ToInt32(Regex.Replace(txbTransferencia.Text, @"[^\d]", "")) > 0 && !ValidarBanco())
            {
                return;
            }

            clasesglobales cg = new clasesglobales();

            int idAfiliado = Convert.ToInt32(Session["IdAfiliado"]);
            int idUsuario = Convert.ToInt32(Session["idusuario"]);
            int idcrm = Convert.ToInt32(Session["idcrm"]);
            string valorPagadoTexto = txbTotal.Text.Trim();
            string valorLimpio = Regex.Replace(valorPagadoTexto, @"[^\d]", "");
            int valorPagado;
            int.TryParse(valorLimpio, out valorPagado);



            //DataTable dtEstado = cg.ConsultarAfiliadoEstadoActivo(idAfiliado);
            //if (dtEstado.Rows.Count > 0 && dtEstado.Rows[0]["EstadoPlan"].ToString() == "Activo")
            //{
            //    string fechaFinal = Convert.ToDateTime(dtEstado.Rows[0]["FechaFinalPlan"]).ToString("dd MMM yyyy");
            //    MostrarAlerta("Plan activo", $"El afiliado ya tiene un plan activo hasta el {fechaFinal}.", "warning");
            //    return;
            //}

            string estadoPago = cbPagaCounter.Checked ? "Pendiente" : "Activo";

            DateTime fechaInicio = Convert.ToDateTime(txbFechaInicio.Text);
            DateTime fechaFinalPlan = fechaInicio
                .AddMonths(Convert.ToInt32(ViewState["meses"]))
                .AddDays(Convert.ToInt32(ViewState["DiasCortesia"]));

            string rta = cg.InsertarAfiliadoPlan(idAfiliado, Convert.ToInt32(ViewState["idPlan"]), fechaInicio.ToString("yyyy-MM-dd"), fechaFinalPlan.ToString("yyyy-MM-dd"), Convert.ToInt32(ViewState["meses"]),
                Convert.ToInt32(ViewState["precioTotal"]), ViewState["observaciones"].ToString(), estadoPago);

            if (!rta.StartsWith("OK"))
            {
                MostrarAlerta("Error", $"No se pudo registrar. Detalle: {rta}", "error");
                return;
            }

            if (estadoPago == "Pendiente")
            {
                MostrarMensajeCounter(idcrm, idAfiliado);
                return;
            }

            int idAfiliadoPlan = int.Parse(rta.Split('|')[1]);

            int idCanalVenta = 0;
            DataTable dtCanal = cg.ConsultarUsuarioSedePerfilPorId(idUsuario);
            if (dtCanal.Rows.Count > 0)
                idCanalVenta = Convert.ToInt32(dtCanal.Rows[0]["idCanalVenta"]);

            DataTable mediosPago = cg.ConsultarMediosDePago();
            DataTable dtAfiliado = cg.ConsultarAfiliadoPorId(idAfiliado);
            string docAfiliado = dtAfiliado.Rows[0]["DocumentoAfiliado"].ToString();

            try
            {

                var resultado = await ProcesarPagoExitosoAsync(idAfiliadoPlan, ViewState["codSiigoPlan"].ToString(), ViewState["nombrePlan"].ToString(), valorPagado.ToString());

                if (!resultado.ok)
                {
                    MostrarAlerta("Error", "No se pudo obtener la información del afiliado.", "error");
                    return;
                }
                else
                {
                    string idSiigoFactura = resultado.idSiigoFactura;
                    DataTable dtActivo = cg.ConsultarAfiliadoEstadoActivo(idAfiliado);
                    string rtaCRM = cg.ActualizarEstadoCRMPagoPlan(idcrm, dtActivo.Rows[0]["NombrePlan"].ToString(), valorPagado, idUsuario, 3);
                    cg.InsertarLog(idUsuario.ToString(), "afiliadosplanes", "Agrega", $"El usuario agregó un nuevo plan al afiliado con documento: {docAfiliado}.", "", "");
                    RegistrarPagos(cg, mediosPago, idAfiliadoPlan, idUsuario, idCanalVenta, idcrm, idSiigoFactura);
                    MostrarMensajeExito(docAfiliado, idcrm);
                }

            }
            catch (Exception ex)
            {
                new clasesglobales().InsertarLog(Session["idusuario"]?.ToString() ?? "0", "ASYNC", "ERROR", ex.ToString(), "", "");

                MostrarAlerta("Error", "No se pudo actualizar el CRM.", "error");
                return;
            }
        }

        public class ResultadoPago
        {
            public bool Exitoso { get; set; }
            public string Mensaje { get; set; }
        }



        private void RegistrarPagos(clasesglobales cg, DataTable medios, int idAfiliadoPlan, int idUsuario, int idCanalVenta, int idcrm, string idSiigoFactura)
        {
            ProcesarMedio(cg, medios.Rows[3], txbWompi.Text, idAfiliadoPlan, idUsuario, "Wompi", idCanalVenta, idcrm, idSiigoFactura);
            ProcesarMedio(cg, medios.Rows[2], txbDatafono.Text, idAfiliadoPlan, idUsuario, "", idCanalVenta, idcrm, txbNroAprobacion.Text, idSiigoFactura);
            ProcesarMedio(cg, medios.Rows[0], txbEfectivo.Text, idAfiliadoPlan, idUsuario, "", idCanalVenta, idcrm, idSiigoFactura);
            ProcesarMedio(cg, medios.Rows[1], txbTransferencia.Text, idAfiliadoPlan, idUsuario, ViewState["Banco"]?.ToString() ?? "Ninguno", idCanalVenta, idcrm, idSiigoFactura);
        }

        private void ProcesarMedio(clasesglobales cg, DataRow medio, string valorText, int idAfiliadoPlan, int idUsuario, string banco, int idCanalVenta, int idcrm, string idSiigoFactura, string referencia = "")
        {
            int valor = Convert.ToInt32(Regex.Replace(valorText, @"[^\d]", ""));
            if (valor <= 0) return;

            cg.InsertarPagoPlanAfiliado(idAfiliadoPlan, valor, Convert.ToInt32(medio["idMedioPago"]), referencia, banco, idUsuario, "Aprobado", idSiigoFactura, idCanalVenta, idcrm);
        }


        private void MostrarMensajeExito(string doc, int idcrm)
        {
            string script = $@"
                Swal.fire({{
                    title: '¡Venta registrada con éxito!',
                    text: 'Hemos enviado un correo al comprador.',
                    icon: 'success',
                    timer: 5000,
                    showConfirmButton: false
                }}).then(() => {{
                    window.location.href = 'detalleafiliado?search={doc}&idcrm={idcrm}';
                }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "Exito", script, true);
        }

        private void MostrarMensajeCounter(int idcrm, int idAfiliado)
        {
            string script = $@"
                Swal.fire({{
                    title: 'Paga en counter',
                    text: 'El afiliado realiza el pago en counter.',
                    icon: 'info'
                }}).then(() => {{
                    window.location.href = 'detalleafiliado?idcrm={idcrm}';
                }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "Counter", script, true);
        }



        protected void rpPlanes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SeleccionarPlan")
            {
                txbWompi.Enabled = true;
                txbDatafono.Enabled = true;
                txbEfectivo.Enabled = true;
                txbTransferencia.Enabled = true;
                cbPagaCounter.Enabled = true;

                int idPlan = Convert.ToInt32(e.CommandArgument.ToString());
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarPlanPorId(idPlan);

                ViewState["idPlan"] = dt.Rows[0]["idPlan"].ToString();
                ViewState["nombrePlan"] = dt.Rows[0]["NombrePlan"].ToString();
                ViewState["codSiigoPlan"] = dt.Rows[0]["CodSiigoPlan"].ToString();
                ViewState["precioTotal"] = Convert.ToInt32(dt.Rows[0]["PrecioTotal"].ToString());
                ViewState["precioMinimo"] = Convert.ToInt32(dt.Rows[0]["PrecioMinimo"].ToString());
                ViewState["precioBase"] = Convert.ToInt32(dt.Rows[0]["PrecioBase"].ToString());
                ViewState["meses"] = Convert.ToDouble(dt.Rows[0]["Meses"].ToString());
                ViewState["mesesCortesia"] = Convert.ToDouble(dt.Rows[0]["MesesCortesia"].ToString());
                ViewState["DebitoAutomatico"] = Convert.ToDouble(dt.Rows[0]["DebitoAutomatico"].ToString());

                ltPrecioBase.Text = "$" + String.Format("{0:N0}", ViewState["precioBase"]);
                ltPrecioFinal.Text = "$" + String.Format("{0:N0}", ViewState["precioTotal"]);

                CalculoPrecios();
                ActivarCortesia(ViewState["mesesCortesia"].ToString());

                ltDescripcion.Text = "<b>Características</b>: " + dt.Rows[0]["DescripcionPlan"].ToString() + "<br />";

                ltNombrePlan.Text = "<b>Plan " + ViewState["nombrePlan"].ToString() + "</b>";
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se enlaza cada ítem en el Repeater de planes.
        /// Modifica la clase CSS del botón de selección del plan según el estado definido en el ViewState.
        /// </summary>
        /// <param name="sender">El control Repeater que genera el evento.</param>
        /// <param name="e">Datos del ítem enlazado, contiene el tipo de ítem y el objeto de datos.</param>
        protected void rpPlanes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    LinkButton lnkPlan = (LinkButton)e.Item.FindControl("btnSeleccionarPlan");
                    lnkPlan.Attributes.Add("class", "btn btn-outline btn-" + ((DataRowView)e.Item.DataItem).Row["NombreColorPlan"].ToString() + " btn-block btn-sm");
                }
            }
        }

        private void MostrarAlertaProcesando()
        {
            string script = @"
            let contador = 5;
            Swal.fire({
                title: 'Cargando',
                html: `Este proceso iniciará en <b>${contador}</b> segundos...`,
                icon: 'info',
                background: '#3C3C3C', 
                allowOutsideClick: false,
                showConfirmButton: false, 
                customClass: {
                    popup: 'alert',
                    confirmButton: 'btn-confirm-alert'
                },
                didOpen: () => {
                    Swal.showLoading();
                    const interval = setInterval(() => {
                        contador--;
                        Swal.getHtmlContainer().querySelector('b').textContent = contador;
                        if (contador <= 0) {
                            clearInterval(interval);
                            Swal.fire({
                                title: 'Continúa en el datáfono',
                                html: 'Por favor, presiona la <b style=""color: #157347;"">TECLA VERDE</b> del datáfono para continuar.',
                                background: '#3C3C3C',
                                icon: 'info',
                                allowOutsideClick: false,
                                showConfirmButton: true,
                                customClass: {
                                    popup: 'alert',
                                    confirmButton: 'btn-confirm-alert'
                                }
                            });
                        }
                    }, 1000);
                }
            });";

            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlertProcesando", script, true);
        }

        protected void btnBancolombia_Click(object sender, EventArgs e)
        {
            btnBancolombia.CssClass += " active";
            btnDavivienda.CssClass = btnDavivienda.CssClass.Replace("active", "");
            btnBBVA.CssClass = btnBBVA.CssClass.Replace("active", "");
            btnBogota.CssClass = btnBogota.CssClass.Replace("active", "");
            ViewState["Banco"] = "Bancolombia";
        }

        protected void btnDavivienda_Click(object sender, EventArgs e)
        {
            btnDavivienda.CssClass += " active";
            btnBancolombia.CssClass = btnBancolombia.CssClass.Replace("active", "");
            btnBBVA.CssClass = btnBBVA.CssClass.Replace("active", "");
            btnBogota.CssClass = btnBogota.CssClass.Replace("active", "");
            ViewState["Banco"] = "Davivienda";
        }

        protected void btnBBVA_Click(object sender, EventArgs e)
        {
            btnBBVA.CssClass += " active";
            btnDavivienda.CssClass = btnDavivienda.CssClass.Replace("active", "");
            btnBancolombia.CssClass = btnBancolombia.CssClass.Replace("active", "");
            btnBogota.CssClass = btnBogota.CssClass.Replace("active", "");
            ViewState["Banco"] = "BBVA";
        }

        protected void btnBogota_Click(object sender, EventArgs e)
        {
            btnBogota.CssClass += " active";
            btnDavivienda.CssClass = btnDavivienda.CssClass.Replace("active", "");
            btnBBVA.CssClass = btnBBVA.CssClass.Replace("active", "");
            btnBancolombia.CssClass = btnBancolombia.CssClass.Replace("active", "");
            ViewState["Banco"] = "Bogota";
        }

        protected void cbPagaCounter_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPagaCounter.Checked)
            {
                txbTotal.Text = string.Format("{0:C0}", ViewState["precioTotal"].ToString());
            }
            else
            {
                if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
                {
                    int intTotal = Convert.ToInt32(Regex.Replace(txbWompi.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbDatafono.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbEfectivo.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbTransferencia.Text, @"[^\d]", ""));
                    txbTotal.Text = intTotal.ToString("C0", new CultureInfo("es-CO"));
                    txbTotal.Text = "$ " + string.Format("{0:N0}", intTotal);
                }
            }
        }
    }
}