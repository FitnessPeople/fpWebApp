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
    public partial class eps : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Eps");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        //No tiene acceso a esta página
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    else
                    {
                        //Si tiene acceso a esta página
                        divBotonesLista.Visible = false;
                        btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            btnImprimir.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            btnImprimir.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                    listaEps();
                    ltTitulo.Text = "Agregar EPS";
                    if (Request.QueryString.Count > 0)
                    {
                        rpEps.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            string strQuery = "SELECT * FROM eps WHERE idEps = " + Request.QueryString["editid"].ToString();
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.TraerDatos(strQuery);
                            if (dt.Rows.Count > 0)
                            {
                                txbEps.Text = dt.Rows[0]["NombreEps"].ToString();
                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar EPS";
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            string strQuery = "SELECT * FROM Empleados WHERE idEps = " + Request.QueryString["deleteid"].ToString();
                            clasesglobales cg1 = new clasesglobales();
                            DataTable dt1 = cg1.TraerDatos(strQuery);

                            if (dt1.Rows.Count > 0)
                            {
                                ltMensaje.Text = "<div class=\"ibox-content\">" +
                                    "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                    "Esta EPS no se puede borrar, hay empleados asociados a ella." +
                                    "</div></div>";
                                

                                strQuery = "SELECT * FROM Eps WHERE idEps = " + Request.QueryString["deleteid"].ToString();
                                clasesglobales cg = new clasesglobales();
                                DataTable dt = cg.TraerDatos(strQuery);
                                if (dt.Rows.Count > 0)
                                {
                                    txbEps.Text = dt.Rows[0]["NombreEps"].ToString();
                                    txbEps.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    btnAgregar.Enabled = false;
                                    ltTitulo.Text = "Borrar EPS";
                                }
                            }
                            else
                            {
                                //Borrar
                                strQuery = "SELECT * FROM Eps WHERE idEps = " + Request.QueryString["deleteid"].ToString();
                                clasesglobales cg = new clasesglobales();
                                DataTable dt = cg.TraerDatos(strQuery);
                                if (dt.Rows.Count > 0)
                                {
                                    txbEps.Text = dt.Rows[0]["NombreEps"].ToString();
                                    txbEps.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    ltTitulo.Text = "Borrar EPS";
                                }
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("logout.aspx");
                }
            }
        }

        private bool ValidarEps(string strNombre)
        {
            bool bExiste = false;

            string strQuery = "SELECT * FROM Eps WHERE NombreEps = '" + strNombre.Trim() + "' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }

            return bExiste;
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

        private void listaEps()
        {
            string strQuery = "SELECT * FROM Eps";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpEps.DataSource = dt;
            rpEps.DataBind();

            dt.Dispose();
        }

        protected void rpEps_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlButton btnEditar = (HtmlButton)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("onClick", "window.location.href='eps?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlButton btnEliminar = (HtmlButton)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("onClick", "window.location.href='eps?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editid"] != null)
                {
                    myConnection.Open();
                    string strQuery = "UPDATE Eps " +
                        "SET NombreEps = '" + txbEps.Text.ToString().Trim() + "' " +
                        "WHERE idEps = " + Request.QueryString["editid"].ToString();
                    OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    myConnection.Close();

                    Response.Redirect("eps");
                }

                if (Request.QueryString["deleteid"] != null)
                {
                    myConnection.Open();
                    string strQuery = "DELETE FROM Eps " +
                        "WHERE idEps = " + Request.QueryString["deleteid"].ToString();
                    OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    myConnection.Close();

                    Response.Redirect("eps");
                }
            }
            else
            {
                if (!ValidarEps(txbEps.Text.ToString()))
                {
                    myConnection.Open();
                    string strQuery = "INSERT INTO Eps " +
                        "(NombreEps) VALUES ('" + txbEps.Text.ToString().Trim() + "') ";
                    OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    myConnection.Close();

                    Response.Redirect("eps");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Ya existe una EPS con ese nombre." +
                        "</div>";
                }
            }
        }
    }
}