using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class perfiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Perfiles");
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
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                    ListaPerfiles();
                    ListaPermisosPerfiles();
                    ltTitulo.Text = "Agregar perfil";

                    if (Request.QueryString.Count > 0)
                    {
                        clasesglobales cg = new clasesglobales();
                        rpPerfiles.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            DataTable dt = cg.ConsultarPerfilPorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                txbPerfil.Text = dt.Rows[0]["Perfil"].ToString();
                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar Perfil";
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            // Si existe un usuario con este perfil, no se puede borrar, de lo contrario si.
                            string strQuery = "SELECT * FROM usuarios WHERE idPerfil = " + Request.QueryString["deleteid"].ToString();
                            DataTable dt = cg.TraerDatos(strQuery);

                            if (dt.Rows.Count > 0)
                            {
                                //No se puede borrar
                                MostrarAlertaRedireccion("Eliminar", "Existe al menos un usuario con este perfil, no se puede eliminar.", "error", "perfiles.aspx");
                            }
                            else
                            {
                                strQuery = "DELETE FROM perfiles WHERE idPerfil = " + Request.QueryString["deleteid"].ToString();
                                cg.TraerDatosStr(strQuery);
                                MostrarAlertaRedireccion("Eliminar", "El perfil fue eliminado con exito.", "error", "perfiles.aspx");
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

        private void ListaPerfiles()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPerfiles();

            rpPerfiles.DataSource = dt;
            rpPerfiles.DataBind();
            dt.Dispose();

            ddlPerfiles.DataSource = dt;
            ddlPerfiles.DataBind();

            dt.Dispose();
        }

        /// <summary>
        /// Lista los permisos que tiene cada página para un perfil específico
        /// </summary>
        private void ListaPermisosPerfiles()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPermisosPerfilesPorPerfil(int.Parse(ddlPerfiles.SelectedItem.Value.ToString()), 0); // Permiso 0: Mostrar todas las páginas. Permiso 1: Mostrar solo las que tiene permiso.

            rpPaginasPermisos.DataSource = dt;
            rpPaginasPermisos.DataBind();
            dt.Dispose();
        }

        protected void ddlPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPerfiles.SelectedItem.Value.ToString() != "")
            {
                ListaPermisosPerfiles();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            if (Request.QueryString.Count > 0)
            {
                string strInitData = TraerData();

                if (Request.QueryString["editid"] != null)
                {
                    string respuesta = cg.ActualizarPerfil(int.Parse(Request.QueryString["editid"].ToString()), txbPerfil.Text.ToString().Trim());

                    string strNewData = TraerData();
                    cg.InsertarLog(Session["idusuario"].ToString(), "perfiles", "Modifica", "El usuario modificó el perfil: " + txbPerfil.Text.ToString() + ".", strInitData, strNewData);
                }
                Response.Redirect("perfiles");
            }
            else
            {
                if (!ValidarPerfil(txbPerfil.Text.ToString()))
                {
                    try
                    {
                        string respuesta = cg.InsertarPerfil(txbPerfil.Text.ToString().Trim());

                        cg.InsertarLog(Session["idusuario"].ToString(), "perfiles", "Agrega", "El usuario agregó un nuevo perfil: " + txbPerfil.Text.ToString() + ".", "", "");

                        DataTable dt = cg.ConsultarUltimoPerfil();
                        int IdPerfil = int.Parse(dt.Rows[0]["idPerfil"].ToString());
                        dt.Dispose();

                        DataTable dt1 = cg.ConsultarPaginas();

                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            try
                            {
                                string respuesta2 = cg.InsertarPermisoPerfil(IdPerfil, int.Parse(dt1.Rows[i]["idPagina"].ToString()), 1, 0, 0, 0, 0);
                            }
                            catch (Exception ex)
                            {
                                string mensaje = ex.Message;
                            }
                        }
                        dt1.Dispose();
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
                        //ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        //"<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        //"Excepción interna." +
                        //"</div>";

                        MostrarAlerta("Excepción interna.", mensajeExcepcionInterna, "error");
                    }
                    Response.Redirect("perfiles");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Ya existe un Perfil con ese nombre." +
                        "</div>";
                }
            }
        }

        private bool ValidarPerfil(string strNombre)
        {
            bool bExiste = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPerfilPorNombre(strNombre);
            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }
            dt.Dispose();
            return bExiste;
        }

        protected void rpPerfiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "perfiles?editid=" + ((DataRowView)e.Item.DataItem).Row["idPerfil"].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "perfiles?deleteid=" + ((DataRowView)e.Item.DataItem).Row["idPerfil"].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void rpPaginasPermisos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lb1 = (LinkButton)e.Item.FindControl("lb1");
            lb1.CommandArgument = ((DataRowView)e.Item.DataItem).Row["idPagina"].ToString() + "," + ((DataRowView)e.Item.DataItem).Row["idPerfil"].ToString();

            LinkButton lb2 = (LinkButton)e.Item.FindControl("lb2");
            lb2.CommandArgument = ((DataRowView)e.Item.DataItem).Row["idPagina"].ToString() + "," + ((DataRowView)e.Item.DataItem).Row["idPerfil"].ToString();

            LinkButton lb3 = (LinkButton)e.Item.FindControl("lb3");
            lb3.CommandArgument = ((DataRowView)e.Item.DataItem).Row["idPagina"].ToString() + "," + ((DataRowView)e.Item.DataItem).Row["idPerfil"].ToString();

            LinkButton lb4 = (LinkButton)e.Item.FindControl("lb4");
            lb4.CommandArgument = ((DataRowView)e.Item.DataItem).Row["idPagina"].ToString() + "," + ((DataRowView)e.Item.DataItem).Row["idPerfil"].ToString();

            LinkButton lb5 = (LinkButton)e.Item.FindControl("lb5");
            lb5.CommandArgument = ((DataRowView)e.Item.DataItem).Row["idPagina"].ToString() + "," + ((DataRowView)e.Item.DataItem).Row["idPerfil"].ToString();
        }

        protected void lb1_Click(object sender, EventArgs e)
        {
            string perm = "1";
            CambiarPermiso(((LinkButton)sender).CommandArgument, perm);
        }

        protected void lb2_Click(object sender, EventArgs e)
        {
            string perm = "2";
            CambiarPermiso(((LinkButton)sender).CommandArgument, perm);
        }

        protected void lb3_Click(object sender, EventArgs e)
        {
            string perm = "3";
            CambiarPermiso(((LinkButton)sender).CommandArgument, perm);
        }

        protected void lb4_Click(object sender, EventArgs e)
        {
            string perm = "4";
            CambiarPermiso(((LinkButton)sender).CommandArgument, perm);
        }

        protected void lb5_Click(object sender, EventArgs e)
        {
            string perm = "5";
            CambiarPermiso(((LinkButton)sender).CommandArgument, perm);
        }

        private void CambiarPermiso(string argumentos, string permiso)
        {
            string[] arguments = argumentos.Split(',');
            string strQuery = "SELECT * FROM permisos_perfiles " +
                "WHERE idPerfil = " + arguments[1] + " " +
                "AND idPagina = " + arguments[0];
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                switch (permiso)
                {
                    case "1":
                        if (dt.Rows[0]["SinPermiso"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "SinPermiso = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "SinPermiso = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    case "2":
                        if (dt.Rows[0]["Consulta"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Consulta = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Consulta = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    case "3":
                        if (dt.Rows[0]["Exportar"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Exportar = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Exportar = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    case "4":
                        if (dt.Rows[0]["CrearModificar"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "CrearModificar = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "CrearModificar = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    case "5":
                        if (dt.Rows[0]["Borrar"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Borrar = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Borrar = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    default:
                        break;
                }
                cg.TraerDatosStr(strQuery);
            }

            dt.Dispose();
            ListaPermisosPerfiles();
        }

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPerfilPorId(int.Parse(Request.QueryString["editid"].ToString()));

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();

            return strData;
        }

        protected void cbSoloPermiso_CheckedChanged(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            if (cbSoloPermiso.Checked)
            {
                DataTable dt = cg.ConsultarPermisosPerfilesPorPerfil(int.Parse(ddlPerfiles.SelectedItem.Value.ToString()), 1); // Permiso 0: Mostrar todas las páginas. Permiso 1: Mostrar solo las que tiene permiso.

                rpPaginasPermisos.DataSource = dt;
                rpPaginasPermisos.DataBind();
                dt.Dispose();
            }
            else
            {
                DataTable dt = cg.ConsultarPermisosPerfilesPorPerfil(int.Parse(ddlPerfiles.SelectedItem.Value.ToString()), 0); // Permiso 0: Mostrar todas las páginas. Permiso 1: Mostrar solo las que tiene permiso.

                rpPaginasPermisos.DataSource = dt;
                rpPaginasPermisos.DataBind();
                dt.Dispose();
            }
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

        private void MostrarAlertaRedireccion(string titulo, string mensaje, string tipo, string urlRedirect)
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
                confirmButtonText: 'Aceptar', 
            }}).then((result) => {{
                if (result.isConfirmed) {{
                    window.location.replace('{urlRedirect}');
                }}
            }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
        }
    }
}