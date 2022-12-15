using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Weavers.Models.Helpers
{
    public static class UploadImage
    {
        public static string SaveImage(string imageStr, string mobileNumber, string imagePath, string namePrefix)
        {
            var uploadedImage = string.Empty;
            if (!string.IsNullOrEmpty(imageStr) && imageStr.Length > 50 && !imageStr.Contains("gjitsolution"))
            {
                try
                {

                    var basepath = imagePath;
                    basepath = Path.Combine(basepath, mobileNumber);
                    if (!Directory.Exists(basepath))
                    {
                        Directory.CreateDirectory(basepath);
                    }

                    var dtm = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                    var imageName = namePrefix + ".jpg";

                    var imgPath = Path.Combine(basepath, imageName);

                    if (File.Exists(imgPath))
                    {
                        File.Delete(imgPath);
                    }

                    byte[] imageBytes = Convert.FromBase64String(imageStr);

                    File.WriteAllBytes(imgPath, imageBytes);

                    uploadedImage = string.Format("{0}/{1}", mobileNumber, imageName);

                }
                catch (Exception ex)
                {
                    uploadedImage = ex.Message;
                }
            }
            return uploadedImage;
        }

        public static string SaveReportImage(string imageStr, string imagePath, string namePrefix)
        {
            var uploadedImage = string.Empty;
            if (!string.IsNullOrEmpty(imageStr) && imageStr.Length > 50 && !imageStr.Contains("gjitsolution"))
            {
                try
                {

                    var basepath = imagePath;
                    basepath = Path.Combine(basepath, namePrefix);
                    if (!Directory.Exists(basepath))
                    {
                        Directory.CreateDirectory(basepath);
                    }

                    var dtm = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                    var imageName = dtm + ".jpg";

                    var imgPath = Path.Combine(basepath, imageName);

                    if (File.Exists(imgPath))
                    {
                        File.Delete(imgPath);
                    }

                    byte[] imageBytes = Convert.FromBase64String(imageStr);

                    File.WriteAllBytes(imgPath, imageBytes);

                    uploadedImage = string.Format("{0}/{1}", namePrefix, imageName);

                }
                catch (Exception ex)
                {
                    uploadedImage = ex.Message;
                }
            }
            return uploadedImage;
        }
    }
}