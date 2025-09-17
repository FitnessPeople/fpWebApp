using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Media.Media3D;

namespace fpWebApp.controles
{
    public partial class indicadoresusucrm2 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
            ConsultarContactosActivosPorUsuario(idUsuario);
            ObtenerGraficaEstrategiasPorMes();
        }

        private void ConsultarContactosActivosPorUsuario(int idUsuario)
        {
            clasesglobales cg = new clasesglobales();
            try
            {


                //////////////////////////////////DECLARACIÓN DE VARIABLES ///////////////////////////////
                decimal valorT = 0;
                int valor = 0;
                int idCanalVenta = Convert.ToInt32(Session["idCanalVenta"].ToString());
                int valorVendidoMes = 0;
                int valorVendidoHoy = 0;
                int valorVendidoAnnio = 0;
                int valorMetaAsesorHoy = 0;
                int valorMetaAsesorMes = 0;
                string tipoSedeUsuario = string.Empty;
                int perfilUsuario = 0;

                ltNumContactos.Text = "$0";
                ltNumNegociacionAceptada.Text = "$0";
                ltNumEnNegociacion.Text = "$0";
                ltNumCaliente.Text = "$0";
                ltNumTibio.Text = "$0";
                ltNumFrio.Text = "$0";
                ltVendidoMes.Text = "$0";
                ltVendidoDia.Text = "$0";
                ltValorMetaAsesorMes.Text = "$0";
                ltValorMetaAsesorHoy.Text = "$0";
                ltBrechaMes.Text = "$0";
                ltBrechaHoy.Text = "$0";


                DateTime hoy = DateTime.Today;
                int mes = hoy.Month;
                int anio = hoy.Year;
                string nombreMes = hoy.ToString("MMMM", new CultureInfo("es-ES"));
                nombreMes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nombreMes);
                ltNomMesActual.Text = $"Objetivo mes {nombreMes} {anio}";
                //////////////////////////////////////////////////////////////////////////////////////////////////////
               

                ///////////////////////////////////////////////META CANAL DE VENTA //////////////////////////////////
                DataTable dt = cg.ConsultarMetasComerciales();

                int canalVenta = Convert.ToInt32(idCanalVenta);
                DataRow meta = ConsultarMetaCanal(dt, canalVenta);

                if (meta != null)
                {
                    valor = Convert.ToInt32(meta["Presupuesto"]);
                    string canal = meta["NombreCanalVenta"].ToString();
                }
                else
                {
                    valor = 0;
                    string canal = canalVenta.ToString();
                }

                ///////////////////////////////////////////////METAS ASESORES MES Y HOY /////////////////////////////////////////////

                DataTable dt6 = cg.ConsultarUsuarioSedePerfilPorId(idUsuario);

                if (dt6.Rows.Count > 0)
                {
                    tipoSedeUsuario = dt6.Rows[0]["TipoSede"].ToString();
                    perfilUsuario = Convert.ToInt32(dt6.Rows[0]["IdPerfil"].ToString());


                    DataTable dt7 = cg.ConsultarEstacionalidadPorDia(idCanalVenta, mes, anio);

                    if (dt7.Rows.Count > 0)
                    {
                        DataRow filaHoy = null;
                        foreach (DataRow row in dt7.Rows)
                        {
                            DateTime fechaInicio = Convert.ToDateTime(row["FechaInicio"]);

                            if (hoy == fechaInicio)
                            {
                                filaHoy = row;
                                break;
                            }
                        }

                        if (filaHoy != null)
                        {
                            if (perfilUsuario == 4 && tipoSedeUsuario == "Deluxe")
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["MetaAsesorDeluxeMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaAsesorDeluxeDia"]);
                            }

                            if (perfilUsuario == 4 && tipoSedeUsuario == "Premium")
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["MetaAsesorPremiumMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaAsesorPremiumDia"]);
                            }

                            if (perfilUsuario == 4 && tipoSedeUsuario == "Elite")
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["MetaAsesorEliteMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaAsesorEliteDia"]);
                            }

                            if (perfilUsuario == 2)
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["MetaDirectorSedeMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaDirectorSedeDia"]);
                            }

                            if (perfilUsuario == 4 && idCanalVenta == 12)
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["MetaAsesorOnlineMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaAsesorOnlineDia"]);
                            }

                            ltValorMetaAsesorMes.Text = valorMetaAsesorMes.ToString("C0", new CultureInfo("es-CO"));
                            ltValorMetaAsesorHoy.Text = valorMetaAsesorHoy.ToString("C0", new CultureInfo("es-CO"));
                        }
                    }


                }

                ///////////////////////////////////////////////PANEL INDICADORES SUPERIOR DERECHC/////////////////////////

                DataTable dt2 = cg.ConsultarContactosCRMPorUsuario(idUsuario, out valorT);

                if (dt2.Rows.Count > 0)
                {
                    ltNumContactos.Text = dt2.Rows.Count.ToString();
                    //Negociación aceptada
                    DataRow[] RegistrosNegociacionAceptada = dt2.Select("idEstadoCRM = 3");
                    ltNumNegociacionAceptada.Text = RegistrosNegociacionAceptada.Length.ToString();
                    //En negociación
                    DataRow[] RegistrosEnNegociacion = dt2.Select("idEstadoCRM = 2");
                    ltNumEnNegociacion.Text = RegistrosEnNegociacion.Length.ToString();
                    //En caliente
                    DataRow[] RegistrosEnCaliente = dt2.Select("idEstadoVenta = 1");
                    ltNumCaliente.Text = RegistrosEnCaliente.Length.ToString();
                    //En tibio
                    DataRow[] RegistrosEnTibio = dt2.Select("idEstadoVenta = 2");
                    ltNumTibio.Text = RegistrosEnTibio.Length.ToString();
                    //En Frio
                    DataRow[] RegistrosEnFrio = dt2.Select("idEstadoVenta = 3");
                    ltNumFrio.Text = RegistrosEnFrio.Length.ToString();
                }

                //////////////INDICADORES DE VALORES - VENDIDO MES Y VENDIDO HOY///////////////////////

                DataTable dt3 = cg.ConsultarVentasAsesorMesVigente(idUsuario);

                valorVendidoMes = Convert.ToInt32(dt3.Rows[0]["TotalVendido"].ToString());

                if (dt3.Rows.Count > 0)
                    ltVendidoMes.Text = valorVendidoMes.ToString("C0", new CultureInfo("es-CO"));

                DataTable dt4 = cg.ConsultarVentasAsesorAnnioVigente(idUsuario);

                if (dt4.Rows.Count > 0)
                    valorVendidoAnnio = Convert.ToInt32(dt4.Rows[0]["TotalVendido"].ToString());

                DataTable dt5 = cg.ConsultarVentasAsesorDiaVigente(idUsuario);

                if (dt5.Rows.Count > 0)
                    valorVendidoHoy = Convert.ToInt32(dt5.Rows[0]["TotalVendido"].ToString());

                ltVendidoDia.Text = valorVendidoHoy.ToString("C0", new CultureInfo("es-CO"));

                /////////////////////////////////////////BRECHAS////////////////////////////////////
                
                int brechames = valorMetaAsesorMes - valorVendidoMes;
                ltBrechaMes.Text = brechames.ToString("C0", new CultureInfo("es-CO"));

                int brechahoy = valorMetaAsesorHoy - valorVendidoHoy;
                ltBrechaHoy.Text = brechahoy.ToString("C0", new CultureInfo("es-CO"));
                /////////////////////////////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
        }

        public DataRow ConsultarMetaCanal(DataTable dt, int idCanalVenta)
        {
            int mesActual = DateTime.Now.Month;
            int annioActual = DateTime.Now.Year;

            foreach (DataRow row in dt.Rows)
            {
                if ((int)row["idCanalVenta"] == idCanalVenta &&
                    (int)row["Mes"] == mesActual &&
                    (int)row["Annio"] == annioActual)
                {
                    return row; // devuelve el registro correcto
                }
            }

            return null;
        }

        public string labelsJson { get; set; }
        public string presupuestoJson { get; set; }
        public string ventasJson { get; set; }
        private void ObtenerGraficaEstrategiasPorMes()
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarEstrategiasMarketingValorMes();

                if (dt.Rows.Count > 0)
                {
                    //Etiquetas para las fechas
                    DateTime fechaActual = DateTime.Now;
                    string mesActualConAnio = System.Globalization.CultureInfo
                     .GetCultureInfo("es-ES")
                     .DateTimeFormat
                     .GetMonthName(fechaActual.Month)
                     + " " + fechaActual.Year;
                    //ltMesActualGraf.Text = mesActualConAnio.ToString();

                    string annioActual = fechaActual.Year.ToString();
                    //ltAnnioActual.Text = annioActual;
                    //

                    DataTable dt1 = cg.ConsultarCuantosLeadsEstrategiaAceptados();
                    if (dt1.Rows.Count > 0)
                    {
                        decimal totalVentasAnio = Convert.ToDecimal(dt1.Rows[0]["TotalVentasAnio"].ToString());
                        decimal TotalContactosVentasAnio = Convert.ToDecimal(dt1.Rows[0]["TotalContactosVentasAnio"].ToString());
                        int TotalContactosAnio = Convert.ToInt32(dt1.Rows[0]["TotalcontactosAnio"].ToString());

                        decimal mediaCuantosAnio = TotalContactosVentasAnio / TotalContactosAnio;

                        int porcentaje = (int)Math.Round(mediaCuantosAnio * 100, MidpointRounding.AwayFromZero);

                        //ltMediaCuantosAnio.Text = porcentaje + "%";
                        //progressBarAnio.Attributes["style"] = "width: " + porcentaje + "%;";
                    }


                    var labels = new List<string>();
                    var presupuestos = new List<decimal>();
                    var ventas = new List<decimal>();

                    var datosPorMes = dt.AsEnumerable()
                        .GroupBy(r => Convert.ToDateTime(r["Mes"]).Month)
                        .ToDictionary(
                            g => g.Key,
                            g => new
                            {
                                Presupuesto = g.Sum(x => Convert.ToDecimal(x["Presupuesto"])),
                                Ventas = g.Sum(x => Convert.ToDecimal(x["Ventas"]))
                            }
                        );


                    // Rellenar los 12 meses
                    for (int mes = 1; mes <= 12; mes++)
                    {
                        string abreviado = new DateTime(DateTime.Now.Year, mes, 1)
                            .ToString("MMM", new System.Globalization.CultureInfo("es-CO"));

                        labels.Add(abreviado);

                        if (datosPorMes.ContainsKey(mes))
                        {
                            presupuestos.Add(datosPorMes[mes].Presupuesto);
                            ventas.Add(datosPorMes[mes].Ventas);
                        }
                        else
                        {
                            presupuestos.Add(0);
                            ventas.Add(0);
                        }
                    }

                    labelsJson = Newtonsoft.Json.JsonConvert.SerializeObject(labels);
                    presupuestoJson = Newtonsoft.Json.JsonConvert.SerializeObject(presupuestos);
                    ventasJson = Newtonsoft.Json.JsonConvert.SerializeObject(ventas);
                }
                else
                {
                    var labels = new List<string>();
                    var presupuestos = new List<decimal>();
                    var ventas = new List<decimal>();

                    for (int mes = 1; mes <= 12; mes++)
                    {
                        string abreviado = new DateTime(DateTime.Now.Year, mes, 1)
                            .ToString("MMM", new System.Globalization.CultureInfo("es-CO"));

                        labels.Add(abreviado);
                        presupuestos.Add(0);
                        ventas.Add(0);
                    }

                    labelsJson = Newtonsoft.Json.JsonConvert.SerializeObject(labels);
                    presupuestoJson = Newtonsoft.Json.JsonConvert.SerializeObject(presupuestos);
                    ventasJson = Newtonsoft.Json.JsonConvert.SerializeObject(ventas);
                }

            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }
        }

    }
}