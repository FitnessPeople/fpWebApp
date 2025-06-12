using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class eliminarafiliado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("afiliados");
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
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            CargarAfiliado();
                            CargarPlanesAfiliado();
                            ViewState["respuesta"] = "";
                            CargarPreguntaConfirmacion();
                            if (ViewState["Borrar"].ToString() == "1")
                            {
                                btnEliminar.Visible = true;
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("logout.aspx");
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

        private void CargarAfiliado()
        {
            string strDocumento = Request.QueryString["deleteid"].ToString();
            string strQuery = "SELECT * FROM Afiliados a " +
                "RIGHT JOIN Sedes s ON a.idSede = s.idSede " +
                "WHERE idAfiliado = " + strDocumento + " ";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                ltNombreAfiliado.Text = dt.Rows[0]["NombreAfiliado"].ToString();
                ltApellidoAfiliado.Text = dt.Rows[0]["ApellidoAfiliado"].ToString();
                ltFotoAfiliado.Text = "<img alt=\"image\" class=\"img-circle circle-border m-b-md\" src=\"img/afiliados/" + dt.Rows[0]["FotoAfiliado"].ToString() + "\" width=\"120px\">";
                ltEmailAfiliado.Text = dt.Rows[0]["EmailAfiliado"].ToString();
                ltCelularAfiliado.Text = dt.Rows[0]["CelularAfiliado"].ToString();
                ltSedeAfiliado.Text = dt.Rows[0]["NombreSede"].ToString();
                if (dt.Rows[0]["FechaNacAfiliado"].ToString() != "1900-01-00")
                {
                    ltCumpleAfiliado.Text = String.Format("{0:dd MMM}", Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"]));
                }
                else
                {
                    ltCumpleAfiliado.Text = "-";
                }
            }
            dt.Dispose();
        }

        private void CargarPlanesAfiliado()
        {
            if (Request.QueryString.Count > 0)
            {
                ViewState["planes"] = "";
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.CargarPlanesAfiliado(Request.QueryString["deleteid"].ToString(), "all");

                if (dt.Rows.Count > 0)
                {
                    rpPlanesAfiliado.DataSource = dt;
                    rpPlanesAfiliado.DataBind();

                    ltMensaje.Text = "<div class=\"ibox-content\">" +
                    "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "El afiliado tiene planes asociados, no se puede eliminar" +
                    "</div></div>";
                    btnEliminar.Enabled = false;
                }
                else
                {
                    ltNoPlanes.Text = "Afiliado sin planes.";
                }
                dt.Dispose();

            }
        }

        private void CargarPreguntaConfirmacion()
        {
            Random aleatorio = new Random();
            int numero = aleatorio.Next(1, 4);
            switch (numero)
            {
                case 1:
                    ltPregunta.Text = "3+2=";
                    ViewState["respuesta"] = "5";
                    break;
                case 2:
                    ltPregunta.Text = "5+1=";
                    ViewState["respuesta"] = "6";
                    break;
                case 3:
                    ltPregunta.Text = "4x2=";
                    ViewState["respuesta"] = "8";
                    break;
                case 4:
                    ltPregunta.Text = "1+2=";
                    ViewState["respuesta"] = "3";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Eliminar".
        /// Verifica una confirmación escrita por el usuario y, si es válida,
        /// marca al afiliado como "Inactivo" en la base de datos, registrando el cambio en el log.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txbConfirmacion.Text.ToString() != "")
            {
                if (txbConfirmacion.Text.ToString() == ViewState["respuesta"].ToString())
                {
                    int idAfiliado = int.Parse(Request.QueryString["deleteid"].ToString());

                    if (idAfiliado != 0)
                    {
                        string strInitData = TraerData();

                        clasesglobales cg = new clasesglobales();
                        DataTable dt = cg.ConsultarAfiliadoPorId(idAfiliado);

                        if (dt.Rows[0]["idAfiliado"].ToString() != "")
                        {
                            string respuesta = cg.EliminarAfiliado(idAfiliado, "Inactivo");

                            string strNewData = TraerData();
                            cg.InsertarLog(Session["idusuario"].ToString(), "afiliados", "Elimina", "El usuario eliminó al afiliado con documento: " + dt.Rows[0]["DocumentoAfiliado"].ToString() + ".", strInitData, strNewData);

                        }

                        dt.Dispose();
                    }

                    Response.Redirect("afiliados");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"ibox-content\">" +
                        "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Respuesta incorrecta." +
                        "</div></div>";
                }
            }
            else
            {
                ltMensaje.Text = "<div class=\"ibox-content\">" +
                    "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Respuesta necesaria." +
                    "</div></div>";
            }
        }

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarAfiliadoPorId(int.Parse(Request.QueryString["deleteid"].ToString()));

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