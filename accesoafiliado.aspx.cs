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
                        ConsultarSedes();
                        ConsultarAccesos();
                        txbDocumento.Attributes.Add("type", "number");
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

        /// <summary>
        /// Consulta las sedes por ID y muestra el título con el nombre de la sede.
        /// Si la sede es la oficina, muestra el título "Todas las sedes"
        /// </summary>
        private void ConsultarSedes()
        {
            int idSedeUsuario = Convert.ToInt32(Session["idSede"]);

            clasesglobales cg = new clasesglobales();

            int? idSede = (idSedeUsuario == 11) ? (int?)null : idSedeUsuario;

            DataTable dt = cg.ConsultaCargarSedesPorId(idSede, "Gimnasio");

            if (idSedeUsuario == 11)
            {
                ltSede.Text = "todas las sedes.";
            }
            else
            {
                ltSede.Text = "Sede " + dt.Rows[0]["NombreSede"].ToString();
            }
        }

        private void ConsultarAccesos()
        {
            int idSedeUsuario = Convert.ToInt32(Session["idSede"]);
            string strQuery = string.Empty;
            clasesglobales cg = new clasesglobales();
            if (idSedeUsuario == 11)
            {
                strQuery = @"SELECT * 
                    FROM AccesoAfiliado aa, Afiliados a, Sedes s 
                    WHERE aa.idAfiliado = a.idAfiliado 
                    AND aa.idSede = s.idSede 
                    ORDER BY FechaHoraIngreso DESC";
            }
            else
            {
                strQuery = @"SELECT * 
                    FROM AccesoAfiliado aa, Afiliados a, Sedes s 
                    WHERE aa.idAfiliado = a.idAfiliado 
                    AND aa.idSede = s.idSede 
                    AND s.idSede = " + idSedeUsuario.ToString() + @"
                    ORDER BY FechaHoraIngreso DESC";
            }

            DataTable dt = cg.TraerDatos(strQuery);
            rpAccesoAfiliados.DataSource = dt;
            rpAccesoAfiliados.DataBind();
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

                string strDatosAfiliado = @"<h2><b>" + strNombre + " " + strApellido + @"</b><br />Nro. de Documento: " + strDocumento + @"</h2>";

                string script = @"
                    Swal.fire({
                        title: 'Acceso permitido a: " + strDatosAfiliado + @"',
                        text: 'Bienvenido a Fitness People',
                        width: 500,
                        background: '#1ab394',
                        color: '#fff',
                        timer: 5000, // 5 segundos
                        showConfirmButton: true,
                        imageUrl: 'img/logo_fp_white.svg',
                        imageWidth: 466,
                        imageHeight: 78,
                        imageAlt: 'Fondo',
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
                        title: 'Acceso denegado.',
                        text: 'Intente nuevamente',
                        width: 500,
                        background: '#ca5f59',
                        color: '#fff',
                        timer: 5000, // 5 segundos
                        showConfirmButton: true,
                        imageUrl: 'img/logo_fp_white.svg',
                        imageWidth: 466,
                        imageHeight: 78,
                        imageAlt: 'Fondo',
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