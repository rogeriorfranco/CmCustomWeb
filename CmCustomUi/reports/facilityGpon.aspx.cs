using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmCustomBll;
using System.Data;
using System.Text;
namespace CmCustomUi.reports
{
    public partial class facilityGpon : System.Web.UI.Page
    {

        private void exportReport()
        {
            ReportBll relatorioBll = new ReportBll();
            Utility.ExportDataTableCsv(relatorioBll.relatorioGeralFacilidade(txtCircuito.Text, txtCto.Text, txtOlt.Text, ddlStatus.SelectedValue), "Facilidade GPON");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbExportar_Click(object sender, EventArgs e)
        {
            exportReport();
        }
    }
}