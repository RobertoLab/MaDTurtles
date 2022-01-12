<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="ShowFollowed.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.User.ShowFollowed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <form runat="server">
        <p>
            <asp:Label ID="lblNoFollowed" runat="server" Visible="False"></asp:Label>
        </p>
        <br />
        <p>
            <asp:HyperLink ID="lnk1" runat="server"></asp:HyperLink>
        </p>
        <br />
        <p>
            <asp:HyperLink ID="lnk2" runat="server"></asp:HyperLink>
        </p>
        <br />
        <p>
            <asp:HyperLink ID="lnk3" runat="server"></asp:HyperLink>
        </p>
        <br />
        <p>
            <asp:HyperLink ID="lnk4" runat="server"></asp:HyperLink>
        </p>
        <br />
        <p>
            <asp:HyperLink ID="lnk5" runat="server"></asp:HyperLink>
        </p>
    </form>
    <br />
    <!-- "Previous" and "Next" links. -->
    <div class="previousNextLinks">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                Visible="False"></asp:HyperLink>
        </span><span class="nextLink">
            <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" Visible="False"></asp:HyperLink>
        </span>
    </div>
    <br />
    <br />
</asp:Content>
