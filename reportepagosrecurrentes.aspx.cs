using fpWebApp.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.POIFS.Crypt.Agile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static fpWebApp.Services.SiigoClient;

namespace fpWebApp
{
	public partial class reportepagosrecurrentes : System.Web.UI.Page
	{
        // PRUEBAS
        //static int idIntegracionWompi = 1; // WOMPI
        //static int idIntegracionSiigo = 3; // SIIGO


        // PRODUCCIÓN
        static int idIntegracionWompi = 4; // WOMPI
        static int idIntegracionSiigo = 6; // SIIGO

        protected string EstadoCobroRechazado
        {
            get { return ViewState["MensajeEstadoCobroRechazado"]?.ToString(); }
            set { ViewState["MensajeEstadoCobroRechazado"] = value; }
        }

        protected string UrlWompi
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

        // Siigo

        protected string UrlSiigo
        {
            get { return ViewState["urlSiigo"]?.ToString(); }
            set { ViewState["urlSiigo"] = value; }
        }

        protected string UserName
        {
            get { return ViewState["username"]?.ToString(); }
            set { ViewState["username"] = value; }
        }

        protected string AccessKey
        {
            get { return ViewState["accessKey"]?.ToString(); }
            set { ViewState["accessKey"] = value; }
        }

        protected string PartnerId
        {
            get { return ViewState["partnerId"]?.ToString(); }
            set { ViewState["partnerId"] = value; }
        }

        //

        protected int IdDocumentType
        {
            get { return ViewState["idDocumentType"] != null ? (int)ViewState["idDocumentType"] : 0; }
            set { ViewState["idDocumentType"] = value; }
        }

        protected int IdSellerUser
        {
            get { return ViewState["idSellerUser"] != null ? (int)ViewState["idSellerUser"] : 0; }
            set { ViewState["idSellerUser"] = value; }
        }

