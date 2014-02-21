using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Galleriet.Model
{
    public class Gallery
    {
        #region Fält

        private static readonly IEnumerable<string> ApprovedExtensions;
        private static string PhysicalUploadImagePath;
        private static readonly Regex SanitizePath;

        #endregion

        #region Konstruktor

        private static Gallery()
        {
            // TODO: Initialisera fälten.
        }

        #endregion

        #region Metoder

        public IEnumerable<string> GetImageNames()
        {
            // Returnerar en referens av typen IEnumerable<string> till ett List-objekt innehållande bildernas filnamn sorterade i bokstavsordning.


            throw new NotImplementedException();
        }

        public bool ImageExists(string name)
        {
            // Returnerar true om bild med angivet namn finns i katalogen Content/Images.


            return false;
        }

        private bool IsValidImage(Image image)
        {
            // Returnerar true om den uppladdade filens innehåll verkligen är av typen gif, jpeg eller png.


            return false;
        }

        public string SaveImage(Stream stream, string fileName)
        {
            /* Verifierar att filen är av rätt MIME-typ (annars kastas ett undantag), säkerställer att filnamnet är unik,
             * sparar bilden samt skapar och sparar en tumnagelbild. Filnamnet bilden sparas under returneras. */


            throw new NotImplementedException();
        }

        #endregion
    }
}