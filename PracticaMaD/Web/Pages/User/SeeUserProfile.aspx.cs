using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Model.InteractionService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Photogram.Web.Pages.User
{
    public partial class SeeUserProfile : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            long userId = Convert.ToInt32(Request.Params.Get("userId"));
            long myId = 0;
            int startIndex;

            /* Get session ID */
            if (SessionManager.GetUserSession(Context) != null)
            {
                UserSession userSession = SessionManager.GetUserSession(Context);
                myId = userSession.UserProfileId;
            }

            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get the Services */
            IIoCManager iocManager = (IIoCManager)System.Web.HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();
            IImageService imageService = iocManager.Resolve<IImageService>();
            IInteractionService interactionService = iocManager.Resolve<IInteractionService>();

            UserProfileDetails userProfile = userService.FindUserProfileDetails(userId);

            lblUserName.Text = userProfile.firstName;
            lblUserLastName.Text = userProfile.lastName;
            lblUserEmail.Text = userProfile.email;
            btnFollow.Visible = !interactionService.Follows(userId, myId);

            Block<ImageBasicInfo> images = imageService.SearchByUserId(userId,startIndex,3);
            if(images.items.Count > 0)
            {
                              
                ImageInfo image = imageService.SearchImageEager(images.items.ElementAt(0).imageId);
                this.img1.ImageUrl = "data:image;base64," + image.imgBase64;
                this.btnImg1.PostBackUrl = String.Format("~/Pages/ImageDetail.aspx?imgID={0}", images.items.ElementAt(0).imageId);
            }
            else
            {
                this.img1.Visible = false;
                this.btnImg1.Visible = false;
            }
            if (images.items.Count > 1)
            {

                ImageInfo image = imageService.SearchImageEager(images.items.ElementAt(1).imageId);
                this.img2.ImageUrl = "data:image;base64," + image.imgBase64;
                this.btnImg2.PostBackUrl = String.Format("~/Pages/ImageDetail.aspx?imgID={0}", images.items.ElementAt(1).imageId);
            }
            else
            {
                this.img2.Visible = false;
                this.btnImg2.Visible = false;
            }
            if (images.items.Count > 2)
            {

                ImageInfo image = imageService.SearchImageEager(images.items.ElementAt(2).imageId);
                this.img3.ImageUrl = "data:image;base64," + image.imgBase64;
                this.btnImg3.PostBackUrl = String.Format("~/Pages/ImageDetail.aspx?imgID={0}", images.items.ElementAt(2).imageId);
            }
            else
            {
                this.img3.Visible = false;
                this.btnImg3.Visible = false;
            }
        }

        protected void BtnImg1Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                /* Get the data */
                String url = this.btnImg1.PostBackUrl;
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }

        }
        protected void BtnImg2Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                /* Get the data */
                String url = this.btnImg2.PostBackUrl;
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }

        }
        protected void BtnImg3Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                /* Get the data */
                String url = this.btnImg3.PostBackUrl;
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }

        }

        protected void BtnFollowClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                /* Get data. */
                long userId = Convert.ToInt32(Request.Params.Get("userId"));
                long myId=0;
                if (SessionManager.GetUserSession(Context) != null)
                {
                    UserSession userSession = SessionManager.GetUserSession(Context);
                    myId = userSession.UserProfileId;
                }

                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IInteractionService interactionService = iocManager.Resolve<IInteractionService>();

                /* Do action. */
                if (myId != 0)
                {
                    interactionService.Follow(userId, myId);
                    btnFollow.Visible = false;
                }
                else
                {
                    Response.Redirect(Response.ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));
                }
            }

        }

        protected void BtnFollowedClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                /* Get the data */
                long userId = Convert.ToInt32(Request.Params.Get("userId"));

                String url = String.Format("~/Pages/User/ShowFollowed.aspx?userId={0}", userId);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }

        }

    }
}