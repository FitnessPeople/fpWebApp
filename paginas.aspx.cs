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
    public partial class paginas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Páginas");
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
                    listaPaginas();
                    ltTitulo.Text = "Agregar página";

                    if (Request.QueryString.Count > 0)
                    {
                        rpPaginas.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            string strQuery = "SELECT * FROM Paginas WHERE idPagina = " + Request.QueryString["editid"].ToString();
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.TraerDatos(strQuery);
                            if (dt.Rows.Count > 0)
                            {
                                txbPagina.Text = dt.Rows[0]["Pagina"].ToString();
                                ddlCategorias.SelectedIndex = Convert.ToInt16(ddlCategorias.Items.IndexOf(ddlCategorias.Items.FindByText(dt.Rows[0]["Categoria"].ToString())));
                                btnAgregar.Text = "Actualizar";
                                //btnCancelar.Visible = true;
                                ltTitulo.Text = "Actualizar página";
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

        private void listaPaginas()
        {
            string strQuery = "SELECT * FROM Paginas WHERE idPagina <> 1";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpPaginas.DataSource = dt;
            rpPaginas.DataBind();

            dt.Dispose();
        }

        private bool ValidarPagina(string strNombre)
        {
            bool bExiste = false;

            string strQuery = "SELECT * FROM Paginas WHERE pagina = '" + strNombre.Trim() + "' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }

            return bExiste;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editid"] != null)
                {
                    string strPagina = txbPagina.Text.ToString().Replace("'", "");
                    myConnection.Open();
                    string strQuery = "UPDATE Paginas " +
                        "SET Pagina = '" + strPagina + "', " +
                        "Categoria = '" + ddlCategorias.SelectedItem.Value.ToString() + "' " +
                        "WHERE idPagina = " + Request.QueryString["editid"].ToString();
                    OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    myConnection.Close();

                    Response.Redirect("paginas");
                }
            }
            else
            {
                if (!ValidarPagina(txbPagina.Text.ToString()))
                {
                    string strPagina = txbPagina.Text.ToString().Replace("'", "");
                    myConnection.Open();
                    string strQuery = "INSERT INTO Paginas " +
                        "(Pagina, Categoria) VALUES ('" + strPagina + "', '" + ddlCategorias.SelectedItem.Value.ToString() + "') ";
                    OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    myConnection.Close();

                    strQuery = "SELECT * FROM Paginas ORDER BY idPagina DESC LIMIT 1";
                    clasesglobales cg = new clasesglobales();
                    DataTable dt1 = cg.TraerDatos(strQuery);
                    string strId = dt1.Rows[0]["idPagina"].ToString();
                    dt1.Dispose();

                    strQuery = "SELECT * FROM Perfiles";
                    DataTable dt2 = cg.TraerDatos(strQuery);

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        try
                        {
                            strQuery = "INSERT INTO Permisos_Perfiles (idPerfil, idPagina, SinPermiso, Consulta, Exportar, CrearModificar, Borrar) " +
                                "VALUES ('" + dt2.Rows[i]["idPerfil"].ToString() + "', '" + strId + "', 1, 0, 0, 0, 0) ";
                            OdbcCommand command2 = new OdbcCommand(strQuery, myConnection);
                            myConnection.Open();
                            command2.ExecuteNonQuery();
                            command2.Dispose();
                            myConnection.Close();
                        }
                        catch (OdbcException ex)
                        {
                            string mensaje = ex.Message;
                            myConnection.Close();
                        }
                    }
                    dt2.Dispose();

                    Response.Redirect("paginas");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Ya existe una página con ese nombre." +
                        "</div>";
                }
            }
        }

        protected void rpPaginas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlButton btnEditar = (HtmlButton)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("onClick", "window.location.href='paginas?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlButton btnEliminar = (HtmlButton)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("onClick", "window.location.href='paginas?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("paginas");
        }
    }
}