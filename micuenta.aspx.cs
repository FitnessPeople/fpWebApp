using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;

namespace fpWebApp
{
    public partial class micuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ltNombreUsuario.Text = Session["NombreUsuario"].ToString();
                    ltCargo.Text = Session["CargoUsuario"].ToString();
                    //ltFoto.Text = "<img src=\"img/empleados/" + Session["Foto"].ToString() + "\" class=\"img-circle circle-border m-b-md\" alt=\"profile\">";

                    if (Session["Foto"].ToString() != "")
                    {
                        ltFoto.Text = "<img src=\"img/empleados/" + Session["Foto"].ToString() + "\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\" />";
                    }
                    else
                    {
                        ltFoto.Text = "<img src=\"img/empleados/nofoto.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\" />";
                    }

                    txbDocumento.Attributes.Add("type", "number");
                    txbTelefono.Attributes.Add("type", "number");
                    txbFechaNac.Attributes.Add("type", "date");
                    txbEmail.Attributes.Add("type", "email");
                    CargarTipoDocumento();
                    CargarCiudad();
                    CargarSedes();
                    CargarEps();
                    CargarFondoPension();
                    CargarArl();
                    CargarCajaComp();
                    CargarCesantias();
                    CargarCargos();
                    CargarProfesiones();
                    CargarEstadoCivil();
                    CargarGeneros();
                    CargarEmpleado();
                }
            }
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
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarEpss();
            ddlEps.DataSource = dt;
            ddlEps.DataBind();
            dt.Dispose();
        }

        private void CargarFondoPension()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarPensiones();
            ddlFondoPension.DataSource = dt;
            ddlFondoPension.DataBind();
            dt.Dispose();
        }

        private void CargarArl()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarArls();
            ddlArl.DataSource = dt;
            ddlArl.DataBind();
            dt.Dispose();
        }

        private void CargarCajaComp()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCajasComp();

            ddlCajaComp.DataSource = dt;
            ddlCajaComp.DataBind();

            dt.Dispose();
        }

        private void CargarCesantias()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarCesantias();
            ddlCesantias.DataSource = dt;
            ddlCesantias.DataBind();
            dt.Dispose();
        }

        //private void CargarCanalesVenta()
        //{
        //    clasesglobales cg1 = new clasesglobales();
        //    DataTable dt = cg1.ConsultarCanalesVenta();
        //    ddlCanalVenta.DataSource = dt;
        //    ddlCanalVenta.DataBind();
        //    dt.Dispose();
        //}

        private void CargarCargos()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarCargos();
            ddlCargo.DataSource = dt;
            ddlCargo.DataBind();
            dt.Dispose();
        }

        private void CargarProfesiones()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarProfesiones();
            ddlProfesion.DataSource = dt;
            ddlProfesion.DataBind();
            dt.Dispose();
        }

        //private void CargarEmpresasFP()
        //{
        //    clasesglobales cg1 = new clasesglobales();
        //    DataTable dt = cg1.ConsultarEmpresasFP();
        //    ddlEmpresasFP.DataSource = dt;
        //    ddlEmpresasFP.DataBind();
        //    dt.Dispose();
        //}

        private void CargarEstadoCivil()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarEstadosCiviles();
            ddlEstadoCivil.DataSource = dt;
            ddlEstadoCivil.DataBind();
            dt.Dispose();
        }

        private void CargarGeneros()
        {
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ConsultarGeneros();
            ddlGenero.DataSource = dt;
            ddlGenero.DataBind();
            dt.Dispose();
        }


        private void CargarEmpleado()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.CargarEmpleados(Session["idEmpleado"].ToString());
            txbDocumento.Text = dt.Rows[0]["DocumentoEmpleado"].ToString();

            if (ConsultarSiActualizo(Session["idEmpleado"].ToString()))
            {
                if (Request.QueryString.Count == 0)
                {
                    Response.Redirect("inicio");
                }
            }

            if (dt.Rows[0]["idTipoDocumento"].ToString() != "")
            {
                ddlTipoDocumento.SelectedIndex = Convert.ToInt16(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt.Rows[0]["idTipoDocumento"].ToString())));
            }
            else
            {
                ddlTipoDocumento.SelectedItem.Value = "0";
            }

            txbNombre.Text = dt.Rows[0]["NombreEmpleado"].ToString();
            txbTelefono.Text = dt.Rows[0]["TelefonoEmpleado"].ToString();
            txbTelefonoCorp.Text = dt.Rows[0]["TelefonoCorporativo"].ToString();
            txbEmail.Text = dt.Rows[0]["EmailEmpleado"].ToString();
            txbEmailCorp.Text = dt.Rows[0]["EmailCorporativo"].ToString();
            txbDireccion.Text = dt.Rows[0]["DireccionEmpleado"].ToString();
            if (dt.Rows[0]["idCiudadEmpleado"].ToString() != "")
            {
                ddlCiudadEmpleado.SelectedIndex = Convert.ToInt16(ddlCiudadEmpleado.Items.IndexOf(ddlCiudadEmpleado.Items.FindByText(dt.Rows[0]["idCiudadEmpleado"].ToString())));
            }
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

            if (dt.Rows[0]["NivelEstudio"].ToString() != "")
            {
                ddlNivelEstudio.SelectedIndex = Convert.ToInt16(ddlNivelEstudio.Items.IndexOf(ddlNivelEstudio.Items.FindByText(dt.Rows[0]["NivelEstudio"].ToString())));
            }

            txbEstratoSocioeconomico.Text = dt.Rows[0]["EstratoSocioeconomico"].ToString();
            if (dt.Rows[0]["TipoVivienda"].ToString() != "")
            {
                ddlTipoVivienda.SelectedIndex = Convert.ToInt16(ddlTipoVivienda.Items.IndexOf(ddlTipoVivienda.Items.FindByText(dt.Rows[0]["TipoVivienda"].ToString())));
            }
            txbNroPersonasNucleo.Text = dt.Rows[0]["PersonasNucleoFamiliar"].ToString();
            if (dt.Rows[0]["ActividadExtra"].ToString() != "")
            {
                ddlActividadExtra.SelectedIndex = Convert.ToInt16(ddlActividadExtra.Items.IndexOf(ddlActividadExtra.Items.FindByText(dt.Rows[0]["ActividadExtra"].ToString())));
            }
            if (dt.Rows[0]["ConsumeLicor"].ToString() != "")
            {
                ddlConsumoLicor.SelectedIndex = Convert.ToInt16(ddlConsumoLicor.Items.IndexOf(ddlConsumoLicor.Items.FindByText(dt.Rows[0]["ConsumeLicor"].ToString())));
            }
            if (dt.Rows[0]["MedioTransporte"].ToString() != "")
            {
                ddlMedioTransporte.SelectedIndex = Convert.ToInt16(ddlMedioTransporte.Items.IndexOf(ddlMedioTransporte.Items.FindByText(dt.Rows[0]["MedioTransporte"].ToString())));
            }

            ddlSedes.SelectedIndex = Convert.ToInt32(ddlSedes.Items.IndexOf(ddlSedes.Items.FindByValue(dt.Rows[0]["idSede"].ToString())));
            
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

            if (dt.Rows[0]["idEstadoCivil"].ToString() != "")
            {
                ddlEstadoCivil.SelectedIndex = Convert.ToInt16(ddlEstadoCivil.Items.IndexOf(ddlEstadoCivil.Items.FindByValue(dt.Rows[0]["idEstadoCivil"].ToString())));
            }

            if (dt.Rows[0]["idGenero"].ToString() != "")
            {
                ddlGenero.SelectedIndex = Convert.ToInt16(ddlGenero.Items.IndexOf(ddlGenero.Items.FindByValue(dt.Rows[0]["idGenero"].ToString())));
            }

            if (dt.Rows[0]["idCargo"].ToString() != "")
            {
                ddlCargo.SelectedIndex = Convert.ToInt32(ddlCargo.Items.IndexOf(ddlCargo.Items.FindByValue(dt.Rows[0]["idCargo"].ToString())));
            }

            if (dt.Rows[0]["idProfesion"].ToString() != "")
            {
                ddlProfesion.SelectedIndex = Convert.ToInt32(ddlProfesion.Items.IndexOf(ddlProfesion.Items.FindByValue(dt.Rows[0]["idProfesion"].ToString())));
            }

            if (dt.Rows[0]["TipoSangre"].ToString() != "")
            {
                ddlTipoSangre.SelectedIndex = Convert.ToInt16(ddlTipoSangre.Items.IndexOf(ddlTipoSangre.Items.FindByText(dt.Rows[0]["TipoSangre"].ToString())));
            }

            ltFotoEmpleado.Text = "<img src=\"img/empleados/" + dt.Rows[0]["FotoEmpleado"].ToString() + "\" class=\"img-circle circle-border m-b-md\" width=\"220px\" alt=\"profile\" />";

            ViewState["FotoEmpleado"] = dt.Rows[0]["FotoEmpleado"].ToString();

            dt.Dispose();
        }

        private bool ConsultarSiActualizo(string idEmpleado)
        {
            bool actualizo = false;

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.CargarEmpleados(idEmpleado);

            if (dt.Rows[0]["FechaNacEmpleado"].ToString() != "" && dt.Rows[0]["idSede"].ToString() != "" && dt.Rows[0]["idEps"].ToString() != "" && dt.Rows[0]["idFondoPension"].ToString() != "" && dt.Rows[0]["idEstadoCivil"].ToString() != "")
            {
                actualizo = true;
            }

            return actualizo;
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
                string nuevoNombre = txbDocumento.Text.ToString()  + ".jpg";
                string rutaServidor = Server.MapPath("~/img/empleados/" + nuevoNombre);

                string filePath = rutaServidor;
                postedFile.SaveAs(filePath);
                strFilename = nuevoNombre;
            }

            string strInitData = TraerData();
            try
            {
                clasesglobales cg = new clasesglobales();
                string strHashClave = cg.ComputeSha256Hash(txbClave.Text.ToString());

                string mensaje = cg.ActualizarEmpleadoNuevo(txbDocumento.Text.ToString(), 
                    Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value.ToString()),
                    txbNombre.Text.ToString(), 
                    txbTelefono.Text.ToString(), 
                    txbTelefonoCorp.Text.ToString(),
                    txbEmail.Text.ToString(), 
                    txbEmailCorp.Text.ToString(), 
                    txbDireccion.Text.ToString(),
                    Convert.ToInt32(ddlCiudadEmpleado.SelectedItem.Value.ToString()), 
                    txbFechaNac.Text.ToString(), 
                    strFilename, 
                    Convert.ToInt32(ddlSedes.SelectedItem.Value.ToString()), 
                    Convert.ToInt32(ddlEps.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlFondoPension.SelectedItem.Value.ToString()), 
                    Convert.ToInt32(ddlArl.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlCajaComp.SelectedItem.Value.ToString()), 
                    Convert.ToInt32(ddlCesantias.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlGenero.SelectedItem.Value.ToString()), 
                    Convert.ToInt32(ddlEstadoCivil.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlCargo.SelectedItem.Value.ToString()), 
                    strHashClave, 
                    Convert.ToInt32(ddlProfesion.SelectedItem.Value.ToString()),
                    ddlNivelEstudio.SelectedItem.Value.ToString(),
                    Convert.ToInt32(txbEstratoSocioeconomico.Text.ToString()),
                    ddlTipoVivienda.SelectedItem.Value.ToString(),
                    Convert.ToInt32(txbNroPersonasNucleo.Text.ToString()),
                    ddlActividadExtra.SelectedItem.Value.ToString(),
                    ddlConsumoLicor.SelectedItem.Value.ToString(),
                    ddlMedioTransporte.SelectedItem.Value.ToString(),
                    ddlTipoSangre.SelectedItem.Value.ToString());

                string strNewData = TraerData();

                if (mensaje == "OK")
                {
                    cg.InsertarLog(Session["idusuario"].ToString(), "Empleados, Usuarios", "Modifica", "El usuario actualizó sus datos por primera vez (documento " + txbDocumento.Text.ToString() + ").", strInitData, strNewData);

                    string script = @"
                        Swal.fire({
                            title: 'El empleado se actualizó de forma exitosa',
                            text: 'Texto.',
                            icon: 'success',
                            timer: 3000, // 3 segundos
                            showConfirmButton: false,
                            timerProgressBar: true
                        }).then(() => {
                            window.location.href = 'inicio';
                        });
                        ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                }
                else
                {
                    string script = @"
                        Swal.fire({
                            title: 'Error',
                            text: '" + mensaje.Replace("'", "\\'") + @"',
                            icon: 'error'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                            
                            }
                        });
                        ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                }

            }
            catch (SqlException ex)
            {
                string script = @"
                    Swal.fire({
                        title: 'Error',
                        text: 'Ha ocurrido un error inesperado. " + ex.Message.ToString() + @"',
                        icon: 'error'
                    }).then(() => {
                        window.location.href = 'inicio';
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
            }
        }

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEmpleado(Session["idEmpleado"].ToString());

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