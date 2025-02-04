using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class perfiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Perfiles");
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
                        btnAgregar.Visible = false;
                        //upPerfiles.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            //upPerfiles.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        
                        listaPerfiles();
                        listaPaginas();
                        ltTitulo.Text = "Agregar perfil";

                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;

                            if (Request.QueryString.Count > 0)
                            {
                                if (Request.QueryString["editid"] != null)
                                {
                                    //Editar
                                    string strQuery = "SELECT * FROM perfiles WHERE idPerfil = " + Request.QueryString["editid"].ToString();
                                    clasesglobales cg = new clasesglobales();
                                    DataTable dt = cg.TraerDatos(strQuery);
                                    if (dt.Rows.Count > 0)
                                    {
                                        txbPerfil.Text = dt.Rows[0]["Perfil"].ToString();
                                        btnAgregar.Text = "Actualizar";
                                        ltTitulo.Text = "Actualizar perfil";
                                    }
                                }
                                if (Request.QueryString["deleteid"] != null)
                                {
                                    //Borrar
                                    string strQuery = "SELECT * FROM perfiles WHERE idPerfil = " + Request.QueryString["deleteid"].ToString();
                                    clasesglobales cg = new clasesglobales();
                                    DataTable dt = cg.TraerDatos(strQuery);
                                    if (dt.Rows.Count > 0)
                                    {
                                        txbPerfil.Text = dt.Rows[0]["Perfil"].ToString();
                                        txbPerfil.Enabled = false;
                                        btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                        ltTitulo.Text = "Eliminar perfil";
                                    }

                                    strQuery = "SELECT * FROM Usuarios WHERE idPerfil = " + Request.QueryString["deleteid"].ToString();
                                    clasesglobales cg1 = new clasesglobales();
                                    DataTable dt1 = cg1.TraerDatos(strQuery);

                                    if (dt1.Rows.Count > 0)
                                    {
                                        //No se puede borrar
                                        ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                        "Este perfil está asociado a un usuario. No se puede borrar." +
                                        "</div>";
                                    }
                                    dt1.Dispose();
                                }
                                rpPerfiles.Visible = false;
                            }
                        }
                    }
                    //indicadores01.Visible = false;
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

        private void listaPerfiles()
        {
            string strQuery = "SELECT * FROM Perfiles ORDER BY Perfil ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpPerfiles.DataSource = dt;
            rpPerfiles.DataBind();

            ddlPerfiles.DataSource = dt;
            ddlPerfiles.DataBind();

            dt.Dispose();
        }

        private void listaPaginas()
        {
            string strQuery = "SELECT pp.idPagina, pp.idPerfil, p.pagina, p.categoria, SinPermiso, Consulta, Exportar, CrearModificar, Borrar, " +
                "IF(SinPermiso=0,'<i class=\"fa fa-thumbs-up text-info fa-lg\"></i>','<i class=\"fa fa-thumbs-down text-danger fa-lg\"></i>') first, " +
                "IF(Consulta=1,'<i class=\"fa fa-thumbs-up text-info fa-lg\"></i>','<i class=\"fa fa-thumbs-down text-danger fa-lg\"></i>') second, " +
                "IF(Exportar=1,'<i class=\"fa fa-thumbs-up text-info fa-lg\"></i>','<i class=\"fa fa-thumbs-down text-danger fa-lg\"></i>') third, " +
                "IF(CrearModificar=1,'<i class=\"fa fa-thumbs-up text-info fa-lg\"></i>','<i class=\"fa fa-thumbs-down text-danger fa-lg\"></i>') fourth, " +
                "IF(Borrar=1,'<i class=\"fa fa-thumbs-up text-info fa-lg\"></i>','<i class=\"fa fa-thumbs-down text-danger fa-lg\"></i>') fifth " +
                "FROM permisos_perfiles pp, paginas p " +
                "WHERE pp.idPagina = p.idPagina " +
                "AND p.idPagina <> 1 " +
                "AND pp.idPerfil = " + ddlPerfiles.SelectedItem.Value.ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpPaginasPermisos.DataSource = dt;
            rpPaginasPermisos.DataBind();

            dt.Dispose();
        }

        protected void ddlPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPerfiles.SelectedItem.Value.ToString() != "")
            {
                listaPaginas();
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
                    string strQuery = "UPDATE Perfiles " +
                        "SET Perfil = '" + txbPerfil.Text.ToString().Trim() + "' " +
                        "WHERE idPerfil = " + Request.QueryString["editid"].ToString();
                    OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    myConnection.Close();

                    Response.Redirect("perfiles");
                }
                if (Request.QueryString["deleteid"] != null)
                {
                    string strQuery = "SELECT * FROM Usuarios WHERE idPerfil = " + Request.QueryString["deleteid"].ToString();
                    clasesglobales cg = new clasesglobales();
                    DataTable dt = cg.TraerDatos(strQuery);

                    if (dt.Rows.Count > 0)
                    {
                        //No se puede borrar
                        ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Este perfil está asociado a un usuario. No se puede borrar." +
                        "</div>";
                        dt.Dispose();
                    }
                    else
                    {
                        //Se borra
                        myConnection.Open();
                        strQuery = "DELETE FROM Perfiles " +
                            "WHERE idPerfil = " + Request.QueryString["deleteid"].ToString();
                        OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                        command1.ExecuteNonQuery();
                        command1.Dispose();

                        strQuery = "DELETE FROM Permisos_Perfiles " +
                           "WHERE idPerfil = " + Request.QueryString["deleteid"].ToString();
                        OdbcCommand command2 = new OdbcCommand(strQuery, myConnection);
                        command2.ExecuteNonQuery();
                        command2.Dispose();
                        myConnection.Close();
                        dt.Dispose();

                        Response.Redirect("perfiles");
                    }
                }
            }
            else
            {
                if (!ValidarPerfil(txbPerfil.Text.ToString()))
                {
                    myConnection.Open();
                    string strQuery = "INSERT INTO Perfiles " +
                        "(Perfil) VALUES ('" + txbPerfil.Text.ToString().Trim() + "') ";
                    OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    myConnection.Close();

                    strQuery = "SELECT * FROM Perfiles ORDER BY idPerfil DESC LIMIT 1";
                    clasesglobales cg1 = new clasesglobales();
                    DataTable dt1 = cg1.TraerDatos(strQuery);

                    string strId = dt1.Rows[0]["idPerfil"].ToString();
                    dt1.Dispose();

                    strQuery = "SELECT * FROM Paginas";
                    clasesglobales cg2 = new clasesglobales();
                    DataTable dt2 = cg2.TraerDatos(strQuery);

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        try
                        {
                            strQuery = "INSERT INTO Permisos_Perfiles (idPerfil, idPagina, SinPermiso, Consulta, Exportar, CrearModificar, Borrar) " +
                                "VALUES ('" + strId + "', '" + dt2.Rows[i]["idPagina"].ToString() + "', 1, 0, 0, 0, 0) ";
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

                    Response.Redirect("perfiles");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Ya existe un perfil con ese nombre." +
                        "</div>";
                }
            }
        }

        private bool ValidarPerfil(string strNombre)
        {
            bool bExiste = false;

            string strQuery = "SELECT * FROM Perfiles WHERE Perfil = '" + strNombre.Trim() + "' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }

            return bExiste;
        }

        protected void rpPerfiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (ViewState["CrearModificar"].ToString() == "1")
            {
                HtmlButton btnEditar = (HtmlButton)e.Item.FindControl("btnEditar");
                btnEditar.Attributes.Add("onClick", "window.location.href='perfiles?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                btnEditar.Visible = true;
            }
            if (ViewState["Borrar"].ToString() == "1")
            {
                HtmlButton btnEliminar = (HtmlButton)e.Item.FindControl("btnEliminar");
                btnEliminar.Attributes.Add("onClick", "window.location.href='perfiles?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                btnEliminar.Visible = true;
            }
        }

        protected void rpPaginasPermisos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lb1 = (LinkButton)e.Item.FindControl("lb1");
            lb1.CommandArgument = ((DataRowView)e.Item.DataItem).Row[0].ToString() + "," + ((DataRowView)e.Item.DataItem).Row[1].ToString();

            LinkButton lb2 = (LinkButton)e.Item.FindControl("lb2");
            lb2.CommandArgument = ((DataRowView)e.Item.DataItem).Row[0].ToString() + "," + ((DataRowView)e.Item.DataItem).Row[1].ToString();

            LinkButton lb3 = (LinkButton)e.Item.FindControl("lb3");
            lb3.CommandArgument = ((DataRowView)e.Item.DataItem).Row[0].ToString() + "," + ((DataRowView)e.Item.DataItem).Row[1].ToString();

            LinkButton lb4 = (LinkButton)e.Item.FindControl("lb4");
            lb4.CommandArgument = ((DataRowView)e.Item.DataItem).Row[0].ToString() + "," + ((DataRowView)e.Item.DataItem).Row[1].ToString();

            LinkButton lb5 = (LinkButton)e.Item.FindControl("lb5");
            lb5.CommandArgument = ((DataRowView)e.Item.DataItem).Row[0].ToString() + "," + ((DataRowView)e.Item.DataItem).Row[1].ToString();
        }

        protected void lb1_Click(object sender, EventArgs e)
        {
            string perm = "1";
            CambiarPermiso(((LinkButton)sender).CommandArgument, perm);
        }

        protected void lb2_Click(object sender, EventArgs e)
        {
            string perm = "2";
            CambiarPermiso(((LinkButton)sender).CommandArgument, perm);
        }

        protected void lb3_Click(object sender, EventArgs e)
        {
            string perm = "3";
            CambiarPermiso(((LinkButton)sender).CommandArgument, perm);
        }

        protected void lb4_Click(object sender, EventArgs e)
        {
            string perm = "4";
            CambiarPermiso(((LinkButton)sender).CommandArgument, perm);
        }

        protected void lb5_Click(object sender, EventArgs e)
        {
            string perm = "5";
            CambiarPermiso(((LinkButton)sender).CommandArgument, perm);
        }

        private void CambiarPermiso(string argumentos, string permiso)
        {
            string[] arguments = argumentos.Split(',');
            string strQuery = "SELECT * FROM permisos_perfiles " +
                "WHERE idPerfil = " + arguments[1] + " " +
                "AND idPagina = " + arguments[0];
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                switch (permiso)
                {
                    case "1":
                        if (dt.Rows[0]["SinPermiso"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "SinPermiso = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "SinPermiso = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    case "2":
                        if (dt.Rows[0]["Consulta"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Consulta = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Consulta = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    case "3":
                        if (dt.Rows[0]["Exportar"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Exportar = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Exportar = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    case "4":
                        if (dt.Rows[0]["CrearModificar"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "CrearModificar = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "CrearModificar = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    case "5":
                        if (dt.Rows[0]["Borrar"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Borrar = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Borrar = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    default:
                        break;
                }

                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, mysqlConexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }

                //OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
                //try
                //{
                //    OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                //    myConnection.Open();
                //    command.ExecuteNonQuery();
                //    command.Dispose();
                //    myConnection.Close();
                //}
                //catch (OdbcException ex)
                //{
                //    string mensaje = ex.Message;
                //    myConnection.Close();
                //    Response.Redirect("perfiles");
                //}
            }

            dt.Dispose();
            listaPaginas();
        }
    }
}