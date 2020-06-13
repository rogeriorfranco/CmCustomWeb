﻿#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmCustomDto;
using CmCustomBll;
using System.Data;
#endregion

namespace CmCustomUi.facility
{
    public partial class change : System.Web.UI.Page
    {
        #region variaveis Globais
        FacilityBll facilityBll = new FacilityBll();
        Olt olt = new Olt();
        Cto cto = new Cto();
        #endregion
        #region metodos privados
        private void checkProfile()
        {
            if (Session["perfil"].Equals("LEITURA"))
            {
                lbSalvar.Visible = false;
                lbLiberar.Visible = false;
            }

            if (ddlStatus.SelectedValue.Equals("VAGO"))
                lbLiberar.Visible = false;
        }

        private void getUrl()
        {
            cto = (Cto)Session["cto_search"];
            DataRow dr = facilityBll.getLatLongByIdCto(cto.id);

            if (dr != null)
                if (!dr["GEO_Y"].Equals("0") && !dr["GEO_X"].Equals("0"))
                {
                    ViewState["URL_CTO"] = "http://maps.google.com/maps?&z=16&q=" + dr["GEO_Y"].ToString().Replace(",", ".") + "+" + dr["GEO_X"].ToString().Replace(",", ".") + "&ll=" + dr["GEO_Y"].ToString().Replace(",", ".") + "+" + dr["GEO_X"].ToString().Replace(",", ".") + "";
                    lbMapa.Visible = true;
                }
                else
                {
                    lbMapa.Visible = false;
                }
        }

