﻿using CmCustomDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmCustomBll;
namespace CmCustomUi.facility
{
    public partial class redesignate : System.Web.UI.Page
    {
        Cto cto = new Cto();

        private void fillGrid()
        {
            FacilityBll facilityBll = new FacilityBll();
            if (txtCtoSearch.Text != "" || txtIpSearch.Text != "")
                gvRedesignar.DataSource = facilityBll.getFacilitysRedesignate(txtCtoSearch.Text, txtIpSearch.Text, cto.point_id);

            gvRedesignar.DataBind();


            if (gvRedesignar.Rows.Count > 0)
            {
                txtRedesignar.Visible = true;
            }
            else
            {
                lbRedesignar.Attributes.Add("disabled", "true");
            }
        }

        private void fillFields()
        {
            if (Session["cto_redesignate"] != null)
                cto = (Cto)Session["cto_redesignate"];

            txtCircuito.Text = cto.circuito;
            txtIP.Text = cto.ipOlt;
            txtOlt.Text = cto.Olt_Nome;
            txtShelf.Text = cto.shelf_slot_porta.Split(':').First();
            txtSlot.Text = cto.shelf_slot_porta.Split(':')[1].ToString();
            txtPorta.Text = cto.shelf_slot_porta.Split(':').Last();
            txtPortaCto.Text = cto.ponto;
            txtCto.Text = cto.nome;
            txtCliente.Text = cto.cliente;
            txtStatus.Text = cto.status;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                fillFields();
        }

        protected void lbRedesignar_Click(object sender, EventArgs e)
        {
            try
            {
                cto = (Cto)Session["cto_redesignate"];

                OperationsCmApiBll operationsCmApiBll = new OperationsCmApiBll();
                operationsCmApiBll.redesignateFacility(Session["usuario"].ToString(), cto.idOlt
                    , Convert.ToInt32(gvRedesignar.DataKeys[gvRedesignar.SelectedIndex].Values["ID_OLT"]),
                    cto.point_id, cto.idSubPorta,
                    Convert.ToInt32(gvRedesignar.DataKeys[gvRedesignar.SelectedIndex].Values["POINT_ID"]),
                    Convert.ToInt32(gvRedesignar.DataKeys[gvRedesignar.SelectedIndex].Values["ID_SUB_PORTA"]),
                    txtCircuito.Text, txtStatus.Text);

                divMsg.Attributes.Add("class", "alert alert-success alert-dismissible");
                divMsg.Visible = true;
                lblMsg.Text = ViewState["MSG_REDESIGNATE"].ToString();
                lbRedesignar.Attributes.Remove("disabled");                
            }


            catch (Exception ex)
            {
                divMsg.Attributes.Add("class", "alert alert-danger alert-dismissible");
                divMsg.Visible = true;
                lblMsg.Text = string.Concat(ex.Message, " - ", ex.InnerException);
            }
        }

        protected void lbVoltar_Click(object sender, EventArgs e)
        {
            if (txtCto.Text != "")
                Session["CTO_SEARCH_NOME"] = (txtCto.Text.Contains('(') ? txtCto.Text.Split('(').First().Trim() : txtCto.Text.Trim());

            if (txtCircuito.Text != "")
                Session["CIRCUITO_SEARCH"] = txtCircuito.Text.Trim();

            Session["cto_redesignate"] = null;
            Response.Redirect("search", false);
        }

        protected void lblPesquisarCTO_Click(object sender, EventArgs e)
        {
            fillGrid();
        }

        protected void gvRedesignar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string newFacility = ((sender as GridView).SelectedRow.Cells[0].Controls[0] as LinkButton).Text;
                txtRedesignar.Text = newFacility;
                lbRedesignar.Attributes.Remove("disabled");

                ViewState["MSG_REDESIGNATE"] = "Circuito " + txtCircuito.Text + " originalmente em " + txtOlt.Text + "/" + txtShelf.Text + "/" + txtSlot.Text + "/" + txtPorta.Text + ":" + txtCto.Text + "/" + txtPortaCto.Text +
                    " foi movido para " + newFacility.Split('/').First() + "/" + newFacility.Split('/')[1] + "/" + newFacility.Split('/')[2] + "/" + newFacility.Split('/')[3] + ":" + newFacility.Split('|').Last().Split('/').Last() + " com sucesso.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}