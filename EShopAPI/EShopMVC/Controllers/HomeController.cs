using EShop.Common;
using EShopMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Reflection;

namespace EShopMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public async Task<ActionResult> GrantPermission()
        {
            var list = new List<UserFunction>();
            try
            {
                var session = HttpContext.Session["User_IsAdmin"] ?? "";
                if (session == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                if (Convert.ToInt32(session) != 1)
                {
                    return RedirectToAction("Index", "Home");
                }

                // Xử lí lấy dữ liệu từ bảng UserFunction trên API

                //lấy bảng userFunction hiển thị lên View 
                var url = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "";
                var baseUrl = "UserFunction/GetListUserFunction";

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //GET Method
                    HttpResponseMessage response = await client.GetAsync(baseUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        list = JsonConvert.DeserializeObject<List<UserFunction>>(result);
                    }
                    else
                    {
                        Console.WriteLine("Internal server Error");
                    }                
                }

            }
            catch (Exception)
            {

                throw;
            }
            return View(list);
        }
        public async Task<ActionResult> GetUserFunctionById (int? id)
        {
            var model = new UserFunction();
            try
            {
                var url = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "";
                var baseUrl = "UserFunction/GetUserFunctionById?id=" + id;
                var bodyJson = JsonConvert.SerializeObject(id);

                var result = HttpRequestHelper.SendPostNoToken(url, baseUrl, bodyJson);
                model = JsonConvert.DeserializeObject<UserFunction>(result);
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        public async Task<JsonResult> UserFunction_Update(UserFunction userFunction)
        {
            var returnData = new ReturnData();
            try
            {
                var url = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "";
                var baseUrl = "UserFunction/UserFunction_Update";
                var bodyJson = JsonConvert.SerializeObject(userFunction);

                var result = HttpRequestHelper.SendPostNoToken(url, baseUrl, bodyJson);
                returnData = JsonConvert.DeserializeObject<ReturnData>(result);
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                returnData.ReturnCode = -999;
                returnData.ReturnMessage = "Hệ thống bận";
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
        }
    }
}