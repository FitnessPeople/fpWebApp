using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace fpWebApp
{
    public partial class genero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Género");
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
                            lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                    ListaGeneros();
                    ltTitulo.Text = "Agregar Género";

                    if (Request.QueryString.Count > 0)
                    {
                        rpGenero.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarGeneroPorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                txbGenero.Text = dt.Rows[0]["Genero"].ToString();
                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar Género";
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ValidarGeneroTablas(Request.QueryString["deleteid"].ToString());
                            if (dt.Rows.Count > 0)
                            {
                                ltMensaje.Text = "<div class=\"ibox-content\">" +
                                    "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                    "Esta Género no se puede borrar, hay registros asociados a ella." +
                                    "</div></div>";

                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarGeneroPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    txbGenero.Text = dt1.Rows[0]["Genero"].ToString();
                                    txbGenero.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    btnAgregar.Enabled = false;
                                    ltTitulo.Text = "Borrar Género";
                                }
                                dt1.Dispose();
                            }
                            else
                            {
                                //Borrar
                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarGeneroPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    txbGenero.Text = dt1.Rows[0]["Genero"].ToString();
                                    txbGenero.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    ltTitulo.Text = "Borrar Género";
                                }
                                dt1.Dispose();
                            }
                        }
                    }
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

        private void ListaGeneros()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarGeneros();
            rpGenero.DataSource = dt;
            rpGenero.DataBind();
            dt.Dispose();
        }

        protected void rpGenero_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "genero?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "genero?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
            }

        }


        private bool ValidarGenero(string strNombre)
        {
            bool bExiste = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarGenerosPorNombre(strNombre);
            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }
            dt.Dispose();
            return bExiste;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editid"] != null)
                {
                    string respuesta = cg.ActualizarGenero(int.Parse(Request.QueryString["editid"].ToString()), txbGenero.Text.ToString().Trim());
                }
                if (Request.QueryString["deleteid"] != null)
                {
                    string respuesta = cg.EliminarGenero(int.Parse(Request.QueryString["deleteid"].ToString()));
                }
                Response.Redirect("genero");
            }
            else
            {
                if (!ValidarGenero(txbGenero.Text.ToString()))
                {
                    try
                    {
                        string respuesta = cg.InsertarGenero(txbGenero.Text.ToString().Trim());
                    }
                    catch (Exception ex)
                    {
                        string mensajeExcepcionInterna = string.Empty;
                        Console.WriteLine(ex.Message);
                        if (ex.InnerException != null)
                        {
                            mensajeExcepcionInterna = ex.InnerException.Message;
                            Console.WriteLine("Mensaje de la excepción interna: " + mensajeExcepcionInterna);
                        }
                        ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Excepción interna." +
                        "</div>";
                    }
                    Response.Redirect("genero");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Ya existe un género con ese nombre." +
                        "</div>";
                }
            }
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {

        }
    }
}