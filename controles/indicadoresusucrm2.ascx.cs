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
            ConsultarContactosActivosPorUsuario( idUsuario);
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
                int cantidadAsesores = 0;
                int metaAsesorDeluxe = 0;
                int metaAsesorPremiun = 0;
                int metaAsesorElite = 0;
                int metaDirectorSede = 0;
                int valorVendidoMes = 0;
                int valorVendidoHoy = 0;
                int valorMetaAsesorHoy = 0;
                int valorMetaAsesorMes = 0;
                string tipoSedeUsuario = string.Empty;
                int perfilUsuario = 0;
              
                ltNumContactos.Text = "0";
                ltNumNegociacionAceptada.Text = "0";
                ltNumEnNegociacion.Text = "0";
                ltNumCaliente.Text = "0";
                ltNumTibio.Text = "0";
                ltNumFrio.Text = "0";
                ltVendidoMes.Text = "0";
                ltVendidoDia.Text = "0";
                ltValorMetaAsesorHoy.Text = "0";
                ltBrechaMes.Text = "0";
                ltBrechaHoy.Text = "0";

                DateTime hoy = DateTime.Today;
                int mes = hoy.Month;
                int anio = hoy.Year;
                string nombreMes = hoy.ToString("MMMM", new CultureInfo("es-ES"));
                nombreMes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nombreMes);
                ltNomMesActual.Text = $"Objetivo mes {nombreMes} {anio}";

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
                tipoSedeUsuario = dt6.Rows[0]["TipoSede"].ToString();
                perfilUsuario = Convert.ToInt32(dt6.Rows[0]["IdPerfil"].ToString());


                DataTable dt7 = cg.ConsultarEstacionalidadPorDia(idCanalVenta, 9, 2025);

                if (tipoSedeUsuario == "Deluxe")
                    valorMetaAsesorMes = Convert.ToInt32(dt7.Rows[0]["MetaAsesorDeluxe"].ToString());
                if (tipoSedeUsuario == "Premium")
                    valorMetaAsesorMes = Convert.ToInt32(dt7.Rows[0]["MetaAsesorPremium"].ToString());
                if (tipoSedeUsuario == "Elite")
                    valorMetaAsesorMes = Convert.ToInt32(dt7.Rows[0]["MetaAsesorElite"].ToString());
                if (perfilUsuario == 2)
                    valorMetaAsesorMes = Convert.ToInt32(dt7.Rows[0]["MetaDirectorSede"].ToString());
                if (idCanalVenta == 12)
                    valorMetaAsesorMes = Convert.ToInt32(dt7.Rows[0]["MetaAsesorOnline"].ToString());

                ltValorMetaAsesorMes.Text = valorMetaAsesorMes.ToString("C0", new CultureInfo("es-CO"));

                DataTable dt8 = cg.ConsultarEstacionalidadPorDia(idCanalVenta, mes, anio);
                valorMetaAsesorHoy = Convert.ToInt32(dt8.Rows[0]["MetaDia"].ToString());
                if (dt8.Rows.Count >0)
                ltValorMetaAsesorHoy.Text = valorMetaAsesorHoy.ToString("C0", new CultureInfo("es-CO")); 


                ///////////////////////////////////////////////PANEL INDICADORES SUPERIOR DERECHC/////////////////////////
                DataTable dt2 = cg.ConsultarContactosCRMPorUsuario(idUsuario, out valorT);
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

                //////////////////////////////////////INDICADORES DE VALORES///////////////////////
               
                DataTable dt3 = cg.ConsultarVentasAsesorMesVigente(idUsuario);
                valorVendidoMes = Convert.ToInt32(dt3.Rows[0]["TotalVendido"].ToString());
               
                if (dt3.Rows.Count > 0 )
                ltVendidoMes.Text = valorVendidoMes.ToString();

                DataTable dt4 = cg.ConsultarVentasAsesorAnnioVigente(idUsuario);
                if (dt4.Rows.Count > 0)
                    ltVendidoMes.Text = dt4.Rows[0]["TotalVendido"].ToString(); 

                DataTable dt5 = cg.ConsultarVentasAsesorDiaVigente(idUsuario);
                valorVendidoHoy = Convert.ToInt32(dt5.Rows[0]["TotalVendido"].ToString());
                if (dt5.Rows.Count > 0)
                    ltVendidoDia.Text = dt5.Rows[0]["TotalVendido"].ToString();

                /////////////////////////////////////////BRECHAS////////////////////////////////////
                int brechames = valorMetaAsesorMes - valorVendidoMes;
                ltBrechaMes.Text = brechames.ToString("C0", new CultureInfo("es-CO"));

                int brechahoy = valorMetaAsesorHoy - valorVendidoHoy;
                ltBrechaHoy.Text = brechahoy.ToString("C0", new CultureInfo("es-CO"));

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

    }
}