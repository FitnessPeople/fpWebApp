using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class eliminarusuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Usuarios");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        txbEmail.Attributes.Add("type", "email");
                        txbClave.Attributes.Add("type", "password");

                        CargarPerfiles();
                        CargarEmpleados();
                        CargarDatosUsuario();
                    }
                    else
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
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

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ValidarPermisos(strPagina, Session["idPerfil"].ToString(), Session["idusuario"].ToString());

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

        private void CargarPerfiles()
        {
            string strQuery = "SELECT * FROM Perfiles";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlPerfiles.DataSource = dt;
            ddlPerfiles.DataBind();

            dt.Dispose();
        }

        private void CargarEmpleados()
        {
            string strQuery = "SELECT * FROM Empleados WHERE Estado = 'Activo' ORDER BY NombreEmpleado";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlEmpleados.DataSource = dt;
            ddlEmpleados.DataBind();

            dt.Dispose();
        }

        private void CargarDatosUsuario()
        {
            string strQuery = "SELECT * FROM Usuarios WHERE idUsuario = " + Request.QueryString["deleteid"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            txbNombre.Text = dt.Rows[0]["NombreUsuario"].ToString();
            txbCargo.Text = dt.Rows[0]["CargoUsuario"].ToString();
            txbEmail.Text = dt.Rows[0]["EmailUsuario"].ToString();
            txbClave.Text = dt.Rows[0]["ClaveUsuario"].ToString();
            ddlPerfiles.SelectedIndex = Convert.ToInt16(dt.Rows[0]["idPerfil"].ToString());
            if (dt.Rows[0]["idEmpleado"].ToString() != "")
            {
                ddlEmpleados.SelectedIndex = Convert.ToInt32(ddlEmpleados.Items.IndexOf(ddlEmpleados.Items.FindByValue(dt.Rows[0]["idEmpleado"].ToString())));
            }
            rblEstado.Items.FindByValue(dt.Rows[0]["EstadoUsuario"].ToString()).Selected = true;

            dt.Dispose();

            strQuery = "SELECT * FROM logs WHERE idUsuario = " + Request.QueryString["deleteid"].ToString();
            clasesglobales cg1 = new clasesglobales();
            DataTable dt1 = cg1.TraerDatos(strQuery);

            if (dt1.Rows.Count > 0)
            {
                ltMensaje.Text = "<div class=\"ibox-content\">" +
                    "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Este usuario no se puede borrar, contiene registros en el flujo de actividades." +
                    "</div></div>";
                btnEliminar.Enabled = false;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string strQuery = "DELETE FROM usuarios " +
                "WHERE idUsuario = " + Request.QueryString["deleteid"].ToString();
                clasesglobales cg = new clasesglobales();
                string mensaje = cg.TraerDatosStr(strQuery);
            }
            catch (SqlException ex)
            {
                string mensaje = ex.Message;
            }

            Response.Redirect("usuarios");
        }
    }
}