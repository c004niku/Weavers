using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Weavers.Common.Models.Entities;
using Weavers.Models.DbHelper;
using Weavers.Models.Helpers;

namespace Weavers.Controllers
{
    public class SearchController : ApiController
    {

        [System.Web.Http.HttpGet]
        public CustomResponse Search(string searchtext, int type,string usertype= "Handloom")
        {
           
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };

            var result = tblUserBasic.GetUsersBySubCategory(type, searchtext,usertype);
            apiResponse.data = result?.OrderByDescending(s => s.ID);
            return apiResponse;
        }


        //[System.Web.Http.HttpGet]
        //public CustomResponse FindPeople(string searchtext)
        //{

        //    var apiResponse = new CustomResponse { status = HttpStatusCode.OK };

        //    var result = tblUserBasic.SearchUsers(searchtext);
        //    apiResponse.data = result?.OrderByDescending(s => s.ID);
        //    return apiResponse;
        //}

        [System.Web.Http.HttpGet]
        public CustomResponse FindPeople(string usertype, string searchtext)
        {
            var requestHeader = this.Request.Headers;
            IEnumerable<string> userLatitude;
            IEnumerable<string> userLongitude;
           requestHeader.TryGetValues("userlat", out userLatitude);
             requestHeader.TryGetValues("userlon", out userLongitude);
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };

            var result = tblUserBasic.SearchUsers(searchtext,usertype);
            var getuserList = result.ToList();
            foreach (var userItem in getuserList)
            {
                double latitude1 = 0;
                double latitude2 = 0;
                double longitude1 = 0;
                double longitude2 = 0;
                double.TryParse(userLatitude?.FirstOrDefault(), out latitude1);
                double.TryParse(userItem.GeoLocation?.Latitude, out latitude2);
                double.TryParse(userLongitude?.FirstOrDefault(), out longitude1);
                double.TryParse(userItem.GeoLocation.Longitude, out longitude2);

                userItem.Distance = CalculateDistance.getDistance(latitude1, latitude1, longitude1, longitude2);
            }
            apiResponse.data = getuserList.Where(s => s.User?.UserType==usertype).OrderBy(s=>s.Distance).OrderBy(s=>s.ID);
            return apiResponse;
        }
    }
}
