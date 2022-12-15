using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WeaverAdmin.Extensions;
using WeaverAdmin.Models;
using Weavers.Common.Models;
using Weavers.Common.Models.Entities;

namespace WeaverAdmin.Controllers
{
    public class AlertController : Controller
    {
        // GET: Alert
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
                var response = client.GetAsync("/api/News?type=1&UserType=All").Result;
                var webResponse = JsonConvert.DeserializeObject<CustomResponse>(response.Content.ReadAsStringAsync().Result);
                if (webResponse?.status == System.Net.HttpStatusCode.OK)
                {
                    newsModel.Reports = JsonConvert.DeserializeObject<ICollection<NewsEntity>>(webResponse.data.ToString());
                }
            }
            TempData["WebsiteName"] = GlobalConstants.WebsiteName;
            return View(newsModel);
        }

        [ValidateInput(false)]
        public JsonResult Save(HttpPostedFileBase file)
        {
            var request = this.Request;
            var userEntity = Session["User"] as UserEntity;
            var getImageValue = request.Files[0];
            var imageStream = getImageValue.InputStream;

            var getImageString = StreamExtensions.ConvertToBase64(imageStream);

            var entity = new ReportItem
            {
                ShortDescription = request.Unvalidated.Form["newsTitle"],
                Images = getImageString,
                DetailDescription = request.Unvalidated.Form["newsDescription"],
                ReportType = ReportType.alert,
                ReportedOn = DateTime.Now.ToShortDateString(),
                Range = request.Unvalidated.Form["newsRange"],
                Url = request.Unvalidated.Form["newsUrl"],
                UserId = userEntity.userBasicEntity.ID,
                Action = "Completed",
                Status = ReportStatusType.ACCEPTED
            };

            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(string.Format("http://{0}", GlobalConstants.WebsiteName));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                    // client.DefaultRequestHeaders.Add("X-Access-Token", accessToken);
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(entity));
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = client.PostAsync("/api/News/AddNews", content).Result;
                    var loginResponse = JsonConvert.DeserializeObject<CustomResponse>(response.Content.ReadAsStringAsync().Result);
                    if (loginResponse?.status == System.Net.HttpStatusCode.OK)
                    {
                        var resp = JsonConvert.DeserializeObject<object>(loginResponse.data.ToString());

                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Error while sign-in {loginResponse.message}";
                        // return RedirectToAction("Index", "Login");
                    }
                }
            }
            catch (Exception ex)
            {
                var execption = ex.Message;

            }

            return Json(getImageString);
        }
    }
}