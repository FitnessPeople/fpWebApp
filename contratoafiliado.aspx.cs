using System;
using System.Data;

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
                    ValidarPermisos("Contrato afiliado");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        //No tiene acceso a esta página
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
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

                    strQuery = @"SELECT * 
                        FROM Afiliados a 
                        LEFT JOIN afiliadosplanes ap ON ap.idAfiliado = a.idAfiliado AND ap.EstadoPlan = 'Activo' 
                        LEFT JOIN eps ON eps.idEps = a.idEps 
                        
                        LIMIT 1";

                    DataTable dt1 = cg.TraerDatos(strQuery);

                    if (dt1.Rows.Count > 0)
                    {
                        strTextoContrato = strTextoContrato.Replace("#NOMBRE#", dt1.Rows[0]["NombreAfiliado"].ToString() + " " + dt1.Rows[0]["ApellidoAfiliado"].ToString());
                        strTextoContrato = strTextoContrato.Replace("#DOCUMENTO#", dt1.Rows[0]["DocumentoAfiliado"].ToString());
                        strTextoContrato = strTextoContrato.Replace("#DIRECCION#", dt1.Rows[0]["DireccionAfiliado"].ToString());
                        strTextoContrato = strTextoContrato.Replace("#CELULAR#", dt1.Rows[0]["CelularAfiliado"].ToString());
                        strTextoContrato = strTextoContrato.Replace("#FECHANAC#", Convert.ToDateTime(dt1.Rows[0]["FechaNacAfiliado"].ToString()).ToString("dd MMMM yyyy"));
                        strTextoContrato = strTextoContrato.Replace("#EMAIL#", dt1.Rows[0]["EmailAfiliado"].ToString());

                        if (dt1.Rows[0]["FechaInicioPlan"].ToString() != "")
                        {
                            strTextoContrato = strTextoContrato.Replace("#FECHAINICIOPLAN#", Convert.ToDateTime(dt1.Rows[0]["FechaInicioPlan"].ToString()).ToString("dd MMMM yyyy"));
                        }
                        strTextoContrato = strTextoContrato.Replace("#EPS#", dt1.Rows[0]["NombreEps"].ToString());

                        ltContrato.Text = strTextoContrato;
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