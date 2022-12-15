using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Weavers.Models.DbHelper;


namespace Weavers.Controllers
{
    public class CategoryController : ApiController
    {
       // [Route("api/Category/Get")]
        public HttpResponseMessage Get(int lanugageId)
        {
            var getCategories = tblCategory.Get(lanugageId);

            return Request.CreateResponse(HttpStatusCode.OK, getCategories);
        }
    }
}
