using System;
using System.Data;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class rightsidebar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int[] vendedores = { Convert.ToInt32(Session["idUsuario"].ToString()) };

            clasesglobales cg = new clasesglobales();
            DataTable dtPlanes = cg.ConsultarPlanesVigentes();
            int mesActual = DateTime.Now.Month;
            int anioActual = DateTime.Now.Year;
            CargarCumpleanosMes(mesActual, anioActual);

            foreach (DataRow dr in dtPlanes.Rows)
            {
                int idPlan = Convert.ToInt32(dr["idPlan"].ToString());

                foreach (int idVendedor in vendedores)
                {
                    DataTable dtToken = cg.ConsultarTokenPorIdPlanYIdVendedor(idPlan, idVendedor);

                    if (dtToken.Rows.Count == 0)
                    {
                        string token = GenerarToken();

                        cg.InsertarTokenPlanVendedor(idPlan, idVendedor, token);
                    }
                }
            }

            //string urlCorta = cg.AcortarURL("https://fitnesspeoplecolombia.com/cambioMetodoPago");
            string urlCorta = "https://fitnesspeoplecolombia.com/cambioMetodoPago";
            ltEnlaceCambio.Text = $"<span class='d-none enlace'>{urlCorta}</span>";

            DataTable dtTokens = cg.ConsultarTokensPorIdVendedor(Convert.ToInt32(Session["idUsuario"].ToString()));

            if (dtTokens.Rows.Count > 0)
            {
                DataView dv = dtTokens.DefaultView;
                dv.RowFilter = "VisibleWeb = 1 AND Permanente = 0";

                ltEtiqueta1.Text = "<a data-toggle=\"tab\" href=\"#tab-2\">Enlaces</a>";
                rpEnlaces.Visible = true;
                rpTareas.Visible = false;

                rpEnlaces.DataSource = dv;
                rpEnlaces.DataBind();
            }
            else
            {
                ltEtiqueta1.Text = "<a data-toggle=\"tab\" href=\"#tab-2\">Tareas</a>";
                rpEnlaces.Visible = false;
                rpTareas.Visible = true;

                // Cargamos proyectos/tareas pendientes por usuario
                string strQuery = "SELECT * FROM tareas WHERE idUsuario = " + Session["idUsuario"].ToString();
                DataTable dt = cg.TraerDatos(strQuery);

                rpTareas.DataSource = dt;
                rpTareas.DataBind();

                dt.Dispose();
            }

            dtTokens.Dispose();
        }

        private static readonly Random _random = new Random();

        private string GenerarToken()
        {
            const int length = 20;
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var random = new Random();
            return new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[_random.Next(s.Length)])
                          .ToArray()
            );
        }

        protected void rpEnlaces_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;
                string token = row["token"].ToString();

                //clasesglobales cg = new clasesglobales();
                //string urlCorta = cg.AcortarURL("https://fitnesspeoplecolombia.com/register?token=" + token);
                string urlCorta = "https://fitnesspeoplecolombia.com/register?token=" + token;

                Literal lt = (Literal)e.Item.FindControl("ltEnlacePago");

                // Inyectamos HTML puro
                lt.Text = $"<span class='d-none enlace'>{urlCorta}</span>";
            }
        }

        protected void CargarCumpleanosMes(int mes, int annio)
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                DataTable dt = cg.consultarCumpleanosEmpleadosMes(mes, annio);

                if (dt != null && dt.Rows.Count > 0)
                {
                    rptCumpleSidebar.DataSource = dt;
                    rptCumpleSidebar.DataBind();

                    lblCantidadCumpleSidebar.Text = dt.Rows.Count + " cumpleaños este mes";
                }
                else
                {
                    rptCumpleSidebar.DataSource = null;
                    rptCumpleSidebar.DataBind();

                    lblCantidadCumpleSidebar.Text = "No hay cumpleaños este mes";
                }
            }
            catch (Exception ex)
            {
                // Manejo simple para UserControl
            }

        }
    }
}