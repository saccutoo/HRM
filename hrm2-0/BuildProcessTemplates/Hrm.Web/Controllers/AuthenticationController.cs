using Hrm.Common;
using Hrm.Service;
using Hrm.Framework.Models;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System;
using Hrm.Framework.Controllers;
using static Hrm.Common.Constant;
using Hrm.Framework.Context;
using Hrm.Framework;
using System.Web.Security;
using Hrm.Framework.Helper;
using Hrm.Core.Infrastructure;
using System.Collections.Generic;
using System.Dynamic;
using Hrm.Web.ViewModels;
using Hrm.Framework.Helpers;
using System.Data.SqlClient;
using Hrm.Common.Helpers;
using System.IO;
using System.Linq;
using Hrm.Repository.Entity;

namespace Hrm.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private IAuthenticationService _authenticationService;
        private ILocalizationService _localizationService;
        private ICustomerService _customerService;

        public AuthenticationController(IAuthenticationService authenticationService, ILocalizationService localizationService, ICustomerService customerService)
        {
            this._authenticationService = authenticationService;
            this._localizationService = localizationService;
            this._customerService = customerService;
        }
        public ActionResult Index(AuthenticationModel m)
        {
            if (Request.Cookies[CurrentUser.DbName] != null)
            {
                var value = Request.Cookies[CurrentUser.DbName].Value;
                string strConvertKey = Security.ConvertKey(value, CurrentUser.DbName);
                var user = JsonConvert.DeserializeObject<UserModel>(strConvertKey);
                return LoginControl(user);
            }
            //var m = new AuthenticationModel();
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var response = localizationService.GetLanguage();
            var result = JsonConvert.DeserializeObject<HrmResultModel<LanguageModel>>(response);
            m.Languages = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
            if (m.User==null || m.User.ErrMess==string.Empty)
            {
                m.User = new UserModel();
            }          
            return View(m);
        }
        [HttpPost]
        public ActionResult Login(UserModel m)
        {
            return LoginControl(m, false);
        }
        public ActionResult LoginControl(UserModel m, bool isEncrypted = true)
        {
            var model = new AuthenticationModel();
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var responseL = localizationService.GetLanguage();
            var resultL = JsonConvert.DeserializeObject<HrmResultModel<LanguageModel>>(responseL);
            model.Languages = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultL.Results));
            model.User = m;
            if (string.IsNullOrEmpty(m.UserName) || string.IsNullOrEmpty(m.Password))
            {
                m.Password = string.Empty;
                m.ErrMess = this._localizationService.GetBaseResources("Authentication.Message.PleaseInputInformation");
            }
            
            else
            {
                var encryptedPassword = string.Empty;
                if (!isEncrypted)
                {
                    encryptedPassword = Security.EncryptKey(m.Password);
                }
                else
                    encryptedPassword = m.Password;
                var response = this._authenticationService.UserAuthentication(m.UserName, encryptedPassword, CurrentContext.DbName);
                if (response != null)
                {
                    var user = JsonConvert.DeserializeObject<HrmResultModel<UserModel>>(response);
                    if (user != null && user.Results.Count > 0)
                    {
                        CurrentContext.User = user.Results[0];

                        //set common
                        CurrentUser.LanguageId = m.LanguageId;
                        CurrentUser.DbName = CurrentContext.DbName;
                        CurrentUser.UserId = CurrentContext.User.UserId;
                        CurrentUser.UserName = CurrentContext.User.UserName;
                        CurrentUser.RoleId = CurrentContext.User.RoleId;
                        CurrentUser.CurrencyId = CurrentContext.User.CurrencyId;
                        Security.UserSignIn(user.Results[0], System.Web.HttpContext.Current, CurrentContext.DbName);
                        return RedirectToAction("Detail", "Dashboard");
                    }
                    else
                    {
                        m.ErrMess = this._localizationService.GetBaseResources("Authentication.Message.UserOrPasswordIncorrect");
                        return PartialView("Index", model);
                    }
                }
                else
                {
                    m.Password = string.Empty;
                    m.ErrMess = this._localizationService.GetBaseResources("Authentication.Message.UserNotExistIn") + CurrentContext.DbName;
                    return View("Index", model);
                }
            }
            return View("Index", model);
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //Clear session
            var current = System.Web.HttpContext.Current;
            current.Session.Clear();
            current.Session.Abandon();
            //Clears out Session
            current.Response.Cookies.Clear();
            // clear authentication cookie
            current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            current.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            HttpCookie cookie = current.Request.Cookies[FormsAuthentication.FormsCookieName];
            Security.Logout(System.Web.HttpContext.Current, CurrentContext.DbName);
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                current.Response.Cookies.Add(cookie);
            }
            var m = new AuthenticationModel();
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var response = localizationService.GetLanguage();
            var result = JsonConvert.DeserializeObject<HrmResultModel<LanguageModel>>(response);
            m.Languages = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
            m.User = new UserModel();
            return View("Index", m);

        }

        public ActionResult ChangePassword()
        {
            return View(UrlHelpers.Template("_ChangePassword.cshtml"));
        }
        public ActionResult Registration()
        {
            return PartialView(UrlHelpers.View("~/Views/Authentication/_Registration.cshtml"));
        }
        public ActionResult SaveRegistration(CustomerModel model)
        {
            //0 bt 1 truc tiep 2 nhân sự
            var validations = new List<ValidationModel>();
            validations.AddRange(ValidationHelper.Validation(model, "", -1, null, SystemDbName));
            var listCustomerResult = this._customerService.GetAllDataCustomer();
            var listCustomerDetail =  JsonConvert.DeserializeObject<HrmResultModel<CustomerModel>>(listCustomerResult);
            if (validations.Count > 0)
            {
                return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
            }
            if (listCustomerDetail.Results.FirstOrDefault().Id != 0)
            {
                if (listCustomerDetail.Results.Any(item => item.DbName.ToLower() == model.DbName.ToLower()))
                {
                    // show validation name Db
                }
                model.Password = Security.EncryptKey(model.Password);
                var customerEntity = MapperHelper.Map<CustomerModel, CustomerEntity>(model);
                var saveCustomerRepo = this._customerService.SaveDataCustomer(customerEntity);
                var saveCustomerResult = JsonConvert.DeserializeObject<HrmResultModel<CustomerModel>>(saveCustomerRepo);
                if(!saveCustomerResult.Results.FirstOrDefault().IsDuplicate)
                {
                    CreateDatabase(model.DbName);

                    var addUserResult = this._customerService.SaveDataUserByDbName(customerEntity);
                    return Json(new { isDuplicate = saveCustomerResult.Results.FirstOrDefault().IsDuplicate }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { IsDuplicate = true }, JsonRequestBehavior.AllowGet);
        }
        private string CreateDatabase(string databaseName)
        {
            try
            {
                var connectionString = Config.GetConfigByKey("Hrm");
                //parse database name
                var builder = new SqlConnectionStringBuilder(connectionString);
                //now create connection string to 'master' dabatase. It always exists.
                builder.InitialCatalog = "master";
                var masterCatalogConnectionString = builder.ToString();
                string query = string.Format("IF NOT EXISTS(SELECT name FROM master.dbo.sysdatabases WHERE ([name ] = '[Hrm_{0}]' OR name = '[Hrm_{0}]' )) BEGIN CREATE DATABASE [Hrm_{0}] END ", databaseName);
                //query += Environment.NewLine + "GO";
                //query += Environment.NewLine + string.Format("ALTER DATABASE [Hrm_{0}] SET ENABLE_BROKER ", databaseName);
                using (var conn = new SqlConnection(masterCatalogConnectionString))
                {
                    conn.Open();
                    using (var command = new SqlCommand(query, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                ExecuteSqlFile("~/App_Data/Client.sql", masterCatalogConnectionString, databaseName);
                return string.Empty;
            }
            catch (Exception ex)
            {
                var connectionString = Config.GetConfigByKey("Hrm");
                //parse database name
                var builder = new SqlConnectionStringBuilder(connectionString);
                //now create connection string to 'master' dabatase. It always exists.
                builder.InitialCatalog = "master";
                var masterCatalogConnectionString = builder.ToString();
                string query = string.Format("IF EXISTS(SELECT name FROM master.dbo.sysdatabases WHERE ([name ] = '[Hrm_{0}]' OR name = '[Hrm_{0}]' )) BEGIN DROP DATABASE [Hrm_{0}] END  ", databaseName);
                //query += Environment.NewLine + "GO";
                //query += Environment.NewLine + string.Format("ALTER DATABASE [Hrm_{0}] SET ENABLE_BROKER ", databaseName);
                using (var conn = new SqlConnection(masterCatalogConnectionString))
                {
                    conn.Open();
                    using (var command = new SqlCommand(query, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                return ex.ToString();
            }
        }

        protected virtual void ExecuteSqlFile(string path, string connectionString, string databaseName)
        {
            var statements = new List<string>();

            using (var stream = System.IO.File.OpenRead(UrlHelpers.MapPath(path)))
            using (var reader = new StreamReader(stream))
            {
                string statement;
                while ((statement = ReadNextStatementFromStream(reader)) != null)
                    statements.Add(statement);
            }
            var builder = new SqlConnectionStringBuilder(connectionString);
            //now create connection string to 'master' dabatase. It always exists.
            builder.InitialCatalog = "Hrm_" + databaseName;
            foreach (string stmt in statements)
            {
                using (var conn = new SqlConnection(builder.ToString()))
                {
                    conn.Open();
                    using (var command = new SqlCommand(stmt, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        protected virtual string ReadNextStatementFromStream(StreamReader reader)
        {
            var sb = new StringBuilder();
            while (true)
            {
                var lineOfText = reader.ReadLine();
                if (lineOfText == null)
                {
                    if (sb.Length > 0)
                        return sb.ToString();

                    return null;
                }

                if (lineOfText.TrimEnd().ToUpper() == "GO")
                    break;
                sb.Append(lineOfText + Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}