<%@ Page Title="" Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true" CodeBehind="Comment.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.Comment" %>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
        <p>
            <asp:Label ID="lblComment" runat="server" Text="Comentario"></asp:Label>
        </p>
        <div id="form">
            <form id="CommentForm" method="post" runat="server">

                <div class="field">
                    <span class="entry">
                                    <asp:TextBox ID="txtComment" runat="server" Width="322px" Columns="16" Height="144px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment"
                                        Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                                        meta:resourcekey="rfvComment"></asp:RequiredFieldValidator>

                    </span>
                </div>
                <div class="button">
                    <asp:Button ID="btnComment" runat="server" Text="Comment" OnClick="BtnCommentClick" />
                </div>
        </form>
        </div>
</asp:Content>
