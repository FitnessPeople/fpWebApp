using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Globalization;
using NPOI.POIFS.Crypt.Agile;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Security.Cryptography;
using fpWebApp.Services;

namespace fpWebApp
{
	public partial class reportepagosrecurrentes : System.Web.UI.Page
	{
        //static int idIntegracion = 1; // Pruebas
        static int idIntegracion = 4; // Producción

        protected string EstadoCobroRechazado
        {
            get { return ViewState["MensajeEstadoCobroRechazado"]?.ToString(); }
            set { ViewState["MensajeEstadoCobroRechazado"] = value; }
        }

        protected string Url
        {
            get { return ViewState["urlWompi"]?.ToString(); }
            set { ViewState["urlWompi"] = value; }
        }

        protected string IntegritySecret
        {
            get { return ViewState["integrity_secret"]?.ToString(); }
            set { ViewState["integrity_secret"] = value; }
        }

        protected string KeyPub
        {
            get { return ViewState["keyPub"]?.ToString(); }
            set { ViewState["keyPub"] = value; }
        }

        protected string KeyPriv
        {
            get { return ViewState["keyPriv"]?.ToString(); }
            set { ViewState["keyPriv"] = value; }
        }

        //

        protected string DataIdTransaccion
        {
            get { return ViewState["dataIdTransaccion"]?.ToString(); }
            set { ViewState["dataIdTransaccion"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
		{
            CultureInfo culture = new CultureInfo("es-CO");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Pagos recurrentes");
                    ConsultarDatosWompi();

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
                            //CargarPlanes();
                            //lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            //CargarPlanes();
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

        private void ConsultarDatosWompi()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dtIntegracionWompi = cg.ConsultarIntegracionWompi(idIntegracion);

            Url = dtIntegracionWompi != null && dtIntegracionWompi.Rows.Count > 0 ? dtIntegracionWompi.Rows[0]["urlTest"].ToString() : null;
            IntegritySecret = dtIntegracionWompi != null && dtIntegracionWompi.Rows.Count > 0 ? dtIntegracionWompi.Rows[0]["integrity_secret"].ToString() : null;
            KeyPub = dtIntegracionWompi != null && dtIntegracionWompi.Rows.Count > 0 ? dtIntegracionWompi.Rows[0]["keyPub"].ToString() : null;
            KeyPriv = dtIntegracionWompi != null && dtIntegracionWompi.Rows.Count > 0 ? dtIntegracionWompi.Rows[0]["keyPriv"].ToString() : null;

            dtIntegracionWompi.Dispose();
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

        //private void CargarPlanes()
        //{
        //    ddlPlanes.Items.Clear();
        //    clasesglobales cg = new clasesglobales();
        //    DataTable dt = cg.ConsultarPlanesWeb();

        //    ddlPlanes.DataSource = dt;
        //    ddlPlanes.DataBind();
        //    dt.Dispose();
        //}

        private void listaTransacciones()
        {
            clasesglobales cg = new clasesglobales();

            string query = @"SELECT 
                                 ppa.idPago, ppa.Valor, ppa.DataIdFuente, ppa.FechaHoraPago, ap.fechaProximoCobro, 
                                 ap.idAfiliadoPlan, 
                                 a.DocumentoAfiliado, a.NombreAfiliado, a.ApellidoAfiliado, a.EmailAfiliado, a.idSede, 
                                 u.idUsuario, 
                                 p.idPlan, p.NombrePlan, p.CodSiigoPlan
                             FROM PagosPlanAfiliado ppa
                             INNER JOIN AfiliadosPlanes ap ON ap.idAfiliadoPlan = ppa.idAfiliadoPlan
                             INNER JOIN Afiliados a ON a.idAfiliado = ap.idAfiliado 
                             INNER JOIN Usuarios u ON u.idUsuario = ppa.idUsuario  
                             INNER JOIN Planes p ON p.idPlan = ap.idPlan 
                             WHERE ap.estadoPlan <> 'Archivado' 
                                 AND ap.fechaProximoCobro <= CURDATE() 
                             ORDER BY ap.fechaProximoCobro ASC;";

            DataTable dt = cg.TraerDatos(query);
            
            rpPagos.DataSource = dt;
            rpPagos.DataBind();
            dt.Dispose();
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT 
                                     ppa.idPago, ppa.Valor, ppa.DataIdFuente, ppa.FechaHoraPago, ap.fechaProximoCobro, 
                                     ap.idAfiliadoPlan, 
                                     a.DocumentoAfiliado, a.NombreAfiliado, a.EmailAfiliado, a.idSede, 
                                     u.idUsuario, 
                                     p.idPlan, p.NombrePlan, p.CodSiigoPlan
                                 FROM PagosPlanAfiliado ppa
                                 INNER JOIN AfiliadosPlanes ap ON ap.idAfiliadoPlan = ppa.idAfiliadoPlan
                                 INNER JOIN Afiliados a ON a.idAfiliado = ap.idAfiliado 
                                 INNER JOIN Usuarios u ON u.idUsuario = ppa.idUsuario  
                                 INNER JOIN Planes p ON p.idPlan = ap.idPlan 
                                 WHERE ap.estadoPlan <> 'Archivado'
                                     AND ap.fechaProximoCobro <= CURDATE()
                                 ORDER BY ap.fechaProximoCobro ASC;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(query);
                string nombreArchivo = $"PagosRecurrentes_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    cg.ExportarExcelEPPlus(dt, nombreArchivo);
                }
                else
                {
                    Response.Write("<script>alert('No existen registros para esta consulta');</script>");
                    //Response.Redirect("gympass.aspx?mensaje=No existen registros para esta consulta");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
                //Response.Redirect("gympass.aspx?mensaje=" + Server.UrlEncode(ex.Message));
            }
        }

        protected async void btnCobrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtenemos la fila
                Button btn = (Button)sender;
                RepeaterItem item = (RepeaterItem)btn.NamingContainer;

                // Recuperamos los datos ocultos
                int idAfiliadoPlan = Convert.ToInt32(((HiddenField)item.FindControl("hfIdAfiliadoPlan")).Value);
                int idVendedor = Convert.ToInt32(((HiddenField)item.FindControl("hfIdVendedor")).Value);
                int idSede = Convert.ToInt32(((HiddenField)item.FindControl("hfIdSede")).Value);
                int idPlan = Convert.ToInt32(((HiddenField)item.FindControl("hfIdPlan")).Value);
                string codSiigoPlan = ((HiddenField)item.FindControl("hfCodSiigoPlan")).Value;
                string nombrePlan = ((HiddenField)item.FindControl("hfNombrePlan")).Value;
                int valor = Convert.ToInt32(((HiddenField)item.FindControl("hfValor")).Value);
                string fuentePago = ((HiddenField)item.FindControl("hfFuentePago")).Value;
                string documentoAfiliado = ((HiddenField)item.FindControl("hfDocumentoAfiliado")).Value;
                string correo = ((HiddenField)item.FindControl("hfEmail")).Value;
                
                // Validaciones básicas
                if (string.IsNullOrEmpty(fuentePago))
                {
                    MostrarAlerta("Error", "Este afiliado no tiene una fuente de pago registrada.", "error");
                    return;
                }

                if (idPlan == 12)
                {
                    codSiigoPlan = "17-PlanImportadoClez";

                    if (valor == 2000)
                    {
                        valor = 89000 - valor;
                    }
                    else
                    {
                        valor = 89000;
                    }
                }

                int monto = valor * 100;
                string moneda = "COP";
                string referencia = $"{documentoAfiliado}-{DateTime.Now.ToString("yyyyMMddHHmmss")}";
                string descripcion = $"Cobro recurrente del plan {nombrePlan}";

                string concatenado = $"{referencia}{monto}{moneda}{IntegritySecret}";
                string hash256 = ComputeSha256Hash(concatenado);

                bool pagoExitoso = await CrearTransaccionRecurrenteAsync(
                    monto,
                    moneda,
                    hash256,
                    correo, 
                    referencia,
                    Convert.ToInt32(fuentePago),
                    descripcion
                );

                clasesglobales cg = new clasesglobales();

                // Si fue exitoso, registramos el pago
                if (pagoExitoso)
                {
                    RegistrarPago(
                        idAfiliadoPlan,
                        documentoAfiliado,
                        codSiigoPlan,
                        nombrePlan, 
                        valor,
                        referencia,
                        idVendedor,
                        fuentePago, 
                        idSede
                    );

                    cg.EliminarHistorialCobrosRechazados(idAfiliadoPlan);

                    MostrarAlerta("Éxito", "El cobro recurrente se procesó correctamente.", "success");
                    listaTransacciones(); // refresca la tabla
                }
                else
                {
                    cg.InsertarCobroRechazado(
                        idAfiliadoPlan, 
                        EstadoCobroRechazado
                    );

                    MostrarAlerta("Error", "El cobro no fue aprobado por la pasarela.", "error");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error inesperado", "No fue posible realizar el cobro. Error: " + ex, "error");
                System.Diagnostics.Debug.WriteLine($"[btnCobrar_Click] Error: {ex}");
            }
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Crea un SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - devuelve una matriz de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convierte una matriz de bytes en una cadena
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private async void RegistrarPago(int idAfiliadoPlan, string documentoAfiliado, string codSiigoPlan, string nombrePlan, int valor, string referencia, int idVendedor, string idFuentePago, int idSede)
        {
            try
            {
                clasesglobales cg = new clasesglobales();

                string idSiigoFactura = null;

                int idPago = cg.InsertarPagoPlanAfiliadoWebYDevolverId(
                    idAfiliadoPlan,
                    valor,
                    4,
                    referencia,
                    "Wompi",
                    idVendedor, 
                    "Aprobado",
                    idSiigoFactura,
                    null,
                    idFuentePago,
                    DataIdTransaccion,
                    null,
                    null,
                    null
                );

                cg.ActualizarFechaProximoCobro(idAfiliadoPlan);

                try
                {
                    DataTable dtIntegracion = cg.ConsultarIntegracion(idSede);
                    string url = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? dtIntegracion.Rows[0]["urlTest"].ToString() : "0";
                    string username = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? dtIntegracion.Rows[0]["username"].ToString() : "0";
                    string accessKey = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? dtIntegracion.Rows[0]["accessKey"].ToString() : "0";
                    string partnerId = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? dtIntegracion.Rows[0]["partnerId"].ToString() : "0";
                    dtIntegracion.Dispose();

                    // PRUEBAS
                    //string url = "https://api.siigo.com/";
                    //string username = "sandbox@siigoapi.com";
                    //string accessKey = "YmEzYTcyOGYtN2JhZi00OTIzLWE5ZjktYTgxNTVhNWUxZDM2Ojc0ODllKUZrSFM=";
                    //string partnerId = "SandboxSiigoApi";

                    // Creación de factura
                    var siigoClient = new SiigoClient(
                        new HttpClient(),
                        url,
                        username,
                        accessKey,
                        partnerId
                    );

                    idSiigoFactura = await siigoClient.RegisterInvoiceAsync(
                        documentoAfiliado,
                        codSiigoPlan,
                        nombrePlan,
                        valor,
                        idSede
                    );

                    // PRUEBAS
                    //string codSiigoPlanPruebas = "COD2433";
                    //string nombrePlanPruebas = "Pago de suscripción";
                    //int precioPlan = 10000;
                    //idSiigoFactura = await siigoClient.RegisterInvoiceAsync(
                    //    documentoAfiliado,
                    //    codSiigoPlanPruebas,
                    //    nombrePlanPruebas,
                    //    precioPlan,
                    //    idSede
                    //);

                    // Actualizar pago con id de factura
                    cg.ActualizarIdSiigoFacturaDePagoPlanAfiliado(idPago, idSiigoFactura);
                }
                catch (Exception siigoEx)
                {
                    System.Diagnostics.Debug.WriteLine("Error creando factura en Siigo: " + siigoEx.ToString());
                }

            } catch(Exception ex)
            {
                MostrarAlerta("Error inesperado", "No fue posible realizar el cobro. Revisa los logs para más detalles.", "error");
                System.Diagnostics.Debug.WriteLine($"[btnCobrar_Click] Error: {ex}");
            }
        }

        // ==============================
        // MÉTODO PRINCIPAL DE COBRO RECURRENTE
        // ==============================
        public async Task<bool> CrearTransaccionRecurrenteAsync(int amount_in_cents, string currency, string signature, string customer_email, string reference, int payment_source_id, string description)
        {
            try
            {
                string url = $"{Url}transactions";

                var transaccion = new
                {
                    amount_in_cents = amount_in_cents,
                    currency = currency,
                    signature = signature,
                    customer_email = customer_email,
                    reference = reference,
                    description = description,
                    payment_source_id = payment_source_id, 
                    payment_method = new
                    {
                        installments = 1
                    }
                };

                string json = JsonConvert.SerializeObject(transaccion);

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", KeyPriv);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    string result = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MostrarAlerta("Error", $"No se pudo crear la transacción. Respuesta: {result}", "error");
                        return false;
                    }

                    // Deserializa la respuesta
                    Root3 rObjetc = JsonConvert.DeserializeObject<Root3>(result);

                    if (rObjetc?.data == null || string.IsNullOrEmpty(rObjetc.data.id))
                    {
                        MostrarAlerta("Error", "No se recibió un ID válido para la transacción recurrente.", "error");
                        return false;
                    }

                    // ==============================
                    // CONSULTA DEL ESTADO
                    // ==============================
                    DataIdTransaccion = rObjetc.data.id;

                    string estado = null;
                    EstadoCobroRechazado = null;
                    int maxIntentos = 15;
                    int intentos = 0;

                    do
                    {
                        await Task.Delay(1000);
                        (estado, EstadoCobroRechazado) = await ConsultarTransaccionPorReferencia(reference);
                        intentos++;
                    }
                    while (estado == "PENDING" && intentos < maxIntentos);

                    if (estado != "APPROVED")
                    {
                        MostrarAlerta("Transacción rechazada", $"{EstadoCobroRechazado ?? "Error desconocido"}", "error");
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error inesperado", "No pudimos procesar la transacción recurrente.<br>Por favor, intenta más tarde.", "error");
                System.Diagnostics.Debug.WriteLine("Error en CrearTransaccionRecurrenteAsync: " + ex);
                return false;
            }
        }

        // ==============================
        // CONSULTA DE TRANSACCIÓN POR REFERENCIA
        // ==============================
        private async Task<(string Estado, string EstadoMensaje)> ConsultarTransaccionPorReferencia(string referencia)
        {
            try
            {
                string respuesta = await GetPostConsultaTransaccionAsync(referencia);

                // Deserializar respuesta de Wompi
                var json = JsonConvert.DeserializeObject<dynamic>(respuesta);

                if (json.status == "ERROR")
                {
                    MostrarAlerta("Error al consultar", (string)json.message, "error");
                    return (null, null);
                }

                var data = json.data;
                if (data == null || data.Count == 0)
                {
                    MostrarAlerta("Sin resultados", "No se encontraron transacciones con esta referencia.", "info");
                    return (null, null);
                }

                string estado = data[0].status;
                EstadoCobroRechazado = data[0].status_message;
                return (estado, EstadoCobroRechazado); // Ejemplo: "APPROVED", "DECLINED", "PENDING"
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error inesperado", "No se pudo consultar el estado de la transacción.", "error");
                System.Diagnostics.Debug.WriteLine("Error en ConsultarTransaccionPorReferencia: " + ex.ToString());
                return (null, null);
            }
        }

        // ==============================
        // CONSULTA HTTP GET A WOMPI
        // ==============================
        public async Task<string> GetPostConsultaTransaccionAsync(string idReferencia)
        {
            string url = $"{Url}transactions?reference={idReferencia}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", KeyPriv);

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    string result = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        return $"{{\"status\":\"ERROR\",\"message\":\"{result}\"}}";
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    return $"{{\"status\":\"ERROR\",\"message\":\"{ex.Message}\"}}";
                }
            }
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            string script = $@"
                Swal.fire({{
                    title: '{titulo}',
                    text: '{mensaje}',
                    icon: '{tipo}',
                    confirmButtonText: 'Aceptar',
                    background: '#3C3C3C'
                }});";

            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), script, true);
        }

        // ==============================
        // MODELOS NECESARIOS
        // ==============================
        public class Data3
        {
            public string id { get; set; }
            public DateTime created_at { get; set; }
            public int amount_in_cents { get; set; }
            public string reference { get; set; }
            public string customer_email { get; set; }
            public string currency { get; set; }
            public string signature { get; set; }
            public string payment_method_type { get; set; }
            public string status { get; set; }
            public string status_message { get; set; }
            public int payment_source_id { get; set; }
        }

        public class Meta { }

        public class Root3
        {
            public Data3 data { get; set; }
            public Meta meta { get; set; }
        }
    }
}