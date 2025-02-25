using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class nuevousuario : System.Web.UI.Page
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
                        clasesglobales cg1 = new clasesglobales();
                        txbClave.Text = cg1.CreatePassword(8);
                        CargarPerfiles();
                        CargarEmpleados();
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

        private bool ExisteEmail(string strEmail)
        {
            bool rta = false;
            string strQuery = "SELECT idUsuario FROM Usuarios WHERE EmailUsuario = '" + strEmail + "' ";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                rta = true;
            }

            dt.Dispose();
            return rta;
        }

        private bool ExisteEmpleado(string strIdEmpleado)
        {
            bool rta = false;
            string strQuery = "SELECT idUsuario FROM Usuarios WHERE idEmpleado = '" + strIdEmpleado + "' ";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                rta = true;
            }

            dt.Dispose();
            return rta;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validar si existe por Email o por Empleado
            if (ExisteEmail(txbEmail.Text.ToString().Trim()))
            {
                ltMensaje.Text = "<div class=\"ibox-content\">" +
                    "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Un usuario con este correo ya existe!" +
                    "</div></div>";
            }
            else
            {
                if (ExisteEmpleado(ddlEmpleados.SelectedItem.Value.ToString()))
                {
                    ltMensaje.Text = "<div class=\"ibox-content\">" +
                    "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Un usuario ya está asignado a este empleado." +
                    "</div></div>";
                }
                else
                {
                    try
                    {
                        string strQuery = "INSERT INTO usuarios " +
                        "(EmailUsuario, ClaveUsuario, NombreUsuario, CargoUsuario, idPerfil, idEmpleado, idEmpresa, EstadoUsuario) " +
                        "VALUES ('" + txbEmail.Text.ToString() + "', '" + txbClave.Text.ToString() + "', " +
                        "'" + txbNombre.Text.ToString() + "', '" + txbCargo.Text.ToString() + "', " +
                        "'" + ddlPerfiles.SelectedItem.Value.ToString() + "', '" + ddlEmpleados.SelectedItem.Value.ToString() + "', " +
                        "1, 'Activo') ";
                        clasesglobales cg = new clasesglobales();
                        string mensaje = cg.TraerDatosStr(strQuery);
                    }
                    catch (OdbcException ex)
                    {
                        string mensaje = ex.Message;
                    }

                    Response.Redirect("usuarios");
                }
            }
        }
    }
}