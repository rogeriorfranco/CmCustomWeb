﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="historic.aspx.cs" Inherits="CmCustomUi.user.historic" %>

<%@ Register Src="~/userControls/UserControlUpdateProgress.ascx" TagPrefix="uc1" TagName="UserControlUpdateProgress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <script src="../scripts/jquery.inputmask.js"></script>
    <script src="../scripts/userHistoric.js"></script>    
    <asp:UpdatePanel ID="UpTitulo" runat="server">
        <ContentTemplate>
            <section class="content-header">
                <h1>Histórico Acesso<asp:Label ID="lblTotalTitulo" runat="server"></asp:Label></h1>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class='row'>

                    <div class="col-sm-5">
                        <div class="form-group">
                            <label>Usuário</label>
                            <asp:TextBox ID="txtUsuario" runat="server" class="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Data Inicial</label>
                            <asp:TextBox ID="txtDtInicio" ClientIDMode="Static" runat="server" class="form-control input-sm"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Data Final</label>
                            <asp:TextBox ID="txtDtFim" runat="server" class="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>


                    <div class="col-xs-2">
                        <div class="pull-right" style="padding-top: 15px">
                            <asp:UpdatePanel ID="upPesquisar" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lbPesquisar" runat="server" CssClass="btn btn-primary" OnClick="lbPesquisar_Click"><i class="fa fa-search"></i> Pesquisar</asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <hr />
                <asp:UpdatePanel ID="upGridView" runat="server">
                    <ContentTemplate>
                        <div class="table-responsive">
                            <asp:GridView ID="gvHistorico" ClientIDMode="Static" runat="server" CssClass="table table-bordered table-hover" EmptyDataText="Sem Informações" AllowPaging="True" AutoGenerateColumns="False" PageSize="200">
                                <Columns>
                                    <asp:BoundField DataField="USUARIO" HeaderText="Usuário" />
                                    <asp:BoundField DataField="PERFIL" HeaderText="Perfil" />
                                    <asp:BoundField DataField="TIPO_USUARIO" HeaderText="Tipo" />
                                    <asp:BoundField DataField="DT_ULTIMO_ACESSO" HeaderText="Ultimo Acesso" />
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Red" />
                                <HeaderStyle CssClass="thead" />
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <uc1:UserControlUpdateProgress runat="server" ID="UserControlUpdateProgress" />
</asp:Content>


