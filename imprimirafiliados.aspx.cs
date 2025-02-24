using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class imprimirafiliados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txbInicio.Text = "01/01/2025";
                txbFinal.Text = "01/31/2025";
                CargarSedes();
            }
        }

        private void CargarSedes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSedes("Gimnasio");

            ddlSedes.DataSource = dt;
            ddlSedes.DataBind();

            dt.Dispose();
        }

        protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarAfiliados();
        }

        private void ListarAfiliados()
        {
            string strQueryAdd = "";
            if (txbInicio.Text.ToString() != "" && txbFinal.Text.ToString() != "")
            {
                strQueryAdd = "AND FechaAfiliacion BETWEEN '" + txbInicio.Text.Substring(6, 4) + "-" + txbInicio.Text.Substring(0, 2) + "-" + txbInicio.Text.Substring(3, 2) + "' " +
                    "AND '" + txbFinal.Text.Substring(6, 4) + "-" + txbFinal.Text.Substring(0, 2) + "-" + txbFinal.Text.Substring(3, 2) + "' ";
            }
            string strQuery = "SELECT *, TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) AS edad, " +
                    "IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 14,'baby',IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 60,'person','person-walking-with-cane')) age " +
                    "FROM Afiliados " +
                    "WHERE idSede = " + ddlSedes.SelectedItem.Value.ToString() + " " + strQueryAdd +
                    "LIMIT 100";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpAfiliados.DataSource = dt;
            rpAfiliados.DataBind();

            dt.Dispose();
        }

        protected void txbInicio_TextChanged(object sender, EventArgs e)
        {
            ListarAfiliados();
        }

        protected void txbFinal_TextChanged(object sender, EventArgs e)
        {
            ListarAfiliados();
        }
    }
}