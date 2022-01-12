using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.Photogram.Web.HTTP.Session;
using Es.Udc.DotNet.Photogram.Web.HTTP.Actions;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using System.Globalization;

namespace Es.Udc.DotNet.Photogram.Web.Pages.Image
{
    public partial class Upload : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lnkUploadImg.Enabled = false;
            lblCategoryError.Visible = false;
            lblDescriptionError.Visible = false;
            lblTitleError.Visible = false;
            lblUploadOk.Visible = false;
            lblUploadFailed.Visible = false;

            if (!SessionManager.IsUserAuthenticated(Context))
                btnUpload.Enabled = false;
            if (!IsPostBack)
            {
                List<ListItem> ddlCategoryItems = ActionsManager.ImageCategories();

                foreach (ListItem categoryItem in ddlCategoryItems)
                {
                    ddlCategory.Items.Add(categoryItem);
                }
            }
        }

        private Dictionary<string, float> ParseImageExif()
        {
            Dictionary<string, float> exifs = new Dictionary<string, float>();

            var ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.NumberDecimalSeparator = ",";
            float aperture, exposure, iso, whiteBalance;

            if (txtDiaph.Text.Count() == 0) aperture = float.NaN;
            else aperture = float.Parse(txtDiaph.Text);

            if (txtExposition.Text.Count() == 0) exposure = float.NaN;
            else exposure = float.Parse(txtExposition.Text);

            if (txtIso.Text.Count() == 0) iso = float.NaN;
            else iso = float.Parse(txtIso.Text);

            if (txtWhiteBalance.Text.Count() == 0) whiteBalance = float.NaN;
            else whiteBalance = float.Parse(txtWhiteBalance.Text);

            exifs.Add("aperture", aperture);
            exifs.Add("exposure", exposure);
            exifs.Add("iso", iso);
            exifs.Add("whiteBalance", whiteBalance);
            return exifs;
        }

        protected void BtnUploadClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    lblUploadFailed.Visible = false;
                    lblUploadOk.Visible = false;

                    byte[] imageAsBytes;
                    Stream imageStream = inImageFile.PostedFile.InputStream;
                    using (var memoryStream = new MemoryStream())
                    {
                        imageStream.CopyTo(memoryStream);
                        imageAsBytes = memoryStream.ToArray();
                    }

                    string imageAsBase64 = Convert.ToBase64String(imageAsBytes);

                    UserSession userSession = SessionManager.GetUserSession(Context);

                    Dictionary<string, float> exifs = ParseImageExif();

                    ImageDto imageDto = new ImageDto(
                        txtTitle.Text, txtDescription.Text,
                        int.Parse(ddlCategory.SelectedValue), imageAsBase64,
                        userSession.UserProfileId, txtTags.Text,
                        exifs["aperture"], exifs["exposure"],
                        exifs["iso"], exifs["whiteBalance"]);

                    ActionsManager.UploadImage(imageDto);
                    lblUploadOk.Visible = true;
            }
                catch (Exception)
            {
                lblUploadFailed.Visible = true;
                lblUploadOk.Visible = false;
            }
        }
        }
    }
}