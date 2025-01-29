using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class especialistas : System.Web.UI.Page
    {
        OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Especialistas");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        string strParam = "";
                        listaEspecialistas(strParam);

                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            btnExportar.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                }
                else
                {
                    Response.Redirect("logout.aspx");
                }
            }
        }

        private void listaEspecialistas(string strParam)
        {
            string strQuery = "SELECT *, " +
                "IF(TIMESTAMPDIFF(YEAR, FechaNacEspecialista, CURDATE()) IS NOT NULL, CONCAT('(',TIMESTAMPDIFF(YEAR, FechaNacEspecialista, CURDATE()),' Años)'),'<i class=\"fa fa-circle-question m-r-lg m-l-lg\"></i>') AS edad, " +
                "IF(TIMESTAMPDIFF(YEAR, FechaNacEspecialista, CURDATE()) < 14,'danger',IF(TIMESTAMPDIFF(YEAR, FechaNacEspecialista, CURDATE()) < 18,'success',IF(TIMESTAMPDIFF(YEAR, FechaNacEspecialista, CURDATE()) < 60,'info','warning'))) badge, " +
                "IF(EstadoEspecialista='Activo','success','danger') badge2 " +
                "FROM Especialistas e " +
                "LEFT JOIN generos g ON g.idGenero = e.idGenero " +
                "LEFT JOIN sedes s ON s.idSede = e.idSede " +
                "LEFT JOIN estadocivil ec ON ec.idEstadoCivil = e.idEstadoCivilEspecialista " +
                "LEFT JOIN profesiones p ON p.idProfesion = e.idProfesion " +
                "LEFT JOIN eps ON eps.idEps = e.idEps " +
                "LEFT JOIN ciudades ON ciudades.idCiudad = e.idCiudadEspecialista " +
                "WHERE DocumentoEspecialista like '%" + strParam + "%' " +
                "OR NombreEspecialista like '%" + strParam + "%' " +
                "OR EmailEspecialista like '%" + strParam + "%' " +
                "LIMIT 100";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpEspecialistas.DataSource = dt;
            rpEspecialistas.DataBind();

            dt.Dispose();
        }

        private void ValidarPermisos(string strPagina)
        {
            ViewState["SinPermiso"] = "1";
            ViewState["Consulta"] = "0";
            ViewState["Exportar"] = "0";
            ViewState["CrearModificar"] = "0";
            ViewState["Borrar"] = "0";

            string strQuery = "SELECT SinPermiso, Consulta, Exportar, CrearModificar, Borrar " +
                "FROM permisos_perfiles pp, paginas p, usuarios u " +
                "WHERE pp.idPagina = p.idPagina " +
                "AND p.Pagina = '" + strPagina + "' " +
                "AND pp.idPerfil = " + Session["idPerfil"].ToString() + " " +
                "AND u.idPerfil = pp.idPerfil " +
                "AND u.idUsuario = " + Session["idusuario"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                ViewState["SinPermiso"] = dt.Rows[0]["SinPermiso"].ToString();
                ViewState["Consulta"] = dt.Rows[0]["Consulta"].ToString();
                ViewState["Exportar"] = dt.Rows[0]["Exportar"].ToString();
                ViewState["CrearModificar"] = dt.Rows[0]["CrearModificar"].ToString();
                ViewState["Borrar"] = dt.Rows[0]["Borrar"].ToString();
            }

            dt.Dispose();
        }

        protected void rpEspecialistas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlButton btnEditar = (HtmlButton)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("onClick", "window.location.href='editarespecialista?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlButton btnEliminar = (HtmlButton)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("onClick", "window.location.href='eliminarespecialista?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                    btnEliminar.Visible = true;
                }
            }
        }
    }
}