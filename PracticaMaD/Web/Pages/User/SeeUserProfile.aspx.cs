using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using System;

namespace Es.Udc.DotNet.Photogram.Web.Pages.User
{
    public partial class SeeUserProfile : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserSession user = (UserSession)Context.Items["user"];

            long userId = user.UserProfileId;

            /* Get the Services */
            IIoCManager iocManager = (IIoCManager)System.Web.HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();
            IImageService imageService = iocManager.Resolve<IImageService>();

            UserProfileDetails userProfile = userService.FindUserProfileDetails(userId);

            lblUserName.Text = userProfile.firstName;
            lblUserLastName.Text = userProfile.lastName;
            lblUserEmail.Text = userProfile.email;

            userProfile.GetImage(userProfile);
        }
    }
}