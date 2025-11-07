using System;
using System.Data;
using System.Data.SqlClient;

namespace fpWebApp
{
    public partial class editarusuario : System.Web.UI.Page
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
                        CargarCargos();
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

        private void CargarCargos()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCargos();
            ddlCargo.DataSource = dt;
            ddlCargo.DataBind();
            dt.Dispose();
        }

        private void CargarPerfiles()
        {
            string strQuery = "SELECT * FROM Perfiles ORDER BY Perfil";
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
            string strQuery = "SELECT * FROM Usuarios WHERE idUsuario = " + Request.QueryString["editid"].ToString();
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            txbNombre.Text = dt.Rows[0]["NombreUsuario"].ToString();
            if (dt.Rows[0]["idCargoUsuario"].ToString() != "")
            {
                ddlCargo.SelectedIndex = Convert.ToInt32(ddlCargo.Items.IndexOf(ddlCargo.Items.FindByValue(dt.Rows[0]["idCargoUsuario"].ToString())));
            }
            txbEmail.Text = dt.Rows[0]["EmailUsuario"].ToString();
            //txbClave.Text = dt.Rows[0]["ClaveUsuario"].ToString();
            ddlPerfiles.SelectedIndex = Convert.ToInt16(dt.Rows[0]["idPerfil"].ToString());
            if (dt.Rows[0]["idEmpleado"].ToString() != "")
            {
                ddlEmpleados.SelectedIndex = Convert.ToInt32(ddlEmpleados.Items.IndexOf(ddlEmpleados.Items.FindByValue(dt.Rows[0]["idEmpleado"].ToString())));
            }
            rblEstado.Items.FindByValue(dt.Rows[0]["EstadoUsuario"].ToString()).Selected = true;

            dt.Dispose();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            string strInitData = TraerData();

            try
            {
                clasesglobales cg = new clasesglobales();
                string strHashClave = cg.ComputeSha256Hash(txbClave.Text.ToString());

                string strQuery = "UPDATE usuarios SET " +
                "EmailUsuario = '" + txbEmail.Text.ToString() + "', ClaveUsuario = '" + strHashClave + "', " +
                "NombreUsuario = '" + txbNombre.Text.ToString() + "', idCargoUsuario = " + ddlCargo.SelectedItem.Value.ToString() + ", " +
                "idPerfil = " + ddlPerfiles.SelectedItem.Value.ToString() + ", idEmpleado = '" + ddlEmpleados.SelectedItem.Value.ToString() + "', " +
                "EstadoUsuario = '" + rblEstado.SelectedItem.Value.ToString() + "' " +
                "WHERE idUsuario = " + Request.QueryString["editid"].ToString();
                
                string mensaje = cg.TraerDatosStr(strQuery);

                string strNewData = TraerData();

                cg.InsertarLog(Session["idusuario"].ToString(), "usuarios", "Modifica", "El usuario modificó información del correo: " + txbEmail.Text.ToString() + ".", strInitData, strNewData);
            }
            catch (SqlException ex)
            {
                string mensaje = ex.Message;
            }

            Response.Redirect("usuarios");
        }

        private string TraerData()
        {
            string strQuery = "SELECT * FROM usuarios WHERE idUsuario = " + Request.QueryString["editid"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();

            return strData;
        }
    }
}
