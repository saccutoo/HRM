using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hrm.Framework.Controllers;
using Hrm.Core.Infrastructure;
using Hrm.Framework.Context;
using Hrm.Service;
using Hrm.Common;
using Newtonsoft.Json;
using Hrm.Framework.Models;
using Hrm.Framework.Helper;
using System.Web.Security;
using Hrm.Common.Helpers;

namespace Hrm.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var authenticationService = EngineContext.Current.Resolve<IAuthenticationService>();
            var rawUrl = System.Web.HttpContext.Current.Request.RawUrl;
            if (CurrentContext.DbName != Constant.SystemDbName)
            {
                var response = authenticationService.GetCustomer(CurrentContext.DbName);

                var result = JsonConvert.DeserializeObject<HrmResultModel<CustomerModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    var user = result.Results.FirstOrDefault();
                    if (user != null && user.Id != 0)
                    {
                        CurrentContext.Theme = user.Theme;
                    }
                    else
                    {
                        //return register page
                        return RedirectToRoute("registration");
                    }
                }
            }
            if (System.Web.HttpContext.Current.Request.Cookies[CurrentContext.DbName] == null)
            {
                return View();
            }
            else
            {
                var user = Security.ConvertKey(System.Web.HttpContext.Current.Request.Cookies[CurrentContext.DbName].Value, CurrentContext.DbName);
                var obj = JsonConvert.DeserializeObject<UserModel>(user);
                if (obj == null)
                {
                    return View();
                }
                else
                {
                    var response = authenticationService.UserAuthentication(obj.UserName, obj.Password, CurrentContext.DbName);
                    var userResult = JsonConvert.DeserializeObject<HrmResultModel<UserModel>>(response);
                    if (userResult != null)
                    {
                        var userAuthenticated = userResult.Results.FirstOrDefault();
                        if (userAuthenticated != null && userAuthenticated.Id != 0)
                        {
                            CurrentContext.User = userAuthenticated;
                            CurrentUser.UserId = userAuthenticated.UserId;
                            if (CurrentUser.LanguageId == 0)
                            {
                                CurrentUser.LanguageId = userAuthenticated.LanguageId;
                            }
                            CurrentUser.UserName = userAuthenticated.UserName;
                            CurrentUser.RoleId = userAuthenticated.RoleId;
                            //set common
                            return RedirectToRoute("dashboard-detail");
                        }
                        else
                            return View();
                    }
                }
            }
            return View();
        }
        public ActionResult Staff()
        {
            return View();
        }
        public ActionResult PriceList()
        {
            return View();
        }
        public ActionResult Onboarding()
        {
            return View();
        }
        public ActionResult WorkingDay()
        {
            return View();
        }
        public ActionResult Salary()
        {
            return View();
        }
        public ActionResult Educate()
        {
            return View();
        }
        public ActionResult Evaluate()
        {
            return View();
        }
     public bool CheckPermission<T>(HrmResultModel<T> response)
        {
            if (response.StatusCode == StatusCode.AccessDenied)
            {
                return false;
            }
            return true;
        }
    }
}