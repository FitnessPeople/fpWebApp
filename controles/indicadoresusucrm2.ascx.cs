using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresusucrm2 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ConsultarContactosActivosPorUsuario();

        }

        private void ConsultarContactosActivosPorUsuario()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarMetasComerciales();
            DataTable dt1 = cg.ConsultarCanalesVenta();
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

        private void ListaCanalesDeVenta()
        {
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

            chblCanales.DataSource = dt;
            chblCanales.DataTextField = "NombreCanalVenta";
            chblCanales.DataValueField = "idCanalVenta";
            chblCanales.DataBind();

            dt.Dispose();
        }
    }
}