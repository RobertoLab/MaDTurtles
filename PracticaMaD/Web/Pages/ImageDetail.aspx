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
        <asp:PlaceHolder ID="PlaceHolder_ImageDetails" runat="server">
        </asp:PlaceHolder>
        <asp:Button ID="btnComment" runat="server" Text="Comment" OnClick="BtnCommentClick" meta:resourceKey="btnComment"/>
        <asp:Button ID="btnShowComments" runat="server" Text="Show Comments" OnClick="BtnShowCommentsClick" meta:resourceKey="btnShowComment" />
        <asp:Button ID="btnLike" runat="server" Text="Like" OnClick="BtnLikeClick" meta:resourceKey="btnLike"/>
        <asp:Button ID="btnUnlike" runat="server" Text="Unlike" OnClick="BtnUnlikeClick" meta:resourceKey="btnUnlike" />
        <asp:Button ID="btnDeleteImage" runat="server" Text="Delete" Visible="false" Enabled="false" OnClick="BtnDeleteImageOnClick" meta:resourceKey="btnDeleteImage"/>
        <div class="field">
            <div class="label">
                <asp:Localize ID="lclTags" runat="server" Visible="false" meta:resourcekey="lclImageTags" />
            </div>
            <span class="entry">
                <asp:TextBox ID="txtTags" runat="server" Width="300px"
                    Columns="16" Visible="false" meta:resourcekey="txtTagsResource1"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revTags" runat="server"
                    ControlToValidate="txtTags"
                    ValidationExpression="^[\w\s]+$"
                    meta:resourcekey="revTagsError1"></asp:RegularExpressionValidator></span>
            <div class="label">
                <asp:Localize ID="lclTagsExplanation" runat="server" Visible="false" meta:resourcekey="lclImageTagsExplanation" /></div>
            <asp:CheckBox ID="chkAddTags" runat="server" Checked="true" Visible="false" Enabled="false"></asp:CheckBox>
            <asp:Localize ID="lclChkAddTagsExplanation" runat="server" Visible="false" meta:resourceKey="chkAddTagsResource"></asp:Localize>
            <asp:Button ID="btnModifyTags" runat="server" Text="Modify" Visible="false" Enabled="false" OnClick="BtnModifyTagsOnClick" meta:resourceKey="btnModigyTags"/>
        </div>
    </form>
</asp:Content>
