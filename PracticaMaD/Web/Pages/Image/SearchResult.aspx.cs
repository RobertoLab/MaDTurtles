using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.Photogram.Web.HTTP.Actions;
using Es.Udc.DotNet.Photogram.Model.Dtos;

namespace Es.Udc.DotNet.Photogram.Web.Pages.Image
{
    public partial class SearchResult : SpecificCulturePage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            int startIndex;
            int count;
            try
            {
                startIndex = int.Parse(Request.QueryString["startIndex"]);
                count = int.Parse(Request.QueryString["count"]);
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
                count = 3;
            }

            string tag = Request.QueryString["tag"];
            string keywordsSeparated = "";
            string category = "";
            Block<ImageBasicInfo> block;
            if (tag == "N/A")
            {
                keywordsSeparated = Request.QueryString["keywords"];
                category = Request.QueryString["category"];
                string keywords = "";
                if (!string.IsNullOrEmpty(keywordsSeparated))
                    keywords = keywordsSeparated.Replace('|', ' ');
                block = ActionsManager.SearchImageBasic(keywords, category, startIndex, count);
            }
            else
            {
                block = ActionsManager.SearchImageBasic(tag, startIndex, count);
            }

            int imageIndex = 1;
            Panel columnPanel = new Panel();
            columnPanel.ID = "panelRow" + imageIndex.ToString();
            columnPanel.CssClass = "w3-container";
            columnPanel.Style["width"] = "40%";

            foreach (ImageBasicInfo imageInfo in block.items)
            {
                Panel rowPanel = new Panel();
                rowPanel.ID = "panel" + imageIndex.ToString();
                rowPanel.CssClass = "w3-row w3-container w3-margin-bottom";
                Panel panelImgCol = new Panel();
                panelImgCol.ID = "panelImgCol" + imageIndex.ToString();
                panelImgCol.CssClass = "w3-quarter w3-container w3-margin-bottom";
                panelImgCol.Style["text-align"] = "right";
                Panel panelContentCol = new Panel();
                panelContentCol.ID = "panelContent" + imageIndex.ToString();
                panelContentCol.CssClass = "w3-rest w3-container";
                panelContentCol.Style["text-align"] = "left";
                Panel panelContentTitleRow = new Panel();
                panelContentTitleRow.ID = "panelContentTitleRow" + imageIndex.ToString();
                panelContentTitleRow.CssClass = "w3-row";
                panelContentTitleRow.Style["margin-bottom"] = "3px";
                Panel panelContentLikeRow = new Panel();
                panelContentLikeRow.ID = "panelContentLikeRow" + imageIndex.ToString();
                panelContentLikeRow.CssClass = "w3-row";
                panelContentLikeRow.Style["margin-bottom"] = "3px";
                Panel panelContentCommentRow = new Panel();
                panelContentCommentRow.ID = "panelContentCommentRow" + imageIndex.ToString();
                panelContentCommentRow.CssClass = "w3-row";
                panelContentCommentRow.Style["margin-bottom"] = "3px";

                System.Web.UI.WebControls.Image imgImage = new System.Web.UI.WebControls.Image();
                HyperLink lnkDetails = new HyperLink();
                Label lblTitle = new Label();
                HyperLink lnkAuthor = new HyperLink();
                HyperLink lnkComments = new HyperLink();
                Button btnNewComment = new Button();
                Button btnLike = new Button();
                Label lblLikes = new Label();
                imgImage.ID = "imgImageId" + imageIndex.ToString();
                bool isAuthenticated = SessionManager.IsUserAuthenticated(Context);
                long userId = 0;
                if (isAuthenticated) userId = SessionManager.GetUserSession(Context).UserProfileId;

                byte[] imageAsBytes1 = ActionsManager.GetThumbnail(imageInfo.imageId);
                imgImage.ImageUrl = "data:image;base64," + Convert.ToBase64String(imageAsBytes1);
                imgImage.AlternateText = imageInfo.imageId.ToString();
                imgImage.CssClass = "w3-hover-opacity";

                lnkDetails.ID = "lnkDetails" + imageIndex.ToString();
                lnkDetails.NavigateUrl = String.Format("~/Pages/ImageDetail.aspx?imgID=" + imageInfo.imageId);
                lnkDetails.Style["margin-left"] = "3px";

                lblTitle.ID = "lblTitle" + imageIndex.ToString();
                lblTitle.Text = imageInfo.title + " by";
                lblTitle.Style["margin-left"] = "3px";
                lblTitle.Style["margin-top"] = "3px";

                lnkAuthor.ID = "lnkAuthor" + imageIndex.ToString();
                lnkAuthor.Text = imageInfo.userName;
                lnkAuthor.NavigateUrl = String.Format("~/Pages/User/UserProfile.aspx?userId=" + imageInfo.userId);
                lnkAuthor.Style["margin-left"] = "3px";
                lnkAuthor.Style["font-style"] = "italic";

                if (imageInfo.hasComments)
                {
                    lnkComments.ID = "lnkComments" + imageIndex.ToString();
                    lnkComments.NavigateUrl = String.Format("~/Pages/Image/ShowComments.aspx?userId=" + imageInfo.userId);
                    lnkComments.Style["margin-left"] = "3px";
                }
                else lnkComments.Visible = false;

                btnNewComment.ID = "btnNewComment" + imageIndex.ToString();
                btnNewComment.CommandArgument = imageInfo.imageId.ToString();
                btnNewComment.Click += new EventHandler(BtnNewCommentOnClick);
                btnNewComment.Text = "Post";
                btnNewComment.Style["margin-left"] = "3px";

                btnLike.ID = "btnLike" + imageIndex.ToString();
                btnLike.Text = "Like";
                btnLike.Style["margin-left"] = "3px";
                if (isAuthenticated && ActionsManager.AlreadyLiked(userId, imageInfo.imageId))
                    btnLike.Enabled = false;

                btnLike.Click += new EventHandler(BtnLikeOnClick);
                btnLike.CommandArgument = imageInfo.imageId.ToString();

                lblLikes.ID = "lblLikes" + imageIndex.ToString();
                lblLikes.Text = "Likes: " + imageInfo.likes.ToString();
                lblLikes.Style["margin-left"] = "3px";

                imageIndex++;
                lnkDetails.Controls.Add(imgImage);
                panelContentTitleRow.Controls.Add(lblTitle);
                panelContentTitleRow.Controls.Add(lnkAuthor);
                panelContentLikeRow.Controls.Add(btnLike);
                panelContentLikeRow.Controls.Add(lblLikes);
                panelContentCommentRow.Controls.Add(btnNewComment);
                panelContentCommentRow.Controls.Add(lnkComments);
                panelContentCol.Controls.Add(panelContentTitleRow);
                panelContentCol.Controls.Add(panelContentLikeRow);
                panelContentCol.Controls.Add(panelContentCommentRow);
                panelImgCol.Controls.Add(lnkDetails);
                rowPanel.Controls.Add(panelImgCol);
                rowPanel.Controls.Add(panelContentCol);
                columnPanel.Controls.Add(rowPanel);
                //PlaceHolder_ImageCards.Controls.Add(imgImage);
                //PlaceHolder_ImageCards.Controls.Add(lnkDetails);
                //PlaceHolder_ImageCards.Controls.Add(lnkAuthor);
                //PlaceHolder_ImageCards.Controls.Add(lnkComments);
                //PlaceHolder_ImageCards.Controls.Add(btnNewComment);
                //PlaceHolder_ImageCards.Controls.Add(btnLike);
                //PlaceHolder_ImageCards.Controls.Add(lblLikes);
                lblFirstImageOk.Visible = true;
            }
            PlaceHolder_ImageCards.Controls.Add(columnPanel);

