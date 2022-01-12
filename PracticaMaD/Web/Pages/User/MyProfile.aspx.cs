using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Photogram.Model.ImageService;

namespace Es.Udc.DotNet.Photogram.Web.Pages.User
{
    public partial class MyProfile : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long userId = 0;
            if (SessionManager.GetUserSession(Context) != null)
            {
                UserSession userSession = SessionManager.GetUserSession(Context);
                userId = userSession.UserProfileId;
            }
            else
            {

                String url = ("~/Pages/MainPage.aspx");
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }

            /* Get the Services */
            IIoCManager iocManager = (IIoCManager)System.Web.HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();
            IImageService imageService = iocManager.Resolve<IImageService>();

            UserProfileDetails userProfile = userService.FindUserProfileDetails(userId);

            lblUserName.Text = userProfile.firstName;
            lblUserLastName.Text = userProfile.lastName;
            lblUserEmail.Text = userProfile.email;

            Block<ImageBasicInfo> images = imageService.SearchByUserId(userId,0,3);
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
    }
}