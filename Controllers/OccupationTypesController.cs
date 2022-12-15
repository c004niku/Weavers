using Microsoft.Ajax.Utilities;
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
    public class OccupationTypesController : ApiController
    {
        [Route("api/Institution/InstitutionList")]
        public HttpResponseMessage Get(int languageId)
        {
            var list = GetInstitutionList(languageId);


            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

       
        [Route("api/Occupation/OccupationList")]
        [HttpGet]
        public HttpResponseMessage OccupationList(int languageId, string usertype="handloom")
        {
            var list = !string.IsNullOrEmpty(usertype) && usertype.ToLower()=="others"?GetInstitutionList(languageId): GetOccupationList(languageId);

            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        private static Collection<CodeValueEntity> GetOccupationList(int languageId)
        {
            var list = new Collection<CodeValueEntity>();
            var entities = tblSubCategory.Get(0);
            if (entities != null)
            {
                entities.ForEach(s =>
                {
                    var value = s.Name;
                    switch (languageId)
                    {
                        case 1:
                            value = s.Name;
                            break;
                        case 2:
                            value = s.Hindi;
                            break;
                        case 3:
                            value = s.Telugu;
                            break;
                        case 5:
                            value = s.kannad;
                            break;
                        case 4:
                            value = s.Tamil;
                            break;
                        default:
                            value = s.Name;
                            break;
                    }
                    list.Add(new CodeValueEntity { Id = s.ID, Name = s.Name, NameToDisplay=value, IconUrl= "http://weavers.gjitsolution.in/UploadFiles/languageIcon/eng_icon.png" });
                });
            }

            return list;
        }

        private static Collection<CodeValueEntity> GetInstitutionList(int languageId)
        {
            var list =new Collection<CodeValueEntity>();
            var entities = tblOccupationTypes.Get();
            if (entities != null)
            {
                entities.ForEach(s =>
                {
                    var value = s.Name;
                    //switch (languageId)
                    //{
                    //    case 1:
                    //        value = s.Name;
                    //        break;
                    //    case 2:
                    //        value = s.Hindi;
                    //        break;
                    //    case 3:
                    //        value = s.Telugu;
                    //        break;
                    //    case 5:
                    //        value = s.kannad;
                    //        break;
                    //    case 4:
                    //        value = s.Tamil;
                    //        break;
                    //    default:
                    //        value = s.Name;
                    //        break;
                    //}
                    list.Add(new CodeValueEntity { Id = s.ID, Name = s.Name, NameToDisplay = value, IconUrl = "http://weavers.gjitsolution.in/UploadFiles/languageIcon/eng_icon.png" });
                });
            }
            return list;
        }

    }
}
