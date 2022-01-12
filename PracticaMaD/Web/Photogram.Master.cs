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
            BuildTagCloudAsp(50);

            //ContentPlaceHolder_TagCloud.Controls.Add(new Literal() { Text = innerHtml });
        }
        
        private void BuildTagCloudAsp(int tagsToTake)
        {
            List<Tuple<string, int>> tagSizes = ActionsManager.TagSizes(0, tagsToTake);

            Panel tagsPanel = new Panel();
            tagsPanel.ID = "tagsPanel";
            tagsPanel.CssClass = "w3-container";
            tagsPanel.Style["text-align"] = "center";

            int tagIndex = 1;
            foreach (Tuple<string, int> tagSize in tagSizes)
            {
                string url = String.Format("~/Pages/Image/SearchResult.aspx?tags={0}&category={1}&tag={2}&startIndex={3}&count={4}", "", "", tagSize.Item1, 0, 3);
                HyperLink hyp = new HyperLink();
                hyp.ID = tagIndex.ToString();
                hyp.NavigateUrl = url;
                hyp.Text = tagSize.Item1 + " ";
                hyp.Font.Size = tagSize.Item2;
                hyp.Style["text-decoration"] = "none";
                tagsPanel.Controls.Add(hyp);
                tagIndex++;
            }
            ContentPlaceHolder_TagCloud.Controls.Add(tagsPanel);
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