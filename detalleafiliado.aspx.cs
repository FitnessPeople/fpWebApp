using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.OpenXmlFormats.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using static fpWebApp.editarafiliado;

namespace fpWebApp
{
    public partial class detalleafiliado : System.Web.UI.Page
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
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        divContenido.Visible = true;
                        btnVolver.Visible = true;
                        string strDocumento = "";
                        string idcrm = "0";
                        Session["idcrm"] = idcrm;
                        clasesglobales cg = new clasesglobales();

                        if (Request.QueryString.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(Request.QueryString["search"]))
                                strDocumento = Request.QueryString["search"].ToString();

                            if (!string.IsNullOrEmpty(Request.QueryString["idcrm"]))
                                idcrm = Request.QueryString["idcrm"].ToString();
                            if (idcrm == "0")
                            {
                                btnVolver.Visible = false;
                                DataTable dt1 = cg.ConsultarAfiliadoPlanPorDocumento(strDocumento);

                                if (dt1.Rows.Count > 0)
                                {
                                    string strQuery = "SELECT * " +
                                       "FROM AfiliadosPlanes ap, PagosPlanAfiliado ppa " +
                                       "WHERE ap.idAfiliado = " + dt1.Rows[0]["idAfiliado"].ToString() + " " +
                                       "AND ap.idAfiliadoPlan = ppa.idAfiliadoPlan " +
                                       "AND ppa.EstadoPago = 'Aprobado'";
                                    DataTable dt2 = cg.TraerDatos(strQuery);

                                    if (dt2.Rows.Count > 0)
                                    {
                                        idcrm = dt2.Rows[0]["idContacto"].ToString();
                                    }

                                    if (!string.IsNullOrEmpty(idcrm)) ltCRM.Text = "No existen registros de CRM";
                                }
                            }

