using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Weavers.Common.Models.Entities;
using Weavers.Models.Helpers;

namespace Weavers.Models.DbHelper
{
    public class ShopsController : ApiController
    {
        [System.Web.Http.HttpGet]
        public CustomResponse GetShopList()
        {
            var requestHeader = this.Request.Headers;
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            //var entities = tblUserBasic.GetUsersByCategory(1);
            IEnumerable<string> userLatitude;
            IEnumerable<string> userLongitude;
            requestHeader.TryGetValues("userlat", out userLatitude);
            requestHeader.TryGetValues("userlon", out userLongitude);

            var result = tblUserBasic.SearchUsers("", "");
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
            apiResponse.data = getuserList.Where(s => s.User.UserType== "Others" && s.Category.Id!=7).OrderBy(s => s.Distance).OrderBy(s => s.ID);

           
            return apiResponse;
        }
    }
}
