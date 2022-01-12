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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int startIndex;
                int count;
                try
                {
                    startIndex = int.Parse(Request.QueryString["startIndex"]);
                    count = int.Parse(Request.QueryString["count"]);
                } catch (ArgumentNullException)
                {
                    startIndex = 0;
                    count = 3;
                }

                string tag = Request.QueryString["tag"];
                string keywordsSeparated = "";
                string category = "";
                Block<ImageBasicInfo> block;
                if (tag=="N/A")
                {
                    keywordsSeparated = Request.QueryString["keywords"];
                    category = Request.QueryString["category"];
                    string keywords = "";
                    if (!string.IsNullOrEmpty(keywordsSeparated))
                        keywords = keywordsSeparated.Replace('|', ' ');
                    block = ActionsManager.SearchImageBasic(keywords, category, startIndex, count);
                } else
                {
                    block = ActionsManager.SearchImageBasic(tag, startIndex, count);
                }
                
                int imageIndex = 1;
                foreach (ImageBasicInfo imageInfo in block.items)
                {
                    System.Web.UI.WebControls.Image imgImage = new System.Web.UI.WebControls.Image();
                    HyperLink lnkDetails = new HyperLink();
                    HyperLink lnkAuthor = new HyperLink();
                    HyperLink lnkComments = new HyperLink();
                    HyperLink lnkNewComment = new HyperLink();
                    Button btnLike = new Button();
                    Label lblLikes = new Label();
                    imgImage.ID = "imgImageId" + imageIndex.ToString();
                    bool isAuthenticated = SessionManager.IsUserAuthenticated(Context);
                    long userId = 0;
                    if (isAuthenticated) userId = SessionManager.GetUserSession(Context).UserProfileId;

                    byte[] imageAsBytes1 = ActionsManager.GetThumbnail(imageInfo.imageId);
                    imgImage.ImageUrl = "data:image;base64," + Convert.ToBase64String(imageAsBytes1);
                    imgImage.AlternateText = imageInfo.imageId.ToString();

                    lnkDetails.ID = "lnkDetails" + imageIndex.ToString();
                    lnkDetails.Text = imageInfo.title;
                    lnkDetails.NavigateUrl = String.Format("~/Pages/ImageDetails.aspx?imgId=" + imageInfo.imageId);

                    lnkDetails.ID = "lnkAuthor" + imageIndex.ToString();
                    lnkDetails.Text = imageInfo.userName;
                    lnkDetails.NavigateUrl = String.Format("~/Pages/User/UserProfile.aspx?userId=" + imageInfo.userId);

                    if (imageInfo.hasComments)
                    {
                        lnkComments.ID = "lnkComments" + imageIndex.ToString();
                        lnkComments.NavigateUrl = String.Format("~/Pages/Image/ShowComments.aspx?userId=" + imageInfo.userId);
                    }
                    else lnkComments.Visible = false;
                    
                    lnkNewComment.ID = "lnkNewComment" + imageIndex.ToString();
                    lnkNewComment.NavigateUrl = String.Format("~/Pages/Comment.aspx?imgId=" + imageInfo.imageId);
                    lnkNewComment.Text = "Post";

                    btnLike.ID = "btnLike" + imageIndex.ToString();
                    btnLike.Text = "Like";
                    if (isAuthenticated && ActionsManager.AlreadyLiked(userId, imageInfo.imageId))
                        btnLike.Enabled = false;

                    btnLike.Command += BtnLikeOnClick;
                    btnLike.CommandArgument = imageInfo.imageId.ToString();

                    lblLikes.ID = "lblLikes" + imageIndex.ToString();
                    lblLikes.Text = imageInfo.likes.ToString();

                    if (!isAuthenticated)
                    {
                        lnkNewComment.NavigateUrl = "~/Pages/User/Register.aspx";
                    }

                    imageIndex++;
                    PlaceHolder_ImageCards.Controls.Add(imgImage);
                    PlaceHolder_ImageCards.Controls.Add(lnkDetails);
                    PlaceHolder_ImageCards.Controls.Add(lnkAuthor);
                    PlaceHolder_ImageCards.Controls.Add(lnkComments);
                    PlaceHolder_ImageCards.Controls.Add(lnkNewComment);
                    PlaceHolder_ImageCards.Controls.Add(btnLike);
                    PlaceHolder_ImageCards.Controls.Add(lblLikes);
                    lblFirstImageOk.Visible = true;
                    lblSecondImageOk.Visible = true;
                }

                lblThirdImageOk.Visible = true;

                if (startIndex > 0)
                {
                    String urlPrevious;
                    if (startIndex > 3)
                    {
                        if (tag=="N/A")
                        {
                            urlPrevious = "~/Pages/Image/SearchResult.aspx?keywords=" + keywordsSeparated
                                + "&category=" + category
                                + "&tag=N/A"
                                + "&startIndex=" + (startIndex - 3)
                                + "&count=3";
                        } else
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
        }
        
        protected void BtnLikeOnClick(object sender, EventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                Button btn = (Button)sender;
                long userId = SessionManager.GetUserSession(Context).UserProfileId;
                ActionsManager.LikeImage(userId, long.Parse(btn.CommandArgument.ToString()));
            }
            else
            {
                Response.Redirect("~/Pages/User/Register.aspx");
            }
        }
        
    }
}