using CmCustomBll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CmCustomUi.reports
{
    public partial class historicViability : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.DefaultButton = lbPesquisar.UniqueID;
        }

        private void paginacao(GridViewPageEventArgs e, DataTable dt)
        {
            Utility.PaginacaoGridView(gvHistorico, e, dt);
        }

        private DataTable relatorioHistoricoViabilidade()
        {
            ReportBll reportBll = new ReportBll();
            return reportBll.relatorioHistoricoViabilidade(ddlStatus.SelectedValue, txtCircuito.Text, ddlEstado.SelectedValue, 
                txtLocalidade.Text, txtBairro.Text, txtLogradouro.Text, txtDtInicio.Text, txtDtFim.Text, txtCaixa.Text);
        }

        private void fillGrid()
        {
            DataTable dt = relatorioHistoricoViabilidade();
            lblTotalTitulo.Text = " (" + dt.Rows.Count + ")";
            gvHistorico.DataSource = dt;
            gvHistorico.DataBind();
        }

        protected void lbPesquisar_Click(object sender, EventArgs e)
        {
            fillGrid();
        }

        protected void gvHistorico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            paginacao(e, relatorioHistoricoViabilidade());
        }
    }
}