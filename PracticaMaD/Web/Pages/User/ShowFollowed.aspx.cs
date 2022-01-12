using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Model.InteractionService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Es.Udc.DotNet.Photogram.Web.Pages.User
{
    public partial class ShowFollowed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long userId = Convert.ToInt32(Request.Params.Get("userId"));
            int startIndex;

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
            IInteractionService interactionService = iocManager.Resolve<IInteractionService>();
            Block<UserInfo> followed = userService.FindFollowed(userId,startIndex,5);

            if (followed.items.Count == 0)
            {
                lblNoFollowed.Visible = true;
                return;
            }

            if (followed.items.Count > 0)
            {
                lnk1.Text = followed.items.ElementAt(0).userName;
                lnk1.NavigateUrl = String.Format("~/Pages/User/SeeUserProfile.aspx?userId={0}", followed.items.ElementAt(0).userId);
            }
            if (followed.items.Count > 1)
            {
                lnk2.Text = followed.items.ElementAt(1).userName;
                lnk2.NavigateUrl = String.Format("~/Pages/User/SeeUserProfile.aspx?userId={0}", followed.items.ElementAt(1).userId);
            }
            if (followed.items.Count > 2)
            {
                lnk3.Text = followed.items.ElementAt(2).userName;
                lnk3.NavigateUrl = String.Format("~/Pages/User/SeeUserProfile.aspx?userId={0}", followed.items.ElementAt(2).userId);
            }
            if (followed.items.Count > 3)
            {
                lnk4.Text = followed.items.ElementAt(3).userName;
                lnk4.NavigateUrl = String.Format("~/Pages/User/SeeUserProfile.aspx?userId={0}", followed.items.ElementAt(3).userId);
            }
            if (followed.items.Count > 4)
            {
                lnk5.Text = followed.items.ElementAt(4).userName;
                lnk5.NavigateUrl = String.Format("~/Pages/User/SeeUserProfile.aspx?userId={0}", followed.items.ElementAt(4).userId);
            }









        }
    }
}