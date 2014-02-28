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

        private static readonly Regex ApprovedExtensions;
        private static string PhysicalUploadImagePath;

        #endregion

        #region Konstruktor

        static Gallery()
        {
            // Initierar fälten.
            ApprovedExtensions = new Regex(@"^.*\.(gif|jpg|png)$");

            PhysicalUploadImagePath = Path.Combine(
                AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Content", "Images");
        }

        #endregion

        #region Metoder

        public static IEnumerable<string> GetImageNames()
        {
            // Returnerar en referens av typen IEnumerable<string> till ett List-objekt innehållande bildernas filnamn sorterade i bokstavsordning.
            List<string> imageList = new List<string>(100);

            string[] files = Directory.GetFiles(PhysicalUploadImagePath);


            foreach (string fileName in files)
            {
                if (ApprovedExtensions.IsMatch(fileName))
                {
                    imageList.Add(fileName.Substring(fileName.LastIndexOf(Path.DirectorySeparatorChar) + 1));
                }
            }

            imageList.Sort();
            return imageList;
        }

        public static bool ImageExists(string name)
        {
            // Returnerar true om bild med angivet namn finns i katalogen Content\Images.
            return File.Exists(Path.Combine(PhysicalUploadImagePath, name));
        }

        private static bool IsValidImage(Image image)
        {
            // Returnerar true om den uppladdade filens innehåll verkligen är av typen gif, jpeg eller png.
            return image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid;
        }

        public static string SaveImage(Stream stream, string fileName)
        {
            /* Verifierar att filen är av rätt MIME-typ (annars kastas ett undantag), säkerställer att filnamnet är unik,
             * sparar bilden samt skapar och sparar en tumnagelbild. Filnamnet bilden sparas under returneras. */

            var image = System.Drawing.Image.FromStream(stream); // stream -> ström med bild

            if (!IsValidImage(image))
            {
                throw new ApplicationException("Bildens filändelse är inte tillåten!");
            }

            if (ImageExists(fileName))
            {
                int counter = 0;
                string extension = Path.GetExtension(fileName);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

                do
                {
                    // Lägger ihop den nya strängen till den duplicerade bilden.
                    fileName = String.Format("{0}({1}){2}", fileNameWithoutExtension, ++counter, extension);
                } while (ImageExists(fileName));
            }

            // Sparar bilden i Imagekatalogen.
            image.Save(Path.Combine(PhysicalUploadImagePath, fileName));

            // Skapar en tumnagelbild och sparar den i tumnagelkatalogen.
            var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
            thumbnail.Save(Path.Combine(PhysicalUploadImagePath, "Thumbs", fileName));

            return fileName;
        }

        #endregion
    }
}