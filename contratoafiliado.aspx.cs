using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
	public partial class contratoafiliado : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Sedes");
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

                    string strQuery = "SELECT * " +
                        "FROM EmpresasFP " +
                        "WHERE idEmpresaFP = 1 ";

                    clasesglobales cg = new clasesglobales();
                    DataTable dt = cg.TraerDatos(strQuery);

                    string strTextoContrato = dt.Rows[0]["ContratoMayorEdad"].ToString();
                    string contenidoEditor = hiddenEditor.Value;
                    hiddenEditor.Value = dt.Rows[0]["ContratoMayorEdad"].ToString();

                    strQuery = "SELECT * " +
                        "FROM Afiliados " +
                        "WHERE idAfiliado = 10036 ";

                    DataTable dt1 = cg.TraerDatos(strQuery);

                    strTextoContrato = strTextoContrato.Replace("#NOMBRE#", dt1.Rows[0]["NombreAfiliado"].ToString() + " " + dt1.Rows[0]["ApellidoAfiliado"].ToString());

                    ltContrato.Text = strTextoContrato;
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            string contenidoEditor = hiddenEditor.Value;

            try
            {
                string respuesta = cg.TraerDatosStr("UPDATE EmpresasFP SET ContratoMayorEdad = '" + contenidoEditor + "' WHERE idEmpresaFP = 1 ");
                //cg.InsertarLog(Session["idusuario"].ToString(), "sedes", "Agrega", "El usuario agregó una nueva sede: " + txbSede.Text.ToString() + ".", "", "");
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }

            Response.Redirect("contratoafiliado");
        }
    }
}