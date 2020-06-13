using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmCustomBll;
using CmCustomDto;
using System.Data;

namespace CmCustomUi.facility
{
    public partial class search : System.Web.UI.Page
    {

        Cto cto = new Cto();
        Olt olt = new Olt();

        private void fillGrid()
        {
            try
            {
                FacilityBll facilityBll = new FacilityBll();
                DataTable dt = facilityBll.consultaFacilidades(txtCircuito.Text, txtEstacao.Text, ddlStatus.SelectedValue, txtCnl.Text, txtSerial.Text,
                    txtCto.Text, txtOlt.Text, txtIpOlt.Text, txtCtoAntiga.Text, txtOuterVlan.Text, txtInnerVlan.Text);
                lblTotalTitulo.Text = " (" + dt.Rows.Count + ")";
                gvFacilidade.DataSource = dt;
                gvFacilidade.DataBind();
            }
            catch (Exception ex)
            {
                Utility.Alertbootsrap(ex.Message, this, upGridView);
            }
        }

        //private void paginacao(GridViewPageEventArgs e)
        //{
        //    Utility.PaginacaoGridView(gvFacilidade, e, dt);
        //}

        private void selectRow(int index)
        {
            try
            {
                if (gvFacilidade.DataKeys[index].Values["ID_SUB_PORTA"].Equals(DBNull.Value))
                {
                    FacilityBll facilityBll = new FacilityBll();

                    if (facilityBll.getTotalSubPortaVagoByPorta(Convert.ToInt32(gvFacilidade.DataKeys[index].Values["COMPONENT_ID_PORTA_OLT"])) < 1)
                        throw new Exception(string.Concat((gvFacilidade.Rows[index].Cells[13].Text.Contains('(') ? gvFacilidade.Rows[index].Cells[13].Text.Split('(').First().Trim() : gvFacilidade.Rows[index].Cells[13].Text)
                            , ": ", gvFacilidade.Rows[index].Cells[9].Text, "/", gvFacilidade.Rows[index].Cells[10].Text, "/", gvFacilidade.Rows[index].Cells[11].Text, " sem sub porta VAGO"));
                                           
                    olt.idSubPorta = facilityBll.getIdSubPortaVagoByPorta(Convert.ToInt32(gvFacilidade.DataKeys[index].Values["COMPONENT_ID_PORTA_OLT"]));
                    if (olt.idSubPorta == 0)
                        throw new Exception(string.Concat((gvFacilidade.Rows[index].Cells[13].Text.Contains('(') ? gvFacilidade.Rows[index].Cells[13].Text.Split('(').First().Trim() : gvFacilidade.Rows[index].Cells[13].Text)
                            , ": ", gvFacilidade.Rows[index].Cells[9].Text, "/", gvFacilidade.Rows[index].Cells[10].Text, "/", gvFacilidade.Rows[index].Cells[11].Text, " sem vlan disponível"));

                }
                else
                {
                    olt.idSubPorta = Convert.ToInt32(gvFacilidade.DataKeys[index].Values["ID_SUB_PORTA"]);
                }

                cto.id = Convert.ToInt32(gvFacilidade.DataKeys[index].Values["DESIGNATION_ID"]);
                cto.component_id = Convert.ToInt32(gvFacilidade.DataKeys[index].Values["COMPONENT_ID"]);
                cto.point_id = Convert.ToInt32(gvFacilidade.DataKeys[index].Values["POINT_ID"]);
                cto.nome = (gvFacilidade.Rows[index].Cells[2].Text.Contains('(') ? gvFacilidade.Rows[index].Cells[2].Text.Split('(').First().Trim() : gvFacilidade.Rows[index].Cells[2].Text);
                cto.nome_antigo = (gvFacilidade.Rows[index].Cells[2].Text.Contains('(') ? gvFacilidade.Rows[index].Cells[2].Text.Split('(').Last().Replace(")", "") : string.Empty);
                cto.estacao = gvFacilidade.Rows[index].Cells[6].Text;
                cto.ponto = gvFacilidade.Rows[index].Cells[5].Text;
                cto.cnl = gvFacilidade.Rows[index].Cells[8].Text;
                cto.status = ((Label)gvFacilidade.Rows[index].FindControl("lblStatus")).Text;
                cto.circuito = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[4].Text);
                cto.localidade = gvFacilidade.Rows[index].Cells[7].Text;
                Session["cto_search"] = cto;

                olt.status = cto.status;
                olt.circuito = cto.circuito;
                olt.idlogradouro = (gvFacilidade.DataKeys[index].Values["ID_LOGRADOURO"] != DBNull.Value ? Convert.ToInt32(gvFacilidade.DataKeys[index].Values["ID_LOGRADOURO"]) : 0);
                olt.tecnologia = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[12].Text);
                olt.nome = (gvFacilidade.Rows[index].Cells[13].Text.Contains('(') ? gvFacilidade.Rows[index].Cells[13].Text.Split('(').First().Trim() : gvFacilidade.Rows[index].Cells[13].Text);
                olt.nome_gerencia = (gvFacilidade.Rows[index].Cells[13].Text.Contains('(') ? gvFacilidade.Rows[index].Cells[13].Text.Split('(').Last().Replace(")", "") : string.Empty);
                olt.modelo = gvFacilidade.Rows[index].Cells[15].Text;
                olt.ipOlt = gvFacilidade.Rows[index].Cells[14].Text;
                olt.shelf = gvFacilidade.Rows[index].Cells[9].Text;
                olt.slot = gvFacilidade.Rows[index].Cells[10].Text;
                olt.porta = gvFacilidade.Rows[index].Cells[11].Text;
                olt.ont_id = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[20].Text);
                olt.vlan_banda_larga = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[16].Text);
                olt.vlan_voz_vobb = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[17].Text);
                olt.vlan_outer = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[23].Text);
                olt.vlan_inner = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[24].Text);
                olt.produto = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[18].Text);
                olt.cliente = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[19].Text);
                olt.banda_mb = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[20].Text);
                olt.ont_id = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[21].Text);
                olt.serial = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[22].Text);
                olt.tipoVlanBandaLarga = gvFacilidade.DataKeys[index].Values["TIPO_VLAN_BANDA_LARGA"].ToString();
                Session["olt_search"] = olt;

                Response.Redirect("change");
            }
            catch (Exception ex)
            {
                Utility.Alertbootsrap(ex.Message, this, upGridView);
            }
        }

        private void redesignar(int index)
        {
            Cto cto = new Cto();
            cto.point_id = Convert.ToInt32(gvFacilidade.DataKeys[index].Values["POINT_ID"]);
            cto.idSubPorta = Convert.ToInt32(gvFacilidade.DataKeys[index].Values["ID_SUB_PORTA"]);
            cto.idOlt = Convert.ToInt32(gvFacilidade.DataKeys[index].Values["ID_OLT"]);
            cto.circuito = gvFacilidade.Rows[index].Cells[4].Text;
            cto.nome = gvFacilidade.Rows[index].Cells[2].Text;
            cto.Olt_Nome = gvFacilidade.Rows[index].Cells[13].Text;
            cto.ipOlt = gvFacilidade.Rows[index].Cells[14].Text;
            cto.ponto = gvFacilidade.Rows[index].Cells[5].Text;
            cto.shelf_slot_porta = string.Concat(gvFacilidade.Rows[index].Cells[9].Text, ":", gvFacilidade.Rows[index].Cells[10].Text, ":", gvFacilidade.Rows[index].Cells[11].Text);
            cto.cliente = gvFacilidade.Rows[index].Cells[18].Text;
            cto.status = ((Label)gvFacilidade.Rows[index].FindControl("lblStatus")).Text;
            Session["cto_redesignate"] = cto;

            olt.produto = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[17].Text);
            olt.banda_mb = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[19].Text);
            olt.ont_id = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[20].Text);
            olt.serial = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[21].Text);
            olt.vlan_outer = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[22].Text);
            olt.vlan_inner = Server.HtmlDecode(gvFacilidade.Rows[index].Cells[23].Text);

            Session["olt_search"] = olt;

            Response.Redirect("redesignate");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.DefaultButton = lbPesquisar.UniqueID;
            if (!Page.IsPostBack)
            {
                if (Session["CTO_SEARCH_NOME"] != null)
                {
                    txtCto.Text = Session["CTO_SEARCH_NOME"].ToString();

                    if (Session["STATUS_SEARCH"] != null)
                        ddlStatus.SelectedValue = Session["STATUS_SEARCH"].ToString();

                    if (Session["CIRCUITO_SEARCH"] != null)
                        txtCircuito.Text = Session["CIRCUITO_SEARCH"].ToString();

                    fillGrid();
                    Session["CTO_SEARCH_NOME"] = null;
                    Session["STATUS_SEARCH"] = null;
                    Session["CIRCUITO_SEARCH"] = null;
                }
            }

        }

        //protected void gvFacilidade_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    paginacao(e);
        //}

        protected void lbPesquisar_Click(object sender, EventArgs e)
        {
            fillGrid();
        }

        protected void gvFacilidade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                LinkButton lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbEdit.CommandArgument = e.Row.RowIndex.ToString();

                LinkButton lbRedesignar = (LinkButton)e.Row.FindControl("lbRedesignar");
                lbRedesignar.CommandArgument = e.Row.RowIndex.ToString();

                if (!string.IsNullOrWhiteSpace(DataBinder.Eval(e.Row.DataItem, "NOME_ANTIGO_CTO").ToString()))
                    e.Row.Cells[2].Text = string.Concat(DataBinder.Eval(e.Row.DataItem, "TEXT"), " (", DataBinder.Eval(e.Row.DataItem, "NOME_ANTIGO_CTO"), ")");

                if (!string.IsNullOrWhiteSpace(DataBinder.Eval(e.Row.DataItem, "NOME_OLT_GERENCIA").ToString()))
                    e.Row.Cells[13].Text = string.Concat(DataBinder.Eval(e.Row.DataItem, "OLT"), " (", DataBinder.Eval(e.Row.DataItem, "NOME_OLT_GERENCIA"), ")");

                if (DataBinder.Eval(e.Row.DataItem, "FULLNAMEOLT") != DBNull.Value)
                    if (DataBinder.Eval(e.Row.DataItem, "FULLNAMEOLT").ToString().Split('>')[1].ToString().Split('<').First().Split('/').Count() == 3)
                    {
                        e.Row.Cells[9].Text = DataBinder.Eval(e.Row.DataItem, "FULLNAMEOLT").ToString().Split('>')[1].ToString().Split('<').First().Split('/').First();
                        e.Row.Cells[10].Text = DataBinder.Eval(e.Row.DataItem, "FULLNAMEOLT").ToString().Split('>')[1].ToString().Split('<').First().Split('/')[1].ToString();
                        e.Row.Cells[11].Text = DataBinder.Eval(e.Row.DataItem, "FULLNAMEOLT").ToString().Split('>')[1].ToString().Split('<').First().Split('/').Last();
                    }
                    else
                    {
                        e.Row.Cells[9].Text = DataBinder.Eval(e.Row.DataItem, "FULLNAMEOLT").ToString().Split('>')[1].ToString().Split('/')[0].ToString();
                        e.Row.Cells[10].Text = DataBinder.Eval(e.Row.DataItem, "FULLNAMEOLT").ToString().Split('>')[1].ToString().Split('<')[0].ToString().Split('/').Last();
                        e.Row.Cells[11].Text = DataBinder.Eval(e.Row.DataItem, "FULLNAMEOLT").ToString().Split('>')[2].ToString().Split('<').First();
                    }

                if (gvFacilidade.DataKeys[e.Row.RowIndex].Values["ID_SUB_PORTA"] != DBNull.Value)
                    lbRedesignar.Visible = true;

                if (gvFacilidade.DataKeys[e.Row.RowIndex].Values["COMPONENT_ID_PORTA_OLT"] == DBNull.Value)
                {
                    lbEdit.Visible = false;
                }
                else
                {
                    lbEdit.Visible = true;
                    lbEdit.ToolTip = "Editar";
                }

                switch (lblStatus.Text)
                {
                    case "VAGO":
                        lblStatus.Attributes.Add("class", "label label-primary");
                        break;
                    case "OCUPADO":
                        lblStatus.Attributes.Add("class", "label label-success");
                        break;
                    case "DESIGNADO":
                        lblStatus.Attributes.Add("class", "label label-warning");
                        break;
                    case "DEFEITO":
                        lblStatus.Attributes.Add("class", "label label-danger");
                        break;
                    case "RESERVADO":
                        lblStatus.Attributes.Add("class", "label label-info");
                        break;
                    case "AUDITORIA":
                        lblStatus.Attributes.Add("class", "label label-default");
                        break;
                }
            }

        }

        protected void gvFacilidade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editar"))
                selectRow(Convert.ToInt32(e.CommandArgument));

            if (e.CommandName.Equals("redesignar"))
                redesignar(Convert.ToInt32(e.CommandArgument));
        }


    }
}