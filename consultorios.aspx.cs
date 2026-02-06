using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class consultorios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Consultorios");
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
                    ListaConsultorios();
                    CargarSedes();
                    ltTitulo.Text = "Agregar consultorio";

                    if (Request.QueryString.Count > 0)
                    {
                        rpConsultorios.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ConsultarConsultorioPorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                txbConsultorio.Text = dt.Rows[0]["NombreConsultorio"].ToString();
                                ddlSedes.SelectedIndex = Convert.ToInt32(ddlSedes.Items.IndexOf(ddlSedes.Items.FindByValue(dt.Rows[0]["idSede"].ToString())));
                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar consultorio";
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ValidarConsultorioDisponibilidad(int.Parse(Request.QueryString["deleteid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                MostrarAlerta("Mensaje", "Este consultorio no se puede borrar, hay citas asociadas a el.", "warning");

                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarConsultorioPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    txbConsultorio.Text = dt1.Rows[0]["NombreConsultorio"].ToString();
                                    txbConsultorio.Enabled = false;
                                    ddlSedes.SelectedIndex = Convert.ToInt32(ddlSedes.Items.IndexOf(ddlSedes.Items.FindByValue(dt.Rows[0]["idSede"].ToString())));
                                    ddlSedes.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    btnAgregar.Enabled = false;
                                    ltTitulo.Text = "Borrar consultorio";
                                }
                                dt1.Dispose();
                            }
                            else
                            {
                                //Borrar
                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarConsultorioPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    txbConsultorio.Text = dt1.Rows[0]["NombreConsultorio"].ToString();
                                    txbConsultorio.Enabled = false;
                                    ddlSedes.SelectedIndex = Convert.ToInt32(ddlSedes.Items.IndexOf(ddlSedes.Items.FindByValue(dt1.Rows[0]["idSede"].ToString())));
                                    ddlSedes.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    ltTitulo.Text = "Borrar consultorio";
                                }
                                dt1.Dispose();
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

        private void ListaConsultorios()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaConsultorios();
            rpConsultorios.DataSource = dt;
            rpConsultorios.DataBind();
            dt.Dispose();
        }

        private void CargarSedes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSedes("Todos");

            ddlSedes.DataSource = dt;
            ddlSedes.DataBind();

            dt.Dispose();
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            // tipo puede ser: 'success', 'error', 'warning', 'info', 'question'
            string script = $@"
                Swal.hideLoading();
                Swal.fire({{
                    title: '{titulo}',
                    text: '{mensaje}',
                    icon: '{tipo}', 
                    allowOutsideClick: false, 
                    showCloseButton: false, 
                    confirmButtonText: 'Aceptar'
                }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string consultaSQL = @"SELECT NombreConsultorio AS 'Nombre consultorio', NombreSede AS 'Nombre Sede' 
	                                   FROM ConsultorioSedes cs 
                                       INNER JOIN Sedes s ON s.idSede = cs.idSede
	                                   ORDER BY NombreSede;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
                string nombreArchivo = $"CONSULTORIOS_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    cg.ExportarExcelOk(dt, nombreArchivo);
                }
                else
                {
                    MostrarAlerta("Mensaje", "No existen registros para esta consulta", "warning");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", "Error al exportar" + ex.Message, "error");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editid"] != null)
                {
                    string strInitData = TraerData();
                    string respuesta = cg.ActualizarConsultorio(
                        int.Parse(Request.QueryString["editid"].ToString()),
                        int.Parse(ddlSedes.SelectedItem.Value.ToString()),
                        txbConsultorio.Text.ToString().Trim()
                    );
                    string strNewData = TraerData();
                    cg.InsertarLog(Session["idusuario"].ToString(), "ConsultorioSedes", "Modifica", "El usuario modificó el consultorio: " + txbConsultorio.Text.ToString() + ".", strInitData, strNewData);
                }
                if (Request.QueryString["deleteid"] != null)
                {
                    string respuesta = cg.EliminarConsultorio(int.Parse(Request.QueryString["deleteid"].ToString()));
                    cg.InsertarLog(Session["idusuario"].ToString(), "ConsultorioSedes", "Elimina", "El usuario eliminó el consultorio: " + txbConsultorio.Text.ToString() + ".", "", "");
                }
                Response.Redirect("consultorios");
            }
            else
            {
                if (!ValidarConsultorio(txbConsultorio.Text.ToString(), ddlSedes.SelectedItem.Value.ToString()))
                {
                    try
                    {
                        string respuesta = cg.InsertarConsultorio(
                            int.Parse(ddlSedes.SelectedItem.Value.ToString()),
                            txbConsultorio.Text.ToString().Trim()
                        );

                        cg.InsertarLog(Session["idusuario"].ToString(), "ConsultorioSedes", "Agregó", "El usuario agregó un nuevo consultorio: " + txbConsultorio.Text.ToString() + ".", "", "");
                    }
                    catch (Exception ex)
                    {
                        string mensajeExcepcionInterna = string.Empty;
                        Console.WriteLine(ex.Message);
                        if (ex.InnerException != null)
                        {
                            mensajeExcepcionInterna = ex.InnerException.Message;
                            MostrarAlerta("Error", "Mensaje de la excepción interna: " + mensajeExcepcionInterna, "error");
                        }
                    }
                    Response.Redirect("consultorios");
                }
                else
                {
                    MostrarAlerta("Mensaje", "Ya existe un consultorio con ese nombre para esta sede.", "warning");
                }
            }
        }

        private bool ValidarConsultorio(string strNombre, string idSede)
        {
            bool bExiste = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarConsultorioPorNombrePorSede(strNombre, Convert.ToInt32(idSede));
            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }
            dt.Dispose();
            return bExiste;
        }

        protected void rpConsultorios_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "consultorios?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "consultorios?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarConsultorioPorId(int.Parse(Request.QueryString["editid"].ToString()));

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