        protected int IdPayment
        {
            get { return ViewState["idPayment"] != null ? (int)ViewState["idPayment"] : 0; }
            set { ViewState["idPayment"] = value; }
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
                    ConsultarIntegracion();

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

        private void ConsultarIntegracion()
        {
            clasesglobales cg = new clasesglobales();

            DataTable dtIntegracionWompi = cg.ConsultarIntegracionPorId(idIntegracionWompi);
            UrlWompi = dtIntegracionWompi != null && dtIntegracionWompi.Rows.Count > 0 ? dtIntegracionWompi.Rows[0]["url"].ToString() : null;
            IntegritySecret = dtIntegracionWompi != null && dtIntegracionWompi.Rows.Count > 0 ? dtIntegracionWompi.Rows[0]["integrity_secret"].ToString() : null;
            KeyPub = dtIntegracionWompi != null && dtIntegracionWompi.Rows.Count > 0 ? dtIntegracionWompi.Rows[0]["keyPub"].ToString() : null;
            KeyPriv = dtIntegracionWompi != null && dtIntegracionWompi.Rows.Count > 0 ? dtIntegracionWompi.Rows[0]["keyPriv"].ToString() : null;
            dtIntegracionWompi.Dispose();

            DataTable dtIntegracionSiigo = cg.ConsultarIntegracionPorId(idIntegracionSiigo);
            UrlSiigo = dtIntegracionSiigo != null && dtIntegracionSiigo.Rows.Count > 0 ? dtIntegracionSiigo.Rows[0]["url"].ToString() : null;
            UserName = dtIntegracionSiigo != null && dtIntegracionSiigo.Rows.Count > 0 ? dtIntegracionSiigo.Rows[0]["username"].ToString() : null;
            AccessKey = dtIntegracionSiigo != null && dtIntegracionSiigo.Rows.Count > 0 ? dtIntegracionSiigo.Rows[0]["accessKey"].ToString() : null;
            PartnerId = dtIntegracionSiigo != null && dtIntegracionSiigo.Rows.Count > 0 ? dtIntegracionSiigo.Rows[0]["partnerId"].ToString() : null;

            IdDocumentType = dtIntegracionSiigo != null && dtIntegracionSiigo.Rows.Count > 0 ? Convert.ToInt32(dtIntegracionSiigo.Rows[0]["idDocumentType"].ToString()) : 0;
            IdSellerUser = dtIntegracionSiigo != null && dtIntegracionSiigo.Rows.Count > 0 ? Convert.ToInt32(dtIntegracionSiigo.Rows[0]["idSellerUser"].ToString()) : 0;
            IdPayment = dtIntegracionSiigo != null && dtIntegracionSiigo.Rows.Count > 0 ? Convert.ToInt32(dtIntegracionSiigo.Rows[0]["idPayment"].ToString()) : 0;
            dtIntegracionSiigo.Dispose();
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
            DataTable dt = cg.ConsultarCobrosRecurrentes();

            if (!dt.Columns.Contains("ProximoValorCobrar")) dt.Columns.Add("ProximoValorCobrar", typeof(int));
            if (!dt.Columns.Contains("MontoAcumulado")) dt.Columns.Add("MontoAcumulado", typeof(int));
            if (!dt.Columns.Contains("MesesACobrar")) dt.Columns.Add("MesesACobrar", typeof(int));

            foreach (DataRow row in dt.Rows)
            {
                int idPlan = Convert.ToInt32(row["idPlan"]);
                int idAfiliadoPlan = Convert.ToInt32(row["idAfiliadoPlan"]);
                int valorBase = Convert.ToInt32(row["Valor"]);
                DateTime fechaProximoCobro = Convert.ToDateTime(row["FechaProximoCobro"]);

                int mesesAtraso = ((DateTime.Now.Year - fechaProximoCobro.Year) * 12) + DateTime.Now.Month - fechaProximoCobro.Month;

                if (DateTime.Now.Day >= fechaProximoCobro.Day) mesesAtraso++;

                if (mesesAtraso < 0) mesesAtraso = 0;

                // Si está al día, al menos mostrar el valor del mes actual
                int mesesACobrar = mesesAtraso > 0 ? mesesAtraso : 1;

                int mesesPagados = cg.ConsultarCantidadMesesPagadosPorIdAfiliadoPlan(idAfiliadoPlan);

                row["ProximoValorCobrar"] = cg.ObtenerValorMesPlanSimulado(idPlan, mesesPagados, valorBase);


                // Monto Acumulado
                int montoTotal = 0; 

                for (int i = 0; i < mesesACobrar; i++)
                {
                    int mesSimulado = mesesPagados + i;

                    int valorMes  = cg.ObtenerValorMesPlanSimulado(idPlan, mesSimulado, valorBase);

                    montoTotal += valorMes;
                }

                row["MontoAcumulado"] = montoTotal;
                row["MesesACobrar"] = mesesACobrar;
            }

            rpPagos.DataSource = dt;
            rpPagos.DataBind();
            dt.Dispose();
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarCobrosRecurrentes();

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

        protected async void btnCobrar_Click(object sender, CommandEventArgs e)
        {
            try
            {
                // Obtenemos la fila
                Button btn = (Button)sender;
                RepeaterItem item = (RepeaterItem)btn.NamingContainer;

                int valorACobrar;
                int mesesACobrar = 1;

                if (e.CommandName == "ACUMULADO")
                {
                    valorACobrar = Convert.ToInt32(((HiddenField)item.FindControl("hfMontoAcumulado")).Value);
                    mesesACobrar = Convert.ToInt32(((HiddenField)item.FindControl("hfMesesACobrar")).Value);
                }
                else
                {
                    valorACobrar = Convert.ToInt32(((HiddenField)item.FindControl("hfProximoValorCobrar")).Value);
                }

                // Recuperamos los datos ocultos
                int idAfiliadoPlan = Convert.ToInt32(((HiddenField)item.FindControl("hfIdAfiliadoPlan")).Value);
                int idVendedor = Convert.ToInt32(((HiddenField)item.FindControl("hfIdVendedor")).Value);
                int idSede = Convert.ToInt32(((HiddenField)item.FindControl("hfIdSede")).Value);
                int idPlan = Convert.ToInt32(((HiddenField)item.FindControl("hfIdPlan")).Value);
                string codSiigoPlan = ((HiddenField)item.FindControl("hfCodSiigoPlan")).Value;
                string nombrePlan = ((HiddenField)item.FindControl("hfNombrePlan")).Value;
                string fuentePago = ((HiddenField)item.FindControl("hfFuentePago")).Value;
                string documentoAfiliado = ((HiddenField)item.FindControl("hfDocumentoAfiliado")).Value;
                string correo = ((HiddenField)item.FindControl("hfEmail")).Value;

                // Validaciones básicas
                if (string.IsNullOrEmpty(fuentePago))
                {
                    MostrarAlerta("Error", "Este afiliado no tiene una fuente de pago registrada.", "error");
                    return;
                }

                clasesglobales cg = new clasesglobales();

                DataTable dtSede = cg.ConsultarSedePorId(idSede);
                int idCostCenter = dtSede != null && dtSede.Rows.Count > 0 ? Convert.ToInt32(dtSede.Rows[0]["idCostCenterSiigo"].ToString()) : 0;
                dtSede.Dispose();

                int monto = valorACobrar * 100;
                string moneda = "COP";
                string referencia = $"{documentoAfiliado}-{DateTime.Now.ToString("yyyyMMddHHmmss")}";

                string plural = mesesACobrar > 1 ? "meses" : "mes";
                string descripcion = e.CommandName == "ACUMULADO"
                     ? $"Cobro de {mesesACobrar} {plural} del plan {nombrePlan} por valor de ${valorACobrar}."
                     : $"Cobro mensual del plan {nombrePlan} por valor de ${valorACobrar}.";

                string concatenado = $"{referencia}{monto}{moneda}{IntegritySecret}";
                string hash256 = ComputeSha256Hash(concatenado);

                string observaciones = e.CommandName == "ACUMULADO"
                    ? $"Pago correspondiente a {mesesACobrar} meses del plan {nombrePlan}."
                    : $"Pago correspondiente a 1 mes del plan {nombrePlan}.";

                bool pagoExitoso = await CrearTransaccionRecurrenteAsync(
                    monto,
                    moneda,
                    hash256,
                    correo, 
                    referencia,
                    Convert.ToInt32(fuentePago),
                    descripcion
                );

                // Si fue exitoso, registramos el pago
                if (pagoExitoso)
                {
                    RegistrarPago(
                        idAfiliadoPlan,
                        documentoAfiliado,
                        codSiigoPlan,
                        nombrePlan,
                        observaciones, 
                        valorACobrar,
                        mesesACobrar,
                        referencia,
                        idVendedor,
                        fuentePago, 
                        idSede,
                        idCostCenter
                    );

                    cg.InsertarLog(Session["idusuario"].ToString(), "PagoPlanAfiliado", "Agrega", "El usuario agregó pago.", "", "");

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

                    MostrarAlerta("Error en el cobro", $"El intento de cobro fue rechazado por Wompi.\nDetalle del rechazo:\n{EstadoCobroRechazado}", "error");
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

        private async void RegistrarPago(int idAfiliadoPlan, string documentoAfiliado, string codSiigoPlan, string nombrePlan, string observaciones, int valor, int mesesCobrados, string referencia, int idVendedor, string idFuentePago, int idSede, int idCostCenter)
        {
            try
            {
                clasesglobales cg = new clasesglobales();

                int idPago = cg.InsertarPagoPlanAfiliadoWebYDevolverId(
                    idAfiliadoPlan,
                    valor,
                    4,
                    referencia,
                    "Wompi",
                    idVendedor, 
                    "Aprobado",
                    null,
                    null,
                    idFuentePago,
                    DataIdTransaccion,
                    null,
                    null,
                    null
                );

                cg.ActualizarMesesPagadosEnPagoPlanAfiliado(idPago, mesesCobrados);

                cg.ActualizarFechaProximoCobro(idAfiliadoPlan, mesesCobrados);

                try
                {
                    string fechaActual = DateTime.Now.ToString("yyyy-MM-dd");

                    // Creación de factura
                    var siigoClient = new SiigoClient(
                        new HttpClient(),
                        UrlSiigo,
                        UserName,
                        AccessKey,
                        PartnerId
                    );


                    // COMENTADO HASTA NUEVO AVISO

                    DataTable dtAfi = cg.ConsultarAfiliadoPorDocumento(Convert.ToInt32(documentoAfiliado));
                    // Obtener datos del afiliado
                    string strNombre = dtAfi.Rows[0]["NombreAfiliado"].ToString();
                    string strApellido = dtAfi.Rows[0]["ApellidoAfiliado"].ToString();
                    string strTelefono = dtAfi.Rows[0]["CelularAfiliado"].ToString();
                    string strCorreo = dtAfi.Rows[0]["EmailAfiliado"].ToString();
                    dtAfi.Dispose();

                    DataTable dtCodSiigo = cg.ConsultarCodigoSiigoPorDocumento(documentoAfiliado);
                    string idTipoDocSiigo = dtCodSiigo.Rows[0]["CodSiigo"].ToString();
                    dtCodSiigo.Dispose();

                    DataTable dtSede = cg.ConsultarSedePorId(idSede);
                    string strDireccion = dtSede.Rows[0]["DireccionSede"].ToString();
                    int idCiudad = Convert.ToInt32(dtSede.Rows[0]["idCiudadSede"].ToString());
                    dtSede.Dispose();

                    DataTable dtCiudad = cg.ConsultarCiudadSedeSiigoPorId(idCiudad);
                    string codEstado = dtCiudad.Rows[0]["CodigoEstado"].ToString();
                    string codCiudad = dtCiudad.Rows[0]["CodigoCiudad"].ToString();
                    dtCiudad.Dispose();

                    await siigoClient.ManageCustomerAsync(idTipoDocSiigo, documentoAfiliado, strNombre, strApellido, strDireccion, codEstado, codCiudad, strTelefono, strCorreo);

                    // COMENTADO HASTA NUEVO AVISO


                    // PRODUCCIÓN
                    // TODO: NO ELIMINAR ESTO, SE USA EN LA CREACIÓN DE LA FACTURA
                    // ESTÁ COMENTADO PARA PRUEBAS LOCALES
                    string idSiigoFactura = await siigoClient.RegisterInvoiceAsync(
                        documentoAfiliado,
                        codSiigoPlan,
                        nombrePlan,
                        valor,
                        observaciones,
                        IdSellerUser,
                        IdDocumentType,
                        fechaActual,
                        idCostCenter,
                        IdPayment
                    );


                    // PRUEBAS
                    //if (idIntegracionSiigo == 3) idCostCenter = 621;

                    //codSiigoPlan = "COD2433";
                    //nombrePlan = "Pago de suscripción";
                    //int precioPlan = 10000;
                    //string idSiigoFactura = await siigoClient.RegisterInvoiceAsync(
                    //    documentoAfiliado,
                    //    codSiigoPlan,
                    //    nombrePlan,
                    //    precioPlan,
                    //    observaciones,
                    //    IdSellerUser,
                    //    IdDocumentType,
                    //    fechaActual,
                    //    idCostCenter,
                    //    IdPayment
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
                string url = $"{UrlWompi}transactions";

                var transaccion = new
                {
                    amount_in_cents = amount_in_cents,
                    currency = currency,
                    signature = signature,
                    customer_email = customer_email,
                    reference = reference,
                    description = description,
                    payment_source_id = payment_source_id, 
                    payment_method = new { installments = 1 }
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
            string url = $"{UrlWompi}transactions?reference={idReferencia}";

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
                    confirmButtonText: 'Aceptar'
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