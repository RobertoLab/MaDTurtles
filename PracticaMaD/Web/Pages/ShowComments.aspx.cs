using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Model.InteractionService;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;

namespace Es.Udc.DotNet.Photogram.Web.Pages
{
    public partial class ShowComments : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex;
            long imgID = Convert.ToInt32(Request.Params.Get("imgID"));

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoComments.Visible = false;

            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }


            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IInteractionService interactionService = iocManager.Resolve<IInteractionService>();

            /* Get the Comments */

            Block<CommentInfo> comments = interactionService.GetImageComments(imgID, startIndex, 3);

            if (comments.items.Count == 0)
            {
                lblNoComments.Visible = true;
                return;
            }

            /* Display the usernames and comments */
            if (comments.items.Count > 0) {
                this.lnkUser1.Text = comments.items.ElementAt(0).userName;
                this.commentUser1.Text = comments.items.ElementAt(0).comment;
            }
            if (comments.items.Count > 1) {
                this.lnkUser2.Text = comments.items.ElementAt(1).userName;
                this.commentUser2.Text = comments.items.ElementAt(1).comment;

            }
            if (comments.items.Count > 2){ 
            this.lnkUser3.Text = comments.items.ElementAt(2).userName;            
            this.commentUser3.Text = comments.items.ElementAt(2).comment;
            }


            /* "Previous" link */
            if (startIndex > 0)
            {
                String url;
                if (startIndex > 3) { 
                    url = "/Pages/ShowComments.aspx" + "?imgID=" + imgID +
                    "&startIndex=" + (startIndex-3);
                }else
                {
                    url = "/Pages/ShowComments.aspx" + "?imgID=" + imgID +
                                        "&startIndex=0";
                }
                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (comments.existMoreItems)
            {
                String url = "/Pages/ShowComments.aspx" + "?imgID=" + imgID +
                    "&startIndex=" + (startIndex + 3);

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }
    }
}