using MathNet.Numerics.LinearAlgebra.Complex.Solvers;
using System;
using System.Data;
using System.Data.SqlClient;

namespace fpWebApp
{
	public partial class asignarcita : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Agendar cita");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        Response.Redirect("agendarcita");
                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        clasesglobales cg = new clasesglobales();
                        try
                        {
                            string strQuery = "UPDATE DisponibilidadEspecialistas SET " +
                                "idAfiliado = " + Request.QueryString["idAfil"].ToString() + ", " +
                                "idUsuarioAsigna = " + Session["idusuario"].ToString() + " " +
                                "WHERE idDisponibilidad = " + Request.QueryString["id"].ToString();
                            
                            string mensaje = cg.TraerDatosStr(strQuery);

                            //Creamos el concepto de pago en la tabla PagosAdicionalesAfiliado
                            strQuery = "INSERT INTO PagosAdicionalesAfiliado (" +
                                "idPagoAdicional, idAfiliado, Valor, Cantidad, EstadoPago) " +
                                "VALUES (1, " + Request.QueryString["idAfil"].ToString() + ", 50000, 1, 'Pendiente')";

                            DataTable dtAfiliado = cg.ConsultarAfiliadoPorId(Convert.ToInt32(Request.QueryString["idAfil"].ToString()));

                            //Enviar correo al afiliado con la cita y el enlace de pago.
                            DataTable dtDisponibilidad = cg.TraerDatos(@"SELECT * 
                                FROM DisponibilidadEspecialistas 
                                WHERE idDisponibilidad = " + Request.QueryString["id"].ToString());

                            string fecha = String.Format("{0:yyyy-MM-dd}", dtDisponibilidad.Rows[0]["FechaHoraInicio"]);
                            string hora = String.Format("{0:HH:mm:ss}", dtDisponibilidad.Rows[0]["FechaHoraInicio"]);
                            string enlacePago = "https://fitnesspeoplecmdcolombia.com";

                            string mensajeCorreo =
                                "Estimado(a) afiliado(a)," + Environment.NewLine + Environment.NewLine +
                                "Reciba un cordial saludo." + Environment.NewLine + Environment.NewLine +
                                "Le confirmamos que su cita ha sido agendada exitosamente con el siguiente detalle:" + Environment.NewLine + Environment.NewLine +
                                "Fecha: " + fecha + Environment.NewLine +
                                "Hora: " + hora + Environment.NewLine + Environment.NewLine +
                                "Para confirmar definitivamente su cita, es necesario realizar el pago correspondiente a través del siguiente enlace:" + Environment.NewLine + Environment.NewLine +
                                "Enlace de pago: " + enlacePago + Environment.NewLine + Environment.NewLine +
                                "Le recomendamos efectuar el pago lo antes posible para garantizar la reserva de su espacio. Una vez realizado, recibirá la confirmación automática de su cita." + Environment.NewLine + Environment.NewLine +
                                "Si tiene alguna inquietud, puede responder a este correo o comunicarse con nuestro equipo de soporte." + Environment.NewLine + Environment.NewLine +
                                "Cordialmente," + Environment.NewLine +
                                "Equipo Fitness People CMD";

                            cg.EnviarCorreo("sistemas@fitnesspeoplecmd.com", dtAfiliado.Rows[0]["EmailAfiliado"].ToString(), "Cita agendada", mensajeCorreo);
                        }
                        catch (SqlException ex)
                        {
                            string mensaje = ex.Message;
                        }
                    }
                    Response.Redirect("agendarcita");
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
    }
}