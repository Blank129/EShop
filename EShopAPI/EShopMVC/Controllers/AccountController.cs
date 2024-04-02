using EShop.Common;
using EShopMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Account_Login(AccountLoginRequestData requestData)
        {
            var returnData = new LoginResponseData();
            try
            {
                var url = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "";
                var baseUrl = "Account/Account_Login";
                //chuyển data từ Object sang Json
                var bodyJson = JsonConvert.SerializeObject(requestData);

                var result = HttpRequestHelper.SendPostNoToken(url, baseUrl, bodyJson);

                // result trả về dạng string convert ngược lại từ string dạng json sang Object LoginResponseData
                returnData = JsonConvert.DeserializeObject<LoginResponseData>(result);

                if (!string.IsNullOrEmpty(returnData.token))
                {
                    //set session 
                    HttpContext.Session.Add("User_Name", returnData.userName);
                    HttpContext.Session.Add("User_Token", returnData.token);
                    HttpContext.Session.Add("User_IsAdmin", returnData.IsAdmin);

                }
                return Json(returnData, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}