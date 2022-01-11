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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="imgCard1" class="row">
        <div class="column">
            <asp:Image ID="imgImage1" runat="server" AlternateText="1" Visible="false"></asp:Image>
        </div>
        <div class="column">
            <div class="imgCardHeader">
                <asp:HyperLink ID="lnkDetails1" runat="server" Visible="false" NavigateUrl="~/Pages/ImageDetails.aspx" meta:resourcekey="lnkDetailsResource1">[lnkDetails]</asp:HyperLink>
                <asp:HyperLink ID="lnkAuthor1" runat="server" Visible="false" NavigateUrl="~/Pages/Profile.aspx" meta:resourcekey="lnkAuthorResource1">[lnkAuthor]</asp:HyperLink>
            </div>
            <div class="ImgCardBody">
                <div class="imgCardComment">
                    <asp:HyperLink ID="lnkComments1" runat="server" Visible="false" NavigateUrl="~/Pages/ShowComments.aspx" meta:resourcekey="lnkCommentsResource1">[lnkComments]</asp:HyperLink>
                    <asp:HyperLink ID="lnkNewComment1" runat="server" Visible="false" NavigateUrl="~/Pages/NewComment.aspx" meta:resourcekey="lnkNewCommentResource1">[lnkNewComment]</asp:HyperLink>
                </div>
                <div class="imgCardLike">
                    <asp:HyperLink ID="lnkLike1" runat="server" meta:resourcekey="lnkNewCommentResource1" Visible="false">[lnkLike]</asp:HyperLink>
                </div>
            </div>
        </div>
    </div>
    <div id="imgCard2" class="row">
        <div class="column">
            <asp:Image ID="imgImage2" runat="server" AlternateText="1" Visible="false"></asp:Image>
        </div>
        <div class="column">
            <div class="imgCardHeader">
                <asp:HyperLink ID="lnkDetails2" runat="server" Visible="false" NavigateUrl="~/Pages/ImageDetails.aspx" meta:resourcekey="lnkDetailsResource1">[lnkDetails]</asp:HyperLink>
                <asp:HyperLink ID="lnkAuthor2" runat="server" Visible="false" NavigateUrl="~/Pages/Profile.aspx" meta:resourcekey="lnkAuthorResource1">[lnkAuthor]</asp:HyperLink>
            </div>
            <div class="ImgCardBody">
                <div class="imgCardComment">
                    <asp:HyperLink ID="lnkComments2" runat="server" Visible="false" NavigateUrl="~/Pages/ShowComments.aspx" meta:resourcekey="lnkCommentsResource1">[lnkComments]</asp:HyperLink>
                    <asp:HyperLink ID="lnkNewComment2" runat="server" Visible="false" NavigateUrl="~/Pages/NewComment.aspx" meta:resourcekey="lnkNewCommentResource1">[lnkNewComment]</asp:HyperLink>
                </div>
                <div class="imgCardLike">
                    <asp:HyperLink ID="lnkLike2" runat="server" Visible="false" meta:resourcekey="lnkNewCommentResource1">[lnkLike]</asp:HyperLink>
                </div>
            </div>
        </div>
    </div>
    <div id="imgCard3" class="row">
        <div class="column">
            <asp:Image ID="imgImage3" runat="server" AlternateText="1" Visible="false"></asp:Image>
        </div>
        <div class="column">
            <div class="imgCardHeader">
                <asp:HyperLink ID="lnkDetails3" runat="server" Visible="false" NavigateUrl="~/Pages/ImageDetails.aspx" meta:resourcekey="lnkDetailsResource1">[lnkDetails]</asp:HyperLink>
                <asp:HyperLink ID="lnkAuthor3" runat="server" Visible="false" NavigateUrl="~/Pages/Profile.aspx" meta:resourcekey="lnkAuthorResource1">[lnkAuthor]</asp:HyperLink>
            </div>
            <div class="ImgCardBody">
                <div class="imgCardComment">
                    <asp:HyperLink ID="lnkComments3" runat="server" Visible="false" NavigateUrl="~/Pages/ShowComments.aspx" meta:resourcekey="lnkCommentsResource1">[lnkComments]</asp:HyperLink>
                    <asp:HyperLink ID="lnkNewComment3" runat="server" Visible="false" NavigateUrl="~/Pages/NewComment.aspx" meta:resourcekey="lnkNewCommentResource1">[lnkNewComment]</asp:HyperLink>
                </div>
                <div class="imgCardLike">
                    <asp:HyperLink ID="lnkLike3" runat="server" Visible="false" meta:resourcekey="lnkNewCommentResource1">[lnkLike]</asp:HyperLink>
                </div>
            </div>
        </div>
    </div>
    <div class="previousNextLinks">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                Visible="False"></asp:HyperLink>
        </span><span class="nextLink">
            <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" Visible="False"></asp:HyperLink>
        </span>
    </div>
</asp:Content>