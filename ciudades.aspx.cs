using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

namespace fpWebApp
{
    public partial class ciudades : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Ciudades");
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
                            //btnImprimir.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //btnImprimir.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                    
                    ListaCiudades();
                    ListaDepartamentos();
                    ltTitulo.Text = "Agregar Ciudad";

                    if (Request.QueryString.Count > 0)
                    {
                        rpCiudad.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarCiudadesPorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                txbCiudad.Text = dt.Rows[0]["NombreCiudad"].ToString();
                                ddlDepartamentos.SelectedIndex = Convert.ToInt16(ddlDepartamentos.Items.IndexOf(ddlDepartamentos.Items.FindByValue(dt.Rows[0]["CodigoEstado"].ToString())));

                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar Ciudad";
                            }
                            dt.Dispose();
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ValidarCiudadTablas(Request.QueryString["deleteid"].ToString());
                            if (dt.Rows.Count > 0)
                            {
                                ltMensaje.Text = "<div class=\"ibox-content\">" +
                                    "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                    "Esta ciudad no se puede borrar, hay registros asociados a ella." +
                                    "</div></div>";

                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarCiudadesPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    txbCiudad.Text = dt1.Rows[0]["NombreCiudad"].ToString();
                                    txbCiudad.Enabled = false;
                                    ddlDepartamentos.SelectedIndex = Convert.ToInt16(ddlDepartamentos.Items.IndexOf(ddlDepartamentos.Items.FindByValue(dt1.Rows[0]["CodigoEstado"].ToString())));
                                    ddlDepartamentos.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    btnAgregar.Enabled = false;
                                    ltTitulo.Text = "Borrar Ciudad";
                                }
                                dt1.Dispose();
                            }
                            else
                            {
                                //Borrar
                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarCiudadesPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    txbCiudad.Text = dt.Rows[0]["NombreCiudad"].ToString();
                                    txbCiudad.Enabled = false;
                                    ddlDepartamentos.SelectedIndex = Convert.ToInt16(ddlDepartamentos.Items.IndexOf(ddlDepartamentos.Items.FindByValue(dt1.Rows[0]["CodigoEstado"].ToString())));
                                    ddlDepartamentos.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    ltTitulo.Text = "Borrar Ciudad";
                                }
                                dt1.Dispose();
                            }
                            dt.Dispose();
                        }
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

        private void ListaDepartamentos()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarDepartamentos();           
            ddlDepartamentos.DataSource = dt;
            ddlDepartamentos.DataBind();
            dt.Dispose();
        }

        private void ListaCiudades()
        {            
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCiudades();
            rpCiudad.DataSource = dt;
            rpCiudad.DataBind();
            dt.Dispose();
        }

        protected void rpCiudad_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "ciudades?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "ciudades?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
            }
        }

        private bool ValidarCiudad(string strNombre)
        {
            bool bExiste = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCiudadesPorNombre(strNombre);
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
                    string respuesta = cg.ActualizarCiudad(int.Parse(Request.QueryString["editid"].ToString()), txbCiudad.Text.ToString().Trim(), ddlDepartamentos.SelectedItem.Text.ToString(), ddlDepartamentos.SelectedItem.Value.ToString());
                }

                if (Request.QueryString["deleteid"] != null)
                {
                    string respuesta = cg.EliminarCiudad(int.Parse(Request.QueryString["deleteid"].ToString()));
                }
                Response.Redirect("ciudades");
            }
            else
            {
                if (!ValidarCiudad(txbCiudad.Text.ToString()))
                {
                    try
                    {
                        string respuesta = cg.InsertarCiudad(txbCiudad.Text.ToString().Trim(),"",ddlDepartamentos.SelectedItem.Text.ToString(),ddlDepartamentos.SelectedItem.Value.ToString(),"Colombia","Co");
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
                    Response.Redirect("ciudades");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Ya existe una Ciudad con ese nombre." +
                    "</div>";
                }
            }
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                clasesglobales cg = new clasesglobales();
                dt = cg.ConsultarCiudades();

                using (XLWorkbook libro = new XLWorkbook())
                {
                    var hoja = libro.Worksheets.Add(dt, "Ciudades");
                    hoja.ColumnsUsed().AdjustToContents();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ciudades.xlsx");
                    using (MemoryStream myMemoryStream = new MemoryStream())
                    {
                        libro.SaveAs(myMemoryStream);
                        Response.BinaryWrite(myMemoryStream.ToArray());
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}