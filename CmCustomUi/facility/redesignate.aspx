﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="redesignate.aspx.cs" Inherits="CmCustomUi.facility.redesignate" %>

<%@ Register Src="~/userControls/UserControlUpdateProgress.ascx" TagPrefix="uc1" TagName="UserControlUpdateProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <section class="content-header">
        <h1>Redesignação de Circuito</h1>
    </section>

    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <asp:UpdatePanel ID="upMsg" runat="server">
                    <ContentTemplate>
                        <div id="divMsg" runat="server" visible="false">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class='row'>
                    <div class="col-md-6">
                        <div class="box box-solid">
                            <div class="box-header with-border">
                                <i class="fa fa-share"></i>
                                <h3 class="box-title">Alocação Atual</h3>
                            </div>
                            <div class="box-body clearfix">
                                <blockquote>
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label for="inputEmail" class="control-label col-xs-2">Circuito</label>
                                            <div class="col-xs-6">
                                                <asp:TextBox ID="txtCircuito" runat="server" class="form-control input-sm" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-xs-2">IP</label>
                                            <div class="col-xs-6">
                                                <asp:TextBox ID="txtIP" runat="server" class="form-control input-sm" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-xs-2">Olt</label>
                                            <div class="col-xs-6">
                                                <asp:TextBox ID="txtOlt" runat="server" class="form-control input-sm" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-xs-2">Shelf</label>
                                            <div class="col-xs-6">
                                                <asp:TextBox ID="txtShelf" runat="server" class="form-control input-sm" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-xs-2">Slot</label>
                                            <div class="col-xs-6">
                                                <asp:TextBox ID="txtSlot" runat="server" class="form-control input-sm" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-xs-2">Porta</label>
                                            <div class="col-xs-6">
                                                <asp:TextBox ID="txtPorta" runat="server" class="form-control input-sm" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-xs-2">Cto</label>
                                            <div class="col-xs-6">
                                                <asp:TextBox ID="txtCto" runat="server" class="form-control input-sm" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-xs-2">P. Cto</label>
                                            <div class="col-xs-6">
                                                <asp:TextBox ID="txtPortaCto" runat="server" class="form-control input-sm" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-xs-2">Cliente</label>
                                            <div class="col-xs-6">
                                                <asp:TextBox ID="txtCliente" runat="server" class="form-control input-sm" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-xs-2">Status</label>
                                            <div class="col-xs-6">
                                                <asp:TextBox ID="txtStatus" runat="server" class="form-control input-sm" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </blockquote>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="box box-solid">
                            <div class="box-header with-border">
                                <i class="fa fa-paste"></i>
                                <h3 class="box-title">Nova Alocação</h3>
                            </div>
                            <div class="box-body clearfix">
                                <blockquote>
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-xs-2">IP</label>
                                            <asp:UpdatePanel ID="upIpSearch" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-xs-6">
                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtIpSearch" runat="server" class="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <asp:LinkButton ID="lbPesquisarIP" runat="server" class="btn btn-info btn-flat" type="button" OnClick="lblPesquisarCTO_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-xs-2">CTO</label>
                                            <asp:UpdatePanel ID="upCtoSearch" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-xs-6">
                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtCtoSearch" runat="server" class="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <asp:LinkButton ID="lblPesquisarCTO" runat="server" class="btn btn-info btn-flat" type="button" OnClick="lblPesquisarCTO_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <asp:UpdatePanel ID="upGrid" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <asp:GridView ID="gvRedesignar" ClientIDMode="Static" runat="server" EmptyDataText="Sem Informações" AutoGenerateColumns="False" ShowHeader="False" DataKeyNames="POINT_ID,ID_SUB_PORTA,ID_OLT" OnSelectedIndexChanged="gvRedesignar_SelectedIndexChanged" GridLines="None">
                                                    <Columns>
                                                        <asp:ButtonField DataTextField="FACILITY" CommandName="select" />
                                                    </Columns>
                                                    <EmptyDataRowStyle ForeColor="Red" />
                                                </asp:GridView>
                                                <br />

                                                <asp:TextBox ID="txtRedesignar" Visible="false" runat="server" class="form-control input-sm" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                                <br />
                                                <div class="pull-right">
                                                    <asp:LinkButton ID="lbVoltar" runat="server" CssClass="btn btn-primary" OnClick="lbVoltar_Click"><i class="fa fa-arrow-left"></i> Voltar</asp:LinkButton>
                                                    <asp:LinkButton ID="lbRedesignar" runat="server" CssClass="btn btn-primary" OnClick="lbRedesignar_Click" disabled><i class="fa fa-share"></i> Redesignar</asp:LinkButton>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>


                                    
                                </blockquote>
                            </div>
                        </div>

                    </div>
                </div>


            </div>

        </div>

    </section>
    <uc1:UserControlUpdateProgress runat="server" ID="UserControlUpdateProgress" />
    
</asp:Content>
