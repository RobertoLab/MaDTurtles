<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="SeeUserProfile.aspx.cs" Inherits="Web.Pages.User.SeeUserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <p>
        <asp:Label ID="lblUserName" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:Button ID="btnSeguidos" runat="server" Text="Seguidos" onClick="btnSeguidosClick"></asp:Button>
        <asp:Button ID="btnSeguidores" runat="server" Text="Seguidores" onClick="btnSeguidoresClick"></asp:Button>
    </p>

    <picture>
        <asp:Image ID="img1" runat="server"></asp:Image>
    </picture>
    <picture>
        <asp:Image ID="img2" runat="server"></asp:Image>
    </picture>
    <picture>
        <asp:Image ID="img3" runat="server"></asp:Image>
    </picture>
    <p>
        <asp:Button ID="btnVerMas" runat="server" Text="Ver Mas" onClick="btnVerMasClick" />
    </p>
