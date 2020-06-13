<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="historicViability.aspx.cs" Inherits="CmCustomUi.reports.historicViability" %>

<%@ Register Src="~/userControls/UserControlUpdateProgress.ascx" TagPrefix="uc1" TagName="UserControlUpdateProgress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <script src="../scripts/jquery.inputmask.js"></script>
    <script src="../scripts/viabilityHistoric.js"></script>
    <asp:UpdatePanel ID="UpTitulo" runat="server">
        <ContentTemplate>
            <section class="content-header">
                <h1>Histórico Viabilidades<asp:Label ID="lblTotalTitulo" runat="server"></asp:Label></h1>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
    <section class="content">
        <div class="box box-primary">
            <div class="box-body">
                <div class='row'>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Status</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control input-sm" ClientIDMode="Static">
                                <asp:ListItem Value="0">Selecione</asp:ListItem>
                                <asp:ListItem Value="VIAVEL">VIAVEL</asp:ListItem>
                                <asp:ListItem Value="INVIAVEL">INVIAVEL</asp:ListItem>
                                <asp:ListItem Value="MANUAL">MANUAL</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Circuito</label>
                            <asp:TextBox ID="txtCircuito" runat="server" class="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Caixa</label>
                            <asp:TextBox ID="txtCaixa" runat="server" class="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Estado</label>
                            <asp:DropDownList ID="ddlEstado" runat="server" class="form-control input-sm" ClientIDMode="Static">
                                <asp:ListItem Value="0">Selecione</asp:ListItem>
                                <asp:ListItem Value="AC">Acre</asp:ListItem>
                                <asp:ListItem Value="AL">Alagoas</asp:ListItem>
                                <asp:ListItem Value="AP">Amapá</asp:ListItem>
                                <asp:ListItem Value="AM">Amazonas</asp:ListItem>
                                <asp:ListItem Value="BA">Bahia</asp:ListItem>
                                <asp:ListItem Value="CE">Ceara</asp:ListItem>
                                <asp:ListItem Value="DF">Distrito Federal</asp:ListItem>
                                <asp:ListItem Value="ES">Espirito Santo</asp:ListItem>
                                <asp:ListItem Value="GO">Goias</asp:ListItem>
                                <asp:ListItem Value="MA">Maranhão</asp:ListItem>
                                <asp:ListItem Value="MT">Mato Grosso</asp:ListItem>
                                <asp:ListItem Value="MS">Mato Grosso do Sul</asp:ListItem>
                                <asp:ListItem Value="MG">Minas Gerais</asp:ListItem>
                                <asp:ListItem Value="PA">Para</asp:ListItem>
                                <asp:ListItem Value="PB">Paraiba</asp:ListItem>
                                <asp:ListItem Value="PR">Paraná</asp:ListItem>
                                <asp:ListItem Value="PE">Pernambuco</asp:ListItem>
                                <asp:ListItem Value="PI">Piaui</asp:ListItem>
                                <asp:ListItem Value="RJ">Rio de Janeiro</asp:ListItem>
                                <asp:ListItem Value="RN">Rio Grande do Norte</asp:ListItem>
                                <asp:ListItem Value="RS">Rio Grande do Sul</asp:ListItem>
                                <asp:ListItem Value="RO">Rondonia</asp:ListItem>
                                <asp:ListItem Value="RR">Roraima</asp:ListItem>
                                <asp:ListItem Value="SC">Santa Catarina</asp:ListItem>
                                <asp:ListItem Value="SP">Sao Paulo</asp:ListItem>
                                <asp:ListItem Value="SE">Sergipe</asp:ListItem>
                                <asp:ListItem Value="TO">Tocantins</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Localidade</label>
                            <asp:TextBox ID="txtLocalidade" runat="server" class="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Bairro</label>
                            <asp:TextBox ID="txtBairro" runat="server" class="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Logradouro</label>
                            <asp:TextBox ID="txtLogradouro" runat="server" class="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
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
                            <asp:GridView ID="gvHistorico" ClientIDMode="Static" runat="server" CssClass="table table-bordered table-hover" EmptyDataText="Sem Informações" AllowPaging="True" PageSize="200" OnPageIndexChanging="gvHistorico_PageIndexChanging">
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
