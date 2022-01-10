using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.Photogram.Web.HTTP.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Photogram.Web
{
    public partial class Photogram : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string innerHtml = BuildTagCloudHtml(50);

                ContentPlaceHolder_TagCloud.Controls.Add(new Literal() { Text = innerHtml });
            }
        }

        private string BuildTagCloudHtml(int tagsToTake)
        {
            List<Tuple<string, int>> tagSizes = ActionsManager.TagSizes(0, tagsToTake);

            string innerHtml = "<ul>";
            foreach (Tuple<string, int> tagSize in tagSizes)
            {
                innerHtml += "<li style=\"font-size:" + tagSize.Item2.ToString() +
                    "px\">" + tagSize.Item1 + "</li>";
            }
            innerHtml += "</ul>";

            return innerHtml;
        }

        //protected void BtnTagCloud1OnClick(object sender, EventArgs e)
        //{
        //    string innerHtml = BuildTagCloudHtml(10);

        //    btTagCloud1.Enabled = false;
        //    btTagCloud2.Enabled = true;
        //    btTagCloud3.Enabled = true;
        //    ContentPlaceHolder_TagCloud.Controls.Add(new Literal() { Text = innerHtml });
        //}

        //protected void BtnTagCloud2OnClick(object sender, EventArgs e)
        //{
        //    string innerHtml = BuildTagCloudHtml(25);

        //    btTagCloud1.Enabled = true;
        //    btTagCloud2.Enabled = false;
        //    btTagCloud3.Enabled = true;
        //    ContentPlaceHolder_TagCloud.Controls.Add(new Literal() { Text = innerHtml });
        //}

        //protected void BtnTagCloud3OnClick(object sender, EventArgs e)
        //{
        //    string innerHtml = BuildTagCloudHtml(25);

        //    btTagCloud1.Enabled = true;
        //    btTagCloud2.Enabled = true;
        //    btTagCloud3.Enabled = false;
        //    ContentPlaceHolder_TagCloud.Controls.Add(new Literal() { Text = innerHtml });
        //}
    }
}