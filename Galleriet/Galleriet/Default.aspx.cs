using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

            }
            else
            {
                // Skapar en custom validator och lägger till i listan.
                CustomValidator validator = new CustomValidator();
                validator.ErrorMessage = String.Format("Ett oväntat fel inträffade vid uppladdning av bilen. Försök igen.");
                Page.Validators.Add(validator);
            }
        }
    }
}