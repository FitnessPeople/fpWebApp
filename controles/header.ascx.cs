using DocumentFormat.OpenXml.Wordprocessing;
using fpWebApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuariosOnline();
                CargarMensajes();
            }
        }

        private void CargarUsuariosOnline()
        {
            var lista = Application["ListaUsuarios"] as List<UsuarioOnline>;
            ltNroUsuarios.Text = lista != null ? lista.Count.ToString() : "0";

            var listaUsuarios = (List<UsuarioOnline>)Application["ListaUsuarios"];
            rpUsuariosEnLinea.DataSource = listaUsuarios;
            rpUsuariosEnLinea.DataBind();
        }

        private void CargarMensajes()
        {
            string strQuery = @"SELECT ci.idCorreo, u.NombreUsuario AS Remitente, ci.Asunto, 
                ci.FechaHora, ci.Leido, cc.NombreCategoria, cc.ColorCategoria, e.FotoEmpleado  
                FROM correointerno ci 
                INNER JOIN usuarios u ON u.idUsuario = ci.idUsuarioDe 
                INNER JOIN categoriasCorreo cc ON cc.idCategoriaCorreo = ci.idCategoriaCorreo 
                INNER JOIN empleados e ON e.DocumentoEmpleado = u.idEmpleado 
                WHERE ci.idsPara = '" + Session["idUsuario"].ToString() + @"' 
                AND ci.Leido = 0 
                ORDER BY FechaHora DESC 
                LIMIT 4";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpMensajes.DataSource = dt;
            rpMensajes.DataBind();

            ltNroMensajes.Text = dt.Rows.Count.ToString();

            dt.Dispose();
        }

        protected void tmRefrescarUsuarios_Tick(object sender, EventArgs e)
        {
            CargarUsuariosOnline();
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
    }
}