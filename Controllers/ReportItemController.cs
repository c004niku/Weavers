using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Weavers.Models.DbHelper;
using Weavers.Common.Models.Entities;
using Weavers.Models.Helpers;

namespace Weavers.Controllers
{
    public class ReportItemController : ApiController
    {
        // GET: ReportItem
        [System.Web.Http.HttpGet]
        public CustomResponse Get(int userId)
        {
            var entities = tblReportItem.Get(userId);
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            apiResponse.data = entities;
            return apiResponse;
        }

        [Route("api/ReportItem/CreateReport")]
        [HttpPost]
        public CustomResponse CreateReport(ReportItemEntity requestObject)
        {
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                if (requestObject.UserId == 0)
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = "not a valid user" };
                    return apiResponse;
                }

               

                var imageFolderPath = HttpContext.Current.Server.MapPath("~/UploadFiles/RepotItem");
                string imgPath = "";
                imgPath = UploadImage.SaveReportImage(requestObject.Images, imageFolderPath, requestObject.ReportType.ToString());

                var entity = tblReportItem.InserReport(requestObject,imgPath);

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


        [Route("api/ReportItem/DeleteReport")]
        [HttpPost]
        public CustomResponse DeleteReport(int itemId, string type)
        {
            var parsedType = (ReportType)Enum.Parse(typeof(ReportType), type);

            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                if (itemId == 0)
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = "not a valid id" };
                    return apiResponse;
                }
                var isItemDeleted = true;
                if (parsedType == ReportType.shops)
                {
                    tblUserBasic.DeleteUser(new UserBasicEntity { ID = itemId });
                }
                else
                {
                    isItemDeleted = tblReportItem.DeleteReport(itemId);
                }

                //var entity1= tblNews.Insert(requestObject,imgPath);

                apiResponse = new CustomResponse { status = HttpStatusCode.OK, message = isItemDeleted ? "Report item deleted successfully":" this item can not delete " };
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