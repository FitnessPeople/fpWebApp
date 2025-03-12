using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class reportepagosmulticanal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("es-CO"); // Cambia según el país
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Autorizaciones");
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
                            txbFechaIni.Attributes.Add("type", "date");
                            txbFechaIni.Value = DateTime.Now.ToString("yyyy-MM-01").ToString();
                            txbFechaInicio.Text = DateTime.Now.ToString("yyyy-MM-01").ToString();
                            txbFechaFin.Text = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                            listaTransacciones("Efectivo",(txbFechaInicio.Text.ToString()),(txbFechaFin.Text.ToString()));
                            CargarCortesias();
                            CargarTraspasos();
                            CargarCongelaciones();
                            CargarIncapacidades();
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

        private void listaTransacciones(string tipoPago, string fechaIni, string fechaFin)
        {            
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosPorTipo(tipoPago, fechaIni, fechaFin,out decimal valorTotal);
            rpTipoEfectivo.DataSource = dt;
            rpTipoEfectivo.DataBind();
            ltValorTotal.Text = valorTotal.ToString("C0");
            dt.Dispose();
        }

        private void CargarCortesias()
        {
            string strQuery = "SELECT *, " +
                "DATEDIFF(CURDATE(), FechaHoraCortesia) AS hacecuanto, " +
                "IF(DATEDIFF(CURDATE(), FechaHoraCortesia)<5,'pie1',IF(DATEDIFF(CURDATE(), FechaHoraCortesia)<10,'pie2',IF(DATEDIFF(CURDATE(), FechaHoraCortesia)<15,'pie3','pie3'))) badge " +
                "FROM Cortesias c, Afiliados a, AfiliadosPlanes ap, Usuarios u " +
                "WHERE c.EstadoCortesia = 'Pendiente' " +
                "AND c.idAfiliadoPlan = ap.idAfiliadoPlan " +
                "AND ap.idAfiliado = a.idAfiliado " +
                "AND c.idUsuario = u.idUsuario " +
                "ORDER BY c.FechaHoraCortesia DESC";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                //rpCortesias.DataSource = dt;
                //rpCortesias.DataBind();
            }

            dt.Dispose();
        }

        private void CargarTraspasos()
        {
            string strQuery = "SELECT t.idTraspaso, CONCAT(a1.NombreAfiliado, ' ', a1.ApellidoAfiliado) nomAfilOrigen, " +
                "CONCAT(a2.NombreAfiliado, ' ', a2.ApellidoAfiliado) nomAfilDestino, t.Observaciones, u.NombreUsuario, t.FechaTraspaso, " +
                "DATEDIFF(CURDATE(), FechaTraspaso) AS hacecuanto, " +
                "IF(DATEDIFF(CURDATE(), FechaTraspaso)<5,'pie1',IF(DATEDIFF(CURDATE(), FechaTraspaso)<10,'pie2',IF(DATEDIFF(CURDATE(), FechaTraspaso)<15,'pie3','pie3'))) badge " +
                "FROM traspasoplanes t, Afiliados a1, Afiliados a2, AfiliadosPlanes ap, Usuarios u " +
                "WHERE t.EstadoTraspaso = 'En proceso' " +
                "AND t.idAfiliadoPlan = ap.idAfiliadoPlan " +
                "AND t.idAfiliadoOrigen = a1.idAfiliado " +
                "AND t.idAfiliadoDestino = a2.idAfiliado " +
                "AND t.idUsuario = u.idUsuario " +
                "ORDER BY t.FechaTraspaso DESC";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                rpTraspasos.DataSource = dt;
                rpTraspasos.DataBind();
            }

            dt.Dispose();
        }

        private void CargarCongelaciones()
        {
            string strQuery = "SELECT *, CONCAT(a.NombreAfiliado, ' ', a.ApellidoAfiliado) AS NombreCompletoAfiliado, " +
                "DATEDIFF(CURDATE(), Fecha) AS hacecuanto, " +
                "IF(DATEDIFF(CURDATE(), Fecha)<5,'pie1',IF(DATEDIFF(CURDATE(), Fecha)<10,'pie2',IF(DATEDIFF(CURDATE(), Fecha)<15,'pie3','pie3'))) badge " +
                "FROM Congelaciones c, Afiliados a, AfiliadosPlanes ap, Usuarios u " +
                "WHERE Estado = 'En proceso' " +
                "AND c.idAfiliadoPlan = ap.idAfiliadoPlan " +
                "AND ap.idAfiliado = a.idAfiliado " +
                "AND c.idUsuario = u.idUsuario " +
                "ORDER BY Fecha DESC";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                rpCongelaciones.DataSource = dt;
                rpCongelaciones.DataBind();
            }

            dt.Dispose();
        }

        private void CargarIncapacidades()
        {
            string strQuery = "SELECT *, CONCAT(a.NombreAfiliado, ' ', a.ApellidoAfiliado) AS NombreCompletoAfiliado, " +
                "DATEDIFF(CURDATE(), Fecha) AS hacecuanto, " +
                "IF(DATEDIFF(CURDATE(), Fecha)<5,'pie1',IF(DATEDIFF(CURDATE(), Fecha)<10,'pie2',IF(DATEDIFF(CURDATE(), Fecha)<15,'pie3','pie3'))) badge " +
                "FROM Incapacidades i, Afiliados a, AfiliadosPlanes ap, Usuarios u " +
                "WHERE Estado = 'En proceso' " +
                "AND i.idAfiliadoPlan = ap.idAfiliadoPlan " +
                "AND ap.idAfiliado = a.idAfiliado " +
                "AND i.idUsuario = u.idUsuario " +
                "ORDER BY Fecha DESC";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                rpIncapacidades.DataSource = dt;
                rpIncapacidades.DataBind();
            }

            dt.Dispose();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            listaTransacciones("Efectivo", txbFechaInicio.Text, txbFechaFin.Text);
        }
    }
}