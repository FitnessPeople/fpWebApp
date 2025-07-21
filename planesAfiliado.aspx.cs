using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class planesAfiliado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

                        string strData = ListarDetalle();
                        ltDetalleWompi.Text = strData;
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

        private void ListaPlanes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanesVigencias();
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
                    ltEmail.Text = dt.Rows[0]["EmailAfiliado"].ToString();
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
                DataTable dt = cg.CargarPlanesAfiliado(Session["IdAfiliado"].ToString(), "Activo");
                rpPlanesAfiliado.DataSource = dt;
                rpPlanesAfiliado.DataBind();
                dt.Dispose();
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

        private string ListarDetalle()
        {
            string parametro = string.Empty;
            //string tester = string.Empty;
            string mensaje = string.Empty;
            int idempresa = 4;//Wompi
            clasesglobales cg = new clasesglobales();
            DataTable dti = cg.ConsultarUrl(idempresa);

            string strFechaHoy = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            parametro = "?from_date=2025-01-01&until_date=2025-03-11&page=1&page_size=50&order_by=created_at&order=DESC";
            //parametro = "?from_date=" + strFechaHoy + "&until_date=" + strFechaHoy + "&page=1&page_size=10&order_by=created_at&order=DESC";

            string url = dti.Rows[0]["urlTest"].ToString() + parametro;
            string[] respuesta = cg.EnviarPeticionGet(url, idempresa.ToString(), out mensaje);
            JToken token = JToken.Parse(respuesta[0]);
            string prettyJson = token.ToString(Formatting.Indented);

            if (mensaje == "Ok") //Verifica respuesta ok
            {
                JObject jsonData = JObject.Parse(prettyJson);

                List<Datum> listaDatos = new List<Datum>();

                foreach (var item in jsonData["data"])
                {
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
                        device_id = item["customer_data"]?["device_id"]?.ToString(),
                        full_name = item["customer_data"]?["full_name"]?.ToString(),
                        phone_number = item["customer_data"]?["phone_number"]?.ToString()
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
            Repeater rptPlanes = sender as Repeater; // Get the Repeater control object.

            // If the Repeater contains no data.
            if (rpPlanesAfiliado.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    // Show the Error Label (if no data is present).
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
            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                int intTotal = Convert.ToInt32(Regex.Replace(txbWompi.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbDatafono.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbEfectivo.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbTransferencia.Text, @"[^\d]", ""));
                txbTotal.Text = intTotal.ToString("C0", new CultureInfo("es-CO"));
            }
        }

        protected void txbDatafono_TextChanged(object sender, EventArgs e)
        {
            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                int intTotal = Convert.ToInt32(Regex.Replace(txbWompi.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbDatafono.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbEfectivo.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbTransferencia.Text, @"[^\d]", ""));
                txbTotal.Text = intTotal.ToString("C0", new CultureInfo("es-CO"));
            }
        }

        protected void txbEfectivo_TextChanged(object sender, EventArgs e)
        {
            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                int intTotal = Convert.ToInt32(Regex.Replace(txbWompi.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbDatafono.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbEfectivo.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbTransferencia.Text, @"[^\d]", ""));
                txbTotal.Text = intTotal.ToString("C0", new CultureInfo("es-CO"));
            }
        }

        protected void txbTransferencia_TextChanged(object sender, EventArgs e)
        {
            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                int intTotal = Convert.ToInt32(Regex.Replace(txbWompi.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbDatafono.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbEfectivo.Text, @"[^\d]", "")) + Convert.ToInt32(Regex.Replace(txbTransferencia.Text, @"[^\d]", ""));
                txbTotal.Text = intTotal.ToString("C0", new CultureInfo("es-CO"));
            }
        }

        private void btn_Click(object sender, CommandEventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanPorId(Convert.ToInt32(e.CommandArgument));

            ViewState["idPlan"] = dt.Rows[0]["idPlan"].ToString();
            ViewState["nombrePlan"] = dt.Rows[0]["NombrePlan"].ToString();
            ViewState["precioTotal"] = Convert.ToInt32(dt.Rows[0]["PrecioTotal"].ToString());
            ViewState["precioBase"] = Convert.ToInt32(dt.Rows[0]["PrecioBase"].ToString());
            ViewState["meses"] = Convert.ToDouble(dt.Rows[0]["Meses"].ToString());
            ViewState["mesesCortesia"] = Convert.ToDouble(dt.Rows[0]["MesesCortesia"].ToString());

            ltPrecioBase.Text = "$" + String.Format("{0:N0}", ViewState["precioBase"]);
            ltPrecioFinal.Text = "$" + String.Format("{0:N0}", ViewState["precioTotal"]);

            CalculoPrecios();
            ActivarCortesia(ViewState["mesesCortesia"].ToString());

            ltDescripcion.Text = "<b>Características</b>: " + dt.Rows[0]["DescripcionPlan"].ToString() + "<br />";
            ltNombrePlan.Text = "<b>Plan " + ViewState["nombrePlan"].ToString() + "</b>";
        }

        /// <summary>
        /// Calcula y muestra los precios finales del plan, incluyendo descuento, ahorro, valor por mes,
        /// tipo de pago y observaciones. También genera y muestra el enlace de pago Wompi.
        /// </summary>
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

            string strDataWompi = Convert.ToBase64String(Encoding.Unicode.GetBytes(ViewState["DocumentoAfiliado"].ToString() + "_" + intPrecio.ToString()));
            //lbEnlaceWompi.Text = "https://fitnesspeoplecolombia.com/wompiplan?code=" + strDataWompi;
            lbEnlaceWompi.Text = "<b>Enlace de pago Wompi:</b> <br />";
            lbEnlaceWompi.Text += AcortarURL("https://fitnesspeoplecolombia.com/wompiplan?code=" + strDataWompi);
            hdEnlaceWompi.Value = AcortarURL("https://fitnesspeoplecolombia.com/wompiplan?code=" + strDataWompi);
            btnPortapaleles.Visible = true;
        }

        /// <summary>
        /// Acorta una URL larga utilizando el servicio de TinyURL.
        /// </summary>
        /// <param name="url">La URL original que se desea acortar.</param>
        /// <returns>
        /// Una versión acortada de la URL si el proceso es exitoso. 
        /// Si la URL ya es corta (menos de 30 caracteres) o ocurre un error en la solicitud, se retorna la URL original.
        /// </returns>
        /// <remarks>
        /// Usa una solicitud HTTP al endpoint público de TinyURL para obtener la versión reducida de la URL.
        /// Si el servicio no está disponible o se produce una excepción, se maneja silenciosamente y se retorna la URL original.
        /// </remarks>
        public static string AcortarURL(string url)
        {
            try
            {
                if (url.Length <= 30)
                {
                    return url;
                }

                var request = WebRequest.Create("http://tinyurl.com/api-create.php?url=" + url);
                var res = request.GetResponse();
                string text;
                using (var reader = new StreamReader(res.GetResponseStream()))
                {
                    text = reader.ReadToEnd();
                }
                return text;
            }
            catch (Exception)
            {
                return url;
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

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Agregar Plan".
        /// Valida si el afiliado está activo, si tiene un plan activo, y si el valor ingresado es correcto.
        /// Si todo está correcto, registra el plan y el pago correspondiente en la base de datos.
        /// </summary>
        /// <param name="sender">Objeto que desencadenó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        /// <remarks>
        /// Este método valida múltiples condiciones antes de registrar un nuevo plan para un afiliado:
        /// - El afiliado debe estar activo.
        /// - No debe tener otro plan activo.
        /// - El precio ingresado debe coincidir con el precio calculado.
        /// - Registra el plan con fecha de inicio y calcula fecha final con base en meses y días de cortesía.
        /// - Registra también el pago asociado al plan, incluyendo el tipo de pago (Wompi, datáfono, transferencia o efectivo),
        ///   banco (si aplica), y número de referencia (si aplica).
        /// 
        /// En caso de error, se muestra un mensaje al usuario.
        /// </remarks>
        /// <summary>
        /// Agrega el plan a un usuario. Inserta en la tabla AfiliadosPlanes y en PagosPlanAfiliado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbAgregarPlan_Click(object sender, EventArgs e)
        {
            if (ViewState["nombrePlan"] != null)
            {
                if (txbFechaInicio.Text != "")
                {
                    // Consultar si este usuario tiene un plan activo y cual es su fecha de inicio y fecha final.
                    clasesglobales cg = new clasesglobales();
                    DataTable dt = cg.ConsultarAfiliadoEstadoActivo(Convert.ToInt32(Session["IdAfiliado"].ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        string script = @"
                            Swal.fire({
                                title: 'Mensaje',
                                text: 'Este afiliado ya tiene un plan activo, hasta el " + string.Format("{0:dd MMM yyyy}", dt.Rows[0]["FechaFinalPlan"]) + @".',
                                icon: 'error'
                            }).then(() => {
                            });
                            ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                    }
                    else
                    {
                        if (txbTotal.Text.ToString() == "")
                        {
                            string script = @"
                                Swal.fire({
                                    title: 'Verificación',
                                    text: 'Falta el valor a pagar',
                                    icon: 'error'
                                }).then(() => {
                                });
                                ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                        }
                        else
                        {
                            if (ViewState["precioTotal"].ToString() != Convert.ToInt32(Regex.Replace(txbTotal.Text, @"[^\d]", "")).ToString())
                            {
                                string script = @"
                                    Swal.fire({
                                        title: 'Error',
                                        text: 'Precios no coinciden',
                                        icon: 'error'
                                    }).then(() => {
                                    });
                                    ";
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                            }
                            else
                            {
                                try
                                {
                                    string DocAfiliado = string.Empty;
                                    string idcrm = Session["idcrm"].ToString();
                                    DateTime fechainicio = Convert.ToDateTime(txbFechaInicio.Text.ToString());
                                    DateTime fechafinal = fechainicio.AddMonths(Convert.ToInt16(ViewState["meses"].ToString()));
                                    fechafinal = fechafinal.AddDays(Convert.ToInt16(ViewState["DiasCortesia"].ToString()));

                                    string rta = cg.InsertarAfiliadoPlan(Convert.ToInt32(Session["IdAfiliado"].ToString()), Convert.ToInt32(ViewState["idPlan"].ToString()),
                                        txbFechaInicio.Text.ToString(), String.Format("{0:yyyy-MM-dd}", fechafinal), Convert.ToInt32(ViewState["meses"].ToString()),
                                        Convert.ToInt32(ViewState["precioTotal"].ToString()), ViewState["observaciones"].ToString(), "Activo");

                                    if (rta == "OK")
                                    {
                                        DataTable dt1 = cg.ConsultarUltimoAfilEnAfiliadosPlan();
                                        int idAfiliado = 0;
                                        if (dt1.Rows.Count > 0) idAfiliado = Convert.ToInt32(dt1.Rows[0]["idAfiliadoPlan"].ToString());

                                        //Consultamos los medios de pago
                                        DataTable dt2 = cg.ConsultarMediosDePago();

                                        string strTipoPago = string.Empty;
                                        string strReferencia = string.Empty;
                                        string strBanco = string.Empty;

                                        if (txbWompi.Text.ToString() != "$0")
                                        {
                                            strTipoPago = dt2.Rows[4]["idMedioPago"].ToString(); //Pago en linea - Wompi
                                        }
                                        if (txbDatafono.Text.ToString() != "$0")
                                        {
                                            strTipoPago = dt2.Rows[2]["idMedioPago"].ToString(); //Pago con Datafono Tarjeta Debito
                                            strReferencia = txbNroAprobacion.Text.ToString();
                                        }
                                        if (txbTransferencia.Text.ToString() != "$0")
                                        {
                                            strTipoPago = dt2.Rows[1]["idMedioPago"].ToString();  //Pago con Transferencia
                                        }
                                        if (txbEfectivo.Text.ToString() != "$0")
                                        {
                                            strTipoPago = dt2.Rows[0]["idMedioPago"].ToString();  //Pago con Efectivo
                                        }

                                        if (rblBancos.SelectedItem == null)
                                        {
                                            strBanco = "No aplica";
                                        }
                                        else
                                        {
                                            strBanco = rblBancos.SelectedItem.Value.ToString();
                                        }



                                        string respuesta = cg.InsertarPagoPlanAfiliado(idAfiliado, Convert.ToInt32(ViewState["precioTotal"].ToString()),
                                            Convert.ToInt32(strTipoPago), strReferencia, strBanco, Convert.ToInt32(Session["idUsuario"].ToString()), "Aprobado", "", 0, Convert.ToInt32(Session["idcrm"]));

                                        DataTable dt3 = cg.ConsultarAfiliadoEstadoActivo(Convert.ToInt32(dt1.Rows[0]["idAfiliado"].ToString()));
                                        string respuesta1 = cg.ActualizarEstadoCRMPagoPlan(Convert.ToInt32(Session["idcrm"].ToString()), dt3.Rows[0]["NombrePlan"].ToString(), Convert.ToInt32(dt3.Rows[0]["PrecioTotal"].ToString()), Convert.ToInt32(Session["idUsuario"].ToString()), 3);

                                        if (respuesta == "OK" && respuesta1 == "OK")
                                        {
                                            DataTable dtAfiliado = cg.ConsultarAfiliadoPorId(int.Parse(Session["IdAfiliado"].ToString()));
                                            DocAfiliado = dtAfiliado.Rows[0]["DocumentoAfiliado"].ToString();
                                            cg.InsertarLog(Session["idusuario"].ToString(), "afiliadosplanes", "Agrega", "El usuario agregó un nuevo plan al afiliado con documento: " + dt3.Rows[0]["PrecioTotal"].ToString() + ".", "", "");

                                            string script = @"
                                            Swal.fire({
                                                title: '¡Venta registrada con éxito!',
                                                text: 'Hemos enviado un correo al comprador para que complete sus datos y responda el formulario de salud (Par-Q).',
                                                icon: 'success',
                                                timer: 5000, // 5 segundos
                                                showConfirmButton: false,
                                                timerProgressBar: true
                                            }).then(() => {
                                                window.location.href = 'detalleafiliado?search=" + DocAfiliado + @"&idcrm=" + idcrm + @"';
                                            });
                                            ";
                                            ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);

                                            /////////////////////////////// ENVÍO DE CORREO ///////////////////////////////////////////////////////////////////////////////////////////

                                            string strString = Convert.ToBase64String(Encoding.Unicode.GetBytes(dt3.Rows[0]["DocumentoAfiliado"].ToString() + "_" + dt3.Rows[0]["precio"].ToString()));

                                            string strMensaje = "Se ha creado un Plan para ud. en Fitness People \r\n\r\n";
                                            strMensaje += "Descripción del plan.\r\n\r\n";
                                            strMensaje += "Por favor, agradecemos realice el pago a través del siguiente enlace: \r\n";
                                            strMensaje += "https://fitnesspeoplecolombia.com/wompiplan?code=" + strString;

                                            cg.EnviarCorreo("afiliaciones@fitnesspeoplecolombia.com", dt3.Rows[0]["EmailAfiliado"].ToString(), "Plan Fitness People", strMensaje);

                                            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                                        }
                                        else
                                        {
                                            string script = @"
                                            Swal.fire({
                                                title: 'Error',
                                                text: 'No se pudo registrar. Detalle: " + respuesta.Replace("'", "\\'") + @"',
                                                icon: 'error'
                                            });
                                        ";
                                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                                        }

                                    }
                                    else
                                    {
                                        string script = @"
                                            Swal.fire({
                                                title: 'Error',
                                                text: 'No se pudo registrar. Detalle: " + rta.Replace("'", "\\'") + @"',
                                                icon: 'error'
                                            });
                                        ";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    string script = @"
                                        Swal.fire({
                                            title: 'Error',
                                            text: 'Ha ocurrido un error inesperado. " + ex.Message.ToString() + @"',
                                            icon: 'error'
                                        }).then(() => {
                                        });
                                        ";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                                }
                            }
                        }
                    }
                    dt.Dispose();
                }
                else
                {
                    string script = @"
                        Swal.fire({
                            title: 'Validación',
                            text: 'Elija la fecha de inicio del plan.',
                            icon: 'error'
                        }).then(() => {
                        });
                        ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                }
            }
            else
            {
                string script = @"
                    Swal.fire({
                        title: 'Validación',
                        text: 'Elija el tipo de plan.',
                        icon: 'error'
                    }).then(() => {
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
            }
            //}
        }

        /// <summary>
        /// Evento que se ejecuta al interactuar con un ítem del Repeater de planes.
        /// Procesa el comando "SeleccionarPlan" para cargar los datos del plan seleccionado
        /// y actualiza los controles y el ViewState con la información del plan.
        /// </summary>
        /// <param name="source">El control que generó el evento (normalmente el Repeater).</param>
        /// <param name="e">Argumentos del comando, que incluyen el nombre y argumento del comando.</param>
        /// <remarks>
        /// Cuando se selecciona un plan, este método realiza lo siguiente:
        /// - Obtiene el ID del plan desde el argumento del comando.
        /// - Consulta la base de datos para obtener toda la información del plan.
        /// - Guarda los datos clave del plan en ViewState para uso posterior.
        /// - Actualiza etiquetas en la interfaz con precio base, precio final, descripción y nombre del plan.
        /// - Llama al método <see cref="CalculoPrecios"/> para actualizar precios calculados.
        /// - Activa o desactiva la cortesía según el valor de meses de cortesía.
        /// 
        /// Es importante que el método <see cref="CalculoPrecios"/> y <see cref="ActivarCortesia(string)"/>
        /// estén implementados para manejar correctamente la actualización visual y lógica.
        /// </remarks>
        protected void rpPlanes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SeleccionarPlan")
            {
                int idPlan = Convert.ToInt32(e.CommandArgument.ToString());
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarPlanPorId(idPlan);

                ViewState["idPlan"] = dt.Rows[0]["idPlan"].ToString();
                ViewState["nombrePlan"] = dt.Rows[0]["NombrePlan"].ToString();
                ViewState["precioTotal"] = Convert.ToInt32(dt.Rows[0]["PrecioTotal"].ToString());
                ViewState["precioBase"] = Convert.ToInt32(dt.Rows[0]["PrecioBase"].ToString());
                ViewState["meses"] = Convert.ToDouble(dt.Rows[0]["Meses"].ToString());
                ViewState["mesesCortesia"] = Convert.ToDouble(dt.Rows[0]["MesesCortesia"].ToString());

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

    }
}