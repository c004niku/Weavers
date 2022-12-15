using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Weavers.Models.DbHelper;


namespace Weavers.Controllers
{
    public class LanguageController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var entities = tblLanguage.Get();

            return Request.CreateResponse(HttpStatusCode.OK, entities);
        }
       
    }
}
