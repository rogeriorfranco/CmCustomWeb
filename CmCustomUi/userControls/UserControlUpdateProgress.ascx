<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlUpdateProgress.ascx.cs" Inherits="CmCustomUi.UserControls.UserControlUpdateProgress" %>
<asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1000">
    <ProgressTemplate>
        <div id="progressBackgroundFilterWithPopUP">
        </div>
        <img alt="Loading" src="../images/loading.gif" style="position: absolute; top: 30%; right: 43%; z-index: 1000999; border: transparent; padding: 10px;" />
    </ProgressTemplate>
</asp:UpdateProgress>
