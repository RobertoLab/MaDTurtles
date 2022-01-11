<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="ShowComments.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.ShowComments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <asp:Label ID="lblNoComments" runat="server" Text=""></asp:Label>
    <div id="divComments">
        <div CssClass="commentDiv">
            <form id="form1" runat="server">
                <asp:HyperLink ID="lnkUser1" runat="server"></asp:HyperLink>
                </br>
                <asp:Label ID="commentUser1" runat="server" Text=""></asp:Label>
            </form>
        </div>
        <div CssClass="commentDiv">
            <form id="form2" runat="server">
                <asp:HyperLink ID="lnkUser2" runat="server"></asp:HyperLink>
                </br>
                <asp:Label ID="commentUser2" runat="server" Text=""></asp:Label>
            </form>
        </div>
        <div CssClass="commentDiv">
            <form id="form3" runat="server">
                <asp:HyperLink ID="lnkUser3" runat="server"></asp:HyperLink>
                </br>
                <asp:Label ID="commentUser3" runat="server" Text=""></asp:Label>
            </form>
        </div>
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
    </div>

</asp:Content>
