using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

            string strQuery = "SELECT SinPermiso, Consulta, Exportar, CrearModificar, Borrar " +
                "FROM permisos_perfiles pp, paginas p, usuarios u " +
                "WHERE pp.idPagina = p.idPagina " +
                "AND p.Pagina = '" + strPagina + "' " +
                "AND pp.idPerfil = " + Session["idPerfil"].ToString() + " " +
                "AND u.idPerfil = pp.idPerfil " +
                "AND u.idUsuario = " + Session["idusuario"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

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
            string strQuery = "SELECT * FROM tiposDocumento";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlTipoDocumento.DataSource = dt;
            ddlTipoDocumento.DataBind();

            dt.Dispose();
        }

        private void CargarCiudad()
        {
            string strQuery = "SELECT idCiudad, CONCAT(NombreCiudad, ' - ', NombreEstado) AS NombreCiudad FROM Ciudades " +
                "WHERE CodigoPais = 'Co' " +
                "ORDER BY NombreCiudad";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

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
            string strQuery = "SELECT s.idSede, CONCAT(s.NombreSede, \" - \", cs.NombreCiudadSede) AS NombreSede " +
                "FROM sedes s " +
                "LEFT JOIN CiudadesSedes cs ON s.idCiudadSede = cs.idCiudadSede " +
                "ORDER BY s.idCiudadSede, NombreSede";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

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

                        OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
                        try
                        {
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
                            "'Pendiente', CURDATE(), "  + Session["idusuario"].ToString() + ") ";
                            OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                            myConnection.Open();
                            command.ExecuteNonQuery();
                            command.Dispose();
                            myConnection.Close();

                            cg.InsertarLog(Session["idusuario"].ToString(), "afiliados", "Nuevo", "El usuario creó un nuevo afiliado con documento " + txbDocumento.Text.ToString() + ".", "", "");

                            DataTable dt = cg.TraerDatos("SELECT idAfiliado FROM Afiliados WHERE DocumentoAfiliado = '" + txbDocumento.Text.ToString() + "' ");

                            string strMensaje = "Bienvenido a Fitness People \r\n\r\n";
                            strMensaje += "Se ha registrado como afiliado en Fitness People. Por favor, agradecemos confirme sus datos a través de este enlace: \r\n";
                            strMensaje += "https://fitnesspeoplecolombia.com/verificacion?id=" + dt.Rows[0]["idAfiliado"].ToString();

                            cg.EnviarCorreo("contabilidad@fitnesspeoplecmd.com", txbEmail.Text.ToString(), "Nuevo registro en Fitness People", strMensaje);
                        }
                        catch (OdbcException ex)
                        {
                            string mensaje = ex.Message;
                            myConnection.Close();
                        }

                        Response.Redirect("afiliados");
                    }
                }
            }
        }
    }
}