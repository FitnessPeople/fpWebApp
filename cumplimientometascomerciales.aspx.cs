using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;

namespace fpWebApp
{
    public partial class cumplimientometascomerciales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Cumplimiento metas");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        //CargarSedes();
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        string eventTarget = Request["__EVENTTARGET"];
                        if (eventTarget == "CalendarChanged")
                        {
                            // Leer el HiddenField
                            string[] valores = hfMes.Value.Split('|');
                            int year = int.Parse(valores[0]);
                            int month = int.Parse(valores[1]);

                            // Aquí llamas tu lógica actual en C#
                            ConsultarMetaMensual(year, month);
                            ConsultarVentaMensual(year, month);
                        }

                        //ConsultarMetaMensual();
                        //ConsultarVentaMensual();
                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        //btnEliminar.Visible = true;
                    }
                    //indicadores01.Visible = false;
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
        /// Consulta la meta comercial del mes y año actual
        /// </summary>
        private void ConsultarMetaMensual(int year, int month)
        {
            clasesglobales cg = new clasesglobales();
            DateTime fechaActual = DateTime.Now;
            int numeroDelMes = month;
            int numeroDelAnnio = year;
            string strQuery = "SELECT * " +
                "FROM MetasComerciales " +
                "WHERE Mes = " + numeroDelMes + " " +
                "AND Annio = " + numeroDelAnnio;
            DataTable dt = cg.TraerDatos(strQuery);
            ViewState.Add("metamensual", dt.Rows[0]["valor"]);
            ltMetaMes.Text = "$ " + string.Format("{0:N0}", dt.Rows[0]["valor"]);
            dt.Dispose();
        }

        /// <summary>
        /// Consulta las ventas acumuladas del mes, 
        /// </summary>
        private void ConsultarVentaMensual(int year, int month)
        {
            clasesglobales cg = new clasesglobales();
            DateTime fechaActual = DateTime.Now;
            int numeroDelMes = month;
            int numeroDelAnnio = year;
            string strQuery = "SELECT SUM(valor) ventasTotal " +
                "FROM pagosplanafiliado " +
                "WHERE MONTH(FechaHoraPago) = " + numeroDelMes + " " +
                "AND YEAR(FechaHoraPago) = " + numeroDelAnnio + " " +
                "AND idCanalVenta = " + Session["idCanalVenta"].ToString();
            DataTable dt = cg.TraerDatos(strQuery);
            ltVentaMes.Text = "$ " + string.Format("{0:N0}", dt.Rows[0]["ventasTotal"]);
            int brecha = Convert.ToInt32(ViewState["metamensual"]) - Convert.ToInt32(dt.Rows[0]["ventasTotal"]);
            ltBrecha.Text = string.Format("{0:N0}", brecha);
            dt.Dispose();
        }

        protected void ddlCanalVenta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}