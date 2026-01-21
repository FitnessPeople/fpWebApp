using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class soporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    CargarPaginas();
                    CargarTickets();
                    ltTitulo.Text = "Agregar solicitud de soporte";
                }
                else
                {
                    Response.Redirect("logout");
                }
            }
        }

        private void CargarPaginas()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<select id='ddlPaginas' name='ddlPaginas' class='form-control input-sm chosen-select'>");
            sb.AppendLine("<option value=''>Seleccione una página</option>");

            clasesglobales cg = new clasesglobales();

            using (DataTable dtCategorias = cg.CargarCategoriasPaginaPorPerfil(
                Convert.ToInt32(Session["idPerfil"])
            ))
            {
                foreach (DataRow cat in dtCategorias.Rows)
                {
                    string categoria = HttpUtility.HtmlEncode(
                        cat["NombreCategoriaPagina"].ToString()
                    );

                    sb.AppendLine($"<optgroup label='{categoria}'>");

                    using (DataTable dtPaginas = cg.CargarPaginasPorCategoriayPerfil(
                        Convert.ToInt32(cat["idCategoriaPagina"]),
                        Convert.ToInt32(Session["idPerfil"])
                    ))
                    {
                        foreach (DataRow pag in dtPaginas.Rows)
                        {
                            string pagina = HttpUtility.HtmlEncode(
                                pag["Pagina"].ToString()
                            );

                            sb.AppendLine(
                                $"<option value='{pag["idPagina"]}'>{pagina}</option>"
                            );
                        }
                    }

                    sb.AppendLine("</optgroup>");
                }
            }

            sb.AppendLine("</select>");

            contenedorSelect.InnerHtml = sb.ToString();
        }

        private void CargarTickets()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt;
            if (Session["idPerfil"].ToString() == "18") // Perfil 18: Ingeniero de Desarrollo
            {
                dt = cg.CargarSoportesFPMas(0);
            }
            else
            {
                dt = cg.CargarSoportesFPMas(Convert.ToInt32(Session["idUsuario"].ToString()));
            }

            rpTickets.DataSource = dt;
            rpTickets.DataBind();

            dt.Dispose();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string descripcion = txtDescripcion.Text;
            int idPagina = int.Parse(Request.Form["ddlPaginas"]);
            string idUsuario = Session["idUsuario"].ToString(); // Aquí se usa el ID del usuario logueado
            string strQuery = "INSERT INTO SoporteFPmas " +
                "(idPagina, idReportadoPor, FechaCreacionTicket, DescripcionTicket, EstadoTicket) " +
                "VALUES (" + idPagina + ", " + idUsuario + ", NOW(), '" + descripcion + "', 'Pendiente')";

            clasesglobales cg = new clasesglobales();
            cg.TraerDatosStr(strQuery);
            cg.InsertarLog(Session["idusuario"].ToString(), "ticket soporte fp+", "Agrega", "El usuario agregó un nuevo ticket de soporte fp+: " + descripcion + ".", "", "");

            Response.Redirect("soporte");
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {

        }

        protected void rpTickets_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Perfil actual
                int idPerfil = Convert.ToInt32(Session["idPerfil"]);

                // Controles
                var btnAsignar = (HtmlButton)e.Item.FindControl("btnAsignar");
                var ltIngeniero = (Literal)e.Item.FindControl("ltIngeniero");

                DataRowView row = (DataRowView)e.Item.DataItem;
                if (row["FechaCreacionTicket"] != DBNull.Value)
                {
                    DateTime FechaCreacionTicket = Convert.ToDateTime(row["FechaCreacionTicket"]);
                    TimeSpan diferencia = DateTime.Now - FechaCreacionTicket;

                    string leyenda = "";
                    if (diferencia.TotalMinutes < 1)
                    {
                        leyenda = "<i class=\"fa fa-hourglass-half m-r-sm\"></i>Hace menos de un minuto";
                    }
                    else if (diferencia.TotalMinutes < 60)
                    {
                        int min = (int)Math.Floor(diferencia.TotalMinutes);
                        leyenda = $"Hace {min} minuto" + (min == 1 ? "" : "s");
                        leyenda = "<i class=\"fa fa-hourglass-half m-r-sm\"></i>" + leyenda;
                    }
                    else if (diferencia.TotalHours < 24)
                    {
                        int hrs = (int)Math.Floor(diferencia.TotalHours);
                        leyenda = $"Hace {hrs} hora" + (hrs == 1 ? "" : "s");
                        leyenda = "<i class=\"fa fa-clock m-r-sm\"></i>" + leyenda;
                    }
                    else
                    {
                        int dias = (int)Math.Floor(diferencia.TotalDays);
                        leyenda = $"Hace {dias} día" + (dias == 1 ? "" : "s");
                        leyenda = "<i class=\"fa fa-calendar-days m-r-sm\"></i>" + leyenda;
                    }

                    Literal ltTiempo = (Literal)e.Item.FindControl("ltTiempoTranscurrido");
                    if (ltTiempo != null)
                    {
                        ltTiempo.Text = leyenda;
                    }
                }

                string strIngeniero = row["Ingeniero"].ToString();

                if (idPerfil == 18) // ejemplo: Ingeniero
                {
                    btnAsignar.Visible = true;
                    ltIngeniero.Visible = false;
                }
                else
                {
                    btnAsignar.Visible = false;
                    ltIngeniero.Visible = true;
                    ltIngeniero.Text = "Asignado a: " + strIngeniero;
                }

            }
        }
    }
}