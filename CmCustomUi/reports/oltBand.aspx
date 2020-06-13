<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="oltBand.aspx.cs" Inherits="CmCustomUi.reports.oltBand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <section class="content-header">
        <h1>Relatório Banda OLT</h1>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class='row'>

                    <div class="col-sm-6">
                        <label>OLT</label>
                        <asp:TextBox ID="txtOlt" runat="server" class="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
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


                    <br />
                    <br />
                    <br />
                    <hr />
                </div>
                <asp:UpdatePanel ID="upGridView" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvOLT" ClientIDMode="Static" runat="server"
                            AutoGenerateColumns="False" class="table table-bordered table-hover" CellSpacing="-1"
                            GridLines="None" EmptyDataText="Sem Informações">
                            <Columns>
                                <asp:BoundField DataField="OLT" HeaderText="OLT"></asp:BoundField>
                                <asp:BoundField DataField="BANDA_MB" HeaderText="Banda MB" />
                                <asp:BoundField DataField="BANDA_UPLINK" HeaderText="Banda uplink" />
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Red" />
                            <HeaderStyle CssClass="thead" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1000">
        <ProgressTemplate>
            <div id="progressBackgroundFilterWithPopUP">
            </div>
            <img alt="Loading" src="../Images/loading.gif" style="position: absolute; top: 30%; right: 43%; z-index: 1000999; border: transparent; padding: 10px;" />
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

