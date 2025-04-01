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
                            txbFechaPrim.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                            txbFechaProx.Attributes.Add("type", "date");
                            txbFechaProx.Value = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                            ListaEmpresasCMR();
                            ListaEstadosCMR();
                            ListaContactos();                            
                        }                      
                    }

                    if (Request.QueryString.Count > 0)
                    {
                        //rpSedes.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            ////Editar
                            //clasesglobales cg = new clasesglobales();
                            //DataTable dt = cg.ConsultarSedePorId(int.Parse(Request.QueryString["editid"].ToString()));
                            //if (dt.Rows.Count > 0)
                            //{
                            //    string contenidoEditor = hiddenEditor.Value;
                            //    txbSede.Text = dt.Rows[0]["NombreSede"].ToString();
                            //    txbDireccion.Text = dt.Rows[0]["DireccionSede"].ToString();
                            //    ddlCiudadSede.SelectedIndex = Convert.ToInt16(ddlCiudadSede.Items.IndexOf(ddlCiudadSede.Items.FindByValue(dt.Rows[0]["idCiudadSede"].ToString())));
                            //    txbTelefono.Text = dt.Rows[0]["TelefonoSede"].ToString();
                            //    hiddenEditor.Value = dt.Rows[0]["HorarioSede"].ToString();
                            //    rblTipoSede.SelectedValue = dt.Rows[0]["TipoSede"].ToString();
                            //    rblClaseSede.SelectedValue = dt.Rows[0]["ClaseSede"].ToString();
                            //    btnAgregar.Text = "Actualizar";
                            //    ltTitulo.Text = "Actualizar sede";
                            //}
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            //Borrar
                        }
                    }


                }
                else
                {
                    Response.StatusCode = 401;
                    Response.End();
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
            DataTable dt = cg.ConsultarContactosCMR();

            rpContactosCMR.DataSource = dt;
            rpContactosCMR.DataBind();

            dt.Dispose();
        }

        private void ListaEmpresasCMR()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEmpresasCMR();

            ddlEmpresa.DataSource = dt;
            ddlEmpresa.DataBind();
            dt.Dispose();            
        }

        private void ListaEstadosCMR()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEstadossCMR();

            ddlStatusLead.DataSource = dt;
            ddlStatusLead.DataBind();
            dt.Dispose();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Button btnEditar = (Button)sender;
            int idContacto = Convert.ToInt32(btnEditar.CommandArgument);

            if (idContacto > 0)
            {
                CargarDatosContacto(idContacto);
                upModal.Update();
                ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", "$('#ModalContacto').modal('show');", true);
            }
           
        }

        protected void rpContactosCMR_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
            DataTable dt = cg.ConsultarContactosCMRPorId(idContacto, out respuesta);

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
                    if (row["idEmpresaCmr"].ToString() != "")                    
                        ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(dt.Rows[0]["idEmpresaCmr"].ToString())));
                    else
                        ddlEmpresa.SelectedItem.Value = "0";
                    ddlStatusLead.SelectedIndex = Convert.ToInt32(ddlStatusLead.Items.IndexOf(ddlStatusLead.Items.FindByValue(dt.Rows[0]["idEstado"].ToString())));
                    txbFechaPrim.Value = Convert.ToDateTime(row["FechaPrimerCon"]).ToString("yyyy-MM-dd");
                    txbFechaProx.Value = Convert.ToDateTime(row["FechaProximoCon"]).ToString("yyyy-MM-dd");
                    int ValorPropuesta = Convert.ToInt32(dt.Rows[0]["ValorPropuesta"]);
                    txbValorPropuesta.Text = ValorPropuesta.ToString("C0", new CultureInfo("es-CO"));
                    string contenidoEditor = hiddenEditor.Value;
                    hiddenEditor.Value = row["observaciones"].ToString();
                }
            }
            else
            {
                DataRow row = dt.Rows[0];
                txbNombreContacto.Value = row["Error"].ToString(); ;
            }
        }



        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            int idContacto = Convert.ToInt32(btnEliminar.CommandArgument);

            if (idContacto > 0)
            {
                ltEliminar.Text = "<span style='color: red;'>¿Está seguro de eliminar el contacto</span>" ;
                CargarDatosContacto(idContacto);
                upEliminar.Update();
                ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", "$('#Modaleliminar').modal('show');", true);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            bool salida = false;            
            string mensaje = string.Empty;
            string respuesta = string.Empty;
           
            string contenidoEditor = hiddenEditor.Value;

            if (ddlEmpresa.SelectedItem.Value != "")
                ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(ddlEmpresa.SelectedItem.Value)));
            else
                ddlEmpresa.SelectedItem.Value = "0";

            clasesglobales cg = new clasesglobales();
            try
            {
                    respuesta = cg.InsertarContactoCMR(txbNombreContacto.Value.ToString().Trim(), txbTelefonoContacto.Value.ToString().Trim(),
                    txbCorreoContacto.Value.ToString().Trim(), Convert.ToInt32(ddlEmpresa.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlStatusLead.SelectedItem.Value.ToString()), txbFechaPrim.Value.ToString(),
                    txbFechaProx.Value.ToString(), Convert.ToInt32(Regex.Replace(txbValorPropuesta.Text, @"[^\d]", "")), "", contenidoEditor,
                    Convert.ToInt32(Session["idUsuario"]), out salida, out mensaje);
                    
                    if (salida)
                    {
                        respuesta = mensaje.ToString();
                        Response.Redirect("nuevocontactocrm");
                    }
                    else
                    {
                        string script = $"alert('{mensaje.Replace("'", "\\'")}');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensaje", script, true);
                        Response.Redirect("nuevocontactocrm");
                }
               

            }
            catch (Exception ex)
            {
                string script = $"alert('{mensaje.Replace("'", "\\'")}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensaje", script, true);
                Response.Redirect("nuevocontactocrm");
            }
        }
    }
}