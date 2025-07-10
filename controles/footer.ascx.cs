using System;

namespace fpWebApp.controles
{
    public partial class footer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblCount.Text = Application["VisitorsCount"].ToString();
            lblAnho.Text = DateTime.Now.Year.ToString();
        }
    }
}