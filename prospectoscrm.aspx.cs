using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class prospectoscrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Prospectos");
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
                        btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                    ListaProspectos();
                    CargarTipoDocumento();

                    ltTitulo.Text = "Agregar prospecto";

                    if (Request.QueryString.Count > 0)
                    {
                        //rpProspectos.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarEpsPorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                txbNombreContacto.Text = dt.Rows[0]["NombreEps"].ToString();
                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar EPS";
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ValidarEpsTablas(int.Parse(Request.QueryString["deleteid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                ltMensaje.Text = "<div class=\"ibox-content\">" +
                                    "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                    "Esta EPS no se puede borrar, hay empleados asociados a ella." +
                                    "</div></div>";

                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarEpsPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    txbNombreContacto.Text = dt1.Rows[0]["NombreEps"].ToString();
                                    txbNombreContacto.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    btnAgregar.Enabled = false;
                                    ltTitulo.Text = "Borrar EPS";
                                }
                                dt1.Dispose();
                            }
                            else
                            {
                                //Borrar
                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarEpsPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    txbNombreContacto.Text = dt1.Rows[0]["NombreEps"].ToString();
                                    txbNombreContacto.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    ltTitulo.Text = "Borrar EPS";
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

        private void ListaProspectos()
        {
            clasesglobales cg = new clasesglobales();
            //DataTable dt = cg.ConsultarEpss();

            string strQuery = "SELECT *, DATEDIFF(FechaHoraPregestion, CURDATE()) AS hacecuanto " +
                "FROM pregestioncrm pg, tiposgestioncrm tg " +
                "WHERE pg.idTipoGestion = 4 " +
                "AND pg.idTipoGestion = tg.idTipoGestionCRM ";
            DataTable dt = cg.TraerDatos(strQuery);

            //rpProspectos.DataSource = dt;
            //rpProspectos.DataBind();
            dt.Dispose();
        }

        private void CargarTipoDocumento()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultartiposDocumento();

            ddlTipoDocumento.DataSource = dt;
            ddlTipoDocumento.DataBind();

            dt.Dispose();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string strQuery = @"INSERT INTO pregestioncrm 
                (FechaHoraPregestion, NombreContacto, ApellidoContacto, DocumentoContacto, 
                idTipoDocumentoContacto, CelularContacto, idTipoGestion, idCanalVenta, idUsuarioAsigna) 
                VALUES (NOW(), @Nombre, @Apellido, @Documento, 
                @TipoDoc, @Celular, @TipoGestion, @IdCanalVenta, @IdUsuarioAsigna)";

            string connString = ConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
            string tipoGestion = "4";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string nombre = txbNombreContacto.Text.ToString();
                string apellido = txbApellidoContacto.Text.ToString();
                string documento = txbDocumento.Text.ToString();
                string idTipoDocumento = ddlTipoDocumento.SelectedItem.Value.ToString();
                string celular = txbCelular.Text.ToString();

                using (MySqlCommand cmd = new MySqlCommand(strQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellido", apellido);
                    cmd.Parameters.AddWithValue("@Documento", documento);
                    cmd.Parameters.AddWithValue("@TipoDoc", idTipoDocumento);
                    cmd.Parameters.AddWithValue("@Celular", celular);
                    cmd.Parameters.AddWithValue("@TipoGestion", tipoGestion);
                    cmd.Parameters.AddWithValue("@IdCanalVenta", Session["idCanalVenta"].ToString());
                    cmd.Parameters.AddWithValue("@IdUsuarioAsigna", Session["idUsuario"].ToString());

                    cmd.ExecuteNonQuery();

                    Response.Redirect("prospectocrm");
                }
            }
        }

        protected void gvAfiliados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAfiliados.PageIndex = e.NewPageIndex;

            //CargarCanalesVenta();
            if (Session["idSede"].ToString() == "11") // Usuario de Sede Administrativa (11)
            {
                //listaAfiliados("Todas");
            }
            else
            {
                //listaAfiliados(Session["idSede"].ToString());
            }
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAgregarDatos_Click(object sender, EventArgs e)
        {

        }

        protected void rblPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}