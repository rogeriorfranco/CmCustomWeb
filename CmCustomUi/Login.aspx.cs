﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmCustomDto;
using CmCustomBll;
using System.Net;

namespace CmCustomUi
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtUsuario.Focus();
                Check();
                if (Session["usuario"] != null)
                    Response.Redirect("Home");                    
            }
            
        }

        public string Check()
        {
            try
            {
                OperationsCmApiBll con = new OperationsCmApiBll();
                return con.getSession();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        protected void lbLogar_Click(object sender, EventArgs e)
        {
            try
            {                
                User usuario = new User();
                LoginBll loginBll = new LoginBll();
                usuario = loginBll.autentication(txtUsuario.Text.ToLower(), txtSenha.Text);

                Session["nomeAssociado"] = usuario.nomeAssociado;
                Session["usuario"] = usuario.usuario;
                Session["perfil"] = usuario.perfil;
                Session["email"] = usuario.email;
                Session["centroResultado"] = usuario.centroResultado;
                Session["CPF"] = usuario.CPF;                

                AcessHistoricBll acessHistoricBll = new AcessHistoricBll();
                AcessHistoric acessHistoric = new AcessHistoric();
                acessHistoric.perfil = usuario.perfil;
                acessHistoric.tipo_usuario = usuario.tipo_usuario;
                acessHistoric.usuario = txtUsuario.Text;
                acessHistoricBll.insereHistoricoAcesso(acessHistoric);

                Response.RedirectToRoute("REDIRECT_HOME");                
            }
            catch (Exception ex)
            {
                Utility.Alertbootsrap(ex.Message, this, UpdatePanelLogin);
            }
        }




    }
}