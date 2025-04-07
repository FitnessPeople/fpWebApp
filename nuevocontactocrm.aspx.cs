using DocumentFormat.OpenXml.Math;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace fpWebApp
{
    public partial class nuevocontactocrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ListaContactos();
                if (ViewState["AbrirModal"] != null && (bool)ViewState["AbrirModal"] == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "$('#ModalContacto').modal('show');", true);
                    ViewState["AbrirModal"] = null; // Limpiar para que no se repita
                }

                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Historias clinicas");
                    clasesglobales cg = new clasesglobales();
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

                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            //lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            //lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {


                            txbFechaPrim.Attributes.Add("type", "date");
                            txbFechaPrim.Attributes.Add("min", DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd").ToString());
                            txbFechaPrim.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                            txbFechaProx.Attributes.Add("type", "date");
                            txbFechaProx.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                            txbCorreoContacto.Attributes.Add("type", "email");

                            ListaEmpresasCRM();
                            ListaEstadosCRM();
                            ListaContactos();
                        }
                    }
                }
                else
                {
                    Response.StatusCode = 401;
                    Response.End();
                    Response.Redirect("logout.aspx");
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "updateDDL", "changeBadge(document.getElementById('" + ddlStatusLead.ClientID + "'));", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "activarBoton", "setTimeout(validarBotonActualizar, 100);", true);

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

        private void ListaContactos()
        {
            decimal valorTotal = 0;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarContactosCRM(out valorTotal);

            rpContactosCRM.DataSource = dt;
            rpContactosCRM.DataBind();

            ltValorTotal.Text = valorTotal.ToString("C0");
            dt.Dispose();
        }

        private void ListaEmpresasCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEmpresasCRM();

            ddlEmpresa.DataSource = dt;
            ddlEmpresa.DataBind();
            dt.Dispose();
        }

        private void ListaEstadosCRM()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstadossCRM();

            ddlStatusLead.DataSource = dt;
            ddlStatusLead.DataBind();
            dt.Dispose();
        }
        protected void rpContactosCRM_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"]?.ToString() == "1")
                {
                    Button btnEditar = (Button)e.Item.FindControl("btnEditar");
                    if (btnEditar != null)
                    {
                        btnEditar.Visible = true;
                    }
                }
            }
        }

        private void CargarDatosContacto(int idContacto)
        {
            bool respuesta = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarContactosCRMPorId(idContacto, out respuesta);
            Session["contactoId"] = idContacto;

            if (respuesta)
            {

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    txbNombreContacto.Value = row["NombreContacto"].ToString();
                    string telefono = Convert.ToString(row["TelefonoContacto"]);
                    if (!string.IsNullOrEmpty(telefono) && telefono.Length == 10)
                    {
                        txbTelefonoContacto.Value = $"{telefono.Substring(0, 3)} {telefono.Substring(3, 3)} {telefono.Substring(6, 4)}";
                    }
                    else
                    {
                        txbTelefonoContacto.Value = row["TelefonoContacto"].ToString();
                    }
                    txbCorreoContacto.Value = row["EmailContacto"].ToString();
                    if (row["idEmpresaCRM"].ToString() != "")
                        ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(dt.Rows[0]["idEmpresaCRM"].ToString())));
                    else
                        ddlEmpresa.SelectedItem.Value = "0";

                    ddlStatusLead.SelectedIndex = Convert.ToInt32(ddlStatusLead.Items.IndexOf(ddlStatusLead.Items.FindByValue(dt.Rows[0]["idEstadoCRM"].ToString())));
                    txbFechaPrim.Value = Convert.ToDateTime(row["FechaPrimerCon"]).ToString("yyyy-MM-dd");
                    txbFechaProx.Value = Convert.ToDateTime(row["FechaProximoCon"]).ToString("yyyy-MM-dd");
                    int ValorPropuesta = Convert.ToInt32(dt.Rows[0]["ValorPropuesta"]);
                    txbValorPropuesta.Text = ValorPropuesta.ToString("C0", new CultureInfo("es-CO"));
                    txaObservaciones.Value = row["observaciones"].ToString();
                }
            }
            else
            {
                DataRow row = dt.Rows[0];
                txbNombreContacto.Value = row["Error"].ToString(); ;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            bool salida = false;
            ViewState["AbrirModal"] = true;
            string mensaje = string.Empty;
            string mensajeValidacion = string.Empty;
            string respuesta = string.Empty;

            if (ddlEmpresa.SelectedItem.Value != "")
                ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(ddlEmpresa.SelectedItem.Value)));
            else
                ddlEmpresa.SelectedItem.Value = "0";

            clasesglobales cg = new clasesglobales();
            try
            {
                respuesta = cg.InsertarContactoCRM(txbNombreContacto.Value.ToString().Trim(), Regex.Replace(txbTelefonoContacto.Value.ToString().Trim(), @"\D", ""),
                txbCorreoContacto.Value.ToString().Trim(), Convert.ToInt32(ddlEmpresa.SelectedItem.Value.ToString()),
                Convert.ToInt32(ddlStatusLead.SelectedItem.Value.ToString()), txbFechaPrim.Value.ToString(),
                txbFechaProx.Value.ToString(), Convert.ToInt32(Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "")), "",
                txaObservaciones.Value.ToString(), Convert.ToInt32(Session["idUsuario"]), out salida, out mensaje);

                if (salida)
                {
                    respuesta = mensaje.ToString();
                    Response.Redirect("nuevocontactocrm", false);
                }
                else
                {
                    string script = $@"
                    alert('{mensaje.Replace("'", "\\'")}');    $('#ModalContacto').modal('show');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);

                }

            }
            catch (Exception ex)
            {
                string script = $"alert('{mensaje.Replace("'", "\\'")}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensaje", script, true);
            }
        }

        //[WebMethod]
        //public static string ValidarTelefono(string telefono)
        //{
        //    string resultado = "ok";
        //    string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

        //    using (MySqlConnection conexion = new MySqlConnection(strConexion))
        //    {
        //        conexion.Open();
        //        string query = "SELECT NombreContacto FROM contactoscrm WHERE TelefonoContacto = @telefono LIMIT 1";

        //        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
        //        {
        //            cmd.Parameters.AddWithValue("@telefono", telefono);
        //            object nombre = cmd.ExecuteScalar();

        //            if (nombre != null)
        //            {
        //                resultado = $"El teléfono ya está registrado a nombre de: {nombre.ToString()}";
        //            }
        //        }
        //    }

        //    return resultado;
        //}


        protected void btnEditar_Click(object sender, EventArgs e)
        {
            btnActualizar.Visible = true;
            btnAgregar.Visible = false;
            Button btnEditar = (Button)sender;
            int idContacto = Convert.ToInt32(btnEditar.CommandArgument);

            if (idContacto > 0)
            {
                CargarDatosContacto(idContacto);
                upModal.Update();
                ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", "$('#ModalContacto').modal('show');", true);
            }
        }
        private void MostrarModalEditar(int idContacto)
        {
            CargarDatosContacto(idContacto);
            upModal.Update();
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", "$('#ModalContacto').modal('show');", true);
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            bool salida = false;
            string mensaje = string.Empty;
            string respuesta = string.Empty;
            string mensajeValidacion = string.Empty;

            if (ddlEmpresa.SelectedItem.Value != "")
                ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(ddlEmpresa.SelectedItem.Value)));
            else
                ddlEmpresa.SelectedItem.Value = "0";

            clasesglobales cg = new clasesglobales();
            try
            {
                // Obtener y limpiar valores
                string nombre = txbNombreContacto.Value?.ToString().Trim();
                string telefono = Regex.Replace(txbTelefonoContacto.Value?.ToString().Trim(), @"\D", "");
                string correo = txbCorreoContacto.Value?.ToString().Trim();
                string fechaPrim = txbFechaPrim?.Value?.ToString().Trim();
                string fechaProx = txbFechaProx?.Value?.ToString().Trim();
                string valorPropuestaTexto = Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "");
                string empresa = ddlEmpresa.SelectedItem?.Value;
                string statusLead = ddlStatusLead.SelectedItem?.Value;

                // Validar campos requeridos
                if (string.IsNullOrWhiteSpace(nombre) ||
                    string.IsNullOrWhiteSpace(telefono) ||
                    string.IsNullOrWhiteSpace(correo) ||
                    string.IsNullOrWhiteSpace(empresa) ||
                    string.IsNullOrWhiteSpace(statusLead) ||
                    string.IsNullOrWhiteSpace(fechaPrim) ||
                    string.IsNullOrWhiteSpace(fechaProx) ||
                    string.IsNullOrWhiteSpace(valorPropuestaTexto))
                {
                    mensajeValidacion = "Todos los campos son obligatorios.";

                    ltMensajeVal.Text = "<div class='alert alert-danger'>Todos los campos son obligatorios.</div>";
                    MostrarModalEditar(Convert.ToInt32(Session["contactoId"]));
                    return;
                }
                else
                {
                    respuesta = cg.ActualizarContactoCRM(Convert.ToInt32(Session["contactoId"].ToString()), txbNombreContacto.Value.ToString().Trim(),
                            Regex.Replace(txbTelefonoContacto.Value.ToString().Trim(), @"\D", ""), txbCorreoContacto.Value.ToString().Trim(),
                            Convert.ToInt32(ddlEmpresa.SelectedItem.Value.ToString()), Convert.ToInt32(ddlStatusLead.SelectedItem.Value.ToString()),
                            txbFechaPrim.Value.ToString(), txbFechaProx.Value.ToString(), Convert.ToInt32(Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "")), "",
                            txaObservaciones.Value.ToString(), Convert.ToInt32(Session["idUsuario"]), out salida, out mensaje);

                    if (salida)
                    {
                        respuesta = mensaje.ToString();
                        Response.Redirect("nuevocontactocrm", false);
                    }
                    else
                    {
                        string script = $"alert('{mensaje.Replace("'", "\\'")}');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensaje", script, true);
                    }
                }
            }

            catch (Exception ex)
            {
                string script = $"alert('{mensaje.Replace("'", "\\'")}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensaje", script, true);
            }

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            bool respuesta = false;
            clasesglobales cg = new clasesglobales();

            int idContacto = Convert.ToInt32(btnEliminar.CommandArgument);

            DataTable dt = cg.ConsultarContactosCRMPorId(idContacto, out respuesta);
            Session["Contacto"] = dt.Rows[0]["NombreContacto"].ToString();


            if (idContacto > 0)
            {
                Session["contactoId"] = idContacto;
                ltEliminar.Text = "<span style='color: red;'>¿Está seguro de eliminar el contacto de : " + Session["Contacto"] + "</span>";
                upEliminar.Update();
                ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", "$('#Modaleliminar').modal('show');", true);
            }
        }

        protected void btnAccionEliminar_Click(object sender, EventArgs e)
        {
            ltEliminar.Text = string.Empty;
            bool respuesta = false;
            string mensaje = string.Empty;
            int idContacto = Convert.ToInt32(Session["contactoId"]);
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarContactosCRMPorId(idContacto, out respuesta);
            Session["contactoId"] = idContacto;

            if (idContacto > 0)
            {
                cg.EliminarContactoCRM(idContacto, out respuesta, out mensaje);
                ltEliminar.Text = "<span style='color: red;'>¿Está seguro de eliminar el contacto de : " + Session["Contacto"] + "</span>";
            }


        }
    }
}