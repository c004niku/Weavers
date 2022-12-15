using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Weavers.Common.Models;
using Weavers.Common.Models.Entities;

namespace WeaverAdmin.Controllers
{
    public class UserController : Controller
    {
        private string website=GlobalConstants.WebsiteName;
        // GET: User
        public ActionResult Index()
        {
            var model = new UserDetailModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(string.Format("http://{0}", website));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                // client.DefaultRequestHeaders.Add("X-Access-Token", accessToken);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(""));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.GetAsync("api/User/GetAll").Result;
                var webResponse = JsonConvert.DeserializeObject<CustomResponse>(response.Content.ReadAsStringAsync().Result);
                if (webResponse?.status == System.Net.HttpStatusCode.OK)
                {
                    model.UserEntities = JsonConvert.DeserializeObject<ICollection<UserEntity>>(webResponse.data.ToString());
                }
            }
            return View(model);
        }

        public JsonResult Edit()
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}