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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Sparar den valda bilden.
                string fileName = Path.GetFileName(ChooseFileUpload.PostedFile.FileName);
                Stream fileStream = ChooseFileUpload.FileContent;

                var gallery = new Gallery();
                Gallery.SaveImage(fileStream, fileName);
            }
            else
            {
                // Skapar en custom validator och lägger till i listan.
                CustomValidator validator = new CustomValidator();
                validator.ErrorMessage = String.Format("Ett oväntat fel inträffade vid uppladdning av bilen. Försök igen.");
                Page.Validators.Add(validator);
            }
        }

        public IEnumerable<string> ImageRepeater_GetData()
        {
            return Gallery.GetImageNames();
        }

        protected void ImageRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}