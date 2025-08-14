using System;
using System.Data;
using static fpWebApp.estacionalidad;

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
            string strQuery = "SELECT cp.idCategoriaPagina, cp.NombreCategoriaPagina, cp.IconoFA, cp.Identificador " + 
                "FROM CategoriasPaginas cp " +
                "INNER JOIN Paginas p ON p.idCategoria = cp.idCategoriaPagina " +
                "INNER JOIN permisos_perfiles pp ON pp.idPagina = p.idPagina " +
                "AND pp.idPerfil = " + Session["idPerfil"].ToString() + " " +
                "AND SinPermiso = 0 " +
                "GROUP BY cp.idCategoriaPagina " +
                "ORDER BY cp.idCategoriaPagina";
            clasesglobales cg = new clasesglobales();
            DataTable dt1 = cg.TraerDatos(strQuery);

            string strMenu = string.Empty;

            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    strQuery = "SELECT p.Pagina, p.NombreASPX, p.IconoFA " +
                        "FROM paginas p " +
                        "INNER JOIN CategoriasPaginas cp " +
                        "ON p.idCategoria = cp.idCategoriaPagina " +
                        "INNER JOIN permisos_perfiles pp ON pp.idPagina = p.idPagina " +
                        "AND pp.idPerfil = " + Session["idPerfil"].ToString() + " " +
                        "AND SinPermiso = 0 " +
                        "AND cp.idCategoriaPagina = " + dt1.Rows[i]["idCategoriaPagina"].ToString() + " " +
                        "ORDER BY p.idPagina ";
                    DataTable dt2 = cg.TraerDatos(strQuery);

                    strMenu += "<li>";
                    strMenu += "<a href=\"#\"><i class=\"fa fa-" + dt1.Rows[i]["IconoFA"].ToString() + "\"></i><span class=\"nav-label\">" + dt1.Rows[i]["NombreCategoriaPagina"].ToString() + "</span><span class=\"fa arrow\"></span></a>";
                    strMenu += "<ul id=\"" + dt1.Rows[i]["Identificador"].ToString() + "\" class=\"nav nav-second-level collapse\">";
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        strMenu += "<li id=\"" + dt2.Rows[j]["NombreASPX"].ToString() + "\" class=\"old\"><a href=\"" + dt2.Rows[j]["NombreASPX"].ToString() + "\"><i class=\"fa fa-" + dt2.Rows[j]["IconoFA"].ToString() + "\"></i>" + dt2.Rows[j]["Pagina"].ToString() + "</a></li>";
                        dt2.Dispose();
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