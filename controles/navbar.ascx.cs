using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class navbar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] != null)
            {
                ltNombreUsuario.Text = Session["NombreUsuario"].ToString();
                ltCargo.Text = Session["Cargo"].ToString();
                if (Session["Foto"].ToString() != "")
                {
                    ltFoto.Text = "<img alt=\"image\" class=\"img-circle circle-border\" width=\"48px\" src=\"img/empleados/" + Session["Foto"].ToString() + "\" />";
                }
                else
                {
                    ltFoto.Text = "<img alt=\"image\" class=\"img-circle img-md\" src=\"img/empleados/nofoto.png\" />";
                }

                totalAfiliados();
                totalEmpleados();
                totalUsuarios();
                totalInscritos();
            }
            else
            {
                Response.Redirect("default");
            }
        }

        private void totalAfiliados()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Afiliados WHERE EstadoAfiliado = 'Activo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltTotalAfiliados.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void totalEmpleados()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Empleados WHERE Estado = 'Activo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltTotalEmpleados.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void totalUsuarios()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Usuarios WHERE EstadoUsuario = 'Activo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltTotalUsuarios.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void totalInscritos()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM GymPass";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltTotalInscritos.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }
    }
}