using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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
                            btnImprimir.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            btnImprimir.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                    
                    listaCiudades();
                    ListaDepartamentos();
                    ltTitulo.Text = "Agregar Ciudad";

                    if (Request.QueryString.Count > 0)
                    {
                        rpCiudad.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = new DataTable();
                            dt = cg.ConsultarCiudadesPorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                txbCiudad.Text = dt.Rows[0]["NombreCiudad"].ToString();
                                ddlDepartamentos.SelectedIndex = Convert.ToInt16(ddlDepartamentos.Items.IndexOf(ddlDepartamentos.Items.FindByValue(dt.Rows[0]["CodigoEstado"].ToString())));

                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar Ciudad";
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            DataTable dt1 = new DataTable();
                            clasesglobales cg = new clasesglobales();

                            dt1 = cg.validarCiudadTablas(Request.QueryString["deleteid"].ToString());
                            if (dt1.Rows.Count > 0)
                            {
                                ltMensaje.Text = "<div class=\"ibox-content\">" +
                                    "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                    "Esta ciudad no se puede borrar, hay registros asociados a ella." +
                                    "</div></div>";


                                DataTable dt = new DataTable();
                                dt = cg.ConsultarCiudadesPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt.Rows.Count > 0)
                                {
                                    txbCiudad.Text = dt.Rows[0]["NombreCiudad"].ToString();
                                    txbCiudad.Enabled = false;
                                    ddlDepartamentos.SelectedIndex = Convert.ToInt16(ddlDepartamentos.Items.IndexOf(ddlDepartamentos.Items.FindByValue(dt.Rows[0]["CodigoEstado"].ToString())));
                                    ddlDepartamentos.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    btnAgregar.Enabled = false;
                                    ltTitulo.Text = "Borrar Ciudad";
                                }
                            }
                            else
                            {
                                //Borrar
                                DataTable dt = new DataTable();
                                dt = cg.ConsultarCiudadesPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt.Rows.Count > 0)
                                {
                                    txbCiudad.Text = dt.Rows[0]["NombreCiudad"].ToString();
                                    txbCiudad.Enabled = false;
                                    ddlDepartamentos.SelectedIndex = Convert.ToInt16(ddlDepartamentos.Items.IndexOf(ddlDepartamentos.Items.FindByValue(dt.Rows[0]["CodigoEstado"].ToString())));
                                    ddlDepartamentos.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    ltTitulo.Text = "Borrar Ciudad";
                                }
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("logout");
                }
            }
        }

        private bool ValidarCiudad(string strNombre)
        {
            bool bExiste = false;
            DataTable dt = new DataTable();
            clasesglobales cg = new clasesglobales();
            dt = cg.ConsultarCiudadesPorNombre(strNombre);
            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }
            return bExiste;
        }

        private void ValidarPermisos(string strPagina)
        {
            ViewState["SinPermiso"] = "1";
            ViewState["Consulta"] = "0";
            ViewState["Exportar"] = "0";
            ViewState["CrearModificar"] = "0";
            ViewState["Borrar"] = "0";

            clasesglobales cg = new clasesglobales();            
            DataTable dt = cg.validarPermisos(strPagina, Session["idPerfil"].ToString(), Session["idusuario"].ToString());

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
            DataTable dt = cg.consultarDepartamentos();           
            ddlDepartamentos.DataSource = dt;
            ddlDepartamentos.DataBind();
            dt.Dispose();
        }

        private void listaCiudades()
        {            
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.consultarCiudades();
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
            clasesglobales cg = new clasesglobales();
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editid"] != null)
                {
                    string respuesta = cg.ActualizarCiudad(int.Parse(Request.QueryString["editid"].ToString()), txbCiudad.Text.ToString().Trim(), ddlDepartamentos.SelectedItem.Text.ToString(), ddlDepartamentos.SelectedItem.Value.ToString());
                    Response.Redirect("ciudades");
                }

                if (Request.QueryString["deleteid"] != null)
                {
                    string respuesta = cg.EliminarCiudad(int.Parse(Request.QueryString["deleteid"].ToString()));
                    //myConnection.Open();
                    //string strQuery = "DELETE FROM ciudades " +
                    //    "WHERE idCiudad = " + Request.QueryString["¨deleteid"].ToString();
                    //OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                    //command1.ExecuteNonQuery();
                    //command1.Dispose();
                    //myConnection.Close();
                    Response.Redirect("ciudades");
                }
            }
            else
            {
                if (!ValidarCiudad(txbCiudad.Text.ToString()))
                {
                    try
                    {
                        string respuesta = cg.InsertarCiudad(txbCiudad.Text.ToString().Trim(),"",ddlDepartamentos.SelectedItem.Text.ToString(),ddlDepartamentos.SelectedItem.Value.ToString(),"Colombia","Co");
                        myConnection.Open();

                        //StringBuilder sql = new StringBuilder();
                        //sql.Append("INSERT INTO fitnesspeople.ciudades (");
                        //sql.Append("NombreCiudad, CodigoCiudad, NombreEstado, ");
                        //sql.Append("CodigoEstado, NombrePais, CodigoPais)");
                        //sql.Append(" VALUES (");
                        //sql.Append("'" + txbCiudad.Text.ToString().Trim() + "', '0013', 'Departamento', ");
                        //sql.Append(" 'CodigoDepartamento', 'Colombia', 'Co');");

                        //string strQuery = sql.ToString();

                        //OdbcCommand command1 = new OdbcCommand(strQuery, myConnection);
                        //command1.ExecuteNonQuery();
                        //command1.Dispose();
                        //myConnection.Close();

                        Response.Redirect("ciudades");

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
    }
}