using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Weavers.Models.DbHelper;
using Weavers.Common.Models.Entities;

namespace Weavers.Controllers
{
    public class SubCategoryController : ApiController
    {
        public HttpResponseMessage Get(int categoryID, int languageId)
        {
            var occupationList = new Collection<CodeValueEntity>();
            var entities = tblSubCategory.Get(0);

            if (entities != null)
            {
                entities.ForEach(s => occupationList.Add(new CodeValueEntity { Id=s.ID, Name= s.Name }));
            }

            return Request.CreateResponse(HttpStatusCode.OK, occupationList);
        }
    }
}
