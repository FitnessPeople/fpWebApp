using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace fpWebApp
{
    public partial class nuevoafiliado : System.Web.UI.Page
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
                        btnAgregarYRedirigir.Visible = false;
                        btnVolver.Visible = false;
                        DateTime dt14 = DateTime.Now.AddYears(-14);
                        DateTime dt100 = DateTime.Now.AddYears(-100);
                        txbFechaNac.Attributes.Add("min", dt100.Year.ToString() + "-" + string.Format("{0:MM}", dt100) + "-" + String.Format("{0:dd}", dt100));
                        txbFechaNac.Attributes.Add("max", dt14.Year.ToString() + "-" + string.Format("{0:MM}", dt14) + "-" + String.Format("{0:dd}", dt14));
                        txbDocumento.Attributes.Add("type", "number");
                        txbTelefono.Attributes.Add("type", "number");
                        txbFechaNac.Attributes.Add("type", "date");
                        txbTelefonoContacto.Attributes.Add("type", "number");
                        txbEmail.Attributes.Add("type", "email");
                        CargarTipoDocumento();
                        CargarCiudad();
                        CargarEmpresas();
                        CargarEstadoCivil();
                        CargarEps();
                        CargarProfesiones();
                        CargarSedes();
                        CargarGeneros();
                    }
                    else
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (Request.QueryString.Count > 0)
                    {
                        bool respuesta = false;
                        int idCRM = Convert.ToInt32(Request.QueryString["idcrm"].ToString());
                        clasesglobales cg = new clasesglobales();
                        DataTable dt = cg.ConsultarContactosCRMPorId(idCRM, out respuesta);
                        btnAgregar.Visible = false;
                        btnCancelar.Visible = false;
                        btnAgregarYRedirigir.Visible = true;
                        btnVolver.Visible = true;
                        if (dt.Rows.Count > 0)
                        {
                            txbNombre.Text = dt.Rows[0]["NombreContacto"].ToString();
                            txbApellido.Text = dt.Rows[0]["ApellidoContacto"].ToString();
                            txbDocumento.Text = dt.Rows[0]["DocumentoAfiliado"].ToString();
                            ddlTipoDocumento.SelectedIndex = Convert.ToInt32(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt.Rows[0]["idTipoDoc"].ToString())));
                            txbTelefono.Text = dt.Rows[0]["TelefonoContacto"].ToString();
                            txbEmail.Text = dt.Rows[0]["EmailContacto"].ToString();
                            ddlEmpresaConvenio.SelectedValue = dt.Rows[0]["idEmpresaCRM"].ToString();
                        }
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
            string strQuery = "SELECT idEmpresaAfiliada, RazonSocial FROM EmpresasAfiliadas " +
                "ORDER BY RazonSocial";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

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

        private bool ExisteDocumento(string strDocumento)
        {
            bool rta = false;
            string strQuery = "SELECT DocumentoAfiliado FROM Afiliados WHERE DocumentoAfiliado = '" + strDocumento + "' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                rta = true;
            }

            dt.Dispose();
            return rta;
        }

        private bool ExisteEmail(string strEmail)
        {
            bool rta = false;
            string strQuery = "SELECT DocumentoAfiliado FROM Afiliados WHERE EmailAfiliado = '" + strEmail + "' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                rta = true;
            }

            dt.Dispose();
            return rta;
        }

        private bool ExisteTelefono(string strTelefono)
        {
            bool rta = false;
            string strQuery = "SELECT DocumentoAfiliado FROM Afiliados WHERE CelularAfiliado = '" + strTelefono + "' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                rta = true;
            }

            dt.Dispose();
            return rta;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            if (ExisteDocumento(txbDocumento.Text.Trim()))
            {
                string script = @"
                    Swal.fire({
                        title: 'Este documento ya está registrado',
                        text: 'Ya existe un afiliado con este número de identificación.',
                        icon: 'warning'
                    });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "DocumentoDuplicado", script, true);
                UpdatePanel1.Update();
                return;
            }

            if (ExisteEmail(txbEmail.Text.Trim()))
            {
                string script = @"
                    Swal.fire({
                    title: 'Este correo ya está registrado',
                    text: 'Ya existe un afiliado con esta cuenta de correo electrónico.',
                    icon: 'warning'
                });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "EmailDuplicado", script, true);
                UpdatePanel1.Update();
                return;
            }


            if (ExisteTelefono(txbTelefono.Text.Trim()))
            {
                string script = @"
                    Swal.fire({
                    title: 'Este teléfono ya está registrado',
                    text: 'Ya existe un afiliado con este número de teléfono.',
                    icon: 'warning'
                });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "EmailDuplicado", script, true);
                UpdatePanel1.Update();
                return;
            }

            string strFilename = "nofoto.png";
            HttpPostedFile postedFile = Request.Files["fileFoto"];
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                string filePath = Server.MapPath("img//afiliados//") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                strFilename = postedFile.FileName;
            }

            clasesglobales cg = new clasesglobales();

            try
            {
                string strClave = cg.CreatePassword(8);

                mensaje = cg.InsertarAfiliado(txbDocumento.Text.Trim(), Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value), txbNombre.Text.Trim(),
                    txbApellido.Text.Trim(), txbTelefono.Text.Trim(), txbEmail.Text.Trim(), strClave, txbDireccion.Text.Trim(), Convert.ToInt32(ddlCiudadAfiliado.SelectedItem.Value),
                    txbFechaNac.Text.Trim(), strFilename, Convert.ToInt32(ddlGenero.SelectedItem.Value), Convert.ToInt32(ddlEstadoCivil.SelectedItem.Value),
                    Convert.ToInt32(ddlProfesiones.SelectedItem.Value), Convert.ToInt32(ddlEmpresaConvenio.SelectedItem.Value), Convert.ToInt32(ddlEps.SelectedItem.Value),
                    Convert.ToInt32(ddlSedes.SelectedItem.Value), txbResponsable.Text.Trim(), ddlParentesco.SelectedItem.Value.Trim(),
                    txbTelefonoContacto.Text.Trim(), Convert.ToInt32(Session["idusuario"]));

                if (mensaje == "OK")
                {
                    cg.InsertarLog(Session["idusuario"].ToString(), "afiliados", "Nuevo",
                        "El usuario creó un nuevo afiliado con documento: " + txbDocumento.Text, "", "");

                    DataTable dt = cg.TraerDatos("SELECT idAfiliado FROM Afiliados WHERE DocumentoAfiliado = '" + txbDocumento.Text + "' ");

                    string strMensaje = "Bienvenido a Fitness People \r\n\r\n";
                    strMensaje += "Se ha registrado como afiliado en Fitness People. Por favor confirme sus datos en este enlace: \r\n";
                    strMensaje += "https://fitnesspeoplecolombia.com/verificacion?id=" + dt.Rows[0]["idAfiliado"].ToString();

                    cg.EnviarCorreo("afiliaciones@fitnesspeoplecolombia.com", txbEmail.Text, "Nuevo registro en Fitness People", strMensaje);

                    string script = @"
                        Swal.fire({
                            title: 'Afiliado registrado',
                            text: 'Se ha enviado una notificación al correo del afiliado para confirmar sus datos.',
                            icon: 'success',
                            timer: 5000,
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
                            text: 'No se pudo registrar. Detalle: " + mensaje.Replace("'", "\\'") + @"',
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
                        text: 'Ocurrió un error inesperado. Detalle: " + ex.Message.Replace("'", "\\'") + @"',
                        icon: 'error'
                    });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
            }
        }


        protected void btnAgregarYRedirigir_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            if (ExisteDocumento(txbDocumento.Text.Trim()))
            {
                string script = @"
                    Swal.fire({
                        title: 'Este documento ya está registrado',
                        text: 'Ya existe un afiliado con este número de identificación.',
                        icon: 'warning'
                    });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "DocumentoDuplicado", script, true);
                UpdatePanel1.Update();
                return;
            }

            if (ExisteEmail(txbEmail.Text.Trim()))
            {
                string script = @"
                    Swal.fire({
                    title: 'Este correo ya está registrado',
                    text: 'Ya existe un afiliado con esta cuenta de correo electrónico.',
                    icon: 'warning'
                });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "EmailDuplicado", script, true);
                UpdatePanel1.Update();
                return;
            }


            if (ExisteTelefono(txbTelefono.Text.Trim()))
            {
                string script = @"
                    Swal.fire({
                    title: 'Este teléfono ya está registrado',
                    text: 'Ya existe un afiliado con este número de teléfono.',
                    icon: 'warning'
                });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "EmailDuplicado", script, true);
                UpdatePanel1.Update();
                return;
            }

            string strFilename = "nofoto.png";
            HttpPostedFile postedFile = Request.Files["fileFoto"];
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                string filePath = Server.MapPath("img//afiliados//") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                strFilename = postedFile.FileName;
            }

            clasesglobales cg = new clasesglobales();

            try
            {
                string strClave = cg.CreatePassword(8);

                mensaje = cg.InsertarAfiliado(txbDocumento.Text.Trim(), Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value), txbNombre.Text.Trim(),
                    txbApellido.Text.Trim(), txbTelefono.Text.Trim(), txbEmail.Text.Trim(), strClave, txbDireccion.Text.Trim(), Convert.ToInt32(ddlCiudadAfiliado.SelectedItem.Value),
                    txbFechaNac.Text.Trim(), strFilename, Convert.ToInt32(ddlGenero.SelectedItem.Value), Convert.ToInt32(ddlEstadoCivil.SelectedItem.Value),
                    Convert.ToInt32(ddlProfesiones.SelectedItem.Value), Convert.ToInt32(ddlEmpresaConvenio.SelectedItem.Value), Convert.ToInt32(ddlEps.SelectedItem.Value),
                    Convert.ToInt32(ddlSedes.SelectedItem.Value), txbResponsable.Text.Trim(), ddlParentesco.SelectedItem.Value.Trim(),
                    txbTelefonoContacto.Text.Trim(), Convert.ToInt32(Session["idusuario"]));

                if (mensaje == "OK")
                {
                    cg.InsertarLog(Session["idusuario"].ToString(), "afiliados", "Nuevo",
                        "El usuario creó un nuevo afiliado con documento: " + txbDocumento.Text, "", "");

                    DataTable dt = cg.TraerDatos("SELECT idAfiliado FROM Afiliados WHERE DocumentoAfiliado = '" + txbDocumento.Text + "' ");

                    string idAfil = dt.Rows[0]["idAfiliado"].ToString();
                    string strMensaje = "Bienvenido a Fitness People \r\n\r\n";
                    strMensaje += "Se ha registrado como afiliado en Fitness People. Por favor confirme sus datos en este enlace: \r\n";
                    strMensaje += "https://fitnesspeoplecolombia.com/verificacion?id=" + idAfil;

                    cg.EnviarCorreo("afiliaciones@fitnesspeoplecolombia.com", txbEmail.Text, "Nuevo registro en Fitness People", strMensaje);

                    string script = @"
                        Swal.fire({
                            title: 'Afiliado registrado correctamente',
                            text: 'Se ha enviado una notificación al correo del afiliado para confirmar sus datos.',
                            icon: 'success',
                            timer: 5000,
                            showConfirmButton: false,
                            timerProgressBar: true
                        }).then(() => {
                            window.location.href = 'planesAfiliado.aspx?idAfil='" + idAfil + @"';
                        });
                    ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                }
                else
                {
                    string script = @"
                        Swal.fire({
                            title: 'Error',
                            text: 'No se pudo registrar. Detalle: " + mensaje.Replace("'", "\\'") + @"',
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
                        text: 'Ocurrió un error inesperado. Detalle: " + ex.Message.Replace("'", "\\'") + @"',
                        icon: 'error'
                    });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
            }
        }
    }
}