using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class reporteefectividadcrm : System.Web.UI.Page
    {
        protected string Grafico1 = "{}";
        protected string Grafico2 = "{}";
        protected string Grafico3 = "{}";
        protected string Grafico4 = "{}";
        protected string Grafico5 = "{}";
        protected string Grafico6 = "{}";

        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("es-CO");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Efectividad gestión");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        //No tiene acceso a esta página
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    else
                    {
                        //Si tiene acceso a esta página
                        divBotonesLista.Visible = false;
                        //btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //CargarPlanes();
                            ListaCanalesDeVenta();
                            listaGestionAesores();
                            //lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            //txbFechaIni.Attributes.Add("type", "date");
                            //txbFechaIni.Value = DateTime.Now.ToString("yyyy-MM-01").ToString();
                            //txbFechaFin.Attributes.Add("type", "date");
                            //txbFechaFin.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                           // CargarPlanes();
                        }
                    }
                    listaVentas();
                    //listaTransaccionesPorFecha(Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()),Convert.ToInt32(ddlPlanes.SelectedValue.ToString()),txbFechaIni.Value.ToString(),txbFechaFin.Value.ToString());
                   // VentasWeb();
                    //VentasCounter();
                }
                else
                {
                    Response.Redirect("logout.aspx");
                }
            }
        }

        //private void VentasWeb()
        //{
        //    try
        //    {
        //        int annio = Convert.ToInt32(ddlAnnio.SelectedItem.Value.ToString());
        //        int mes = Convert.ToInt32(ddlMes.SelectedItem.Value.ToString());

        //        string strQuery = "";

        //        if (ddlPlanes.SelectedValue.ToString() == "0")
        //        {
        //            strQuery = @"SELECT ppa.valor 
        //            FROM PagosPlanAfiliado ppa 
        //            INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
        //            WHERE ppa.idUsuario = 156 AND ap.idPlan IN (18,19) 
	       //             AND (ppa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @") 
	       //             AND MONTH(ppa.fechaHoraPago) = " + mes + @" 
        //                AND YEAR(ppa.fechaHoraPago) = " + annio + @" 
        //                AND MONTH(ap.FechaInicioPlan) IN (" + mes + @");";
        //        }
        //        else
        //        {
        //            strQuery = @"SELECT ppa.valor 
        //            FROM PagosPlanAfiliado ppa 
        //            INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
        //            WHERE ppa.idUsuario = 156 AND ap.idPlan IN (18,19) 
	       //             AND (ppa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @") 
	       //             AND MONTH(ppa.fechaHoraPago) = " + mes + @" 
        //                AND YEAR(ppa.fechaHoraPago) = " + annio + @" 
        //                AND MONTH(ap.FechaInicioPlan) IN (" + mes + @") 
        //                AND ap.idPlan = " + ddlPlanes.SelectedValue.ToString();
        //        }

        //        clasesglobales cg = new clasesglobales();
        //        DataTable dt = cg.TraerDatos(strQuery);

        //        decimal sumatoriaValor = 0;

        //        if (dt.Rows.Count > 0)
        //        {
        //            object suma = dt.Compute("SUM(Valor)", "");
        //            sumatoriaValor = suma != DBNull.Value ? Convert.ToDecimal(suma) : 0;
        //        }

        //        ltCuantos2.Text = "$ " + String.Format("{0:N0}", sumatoriaValor);
        //        ltRegistros2.Text = dt.Rows.Count.ToString();

        //        dt.Dispose();

        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = ex.Message.ToString();
        //    }

        //}

        //private void VentasCounter()
        //{
        //    try
        //    {
        //        int annio = Convert.ToInt32(ddlAnnio.SelectedItem.Value.ToString());
        //        int mes = Convert.ToInt32(ddlMes.SelectedItem.Value.ToString());
        //        string strQuery = "";

        //        if (ddlPlanes.SelectedValue.ToString() == "0")
        //        {
        //            strQuery = @"SELECT ppa.valor 
        //            FROM PagosPlanAfiliado ppa 
        //            INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
        //            WHERE ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17) 
	       //             AND (ppa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @") 
	       //             AND MONTH(ppa.fechaHoraPago) = " + mes + @" 
        //                AND YEAR(ppa.fechaHoraPago) = " + annio + @" 
        //                AND MONTH(ap.FechaInicioPlan) IN (" + mes + @");";
        //        }
        //        else
        //        {
        //            strQuery = @"SELECT ppa.valor 
        //            FROM PagosPlanAfiliado ppa 
        //            INNER JOIN AfiliadosPlanes ap ON ppa.idAfiliadoPlan = ap.idAfiliadoPlan 
        //            WHERE ppa.idUsuario NOT IN (156) AND ap.idPlan IN (1,17) 
	       //             AND (ppa.idMedioPago = " + Convert.ToInt32(ddlTipoPago.SelectedValue.ToString()) + @") 
	       //             AND MONTH(ppa.fechaHoraPago) = " + mes + @" 
        //                AND YEAR(ppa.fechaHoraPago) = " + annio + @" 
        //                AND MONTH(ap.FechaInicioPlan) IN (" + mes + @") 
        //                AND ap.idPlan = " + ddlPlanes.SelectedValue.ToString();
        //        }

        //        clasesglobales cg = new clasesglobales();
        //        DataTable dt = cg.TraerDatos(strQuery);

        //        decimal sumatoriaValor = 0;

        //        if (dt.Rows.Count > 0)
        //        {
        //            object suma = dt.Compute("SUM(Valor)", "");
        //            sumatoriaValor = suma != DBNull.Value ? Convert.ToDecimal(suma) : 0;
        //        }

        //        ltCuantos3.Text = "$ " + String.Format("{0:N0}", sumatoriaValor);
        //        ltRegistros3.Text = dt.Rows.Count.ToString();

        //        dt.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = ex.Message.ToString();
        //    }


        //}

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


        private void ListaCanalesDeVenta()
        {
            ddlCanalesVenta.Items.Clear();
            ListItem li = new ListItem("Todos los canales de venta", "0");
            ddlCanalesVenta.Items.Add(li);
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCanalesVenta();


            DataRow[] filasFiltradas = dt.Select("idCanalVenta <> 1"); // Se excluye la opción 1 Ninguno

            if (filasFiltradas.Length > 0)
            {
                dt = filasFiltradas.CopyToDataTable();
            }
            else
            {
                dt.Clear();
            }
            ddlCanalesVenta.DataSource = dt;
            ddlCanalesVenta.DataBind();

            dt.Dispose();
        }

        private void CargarAsesoresPorSede(int idCanalVenta)
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                int idSede = Convert.ToInt32(Session["idSede"].ToString());
                DataTable dt = cg.ConsultaCargarAsesoresPorSede(idSede);

                if (idCanalVenta > 0)
                {
                    var filteredRows = dt.AsEnumerable()
                                         .Where(r => r.Field<int>("idCanalVenta") == idCanalVenta);

                    if (filteredRows.Any())
                    {
                        dt = filteredRows.CopyToDataTable();
                    }
                    else
                    {
                        dt = dt.Clone();
                    }
                }
                //ddlAsesores.DataSource = dt;
                //ddlAsesores.DataTextField = "NombreUsuario";
                //ddlAsesores.DataValueField = "idUsuario";
                //ddlAsesores.DataBind();

                dt.Dispose();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }
        }


        //private void CargarPlanes()
        //{
        //    ddlPlanes.Items.Clear();
        //    ListItem li = new ListItem("Todos los planes", "0");
        //    ddlPlanes.Items.Add(li);
        //    clasesglobales cg = new clasesglobales();
        //    DataTable dt = cg.ConsultarPlanesVigentes();

        //    ddlPlanes.DataSource = dt;
        //    ddlPlanes.DataBind();
        //    dt.Dispose();
        //}

        private void listaVentas()
        {
            clasesglobales cg = new clasesglobales();
            int idCanalVenta = 0;
            DateTime? FechaIni = null;
            DateTime? FechaFin = null;
            int idUsuario = 0;

            try
            {
                DataTable dt = cg.ConsultarEfectividadGestionCRM(idCanalVenta,FechaIni,FechaFin,idUsuario);
                rpContactos.DataSource = dt;
                rpContactos.DataBind();

                decimal sumatoriaValor = 0;

                if (dt.Rows.Count > 0)
                {
                    object suma = dt.Compute("SUM(Valor)", "");
                    sumatoriaValor = suma != DBNull.Value ? Convert.ToDecimal(suma) : 0;
                }

                ltCuantos1.Text = "$ " + String.Format("{0:N0}", sumatoriaValor);
                ltRegistros1.Text = dt.Rows.Count.ToString();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }

        }

        private void listaGestionAesores()
        {
            clasesglobales cg = new clasesglobales();
            int idCanalVenta = 0;
            DateTime? FechaIni = null;
            DateTime? FechaFin = null;
            int idUsuario = 0;

            try
            {
                DataTable dt = cg.ConsultarIndicadorGestionAsesorCRM(idCanalVenta, FechaIni, FechaFin, idUsuario);
                rpGestionAsesores.DataSource = dt;
                rpGestionAsesores.DataBind();

                decimal sumatoriaValor = 0;

                if (dt.Rows.Count > 0)
                {
                    object suma = "0";
                    sumatoriaValor = suma != DBNull.Value ? Convert.ToDecimal(suma) : 0;
                }

                ltCuantos1.Text = "$ " + String.Format("{0:N0}", sumatoriaValor);
                ltRegistros1.Text = dt.Rows.Count.ToString();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }

        }



        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            listaVentas();
            //VentasCounter();
            //VentasWeb();
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {

        }

        protected void btnDetalle_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "mostrarDetalle")
            {
                int idAfiliadoPlan = int.Parse(e.CommandArgument.ToString());

                Literal ltDetalleModal = (Literal)Page.FindControl("ltDetalleModal");

                if (ltDetalleModal != null)
                {
                    //ltDetalleModal.Text = listarDetalle(idAfiliadoPlan);
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal",
                   "setTimeout(function() { $('#ModalDetalle').modal('show'); }, 500);", true);
            }
        }
    }
}