using System;
using System.Collections.Generic;
using System.Data;
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
                decimal valor = 0;
                ltNumContactos.Text = "0";
                ltNumNegociacionAceptada.Text = "0";
                ltNumEnNegociacion.Text = "0";
                ltNumCaliente.Text = "0";
                ltNumTibio.Text = "0";
                ltNumFrio.Text = "0";
                ltVendidoMes.Text = "0";

                DataTable dt = cg.ConsultarMetasComerciales();
                DataTable dt1 = cg.ConsultarCanalesVenta();

                ///////////////////////////////////////////////PANEL INDICADORES SUPERIOR DERECHC/////////////////////////
                DataTable dt2 = cg.ConsultarContactosCRMPorUsuario(idUsuario, out valor);
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
                if (dt3.Rows.Count > 0 )
                ltVendidoMes.Text = dt3.Rows[0]["TotalVendido"].ToString();

                DataTable dt4 = cg.ConsultarVentasAsesorAnnioVigente(idUsuario);
                if (dt4.Rows.Count > 0)
                    ltVendidoMes.Text = dt4.Rows[0]["TotalVendido"].ToString();

                


                DateTime hoy = DateTime.Today;

                DataRow[] filasFiltradas = dt.Select("idCanalVenta <> 1"); // Se excluye la opción 1 Ninguno

                if (filasFiltradas.Length > 0)
                {
                    dt = filasFiltradas.CopyToDataTable();
                }
                else
                {
                    dt.Clear();
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }


        }

        private void ListaCanalesDeVenta()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCanalesVenta();
            try
            {
                DataRow[] filasFiltradas = dt.Select("idCanalVenta <> 1"); // Se excluye la opción 1 Ninguno

                if (filasFiltradas.Length > 0)
                {
                    dt = filasFiltradas.CopyToDataTable();
                }
                else
                {
                    dt.Clear();
                }

                //chblCanales.DataSource = dt;
                //chblCanales.DataTextField = "NombreCanalVenta";
                //chblCanales.DataValueField = "idCanalVenta";
                //chblCanales.DataBind();

                dt.Dispose();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();                
            }

        }
    }
}