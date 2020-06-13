using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmCustomBll;

namespace CmCustomUi.reports
{
    public partial class oltBand : System.Web.UI.Page
    {
        private void fillReport()
        {
            ReportBll relatorioBll = new ReportBll();
            gvOLT.DataSource = relatorioBll.relatorioBandaOltTotal(txtOlt.Text);
            gvOLT.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                fillReport();
            }
        }

        protected void lbPesquisar_Click(object sender, EventArgs e)
        {
            fillReport();
        }
    }
}