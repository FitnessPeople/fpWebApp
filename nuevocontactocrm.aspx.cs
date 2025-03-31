using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
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
                            
                            ListaEmpresasCMR();
                            ListaEstadosCMR();
                            ListaContactos();
                            
                        }
                       //ListaContactos();
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
                    txbTelefonoContacto.Value = row["TelefonoContacto"].ToString();
                    txbCorreoContacto.Value = row["EmailContacto"].ToString();
                    ddlEmpresa.SelectedIndex = Convert.ToInt32(ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(dt.Rows[0]["idEmpresaCmr"].ToString())));
                    ddlStatusLead.SelectedIndex = Convert.ToInt32(ddlStatusLead.Items.IndexOf(ddlStatusLead.Items.FindByValue(dt.Rows[0]["idEstado"].ToString())));
                    txbFechaPrim.Value = Convert.ToDateTime(row["FechaPrimerCon"]).ToString("yyyy-MM-dd");
                    txbFechaProx.Value = Convert.ToDateTime(row["FechaProximoCon"]).ToString("yyyy-MM-dd");
                    int ValorPropuesta = Convert.ToInt32(dt.Rows[0]["ValorPropuesta"]);
                    txbValorPropuesta.Text = ValorPropuesta.ToString("C0", new CultureInfo("es-CO"));               
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
    }
}