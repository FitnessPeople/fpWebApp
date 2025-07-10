using System;
using System.Data;
using System.IO;
using System.Web;

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


                        // txbNombre.Text = Request.QueryString["idcrm"].ToString();   
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
            string strQuery = "SELECT * FROM estadocivil";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlEstadoCivil.DataSource = dt;
            ddlEstadoCivil.DataBind();

            dt.Dispose();
        }

        private void CargarEps()
        {
            string strQuery = "SELECT * FROM eps";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlEps.DataSource = dt;
            ddlEps.DataBind();

            dt.Dispose();
        }

        private void CargarProfesiones()
        {
            string strQuery = "SELECT * FROM profesiones";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

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
            string strQuery = "SELECT * FROM generos ORDER BY idGenero";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

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
            // Validar si existe por Cedula, Email y/o Telefono
            if (ExisteDocumento(txbDocumento.Text.ToString().Trim()))
            {
                ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Un afiliado con este documento ya existe!" +
                    "</div>";
            }
            else
            {
                if (ExisteEmail(txbEmail.Text.ToString().Trim()))
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Un afiliado con este correo electronico ya existe!" +
                        "</div>";
                }
                else
                {
                    if (ExisteTelefono(txbTelefono.Text.ToString().Trim()))
                    {
                        ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Un afiliado con este teléfono ya existe!" +
                        "</div>";
                    }
                    else
                    {
                        // Inserta en la tabla Afiliados
                        string strFilename = "nofoto.png";
                        HttpPostedFile postedFile = Request.Files["fileFoto"];

                        if (postedFile != null && postedFile.ContentLength > 0)
                        {
                            //Save the File.
                            string filePath = Server.MapPath("img//afiliados//") + Path.GetFileName(postedFile.FileName);
                            postedFile.SaveAs(filePath);
                            strFilename = postedFile.FileName;
                        }

                        clasesglobales cg = new clasesglobales();
                        string strClave = cg.CreatePassword(8);

                        string strQuery = "INSERT INTO afiliados " +
                        "(DocumentoAfiliado, idTipoDocumento, NombreAfiliado, ApellidoAfiliado, CelularAfiliado, EmailAfiliado, ClaveAfiliado, " +
                        "DireccionAfiliado, idCiudadAfiliado, FechaNacAfiliado, FotoAfiliado, idGenero, idEstadoCivilAfiliado, idEmpresaAfil, idProfesion, " +
                        "idEps, idSede, ResponsableAfiliado, Parentesco, ContactoAfiliado, EstadoAfiliado, FechaAfiliacion, idUsuario) " +
                        "VALUES ('" + txbDocumento.Text.ToString() + "', " + ddlTipoDocumento.SelectedItem.Value.ToString() + ", " +
                        "'" + txbNombre.Text.ToString() + "', '" + txbApellido.Text.ToString() + "', " +
                        "'" + txbTelefono.Text.ToString() + "', '" + txbEmail.Text.ToString() + "', '" + strClave + "', " +
                        "'" + txbDireccion.Text.ToString() + "', " + ddlCiudadAfiliado.SelectedItem.Value.ToString() + ", " +
                        "'" + txbFechaNac.Text.ToString() + "', '" + strFilename + "', " +
                        "" + ddlGenero.SelectedItem.Value.ToString() + ", " + ddlEstadoCivil.SelectedItem.Value.ToString() + ", " +
                        "" + ddlEmpresaConvenio.SelectedItem.Value.ToString() + ", " +
                        "" + ddlProfesiones.SelectedItem.Value.ToString() + ", " + ddlEps.SelectedItem.Value.ToString() + ", " +
                        "" + ddlSedes.SelectedItem.Value.ToString() + ", '" + txbResponsable.Text.ToString() + "', " +
                        "'" + ddlParentesco.SelectedItem.Value.ToString() + "', '" + txbTelefonoContacto.Text.ToString() + "', " +
                        "'Pendiente', CURDATE(), " + Session["idusuario"].ToString() + ") ";

                        cg.TraerDatosStr(strQuery);
                        
                        cg.InsertarLog(Session["idusuario"].ToString(), "afiliados", "Nuevo", "El usuario creó un nuevo afiliado con documento: " + txbDocumento.Text.ToString() + ".", "", "");

                        DataTable dt = cg.TraerDatos("SELECT idAfiliado FROM Afiliados WHERE DocumentoAfiliado = '" + txbDocumento.Text.ToString() + "' ");

                        string strMensaje = "Bienvenido a Fitness People \r\n\r\n";
                        strMensaje += "Se ha registrado como afiliado en Fitness People. Por favor, agradecemos confirme sus datos a través de este enlace: \r\n";
                        strMensaje += "https://fitnesspeoplecolombia.com/verificacion?id=" + dt.Rows[0]["idAfiliado"].ToString();

                        cg.EnviarCorreo("afiliaciones@fitnesspeoplecolombia.com", txbEmail.Text.ToString(), "Nuevo registro en Fitness People", strMensaje);

                        Response.Redirect("afiliados");
                    }
                }
            }
        }
    }
}