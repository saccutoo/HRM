using Hrm.Framework.Context;
using Hrm.Framework.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Hrm.Service;
using System.Web;
using System;
using Hrm.Common;
using Hrm.Framework;
using System.Web.Security;
using static Hrm.Common.Constant;
using Hrm.Framework.Helper;
using System.Collections.Generic;
using System.Linq;
using Hrm.Common.Helpers;
using Hrm.Core.Infrastructure;
using System.Net.Mail;
using System.IO;
using Hrm.Repository.Entity;

namespace Hrm.Framework.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }
        public ActionResult CreateTable(TableViewModel tableConfig)
        {
            var tableData = tableConfig.TableData;
            //sorting
            var fields = tableConfig.Fields.Where(x => x.Sorting != Sorting.Default);
            if (fields.Count() > 0)
            {
                foreach (var field in fields)
                {
                    tableData = FunctionHelpers.Sort(tableData, field.FieldName, field.Sorting);
                }
            }
            return View(UrlHelpers.Template("_Table.cshtml"), tableConfig);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var authenticationService = EngineContext.Current.Resolve<IAuthenticationService>();
            var rawUrl = System.Web.HttpContext.Current.Request.RawUrl;
            if (CurrentContext.DbName != SystemDbName)
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
                        filterContext.Result = new RedirectResult(Config.GetConfig("UrlScheme") + Config.GetConfig(WebBaseUrl) + "/registration");
                        goto Finish;
                    }
                }
            }
            CurrentUser.DbName = CurrentContext.DbName;
            if (filterContext.ActionDescriptor.ActionName != "Login")
            {
                if (System.Web.HttpContext.Current.Request.Cookies[CurrentContext.DbName] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new
                    System.Web.Routing.RouteValueDictionary(new { controller = "Authentication", action = "Index", RedirectUrl = rawUrl }));
                }
                else
                {
                    var user = Security.ConvertKey(System.Web.HttpContext.Current.Request.Cookies[CurrentContext.DbName].Value, CurrentContext.DbName);
                    var obj = JsonConvert.DeserializeObject<UserModel>(user);
                    if (obj == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(new
                        System.Web.Routing.RouteValueDictionary(new { controller = "Authentication", action = "Index", RedirectUrl = rawUrl }));
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
                                goto Finish;
                            }
                            else
                                goto Logout;
                        }
                    }
                }
            }
            Logout:
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
            filterContext.Result = new RedirectToRouteResult(new
            System.Web.Routing.RouteValueDictionary(new { controller = "Authentication", action = "Index", RedirectUrl = rawUrl }));
            Finish:
            base.OnActionExecuting(filterContext);
        }
        public List<LanguageModel> GetLanguage()
        {
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var response = localizationService.GetLanguage();
            var result = JsonConvert.DeserializeObject<HrmResultModel<LanguageModel>>(response);
            if (!CheckPermission(result))
            {
                //return to Access Denied
            }
            else
            {
                return result.Results;
            }
            return new List<LanguageModel>();
        }
        public bool CheckPermission<T>(HrmResultModel<T> response)
        {
            if (response.StatusCode == StatusCode.AccessDenied)
            {
                return false;
            }
            return true;
        }
        public int SendMail(string subject, string body, List<string> toMail, List<string> cc, List<string> bcc, string from, string replyTo, List<AttachmentJs> attachments)
        {
            //var to = new List<string>();
            //toMail.Add(replyTo);
            var lstAttach = new List<Attachment>();
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    var dir = System.Configuration.ConfigurationManager.AppSettings["UploadFolder"].Replace("\\", "/");
                    var path = Path.Combine(dir, attachment.Realname);
                    if (System.IO.File.Exists(path))
                    {
                        lstAttach.Add(new Attachment(new MemoryStream(System.IO.File.ReadAllBytes(path)), attachment.Name));
                    }
                }
            }
            var resultCode = EmailHelper.SendMail(subject, body, from, toMail, cc, bcc, lstAttach);
            return resultCode;
        }
        public int SaveFile(IEnumerable<HttpPostedFileBase> attachment, string dataType, long recordId)
        {
            var fileUpload = UploadFileHelper.SaveFile(attachment);
            if(fileUpload!= null && fileUpload.Count > 0)
            {
                foreach(var file in fileUpload)
                {
                    var attachmentModel = new AttachmentModel()
                    {
                        FileName = file.Realname,
                        FileSize = file.Size,
                        DisplayFileName = file.Name,
                        RecordId = recordId,
                        DataType = dataType,
                        FileExtension =file.Extension
                    };
                    var attachmentEntity = MapperHelper.Map<AttachmentModel, AttachmentEntity>(attachmentModel);
                    var attachmentService = EngineContext.Current.Resolve<IAttachmentService>();
                    attachmentService.SaveAttachment(attachmentEntity);
                }
            }
            var result = fileUpload.Count;
            return result;
        }
        public bool RemoveFile( string realName, long id)
        {
            var attachmentService = EngineContext.Current.Resolve<IAttachmentService>();
            var response = attachmentService.DeleteAttackmentById(id);
            var result = new HrmResultModel<AttachmentModel>();
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<AttachmentModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    UploadFileHelper.RemoveFileUpload(realName);
                    if (result.Results.Count > 0)
                    {
                        result.Success = false;
                    }
                    else
                    {
                        result.Success = true;

                    }
                }
            }
            return result.Success;
        }
    }
}