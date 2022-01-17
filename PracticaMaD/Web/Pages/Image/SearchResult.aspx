<%@ Page Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true"
    CodeBehind="SearchResult.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.Image.SearchResult" 
    meta:resourcekey="Page" %>

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
</asp:Content>