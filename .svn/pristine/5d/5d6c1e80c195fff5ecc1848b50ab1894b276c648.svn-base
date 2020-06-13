<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="quantityNetwork.aspx.cs" Inherits="CmCustomUi.reports.quantityNetwork" %>
<%@ Register Src="~/userControls/UserControlUpdateProgress.ascx" TagPrefix="uc1" TagName="UserControlUpdateProgress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:UpdatePanel ID="UpTitulo" runat="server">
        <ContentTemplate>
            <section class="content-header">
                <h1>Relatório Quantitativo de Rede Óptica<asp:Label ID="lblTotalTitulo" runat="server"></asp:Label></h1>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>


    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class='row'>

                    <div class="col-sm-2">
                        <label>Tecnologia</label>
                        <asp:DropDownList ID="ddlTecnologia" runat="server" class="form-control input-sm" AppendDataBoundItems="true">
                            <asp:ListItem Text="" Value="" />
                        </asp:DropDownList>
                    </div>

                    <div class="col-sm-2" style="padding-top: 20px">
                        <asp:UpdatePanel ID="upPesquisar" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="lbPesquisar" runat="server" CssClass="btn btn-primary" OnClick="lbPesquisar_Click"><i class="fa fa-search"></i> Pesquisar</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-2" style="padding-top: 20px">
                        <asp:LinkButton ID="lbExportar" runat="server" CssClass="btn btn-primary" ClientIDMode="Static" OnClick="lbExportar_Click"><i class="fa fa-download"></i> Exportar</asp:LinkButton>
                    </div>

                    <br />
                    <br />
                    <br />
                    <hr />
                </div>
                <asp:UpdatePanel ID="upGridView" runat="server">
                    <ContentTemplate>
                        <div class="table-responsive">
                            <asp:GridView ID="gvGpon" ClientIDMode="Static" runat="server" class="table table-bordered table-hover" CellSpacing="-1"
                                GridLines="None" EmptyDataText="Sem Informações" AutoGenerateColumns="False" PageSize="200">
                                <Columns>
                                    <asp:BoundField DataField="LOCALIDADE" HeaderText="Localidade" />
                                    <asp:BoundField DataField="TECNOLOGIA" HeaderText="Tecnologia" />
                                    <asp:BoundField DataField="OLT" HeaderText="OLT" />
                                    <asp:BoundField DataField="CTO" HeaderText="CTO" />
                                    <asp:BoundField DataField="TOTAL_PORTAS" HeaderText="Total de Portas" />
                                    <asp:BoundField DataField="PORTAS_DISPONIVEIS" HeaderText="Portas disponíveis" />
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
