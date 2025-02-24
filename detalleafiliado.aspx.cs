using MySqlX.XDevAPI.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class detalleafiliado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Detalle afiliado");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        divContenido.Visible = true;
                        string[] strDocumento = Request.QueryString["top-search"].ToString().Split('-');
                        MostrarDatosAfiliado(strDocumento[0].Trim());
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {

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

        private void MostrarDatosAfiliado(string strDocumento)
        {
            string strQuery = "SELECT * FROM Afiliados a " +
                "RIGHT JOIN Sedes s ON a.idSede = s.idSede " +
                "LEFT JOIN ciudades ON ciudades.idCiudad = a.idCiudadAfiliado " +
                "WHERE DocumentoAfiliado = '" + strDocumento + "' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            CargarPlanesAfiliado(dt.Rows[0]["idAfiliado"].ToString());

            ltNombre.Text = dt.Rows[0]["NombreAfiliado"].ToString();
            ltApellido.Text = dt.Rows[0]["ApellidoAfiliado"].ToString();
            ltEmail.Text = dt.Rows[0]["EmailAfiliado"].ToString();
            ltCelular.Text = dt.Rows[0]["CelularAfiliado"].ToString();
            ltSede.Text = dt.Rows[0]["NombreSede"].ToString();
            ltDireccion.Text = dt.Rows[0]["DireccionAfiliado"].ToString();
            ltCiudad.Text = dt.Rows[0]["NombreCiudad"].ToString();
            ltCumple.Text = String.Format("{0:dd MMM}", Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"]));
            ltEstado.Text = dt.Rows[0]["EstadoAfiliado"].ToString();
            //ltFoto.Text = "<img src=\"img/afiliados/nofoto.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";

            if (dt.Rows[0]["FotoAfiliado"].ToString() != "")
            {
                ltFoto.Text = "<img src=\"img/afiliados/" + dt.Rows[0]["FotoAfiliado"].ToString() + "\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
            }
            else
            {
                if (dt.Rows[0]["idGenero"].ToString() == "1" || dt.Rows[0]["idGenero"].ToString() == "3")
                {
                    ltFoto.Text = "<img src=\"img/afiliados/avatar_male.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                }
                if (dt.Rows[0]["idGenero"].ToString() == "2")
                {
                    ltFoto.Text = "<img src=\"img/afiliados/avatar_female.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                }
            }

            string url = "https://aone.armaturacolombia.co/api/person/get/" + strDocumento + "?access_token=D2BCF6E6BD09DECAA1266D9F684FFE3F5310AD447D107A29974F71E1989AABDB";
            string respuesta = EnviarPeticionGet(url);
            ltMensaje.Text = respuesta;
        }

        private static string EnviarPeticionGet(string url)
        {
            string resultado = "";
            string resultadoj = "";
            try
            {
                WebRequest oRequest = WebRequest.Create(url);
                oRequest.Method = "GET";
                oRequest.ContentType = "application/json;charset=UTF-8";

                WebResponse oResponse = oRequest.GetResponse();
                using (var oSr = new StreamReader(oResponse.GetResponseStream()))
                {
                    resultado = oSr.ReadToEnd().Trim();
                    JObject jsonObj = JObject.Parse(resultado);
                    resultadoj = jsonObj["message"].ToString();
                }

                return resultadoj;
            }
            catch (Exception ex)
            {
                return "Error al enviar la petición: " + ex.Message;
            }
        }

        private void CargarPlanesAfiliado(string strIdAfiliado)
        {
            string strQuery = "SELECT *, " +
                "DATEDIFF(FechaFinalPlan, CURDATE()) AS diasquefaltan, " +
                "DATEDIFF(CURDATE(), FechaInicioPlan) AS diasconsumidos, " +
                "DATEDIFF(FechaFinalPlan, FechaInicioPlan) AS diastotales, " +
                "ROUND(DATEDIFF(CURDATE(), FechaInicioPlan) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje1, " +
                "ROUND(DATEDIFF(FechaFinalPlan, CURDATE()) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje2 " +
                "FROM afiliadosPlanes ap, Afiliados a, Planes p " +
                "WHERE a.idAfiliado = " + strIdAfiliado + " " +
                "AND ap.idAfiliado = a.idAfiliado " +
                "AND ap.idPlan = p.idPlan " +
                "AND ap.EstadoPlan = 'Activo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                rpPlanesAfiliado.DataSource = dt;
                rpPlanesAfiliado.DataBind();
            }
            dt.Dispose();
        }
    }
}