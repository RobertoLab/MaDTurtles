<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="ShowComments.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.ShowComments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <asp:Label ID="lblNoComments" runat="server" Text="No hay comentarios"></asp:Label>
    <div id="divComments">
        <div CssClass="commentDiv">
            <asp:HyperLink ID="lnkUser1" runat="server"></asp:HyperLink>
            </br>
            <asp:Label ID="commentUser1" runat="server" Text=""></asp:Label>
        </div>
        <div CssClass="commentDiv">
            <asp:HyperLink ID="lnkUser2" runat="server"></asp:HyperLink>
            </br>
            <asp:Label ID="commentUser2" runat="server" Text=""></asp:Label>
        </div>
        <div CssClass="commentDiv">
            <asp:HyperLink ID="lnkUser3" runat="server"></asp:HyperLink>
            </br>
            <asp:Label ID="commentUser3" runat="server" Text=""></asp:Label>
        </div>
        <br />
        <!-- "Previous" and "Next" links. -->
        <div class="previousNextLinks">
            <span class="previousLink">
                <asp:HyperLink ID="lnkPrevious" Text="Previous" runat="server"
                    Visible="False"></asp:HyperLink>
            </span><span class="nextLink">
                <asp:HyperLink ID="lnkNext" Text="Next" runat="server" Visible="False"></asp:HyperLink>
            </span>
        </div>
    </div>

</asp:Content>
