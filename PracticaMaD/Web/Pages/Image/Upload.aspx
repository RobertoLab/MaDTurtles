<%@ Page Language="C#" MasterPageFile="~/Photogram.Master" AutoEventWireup="true"
    CodeBehind="Upload.aspx.cs" Inherits="Es.Udc.DotNet.Photogram.Web.Pages.Image.Upload" 
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
    <ul id="menuLinks">

        <li>
            <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Pages/MainPage.aspx" meta:resourcekey="lnkHomeResource1">[lnkHome]</asp:HyperLink>
        </li>

        <li>
            <asp:HyperLink ID="lnkSearch" runat="server" NavigateUrl="~/Pages/SearchImg.aspx" meta:resourcekey="lnkSearch">[lnkSearch]</asp:HyperLink>
        </li>

        <li>
            <asp:HyperLink ID="lnkUploadImg" runat="server" NavigateUrl="~/Pages/UploadImg.aspx" meta:resourcekey="lnkUploadImg">[lnkUploadImg]</asp:HyperLink>
        </li>

    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="UploadForm" method="post" runat="server">

            <div class="field">
                <div class="label">
                    <asp:Localize ID="lclTitle" runat="server" meta:resourcekey="lclImageTitle" />
                </div>
                    <span class="entry">
                        <asp:TextBox ID="txtTitle" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtTitleResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                            Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblTitleError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblTitleError"></asp:Label></span>
            </div>
            <div class="field">
                <div class="label">
                    <asp:Localize ID="lclDescription" runat="server" meta:resourcekey="lclDescription" /></div>
                <span class="entry">
                        <asp:TextBox TextMode="MultiLine" ID="txtDescription" runat="server"
                            Width="300px" Columns="16"
                            meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                            Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblDescriptionError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblDescriptionError1"></asp:Label></span>
            </div>
            
            <div class="field">
                <div class="label">
                    <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclImageCategory" /></div>
                <span class="entry">
                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True"
                        Width="100px" meta:resourcekey="ddlCategoryResource1"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory"
                        Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblCategoryError" runat="server" ForeColor="Red" Style="position: relative"
                        Visible="False" meta:resourcekey="rfvCategoryError1"></asp:Label></span>
            </div>
            <div class="field">
                <div class="label">
                    <asp:Localize ID="lclTags" runat="server" meta:resourcekey="lclImageTags" /></div>
                <span class="entry">
                        <asp:TextBox TextMode="Multiline" ID="txtTags" runat="server" Width="300px"
                            Columns="16" meta:resourcekey="txtTagsResource1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revTags" runat="server" 
                            ControlToValidate="txtTags"
                            ValidationExpression="^[\w\s]+$"
                            meta:resourcekey="revTagsError1"></asp:RegularExpressionValidator></span>
                <div class="label">
                    <asp:Localize ID="lclTagsExplanation" runat="server" meta:resourcekey="lclImageTagsExplanation" /></div>
            </div>
            <div class="field">
                <div class="label">
                    <asp:Localize ID="lclLoad" runat="server" meta:resourcekey="lclImageLoad" /></div>
                <span class="entry">
                    <input id="inImageFile" type="file" runat="server"/>
                </span>
            </div>
            <div class="field">
                <div class="label">
                    <asp:Localize ID="lclDiaph" runat="server" meta:resourcekey="lclDiaph" /></div>
                <span class="entry">
                        <asp:TextBox TextMode="Number" ID="txtDiaph" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtDiaphResource1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revDiaph" runat="server" 
                            ControlToValidate="txtDiaph"
                            ValidationExpression="^[1-9]\d*(,\d+)?$"
                            meta:resourcekey="revDiaphError1"></asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="rvDiaph" runat="server"   
                            ControlToValidate="txtDiaph"
                            MaximumValue="99999999" MinimumValue="0" Type="Integer"></asp:RangeValidator></span>
            </div>
            <div class="field">
                <div class="label">
                    <asp:Localize ID="lclExposition" runat="server" meta:resourcekey="lclExposition" /></div>
                <span class="entry">
                        <asp:TextBox TextMode="Number" ID="txtExposition" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtExpositionResource1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revExposition" runat="server" 
                            ControlToValidate="txtExposition"
                            ValidationExpression="^[1-9]\d*(,\d+)?$"
                            meta:resourcekey="revExpositionError1"></asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="rvExposition" runat="server"   
                            ControlToValidate="txtExposition"
                            MaximumValue="99999999" MinimumValue="0" Type="Integer"></asp:RangeValidator></span>
            </div>
            
            <div class="field">
                <div class="label">
                    <asp:Localize ID="lclIso" runat="server" meta:resourcekey="lclIso" /></div>
                <span class="entry">
                        <asp:TextBox TextMode="Number" ID="txtIso" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtIsoResource1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revIso" runat="server" 
                            ControlToValidate="txtIso"
                            ValidationExpression="^[1-9]\d*$"
                            meta:resourcekey="revIsoError1"></asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="rvIso" runat="server"   
                            ControlToValidate="txtIso"
                            MaximumValue="99999999" MinimumValue="0" Type="Integer"></asp:RangeValidator></span>
            </div>
            <div class="field">
                <div class="label">
                    <asp:Localize ID="lclWhiteBalance" runat="server" meta:resourcekey="lclWhiteBalance" /></div>
                <span class="entry">
                        <asp:TextBox TextMode="Number" ID="txtWhiteBalance" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtWhiteBalanceResource1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revWhiteBalance" runat="server" 
                            ControlToValidate="txtWhiteBalance"
                            ValidationExpression="^[1-9]\d*(,\d+)?$"
                            meta:resourcekey="revWhiteBalanceError1"></asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="rvWhiteBalance" runat="server"   
                            ControlToValidate="txtWhiteBalance"
                            MaximumValue="99999999" MinimumValue="0" Type="Integer"></asp:RangeValidator></span>
            </div>
            <div class="button">
                <asp:Button ID="btnUpload" runat="server" OnClick="BtnUploadClick" meta:resourcekey="btnUpload" />
            </div>
            <div class="frmResult">
                <asp:Panel ID="frmConfirmation" Visible="False" Runat="server">
                    <asp:Label id="lblUploadOk" Runat="server" 
                        ForeColor="Green" Style="position: relative"
                        Visible="False" meta:resourcekey="lblUploadOk"></asp:Label>
                    <asp:Label id="lblUploadFailed" Runat="server" 
                        ForeColor="Red" Style="position: relative"
                        Visible="False" meta:resourcekey="lblUploadFailed"></asp:Label>
                </asp:Panel>
            </div>
        </form>
    </div>
</asp:Content>
