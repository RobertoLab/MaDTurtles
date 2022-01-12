<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="SeeUserProfile.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.User.SeeUserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <p>
        <asp:Label ID="lblUserName" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="lblUserLastName" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblUserEmail" runat="server" Text="Label"></asp:Label>
    </p>
    <form id="profileBtnsForm" runat="server">
        <asp:Button ID="btnFollowed" runat="server" Text="Followed"  OnClick="BtnFollowedClick"/>
        <br />
        <asp:Button ID="btnFollow" runat="server" Text="Button"  OnClick="BtnFollowClick"/>
        <br />
        <asp:Image ID="img1" runat="server" Height="150px" Width="300px"></asp:Image> 
        <asp:Button ID="btnImg1" runat="server" Text="Foto1" OnClick="BtnImg1Click" />
        <br />
        <asp:Image ID="img2" runat="server" Height="150px" Width="300px"></asp:Image>
        <asp:Button ID="btnImg2" runat="server" Text="Foto2" OnClick="BtnImg2Click" />
        <br />    
        <asp:Image ID="img3" runat="server" Height="150px" Width="300px"></asp:Image>
        <asp:Button ID="btnImg3" runat="server" Text="Foto3" OnClick="BtnImg3Click" />
        <br />
    </form>
</asp:Content>