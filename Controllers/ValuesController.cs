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
    public class ValuesController : ApiController
    {
        // GET api/values
        [Route("api/Values/GetWorkingStatus")]
        [HttpGet]
        public CustomResponse GetWorkingStatus()
        {
            var response = new CustomResponse { status=HttpStatusCode.OK, message="Working Status List" };
            response.data = new Collection<CodeValueEntity>() {
                new CodeValueEntity { Id=1, Name="Working" }, 
                new CodeValueEntity { Id=2, Name="Not Working" }, 
                new CodeValueEntity { Id=3, Name="Open" }, 
                new CodeValueEntity { Id=4, Name="Close" }, 
                new CodeValueEntity { Id=5, Name="Leave" }, 
                new CodeValueEntity { Id=6, Name="Holiday" }, 
                new CodeValueEntity { Id=7, Name="Active" }, 
                new CodeValueEntity { Id=8, Name="In Active" }, 
            }.OrderBy(s=>s.Id);
            return response;
        }

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
