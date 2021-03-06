﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmCustomBll;
using System.Data;
namespace CmCustomUi.reports
{
    public partial class quantityNetwork : System.Web.UI.Page
    {
        static DataTable dt;
        static ReportBll relatorioBll = new ReportBll();

        private void fillReport()
        {
            try
            {
                dt = relatorioBll.relatorioQuantidadeRede(ddlTecnologia.SelectedValue);
                gvGpon.DataSource = dt;
                gvGpon.DataBind();
            }
            catch (Exception ex)
            {
                Utility.Alertbootsrap(ex.Message, this, upPesquisar);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                dt = relatorioBll.getFabricante();
                ddlTecnologia.DataSource = dt;
                ddlTecnologia.DataTextField = "FABRICANTE";
                ddlTecnologia.DataBind();
            }
        }

        protected void lbPesquisar_Click(object sender, EventArgs e)
        {
            fillReport();
        }

        protected void lbExportar_Click(object sender, EventArgs e)
        {
             Utility.ExportarDataTableExcel(relatorioBll.relatorioQuantidadeRede(ddlTecnologia.SelectedValue), "Quantidade Rede");
        }
    }
}