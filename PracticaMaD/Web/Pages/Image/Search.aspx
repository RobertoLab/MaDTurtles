<%@ Page Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" 
    CodeFile="Search.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.Image.Search" 
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
    <ul id="menuLinks">

        <li>
            <asp:HyperLink ID="lnkLogin" runat="server" NavigateUrl="~/Pages/User/Authentication.aspx" meta:resourcekey="lnkLoginResource1">[lnkLogin]</asp:HyperLink>
        </li>

        <li>
            <asp:HyperLink ID="lnkLogout" runat="server" NavigateUrl="~/Pages/User/Logout.aspx" meta:resourcekey="lnkLogoutResource1">[lnkLogout]</asp:HyperLink>
        </li>

        <li>
            <asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="~/Pages/User/Register.aspx" meta:resourcekey="lnkRegisterResource1">[lnkRegister]</asp:HyperLink>
        </li>

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
    
    <form runat="server">
        <div class="field">
            <div class="label">
                <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclImageCategory" />
            </div>
            <span class="entry">
                <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True"
                    Width="100px" meta:resourcekey="ddlCategoryResource1">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory"
                    Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                <asp:Label ID="lblCategoryError" runat="server" ForeColor="Red" Style="position: relative"
                    Visible="False" meta:resourcekey="rfvCategoryError1"></asp:Label></span>
        </div>
        <div class="field">
            <div class="label">
                <asp:Localize ID="lclTags" runat="server" meta:resourcekey="lclImageTags" />
            </div>
            <span class="entry">
                <asp:TextBox TextMode="Multiline" ID="txtTags" runat="server" Width="300px"
                    Columns="16" meta:resourcekey="txtTagsResource1"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revTags" runat="server"
                    ControlToValidate="txtTags"
                    ValidationExpression="^[\w\s]+$"
                    meta:resourcekey="revTagsError1"></asp:RegularExpressionValidator></span>
            <div class="label">
                <asp:Localize ID="lclTagsExplanation" runat="server" meta:resourcekey="lclImageTagsExplanation" />
            </div>
        </div>
        <div class="button">
            <asp:Button ID="btnSearch" runat="server" OnClick="BtnSearchOnClick" meta:resourcekey="btnSearch" />
        </div>
    </form>
</asp:Content>