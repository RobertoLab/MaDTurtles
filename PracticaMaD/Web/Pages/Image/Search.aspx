<%@ Page Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" 
    CodeFile="Search.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.Image.Search" 
    meta:resourcekey="Page" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    
    <form runat="server" style="width:25%;text-align:center">
        <div class="field">
            <div class="label">
                <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclImageCategory" />
            </div>
            <span class="entry">
                <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True"
                    Width="100px" meta:resourcekey="ddlCategoryResource1">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory"
                    Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                <asp:Label ID="lblCategoryError" runat="server" ForeColor="Red" Style="position: relative"
                    Visible="False" meta:resourcekey="rfvCategoryError1"></asp:Label></span>
        </div>
        <div class="field">
            <div class="label">
                <asp:Localize ID="lclKeywords" runat="server" meta:resourcekey="lclImageKeywords" />
            </div>
            <span class="entry">
                <asp:TextBox ID="txtKeywords" runat="server" Width="300px"
                    Columns="16" meta:resourcekey="txtKeywordsResource1"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revKeywords" runat="server"
                    ControlToValidate="txtKeywords"
                    ValidationExpression="^[\w\s]+$"
                    meta:resourcekey="revKeywordsError1"></asp:RegularExpressionValidator></span>
            <div class="label">
                <asp:Localize ID="lclKeywordsExplanation" runat="server" meta:resourcekey="lclImageKeywordsExplanation" />
            </div>
        </div>
        <div class="button">
            <asp:Button ID="btnSearch" runat="server" OnClick="BtnSearchOnClick" meta:resourcekey="btnSearch" />
        </div>
    </form>
</asp:Content>