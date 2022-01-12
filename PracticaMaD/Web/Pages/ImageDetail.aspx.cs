using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Model.InteractionService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.Photogram.Web.HTTP.Actions;
using Es.Udc.DotNet.ModelUtil.IoC;

namespace Es.Udc.DotNet.Photogram.Web.Pages
{
    public partial class ImageDetail : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long imgID = Convert.ToInt32(Request.Params.Get("imgID"));
            long userID = 0;
            if (SessionManager.GetUserSession(Context) != null)
            {
                UserSession userSession = SessionManager.GetUserSession(Context);
                userID = userSession.UserProfileId;
            }
            
            /* Get the Services */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageService imageService = iocManager.Resolve<IImageService>();
            IUserService userService = iocManager.Resolve<IUserService>();
            IInteractionService interactionService = iocManager.Resolve<IInteractionService>();

            /* Get the Image*/
            ImageInfo image = imageService.SearchImageEager(imgID);

            /* Get the userName of the uploader */

            UserProfileDetails user = userService.FindUserProfileDetails(image.userId);

            /* Get if the image was given a like*/
            if (userID != 0)
            {
                bool liked = interactionService.AlreadyLiked(userID, imgID);
            }
            /* Update the info */

            this.imgTitle.Text = image.title;
            this.imgTitle.Style["font-style"] = "bold";

            this.imgAuthor.Text = user.userName;
            this.imgAuthor.Style["font-style"] = "italic";

            this.Image1.ImageUrl = "data:image;base64," + image.imgBase64;
            this.Image1.Width = Unit.Pixel(500);
            this.Image1.Height = Unit.Pixel(500);

            this.imgLikes.Text = Convert.ToString(interactionService.GetImageLikes(imgID));

            this.btnLike.Visible = true;
            this.btnUnlike.Visible = false;
            
            if (SessionManager.IsUserAuthenticated(Context)
                && ActionsManager.IsPropietary(userID, imgID))
            {
                // Config delete button
                btnDeleteImage.Enabled = true;
                btnDeleteImage.Visible = true;
                // Config update tags button
                chkAddTags.Enabled = true;
                chkAddTags.Visible = true;
                txtTags.Visible = true;
                lclTags.Visible = true;
                lclTagsExplanation.Visible = true;
                lclChkAddTagsExplanation.Visible = true;
                btnModifyTags.Enabled = true;
                btnModifyTags.Visible = true;
                List<string> tagsList = image.tags;
                string tags = String.Join(" ", tagsList);
                btnModifyTags.CommandArgument = tags;
            }

