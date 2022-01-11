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

                string tagsSeparated = Request.QueryString["tags"];
                string category = Request.QueryString["category"];
                string tags = tagsSeparated.Replace('-', ' ');

                Block<ImageBasicInfo> block = 
                    ActionsManager.SearchImageBasic(tags, category, startIndex, count);

                if (block.items.Count == 1)
                {
                    byte[] imageAsBytes1 = ActionsManager.GetThumbnail(block.items[0].imageId);
                    imgImage1.Visible = true;
                    imgImage1.ImageUrl = "data:image;base64," + Convert.ToBase64String(imageAsBytes1);

                    lnkDetails1.Visible = true;
                    lnkDetails1.Text = block.items[0].title;
                    lnkDetails1.NavigateUrl = String.Format("~/Pages/Image/ImageDetails.aspx?imgId=" + block.items[0].imageId);

                    lnkAuthor1.Visible = true;
                    lnkAuthor1.Text = block.items[0].userName;
                    lnkAuthor1.NavigateUrl = String.Format("~/Pages/User/UserProfile.aspx?userId=" + block.items[0].userId);

                    if (block.items[0].hasComments)
                    {
                        lnkComments1.Visible = true;
                        lnkComments1.NavigateUrl = String.Format("~/Pages/Image/ShowComments.aspx?userId=" + block.items[0].userId);
                    }
                    lnkNewComment1.Visible = true;
                    lnkNewComment1.NavigateUrl = String.Format("~/Pages/Image/PostComment.aspx?imgId=" + block.items[0].imageId);

                    lblLikes1.Visible = true;
                    lblLikes1.Text = block.items[0].likes.ToString();
                }
                if (block.items.Count == 2)
                {
                    byte[] imageAsBytes2 = ActionsManager.GetThumbnail(block.items[1].imageId);
                    imgImage2.Visible = true;
                    imgImage2.ImageUrl = "data:image;base64," + Convert.ToBase64String(imageAsBytes2);

                    lnkDetails2.Visible = true;
                    lnkDetails2.Text = block.items[0].title;
                    lnkDetails2.NavigateUrl = String.Format("~/Pages/Image/ImageDetails.aspx?imgId=" + block.items[1].imageId);

                    lnkAuthor2.Visible = true;
                    lnkAuthor2.Text = block.items[0].userName;
                    lnkAuthor2.NavigateUrl = String.Format("~/Pages/User/UserProfile.aspx?userId=" + block.items[1].userId);

                    if (block.items[1].hasComments)
                    {
                        lnkComments2.Visible = true;
                        lnkComments2.NavigateUrl = String.Format("~/Pages/Image/ShowComments.aspx?userId=" + block.items[1].userId);
                    }
                    lnkNewComment2.Visible = true;
                    lnkNewComment2.NavigateUrl = String.Format("~/Pages/Image/PostComment.aspx?imgId=" + block.items[1].imageId);
                    
                    lblLikes2.Visible = true;
                    lblLikes2.Text = block.items[1].likes.ToString();
                }
                if (block.items.Count == 3)
                {
                    byte[] imageAsBytes3 = ActionsManager.GetThumbnail(block.items[2].imageId);
                    imgImage3.Visible = true;
                    imgImage3.ImageUrl = "data:image;base64," + Convert.ToBase64String(imageAsBytes3);

                    lnkDetails3.Visible = true;
                    lnkDetails3.Text = block.items[0].title;
                    lnkDetails3.NavigateUrl = String.Format("~/Pages/Image/ImageDetails.aspx?imgId=" + block.items[2].imageId);

                    lnkAuthor3.Visible = true;
                    lnkAuthor3.Text = block.items[0].userName;
                    lnkAuthor3.NavigateUrl = String.Format("~/Pages/User/UserProfile.aspx?userId=" + block.items[2].userId);

                    if (block.items[2].hasComments)
                    {
                        lnkComments3.Visible = true;
                        lnkComments3.NavigateUrl = String.Format("~/Pages/Image/ShowComments.aspx?userId=" + block.items[2].userId);
                    }
                    lnkNewComment3.Visible = true;
                    lnkNewComment3.NavigateUrl = String.Format("~/Pages/Image/PostComment.aspx?imgId=" + block.items[2].imageId);
                    
                    lblLikes3.Visible = true;
                    lblLikes3.Text = block.items[2].likes.ToString();
                }

                if (startIndex > 0)
                {
                    String urlPrevious;
                    if (startIndex > 3)
                    {
                        urlPrevious = "~/Pages/SearchResult.aspx?tags=" + tagsSeparated
                            + "&category=" + category
                            + "&startIndex=" + (startIndex - 3)
                            + "&count=3";
                    }
                    else
                    {
                        urlPrevious = "~/Pages/SearchResult.aspx?tags=" + tagsSeparated
                            + "&category=" + category
                            + "&startIndex=" + 0
                            + "&count=3";
                    }
                    this.lnkPrevious.NavigateUrl =
                        Response.ApplyAppPathModifier(urlPrevious);
                    this.lnkPrevious.Visible = true;
                }

                if (block.existMoreItems)
                {
                    String urlNext = "~/Pages/SearchResult.aspx?tags=" + tagsSeparated
                        + "&category=" + category
                        + "&startIndex=" + (startIndex + 3)
                        + "&count=3";

                    this.lnkNext.NavigateUrl =
                        Response.ApplyAppPathModifier(urlNext);
                    this.lnkNext.Visible = true;
                }
            }
        }
    }
}