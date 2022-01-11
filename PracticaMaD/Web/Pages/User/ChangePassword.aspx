<%@ Page Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true"
    Codebehind="ChangePassword.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.User.ChangePassword"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
    <ul id="menuLinks">

        <li>
            <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Pages/MainPage.aspx" meta:resourcekey="lnkHomeResource1">[lnkHome]</asp:HyperLink>
        </li>

        <li>
            <asp:HyperLink ID="lnkSearch" runat="server" NavigateUrl="~/Pages/Image/Search.aspx" meta:resourcekey="lnkSearch">[lnkSearch]</asp:HyperLink>
        </li>

        <li>
            <asp:HyperLink ID="lnkUploadImg" runat="server" NavigateUrl="~/Pages/Image/Upload.aspx" meta:resourcekey="lnkUploadImg">[lnkUploadImg]</asp:HyperLink>
        </li>

    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="ChangePasswordForm" method="post" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclOldPassword" runat="server" meta:resourcekey="lclOldPassword" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtOldPassword" TextMode="Password" runat="server" Width="100" Columns="16">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txtOldPassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                        <asp:Label ID="lblOldPasswordError" runat="server" ForeColor="Red" Visible="False"
                            meta:resourcekey="lblOldPasswordError">
                        </asp:Label>
                    </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclNewPassword" runat="server" meta:resourcekey="lclNewPassword" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtNewPassword" runat="server" Width="100" Columns="16">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                        <asp:CompareValidator ID="cvCreateNewPassword" runat="server" ControlToCompare="txtOldPassword"
                            ControlToValidate="txtNewPassword" Operator="NotEqual" meta:resourcekey="cvCreateNewPassword"></asp:CompareValidator>
                    </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclRetypePassword" runat="server" meta:resourcekey="lclRetypePassword" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtRetypePassword" runat="server" Width="100"
                            Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                        <asp:CompareValidator ID="cvPasswordCheck" runat="server" ControlToCompare="txtNewPassword"
                            ControlToValidate="txtRetypePassword" meta:resourcekey="cvPasswordCheck"></asp:CompareValidator>
                    </span>
            </div>
            <div class="button">
                <asp:Button ID="btnChangePassword" runat="server" OnClick="BtnChangePasswordClick"
                    meta:resourcekey="btnChangePassword" />
            </div>
        </form>
    </div>
</asp:Content>