            BuildImageDetails(image);
        }

        private void BuildImageDetails(ImageInfo image)
        {
            Panel panelDetailsColumn = new Panel();
            panelDetailsColumn.ID = "panelDetailsColumn";
            panelDetailsColumn.CssClass = "w3-col";
            Panel panelDescriptionRow = new Panel();
            panelDescriptionRow.ID = "panelDescriptionRow";
            panelDescriptionRow.CssClass = "w3-row";
            Panel panelCategoryRow = new Panel();
            panelCategoryRow.ID = "panelCategoryRow";
            panelCategoryRow.CssClass = "w3-row";
            Panel panelTagsRow = new Panel();
            panelTagsRow.ID = "panelTagsRow";
            panelTagsRow.CssClass = "w3-row";

            Label lblDescriptionInt = new Label();
            lblDescriptionInt.ID = "lblDescriptionInt";
            lblDescriptionInt.Text = GetLocalResourceObject("lblDescriptionInt").ToString();
            lblDescriptionInt.Style["font-style"] = "italic";
            Label lblDescription = new Label();
            lblDescription.ID = "lblDescription";
            lblDescription.Text = image.description;

            Label lblCategoryInt = new Label();
            lblCategoryInt.ID = "lblCategoryInt";
            lblCategoryInt.Text = GetLocalResourceObject("lblCategoryInt").ToString() + " ";
            lblCategoryInt.Style["font-style"] = "italic";
            Label lblCategory = new Label();
            lblCategory.ID = "lblCategory";
            lblCategory.Text = image.category;

            Label lblTagsInt = new Label();
            lblTagsInt.ID = "lblTagsInt";
            lblTagsInt.Text = GetLocalResourceObject("lblTagsInt").ToString() + " ";
            lblTagsInt.Style["font-style"] = "italic";
            Label lblTags = new Label();
            lblTags.ID = "lblTags";
            lblTags.Text = "[" + String.Join("] [", image.tags) + "]";

            panelDescriptionRow.Controls.Add(lblDescriptionInt);
            panelDescriptionRow.Controls.Add(lblDescription);
            panelCategoryRow.Controls.Add(lblCategoryInt);
            panelCategoryRow.Controls.Add(lblCategory);
            panelTagsRow.Controls.Add(lblTagsInt);
            panelTagsRow.Controls.Add(lblTags);

            panelDetailsColumn.Controls.Add(panelDescriptionRow);
            panelDetailsColumn.Controls.Add(panelCategoryRow);
            panelDetailsColumn.Controls.Add(panelTagsRow);

            if (image.metadata.Count >0)
            {
                Panel panelMetadataRow = new Panel();
                panelMetadataRow.ID = "panelMetadataRow";
                panelMetadataRow.CssClass = "w3-row";

                Label lblMetadataHeader = new Label();
                lblMetadataHeader.ID = "lblMetadataHeader";
                lblMetadataHeader.Text = 
                    GetLocalResourceObject("lblMetadataHeader").ToString() + "\t";
                lblMetadataHeader.Style["font-weight"] = "bold";

                panelDetailsColumn.Controls.Add(lblMetadataHeader);
                panelMetadataRow.Controls.Add(lblMetadataHeader);

                int metadataIndex = 1;
                foreach (Model.Exif metadata in image.metadata)
                {
                    Panel panelMetadataContentRow = new Panel();
                    panelMetadataContentRow.ID = "panelMetadataContentRow" + metadataIndex.ToString();
                    panelMetadataContentRow.CssClass = "w3-row";

                    Label lblMetadata = new Label();
                    lblMetadata.ID = "lblMetadata" + metadataIndex.ToString();
                    lblMetadata.Text = 
                        GetLocalResourceObject(ParseMetadataName(metadata.infoType)).ToString()
                        + " : " + metadata.value;
                    lblMetadataHeader.Style["font-style"] = "italic";

                    panelMetadataContentRow.Controls.Add(lblMetadata);
                    panelMetadataRow.Controls.Add(panelMetadataContentRow);
                    metadataIndex++;
                }
                panelDetailsColumn.Controls.Add(panelMetadataRow);
            }
            PlaceHolder_ImageDetails.Controls.Add(panelDetailsColumn);
        }

        private string ParseMetadataName(string metadataType)
        {
            switch (metadataType)
            {
                case "f": return "Aperture";
                case "iso": return "ISO";
                case "t": return "Exposure";
                case "wb": return "WhiteBalance";
                default: return "Unknown";
            }
        }
        protected void BtnLikeClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                /* Get data. */
                long userID = 0;
                if (SessionManager.GetUserSession(Context) != null)
                {
                    UserSession userSession = SessionManager.GetUserSession(Context);
                    userID = userSession.UserProfileId;
                }
                long imgID = Convert.ToInt32(Request.Params.Get("imgID"));

                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IInteractionService interactionService = iocManager.Resolve<IInteractionService>();
                if (userID != 0)
                {
                    /* Do action. */
                    interactionService.LikeImage(userID, imgID);

                    /* Change display */
                    this.btnLike.Visible = false;
                    this.btnUnlike.Visible = true;
                }
                else
                {
                    String url = String.Format("~/Pages/User/Authentication.aspx");
                    Response.Redirect(Response.ApplyAppPathModifier(url));
                }

            }
        }

        protected void BtnUnlikeClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                /* Get data. */
                UserSession userSession = SessionManager.GetUserSession(Context);
                long userID = userSession.UserProfileId;
                long imgID = Convert.ToInt32(Request.Params.Get("imgID"));

                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IInteractionService interactionService = iocManager.Resolve<IInteractionService>();
                /* Do action. */

                interactionService.Unlike(userID, imgID);

                /* Change display */

                this.btnLike.Visible = true;
                this.btnUnlike.Visible = false;

            }
        }

        protected void BtnCommentClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                long userId = 0;
                if (SessionManager.GetUserSession(Context) != null)
                {
                    UserSession userSession = SessionManager.GetUserSession(Context);
                    userId = userSession.UserProfileId;
                }
                long imgID = Convert.ToInt32(Request.Params.Get("imgID"));
                if (userId != 0)
                {
                    String url = String.Format("./Comment.aspx?imgID={0}", imgID);
                    Response.Redirect(Response.ApplyAppPathModifier(url));
                }
                else
                {
                    String url = String.Format("~/Pages/User/Authentication.aspx");
                    Response.Redirect(Response.ApplyAppPathModifier(url));
                }
                


            }
        }

        protected void BtnShowCommentsClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                long imgID = Convert.ToInt32(Request.Params.Get("imgID"));
                String url = String.Format("./ShowComments.aspx?imgID={0}", imgID);
                Response.Redirect(Response.ApplyAppPathModifier(url));

            }
        }

        protected void BtnDeleteImageOnClick(object sender, EventArgs e)
        {
            long imgId = Convert.ToInt32(Request.Params.Get("imgID"));
            long userId = SessionManager.GetUserSession(Context).UserProfileId;
            ActionsManager.DeleteImage(imgId, userId);
            Response.Redirect("~/Pages/MainPage.aspx");
        }

        protected void BtnModifyTagsOnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string oldTags = btn.CommandArgument.ToString();
            string newTags = txtTags.Text;
            long imgId = Convert.ToInt32(Request.Params.Get("imgID"));
            long userId = SessionManager.GetUserSession(Context).UserProfileId;
            string finalTags = "";
            if (chkAddTags.Checked)
            {
                finalTags = oldTags + " " + newTags;
            } else
            {
                finalTags = newTags;
            }
            ActionsManager.UpdateImageTags(imgId, finalTags);
        }
    }
}