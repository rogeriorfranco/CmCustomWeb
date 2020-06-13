<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="facilityGpon.aspx.cs" Inherits="CmCustomUi.reports.facilityGpon" %>

<%@ Register Src="~/userControls/UserControlUpdateProgress.ascx" TagPrefix="uc1" TagName="UserControlUpdateProgress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:UpdatePanel ID="UpTitulo" runat="server">
        <ContentTemplate>
            <section class="content-header">
                <h1>Relatório Facilidades GPON<asp:Label ID="lblTotalTitulo" runat="server"></asp:Label></h1>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>


    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class='row'>

                    <div class="col-sm-2">
                        <label>Circuito</label>
                        <asp:TextBox ID="txtCircuito" runat="server" class="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                    </div>

                    <div class="col-sm-2">
                        <label>CTO</label>
                        <asp:TextBox ID="txtCto" runat="server" class="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                    </div>

                    <div class="col-sm-2">
                        <label>OLT</label>
                        <asp:TextBox ID="txtOlt" runat="server" class="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                    </div>

                    <div class="col-sm-2">
                        <label>Status</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control input-sm" ClientIDMode="Static">
                            <asp:ListItem Value="0">Selecione</asp:ListItem>
                            <asp:ListItem>AUDITORIA</asp:ListItem>
                            <asp:ListItem>DEFEITO</asp:ListItem>
                            <asp:ListItem>DESIGNADO</asp:ListItem>
                            <asp:ListItem>OCUPADO</asp:ListItem>
                            <asp:ListItem>RESERVADO</asp:ListItem>
                            <asp:ListItem>VAGO</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-sm-2" style="padding-top: 20px">
                        <asp:LinkButton ID="lbExportar" runat="server" CssClass="btn btn-primary" ClientIDMode="Static" OnClick="lbExportar_Click"><i class="fa fa-download"></i> Exportar</asp:LinkButton>
                    </div>

                    <br />
                    <br />
                    <br />
                    <hr />
                </div>

            </div>
        </div>
    </section>
    <uc1:UserControlUpdateProgress runat="server" ID="UserControlUpdateProgress" />
</asp:Content>
