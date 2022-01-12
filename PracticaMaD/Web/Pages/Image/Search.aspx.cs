using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Web.HTTP.Actions;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.Photogram.Web.Pages.Image
{
    public partial class Search : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<ListItem> ddlCategoryItems = ActionsManager.ImageCategories();

                ddlCategory.Items.Add(new ListItem("N/A", "N/A"));
                foreach (ListItem categoryItem in ddlCategoryItems)
                {
                    ddlCategory.Items.Add(categoryItem);
                }
            }
        }

        protected void BtnSearchOnClick(object sender, EventArgs e)
        {
            string keywords = txtKeywords.Text;
            string category = ddlCategory.SelectedValue;
            string keywordsSeparated = "";
            if (!string.IsNullOrEmpty(keywords))
            {
                keywordsSeparated = keywords.Replace(System.Environment.NewLine, "|");
                keywordsSeparated = keywords.Replace(' ', '|');
            }
            if (category == "N/A") category = "";

            Response.Redirect("~/Pages/Image/SearchResult.aspx?keywords=" + keywordsSeparated
                + "&category=" + category
                + "&tag=N/A" 
                + "&startIndex=0"
                + "&count=3");
        }
    }
}