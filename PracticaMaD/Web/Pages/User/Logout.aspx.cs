using System;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;

namespace Es.Udc.DotNet.Photogram.Web.Pages.User
{

    public partial class Logout : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionManager.Logout(Context);

            Response.Redirect("~/Pages/MainPage.aspx");


        }
    }
}
