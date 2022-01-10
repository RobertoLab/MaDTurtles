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
    public partial class Photogram : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Tuple<string, int>> tagSizes = ActionsManager.TagSizes(0, 50);

                string innerHtml = "<ul>";
                foreach (Tuple<string, int> tagSize in tagSizes)
                {
                    innerHtml += "<li style=\"font-size:"+ tagSize.Item2.ToString() +
                        "px\">" + tagSize.Item1 + "</li>";
                }
                innerHtml += "</ul>";
                
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

        protected void BtnTagCloud1OnClick()
        {
            string innerHtml = BuildTagCloudHtml(10);

            rbTagCloud1.Checked = true;
            rbTagCloud2.Checked = false;
            rbTagCloud3.Checked = false;
            ContentPlaceHolder_TagCloud.Controls.Add(new Literal() { Text = innerHtml });
        }

        protected void BtnTagCloud2OnClick()
        {
            string innerHtml = BuildTagCloudHtml(25);

            rbTagCloud1.Checked = false;
            rbTagCloud2.Checked = true;
            rbTagCloud3.Checked = false;
            ContentPlaceHolder_TagCloud.Controls.Add(new Literal() { Text = innerHtml });
        }

        protected void BtnTagCloud3OnClick()
        {
            string innerHtml = BuildTagCloudHtml(25);

            rbTagCloud1.Checked = false;
            rbTagCloud2.Checked = false;
            rbTagCloud3.Checked = true;
            ContentPlaceHolder_TagCloud.Controls.Add(new Literal() { Text = innerHtml });
        }
    }
}