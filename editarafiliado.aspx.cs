using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace fpWebApp
{
    public partial class editarafiliado : System.Web.UI.Page
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
                        txbDocumento.Attributes.Add("type", "number");
                        //txbTelefono.Attributes.Add("type", "number");
                        txbTelefonoContacto.Attributes.Add("type", "number");
                        btnCancelar.Visible = true;

                        CargarTipoDocumento();
                        CargarCiudad();
                        CargarEmpresas();
                        CargarEstadoCivil();
                        CargarEps();
                        CargarProfesiones();
                        CargarSedes();
                        CargarGeneros();
                        CargarAfiliado();
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

        private void CargarTipoDocumento()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultartiposDocumento();

            ddlTipoDocumento.DataSource = dt;
            ddlTipoDocumento.DataBind();

            dt.Dispose();
        }

        private void CargarCiudad()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCiudadesCol();

            ddlCiudadAfiliado.DataSource = dt;
            ddlCiudadAfiliado.DataBind();

            dt.Dispose();
        }

        private void CargarEmpresas()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEmpresasAfiliadas();

            ddlEmpresaConvenio.DataSource = dt;
            ddlEmpresaConvenio.DataBind();

            dt.Dispose();
        }

        private void CargarEstadoCivil()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstadosCiviles();

            ddlEstadoCivil.DataSource = dt;
            ddlEstadoCivil.DataBind();

            dt.Dispose();
        }

        private void CargarEps()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEpss();

            ddlEps.DataSource = dt;
            ddlEps.DataBind();

            dt.Dispose();
        }

        private void CargarProfesiones()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarProfesiones();

            ddlProfesiones.DataSource = dt;
            ddlProfesiones.DataBind();

            dt.Dispose();
        }

        private void CargarSedes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSedes("Gimnasio");

            ddlSedes.DataSource = dt;
            ddlSedes.DataBind();

            dt.Dispose();
        }

        private void CargarGeneros()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarGeneros();

            ddlGenero.DataSource = dt;
            ddlGenero.DataBind();

            dt.Dispose();
        }

        private void CargarAfiliado()
        {
            string idcrm = string.Empty;

            string parametro = string.Empty;
            Session["IdAfiliado"] = string.Empty;
            Session["IdCRM"] = string.Empty;
            bool respuesta = false;
            btnActualizar.Visible = true;
            btnCancelar.Visible = true;
            btnActualizaryVenderPlan.Visible = false;
            btnVolver.Visible = false;



            clasesglobales cg = new clasesglobales();
            try
            {
                idcrm = Request.QueryString["idcrm"];
                Session["IdCRM"] = idcrm;
                string editid = Request.QueryString["editid"];

                if (Request.QueryString.Count > 0)
                {
                    if (!string.IsNullOrEmpty(idcrm))
                    {
                        DataTable dt1 = cg.ConsultarContactosCRMPorId(Convert.ToInt32(idcrm), out respuesta);
                        DataTable dt2 = cg.ConsultarAfiliadoPorDocumento(Convert.ToInt32(dt1.Rows[0]["DocumentoAfiliado"].ToString()));
                        parametro = dt2.Rows[0]["idAfiliado"].ToString();
                        btnActualizar.Visible = false;
                        btnCancelar.Visible = false;
                        btnActualizaryVenderPlan.Visible = true;
                        btnVolver.Visible = true;

                        Session["IdAfiliado"] = parametro.ToString();
                    }
                    else if (!string.IsNullOrEmpty(editid))
                    {
                        parametro = editid;
                        Session["IdAfiliado"] = parametro.ToString();
                    }

                    if (!string.IsNullOrEmpty(parametro))
                    {
                        DataTable dt = cg.ConsultarAfiliadoPorId(Convert.ToInt32(parametro));

                        if (dt.Rows.Count > 0)
                        {
                            txbNombre.Text = dt.Rows[0]["NombreAfiliado"].ToString();
                            txbApellido.Text = dt.Rows[0]["ApellidoAfiliado"].ToString();
                            txbDocumento.Text = dt.Rows[0]["DocumentoAfiliado"].ToString();
                            ddlTipoDocumento.SelectedIndex = Convert.ToInt16(dt.Rows[0]["idTipoDocumento"].ToString());
                            txbTelefono.Text = dt.Rows[0]["CelularAfiliado"].ToString();
                            txbEmail.Text = dt.Rows[0]["EmailAfiliado"].ToString();
                            txbDireccion.Text = dt.Rows[0]["DireccionAfiliado"].ToString();
                            ddlCiudadAfiliado.SelectedIndex = ddlCiudadAfiliado.Items.IndexOf(ddlCiudadAfiliado.Items.FindByValue(dt.Rows[0]["idCiudadAfiliado"].ToString()));
                            ddlEmpresaConvenio.SelectedIndex = ddlEmpresaConvenio.Items.IndexOf(ddlEmpresaConvenio.Items.FindByValue(dt.Rows[0]["idEmpresaAfil"].ToString()));
                            txbFechaNac.Attributes.Add("type", "date");

                            DateTime dt14 = DateTime.Now.AddYears(-14);
                            DateTime dt100 = DateTime.Now.AddYears(-100);
                            txbFechaNac.Attributes.Add("min", $"{dt100:yyyy-MM-dd}");
                            txbFechaNac.Attributes.Add("max", $"{dt14:yyyy-MM-dd}");

                            DateTime dtFecha = DateTime.MinValue;
                            if (DateTime.TryParse(dt.Rows[0]["FechaNacAfiliado"].ToString(), out dtFecha))
                            {
                                txbFechaNac.Text = dtFecha.ToString("yyyy-MM-dd");
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[0]["FotoAfiliado"].ToString()))
                            {
                                imgFoto.ImageUrl = "img/afiliados/" + dt.Rows[0]["FotoAfiliado"].ToString();
                                ViewState["FotoAfiliado"] = dt.Rows[0]["FotoAfiliado"].ToString();
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[0]["idGenero"].ToString()))
                            {
                                ddlGenero.SelectedIndex = ddlGenero.Items.IndexOf(
                                    ddlGenero.Items.FindByValue(dt.Rows[0]["idGenero"].ToString()));
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[0]["idEstadoCivilAfiliado"].ToString()))
                            {
                                ddlEstadoCivil.SelectedIndex = ddlEstadoCivil.Items.IndexOf(
                                    ddlEstadoCivil.Items.FindByValue(dt.Rows[0]["idEstadoCivilAfiliado"].ToString()));
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[0]["idProfesion"].ToString()))
                            {
                                ddlProfesiones.SelectedIndex = ddlProfesiones.Items.IndexOf(
                                    ddlProfesiones.Items.FindByValue(dt.Rows[0]["idProfesion"].ToString()));
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[0]["idEps"].ToString()))
                            {
                                ddlEps.SelectedIndex = ddlEps.Items.IndexOf(
                                    ddlEps.Items.FindByValue(dt.Rows[0]["idEps"].ToString()));
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[0]["idSede"].ToString()))
                            {
                                ddlSedes.SelectedIndex = ddlSedes.Items.IndexOf(
                                    ddlSedes.Items.FindByValue(dt.Rows[0]["idSede"].ToString()));
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[0]["Parentesco"].ToString()))
                            {
                                ddlParentesco.SelectedIndex = ddlParentesco.Items.IndexOf(
                                    ddlParentesco.Items.FindByText(dt.Rows[0]["Parentesco"].ToString()));
                            }

                            txbResponsable.Text = dt.Rows[0]["ResponsableAfiliado"].ToString();
                            txbTelefonoContacto.Text = dt.Rows[0]["ContactoAfiliado"].ToString();
                            rblEstado.Items.FindByValue(dt.Rows[0]["EstadoAfiliado"].ToString()).Selected = true;
                        }
                        else
                        {
                            divMensaje1.Visible = true;
                            btnActualizar.Visible = false;
                        }

                        dt.Dispose();
                    }
                    else
                    {
                        divMensaje1.Visible = true;
                        btnActualizar.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string mensaje = "ERROR: " + ex.Message;
            }

        }


        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            string strFilename = "";

            // Actualiza la tabla Afiliados
            if (ViewState["FotoAfiliado"] != null)
            {
                strFilename = ViewState["FotoAfiliado"].ToString();
            }
            else
            {
                strFilename = "nofoto.png";
            }

            HttpPostedFile postedFile = Request.Files["fileFoto"];

            if (postedFile != null && postedFile.ContentLength > 0)
            {
                //Borrar la foto del afiliado si tiene una.
                if (ViewState["FotoAfiliado"] != null)
                {
                    if (ViewState["FotoAfiliado"].ToString() != "nofoto.png")
                    {
                        string strPhysicalFolder = Server.MapPath("img//afiliados//");
                        string strFileFullPath = strPhysicalFolder + ViewState["FotoAfiliado"].ToString();

                        if (File.Exists(strFileFullPath))
                        {
                            File.Delete(strFileFullPath);
                        }
                    }
                }

                //Guardar la foto del afiliado
                string filePath = Server.MapPath("img//afiliados//") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                strFilename = postedFile.FileName;
            }

            string strInitData = TraerData();

            try
            {
                clasesglobales cg = new clasesglobales();

                mensaje = cg.ActualizarAfiliado(Convert.ToInt32(Request.QueryString["editid"]), Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value),
                    txbNombre.Text.Trim().Replace("'", "").Replace("<", "").Replace(">", ""), txbApellido.Text.Trim(),
                    txbTelefono.Text.Trim(), txbEmail.Text.Trim(), txbDireccion.Text.Trim(), Convert.ToInt32(ddlCiudadAfiliado.SelectedItem.Value),
                    txbFechaNac.Text.Trim(), strFilename, Convert.ToInt32(ddlGenero.SelectedItem.Value),
                    Convert.ToInt32(ddlEstadoCivil.SelectedItem.Value), Convert.ToInt32(ddlProfesiones.SelectedItem.Value),
                    Convert.ToInt32(ddlEmpresaConvenio.SelectedItem.Value.ToString()), Convert.ToInt32(ddlEps.SelectedItem.Value), Convert.ToInt32(ddlSedes.SelectedItem.Value),
                    txbResponsable.Text.Trim(), ddlParentesco.SelectedItem.Value.Trim(), txbTelefonoContacto.Text.Trim()
                );

                if (mensaje == "OK")
                {
                    string strNewData = TraerData();
                    cg.InsertarLog(Session["idusuario"].ToString(), "afiliados", "Modifica", "El usuario modificó datos del afiliado con documento: " + txbDocumento.Text.ToString() + ".", strInitData, strNewData);

                    //Consulta si existe el afiliado en Armatura y lo actualiza
                    string url = "https://aone.armaturacolombia.co/api/person/get/" + txbDocumento.Text.ToString() + "?access_token=D2BCF6E6BD09DECAA1266D9F684FFE3F5310AD447D107A29974F71E1989AABDB";
                    string respuesta = ConsultarPersona(url);

                    if (respuesta == "success")
                    {
                        //Actualiza usuario en Armatura
                        PostArmatura(txbDocumento.Text.ToString());
                    }

                    string script = @"
                                    Swal.fire({
                                        title: '¡Afiliado actualizado correctamente!',
                                        text: '',
                                        icon: 'success',
                                        timer: 3000, // 3 segundos
                                        showConfirmButton: false,
                                        timerProgressBar: true
                                    }).then(() => {
                                        window.location.href = 'afiliados';
                                    });
                                    ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                }
                else
                {
                    string script = @"
                                    Swal.fire({
                                        title: 'Error',
                                        text: 'Por favor contáctese con Sistemas Fitness People. Detalle: " + mensaje.Replace("'", "\\'") + @"',
                                        icon: 'error'
                                    }).then((result) => {
                                        if (result.isConfirmed) {
                                            
                                        }
                                    });
                                ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                }
            }

            catch (OdbcException ex)
            {
                mensaje = ex.Message;
                string script = @"
                                Swal.fire({
                                    title: 'Error',
                                    text: 'Por favor contáctese con Sistemas Fitness People. Detalle: " + mensaje.Replace("'", "\\'") + @"',
                                    icon: 'error'
                                }).then(() => {
                                    window.location.href = 'editarafiliado';
                                });
                            ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
            }
        }

        private static string ConsultarPersona(string url)
        {
            string resultado = "";
            string resultadoj = "";
            try
            {
                WebRequest oRequest = WebRequest.Create(url);
                oRequest.Method = "GET";
                oRequest.ContentType = "application/json;charset=UTF-8";

                WebResponse oResponse = oRequest.GetResponse();
                using (var oSr = new StreamReader(oResponse.GetResponseStream()))
                {
                    resultado = oSr.ReadToEnd().Trim();
                    JObject jsonObj = JObject.Parse(resultado);
                    resultadoj = jsonObj["message"].ToString();
                }

                return resultadoj;
            }
            catch (Exception ex)
            {
                return "Error al enviar la petición: " + ex.Message;
            }
        }

        private void PostArmatura(string strDocumento)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarAfiliadoPlanPorDocumento(strDocumento);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strGenero = "";
                    string strFechaInicio = string.Empty;
                    string strFechaFinal = string.Empty;

                    if (dt.Rows[i]["idGenero"].ToString() == "1")
                        strGenero = "M";
                    if (dt.Rows[i]["idGenero"].ToString() == "2")
                        strGenero = "F";

                    if (dt.Rows[i]["EstadoPlan"].ToString() != "Archivado")
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i]["FechaInicioPlan"].ToString()))
                        {
                            strFechaInicio = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dt.Rows[i]["FechaInicioPlan"])) + " 15:00:00";
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[i]["FechaFinalPlan"].ToString()))
                        {
                            strFechaFinal = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dt.Rows[i]["FechaFinalPlan"])) + " 23:00:00";
                        }
                    }

                    Persona oPersona = new Persona()
                    {
                        pin = dt.Rows[i]["DocumentoAfiliado"].ToString(),
                        name = dt.Rows[i]["NombreAfiliado"].ToString(),
                        lastName = dt.Rows[i]["ApellidoAfiliado"].ToString(),
                        gender = strGenero,
                        personPhoto = "", // ¿Debes incluirlo?
                        certType = "",
                        certNumber = "",
                        mobilePhone = dt.Rows[i]["CelularAfiliado"].ToString(),
                        personPwd = "",
                        birthday = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dt.Rows[i]["FechaNacAfiliado"])),
                        isSendMail = "false",
                        email = dt.Rows[i]["EmailAfiliado"].ToString(),
                        deptCode = "01",
                        ssn = "",
                        cardNo = "",
                        supplyCards = "",
                        carPlate = "",
                        accStartTime = strFechaInicio,
                        accEndTime = strFechaFinal,
                        accLevelIds = "402883f08df57ba4018df57cddf70490",
                        hireDate = ""
                    };

                    string contenido = JsonConvert.SerializeObject(oPersona, Formatting.Indented);
                    string url = "https://aone.armaturacolombia.co/api/person/add/?access_token=D2BCF6E6BD09DECAA1266D9F684FFE3F5310AD447D107A29974F71E1989AABDB";
                    EnviarPeticion(url, contenido);
                }
            }
        }

        public static string EnviarPeticion(string url, string contenido)
        {
            string result = "";
            string resultadoj = "";
            try
            {
                WebRequest oRequest = WebRequest.Create(url);
                oRequest.Method = "post";
                oRequest.ContentType = "application/json;charset-UTF-8";

                using (var oSw = new StreamWriter(oRequest.GetRequestStream()))
                {
                    oSw.Write(contenido);
                    oSw.Flush();
                    oSw.Close();
                }

                WebResponse oResponse = oRequest.GetResponse();
                using (var oSr = new StreamReader(oResponse.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    result = oSr.ReadToEnd().Trim();
                    JObject jsonObj = JObject.Parse(result);
                    resultadoj = jsonObj["message"].ToString();
                }
                return resultadoj;
            }
            catch (Exception ex)
            {
                string error = "Error al enviar la petición: " + ex.Message;
                return error;
            }
        }

        public class Persona
        {
            public string pin { get; set; }
            public string name { get; set; }
            public string lastName { get; set; }
            public string gender { get; set; }
            public string personPhoto { get; set; }
            public string certType { get; set; }
            public string certNumber { get; set; }
            public string mobilePhone { get; set; }
            public string personPwd { get; set; }
            public string birthday { get; set; }
            public string isSendMail { get; set; }
            public string email { get; set; }
            public string deptCode { get; set; }
            public string ssn { get; set; }
            public string cardNo { get; set; }
            public string supplyCards { get; set; }
            public string carPlate { get; set; }
            public string accStartTime { get; set; }
            public string accEndTime { get; set; }
            public string accLevelIds { get; set; }
            public string hireDate { get; set; }

        }

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarAfiliadoPorId(Convert.ToInt32(Session["IdAfiliado"]));

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();

            return strData;
        }

        protected void btnActualizaryVenderPlan_Click(object sender, EventArgs e)
        {
            string idAfil = Session["IdAfiliado"].ToString();
            string idcrm = Session["idcrm"].ToString();
            string mensaje = string.Empty;
            string strFilename = "";

            // Actualiza la tabla Afiliados
            if (ViewState["FotoAfiliado"] != null)
            {
                strFilename = ViewState["FotoAfiliado"].ToString();
            }
            else
            {
                strFilename = "nofoto.png";
            }

            HttpPostedFile postedFile = Request.Files["fileFoto"];

            if (postedFile != null && postedFile.ContentLength > 0)
            {
                //Borrar la foto del afiliado si tiene una.
                if (ViewState["FotoAfiliado"] != null)
                {
                    if (ViewState["FotoAfiliado"].ToString() != "nofoto.png")
                    {
                        string strPhysicalFolder = Server.MapPath("img//afiliados//");
                        string strFileFullPath = strPhysicalFolder + ViewState["FotoAfiliado"].ToString();

                        if (File.Exists(strFileFullPath))
                        {
                            File.Delete(strFileFullPath);
                        }
                    }
                }

                //Guardar la foto del afiliado
                string filePath = Server.MapPath("img//afiliados//") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                strFilename = postedFile.FileName;
            }

            string strInitData = TraerData();

            try
            {
                clasesglobales cg = new clasesglobales();

                mensaje = cg.ActualizarAfiliado(Convert.ToInt32(idAfil), Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value),
                    txbNombre.Text.Trim().Replace("'", "").Replace("<", "").Replace(">", ""), txbApellido.Text.Trim(),
                    txbTelefono.Text.Trim(), txbEmail.Text.Trim(), txbDireccion.Text.Trim(), Convert.ToInt32(ddlCiudadAfiliado.SelectedItem.Value),
                    txbFechaNac.Text.Trim(), strFilename, Convert.ToInt32(ddlGenero.SelectedItem.Value),
                    Convert.ToInt32(ddlEstadoCivil.SelectedItem.Value), Convert.ToInt32(ddlProfesiones.SelectedItem.Value),
                    Convert.ToInt32(ddlEmpresaConvenio.SelectedItem.Value.ToString()), Convert.ToInt32(ddlEps.SelectedItem.Value), Convert.ToInt32(ddlSedes.SelectedItem.Value),
                    txbResponsable.Text.Trim(), ddlParentesco.SelectedItem.Value.Trim(), txbTelefonoContacto.Text.Trim()
                );

                if (mensaje == "OK")
                {
                    string strNewData = TraerData();
                    cg.InsertarLog(Session["idusuario"].ToString(), "afiliados", "Modifica", "El usuario modificó datos del afiliado con documento: " + txbDocumento.Text.ToString() + ".", strInitData, strNewData);

                    //Consulta si existe el afiliado en Armatura y lo actualiza
                    string url = "https://aone.armaturacolombia.co/api/person/get/" + txbDocumento.Text.ToString() + "?access_token=D2BCF6E6BD09DECAA1266D9F684FFE3F5310AD447D107A29974F71E1989AABDB";
                    string respuesta = ConsultarPersona(url);

                    if (respuesta == "success")
                    {
                        //Actualiza usuario en Armatura
                        PostArmatura(txbDocumento.Text.ToString());
                    }

                    string script = @"
                        Swal.fire({
                            title: '¡Afiliado actualizado correctamente!',
                            text: 'Serás redirigido a los planes comerciales',
                            icon: 'success',
                            timer: 2000, // 2 segundos
                            showConfirmButton: false,
                            timerProgressBar: true
                        }).then(() => {                           
                            window.location.href = 'planesAfiliado.aspx?idAfil=" + idAfil + "&idcrm=" + idcrm + @"';
                        });
                    ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                }
                else
                {
                    string script = @"
                            Swal.fire({
                                title: 'Error',
                                text: 'Por favor contáctese con Sistemas Fitness People. Detalle: " + mensaje.Replace("'", "\\'") + @"',
                                icon: 'error'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                            
                                }
                            });
                        ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                }
            }

            catch (OdbcException ex)
            {
                mensaje = ex.Message;
                string script = @"
                            Swal.fire({
                                title: 'Error',
                                text: 'Por favor contáctese con Sistemas Fitness People. Detalle: " + mensaje.Replace("'", "\\'") + @"',
                                icon: 'error'
                            }).then(() => {
                                window.location.href = 'editarafiliado';
                            });
                        ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("agendacrm.aspx");
        }
    }
}
