<%@ Page Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="ImageDetail.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.ImageDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <p>
        <asp:Label ID="imgTitle" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:Label ID="imgAuthor" runat="server" Text="Label"></asp:Label>
    </p>

    <asp:Image ID="Image1" runat="server" />
    <p>
        <asp:Label ID="imgLikes" runat="server" Text="Label"></asp:Label>
    </p>
    <br/>
    <form id="imgBtnsForm" runat="server">
        <asp:Button ID="btnComment" runat="server" Text="Comment" OnClick="BtnCommentClick" />
        <asp:Button ID="btnShowComments" runat="server" Text="Show Comments" OnClick="BtnShowCommentsClick" />
        <asp:Button ID="btnLike" runat="server" Text="Like" OnClick="BtnLikeClick" />
        <asp:Button ID="btnUnlike" runat="server" Text="Unlike" OnClick="BtnUnlikeClick" />
    </form>
</asp:Content>
