using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmCustomDto;
using CmCustomBll;
using System.Data;
using System.Net.Sockets;
namespace CmCustomUi.viability
{
    public partial class search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.DefaultButton = lbPesquisar.UniqueID;
        }
     
        private void getViability()
        {
            try
            {
                ddlBairro.Items.Add(Request.Form["ctl00$ContentPlaceHolder$ddlBairro"]);
                LocalityBll localityBll = new LocalityBll();
                WebClient proxy = new WebClient();
                string serviceURL = "http://" + Dns.GetHostName() + ":8080/CmCustomAPI/Aprovisionamento/VIABILIDADE/NULL/NULL/NULL/NULL/NULL/NULL/NULL/OCUPADO/FALSO/NULL/N/NULL/NULL/" + (Request.Form["ctl00$ContentPlaceHolder$ddlBairro"] == null ? "NULL" : Request.Form["ctl00$ContentPlaceHolder$ddlBairro"]) + "/" + txtNumero.Text + "/NULL/NULL/NULL/NULL/NULL/NULL/NULL/NULL/NULL/NULL/" + txtLogradouro.Text + "/" + txtLocalidade.Text + "/" + ddlEstado.SelectedValue;                
                byte[] data = proxy.DownloadData(serviceURL);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(Elements));
                Elements elements = obj.ReadObject(stream) as Elements;
                if (elements.cto != null)
                {
                    txtCto.Text = (elements.cto.Contains('(') ? elements.cto.Split('(').First() : elements.cto);
                    txtEstacao.Text = elements.estacao;
                    txtLocal.Text = elements.localidade;
                    txtPortaCto.Text = elements.porta_cto;
                    txtOlt.Text = elements.olt;
                    txtIpOlt.Text = elements.ip_olt;
                    txtModeloOlt.Text = elements.modelo;
                    txtShelf.Text = elements.shelf;
                    txtSlot.Text = elements.slot;
                    txtPortaFisica.Text = elements.porta;
                    txtPortaLogica.Text = elements.porta_logica;
                    txtVelocidades.Text = elements.velocidades;
                }

                txtComprimentoDrop.Text = elements.comprimento_drop != null ? elements.comprimento_drop : string.Empty;
                txtLatCLiente.Text = elements.latitude_cliente != null ? elements.latitude_cliente : string.Empty;
                txtLongCliente.Text = elements.longitude_cliente != null ? elements.longitude_cliente : string.Empty;

                // iFrameMaps.Src = "https://maps.google.com?saddr=" + string.Concat(elements.latitude_cliente.Replace(",", "."), ",", elements.longitude_cliente.Replace(",", "."), "&daddr=", elements.latitude_rede.Replace(",", "."), ",", elements.longitude_rede.Replace(",", "."), "&output=embed");

                //if (elements.status_viabilidade == "MANUAL" || elements.status_viabilidade == "VIAVEL")

                if (elements.latitude_cliente != null && elements.longitude_cliente != null && elements.latitude_rede != null && elements.longitude_rede != null)
                {
                    iFrameMaps.Src = string.Format("../map/view?lat_cliente={0}&long_cliente={1}&lat_rede={2}&long_rede={3}&filtro=cto", elements.latitude_cliente.Replace(",", "."), elements.longitude_cliente.Replace(",", "."), elements.latitude_rede.Replace(",", "."), elements.longitude_rede.Replace(",", "."));
                }
                else
                {
                    iFrameMaps.Src = string.Empty;
                }

                if (elements.status_viabilidade == "MANUAL")
                    divMsg.Attributes.Add("class", "alert alert-warning alert-dismissible");

                if (elements.status_viabilidade == "VIAVEL")
                {
                    divMsg.Attributes.Add("class", "alert alert-success alert-dismissible");
                    lbDesignar.Visible = true;
                    Session["CTO_SEARCH_NOME"] = txtCto.Text.Trim();
                    Session["STATUS_SEARCH"] = elements.status;
                }
                else
                {
                    lbDesignar.Visible = false;
                    Session["CTO_SEARCH_NOME"] = null;
                    Session["STATUS_SEARCH"] = null;
                }


                if (elements.status_viabilidade == "INVIAVEL")
                    divMsg.Attributes.Add("class", "alert alert-danger alert-dismissible");

                if (elements.status_viabilidade != null)
                {
                    divMsg.Visible = true;
                    lblMsg.Text = elements.status_viabilidade + " - " + elements.desc_retorno;
                }
                else
                {
                    divMsg.Visible = false;
                    lblMsg.Text = string.Empty;
                }


            }
            catch (Exception ex)
            {
                divMsg.Attributes.Add("class", "alert alert-danger alert-dismissible");
                divMsg.Visible = true;
                lblMsg.Text = string.Concat(ex.Message, " - ", ex.InnerException);
            }
        }

        protected void lbPesquisar_Click(object sender, EventArgs e)
        {
            getViability();
        }

        protected void lbDesignar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../facility/search", false);
        }




    }
}