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
    public class VideoController : Controller
    {
        // GET: Video
        public ActionResult Index()
        {
            var newsModel = new NewsModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(string.Format("http://{0}", GlobalConstants.WebsiteName));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                // client.DefaultRequestHeaders.Add("X-Access-Token", accessToken);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(""));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.GetAsync("/api/News?type=2&UserType=All").Result;
                var webResponse = JsonConvert.DeserializeObject<CustomResponse>(response.Content.ReadAsStringAsync().Result);
                if (webResponse?.status == System.Net.HttpStatusCode.OK)
                {
                    newsModel.Reports = JsonConvert.DeserializeObject<ICollection<NewsEntity>>(webResponse.data.ToString());
                }
            }
            TempData["WebsiteName"] = GlobalConstants.WebsiteName;
            return View(newsModel);
        }
    }
}