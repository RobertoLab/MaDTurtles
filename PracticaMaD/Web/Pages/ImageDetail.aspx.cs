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

            this.imgAuthor.Text = user.userName;

            this.Image1.ImageUrl = "data:image;base64," + image.imgBase64;
            this.Image1.Width = Unit.Pixel(500);
            this.Image1.Height = Unit.Pixel(500);

            this.imgLikes.Text = Convert.ToString(interactionService.GetImageLikes(imgID));

            this.btnLike.Visible = true;
            this.btnUnlike.Visible = false;


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
    }
}