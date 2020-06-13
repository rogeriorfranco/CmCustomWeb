<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlAlertConfirmBootstrap.ascx.cs" Inherits="CmCustomUi.userControls.UserControlAlertConfirmBootstrap" %>
<asp:UpdatePanel ID="UpSave" runat="server">
    <ContentTemplate>
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">Connect Master</h4>
                    </div>
                    <div class="modal-body">
                        <h4>
                            <label id="modalId"></label>
                        </h4>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnConfirmaOk" runat="server" class="btn btn-success" Text="Confirmar" ClientIDMode="Static" OnClick="btnConfirmaOk_Click" />
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
