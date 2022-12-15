using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Weavers.Models.DbHelper;
using Weavers.Common.Models.Entities;
using System.Web;
using Weavers.Models.Helpers;

namespace Weavers.Controllers
{
    public class NewsController : ApiController
    {
        [System.Web.Http.HttpGet]
        public CustomResponse Get(ReportType type, string UserType= "Handloom")
        {
            var entities=new object();

            switch (type)
            {
                case ReportType.news:
                    entities = tblNews.Get(UserType);
                    break;
                case ReportType.alert:
                    entities = tblAlerts.Get(UserType);
                    break;
                case ReportType.videos:
                    entities = tblNews.GetVideo(UserType);
                    break;
                case ReportType.shops:
                    break;
                default:
                    break;
            }

            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            apiResponse.data = entities;
            return apiResponse;
        }

        [System.Web.Http.HttpGet]
        public CustomResponse Search(string searchtext, string type, string userType="Handloom")
        {
            var parsedType = (ReportType)Enum.Parse(typeof(ReportType), type);

            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };

            if (parsedType == ReportType.shops)
            {
                var result = tblUserBasic.GetUsersByCategory(1, searchtext, userType);
                apiResponse.data =result?.OrderByDescending(s=>s.ID);

            }
            else
            {
                var entities = tblReportItem.SearchItem(searchtext, parsedType, userType);

                apiResponse.data = entities?.OrderByDescending(s=>s.ParsedDateValue);
            }
            return apiResponse;
        }

        [Route("api/News/AddNews")]
        [HttpPost]
        public CustomResponse Save(ReportItemEntity requestObject)
        {
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                if (string.IsNullOrEmpty(requestObject.ShortDescription) || string.IsNullOrEmpty(requestObject.DetailDescription))
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = " Please enter Title/Description" };
                    return apiResponse;
                }



                var imageFolderPath = HttpContext.Current.Server.MapPath("~/UploadFiles/RepotItem");
                string imgPath = "";
                imgPath = UploadImage.SaveReportImage(requestObject.Images, imageFolderPath, requestObject.ReportType.ToString());

                var entity = tblNews.Insert(requestObject, imgPath);

                //var entity1= tblNews.Insert(requestObject,imgPath);

                apiResponse = new CustomResponse { status = HttpStatusCode.OK, message = "Report item saved successfully", data = entity };
            }
            catch (Exception ex)
            {
                apiResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = "unexpected error, please try again" };
                return apiResponse;
            }

            return apiResponse;
        }

        [Route("api/News/Delete")]
        [HttpPost]
        public CustomResponse Save(int newsId)
        {
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {                
                 tblNews.DeleteNews(newsId);

                apiResponse = new CustomResponse { status = HttpStatusCode.OK, message = "Report item deleted successfully" };
            }
            catch (Exception ex)
            {
                apiResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = "unexpected error, please try again" };
                return apiResponse;
            }

            return apiResponse;
        }
    }
}
