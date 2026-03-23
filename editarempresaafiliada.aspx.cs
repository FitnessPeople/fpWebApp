using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;

namespace fpWebApp
{
    public partial class editarempresaafiliada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Empresas convenio");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        txbTelefonoPpal.Attributes.Add("type", "number");
                        //txbTelefonoSrio.Attributes.Add("type", "number");
                        //txbCelular.Attributes.Add("type", "number");
                        txbFechaConvenio.Attributes.Add("type", "date");
                        txbFechaFinConvenio.Attributes.Add("type", "date");
                        txbNroEmpleados.Attributes.Add("type", "number");
                        CargarTipoDocumento();
                        CargarCiudad();
                        CargarEmpresa();
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

            ddlCiudadEmpresa.DataSource = dt;
            ddlCiudadEmpresa.DataBind();

            dt.Dispose();
        }

        private void CargarEmpresa()
        {
            if (Request.QueryString.Count > 0)
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarEmpresaAfiliadaPorId(Convert.ToInt32(Request.QueryString["editid"].ToString()));
                string contenidoEditor = hiddenEditor.Value;

                if (dt.Rows.Count > 0)
                {
                    txbNombreCcial.Text = dt.Rows[0]["NombreComercial"].ToString();
                    txbRazonSocial.Text = dt.Rows[0]["RazonSocial"].ToString();
                    txbDocumento.Text = dt.Rows[0]["DocumentoEmpresa"].ToString();
                    ddlTipoDocumento.SelectedIndex = Convert.ToInt16(dt.Rows[0]["idTipoDocumento"].ToString());
                    txbDV.Text = dt.Rows[0]["digitoverificacion"].ToString();
                    txbNombreContacto.Text = dt.Rows[0]["NombreContacto"].ToString();
                    txbCargoContacto.Text = dt.Rows[0]["CargoContacto"].ToString();

                    txbTelefonoPpal.Text = dt.Rows[0]["CelularEmpresa"].ToString();
                    txbCorreo.Text = dt.Rows[0]["CorreoEmpresa"].ToString();

                    txbNombrepagador.Text = dt.Rows[0]["NombrePagador"].ToString();
                    txbCelularPagador.Text = dt.Rows[0]["TelefonoPagador"].ToString();
                    txbCorreoPagador.Text = dt.Rows[0]["CorreoPagador"].ToString();

                    if (dt.Rows.Count > 0 && dt.Rows[0]["RetornoAdm"] != DBNull.Value)
                    {
                        rblActivo.SelectedValue = dt.Rows[0]["RetornoAdm"].ToString();
                    }
                    hiddenEditor.Value = dt.Rows[0]["Descripcion"].ToString();

                    //string activo = rblActivo.SelectedValue;

                    txbDireccion.Text = dt.Rows[0]["DireccionEmpresa"].ToString();
                    ddlCiudadEmpresa.SelectedIndex = Convert.ToInt16(ddlCiudadEmpresa.Items.IndexOf(ddlCiudadEmpresa.Items.FindByValue(dt.Rows[0]["idCiudadEmpresa"].ToString())));
                    txbFechaConvenio.Attributes.Add("type", "date");

                    DateTime dtFecha = Convert.ToDateTime(dt.Rows[0]["FechaConvenio"].ToString());
                    DateTime dtFechaFin = Convert.ToDateTime(dt.Rows[0]["FechaFinConvenio"].ToString());

                    txbFechaConvenio.Text = dtFecha.ToString("yyyy-MM-dd");
                    txbFechaFinConvenio.Text = dtFechaFin.ToString("yyyy-MM-dd");
                    txbNroEmpleados.Text = dt.Rows[0]["NroEmpleados"].ToString();
                    ddlTipoNegociacion.SelectedIndex = Convert.ToInt16(ddlTipoNegociacion.Items.IndexOf(ddlTipoNegociacion.Items.FindByText(dt.Rows[0]["TipoNegociacion"].ToString())));
                    ddlDiasCredito.SelectedIndex = Convert.ToInt16(ddlDiasCredito.Items.IndexOf(ddlDiasCredito.Items.FindByText(dt.Rows[0]["DiasCredito"].ToString())));

                    if (dt.Rows.Count > 0)
                    {

                        string rutaDocs = "~/docs/contratos/";

                        string contrato = dt.Rows[0]["Contrato"].ToString();

                        if (!string.IsNullOrEmpty(contrato) &&
                            File.Exists(Server.MapPath(rutaDocs + contrato)))
                        {
                            lnkContrato.Text = contrato;
                            lnkContrato.NavigateUrl = rutaDocs + contrato;
                        }
                        else
                        {
                            lnkContrato.Text = "No hay archivo cargado";
                            lnkContrato.NavigateUrl = "";
                        }
                        // CAMARA
                        string camara = dt.Rows[0]["CamaraComercio"].ToString();

                        if (!string.IsNullOrEmpty(camara) &&
                            File.Exists(Server.MapPath(rutaDocs + camara)))
                        {
                            lnkCamara.Text = camara;
                            lnkCamara.NavigateUrl = rutaDocs + camara;
                        }

                        // RUT
                        string rut = dt.Rows[0]["Rut"].ToString();

                        if (!string.IsNullOrEmpty(rut) &&
                            File.Exists(Server.MapPath(rutaDocs + rut)))
                        {
                            lnkRut.Text = rut;
                            lnkRut.NavigateUrl = rutaDocs + rut;
                        }

                        // CEDULA
                        string cedula = dt.Rows[0]["CedulaRepLeg"].ToString();

                        if (!string.IsNullOrEmpty(cedula) &&
                            File.Exists(Server.MapPath(rutaDocs + cedula)))
                        {
                            lnkCedula.Text = cedula;
                            lnkCedula.NavigateUrl = rutaDocs + cedula;
                        }
                    }
 
                    rblEstado.Items.FindByValue(dt.Rows[0]["EstadoEmpresa"].ToString()).Selected = true;
                }
                else
                {
                    //divMensaje1.Visible = true;
                    btnActualizar.Visible = false;
                }

                dt.Dispose();
            }
            else
            {
                //divMensaje1.Visible = true;
                btnActualizar.Visible = false;
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {


            clasesglobales cg1 = new clasesglobales();
            string respuesta = string.Empty;
            try
            {
                DataTable dt = cg1.ConsultarEmpresaAfiliadaPorId(Convert.ToInt32(Request.QueryString["editid"].ToString()));
                string strInitData = TraerData();




                string carpeta = Server.MapPath("~/docs/contratos/");
                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }


                HttpPostedFile postedFileContrato = Request.Files["fileConvenio"];
                HttpPostedFile postedFileCamara = Request.Files["fileCamara"];
                HttpPostedFile postedFileRut = Request.Files["fileRut"];
                HttpPostedFile postedFileCedRep = Request.Files["fileCedulaRepLeg"];

                string strFilenameContrato = dt.Rows[0]["Contrato"].ToString();
                string strFilenameCamara = dt.Rows[0]["CamaraComercio"].ToString();
                string strFilenameRut = dt.Rows[0]["Rut"].ToString();
                string strFilenameCedula = dt.Rows[0]["CedulaRepLeg"].ToString();




                if (postedFileContrato != null && postedFileContrato.ContentLength > 0)
                {
                    string nombre = txbDocumento.Text.Trim() + "_Contrato.pdf";

                    postedFileContrato.SaveAs(Path.Combine(carpeta, nombre));

                    strFilenameContrato = nombre;
                }

                if (postedFileCamara != null && postedFileCamara.ContentLength > 0)
                {
                    string nombre = txbDocumento.Text.Trim() + "_CamaraComercio.pdf";

                    postedFileCamara.SaveAs(Path.Combine(carpeta, nombre));

                    strFilenameCamara = nombre;
                }

                if (postedFileRut != null && postedFileRut.ContentLength > 0)
                {
                    string nombre = txbDocumento.Text.Trim() + "_Rut.pdf";

                    postedFileRut.SaveAs(Path.Combine(carpeta, nombre));

                    strFilenameRut = nombre;
                }

                if (postedFileCedRep != null && postedFileCedRep.ContentLength > 0)
                {
                    string nombre = txbDocumento.Text.Trim() + "_CedulaRepLegal.pdf";

                    postedFileCedRep.SaveAs(Path.Combine(carpeta, nombre));

                    strFilenameCedula = nombre;
                }

                clasesglobales cg = new clasesglobales();
                respuesta = cg.EditarEmpresaAfiliada(Convert.ToInt32(Request.QueryString["editid"].ToString()), txbDocumento.Text, Convert.ToInt32(ddlTipoDocumento.SelectedValue),
                txbNombreCcial.Text, txbRazonSocial.Text, Convert.ToDateTime(txbFechaConvenio.Text), Convert.ToDateTime(txbFechaFinConvenio.Text), txbNombreContacto.Text,
                txbCargoContacto.Text, txbTelefonoPpal.Text, txbCorreo.Text, txbNombrepagador.Text, txbCelularPagador.Text, txbCorreoPagador.Text,
                txbDireccion.Text, Convert.ToInt32(ddlCiudadEmpresa.SelectedValue), Convert.ToInt32(txbNroEmpleados.Text), ddlTipoNegociacion.SelectedItem.Text,
                Convert.ToInt32(ddlDiasCredito.SelectedItem.Text), strFilenameContrato, strFilenameCamara, strFilenameRut, strFilenameCedula, "Activo", Convert.ToInt32(rblActivo.SelectedValue),
                txbDV.Text, hiddenEditor.Value, Convert.ToInt32(Session["idUsuario"]));


                string strNewData = TraerData();

                cg.InsertarLog(Session["idusuario"].ToString(), "empresas convenio", "Modifica", "El usuario modificó datos a la empresa afiliada con documento: " + txbDocumento.Text.ToString() + ".", strInitData, strNewData);

                if (respuesta != "OK")
                {
                    string script = @"
                            Swal.fire({
                                title: 'Error',
                                text: '" + respuesta.Replace("'", "\\'") + @"',
                                icon: 'error'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                            
                                }
                            });
                        ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                }
                else
                {
                    string script = @"
                            Swal.fire({
                                title: 'La empresa se actualizó de forma exitosa',
                                text: 'Corporativo - Fitness People',
                                icon: 'success',
                                timer: 3000, // 3 segundos
                                showConfirmButton: false,
                                timerProgressBar: true
                            }).then(() => {
                                window.location.href = 'empresasconvenio';
                            });
                            ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);

                    cg.InsertarLog(Session["idusuario"].ToString(), "empresas convenio", "Nuevo", "El usuario creó una nueva empresa convenio con documento: " + txbDocumento.Text.ToString() + ".", "", "");
                }
            }
            catch (SqlException ex)
            {
                respuesta = ex.Message;
            }
        }


        private string TraerData()
        {

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEmpresaAfiliadaPorId(Convert.ToInt32(Request.QueryString["editid"].ToString()));

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();

            return strData;
        }
    }
}