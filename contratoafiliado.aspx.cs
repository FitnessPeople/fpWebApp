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
                        btnAgregar1.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {

                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {

                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar1.Visible = true;
                        }
                    }

                    string strQuery = "SELECT * " +
                        "FROM EmpresasFP " +
                        "WHERE idEmpresaFP IN (1,2) ";

                    clasesglobales cg = new clasesglobales();
                    DataTable dt = cg.TraerDatos(strQuery);

                    string strTextoContrato1 = dt.Rows[0]["ContratoMayorEdad"].ToString();
                    string contenidoEditor1 = hiddenEditor1.Value;
                    hiddenEditor1.Value = dt.Rows[0]["ContratoMayorEdad"].ToString();

                    string strTextoContrato2 = dt.Rows[1]["ContratoMayorEdad"].ToString();
                    string contenidoEditor2 = hiddenEditor2.Value;
                    hiddenEditor2.Value = dt.Rows[1]["ContratoMayorEdad"].ToString();

                    strQuery = @"SELECT * 
                        FROM Afiliados a 
                        LEFT JOIN afiliadosplanes ap ON ap.idAfiliado = a.idAfiliado AND ap.EstadoPlan = 'Activo' 
                        LEFT JOIN eps ON eps.idEps = a.idEps 
                        LIMIT 1";

                    DataTable dt1 = cg.TraerDatos(strQuery);

                    if (dt1.Rows.Count > 0)
                    {
                        strTextoContrato1 = strTextoContrato1.Replace("#NOMBRE#", dt1.Rows[0]["NombreAfiliado"].ToString() + " " + dt1.Rows[0]["ApellidoAfiliado"].ToString());
                        strTextoContrato1 = strTextoContrato1.Replace("#DOCUMENTO#", dt1.Rows[0]["DocumentoAfiliado"].ToString());
                        strTextoContrato1 = strTextoContrato1.Replace("#DIRECCION#", dt1.Rows[0]["DireccionAfiliado"].ToString());
                        strTextoContrato1 = strTextoContrato1.Replace("#CELULAR#", dt1.Rows[0]["CelularAfiliado"].ToString());
                        strTextoContrato1 = strTextoContrato1.Replace("#FECHANAC#", Convert.ToDateTime(dt1.Rows[0]["FechaNacAfiliado"].ToString()).ToString("dd MMMM yyyy"));
                        strTextoContrato1 = strTextoContrato1.Replace("#EMAIL#", dt1.Rows[0]["EmailAfiliado"].ToString());

                        if (dt1.Rows[0]["FechaInicioPlan"].ToString() != "")
                        {
                            strTextoContrato1 = strTextoContrato1.Replace("#FECHAINICIOPLAN#", Convert.ToDateTime(dt1.Rows[0]["FechaInicioPlan"].ToString()).ToString("dd MMMM yyyy"));
                        }
                        strTextoContrato1 = strTextoContrato1.Replace("#EPS#", dt1.Rows[0]["NombreEps"].ToString());
                        strTextoContrato1 = strTextoContrato1.Replace("#RESPONSABLE#", dt1.Rows[0]["ResponsableAfiliado"].ToString());
                        strTextoContrato1 = strTextoContrato1.Replace("#PARENTESCO#", dt1.Rows[0]["Parentesco"].ToString());
                        strTextoContrato1 = strTextoContrato1.Replace("#CELULARRESPONSABLE#", dt1.Rows[0]["ContactoAfiliado"].ToString());

                        ltContrato1.Text = strTextoContrato1;

                        strTextoContrato2 = strTextoContrato2.Replace("#NOMBRE#", dt1.Rows[0]["NombreAfiliado"].ToString() + " " + dt1.Rows[0]["ApellidoAfiliado"].ToString());
                        strTextoContrato2 = strTextoContrato2.Replace("#DOCUMENTO#", dt1.Rows[0]["DocumentoAfiliado"].ToString());
                        strTextoContrato2 = strTextoContrato2.Replace("#DIRECCION#", dt1.Rows[0]["DireccionAfiliado"].ToString());
                        strTextoContrato2 = strTextoContrato2.Replace("#CELULAR#", dt1.Rows[0]["CelularAfiliado"].ToString());
                        strTextoContrato2 = strTextoContrato2.Replace("#FECHANAC#", Convert.ToDateTime(dt1.Rows[0]["FechaNacAfiliado"].ToString()).ToString("dd MMMM yyyy"));
                        strTextoContrato2 = strTextoContrato2.Replace("#EMAIL#", dt1.Rows[0]["EmailAfiliado"].ToString());

                        if (dt1.Rows[0]["FechaInicioPlan"].ToString() != "")
                        {
                            strTextoContrato2 = strTextoContrato2.Replace("#FECHAINICIOPLAN#", Convert.ToDateTime(dt1.Rows[0]["FechaInicioPlan"].ToString()).ToString("dd MMMM yyyy"));
                        }
                        strTextoContrato2 = strTextoContrato2.Replace("#EPS#", dt1.Rows[0]["NombreEps"].ToString());
                        strTextoContrato2 = strTextoContrato2.Replace("#RESPONSABLE#", dt1.Rows[0]["ResponsableAfiliado"].ToString());
                        strTextoContrato2 = strTextoContrato2.Replace("#PARENTESCO#", dt1.Rows[0]["Parentesco"].ToString());
                        strTextoContrato2 = strTextoContrato2.Replace("#CELULARRESPONSABLE#", dt1.Rows[0]["ContactoAfiliado"].ToString());

                        ltContrato2.Text = strTextoContrato2;
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

        protected void btnAgregar1_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            string contenidoEditor = hiddenEditor1.Value;

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

        protected void btnAgregar2_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            string contenidoEditor = hiddenEditor2.Value;

            try
            {
                string respuesta = cg.TraerDatosStr("UPDATE EmpresasFP SET ContratoMayorEdad = '" + contenidoEditor + "' WHERE idEmpresaFP = 2 ");
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