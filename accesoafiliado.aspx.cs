using NPOI.OpenXmlFormats.Spreadsheet;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class accesoafiliado : System.Web.UI.Page
    {
        private SerialController serialController;
        protected void Page_Load(object sender, EventArgs e)
        {
            //InitializeComponent();
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

                        clasesglobales cg = new clasesglobales();
                        string strQuery = @"SELECT * 
                            FROM AccesoAfiliado aa, Afiliados a, Sedes s 
                            WHERE aa.idAfiliado = a.idAfiliado 
                            AND aa.idSede = s.idSede ";
                        DataTable dt = cg.TraerDatos(strQuery);

                        rpAccesoAfiliados.DataSource = dt;
                        rpAccesoAfiliados.DataBind();

                        //txbTelefono.Attributes.Add("type", "number");
                        //txbFechaNac.Attributes.Add("type", "date");
                        //txbTelefonoContacto.Attributes.Add("type", "number");
                        //txbEmail.Attributes.Add("type", "email");
                        //CargarTipoDocumento();
                        //CargarCiudad();
                        //CargarEmpresas();
                        //CargarEstadoCivil();
                        //CargarEps();
                        //CargarProfesiones();
                        //CargarSedes();
                        //CargarGeneros();
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

        protected void txbDocumento_TextChanged(object sender, EventArgs e)
        {
            ltMensaje.Text = string.Empty;
            string strQuery = @"SELECT * 
                FROM Afiliados a, AfiliadosPlanes ap 
                WHERE DocumentoAfiliado = '" + txbDocumento.Text.ToString() + @"' 
                AND ap.idAfiliado = a.idAfiliado 
                AND ap.EstadoPlan = 'Activo' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                //Cargar Datos del Afiliado
                //txbNombre.Text = dt.Rows[0]["NombreAfiliado"].ToString();

                //Cargar Planes del Afiliado
                serialController = new SerialController("COM1"); // Reemplaza con tu puerto
                serialController.Open();

                serialController.SendData("ON");

                serialController.Close();

                strQuery = @"INSERT INTO AccesoAfiliado 
                    (idAfiliado, idSede, FechaHoraIngreso) 
                    VALUES (" + dt.Rows[0]["idAfiliado"].ToString() + ", 1, NOW())";
                cg.TraerDatosStr(strQuery);

                string strNombre = dt.Rows[0]["NombreAfiliado"].ToString();
                string strApellido = dt.Rows[0]["ApellidoAfiliado"].ToString();
                string strDocumento = dt.Rows[0]["DocumentoAfiliado"].ToString();

                string strDatosAfiliado = @"<h1><b>" + strNombre + " " + strApellido + @"</b><br />Nro. de Documento: " + strDocumento + @"</h1>";

                string script = @"
                    Swal.fire({
                        title: 'Acceso permitido a: " + strDatosAfiliado + @"',
                        text: '',
                        icon: 'success',
                        timer: 5000, // 5 segundos
                        showConfirmButton: false,
                        timerProgressBar: true
                    }).then(() => {
                        window.location.href = 'accesoafiliado';
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
            }
            else
            {
                string script = @"
                    Swal.fire({
                        title: 'El afiliado no existe.',
                        text: '',
                        icon: 'error',
                        timer: 3000, // 3 segundos
                        showConfirmButton: false,
                        backdrop: `
                            rgba(71,80,100,0.8)
                            left top
                            no-repeat
                          `,
                        timerProgressBar: true
                    }).then(() => {
                        window.location.href = 'accesoafiliado';
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
            }
        }

        public class SerialController
        {
            private SerialPort arduinoPort;

            public SerialController(string portName)
            {
                arduinoPort = new SerialPort(portName, 9600);
                arduinoPort.ReadTimeout = 2000;
                arduinoPort.WriteTimeout = 2000;
            }

            public void Open()
            {
                try
                {
                    arduinoPort.Open();
                }
                catch (Exception ex)
                {
                    string strMessage = "Error al abrir el puerto: " + ex.Message;
                }
            }

            public void Close()
            {
                if (arduinoPort.IsOpen)
                {
                    arduinoPort.Close();
                }
            }

            public void SendData(string data)
            {
                try
                {
                    if (arduinoPort.IsOpen)
                    {
                        arduinoPort.WriteLine(data);
                    }
                }
                catch (Exception ex)
                {
                    string strMessage = "Error al enviar datos: " + ex.Message;
                }
            }
        }
    }
}