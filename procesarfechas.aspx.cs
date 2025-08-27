using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class procesarfechas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarSedes();
        }



        private void CargarSedes()
        {
            clasesglobales cg = new clasesglobales();
            string strQuery = "SELECT s.idSede, CONCAT(s.NombreSede, ' - ', cs.NombreCiudadSede) AS NombreSedeCiudad " +
                "FROM Sedes s, CiudadesSedes cs " +
                "WHERE s.idCiudadSede = cs.idCiudadSede ";

            DataTable dt = cg.TraerDatos(strQuery);

            ddlSede.DataSource = dt;
            ddlSede.DataValueField = "idSede";
            ddlSede.DataTextField = "NombreSedeCiudad";
            ddlSede.DataBind();


        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {

        }
    }
}