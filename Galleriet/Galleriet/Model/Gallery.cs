﻿using System;
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
        private static readonly Regex SanitizePath;

        #endregion

        #region Konstruktor

        static Gallery()
        {
            // Initierar fälten.
            ApprovedExtensions = new Regex(@"^.*\.(gif|jpg|png)$");

            PhysicalUploadImagePath = Path.Combine(
                AppDomain.CurrentDomain.GetData("APPBASE").ToString(),
                @"Content\Images");

            // http://stackoverflow.com/questions/146134/how-to-remove-illegal-characters-from-path-and-filenames/146162#146162
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            SanitizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
            invalidChars = SanitizePath.Replace(invalidChars, "");
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
                    imageList.Add(fileName);
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

            if (IsValidImage(image))
            {
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

                    // Sparar bilden i Imagekatalogen.
                    image.Save(Path.Combine(PhysicalUploadImagePath, fileName));

                    // Skapar en tumnagelbild och sparar den i tumnagelkatalogen.
                    var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
                    thumbnail.Save(Path.Combine(
                    AppDomain.CurrentDomain.GetData("APPBASE").ToString(),
                    @"Content\Images\Thumbs", fileName));
                }
                else
                {
                    // Sparar bilden i Imagekatalogen.
                    image.Save(Path.Combine(PhysicalUploadImagePath, fileName));

                    // Skapar en tumnagelbild och sparar den i tumnagelkatalogen.
                    var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
                    thumbnail.Save(Path.Combine(
                    AppDomain.CurrentDomain.GetData("APPBASE").ToString(),
                    @"Content\Images\Thumbs", fileName));
                }
            }
            else
            {
                throw new ApplicationException("Bildens filändelse är inte tillåten!");
            }

            return fileName;
        }

        #endregion
    }
}