﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmCustomBll;
using System.Data;

namespace CmCustomUi.user
{
    public partial class historic : System.Web.UI.Page
    {
        private void fillGrid()
        {
            AcessHistoricBll acessHistoricBll = new AcessHistoricBll();
            DataTable dt = acessHistoricBll.consultaHistorico(txtUsuario.Text, txtDtInicio.Text, txtDtFim.Text);
            lblTotalTitulo.Text = " (" + dt.Rows.Count + ")";
            gvHistorico.DataSource = dt;
            gvHistorico.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {           
        }

        protected void lbPesquisar_Click(object sender, EventArgs e)
        {
            fillGrid();
        }

    }
}