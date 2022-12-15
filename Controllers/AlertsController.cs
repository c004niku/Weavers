using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Weavers.Models.DbHelper;
using Weavers.Common.Models.Entities;

namespace Weavers.Controllers
{
    public class AlertsController : ApiController
    {
        [System.Web.Http.HttpGet]
        public CustomResponse Get(string UserType = "Handloom")
        {
            var entities = tblAlerts.Get(UserType);
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            apiResponse.data = entities;
            return apiResponse;
        }
    }
}
