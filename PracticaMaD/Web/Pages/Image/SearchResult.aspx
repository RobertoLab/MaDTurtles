<%@ Page Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true"
    CodeBehind="SearchResult.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.Image.SearchResult" 
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
<asp:Content ID="contentBody" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form runat="server">
        <asp:PlaceHolder ID="PlaceHolder_ImageCards" runat="server">
        </asp:PlaceHolder>
    </form>
    <div class="previousNextLinks">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                Visible="False"></asp:HyperLink>
        </span><span class="nextLink">
            <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" Visible="False"></asp:HyperLink>
        </span>
    </div>
    <div>
        <asp:Label ID="lblFirstImageOk" runat="server"
            ForeColor="Green" Style="position: relative"
            Visible="False" Text="Ok1"></asp:Label>
        <asp:Label ID="lblSecondImageOk" runat="server"
            ForeColor="Green" Style="position: relative"
            Visible="False" Text="Ok2"></asp:Label>
        <asp:Label ID="lblThirdImageOk" runat="server"
            ForeColor="Green" Style="position: relative"
            Visible="False" Text="Ok3"></asp:Label>
    </div>
</asp:Content>