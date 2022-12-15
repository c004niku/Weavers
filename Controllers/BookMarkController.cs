using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Weavers.Common.Models.Entities;
using Weavers.Models.DbHelper;

namespace Weavers.Controllers
{
    public class BookMarkController : ApiController
    {

        [Route("api/BookMark")]
        [HttpPost]
        public CustomResponse CreateBookmark(BookMarkItemEntity requestObject)
        {
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                if (requestObject.ItemId == 0)
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = "not a valid item to bookmark" };
                    return apiResponse;
                }

                tblNews.BookMarkItem(requestObject);

                apiResponse = new CustomResponse { status = HttpStatusCode.OK, message = "Item bookmarked successfully", data=requestObject };
            }
            catch (Exception ex)
            {
                apiResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = "unexpected error, please try again" };
                return apiResponse;
            }

            return apiResponse;
        }

        [Route("api/BookMark")]
        [HttpGet]
        public CustomResponse GetBookmark(string userId)
        {
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                var BookMarkList = new BookMarkList { BookMarkItems= new Collection<BookMarkItem>() {
                    new BookMarkItem {
                        Type= ReportType.news, 
                        BookMarkItems=new List<CodeValueEntity>() { new CodeValueEntity { Id=12, Name="Test News" } } },
                  new BookMarkItem {
                        Type= ReportType.sales,
                        BookMarkItems=new List<CodeValueEntity>() { new CodeValueEntity { Id=22, Name="Test Sales" } } },
                  new BookMarkItem {
                        Type= ReportType.shops,
                        BookMarkItems=new List<CodeValueEntity>() { new CodeValueEntity { Id=223, Name="Shop book mark item" } } }} };


                BookMarkList= tblNews.GetBookMarkItems(userId);


                apiResponse = new CustomResponse { status = HttpStatusCode.OK, message = "bookmark items", data = BookMarkList };
            }
            catch (Exception ex)
            {
                apiResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = "unexpected error, please try again" };
                return apiResponse;
            }

            return apiResponse;
        }

        [Route("api/BookMarkCount")]
        [HttpGet]
        public CustomResponse GetBookmarkCount(string userId)
        {
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                var BookMarkList = tblNews.BookMarkItemsCount(userId);

                if (!BookMarkList.ContainsKey(ReportType.alert.ToString()))
                {
                    BookMarkList.Add(ReportType.alert.ToString(), "0");
                }
                if (!BookMarkList.ContainsKey(ReportType.news.ToString()))
                {
                    BookMarkList.Add(ReportType.news.ToString(), "0");
                }
                if (!BookMarkList.ContainsKey(ReportType.member.ToString()))
                {
                    BookMarkList.Add(ReportType.member.ToString(), "0");
                }
                if (!BookMarkList.ContainsKey(ReportType.videos.ToString()))
                {
                    BookMarkList.Add(ReportType.videos.ToString(), "0");
                }
                if (!BookMarkList.ContainsKey(ReportType.shops.ToString()))
                {
                    BookMarkList.Add(ReportType.shops.ToString(), "0");
                }
                if (!BookMarkList.ContainsKey(ReportType.sales.ToString()))
                {
                    BookMarkList.Add(ReportType.sales.ToString(), "0");
                }

                apiResponse = new CustomResponse { status = HttpStatusCode.OK, message = "bookmark count items", data = BookMarkList };
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
