using System;
using System.Data;

namespace fpWebApp
{
    public partial class respuestaautorizacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idTraspaso"] != null)
            {
                //Cargar datos del traspaso
                divTraspasos.Visible = true;
                divCortesias.Visible = false;
                divCongelaciones.Visible = false;
                divIncapacidades.Visible = false;
                CargarTraspaso();
            }
            if (Request.QueryString["idCortesia"] != null)
            {
                //Cargar datos de la cortesia
                divTraspasos.Visible = false;
                divCortesias.Visible = true;
                divCongelaciones.Visible = false;
                divIncapacidades.Visible = false;
                CargarCortesia();
            }
            if (Request.QueryString["idCongelacion"] != null)
            {
                //Cargar datos de la congelancion
                divTraspasos.Visible = false;
                divCortesias.Visible = false;
                divCongelaciones.Visible = true;
                divIncapacidades.Visible = false;
                CargarCongelacion();
            }
            if (Request.QueryString["idIncapacidad"] != null)
            {
                //Cargar datos de la congelancion
                divTraspasos.Visible = false;
                divCortesias.Visible = false;
                divCongelaciones.Visible = false;
                divIncapacidades.Visible = true;
                CargarIncapacidad();
            }
        }

        private void CargarTraspaso()
        {
            string strQuery = "SELECT t.idTraspaso, CONCAT(a1.NombreAfiliado, ' ', a1.ApellidoAfiliado) nomAfilOrigen, " +
                "CONCAT(a2.NombreAfiliado, ' ', a2.ApellidoAfiliado) nomAfilDestino, t.Observaciones, u.NombreUsuario, t.FechaTraspaso, " +
                "DATEDIFF(CURDATE(), FechaTraspaso) AS hacecuanto, " +
                "IF(DATEDIFF(CURDATE(), FechaTraspaso)<5,'pie1',IF(DATEDIFF(CURDATE(), FechaTraspaso)<10,'pie2',IF(DATEDIFF(CURDATE(), FechaTraspaso)<15,'pie3','pie3'))) badge," +
                "t.idAfiliadoPlan, t.FechaInicioTraspaso, t.DocumentoRespaldo " +
                "FROM traspasoplanes t, Afiliados a1, Afiliados a2, AfiliadosPlanes ap, Usuarios u " +
                "WHERE t.EstadoTraspaso = 'En proceso' " +
                "AND t.idAfiliadoPlan = ap.idAfiliadoPlan " +
                "AND t.idAfiliadoOrigen = a1.idAfiliado " +
                "AND t.idAfiliadoDestino = a2.idAfiliado " +
                "AND t.idUsuario = u.idUsuario " +
                "AND t.idTraspaso = " + Request.QueryString["idTraspaso"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                ltAfiliadoOrigen.Text = dt.Rows[0]["nomAfilOrigen"].ToString();
                ltAfiliadoDestino.Text = dt.Rows[0]["nomAfilDestino"].ToString();
                CargarPlanAfiliado(dt.Rows[0]["idAfiliadoPlan"].ToString());
                ltFechaTraspaso.Text = string.Format("{0:dd MMM yyyy}", dt.Rows[0]["FechaTraspaso"]) + " (Hace " + dt.Rows[0]["hacecuanto"].ToString() + " días)";
                ltFechaInicioTraspaso.Text = string.Format("{0:dd MMM yyyy}", dt.Rows[0]["FechaInicioTraspaso"]);
                ltDocumentoTraspaso.Text = "<i class=\"fa fa-file-pdf\"></i> " +
                    "<a class=\"dropdown-toggle\" data-toggle=\"modal\" " +
                    "href=\"#\" data-target=\"#myModal2\" data-path=\"./docs/traspasos/\" " +
                    "data-file=\"" + dt.Rows[0]["DocumentoRespaldo"].ToString() + "\">" + dt.Rows[0]["DocumentoRespaldo"].ToString() + "</a>";
                ltObservacionesTraspaso.Text = dt.Rows[0]["Observaciones"].ToString();
                ltUsuarioTraspaso.Text = dt.Rows[0]["NombreUsuario"].ToString();
            }

            dt.Dispose();
        }

        private void CargarCortesia()
        {
            string strQuery = "SELECT *, " +
                "DATEDIFF(CURDATE(), FechaHoraCortesia) AS hacecuanto, " +
                "IF(DATEDIFF(CURDATE(), FechaHoraCortesia)<5,'pie1',IF(DATEDIFF(CURDATE(), FechaHoraCortesia)<10,'pie2',IF(DATEDIFF(CURDATE(), FechaHoraCortesia)<15,'pie3','pie3'))) badge " +
                "FROM Cortesias c, Afiliados a, AfiliadosPlanes ap, Usuarios u " +
                "WHERE c.EstadoCortesia = 'Pendiente' " +
                "AND c.idAfiliadoPlan = ap.idAfiliadoPlan " +
                "AND ap.idAfiliado = a.idAfiliado " +
                "AND c.idUsuario = u.idUsuario " +
                "AND c.idCortesia = " + Request.QueryString["idCortesia"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                ltNombreAfiliadoCortesia.Text = dt.Rows[0]["NombreAfiliado"].ToString() + " " + dt.Rows[0]["ApellidoAfiliado"].ToString();
                CargarPlanAfiliado(dt.Rows[0]["idAfiliadoPlan"].ToString());
                ltDiasCortesia.Text = dt.Rows[0]["DiasCortesia"].ToString();
                ltFechaHoraCortesia.Text = string.Format("{0:dd MMM yyyy}", dt.Rows[0]["FechaHoraCortesia"]);
                ltObservacionesCortesia.Text = dt.Rows[0]["ObservacionesCortesia"].ToString();
                ltUsuarioCortesia.Text = dt.Rows[0]["NombreUsuario"].ToString();
            }

            dt.Dispose();
        }

        private void CargarCongelacion()
        {
            string strQuery = "SELECT *, " +
                "DATEDIFF(CURDATE(), c.Fecha) AS hacecuanto, " +
                "IF(DATEDIFF(CURDATE(), c.Fecha)<5,'pie1',IF(DATEDIFF(CURDATE(), c.Fecha)<10,'pie2',IF(DATEDIFF(CURDATE(), c.Fecha)<15,'pie3','pie3'))) badge " +
                "FROM Congelaciones c, Afiliados a, AfiliadosPlanes ap, Usuarios u, TiposIncapacidad ti " +
                "WHERE c.Estado = 'En proceso' " +
                "AND c.idAfiliadoPlan = ap.idAfiliadoPlan " +
                "AND ap.idAfiliado = a.idAfiliado " +
                "AND c.idUsuario = u.idUsuario " +
                "AND c.idTipoIncapacidad = ti.idTipoIncapacidad " +
                "AND c.idCongelacion = " + Request.QueryString["idCongelacion"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                ltNombreAfiliadoCongelacion.Text = dt.Rows[0]["NombreAfiliado"].ToString() + " " + dt.Rows[0]["ApellidoAfiliado"].ToString();
                CargarPlanAfiliado(dt.Rows[0]["idAfiliadoPlan"].ToString());
                ltDiasCongelacion.Text = dt.Rows[0]["Dias"].ToString();
                ltTipoCongelacion.Text = dt.Rows[0]["TipoIncapacidad"].ToString();
                ltFechaHoraCongelacion.Text = string.Format("{0:dd MMM yyyy}", dt.Rows[0]["Fecha"]);
                ltFechaInicioCongelacion.Text = string.Format("{0:dd MMM yyyy}", dt.Rows[0]["FechaInicio"]);
                ltObservacionesCongelacion.Text = dt.Rows[0]["Observaciones"].ToString();
                ltUsuarioCongelacion.Text = dt.Rows[0]["NombreUsuario"].ToString();
                ltDocumentoCongelacion.Text = "<i class=\"fa fa-file-pdf\"></i> " +
                    "<a class=\"dropdown-toggle\" data-toggle=\"modal\" " +
                    "href=\"#\" data-target=\"#myModal2\" data-path=\"./docs/congelaciones/\" " +
                    "data-file=\"" + dt.Rows[0]["DocumentoCongelacion"].ToString() + "\">" + dt.Rows[0]["DocumentoCongelacion"].ToString() + "</a>";
            }

            dt.Dispose();
        }

        private void CargarIncapacidad()
        {
            string strQuery = "SELECT *, " +
                "DATEDIFF(CURDATE(), i.Fecha) AS hacecuanto, " +
                "IF(DATEDIFF(CURDATE(), i.Fecha)<5,'pie1',IF(DATEDIFF(CURDATE(), i.Fecha)<10,'pie2',IF(DATEDIFF(CURDATE(), i.Fecha)<15,'pie3','pie3'))) badge " +
                "FROM Incapacidades i, Afiliados a, AfiliadosPlanes ap, Usuarios u, TiposIncapacidad ti " +
                "WHERE i.Estado = 'En proceso' " +
                "AND i.idAfiliadoPlan = ap.idAfiliadoPlan " +
                "AND ap.idAfiliado = a.idAfiliado " +
                "AND i.idUsuario = u.idUsuario " +
                "AND i.idTipoIncapacidad = ti.idTipoIncapacidad " +
                "AND i.idIncapacidad = " + Request.QueryString["idIncapacidad"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                ltNombreAfiliadoIncapacidad.Text = dt.Rows[0]["NombreAfiliado"].ToString() + " " + dt.Rows[0]["ApellidoAfiliado"].ToString();
                CargarPlanAfiliado(dt.Rows[0]["idAfiliadoPlan"].ToString());
                ltDiasIncapacidad.Text = dt.Rows[0]["Dias"].ToString();
                ltTipoIncapacidad.Text = dt.Rows[0]["TipoIncapacidad"].ToString();
                ltFechaHoraIncapacidad.Text = string.Format("{0:dd MMM yyyy}", dt.Rows[0]["Fecha"]);
                ltFechaInicioIncapacidad.Text = string.Format("{0:dd MMM yyyy}", dt.Rows[0]["FechaInicio"]);
                ltObservacionesIncapacidad.Text = dt.Rows[0]["Observaciones"].ToString();
                ltUsuarioIncapacidad.Text = dt.Rows[0]["NombreUsuario"].ToString();
                ltDocumentoIncapacidad.Text = "<i class=\"fa fa-file-pdf\"></i> " +
                    "<a class=\"dropdown-toggle\" data-toggle=\"modal\" " +
                    "href=\"#\" data-target=\"#myModal2\" data-path=\"./docs/incapacidades/\" " +
                    "data-file=\"" + dt.Rows[0]["DocumentoIncapacidad"].ToString() + "\">" + dt.Rows[0]["DocumentoIncapacidad"].ToString() + "</a>";
            }

            dt.Dispose();
        }

        private void CargarPlanAfiliado(string idAfiliadoPlan)
        {
            string strQuery = "SELECT *, " +
                "DATEDIFF(FechaFinalPlan, CURDATE()) AS diasquefaltan, " +
                "DATEDIFF(CURDATE(), FechaInicioPlan) AS diasconsumidos, " +
                "DATEDIFF(FechaFinalPlan, FechaInicioPlan) AS diastotales, " +
                "ROUND(DATEDIFF(CURDATE(), FechaInicioPlan) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje1, " +
                "ROUND(DATEDIFF(FechaFinalPlan, CURDATE()) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje2 " +
                "FROM afiliadosPlanes ap, Afiliados a, Planes p " +
                "WHERE ap.idAfiliadoPlan = " + idAfiliadoPlan + " " +
                "AND ap.idAfiliado = a.idAfiliado " +
                "AND ap.idPlan = p.idPlan " +
                "AND ap.EstadoPlan = 'Activo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                rpPlanAfiliadoTraspaso.DataSource = dt;
                rpPlanAfiliadoTraspaso.DataBind();

                rpPlanAfiliadoCortesia.DataSource = dt;
                rpPlanAfiliadoCortesia.DataBind();

                rpPlanAfiliadoCongelacion.DataSource = dt;
                rpPlanAfiliadoCongelacion.DataBind();

                rpPlanAfiliadoIncapacidad.DataSource = dt;
                rpPlanAfiliadoIncapacidad.DataBind();
            }
            dt.Dispose();
        }

        protected void btnAutorizarCortesia_Click(object sender, EventArgs e)
        {
            btnAutorizarCortesia.CssClass += " active";
            btnNoAutorizarCortesia.CssClass = btnNoAutorizarCortesia.CssClass.Replace("active", "");
            ViewState["EstadoCortesia"] = "Autorizada";
        }

        protected void btnNoAutorizarCortesia_Click(object sender, EventArgs e)
        {
            btnNoAutorizarCortesia.CssClass += " active";
            btnAutorizarCortesia.CssClass = btnAutorizarCortesia.CssClass.Replace("active", "");
            ViewState["EstadoCortesia"] = "No Autorizada";
        }

        protected void btnResponderCortesia_Click(object sender, EventArgs e)
        {
            string strInitData = TraerDataCortesia();
            try
            {
                string strQuery = "UPDATE Cortesias SET " +
                    "EstadoCortesia = '" + ViewState["EstadoCortesia"].ToString() + "', " +
                    "RazonesCortesia = '" + txbRespuestaCortesia.Text.ToString() + "', " +
                    "idusuarioAutoriza = " + Session["idUsuario"].ToString() + ", " +
                    "FechaRespuesta = Now() " +
                    "WHERE idCortesia = " + Request.QueryString["idCortesia"].ToString();
                clasesglobales cg = new clasesglobales();
                string mensaje = cg.TraerDatosStr(strQuery);
                string strNewData = TraerDataCortesia();
                cg.InsertarLog(Session["idusuario"].ToString(), "Cortesias", "Modifica", "El usuario dio respuesta a la autorización de la cortesía.", strInitData, strNewData);

                // Actualizar plan con la cortesía. Agregar los días al final del plan.

                Response.Redirect("autorizaciones");
            }
            catch (Exception ex)
            {
                ltDiasCortesia.Text = ex.Message.ToString();
                throw;
            }
        }

        private string TraerDataCortesia()
        {
            string strQuery = "SELECT c.idCortesia, u1.NombreUsuario AS SolicitadoPor, " +
                "CONCAT(a.NombreAfiliado, ' ', a.ApellidoAfiliado) AS Afiliado, " +
                "c.DiasCortesia, c.FechaHoraCortesia, c.ObservacionesCortesia, c.EstadoCortesia, " +
                "IF(c.RazonesCortesia IS NULL, '-.', c.RazonesCortesia) AS RespuestaCortesia, " +
                "u2.NombreUsuario AS UsuarioQueAutoriza, c.FechaRespuesta " +
                "FROM Cortesias c " +
                "LEFT JOIN AfiliadosPlanes ap ON ap.idAfiliadoPlan = c.idAfiliadoPlan " +
                "LEFT JOIN Afiliados a ON a.idAfiliado = ap.idAfiliado " +
                "LEFT JOIN Usuarios u1 ON u1.idUsuario = c.idUsuario " +
                "LEFT JOIN Usuarios u2 ON u2.idUsuario = c.idUsuarioAutoriza " +
                "WHERE idCortesia = " + Request.QueryString["idCortesia"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();

            return strData;
        }

        protected void btnAutorizarTraspaso_Click(object sender, EventArgs e)
        {
            btnAutorizarTraspaso.CssClass += " active";
            btnNoAutorizarTraspaso.CssClass = btnNoAutorizarTraspaso.CssClass.Replace("active", "");
            ViewState["EstadoTraspaso"] = "Autorizado";
        }

        protected void btnNoAutorizarTraspaso_Click(object sender, EventArgs e)
        {
            btnNoAutorizarTraspaso.CssClass += " active";
            btnAutorizarTraspaso.CssClass = btnAutorizarTraspaso.CssClass.Replace("active", "");
            ViewState["EstadoTraspaso"] = "Rechazado";
        }

        protected void btnResponderTraspaso_Click(object sender, EventArgs e)
        {
            string strInitData = TraerDataTraspaso();
            try
            {
                string strQuery = "UPDATE Cortesias SET " +
                    "EstadoCortesia = '" + ViewState["EstadoCortesia"].ToString() + "', " +
                    "RazonesCortesia = '" + txbRespuestaCortesia.Text.ToString() + "', " +
                    "idusuarioAutoriza = " + Session["idUsuario"].ToString() + ", " +
                    "FechaRespuesta = Now() " +
                    "WHERE idCortesia = " + Request.QueryString["idCortesia"].ToString();
                clasesglobales cg = new clasesglobales();
                string mensaje = cg.TraerDatosStr(strQuery);
                string strNewData = TraerDataTraspaso();

                cg.InsertarLog(Session["idusuario"].ToString(), "Cortesias", "Modifica", "El usuario dio respuesta a la autorización de la cortesía.", strInitData, strNewData);

                // Actualizar plan con la cortesía. Agregar los días al final del plan.

                Response.Redirect("autorizaciones");
            }
            catch (Exception ex)
            {
                ltDiasCortesia.Text = ex.Message.ToString();
                throw;
            }
        }

        private string TraerDataTraspaso()
        {
            string strQuery = "SELECT t.idTraspaso, u1.NombreUsuario AS SolicitadoPor, " +
                "CONCAT(a1.NombreAfiliado, ' ', a1.ApellidoAfiliado) AS AfiliadoOrigen, " +
                "CONCAT(a2.NombreAfiliado, ' ', a2.ApellidoAfiliado) AS AfiliadoDestino, " +
                "t.FechaTraspaso, t.FechaInicioTraspaso, t.Observaciones, t.EstadoTraspaso, " +
                "IF(t.Razones IS NULL, '-.', t.Razones) AS RespuestaTraspaso, " +
                "u2.NombreUsuario AS UsuarioQueAutoriza, t.FechaRespuesta " +
                "FROM TraspasosPlanes t " +
                "LEFT JOIN Afiliados a1 ON a1.idAfiliado = t.idAfiliadoOrigen " +
                "LEFT JOIN Afiliados a2 ON a2.idAfiliado = t.idAfiliadoDestino " +
                "LEFT JOIN Usuarios u1 ON u1.idUsuario = c.idUsuario " +
                "LEFT JOIN Usuarios u2 ON u2.idUsuario = c.idUsuarioAutoriza " +
                "WHERE idTraspaso = " + Request.QueryString["idTraspaso"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();

            return strData;
        }

        protected void btnAutorizarCongelacion_Click(object sender, EventArgs e)
        {

        }

        protected void btnNoAutorizarCongelacion_Click(object sender, EventArgs e)
        {

        }

        protected void btnResponderCongelacion_Click(object sender, EventArgs e)
        {

        }

        protected void btnAutorizarIncapacidad_Click(object sender, EventArgs e)
        {

        }

        protected void btnNoAutorizarIncapacidad_Click(object sender, EventArgs e)
        {

        }

        protected void btnResponderIncapacidad_Click(object sender, EventArgs e)
        {

        }
    }
}