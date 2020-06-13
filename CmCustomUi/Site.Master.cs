using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmCustomBll;
using CmCustomDto;

namespace CmCustomUi
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("~/Login");

            if (!Page.IsPostBack)
                carregaDadosUsuario();
        }

        private void verificaPerfil()
        {
            MenuAdmistracao.Visible = (Session["perfil"].Equals("ADMINISTRADOR_TI") || Session["perfil"].Equals("ADMINISTRADOR_REDE") ? true : false);
        }

        private void carregaDadosUsuario()
        {
            if (Session["nomeAssociado"] != null)
            {
                TimeSpan tspan = DateTime.Now.TimeOfDay;
                if (((tspan >= new TimeSpan(6, 0, 0)) && (tspan <= new TimeSpan(12, 0, 0))))
                {
                    lblBemVindo.Text = "Bom Dia!";
                }
                else if (((tspan >= new TimeSpan(12, 0, 0)) && (tspan <= new TimeSpan(19, 0, 0))))
                {
                    lblBemVindo.Text = "Boa Tarde!";
                }
                else
                {
                    lblBemVindo.Text = "Boa Noite!";
                }

                lblUserLogin.Text = Session["nomeAssociado"].ToString();
                lblPerfil.Text = Session["perfil"].ToString();
                lblEmail.Text = Session["email"].ToString();
                lblCR.Text = Session["centroResultado"].ToString();
                lblCPF.Text = Session["CPF"].ToString();

                verificaPerfil();
                checkUserCmClient();
            }
        }

        private void checkUserCmClient()
        {
            UserBll userBll = new UserBll();
            User user = userBll.getSessionActiveCmClient(Session["usuario"].ToString());
            if (user.id != 0 && user.serial != 0)
            {
                cbUnlockUser.Checked = true;
                cbUnlockUser.Visible = true;
                ViewState["id"] = user.id;
                ViewState["serial"] = user.serial;
            }
        }

        protected void lbSair_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login");
        }

        protected void imbHome_Click(object sender, ImageClickEventArgs e)
        {
            Response.RedirectToRoute("REDIRECT_HOME");
        }

        protected void cbUnlockUser_CheckedChanged(object sender, EventArgs e)
        {
            UserBll userBll = new UserBll();
            userBll.disconnectUserCmClient(Convert.ToInt32(ViewState["id"]), Convert.ToInt32(ViewState["serial"]));
            cbUnlockUser.Visible = false;
        }

    }
}