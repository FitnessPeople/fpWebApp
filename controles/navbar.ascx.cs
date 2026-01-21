using System;
using System.Data;
using static fpWebApp.estacionalidad;

namespace fpWebApp.controles
{
    public partial class navbar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();

            string etiqueta =string.Empty;
            DataTable dt = cg.ConsultarUsuarioSedePerfilPorId(Convert.ToInt32(Session["idUsuario"]));
            if (dt.Rows.Count > 0)
            {
                lblNombrePerfil.Text = dt.Rows[0]["Perfil"].ToString();
                if (dt.Rows[0]["idCanalVenta"].ToString() == "1")
                {
                    lblNombreSede.Text = dt.Rows[0]["NombreSede"].ToString();
                    etiqueta = "Canal de ventas: " + dt.Rows[0]["NombreCanalVenta"].ToString();
                }
                else if (dt.Rows[0]["idCanalVenta"].ToString() == "12" || dt.Rows[0]["idCanalVenta"].ToString() == "13" || dt.Rows[0]["idCanalVenta"].ToString() == "14")
                {
                    lblNombreSede.Text = dt.Rows[0]["NombreSede"].ToString();
                    etiqueta = "Canal de ventas: " + dt.Rows[0]["NombreCanalVenta"].ToString() + " Cargo: " + dt.Rows[0]["NombreCargo"].ToString(); 
                }
                else
                {
                    lblNombreSede.Text = dt.Rows[0]["NombreCanalVenta"].ToString();
                    etiqueta = dt.Rows[0]["NombreCargo"].ToString();
                }
            }

            if (Session["idUsuario"] != null)
            {
                ltNombreUsuario.Text = Session["NombreUsuario"].ToString();
                ltCargo.Text = etiqueta;
                if (Session["Foto"].ToString() != "")
                {
                    ltFoto.Text = "<img alt=\"image\" class=\"img-lg\" src=\"img/empleados/" + Session["Foto"].ToString() + "\" />";
                }
                else
                {
                    ltFoto.Text = "<img alt=\"image\" class=\"img-lg\" src=\"img/empleados/nofoto.png\" />";
                }

                //totalAfiliados();
                //totalEmpleados();
                //totalUsuarios();
                //totalInscritos();

                if (Session["idSede"].ToString() == "11")
                {
                    ltMenuCalendario.Text = "<li class=\"special_link\">";
                    ltMenuCalendario.Text += "<a href=\"calendariofpadmin\"><i class=\"fa fa-calendar\"></i><span class=\"nav-label\">Calendario FP+ Admin</span></a>";
                    ltMenuCalendario.Text += "</li>";
                }
                
                cargarMenu();
            }
            else
            {
                Response.Redirect("default");
            }
        }

        private void cargarMenu()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt1 = cg.CargarCategoriasPaginaPorPerfil(Convert.ToInt32(Session["idPerfil"].ToString()));

            string strMenu = string.Empty;

            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    DataTable dt2 = cg.CargarPaginasPorCategoriayPerfil(Convert.ToInt32(dt1.Rows[i]["idCategoriaPagina"].ToString()),
                        Convert.ToInt32(Session["idPerfil"].ToString()));

                    strMenu += "<li>";
                    strMenu += "<a href=\"#\" title=\"" + dt1.Rows[i]["NombreCategoriaPagina"].ToString() + "\"><i class=\"fa fa-" + dt1.Rows[i]["IconoFA"].ToString() + "\"></i><span class=\"nav-label\">" + dt1.Rows[i]["NombreCategoriaPagina"].ToString() + "</span><span class=\"fa arrow\"></span></a>";
                    strMenu += "<ul id=\"" + dt1.Rows[i]["Identificador"].ToString() + "\" class=\"nav nav-second-level collapse\">";
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        strMenu += "<li id=\"" + dt2.Rows[j]["NombreASPX"].ToString() + "\" class=\"old\"><a href=\"" + dt2.Rows[j]["NombreASPX"].ToString() + "\"><i class=\"fa fa-" + dt2.Rows[j]["IconoFA"].ToString() + "\"></i>" + dt2.Rows[j]["Pagina"].ToString() + "</a></li>";
                    }
                    strMenu += "</ul>";
                    strMenu += "</li>";

                    dt2.Dispose();
                }
                ltMenu.Text = strMenu;
            }

            dt1.Dispose();
        }
    }
}