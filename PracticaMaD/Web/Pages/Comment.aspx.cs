using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Model.InteractionService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;

namespace Es.Udc.DotNet.Photogram.Web.Pages
{
    public partial class Comment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCommentClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                /* Get the data */

                long imgID = Convert.ToInt32(Request.Params.Get("imgID"));
                UserSession userSession = SessionManager.GetUserSession(Context);
                long userID = userSession.UserProfileId;
                String comment = this.txtComment.Text;


                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IInteractionService interactionService = iocManager.Resolve<IInteractionService>();

                interactionService.PostComment(comment, userID, imgID);
                String url = String.Format("./ImageDetail.aspx?imgID={0}", imgID);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }

        }
    }
}