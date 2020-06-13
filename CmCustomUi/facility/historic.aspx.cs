﻿using CmCustomBll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CmCustomUi.facility
{
    public partial class historic : System.Web.UI.Page
    {

        private void fillGrid()
        {
            FacilityHistoricBll facilityHistoricBll = new FacilityHistoricBll();
            DataTable dt = facilityHistoricBll.consultaHistorico(txtUsuario.Text, txtCircuito.Text, txtVlan.Text, txtVlanInner.Text,
                txtSerial.Text, txtShelf.Text, txtSlot.Text, txtPorta.Text, txtOntId.Text, txtIpOlt.Text, txtDtInicio.Text, txtDtFim.Text);
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