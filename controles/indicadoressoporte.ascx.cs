using System;
using System.Data;

namespace fpWebApp.controles
{
    public partial class indicadoressoporte : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CuantosTicketsTotales();
            CuantosTicketsPendientes();
            CuantosTicketsResueltos();
            CuantasTicketsEnproceso();
        }

        private void CuantosTicketsTotales()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM TicketSoporte";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos1.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosTicketsPendientes()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM TicketSoporte WHERE EstadoTicket = 'Pendiente'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos2.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosTicketsResueltos()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM TicketSoporte WHERE EstadoTicket = 'Resuelto'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos3.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantasTicketsEnproceso()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM TicketSoporte WHERE EstadoTicket = 'En Proceso'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos4.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }
    }
}