        private void showMaps()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "window.open('" + ViewState["URL_CTO"] + "', 'popup_window', 'width=1000,height=600,left=250,top=50,resizable=yes');", true);
        }

        private void fillFields()
        {
            checkProfile();
            getUrl();

            if (Session["cto_search"] != null)
            {
                cto = (Cto)Session["cto_search"];

                txtCto.Text = cto.nome;
                txtCtoAntigo.Text = cto.nome_antigo;
                txtEstacao.Text = cto.estacao;
                txtCnl.Text = cto.cnl;
                txtPortaCTO.Text = cto.ponto;
                txtLocalidade.Text = cto.localidade;
                txtPais.Text = "BR";
                ddlStatus.SelectedValue = cto.status;
                txtCircuito.Text = cto.circuito;
            }

            if (Session["olt_search"] != null)
            {
                olt = (Olt)Session["olt_search"];

                txtOlt.Text = olt.nome;
                txtOltGerencia.Text = olt.nome_gerencia;
                txtModelo.Text = olt.modelo;
                txtIPOlt.Text = olt.ipOlt;
                txtShelf.Text = olt.shelf;
                txtSlot.Text = olt.slot;
                txtPorta.Text = olt.porta;
                txtTecnologia.Text = "GPON " + olt.tecnologia;
                txtIPOlt.Text = olt.ipOlt;
                txtVlanInner.Text = olt.vlan_inner;
                txtVlan.Text = olt.vlan_outer;
                txtVlanCompartilhada.Text = olt.vlan_banda_larga;
                txtVlanVobb.Text = olt.vlan_voz_vobb;
                txtCliente.Text = olt.cliente;
                txtProduto.Text = olt.produto;
                txtBandaMB.Text = olt.banda_mb;
                txtSerial.Text = olt.serial;
                txtOnt.Text = olt.ont_id;
                hfLogradouro.Value = olt.idlogradouro.ToString();

                fillModelOnt(olt.tecnologia);

                if (cto.status.Equals("RESERVADO"))
                    txtDtValidadeReserva.Attributes.Remove("disabled");
            }

            if (!olt.idSubPorta.Equals(DBNull.Value) && olt.idSubPorta != 0)
            {
                DataRow drOlt = facilityBll.getFacilityOLTByIdSubPorta(olt.idSubPorta);

                txtDtValidadeReserva.Text = drOlt["VALIDADE_RESERVA"].ToString();
                txtNomeLogradouro.Text = drOlt["LOGRADOURO"].ToString();
                ddlBairro.Items.Add(new ListItem(drOlt["BAIRRO"].ToString()));

                if (ddlModeloOnt.Items.FindByValue(drOlt["MODELO_ONT"].ToString()) != null)
                    ddlModeloOnt.Items.FindByValue(drOlt["MODELO_ONT"].ToString()).Selected = true;

                txtObs.Text = drOlt["OBSERVACAO"].ToString();
                txtNumero.Text = drOlt["NUMERO_LOTE"].ToString();
                txtBandaUpLink.Text = drOlt["BANDA_UPLINK"].ToString();
                txtVlanInner.Text = drOlt["VLAN_OUTER_INNER"].ToString().Split(':').Last();
                txtVlan.Text = drOlt["VLAN_OUTER_INNER"].ToString().Split(':').First();
                txtOnt.Text = drOlt["ONT_ID"].ToString();
            }

            if (olt.tipoVlanBandaLarga == "COMPARTILHADA")
            {
                txtVlan.ReadOnly = true;
                txtVlanInner.ReadOnly = true;
                if (txtVlanInner.Text == string.Empty)
                    txtVlanInner.Text = "0";
                if (txtVlan.Text == string.Empty)
                    txtVlan.Text = "0";

                dVlan.Visible = true;
            }
            else
            {
                dVlan.Visible = false;
                dVlanVobb.Attributes.Add("class", "col-sm-2");
            }

            if (!cto.status.Equals("VAGO"))
                lbLiberar.Visible = true;

        }

        private void setFacility()
        {
            olt = (Olt)Session["olt_search"];
            cto = (Cto)Session["cto_search"];

            olt.circuito = txtCircuito.Text;
            olt.status = ddlStatus.SelectedValue;
            olt.vlan_inner = txtVlanInner.Text;
            olt.vlan_outer = txtVlan.Text;
            olt.cliente = txtCliente.Text;
            olt.produto = txtProduto.Text;
            olt.banda_mb = txtBandaMB.Text;
            olt.banda_uplink = txtBandaUpLink.Text;
            olt.serial = txtSerial.Text;
            olt.ont_id = txtOnt.Text;
            olt.validade_reserva = txtDtValidadeReserva.Text;
            olt.nro_lote = txtNumero.Text;
            olt.bairro = Request.Form["ctl00$ContentPlaceHolder$ddlBairro"];
            olt.modelo_ont = ddlModeloOnt.SelectedValue;
            olt.observacao = txtObs.Text;
            olt.idlogradouro = (hfLogradouro.Value != "" ? Convert.ToInt32(hfLogradouro.Value) : 0);
        }

        private void save(string acao)
        {
            try
            {
                setFacility();
                OperationsCmApiBll operationsCmApiBll = new OperationsCmApiBll();
                olt.acao = acao;
                operationsCmApiBll.atualizaFacilidades(cto.point_id, olt, Session["usuario"].ToString());

                if (acao.Equals("LIBERAR"))
                {
                    Session["CTO_SEARCH_NOME"] = cto.nome;
                    Response.Redirect("search", false);
                }
            }
            catch (Exception ex)
            {
                Utility.Alertbootsrap(ex.Message, this, UpSave);
            }

        }

        private void fillModelOnt(string fabricante)
        {
            ddlModeloOnt.DataSource = facilityBll.getModelOnt(fabricante);
            ddlModeloOnt.DataValueField = "MODELO";
            ddlModeloOnt.DataBind();

        }

        #endregion

        #region eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.DefaultButton = lbSalvar.UniqueID;
            AlertConfirmBootstrap.btn_Click += new EventHandler(btnConfirmaOk_ButtonClick);
            if (!Page.IsPostBack)
                fillFields();
        }

        protected void lbSalvar_Click(object sender, EventArgs e)
        {
            save("SALVAR");
        }

        protected void btnConfirmaOk_ButtonClick(object sender, EventArgs e)
        {
            save("LIBERAR");
        }

        protected void lbLiberar_Click(object sender, EventArgs e)
        {
            Utility.AlertbootsrapConfirm("Deseja realmente liberar essa facilidade?", this, UpSave);
        }

        protected void lbVoltar_Click(object sender, EventArgs e)
        {
            if (txtCto.Text != "")
                Session["CTO_SEARCH_NOME"] = txtCto.Text;

            if (txtCircuito.Text != "")
                Session["CIRCUITO_SEARCH"] = txtCircuito.Text.Trim();

            Session["cto_search"] = null;
            Session["olt_search"] = null;
            Response.Redirect("search", false);
        }

        protected void lbMapa_Click(object sender, EventArgs e)
        {
            showMaps();
        }

        #endregion
    }
}
