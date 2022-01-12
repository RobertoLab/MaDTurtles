<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="SeeUserProfile.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.User.SeeUserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <p>
        <asp:Label ID="lblUserName" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="lblUserLastName" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblUserEmail" runat="server" Text="Label"></asp:Label>
    </p>


    <asp:Image ID="img1" runat="server" Height="150px" Width="300px"></asp:Image>
    <br />
    <asp:Image ID="img2" runat="server" Height="150px" Width="300px"></asp:Image>
    <br />    
    <asp:Image ID="img3" runat="server" Height="150px" Width="300px"></asp:Image>
    <br />

</asp:Content>