using System;
using System.Data;
using System.Web.UI.WebControls;


namespace fpWebApp
{
    public partial class correoeliminado : System.Web.UI.Page
    {
        private int PageSize = 10; // cantidad de registros por página
        public int CurrentPage
        {
            get
            {
                return ViewState["CurrentPage"] != null ? (int)ViewState["CurrentPage"] : 0;
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    CargarCategorias();
                    CargarMensajes();
                }
                else
                {
                    Response.Redirect("logout");
                }
            }
        }

        private void CargarMensajes()
        {
            string strQuery = @"
                SELECT ci.idCorreo, u.NombreUsuario AS Destinatario, ci.Asunto, 
                ci.FechaHora, ci.Leido, cc.NombreCategoria, cc.ColorCategoria 
                FROM correointerno ci 
                INNER JOIN usuarios u ON u.idUsuario = ci.idsPara 
                INNER JOIN categoriasCorreo cc ON cc.idCategoriaCorreo = ci.idCategoriaCorreo 
                WHERE ci.idUsuarioDe = " + Session["idUsuario"].ToString() + @" 
                AND Papelera = 1 
                ORDER BY FechaHora DESC";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dt.DefaultView;
            pds.AllowPaging = true;
            pds.PageSize = PageSize;
            pds.CurrentPageIndex = CurrentPage;

            // Verificación para habilitar/deshabilitar botones
            btnAnterior.Enabled = !pds.IsFirstPage;
            btnSiguiente.Enabled = !pds.IsLastPage;

            rpMensajes.DataSource = pds;
            rpMensajes.DataBind();

            dt.Dispose();

            strQuery = @"
                SELECT ci.idCorreo, u.NOmbreUsuario AS Remitente, ci.Asunto, ci.FechaHora 
                FROM correointerno ci 
                INNER JOIN usuarios u ON u.idUsuario = ci.idUsuarioDe 
                WHERE ci.idUsuarioDe = " + Session["idUsuario"].ToString() + @" 
                AND ci.Leido = 0 
                AND Papelera = 1 
                ORDER BY FechaHora DESC";

            DataTable dt1 = cg.TraerDatos(strQuery);

            ltNroMensajesSinLeer.Text = dt1.Rows.Count.ToString();

            dt1.Dispose();

            strQuery = @"
                SELECT ci.idCorreo, u.NOmbreUsuario AS Remitente, ci.Asunto, ci.FechaHora, ci.Leido 
                FROM correointerno ci 
                INNER JOIN usuarios u ON u.idUsuario = ci.idUsuarioDe 
                WHERE ci.idUsuarioDe = " + Session["idUsuario"].ToString() + @" 
                AND Papelera = 1 
                ORDER BY FechaHora DESC";

            DataTable dt2 = cg.TraerDatos(strQuery);

            ltNroMensajesPapelera.Text = dt2.Rows.Count.ToString();

            dt2.Dispose();

            strQuery = @"
                SELECT ci.idCorreo, u.NombreUsuario AS Destinatario, ci.Asunto, 
                ci.FechaHora, cc.NombreCategoria, cc.ColorCategoria, ci.Leido 
                FROM correointerno ci 
                INNER JOIN usuarios u ON u.idUsuario = ci.idsPara 
                INNER JOIN categoriasCorreo cc ON cc.idCategoriaCorreo = ci.idCategoriaCorreo 
                WHERE ci.idUsuarioDe = " + Session["idUsuario"].ToString() + @" 
                AND Papelera = 0 
                ORDER BY FechaHora DESC";

            DataTable dt3 = cg.TraerDatos(strQuery);

            ltNroMensajesEnviados.Text = dt3.Rows.Count.ToString();

            dt3.Dispose();

            strQuery = @"
                SELECT ci.idCorreo, u.NOmbreUsuario AS Remitente, ci.Asunto, 
                ci.FechaHora, cc.NombreCategoria, cc.ColorCategoria, ci.Leido  
                FROM correointerno ci 
                INNER JOIN usuarios u ON u.idUsuario = ci.idUsuarioDe 
                INNER JOIN categoriasCorreo cc ON cc.idCategoriaCorreo = ci.idCategoriaCorreo 
                WHERE ci.idsPara = " + Session["idUsuario"].ToString() + @" 
                AND Papelera = 0 
                ORDER BY FechaHora DESC";

            DataTable dt4 = cg.TraerDatos(strQuery);

            ltNroMensajesTotal.Text = dt4.Rows.Count.ToString();

            dt4.Dispose();
        }

        private void CargarCategorias()
        {
            string strQuery = @"SELECT * 
                FROM categoriascorreo ";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpCategorias.DataSource = dt;
            rpCategorias.DataBind();

            dt.Dispose();
        }

        protected void rpMensajes_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;

                if (row["FechaHora"] != DBNull.Value)
                {
                    DateTime fechaMensaje = Convert.ToDateTime(row["FechaHora"]);
                    TimeSpan diferencia = DateTime.Now - fechaMensaje;

                    string leyenda = "";
                    if (diferencia.TotalMinutes < 1)
                        leyenda = "Hace menos de un minuto";
                    else if (diferencia.TotalMinutes < 60)
                        leyenda = $"Hace {(int)diferencia.TotalMinutes} minuto{((int)diferencia.TotalMinutes == 1 ? "" : "s")}";
                    else if (diferencia.TotalHours < 24)
                        leyenda = $"Hace {(int)diferencia.TotalHours} hora{((int)diferencia.TotalHours == 1 ? "" : "s")}";
                    else
                        leyenda = $"Hace {(int)diferencia.TotalDays} día{((int)diferencia.TotalDays == 1 ? "" : "s")}";

                    Literal ltTiempo = (Literal)e.Item.FindControl("ltTiempoTranscurrido");
                    if (ltTiempo != null)
                        ltTiempo.Text = leyenda;
                }
            }
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            CurrentPage--;
            CargarMensajes();
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            CargarMensajes();
        }
    }
}