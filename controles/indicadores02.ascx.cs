﻿using System;
using System.Data;

namespace fpWebApp.controles
{
    public partial class indicadores02 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ValidarPermisos("Páginas");
            //if (ViewState["SinPermiso"].ToString() == "0")
            //{
                CuantosAfiliadosActivos();
                CuantosAfiliadosInactivos();
                CuantasSedes();
                CuantosNuevosUltimoMes();
            //}
        }

        //private void ValidarPermisos(string strPagina)
        //{
        //    ViewState["SinPermiso"] = "1";
        //    ViewState["Consulta"] = "0";
        //    ViewState["Exportar"] = "0";
        //    ViewState["CrearModificar"] = "0";
        //    ViewState["Borrar"] = "0";

        //    clasesglobales cg = new clasesglobales();
        //    DataTable dt = cg.ValidarPermisos(strPagina, Session["idPerfil"].ToString(), Session["idusuario"].ToString());

        //    if (dt.Rows.Count > 0)
        //    {
        //        ViewState["SinPermiso"] = dt.Rows[0]["SinPermiso"].ToString();
        //        ViewState["Consulta"] = dt.Rows[0]["Consulta"].ToString();
        //        ViewState["Exportar"] = dt.Rows[0]["Exportar"].ToString();
        //        ViewState["CrearModificar"] = dt.Rows[0]["CrearModificar"].ToString();
        //        ViewState["Borrar"] = dt.Rows[0]["Borrar"].ToString();
        //    }

        //    dt.Dispose();
        //}

        private void CuantosAfiliadosActivos()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Afiliados WHERE EstadoAfiliado = 'Activo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos1.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosAfiliadosInactivos()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Afiliados WHERE EstadoAfiliado = 'Inactivo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos2.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantasSedes()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Sedes WHERE idSede <> 11";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos3.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }

        private void CuantosNuevosUltimoMes()
        {
            string strQuery = "SELECT COUNT(*) AS cuantos FROM Afiliados WHERE EstadoAfiliado = 'Activo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltCuantos4.Text = dt.Rows[0]["cuantos"].ToString();

            dt.Dispose();
        }
    }
}