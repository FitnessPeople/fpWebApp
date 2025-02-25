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
    public partial class nuevoespecialista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Especialistas");
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
                        txbFechaNac.Attributes.Add("min", dt100.Year.ToString() + "-0" + dt100.Month.ToString() + "-" + String.Format("{0:dd}", dt100));
                        txbFechaNac.Attributes.Add("max", dt14.Year.ToString() + "-0" + dt14.Month.ToString() + "-" + String.Format("{0:dd}", dt14));
                        txbDocumento.Attributes.Add("type", "number");
                        txbTelefono.Attributes.Add("type", "number");
                        txbFechaNac.Attributes.Add("type", "date");
                        CargarTipoDocumento();
                        CargarCiudad();
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
            string strQuery = "SELECT idCiudad, CONCAT(NombreCiudad, ' - ', NombreEstado) AS NombreCiudad FROM Ciudades " +
                "WHERE CodigoPais = 'Co' " +
                "ORDER BY NombreCiudad";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlCiudadEspecialista.DataSource = dt;
            ddlCiudadEspecialista.DataBind();

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
            string strQuery = "SELECT * FROM profesiones WHERE area = 'Salud'";
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
            string strQuery = "SELECT DocumentoEspecialista FROM Especialistas WHERE DocumentoEspecialista = '" + strDocumento + "' ";
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
            string strQuery = "SELECT DocumentoEspecialista FROM Especialistas WHERE EmailEspecialista = '" + strEmail + "' ";
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
            string strQuery = "SELECT DocumentoEspecialista FROM Especialistas WHERE CelularEspecialista = '" + strTelefono + "' ";
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
                divMensaje1.Visible = true;
            }
            else
            {
                if (ExisteEmail(txbEmail.Text.ToString().Trim()))
                {
                    divMensaje2.Visible = true;
                }
                else
                {
                    if (ExisteTelefono(txbTelefono.Text.ToString().Trim()))
                    {
                        divMensaje3.Visible = true;
                    }
                    else
                    {
                        
                        // Inserta en la tabla Especialistas
                        string strFilename = "nofoto.png";
                        HttpPostedFile postedFile = Request.Files["fileFoto"];

                        if (postedFile != null && postedFile.ContentLength > 0)
                        {
                            //Save the File.
                            string filePath = Server.MapPath("img//especialistas//") + Path.GetFileName(postedFile.FileName);
                            postedFile.SaveAs(filePath);
                            strFilename = postedFile.FileName;
                        }

                        try
                        {
                            string strQuery = "INSERT INTO Especialistas " +
                            "(DocumentoEspecialista, idTipoDocumento, NombreEspecialista, ApellidoEspecialista, CelularEspecialista, EmailEspecialista, " +
                            "DireccionEspecialista, idCiudadEspecialista, FechaNacEspecialista, FotoEspecialista, idGenero, idEstadoCivilEspecialista, idProfesion, " +
                            "idEps, idSede, EstadoEspecialista) " +
                            "VALUES ('" + txbDocumento.Text.ToString() + "', " + ddlTipoDocumento.SelectedItem.Value.ToString() + ", " +
                            "'" + txbNombre.Text.ToString() + "', '" + txbApellido.Text.ToString() + "', " +
                            "'" + txbTelefono.Text.ToString() + "', '" + txbEmail.Text.ToString() + "', " +
                            "'" + txbDireccion.Text.ToString() + "', " + ddlCiudadEspecialista.SelectedItem.Value.ToString() + ", " +
                            "'" + txbFechaNac.Text.ToString() + "', '" + strFilename + "', " +
                            "" + ddlGenero.SelectedItem.Value.ToString() + ", " + ddlEstadoCivil.SelectedItem.Value.ToString() + ", " +
                            "" + ddlProfesiones.SelectedItem.Value.ToString() + ", " + ddlEps.SelectedItem.Value.ToString() + ", " +
                            "" + ddlSedes.SelectedItem.Value.ToString() + ", 'Activo') ";
                            clasesglobales cg = new clasesglobales();
                            string mensaje = cg.TraerDatosStr(strQuery);
                        }
                        catch (OdbcException ex)
                        {
                            string mensaje = ex.Message;
                        }

                        Response.Redirect("especialistas");
                    }
                }
            }
        }
    }
}