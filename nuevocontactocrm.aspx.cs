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
                        //btnAgregar.Visible = false;
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
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarContactosCRM();

            rpContactosCRM.DataSource = dt;
            rpContactosCRM.DataBind();

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

            if (respuesta) {
               
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    txbNombreContacto.Value = row["NombreContacto"].ToString();
                    string telefono = Convert.ToString(row["TelefonoContacto"]);
                    if (!string.IsNullOrEmpty(telefono) && telefono.Length == 10) { 
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
            string mensaje = string.Empty;
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
                    string script = $"alert('{mensaje.Replace("'", "\\'")}');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensaje", script, true);
                }
            }
            catch (Exception ex)
            {
                string script = $"alert('{mensaje.Replace("'", "\\'")}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensaje", script, true);
            }
        }

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

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            bool salida = false;
            string mensaje = string.Empty;
            string respuesta = string.Empty;
            

            if (ddlEmpresa.SelectedItem.Value != "")
                ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(ddlEmpresa.SelectedItem.Value)));
            else
                ddlEmpresa.SelectedItem.Value = "0";

            clasesglobales cg = new clasesglobales();
            try
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

            catch (Exception ex)
            {
                string script = $"alert('{mensaje.Replace("'", "\\'")}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensaje", script, true);
            }

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            btnActualizar.Visible = true;
            btnAgregar.Visible = false;
            int idContacto = Convert.ToInt32(btnEliminar.CommandArgument);

            if (idContacto > 0)
            {
                ltEliminar.Text = "<span style='color: red;'>¿Está seguro de eliminar el contacto</span>" ;
                CargarDatosContacto(idContacto);
                upEliminar.Update();
                ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", "$('#Modaleliminar').modal('show');", true);
            }
        }
    }
}