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
    public partial class nuevoempleado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Empleados");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        txbDocumento.Attributes.Add("type", "number");
                        txbTelefono.Attributes.Add("type", "number");
                        txbFechaNac.Attributes.Add("type", "date");
                        txbFechaInicio.Attributes.Add("type", "date");
                        txbFechaFinal.Attributes.Add("type", "date");

                        DateTime dt14 = DateTime.Now.AddYears(-14);
                        DateTime dt80 = DateTime.Now.AddYears(-80);
                        txbFechaNac.Attributes.Add("min", dt80.Year.ToString() + "-" + String.Format("{0:MM}", dt80) + "-" + String.Format("{0:dd}", dt80));
                        txbFechaNac.Attributes.Add("max", dt14.Year.ToString() + "-" + String.Format("{0:MM}", dt14) + "-" + String.Format("{0:dd}", dt14));

                        CargarTipoDocumento();
                        CargarCiudad();
                        CargarSedes();
                        CargarEps();
                        CargarFondoPension();
                        CargarArl();
                        CargarCajaComp();
                        CargarCesantias();
                        CargarEmpresasFP();
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
            string strQuery = "SELECT * FROM tiposDocumento";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

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

            ddlCiudadEmpleado.DataSource = dt;
            ddlCiudadEmpleado.DataBind();

            dt.Dispose();
        }

        private void CargarSedes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSedes("Todos");

            ddlSedes.DataSource = dt;
            ddlSedes.DataBind();

            dt.Dispose();
        }

        private void CargarEps()
        {
            string strQuery = "SELECT * FROM eps";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlEps.DataSource = dt;
            ddlEps.DataBind();

            dt.Dispose();
        }

        private void CargarFondoPension()
        {
            string strQuery = "SELECT * FROM fondospension";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlFondoPension.DataSource = dt;
            ddlFondoPension.DataBind();

            dt.Dispose();
        }

        private void CargarArl()
        {
            string strQuery = "SELECT * FROM arl";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlArl.DataSource = dt;
            ddlArl.DataBind();

            dt.Dispose();
        }

        private void CargarCajaComp()
        {
            string strQuery = "SELECT * FROM cajascompensacion";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlCajaComp.DataSource = dt;
            ddlCajaComp.DataBind();

            dt.Dispose();
        }

        private void CargarCesantias()
        {
            string strQuery = "SELECT * FROM cesantias";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlCesantias.DataSource = dt;
            ddlCesantias.DataBind();

            dt.Dispose();
        }

        private void CargarEmpresasFP()
        {
            string strQuery = "SELECT * FROM empresasfp";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            //ddlCesantias.DataSource = dt;
            //ddlCesantias.DataBind();

            dt.Dispose();
        }

        private bool ExisteDocumento(string strDocumento)
        {
            bool rta = false;
            string strQuery = "SELECT DocumentoEmpleado FROM Empleados WHERE DocumentoEmpleado = '" + strDocumento + "' ";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

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
            string strQuery = "SELECT DocumentoEmpleado FROM Empleados WHERE EmailEmpleado = '" + strEmail + "' ";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

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
            string strQuery = "SELECT DocumentoEmpleado FROM Empleados WHERE TelefonoEmpleado = '" + strTelefono + "' ";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

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
                        string strFilename = "";
                        HttpPostedFile postedFile = Request.Files["fileFoto"];

                        if (postedFile != null && postedFile.ContentLength > 0)
                        {
                            //Save the File.
                            string filePath = Server.MapPath("img//empleados//") + Path.GetFileName(postedFile.FileName);
                            postedFile.SaveAs(filePath);
                            strFilename = postedFile.FileName;
                        }

                        OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
                        try
                        {
                            string strQuery = "INSERT INTO empleados " +
                            "(DocumentoEmpleado, idTipoDocumento, NombreEmpleado, TelefonoEmpleado, EmailEmpleado, " +
                            "DireccionEmpleado, idCiudadEmpleado, CargoEmpleado, FechaNacEmpleado, FotoEmpleado, NroContrato, " +
                            "TipoContrato, idSede, FechaInicio, FechaFinal, Sueldo, GrupoNomina, idEPS, idFondoPension, idARL, " +
                            "idCajaComp, idCesantias, Estado) " +
                            "VALUES ('" + txbDocumento.Text.ToString() + "', " + ddlTipoDocumento.SelectedItem.Value.ToString() + ", " +
                            "'" + txbNombre.Text.ToString() + "', '" + txbTelefono.Text.ToString() + "', " +
                            "'" + txbEmail.Text.ToString() + "', '" + txbDireccion.Text.ToString() + "', " +
                            "" + ddlCiudadEmpleado.SelectedItem.Value.ToString() + ", '" + txbCargo.Text.ToString() + "', " +
                            "'" + txbFechaNac.Text.ToString() + "'" +
                            "'" + strFilename + "', '" + txbContrato.Text.ToString() + "', " +
                            "'" + ddlTipoContrato.SelectedItem.Value.ToString() + "', " +
                            "" + ddlSedes.SelectedItem.Value.ToString() + ", '" + txbFechaInicio.Text.ToString() + "', " +
                            "'" + txbFechaFinal.Text.ToString() + "', '" + txbSueldo.Text.ToString() + "', " +
                            "'" + ddlGrupo.SelectedItem.Value.ToString() + "', " + ddlEps.SelectedItem.Value.ToString() + ", " +
                            "" + ddlFondoPension.SelectedItem.Value.ToString() + ", " + ddlArl.SelectedItem.Value.ToString() + ", " +
                            "" + ddlCajaComp.SelectedItem.Value.ToString() + ", " + ddlCesantias.SelectedItem.Value.ToString() + ", " +
                            "'Activo') ";
                            OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                            myConnection.Open();
                            command.ExecuteNonQuery();
                            command.Dispose();
                            myConnection.Close();

                            clasesglobales cg = new clasesglobales();
                            cg.InsertarLog(Session["idusuario"].ToString(), "Empleados", "Nuevo registro", "El usuario agregó un nuevo empleado con documento " + txbDocumento.Text.ToString() + ".", "", "");

                            Response.Redirect("empleados");
                        }
                        catch (OdbcException ex)
                        {
                            ltMensaje.Text = "<div class=\"ibox-content\">" +
                                "<div class=\"alert alert-danger alert-dismissable\">" +
                                "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" + ex.Message +
                                "</div></div>";
                            myConnection.Close();
                        }
                    }
                }
            }
        }
    }
}