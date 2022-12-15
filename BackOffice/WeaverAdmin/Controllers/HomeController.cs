using Microsoft.Extensions.Logging;
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
    public class HomeController : Controller
    {
        private UserEntity userEntity { get; set; }
        public ActionResult Index()
        {
            userEntity=Session["User"] as UserEntity;
            if (userEntity == null)
            {
                TempData["ErrorMessage"] = "Please sign-in with your credential";
                return RedirectToAction("Index", "Login");
            }
            return View(new UserAdminEntity { userBasicEntity =userEntity.userBasicEntity });
        }

        public ActionResult UserLogin()
        {
            var formData = Request.Form;

            var commonValues = new UserLoginEntity
            {
                UserName = formData["UserName"],
                Password = formData["Password"]
            };
            if (commonValues.UserName != "Admin")
            {
                TempData["ErrorMessage"] = $"User {commonValues.UserName} not authorize to login as admin";
                return RedirectToAction("Index", "Login");
            }
            try
            {
                //HttpContent content = new StringContent(JsonConvert.SerializeObject(commonValues));
                //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //var client1 = new HttpClient
                //{
                //    BaseAddress = new Uri(string.Format("http://{0}", GlobalConstants.WebsiteName))

                //}.PostAsync("/api/User/Login", new StringContent(JsonConvert.SerializeObject(commonValues))).Result;




                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(string.Format("http://{0}", GlobalConstants.WebsiteName));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                    // client.DefaultRequestHeaders.Add("X-Access-Token", accessToken);
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(commonValues));
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = client.PostAsync("/api/User/Login", content).Result;
                    var loginResponse = JsonConvert.DeserializeObject<CustomResponse>(response.Content.ReadAsStringAsync().Result);
                    if (loginResponse?.status == System.Net.HttpStatusCode.OK)
                    {
                        userEntity = JsonConvert.DeserializeObject<UserEntity>(loginResponse.data.ToString());
                        Session["User"] = userEntity;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Error while sign-in {loginResponse.message}";
                        return RedirectToAction("Index","Login");
                    }
                }
            }
            catch (Exception ex)
            {
                var execption = ex.Message;
               
            }
            

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}