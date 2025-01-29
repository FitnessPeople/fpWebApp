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
    public partial class planes : System.Web.UI.Page
    {
        OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    if (Session["idUsuario"] != null)
                    {
                        ValidarPermisos("Planes");
                        if (ViewState["SinPermiso"].ToString() == "1")
                        {
                            divMensaje.Visible = true;
                            paginasperfil.Visible = true;
                            divContenido.Visible = false;
                        }
                        if (ViewState["Consulta"].ToString() == "0")
                        {
                            btnAgregar.Visible = false;
                        }
                        listaPlanes();
                        ltTitulo.Text = "Crear plan";
                        txbPrecio.Attributes.Add("type", "number");
                        txbDiasCongelamiento.Attributes.Add("type", "number");

                        if (Request.QueryString.Count > 0)
                        {
                            rpPlanes.Visible = false;
                            if (Request.QueryString["editid"] != null)
                            {
                                //Editar
                                string strQuery = "SELECT * FROM Planes WHERE idPlan = " + Request.QueryString["editid"].ToString();
                                clasesglobales cg1 = new clasesglobales();
                                DataTable dt = cg1.TraerDatos(strQuery);
                                if (dt.Rows.Count > 0)
                                {
                                    txbPlan.Text = dt.Rows[0]["NombrePlan"].ToString();
                                    txbDescripcion.Text = dt.Rows[0]["DescripcionPlan"].ToString();
                                    txbPrecio.Text = dt.Rows[0]["PrecioBase"].ToString();
                                    txbDiasCongelamiento.Text = dt.Rows[0]["DiasCongelamientoMes"].ToString();
                                    btnAgregar.Text = "Actualizar";
                                    //btnCancelar.Visible = true;
                                    ltTitulo.Text = "Actualizar plan";
                                }
                            }
                            if (Request.QueryString["deleteid"] != null)
                            {
                                //Borrar
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("logout");
                    }
                }
            }
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
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

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

        private void listaPlanes()
        {
            string strQuery = "SELECT *, " +
                "IF(p.EstadoPlan='Activo','primary','danger') AS label " +
                "FROM Planes p " +
                "LEFT JOIN Usuarios u ON p.idusuario = u.idUsuario";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            rpPlanes.DataSource = dt;
            rpPlanes.DataBind();

            dt.Dispose();
        }

        protected void rpPlanes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlButton btnEditar = (HtmlButton)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("onClick", "window.location.href='planes?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlButton btnEliminar = (HtmlButton)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("onClick", "window.location.href='planes?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editid"] != null)
                {
                    myConnection.Open();
                    string strQuery = "UPDATE Planes " +
                        "SET NombrePlan = '" + txbPlan.Text.ToString().Replace("'", "") + "', " +
                        "DescripcionPlan = '" + txbDescripcion.Text.ToString().Replace("'", "") + "', " +
                        "PrecioBase = " + txbPrecio.Text.ToString() + ", " +
                        "DiasCongelamientoMes = " + txbDiasCongelamiento.Text.ToString() + ", " +
                        "WHERE idPlan = " + Request.QueryString["editid"].ToString();
                    OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    myConnection.Close();

                    Response.Redirect("planes");
                }
            }
            else
            {
                myConnection.Open();
                string strQuery = "INSERT INTO Planes " +
                    "(NombrePlan, DescripcionPlan, Preciobase, EstadoPlan, idUsuario) " +
                    "VALUES ('" + txbPlan.Text.ToString().Replace("'", "") + "', '" + txbDescripcion.Text.ToString().Replace("'", "") + "', " +
                    "" + txbPrecio.Text.ToString() + ", 1, " + Session["idUsuario"].ToString() + ") ";
                OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                command1.ExecuteNonQuery();
                command1.Dispose();
                myConnection.Close();

                Response.Redirect("planes");   
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("planes");
        }
    }
}