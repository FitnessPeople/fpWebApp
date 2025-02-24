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
    public partial class editarempleado : System.Web.UI.Page
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
                        CargarTipoDocumento();
                        CargarCiudad();
                        CargarSedes();
                        CargarEps();
                        CargarFondoPension();
                        CargarArl();
                        CargarCajaComp();
                        CargarCesantias();
                        CargarEmpresasFP();
                        CargarEmpleado();
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

        private void CargarEmpleado()
        {
            string strQuery = "SELECT * " +
                "FROM Empleados e " +
                "LEFT JOIN Ciudades c ON e.idCiudadEmpleado = c.idCiudad " +
                "WHERE e.DocumentoEmpleado = '" + Request.QueryString["editid"].ToString() + "' ";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            txbDocumento.Text = dt.Rows[0]["DocumentoEmpleado"].ToString();
            ddlTipoDocumento.SelectedIndex = Convert.ToInt16(dt.Rows[0]["idTipoDocumento"].ToString());
            txbNombre.Text = dt.Rows[0]["NombreEmpleado"].ToString();
            ltNombre.Text = dt.Rows[0]["NombreEmpleado"].ToString();
            txbTelefono.Text = dt.Rows[0]["TelefonoEmpleado"].ToString();
            ltTelefono.Text = dt.Rows[0]["TelefonoEmpleado"].ToString();
            txbEmail.Text = dt.Rows[0]["EmailEmpleado"].ToString();
            txbDireccion.Text = dt.Rows[0]["DireccionEmpleado"].ToString();
            ddlCiudadEmpleado.SelectedIndex = Convert.ToInt32(ddlCiudadEmpleado.Items.IndexOf(ddlCiudadEmpleado.Items.FindByValue(dt.Rows[0]["idCiudadEmpleado"].ToString())));
            DateTime dt14 = DateTime.Now.AddYears(-14);
            DateTime dt80 = DateTime.Now.AddYears(-80);
            txbFechaNac.Attributes.Add("min", dt80.Year.ToString() + "-" + String.Format("{0:MM}", dt80) + "-" + String.Format("{0:dd}", dt80));
            txbFechaNac.Attributes.Add("max", dt14.Year.ToString() + "-" + String.Format("{0:MM}", dt14) + "-" + String.Format("{0:dd}", dt14));

            DateTime dtFecha = new DateTime();
            if (dt.Rows[0]["FechaNacEmpleado"].ToString() != "")
            {
                dtFecha = Convert.ToDateTime(dt.Rows[0]["FechaNacEmpleado"].ToString());
            }

            txbFechaNac.Text = dtFecha.ToString("yyyy-MM-dd");
            txbCargo.Text = dt.Rows[0]["CargoEmpleado"].ToString();
            ltCargo.Text = dt.Rows[0]["CargoEmpleado"].ToString();
            ltCiudad.Text = dt.Rows[0]["NombreCiudad"].ToString();
            if (dt.Rows[0]["FotoEmpleado"].ToString() != "")
            {
                imgFoto.Src = "img/empleados/" + dt.Rows[0]["FotoEmpleado"].ToString();
                ViewState["FotoEmpleado"] = dt.Rows[0]["FotoEmpleado"].ToString();
            }
            txbContrato.Text = dt.Rows[0]["NroContrato"].ToString();
            if (dt.Rows[0]["TipoContrato"].ToString() != "")
            {
                ddlTipoContrato.SelectedIndex = Convert.ToInt16(ddlTipoContrato.Items.IndexOf(ddlTipoContrato.Items.FindByText(dt.Rows[0]["TipoContrato"].ToString())));
            }
            DateTime dtFechaIni = Convert.ToDateTime(dt.Rows[0]["FechaInicio"].ToString());
            txbFechaInicio.Text = dtFechaIni.ToString("yyyy-MM-dd");
            DateTime dtFechaFin = Convert.ToDateTime(dt.Rows[0]["FechaFinal"].ToString());
            txbFechaFinal.Text = dtFechaFin.ToString("yyyy-MM-dd");
            ddlSedes.SelectedIndex = Convert.ToInt32(ddlSedes.Items.IndexOf(ddlSedes.Items.FindByValue(dt.Rows[0]["idSede"].ToString())));
            txbSueldo.Text = dt.Rows[0]["Sueldo"].ToString();
            if (dt.Rows[0]["GrupoNomina"].ToString() != "")
            {
                ddlGrupo.SelectedIndex = Convert.ToInt16(ddlGrupo.Items.IndexOf(ddlGrupo.Items.FindByValue(dt.Rows[0]["GrupoNomina"].ToString())));
            }
            if (dt.Rows[0]["idEps"].ToString() != "")
            {
                ddlEps.SelectedIndex = Convert.ToInt16(ddlEps.Items.IndexOf(ddlEps.Items.FindByValue(dt.Rows[0]["idEps"].ToString())));
            }
            if (dt.Rows[0]["idFondoPension"].ToString() != "")
            {
                ddlFondoPension.SelectedIndex = Convert.ToInt16(ddlFondoPension.Items.IndexOf(ddlFondoPension.Items.FindByValue(dt.Rows[0]["idFondoPension"].ToString())));
            }
            if (dt.Rows[0]["idArl"].ToString() != "")
            {
                ddlArl.SelectedIndex = Convert.ToInt16(ddlArl.Items.IndexOf(ddlArl.Items.FindByValue(dt.Rows[0]["idArl"].ToString())));
            }
            if (dt.Rows[0]["idCajaComp"].ToString() != "")
            {
                ddlCajaComp.SelectedIndex = Convert.ToInt16(ddlCajaComp.Items.IndexOf(ddlCajaComp.Items.FindByValue(dt.Rows[0]["idCajaComp"].ToString())));
            }
            if (dt.Rows[0]["idCesantias"].ToString() != "")
            {
                ddlCesantias.SelectedIndex = Convert.ToInt16(ddlCesantias.Items.IndexOf(ddlCesantias.Items.FindByValue(dt.Rows[0]["idCesantias"].ToString())));
            }
            rblEstado.Items.FindByValue(dt.Rows[0]["Estado"].ToString()).Selected = true;

            dt.Dispose();
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

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            string strFilename = "";
            // Actualiza la tabla Empleados
            if (ViewState["FotoEmpleado"] != null)
            {
                strFilename = ViewState["FotoEmpleado"].ToString();
            }
            else
            {
                strFilename = "nofoto.png";
            }

            HttpPostedFile postedFile = Request.Files["fileFoto"];

            if (postedFile != null && postedFile.ContentLength > 0)
            {
                //Save the File.
                string filePath = Server.MapPath("img//afiliados//") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                strFilename = postedFile.FileName;
            }

            OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
            string strInitData = TraerData();
            try
            {
                string strQuery = "UPDATE empleados SET " +
                "idTipoDocumento = " + ddlTipoDocumento.SelectedItem.Value.ToString() + ", " +
                "NombreEmpleado = '" + txbNombre.Text.ToString().Replace("'","").Trim() + "', " +
                "TelefonoEmpleado = '" + txbTelefono.Text.ToString() + "', " +
                "EmailEmpleado = '" + txbEmail.Text.ToString() + "', " +
                "DireccionEmpleado = '" + txbDireccion.Text.ToString() + "', " +
                "idCiudadEmpleado = " + ddlCiudadEmpleado.SelectedItem.Value.ToString() + ", " +
                "CargoEmpleado = '" + txbCargo.Text.ToString() + "', " +
                "FechaNacEmpleado= '" + txbFechaNac.Text.ToString() + "', " +
                "FotoEmpleado = '" + strFilename + "', " +
                "NroContrato = '" + txbContrato.Text.ToString() + "', " +
                "TipoContrato = '" + ddlTipoContrato.SelectedItem.Value.ToString() + "', " +
                "idSede = " + ddlSedes.SelectedItem.Value.ToString() + ", " +
                "FechaInicio = '" + txbFechaInicio.Text.ToString() + "', " +
                "FechaFinal = '" + txbFechaFinal.Text.ToString() + "', " +
                "Sueldo = '" + txbSueldo.Text.ToString() + "', " +
                "GrupoNomina = '" + ddlGrupo.SelectedItem.Value.ToString() + "', " +
                "idEPS = " + ddlEps.SelectedItem.Value.ToString() + ", " +
                "idFondoPension = " + ddlFondoPension.SelectedItem.Value.ToString() + ", " +
                "idARL = " + ddlArl.SelectedItem.Value.ToString() + ", " +
                "idCajaComp = " + ddlCajaComp.SelectedItem.Value.ToString() + ", " +
                "idCesantias = " + ddlCesantias.SelectedItem.Value.ToString() + ", " +
                "Estado = '" + rblEstado.Text.ToString() + "' " +
                "WHERE DocumentoEmpleado = '" + txbDocumento.Text.ToString() + "' ";
                OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                myConnection.Open();
                command.ExecuteNonQuery();
                command.Dispose();
                myConnection.Close();

                if(rblEstado.Text.ToString() == "Inactivo")
                {
                    strQuery = "UPDATE Usuarios SET " +
                            "EstadoUsuario = 'Inactivo' " +
                            "WHERE idEmpleado = '" + txbDocumento.Text.ToString() + "' ";
                    OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                    myConnection.Open();
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    myConnection.Close();
                }

                string strNewData = TraerData();

                clasesglobales cg = new clasesglobales();
                cg.InsertarLog(Session["idusuario"].ToString(), "Empleados", "Modifica", "El usuario modificó datos al empleado con documento " + txbDocumento.Text.ToString() + ".", strInitData, strNewData);

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

        private string TraerData()
        {
            string strQuery = "SELECT e.DocumentoEmpleado, td.TipoDocumento, e.NombreEmpleado, e.TelefonoEmpleado, e.EmailEmpleado, " +
                "e.DireccionEmpleado, c.NombreCiudad, e.CargoEmpleado, e.FechaNacEmpleado, e.FotoEmpleado, e.NroContrato, e.TipoContrato, " +
                "s.NombreSede, e.FechaInicio, e.FechaFinal, e.Sueldo, e.GrupoNomina, eps.NombreEps, fp.NombreFondoPension, " +
                "arl.NombreArl, cp.NombreCajaComp, ces.NombreCesantias, e.Estado " +
                "FROM empleados e " +
                "LEFT JOIN Ciudades c ON e.idCiudadEmpleado = c.idCiudad " +
                "LEFT JOIN TiposDocumento td ON e.idTipoDocumento = td.idTipoDoc " +
                "LEFT JOIN Sedes s ON e.idSede = s.idSede " +
                "LEFT JOIN Eps ON e.idEps = eps.idEps " +
                "LEFT JOIN FondosPension fp ON e.idFondoPension = fp.idFondoPension " +
                "LEFT JOIN Arl ON e.idArl = arl.idArl " +
                "LEFT JOIN CajasCompensacion cp ON e.idCajaComp = cp.idCajaComp " +
                "LEFT JOIN Cesantias ces ON e.idCesantias = ces.idCesantias " +
                "WHERE e.DocumentoEmpleado = '" + Request.QueryString["editid"].ToString() + "' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

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