using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO.Ports;
using Org.BouncyCastle.Asn1.Ocsp;

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
                        txbTelefono.Attributes.Add("type", "number");
                        txbFechaNac.Attributes.Add("type", "date");
                        txbTelefonoContacto.Attributes.Add("type", "number");
                        txbEmail.Attributes.Add("type", "email");
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
            string strQuery = "SELECT * FROM afiliados WHERE DocumentoAfiliado = '" + txbDocumento.Text.ToString() + "' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                //Cargar Datos del Afiliado
                txbNombre.Text = dt.Rows[0]["NombreAfiliado"].ToString();

                //Cargar Planes del Afiliado
                serialController = new SerialController("COM1"); // Reemplaza con tu puerto
                serialController.Open();

                serialController.SendData("ON");

                serialController.Close();
            }
            else
            {
                ltMensaje.Text = "El afiliado no existe.";
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