            if (startIndex > 0)
            {
                String urlPrevious;
                if (startIndex > 3)
                {
                    if (tag == "N/A")
                    {
                        urlPrevious = "~/Pages/Image/SearchResult.aspx?keywords=" + keywordsSeparated
                            + "&category=" + category
                            + "&tag=N/A"
                            + "&startIndex=" + (startIndex - 3)
                            + "&count=3";
                    }
                    else
                    {
                        urlPrevious = "~/Pages/Image/SearchResult.aspx?keywords="
                            + "&category="
                            + "&tag=" + tag
                            + "&startIndex=" + (startIndex - 3)
                            + "&count=3";
                    }
                }
                else
                {
                    if (tag == "N/A")
                    {
                        urlPrevious = "~/Pages/Image/SearchResult.aspx?keywords=" + keywordsSeparated
                            + "&category=" + category
                            + "&tag=N/A"
                            + "&startIndex=" + 0
                            + "&count=3";
                    }
                    else
                    {
                        urlPrevious = "~/Pages/Image/SearchResult.aspx?keywords="
                            + "&category="
                            + "&tag=" + tag
                            + "&startIndex=" + 0
                            + "&count=3";
                    }
                }
                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(urlPrevious);
                this.lnkPrevious.Visible = true;
            }

            if (block.existMoreItems)
            {
                String urlNext = "";
                if (tag == "N/A")
                {
                    urlNext = "~/Pages/Image/SearchResult.aspx?keywords=" + keywordsSeparated
                        + "&category=" + category
                        + "&tag=N/A"
                        + "&startIndex=" + (startIndex + 3)
                        + "&count=3";
                }
                else
                {
                    urlNext = "~/Pages/Image/SearchResult.aspx?keywords="
                        + "&category="
                        + "&tag=" + tag
                        + "&startIndex=" + (startIndex + 3)
                        + "&count=3";
                }

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(urlNext);
                this.lnkNext.Visible = true;
            }
        }

        protected void BtnLikeOnClick(object sender, EventArgs e)
        {
            lblSecondImageOk.Visible = true;
            if (SessionManager.IsUserAuthenticated(Context))
            {
                lblThirdImageOk.Visible = true;
                Button btn = (Button)sender;
                long userId = SessionManager.GetUserSession(Context).UserProfileId;
                ActionsManager.LikeImage(userId, long.Parse(btn.CommandArgument.ToString()));
            }
            else
            {
                Response.Redirect("~/Pages/User/Authentication.aspx");
            }
        }

        protected void BtnNewCommentOnClick(object sender, EventArgs e)
        {
            lblSecondImageOk.Visible = true;
            if (SessionManager.IsUserAuthenticated(Context))
            {
                lblThirdImageOk.Visible = true;
                Button btn = (Button)sender;
                Response.Redirect("~/Pages/Comment.aspx?imgID=" + btn.CommandArgument.ToString());
            } else
            {
                Response.Redirect("~/Pages/User/Authentication.aspx");
            }
        }

    }
}