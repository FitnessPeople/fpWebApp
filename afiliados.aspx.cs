﻿using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class afiliados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Afiliados");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        string strParam = "";
                        CargarSedes();
                        listaAfiliados(strParam, "todas");

                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            btnExportar.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
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

        private void CargarSedes()
        {

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSedes("Gimnasio");

            ddlSedes.DataSource = dt;
            ddlSedes.DataBind();

            dt.Dispose();
        }

        private void listaAfiliados(string strParam, string strSede)
        {
            string strQueryAdd = "";
            string strLimit = "100";
            if (strSede != "todas")
            {
                strQueryAdd = "AND a.idSede = " + strSede;
            }
            if (strParam != "")
            {
                strLimit = "1000";
            }
            string strQuery = "SELECT *, " +
                "IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) IS NOT NULL, CONCAT('(',TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()),')'),'<i class=\"fa fa-circle-question m-r-lg m-l-lg\"></i>') AS edad, " +
                "IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 14,'danger',IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 14,'success',IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 60,'info','warning'))) AS badge, " +
                "IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) < 14,'baby',IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) >= 60,'person-walking-with-cane','')) AS age, " +
                "IF(EstadoAfiliado='Activo','info',IF(EstadoAfiliado='Inactivo','danger','warning')) AS badge2, " +
                "IF(EstadoPlan='Activo','info',IF(EstadoAfiliado='Inactivo','danger','warning')) AS badge3 " +
                "FROM Afiliados a " +
                "LEFT JOIN generos g ON g.idGenero = a.idGenero " +
                "LEFT JOIN sedes s ON s.idSede = a.idSede " +
                "LEFT JOIN ciudadessedes cs ON s.idCiudadSede = cs.idCiudadSede " +
                "LEFT JOIN estadocivil ec ON ec.idEstadoCivil = a.idEstadoCivilAfiliado " +
                "LEFT JOIN AfiliadosPlanes ap ON ap.idAfiliado = a.idAfiliado AND ap.EstadoPlan = 'Activo' " +
                "LEFT JOIN profesiones p ON p.idProfesion = a.idProfesion " +
                "LEFT JOIN eps ON eps.idEps = a.idEps " +
                "LEFT JOIN ciudades ON ciudades.idCiudad = a.idCiudadAfiliado " +
                "WHERE (DocumentoAfiliado like '%" + strParam + "%' " +
                "OR NombreAfiliado like '%" + strParam + "%' " +
                "OR EmailAfiliado like '%" + strParam + "%' " +
                "OR CelularAfiliado like '%" + strParam + "%') " + strQueryAdd + " " +
                "ORDER BY a.idAfiliado DESC " +
                "LIMIT " + strLimit + "";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpAfiliados.DataSource = dt;
            rpAfiliados.DataBind();

            dt.Dispose();
        }

        protected void rpAfiliados_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlButton btnEditar = (HtmlButton)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("onClick", "window.location.href='editarafiliado?editid=" + ((DataRowView)e.Item.DataItem).Row["idAfiliado"].ToString() + "'");
                    btnEditar.Visible = true;

                    HtmlButton btnPlan = (HtmlButton)e.Item.FindControl("btnPlan");
                    btnPlan.Attributes.Add("onClick", "window.location.href='planesAfiliado?id=" + ((DataRowView)e.Item.DataItem).Row["idAfiliado"].ToString() + "'");
                    btnPlan.Visible = true;

                    HtmlButton btnTraspaso = (HtmlButton)e.Item.FindControl("btnTraspaso");
                    btnTraspaso.Attributes.Add("onClick", "window.location.href='traspasosAfil?id=" + ((DataRowView)e.Item.DataItem).Row["idAfiliado"].ToString() + "'");
                    btnTraspaso.Visible = true;

                    HtmlButton btnCortesia = (HtmlButton)e.Item.FindControl("btnCortesia");
                    btnCortesia.Attributes.Add("onClick", "window.location.href='cortesiasAfil?id=" + ((DataRowView)e.Item.DataItem).Row["idAfiliado"].ToString() + "'");
                    btnCortesia.Visible = true;

                    HtmlButton btnIncapacidad = (HtmlButton)e.Item.FindControl("btnIncapacidad");
                    btnIncapacidad.Attributes.Add("onClick", "window.location.href='incapacidadesAfil?id=" + ((DataRowView)e.Item.DataItem).Row["idAfiliado"].ToString() + "'");
                    btnIncapacidad.Visible = true;

                    HtmlButton btnCongelacion = (HtmlButton)e.Item.FindControl("btnCongelacion");
                    btnCongelacion.Attributes.Add("onClick", "window.location.href='congelacionesAfil?id=" + ((DataRowView)e.Item.DataItem).Row["idAfiliado"].ToString() + "'");
                    btnCongelacion.Visible = true;

                    HtmlButton btnAdres = (HtmlButton)e.Item.FindControl("btnAdres");
                    btnAdres.Attributes.Add("data-toggle", "modal");
                    btnAdres.Attributes.Add("data-target", "#myModal2");
                    btnAdres.Attributes.Add("data-documento", "" + ((DataRowView)e.Item.DataItem).Row["DocumentoAfiliado"].ToString() + "");
                    btnAdres.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlButton btnEliminar = (HtmlButton)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("onClick", "window.location.href='eliminarafiliado?deleteid=" + ((DataRowView)e.Item.DataItem).Row["idAfiliado"].ToString() + "'");
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string strParam = txbBuscar.Value.ToString();
            listaAfiliados(strParam, ddlSedes.SelectedItem.Value.ToString());
        }

        protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strParam = txbBuscar.Value.ToString();
            listaAfiliados(strParam, ddlSedes.SelectedItem.Value.ToString());
        }
    }
}