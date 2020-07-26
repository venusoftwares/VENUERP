using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VENUERP.Models;

namespace VENUERP.Reports
{
    public partial class Reports : System.Web.UI.Page
    {
      
      
        DatabaseContext db = new DatabaseContext();
        protected void page_init(object sender, EventArgs e)
        {
            ReportDocument rd = new ReportDocument();
            CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
            rd.Load(Server.MapPath(Convert.ToString(Session["Path"])));
            rd.SetDataSource(Session["Source"]);
            CrystalReportViewer1.ReportSource = rd;
            CrystalReportViewer1.RefreshReport();
        }
    }
}