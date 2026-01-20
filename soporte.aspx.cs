using System;
using System.Data;
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
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPaginas();

            dt.Columns.Add("NombrePagina", typeof(string), "'🌐 ' + NombreCategoriaPagina + ' ◾ ' + Pagina");

            ListItem li = new ListItem("Categoria y página", "");
            ddlPaginas.Items.Add(li);

            ddlPaginas.DataSource = dt;
            ddlPaginas.DataValueField = "idPagina";
            ddlPaginas.DataTextField = "NombrePagina";
            ddlPaginas.DataBind();

            dt.Dispose();
        }

        private void CargarTickets()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.CargarSoportesFPMas(Convert.ToInt32(Session["idUsuario"].ToString()));

            rpTickets.DataSource = dt;
            rpTickets.DataBind();

            dt.Dispose();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string descripcion = txtDescripcion.Text;
            int idPagina = int.Parse(ddlPaginas.SelectedValue);
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
            }
        }
    }
}