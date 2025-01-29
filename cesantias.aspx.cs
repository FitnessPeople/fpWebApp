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
    public partial class cesantias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Cesantias");
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
                    listaCesantias();
                    ltTitulo.Text = "Agregar fondo de cesantías";
                }
                else
                {
                    Response.Redirect("logout.aspx");
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

        private void listaCesantias()
        {
            string strQuery = "SELECT * FROM Cesantias";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpCesantias.DataSource = dt;
            rpCesantias.DataBind();

            dt.Dispose();
        }

        private bool ValidarCesantias(string strNombre)
        {
            bool bExiste = false;

            string strQuery = "SELECT * FROM Cesantias WHERE NombreCesantias = '" + strNombre.Trim() + "' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }

            return bExiste;
        }

        private bool ValidarIdCesantias(string idCesantias)
        {
            bool bExiste = false;

            string strQuery = "SELECT * FROM Empleados WHERE idCesantias = " + idCesantias + " ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }

            return bExiste;
        }

        protected void rpCesantias_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    LinkButton lbEditar = (LinkButton)e.Item.FindControl("lbEditar");
                    lbEditar.CommandArgument = ((DataRowView)e.Item.DataItem).Row[0].ToString();
                    lbEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    LinkButton lbEliminar = (LinkButton)e.Item.FindControl("lbEliminar");
                    lbEliminar.CommandArgument = ((DataRowView)e.Item.DataItem).Row[0].ToString();
                    lbEliminar.Visible = true;
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
            if (btnAgregar.Text == "Actualizar")
            {
                string strnombre = txbCesantias.Text.ToString().Replace("'", "");
                if (strnombre.Length > 2)
                {
                    myConnection.Open();
                    string strQuery = "UPDATE Cesantias " +
                        "SET NombreCesantias = '" + strnombre + "' " +
                        "WHERE idCesantias = " + ViewState["idCesantias"].ToString();
                    OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    myConnection.Close();

                    //Response.Redirect("cesantias");
                    listaCesantias();
                    btnCancelar_Click(sender, e);
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "El nombre del fondo de cesantías no puede ser vacío y debe tener al menos 3 caracteres." +
                        "</div>";
                }
            }
            else
            {
                if (btnAgregar.Text == "Confirmar borrado")
                {
                    //Buscar idCesantias en tabla Empleados, si existe, no se puede borrar.
                    if (!ValidarIdCesantias(ViewState["idCesantias"].ToString()))
                    {
                        myConnection.Open();
                        string strQuery = "DELETE FROM Cesantias " +
                            "WHERE idCesantias = " + ViewState["idCesantias"].ToString();
                        OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                        command1.ExecuteNonQuery();
                        command1.Dispose();
                        myConnection.Close();

                        //Response.Redirect("cesantias");
                        listaCesantias();
                        btnCancelar_Click(sender, e);
                    }
                    else
                    {
                        ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                            "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                            "Este fondo de cesantías está asociado al menos a un empleado. No se puede eliminar." +
                            "</div>";
                    }
                }
                else
                {
                    if (btnAgregar.Text == "Agregar")
                    {
                        string strnombre = txbCesantias.Text.ToString().Replace("'", "");
                        if (strnombre.Length > 2)
                        {
                            if (!ValidarCesantias(strnombre))
                            {
                                myConnection.Open();
                                string strQuery = "INSERT INTO Cesantias " +
                                    "(NombreCesantias) VALUES ('" + strnombre + "') ";
                                OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                                command1.ExecuteNonQuery();
                                command1.Dispose();
                                myConnection.Close();

                                //Response.Redirect("cesantias");
                                listaCesantias();
                                btnCancelar_Click(sender, e);
                            }
                            else
                            {
                                ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                    "Ya existe un fondo de cesantías con ese nombre." +
                                    "</div>";
                            }
                        }
                        else
                        {
                            ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                                "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                "El nombre del fondo de cesantías no puede ser vacío y debe tener al menos 3 caracteres." +
                                "</div>";
                        }
                    }
                }
            }
        }

        protected void lbEliminar_Click(object sender, EventArgs e)
        {
            ViewState["idCesantias"] = ((LinkButton)sender).CommandArgument;
            string strQuery = "SELECT * FROM Cesantias WHERE idCesantias = " + ((LinkButton)sender).CommandArgument;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
            if (dt.Rows.Count > 0)
            {
                txbCesantias.Text = dt.Rows[0]["NombreCesantias"].ToString();
                txbCesantias.Enabled = false;
                btnAgregar.Text = "Confirmar borrado";
                ltTitulo.Text = "Borrar fondo de cesantías";
            }
        }

        protected void lbEditar_Click(object sender, EventArgs e)
        {
            ViewState["idCesantias"] = ((LinkButton)sender).CommandArgument;
            string strQuery = "SELECT * FROM Cesantias WHERE idCesantias = " + ((LinkButton)sender).CommandArgument;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
            if (dt.Rows.Count > 0)
            {
                txbCesantias.Text = dt.Rows[0]["NombreCesantias"].ToString();
                btnAgregar.Text = "Actualizar";
                ltTitulo.Text = "Actualizar fondo de cesantías";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txbCesantias.Text = "";
            txbCesantias.Enabled = true;
            listaCesantias();
            ltMensaje.Text = "";
            btnAgregar.Text = "Agregar";
        }
    }
}