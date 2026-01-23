using System;
using System.Data;
using System.Linq;

namespace fpWebApp.controles
{
    public partial class rightsidebar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int[] vendedores = { Convert.ToInt32(Session["idUsuario"].ToString()) };

            clasesglobales cg = new clasesglobales();
            DataTable dtPlanes = cg.ConsultarPlanesVigentes();

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

            DataTable dtTokens = cg.ConsultarTokensPorIdVendedor(Convert.ToInt32(Session["idUsuario"].ToString()));

            if (dtTokens.Rows.Count > 0)
            {
                DataView dv = dtTokens.DefaultView;
                dv.RowFilter = "VisibleWeb = 1";

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
    }
}