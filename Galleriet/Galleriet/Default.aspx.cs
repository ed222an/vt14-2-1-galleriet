using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Galleriet.Model;
using System.IO;

namespace Galleriet
{
    public partial class Default : System.Web.UI.Page
    {
        private bool SuccessMessage
        {
            get { return Session["SuccessMessage"] as bool? == true; }
            set { Session["SuccessMessage"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var fileName = Request.QueryString["name"];
            if (!String.IsNullOrWhiteSpace(fileName))
            {
                LargeImage.ImageUrl = String.Format("~/Content/Images/{0}", fileName);
                LargeImage.Visible = true;
            }

            if (SuccessMessage)
            {
                SuccessPanel.Visible = true;
                Session.Remove("SuccessMessage");
            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Sparar den valda bilden.
                string fileName = Path.GetFileName(ChooseFileUpload.PostedFile.FileName);
                Stream fileStream = ChooseFileUpload.FileContent;

                Gallery.SaveImage(fileStream, fileName);
                Session["SuccessMessage"] = true;
                Response.Redirect("?name=" + fileName);
            }
            else
            {
                // Skapar en custom validator och lägger till i listan.
                CustomValidator validator = new CustomValidator();
                validator.ErrorMessage = String.Format("Ett oväntat fel inträffade vid uppladdning av bilen. Försök igen.");
                Page.Validators.Add(validator);
                SuccesLabel.Visible = false;
            }
        }

        public IEnumerable<string> ImageRepeater_GetData()
        {
            return Gallery.GetImageNames();
        }

        protected void ImageRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void CloseImageButton_Click(object sender, ImageClickEventArgs e)
        {
            SuccessPanel.Visible = false;
        }
    }
}