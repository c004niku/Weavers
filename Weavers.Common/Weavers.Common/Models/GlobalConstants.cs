using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Common.Models
{
    public static class GlobalConstants
    {
        private const bool IsGodaddy = true;
        public const string WebsiteName = IsGodaddy? "weavers.gjitsolution.in" : "www.nekaaramitra.com";

        public static string GetProfileImagePath(string value1, string value2)
        {
            return string.Format("http://{2}/UploadFiles/User/Profile/{0}/{1}.jpg", value1, value2, WebsiteName);
        }

        public static string GetIdProofImagePath(string value1, string value2)
        {
            return string.Format("http://{2}/UploadFiles/User/IdProof/{0}/{1}.jpg", value1, value2, WebsiteName);
        }

        public static string GetReportItemImagePath(string value1)
        {
            if (!string.IsNullOrEmpty(value1) && (value1.Contains("http") || value1.Contains("www")))
            {
                return value1;
            }
            else
            {
                return string.Format("http://{1}/UploadFiles/RepotItem/{0}", value1, GlobalConstants.WebsiteName);
            }
        }
    }
}