using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Weavers.Models;
using Weavers.Common.Models.Entities;

namespace Weavers.Controllers
{
    public class TermsAndConditionController : ApiController
    {

        [Route("api/TermsAndCondition/Get")]
        public CustomResponse Get(string languageId)
        {
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            var htmlFilePath = HttpContext.Current.Server.MapPath("~/Data");
            var fileName = "terms.html";
            switch (languageId)
            {
                case "2":
                    fileName = "terms-hindi.html";
                    break;
                case "3":
                    fileName = "terms-telugu.html";
                    break;
                case "5":
                    fileName = "terms-tamil.html";
                    break;
                case "4":
                    fileName = "terms-kannad.html";
                    break;
                default:
                   fileName = "terms.html";
                    break;
            }

            var fileContent = File.ReadAllText(Path.Combine(htmlFilePath, fileName));
            apiResponse.data = fileContent;
            return apiResponse;
        }

        [Route("api/AboutUs/Get")]
        public CustomResponse GetAboutUs(string languageId)
        {
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            var htmlFilePath = HttpContext.Current.Server.MapPath("~/Data");
            var fileName = "terms.html";
            switch (languageId)
            {
                case "2":
                    fileName = "aboutus-hindi.html";
                    break;
                case "3":
                    fileName = "aboutus-telugu.html";
                    break;
                case "5":
                    fileName = "aboutus-tamil.html";
                    break;
                case "4":
                    fileName = "aboutus-kannad.html";
                    break;
                default:
                    fileName = "aboutus.html";
                    break;
            }

            var fileContent = File.ReadAllText(Path.Combine(htmlFilePath, fileName));
            apiResponse.data = fileContent;
            return apiResponse;
        }
    }
}
