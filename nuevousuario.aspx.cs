using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                        //txbClave.Text = cg1.CreatePassword(8);
                        txbClave.Text = "Fitness2025";
                        CargarCargos();
                        CargarPerfiles();
                        CargarEmpleados();
                        CargarCanalesVenta();
                       
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
        private void CargarCanalesVenta()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCanalesVenta();

            ddlCanalVenta.DataSource = dt;
            ddlCanalVenta.DataTextField = "NombreCanalVenta";      
            ddlCanalVenta.DataValueField = "idCanalVenta";   
            ddlCanalVenta.DataBind();

            // 👇 Ahora sí puedes eliminar el 15
            ListItem item = ddlCanalVenta.Items.FindByValue("15");
            if (item != null)
            {
                ddlCanalVenta.Items.Remove(item);
            }

            dt.Dispose();
        }

        private void CargarPerfiles()
        {
            clasesglobales cg1 = new clasesglobales();

            int idPerfil = Convert.ToInt32(Session["idPerfil"]);

            DataTable dt = cg1.ConsultarPerfiles();
       
            int[] perfilesRestringidos = { 2, 4, 6, 10, 11, 24, 25, 36 };

            if (idPerfil == 2 || idPerfil == 11 || idPerfil == 36)
            {
                var perfilesPermitidos = dt.AsEnumerable()
                                           .Where(r => perfilesRestringidos.Contains(Convert.ToInt32(r["IdPerfil"])));

                if (perfilesPermitidos.Any())
                    dt = perfilesPermitidos.CopyToDataTable();
                else
                    dt = dt.Clone(); 
            }

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
            if (ExisteEmail(txbEmail.Text.Trim()))
            {
                string script = @"
                Swal.fire({
                    title: 'Correo ya registrado',
                    text: 'Un usuario con este correo ya existe.',
                    icon: 'warning',
                    confirmButtonText: 'Entendido'
                });
            ";

                ScriptManager.RegisterStartupScript(this, GetType(), "EmailExiste", script, true);
                return;
            }

            if (ExisteEmpleado(ddlEmpleados.SelectedValue))
            {
                string script = @"
                Swal.fire({
                    title: 'Empleado ya asignado',
                    text: 'Este empleado ya tiene un usuario asociado.',
                    icon: 'warning',
                    confirmButtonText: 'Entendido'
                });
            ";

                ScriptManager.RegisterStartupScript(this, GetType(), "EmpleadoExiste", script, true);
                return;
            }

            clasesglobales cg = new clasesglobales();




            try
            {
                
                string strHashClave = cg.ComputeSha256Hash(txbClave.Text.Trim());
                int idUsuario;
                string rta;

                idUsuario = cg.InsertarUsuario(txbEmail.Text.Trim(), strHashClave,  txbNombre.Text.Trim(), Convert.ToInt32(ddlCargo.SelectedValue), Convert.ToInt32(ddlPerfiles.SelectedValue), Convert.ToInt32(ddlEmpleados.SelectedValue),
                                1, "Activo", Convert.ToInt32(ddlCanalVenta.SelectedValue), out rta);

                if (rta == "OK")
                {
                    cg.InsertarLog(Session["idusuario"].ToString(), "usuarios", "Agrega", "El usuario agregó información del correo: " + txbEmail.Text + ".", "", "");

                    string script = @"
                    Swal.fire({
                        title: 'Usuario creado correctamente',
                        icon: 'success',
                        timer: 2500,
                        showConfirmButton: false,
                        timerProgressBar: true
                    }).then(() => {
                        window.location.href = 'usuarios.aspx';
                    });
                ";

                    ScriptManager.RegisterStartupScript(this, GetType(), "SuccessUsuario", script, true);
                }
                else
                {
                    string script = @"
                    Swal.fire({
                        title: 'Error',
                        text: '" + rta.Replace("'", "\\'") + @"',
                        icon: 'error'
                    });
                ";

                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorUsuario", script, true);
                }
            }
            catch (Exception ex)
            {
                string script = @"
                Swal.fire({
                    title: 'Error inesperado',
                    text: '" + ex.Message.Replace("'", "\\'") + @"',
                    icon: 'error'
                });
            ";

                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
            }
        }

        protected void ddlEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string documento = ddlEmpleados.SelectedValue;          

            if (!string.IsNullOrEmpty(documento))
            {
                CargarEmpleado(documento);
            }
            else
            {
                LimpiarCampos();
            }
        }

        private void CargarEmpleado(string documento)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEmpleado(documento);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                txbNombre.Text = row["NombreEmpleado"] != DBNull.Value
                                 ? row["NombreEmpleado"].ToString()
                                 : "";

                txbEmail.Text = row["EmailCorporativo"] != DBNull.Value
                                ? row["EmailCorporativo"].ToString()
                                : "";

                txbClave.Attributes["value"] = "Fitness2025";


                if (row["idCargo"] != DBNull.Value)
                {
                    string idCargo = row["idCargo"].ToString();
                    ListItem itemCargo = ddlCargo.Items.FindByValue(idCargo);

                    if (itemCargo != null)
                    {
                        ddlCargo.ClearSelection();
                        itemCargo.Selected = true;
                    }
                }


                if (row["idCanalVenta"] != DBNull.Value)
                {
                    string idCanal = row["idCanalVenta"].ToString();
                    ListItem itemCanal = ddlCanalVenta.Items.FindByValue(idCanal);

                    if (itemCanal != null)
                    {
                        ddlCanalVenta.ClearSelection();
                        itemCanal.Selected = true;
                    }
                }
            }
        }


        private void LimpiarCampos()
        {
            txbNombre.Text = "";
            txbEmail.Text = "";
            txbEmail.Text = "";
            txbClave.Text = "";
            ddlCanalVenta.SelectedIndex = 0;
            ddlCargo.SelectedIndex = 0;
        }



    }
}