                            Session["idcrm"] = idcrm;
                        }
                        else
                        {
                            Response.Redirect("afiliados");
                        }

                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            divAcceso.Visible = true;
                        }
                        MostrarDatosAfiliado(strDocumento);
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

        private void MostrarDatosAfiliado(string strDocumento)
        {
            string strQuery = "SELECT *, " +
                "IF(EstadoAfiliado='Activo','info',IF(EstadoAfiliado='Inactivo','danger','warning')) AS label " +
                "FROM Afiliados a " +
                "LEFT JOIN Sedes s ON a.idSede = s.idSede " +
                "LEFT JOIN ciudades c ON c.idCiudad = a.idCiudadAfiliado " +
                "LEFT JOIN tiposdocumento td ON td.idTipoDoc = a.idTipoDocumento " +
                "WHERE DocumentoAfiliado = '" + strDocumento + "' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            CargarPlanesAfiliado(dt.Rows[0]["idAfiliado"].ToString());
            CargarCongelaciones(dt.Rows[0]["idAfiliado"].ToString());
            CargarIncapacidades(dt.Rows[0]["idAfiliado"].ToString());
            CargarCortesias(dt.Rows[0]["idAfiliado"].ToString());
            CargarParq(dt.Rows[0]["idAfiliado"].ToString());
            CargarCRM(Convert.ToInt32(Session["idcrm"]));

            ViewState["DocumentoAfiliado"] = dt.Rows[0]["DocumentoAfiliado"].ToString();
            ltNombre.Text = dt.Rows[0]["NombreAfiliado"].ToString();
            ltApellido.Text = dt.Rows[0]["ApellidoAfiliado"].ToString();
            ltEmail.Text = dt.Rows[0]["EmailAfiliado"].ToString();
            ltDocumento.Text = dt.Rows[0]["DocumentoAfiliado"].ToString();
            ltTipoDoc.Text = dt.Rows[0]["SiglaDocumento"].ToString();
            ltCelular.Text = dt.Rows[0]["CelularAfiliado"].ToString();
            ltSede.Text = dt.Rows[0]["NombreSede"].ToString();
            ltDireccion.Text = dt.Rows[0]["DireccionAfiliado"].ToString();
            ltCiudad.Text = dt.Rows[0]["NombreCiudad"].ToString();
            ltCumple.Text = String.Format("{0:dd MMM}", Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"]));
            ltEstado.Text = "<span class=\"label label-" + dt.Rows[0]["label"].ToString() + "\">" + dt.Rows[0]["EstadoAfiliado"].ToString() + "</span>";
            //ltFoto.Text = "<img src=\"img/afiliados/nofoto.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";

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

            strQuery = "SELECT * " +
                "FROM AfiliadosPlanes ap, PagosPlanAfiliado ppa " +
                "WHERE ap.idAfiliado = " + dt.Rows[0]["idAfiliado"].ToString() + " " +
                "AND ap.idAfiliadoPlan = ppa.idAfiliadoPlan ";

            DataTable dt2 = cg.TraerDatos(strQuery);


            ltDetalle.Text = "<table class=\"footable table table-striped list-group-item-text\" data-paging-size=\"10\" data-paging=\"true\" data-sorting=\"true\" data-paging-count-format=\"{CP} de {TP}\" data-paging-limit=\"10\" data-empty=\"Sin resultados\" data-toggle-column=\"first\">";
            ltDetalle.Text += "<tr>";
            ltDetalle.Text += "<th>Referencia</th><th>Fecha</th><th>Valor</th>";
            ltDetalle.Text += "<th>Método</th><th>Estado</th>";
            ltDetalle.Text += "</tr>";

            string strFila = "";


            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    //strFila = listarDetalle(int.Parse(dt2.Rows[i]["idAfiliadoPlan"].ToString()));
                    strFila = "";
                    ltDetalle.Text += strFila;
                }
            }
            ltDetalle.Text += "</table>";


            // Consulta en Armatura la existencia de los datos biométricos
            int idempresa = 2; //2 Armatura tabla integraciones
            string parametro = string.Empty;
            DataTable dti = cg.ConsultarUrl(idempresa);
            string urlServicio = dti.Rows[0]["urlTest"].ToString() + parametro;
            if (dt.Rows.Count > 0)
            {
                parametro = dti.Rows[0]["urlServicioAd1"].ToString();
            }
            string mensaje = "falso";
            string url = urlServicio + strDocumento + parametro;
            string[] respuesta = cg.EnviarPeticionGet(url, idempresa.ToString(), out mensaje);

            ltImagen.Text = "<img src=\"img/facial-recognition.png\" width=\"100px\" />";
            if (mensaje == "Ok")
            {
                if (respuesta[1] == "success")
                {
                    ltMensaje.Text = "Con acceso biométrico";
                    divAcceso.Visible = false;
                }
                else
                {
                    ltMensaje.Text = "Sin acceso biométrico";
                }
            }
            else
            {
                ltMensaje.Text = respuesta[0];
            }
        }

        private void CargarCongelaciones(string idAfiliado)
        {
            string strQuery = "SELECT * " +
                "FROM Congelaciones c, Afiliados a, AfiliadosPlanes ap " +
                "WHERE c.idAfiliadoPlan = ap.idAfiliadoPlan " +
                "AND ap.idAfiliado = a.idAfiliado " +
                "AND a.idAfiliado = " + idAfiliado + " " +
                "ORDER BY Fecha DESC";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                rpCongelaciones.DataSource = dt;
                rpCongelaciones.DataBind();
            }

            dt.Dispose();
        }

        private void CargarIncapacidades(string idAfiliado)
        {
            string strQuery = "SELECT * " +
                "FROM Incapacidades i, Afiliados a, AfiliadosPlanes ap " +
                "WHERE i.idAfiliadoPlan = ap.idAfiliadoPlan " +
                "AND ap.idAfiliado = a.idAfiliado " +
                "AND a.idAfiliado = " + idAfiliado + " " +
                "ORDER BY Fecha DESC";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                rpIncapacidades.DataSource = dt;
                rpIncapacidades.DataBind();
            }

            dt.Dispose();
        }

        private void CargarCortesias(string idAfiliado)
        {
            string strQuery = "SELECT * " +
                "FROM Cortesias c, Afiliados a, AfiliadosPlanes ap " +
                "WHERE c.idAfiliadoPlan = ap.idAfiliadoPlan " +
                "AND ap.idAfiliado = a.idAfiliado " +
                "AND a.idAfiliado = " + idAfiliado + " " +
                "ORDER BY c.FechaHoraCortesia DESC";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                rpCortesias.DataSource = dt;
                rpCortesias.DataBind();
            }

            dt.Dispose();
        }

        private void CargarParq(string idAfiliado)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPreguntasPARQporIdAfiliado(Convert.ToInt32(idAfiliado));

            if (dt.Rows.Count > 0)
            {
                rpParq.DataSource = dt;
                rpParq.DataBind();
            }
            dt.Dispose();
        }

        private void CargarCRM(int idcrm)
        {
            bool respuesta = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarContactosCRMPorId(idcrm, out respuesta);

            if (dt.Rows.Count > 0)
            {
                ltCRM.Text = dt.Rows[0]["HistorialHTML2"].ToString();
            }
            dt.Dispose();
        }

        private void CargarPlanesAfiliado(string strIdAfiliado)
        {
            string strQuery = @"
                SELECT *, 
                IF(ap.EstadoPlan='Archivado','danger',IF(DATEDIFF(FechaFinalPlan, CURDATE())<=0,'danger','info')) AS label1, 
                IF(ap.EstadoPlan='Archivado','Archivado',IF(DATEDIFF(FechaFinalPlan, CURDATE())<=0,CONCAT(DATEDIFF(FechaFinalPlan, CURDATE())*(-1),' días vencidos'),CONCAT(DATEDIFF(FechaFinalPlan, CURDATE()),' días disponibles'))) AS diasquefaltan, 
                DATEDIFF(CURDATE(), FechaInicioPlan) AS diasconsumidos, 
                DATEDIFF(FechaFinalPlan, FechaInicioPlan) AS diastotales, 
                ROUND(DATEDIFF(CURDATE(), FechaInicioPlan) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje1, 
                ROUND(DATEDIFF(FechaFinalPlan, CURDATE()) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje2 
                FROM afiliadosPlanes ap, Afiliados a, Planes p 
                WHERE a.idAfiliado = " + strIdAfiliado + @" 
                AND ap.idAfiliado = a.idAfiliado 
                AND ap.idPlan = p.idPlan ";

            //string strQuery = "SELECT *, " +
            //    "IF(DATEDIFF(FechaFinalPlan, CURDATE())<=0,'danger','info') AS label1, " +
            //    "IF(DATEDIFF(FechaFinalPlan, CURDATE())<=0,CONCAT(DATEDIFF(FechaFinalPlan, CURDATE())*(-1),' días vencidos'),CONCAT(DATEDIFF(FechaFinalPlan, CURDATE()),' días disponibles')) AS diasquefaltan, " +
            //    "DATEDIFF(CURDATE(), FechaInicioPlan) AS diasconsumidos, " +
            //    "DATEDIFF(FechaFinalPlan, FechaInicioPlan) AS diastotales, " +
            //    "ROUND(DATEDIFF(CURDATE(), FechaInicioPlan) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje1, " +
            //    "ROUND(DATEDIFF(FechaFinalPlan, CURDATE()) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje2 " +
            //    "FROM afiliadosPlanes ap, Afiliados a, Planes p " +
            //    "WHERE a.idAfiliado = " + strIdAfiliado + " " +
            //    "AND ap.idAfiliado = a.idAfiliado " +
            //    "AND ap.idPlan = p.idPlan " +
            //    "AND ap.EstadoPlan NOT IN ('Archivado')";
            //"AND ap.EstadoPlan = 'Activo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                rpPlanesAfiliado.DataSource = dt;
                rpPlanesAfiliado.DataBind();
            }
            dt.Dispose();
        }

        private string listarDetalle(int id)
        {
            string parametro = string.Empty;
            string mensaje = string.Empty;
            int idempresa = 1;

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosPorId(id);
            DataTable dti = cg.ConsultarUrl(idempresa);

            if (dt.Rows.Count > 0)
            {
                parametro = dt.Rows[0]["IdReferencia"].ToString();
            }

            string url = dti.Rows[0]["urlTest"].ToString() + parametro;
            string[] rta = cg.EnviarPeticionGet(url, idempresa.ToString(), out mensaje);
            JToken token = JToken.Parse(rta[0]);
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
                            Valor = ((jsonData["data"]["amount_in_cents"]?.Value<int>() ?? 0) / 100).ToString("N0"),
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

                foreach (var pago in listaPagos)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{pago.Referencia}</td>");
                    sb.Append($"<td>" + String.Format("{0:dd MMM yyyy}", Convert.ToDateTime(pago.FechaCreacion)) + "</td>");
                    sb.Append($"<td>{pago.Valor}</td>");
                    sb.Append("<td>" + pago.MetodoPago.Substring(0, 1) + pago.MetodoPago.Substring(1, pago.MetodoPago.Length - 1).ToLower() + "</td>");
                    if (pago.Estado.ToString() == "APPROVED")
                    {
                        sb.Append($"<td><span class=\"label label-info\" " + pago.Estado.Substring(0, 1) + pago.Estado.Substring(1, pago.Estado.Length - 1).ToLower() + "<span></td>");
                    }
                    else
                    {
                        sb.Append($"<td><span class=\"label label-info\" " + pago.Estado.Substring(0, 1) + pago.Estado.Substring(1, pago.Estado.Length - 1).ToLower() + "</td>");
                    }
                    sb.Append("</tr>");
                }

                return sb.ToString();
            }
            else
            {
                return prettyJson;
            }
        }

        protected void lbDarAcceso_Click(object sender, EventArgs e)
        {
            PostArmatura(ViewState["DocumentoAfiliado"].ToString());
            Response.Redirect("detalleafiliado?search=" + Request.QueryString["search"].ToString());
        }

        private void PostArmatura(string strDocumento)
        {
            clasesglobales cg = new clasesglobales();
            string strQuery = "SELECT * " +
                "FROM Afiliados a, AfiliadosPlanes ap " +
                "WHERE a.DocumentoAfiliado = '" + strDocumento + "' " +
                "AND a.idAfiliado = ap.idAfiliado " +
                "AND ap.EstadoPlan = 'Activo'";
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                string strGenero = "";
                if (dt.Rows[0]["idGenero"].ToString() == "1")
                {
                    strGenero = "M";
                }
                if (dt.Rows[0]["idGenero"].ToString() == "2")
                {
                    strGenero = "F";
                }

                Persona oPersona = new Persona()
                {
                    pin = "" + dt.Rows[0]["DocumentoAfiliado"].ToString() + "",
                    name = "" + dt.Rows[0]["NombreAfiliado"].ToString() + "",
                    lastName = "" + dt.Rows[0]["ApellidoAfiliado"].ToString() + "",
                    gender = strGenero,
                    personPhoto = "",
                    certType = "",
                    certNumber = "",
                    mobilePhone = "" + dt.Rows[0]["CelularAfiliado"].ToString() + "",
                    personPwd = "",
                    birthday = "" + String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"].ToString())) + "",
                    isSendMail = "false",
                    email = "" + dt.Rows[0]["EmailAfiliado"].ToString() + "",
                    deptCode = "01",
                    ssn = "",
                    cardNo = "",
                    supplyCards = "",
                    carPlate = "",
                    accStartTime = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dt.Rows[0]["FechaInicioPlan"].ToString())) + " 05:00:00",
                    accEndTime = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dt.Rows[0]["FechaFinalPlan"].ToString())) + " 23:00:00",
                    accLevelIds = "402883f08df57ba4018df57cddf70490",
                    hireDate = ""
                };

                string contenido = JsonConvert.SerializeObject(oPersona, Formatting.Indented);

                string url = "https://aone.armaturacolombia.co/api/person/add/?access_token=D2BCF6E6BD09DECAA1266D9F684FFE3F5310AD447D107A29974F71E1989AABDB";
                string rta = EnviarPeticion(url, contenido);
            }
        }
    }
}