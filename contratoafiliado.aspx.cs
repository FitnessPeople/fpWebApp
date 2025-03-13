using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
	public partial class contratoafiliado : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            string strQuery = "SELECT * " +
                "FROM EmpresasFP " +
                "WHERE idEmpresaFP = 1 ";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            string strTextoContrato = dt.Rows[0]["ContratoMayorEdad"].ToString();
            string contenidoEditor = hiddenEditor.Value;
            hiddenEditor.Value = dt.Rows[0]["ContratoMayorEdad"].ToString();

            strQuery = "SELECT * " +
                "FROM Afiliados " +
                "WHERE idAfiliado = 10036 ";

            DataTable dt1 = cg.TraerDatos(strQuery);

            strTextoContrato = strTextoContrato.Replace("#NOMBRE#", dt1.Rows[0]["NombreAfiliado"].ToString() + " " + dt1.Rows[0]["ApellidoAfiliado"].ToString());


            // Traer todo el texto del contrato desde la BD.

            //Traer los datos del afiliado en un dt

            // Llenar los campos del texto del contrato con los datos del dt del afiliado.

            ltContrato.Text = strTextoContrato;

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            string contenidoEditor = hiddenEditor.Value;

            try
            {
                string respuesta = cg.TraerDatosStr("UPDATE EmpresasFP" + contenidoEditor);
                //cg.InsertarLog(Session["idusuario"].ToString(), "sedes", "Agrega", "El usuario agregó una nueva sede: " + txbSede.Text.ToString() + ".", "", "");
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }

            Response.Redirect("contratoafiliado");
        }
    }
}