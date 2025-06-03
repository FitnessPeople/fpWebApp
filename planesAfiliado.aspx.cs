using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

                        txbWompi.Attributes.Add("type", "number");
                        txbWompi.Attributes.Add("min", "0");
                        txbWompi.Attributes.Add("max", "10000000");
                        txbWompi.Attributes.Add("step", "100");
                        txbWompi.Text = "0";

                        txbDatafono.Attributes.Add("type", "number");
                        txbDatafono.Attributes.Add("min", "0");
                        txbDatafono.Attributes.Add("max", "10000000");
                        txbDatafono.Attributes.Add("step", "100");
                        txbDatafono.Text = "0";

                        txbEfectivo.Attributes.Add("type", "number");
                        txbEfectivo.Attributes.Add("min", "0");
                        txbEfectivo.Attributes.Add("max", "10000000");
                        txbEfectivo.Attributes.Add("step", "100");
                        txbEfectivo.Text = "0";

                        txbTransferencia.Attributes.Add("type", "number");
                        txbTransferencia.Attributes.Add("min", "0");
                        txbTransferencia.Attributes.Add("max", "10000000");
                        txbTransferencia.Attributes.Add("step", "100");
                        txbTransferencia.Text = "0";

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

                        string strData = listarDetalle();
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

        //private void CargarPlanes()
        //{
        //    string strQuery = "SELECT * " +
        //        "FROM Planes " +
        //        "WHERE EstadoPlan = 'Activo' " +
        //        "AND (FechaInicial IS NULL OR FechaInicial <= CURDATE()) " +
        //        "AND (FechaFinal IS NULL OR FechaFinal >= CURDATE())";
        //    clasesglobales cg = new clasesglobales();
        //    DataTable dt = cg.TraerDatos(strQuery);

        //    if (dt.Rows.Count > 0)
        //    {
        //        PlaceHolder ph = ((PlaceHolder)this.FindControl("phPlanes"));
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            Button btn = new Button();
        //            btn.Text = dt.Rows[i]["NombrePlan"].ToString();
        //            btn.CssClass = "btn btn-" + dt.Rows[i]["NombreColorPlan"].ToString() + " btn-outline btn-block btn-sm font-bold";
        //            btn.ToolTip = dt.Rows[i]["NombrePlan"].ToString();
        //            btn.Command += new CommandEventHandler(btn_Click);
        //            btn.CommandArgument = dt.Rows[i]["idPlan"].ToString();
        //            btn.ID = dt.Rows[i]["idPlan"].ToString();
        //            ph.Controls.Add(btn);
        //        }
        //    }
        //    dt.Dispose();
        //}

        private void ListaPlanes()
        {
            clasesglobales cg = new clasesglobales();
            //DataTable dt = cg.ConsultarPlanes();
            string strQuery = "SELECT *, " +
                "IF(Permanente=1,'Sin caducidad',CONCAT('Hasta el ', DAY(FechaFinal), ' de ', MONTHNAME(FechaFinal))) AS Vigencia, " +
                "DATEDIFF(CURDATE(), FechaInicial) diaspasados, " +
                "DATEDIFF(FechaFinal, CURDATE()) diasporterminar, " +
                "DATEDIFF(FechaFinal, FechaInicial) diastotales " +
                "FROM Planes ";
            DataTable dt = cg.TraerDatos(strQuery);
            rpPlanes.DataSource = dt;
            rpPlanes.DataBind();
            dt.Dispose();
        }

        //protected void rpPlanes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        if (ViewState["CrearModificar"].ToString() == "1")
        //        {
        //            Button btnSeleccionarPlan = (Button)e.Item.FindControl("btnSeleccionarPlan");
        //            btnSeleccionarPlan.Text = ((DataRowView)e.Item.DataItem).Row[1].ToString();
        //            btnSeleccionarPlan.CssClass = "btn btn-" + ((DataRowView)e.Item.DataItem).Row[8].ToString() + " btn-outline";
        //            btnSeleccionarPlan.Command += new CommandEventHandler(btn_Click);
        //            btnSeleccionarPlan.CommandArgument = ((DataRowView)e.Item.DataItem).Row[0].ToString();
        //            btnSeleccionarPlan.ID = ((DataRowView)e.Item.DataItem).Row[0].ToString();
        //            btnSeleccionarPlan.Visible = true;
        //        }
        //    }
        //}

        private void CargarAfiliado()
        {
            if (Request.QueryString.Count > 0)
            {
                string strQuery = "SELECT *, " +
                    "IF(EstadoAfiliado='Activo','info',IF(EstadoAfiliado='Inactivo','danger','warning')) AS label " +
                    "FROM afiliados a " +
                    "RIGHT JOIN Sedes s ON a.idSede = s.idSede " +
                    "LEFT JOIN ciudades ON ciudades.idCiudad = a.idCiudadAfiliado " +
                    "WHERE a.idAfiliado = " + Request.QueryString["id"].ToString() + " ";
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);

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
                DataTable dt = cg.CargarPlanesAfiliado(Request.QueryString["id"].ToString(), "Activo");
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

        private string listarDetalle()
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
                txbTotal.Text = Convert.ToString(Convert.ToInt32(txbWompi.Text) + Convert.ToInt32(txbDatafono.Text) + Convert.ToInt32(txbEfectivo.Text) + Convert.ToInt32(txbTransferencia.Text));
            }
        }

        protected void txbDatafono_TextChanged(object sender, EventArgs e)
        {
            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                txbTotal.Text = Convert.ToString(Convert.ToInt32(txbWompi.Text) + Convert.ToInt32(txbDatafono.Text) + Convert.ToInt32(txbEfectivo.Text) + Convert.ToInt32(txbTransferencia.Text));
            }
        }

        protected void txbEfectivo_TextChanged(object sender, EventArgs e)
        {
            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                txbTotal.Text = Convert.ToString(Convert.ToInt32(txbWompi.Text) + Convert.ToInt32(txbDatafono.Text) + Convert.ToInt32(txbEfectivo.Text) + Convert.ToInt32(txbTransferencia.Text));
            }
        }

        protected void txbTransferencia_TextChanged(object sender, EventArgs e)
        {
            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                txbTotal.Text = Convert.ToString(Convert.ToInt32(txbWompi.Text) + Convert.ToInt32(txbDatafono.Text) + Convert.ToInt32(txbEfectivo.Text) + Convert.ToInt32(txbTransferencia.Text));
            }
        }

        private void btn_Click(object sender, CommandEventArgs e)
        {
            string strQuery = "SELECT * " +
                "FROM Planes " +
                "WHERE idPlan = " + e.CommandArgument;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ViewState["idPlan"] = dt.Rows[0]["idPlan"].ToString();
            ViewState["nombrePlan"] = dt.Rows[0]["NombrePlan"].ToString();
            ViewState["precioTotal"] = Convert.ToInt32(dt.Rows[0]["PrecioTotal"].ToString());
            ViewState["precioBase"] = Convert.ToInt32(dt.Rows[0]["PrecioBase"].ToString());
            ViewState["meses"] = Convert.ToDouble(dt.Rows[0]["Meses"].ToString());
            ViewState["mesesCortesia"] = Convert.ToDouble(dt.Rows[0]["MesesCortesia"].ToString());

            //divPanelResumen.Attributes.Remove("class");
            //divPanelResumen.Attributes.Add("class", "panel panel-" + dt.Rows[0]["NombreColorPlan"].ToString());

            ltPrecioBase.Text = "$" + String.Format("{0:N0}", ViewState["precioBase"]);
            ltPrecioFinal.Text = "$" + String.Format("{0:N0}", ViewState["precioTotal"]);

            CalculoPrecios();
            ActivarCortesia(ViewState["mesesCortesia"].ToString());

            //ltDescuento.Text = "0%";
            //ltAhorro.Text = "$0";
            //ltConDescuento.Text = "$0";
            ltDescripcion.Text = "<b>Características</b>: " + dt.Rows[0]["DescripcionPlan"].ToString() + "<br />";

            ltNombrePlan.Text = "<b>Plan " + ViewState["nombrePlan"].ToString() + "</b>";
        }

        private void CalculoPrecios()
        {
            double intPrecio = Convert.ToInt32(ViewState["precioTotal"]);
            double intPrecioBase = Convert.ToInt32(ViewState["precioBase"]);
            double intMeses = Convert.ToInt32(ViewState["meses"]);
            double dobDescuento = (1 - (intPrecio / intMeses) / intPrecioBase) * 100;
            double intConDescuento = (intPrecioBase * intMeses) - intPrecio;

            double dobAhorro = ((intPrecioBase * dobDescuento) / 100) * intMeses;
            

            ltPrecioBase.Text = "$" + string.Format("{0:N0}", intPrecioBase);
            ltDescuento.Text = string.Format("{0:N2}", dobDescuento) + "%";
            
            ltPrecioFinal.Text = "$" + string.Format("{0:N0}", intPrecio);
            
            ltAhorro.Text = "$" + string.Format("{0:N0}", (intPrecioBase * intMeses) - intPrecio);
            
            ltConDescuento.Text = "$" + string.Format("{0:N0}", intConDescuento);

            ltObservaciones.Text = "Valor sin descuento: $" + string.Format("{0:N0}", intPrecioBase) + "<br /><br />";
            ltObservaciones.Text += "<b>Meses</b>: " + intMeses.ToString() + ".<br />";
            ltObservaciones.Text += "<b>Descuento</b>: " + string.Format("{0:N2}", dobDescuento) + "%.<br />";
            ltObservaciones.Text += "<b>Valor del mes con descuento</b>: $" + string.Format("{0:N0}", intConDescuento) + ".<br />";
            ltObservaciones.Text += "<b>Ahorro</b>: $" + string.Format("{0:N0}", dobAhorro) + ".<br />";
            ltObservaciones.Text += "<b>Valor Total</b>: $" + string.Format("{0:N0}", intPrecio) + ".<br />";

            ViewState["observaciones"] = ltObservaciones.Text.ToString().Replace("<b>", "").Replace("</b>", "").Replace("<br />", "\r\n");
            ltValorTotal.Text = "($" + string.Format("{0:N0}", intPrecio) + ")";

            string strDataWompi = Convert.ToBase64String(Encoding.Unicode.GetBytes(ViewState["DocumentoAfiliado"].ToString() + "_" + intPrecio.ToString()));
            //lbEnlaceWompi.Text = "https://fitnesspeoplecolombia.com/wompiplan?code=" + strDataWompi;
            lbEnlaceWompi.Text = "<b>Enlace de pago Wompi:</b> <br />";
            lbEnlaceWompi.Text += MakeTinyUrl("https://fitnesspeoplecolombia.com/wompiplan?code=" + strDataWompi);
            hdEnlaceWompi.Value = MakeTinyUrl("https://fitnesspeoplecolombia.com/wompiplan?code=" + strDataWompi);
            btnPortapaleles.Visible = true;
        }

        public static string MakeTinyUrl(string url)
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

        protected void lbAgregarPlan_Click(object sender, EventArgs e)
        {
            if (ViewState["EstadoAfiliado"].ToString() != "Activo")
            {
                ltMensaje.Text = "<div class=\"ibox-content\">" +
                    "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "El afiliado no se encuentra activo." +
                    "</div></div>";
            }
            else
            {
                if (ViewState["nombrePlan"] != null)
                {
                    if (txbFechaInicio.Text != "")
                    {
                        // Consultar si este usuario tiene un plan activo y cual es su fecha de inicio y fecha final.
                        string strQuery = "SELECT * FROM AfiliadosPlanes " +
                            "WHERE idAfiliado = " + Request.QueryString["id"].ToString() + " " +
                            "AND EstadoPlan = 'Activo'";
                        clasesglobales cg = new clasesglobales();
                        DataTable dt = cg.TraerDatos(strQuery);
                        if (dt.Rows.Count > 0)
                        {
                            ltMensaje.Text = "<div class=\"ibox-content\">" +
                                "<div class=\"alert alert-danger alert-dismissable\">" +
                                "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                "Este afiliado ya tiene un plan activo, hasta el " + string.Format("{0:dd MMM yyyy}", dt.Rows[0]["FechaFinalPlan"]) +
                                "</div></div>";
                        }
                        else
                        {
                            if (ViewState["precioTotal"].ToString() != txbTotal.Text.ToString())
                            {
                                ltMensaje.Text = "<div class=\"ibox-content\">" +
                                "<div class=\"alert alert-danger alert-dismissable\">" +
                                "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                "El precio del plan es diferente al precio a pagar." +
                                "</div></div>";
                            }
                            else
                            {
                                try
                                {
                                    DateTime fechainicio = Convert.ToDateTime(txbFechaInicio.Text.ToString());
                                    DateTime fechafinal = fechainicio.AddMonths(Convert.ToInt16(ViewState["meses"].ToString()));
                                    fechafinal = fechafinal.AddDays(Convert.ToInt16(ViewState["DiasCortesia"].ToString()));
                                    strQuery = "INSERT INTO AfiliadosPlanes " +
                                        "(idAfiliado, idPlan, FechaInicioPlan, FechaFinalPlan, EstadoPlan, Meses, Valor, ObservacionesPlan) " +
                                        "VALUES (" + Request.QueryString["id"].ToString() + ", " + ViewState["idPlan"].ToString() + ", " +
                                        "'" + txbFechaInicio.Text.ToString() + "', '" + String.Format("{0:yyyy-MM-dd}", fechafinal) + "', 'Activo', " +
                                        "" + ViewState["meses"].ToString() + ", " + ViewState["precioTotal"].ToString() + ",  " +
                                        "'" + ViewState["observaciones"].ToString() + "') ";

                                    try
                                    {
                                        string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                                        using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                                        {
                                            mysqlConexion.Open();
                                            using (MySqlCommand cmd = new MySqlCommand(strQuery, mysqlConexion))
                                            {
                                                cmd.CommandType = CommandType.Text;
                                                cmd.ExecuteNonQuery();
                                            }
                                            mysqlConexion.Close();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string respuesta = "ERROR: " + ex.Message;
                                    }

                                    //if (txbWompi.Text.ToString() != "0")
                                    //{
                                    //    string strString = Convert.ToBase64String(Encoding.Unicode.GetBytes(ViewState["DocumentoAfiliado"].ToString() + "_" + ViewState["precio"].ToString()));

                                    //    string strMensaje = "Se ha creado un Plan para ud. en Fitness People \r\n\r\n";
                                    //    strMensaje += "Descripción del plan.\r\n\r\n";
                                    //    strMensaje += "Por favor, agradecemos realice el pago a través del siguiente enlace: \r\n";
                                    //    strMensaje += "https://fitnesspeoplecolombia.com/wompiplan?code=" + strString;

                                    //    cg.EnviarCorreo("afiliaciones@fitnesspeoplecolombia.com", ViewState["EmailAfiliado"].ToString(), "Plan Fitness People", strMensaje);
                                    //}

                                    strQuery = "SELECT * FROM AfiliadosPlanes ORDER BY idAfiliadoPlan DESC LIMIT 1";
                                    DataTable dt1 = cg.TraerDatos(strQuery);

                                    string strTipoPago = string.Empty;
                                    string strReferencia = string.Empty;
                                    string strBanco = string.Empty;

                                    if (txbWompi.Text.ToString() != "0")
                                    {
                                        strTipoPago = "Wompi";
                                    }
                                    if (txbDatafono.Text.ToString() != "0")
                                    {
                                        strTipoPago = "Datafono";
                                        strReferencia = txbNroAprobacion.Text.ToString();
                                    }
                                    if (txbTransferencia.Text.ToString() != "0")
                                    {
                                        strTipoPago = "Transferencia";
                                    }
                                    if (txbEfectivo.Text.ToString() != "0")
                                    {
                                        strTipoPago = "Efectivo";
                                    }

                                    if (rblBancos.SelectedItem == null)
                                    {
                                        strBanco = "No aplica";
                                    }
                                    else
                                    {
                                        strBanco = rblBancos.SelectedItem.Value.ToString();
                                    }

                                    strQuery = "INSERT INTO PagosPlanAfiliado (idAfiliadoPlan, Valor, TipoPago, idReferencia, " +
                                        "Banco, FechaHoraPago, idUsuario, EstadoPago) " +
                                        "VALUES (" + dt1.Rows[0]["idAfiliadoPlan"].ToString() + ", " +
                                        "" + txbTotal.Text.ToString() + ", " +
                                        "'" + strTipoPago + "', " +
                                        "'" + strReferencia + "', " +
                                        "'" + strBanco + "', " +
                                        "NOW(), 'Aprobado' " +
                                        "" + Session["idUsuario"].ToString() + ") ";

                                    try
                                    {
                                        string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                                        using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                                        {
                                            mysqlConexion.Open();
                                            using (MySqlCommand cmd = new MySqlCommand(strQuery, mysqlConexion))
                                            {
                                                cmd.CommandType = CommandType.Text;
                                                cmd.ExecuteNonQuery();
                                            }
                                            mysqlConexion.Close();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string respuesta = "ERROR: " + ex.Message;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    ltMensaje.Text = "<div class=\"ibox-content\">" +
                                    "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" + ex.Message.ToString() +
                                    "</div></div>";
                                    throw;
                                }
                            }
                        }
                        dt.Dispose();
                    }
                    else
                    {
                        ltMensaje.Text = "<div class=\"ibox-content\">" +
                            "<div class=\"alert alert-danger alert-dismissable\">" +
                            "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                            "Elija la fecha de inicio del plan." +
                            "</div></div>";
                    }
                }
                else
                {
                    ltMensaje.Text = "<div class=\"ibox-content\">" +
                        "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Elija el tipo de plan." +
                        "</div></div>";
                }
            }
        }

        //protected void btnSeleccionarPlan_Click(object sender, EventArgs e)
        //{
        //    string strQuery = "SELECT * " +
        //        "FROM Planes " +
        //        "WHERE idPlan = ";
        //    clasesglobales cg = new clasesglobales();
        //    DataTable dt = cg.TraerDatos(strQuery);

        //    ViewState["idPlan"] = dt.Rows[0]["idPlan"].ToString();
        //    ViewState["nombrePlan"] = dt.Rows[0]["NombrePlan"].ToString();
        //    ViewState["precioTotal"] = Convert.ToInt32(dt.Rows[0]["PrecioTotal"].ToString());
        //    ViewState["precioBase"] = Convert.ToInt32(dt.Rows[0]["PrecioBase"].ToString());
        //    ViewState["meses"] = Convert.ToDouble(dt.Rows[0]["Meses"].ToString());
        //    ViewState["mesesCortesia"] = Convert.ToDouble(dt.Rows[0]["MesesCortesia"].ToString());

        //    ltPrecioBase.Text = "$" + String.Format("{0:N0}", ViewState["precioBase"]);
        //    ltPrecioFinal.Text = "$" + String.Format("{0:N0}", ViewState["precioTotal"]);

        //    CalculoPrecios();
        //    ActivarCortesia(ViewState["mesesCortesia"].ToString());

        //    ltDescripcion.Text = "<b>Características</b>: " + dt.Rows[0]["DescripcionPlan"].ToString() + "<br />";

        //    ltNombrePlan.Text = "<b>Plan " + ViewState["nombrePlan"].ToString() + "</b>";
        //}

        protected void rpPlanes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SeleccionarPlan")
            {
                int idPlan = Convert.ToInt32(e.CommandArgument.ToString());
                string strQuery = "SELECT * " +
                    "FROM Planes " +
                    "WHERE idPlan = " + idPlan.ToString();
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);

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

        protected void rpPlanes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    LinkButton lnkPlan = (LinkButton)e.Item.FindControl("btnSeleccionarPlan");
                    lnkPlan.Attributes.Add("class", "btn btn-outline btn-" + ((DataRowView)e.Item.DataItem).Row[7].ToString() + " btn-block btn-sm");
                }
            }
        }
    }
}