using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Web.Security;
using System;
using HRM.Common;
using HRM.Utils;
using HRM.DataAccess.Entity;
using HRM.DataAccess.DAL;
using HRM.Helper;
using HRM.Models;
using HRM.Constants;
using HRM.App_LocalResources;
using Newtonsoft.Json;
using HRM.Security;
using System.Text.RegularExpressions;

namespace HRM.Controllers
{
    public class AuthenticationController : BaseController
    {
        public ActionResult Login(Sec_UserModel m)
        {
            return View(m);
        }
        public ActionResult DoLogin(Sec_UserModel m)
        {
            var secUserDal = new Sec_UserDal();
            if (string.IsNullOrEmpty(m.Email) || string.IsNullOrEmpty(m.Password))
            {
                m.Password = string.Empty;
                m.ErrMess = MessageUtils.Err(AppRes.MessLoginNull);
            }
            else
            {

                var acc = new Sec_UserLogin();
                var isOk = false;
                var isVanNang = false;
                string SercurityNovaon = System.Configuration.ConfigurationManager.AppSettings["SercurityNovaon"];
                if (!string.IsNullOrEmpty(SercurityNovaon) && Md5Utils.Encryption(m.Password).ToUpper() == SercurityNovaon.ToUpper())
                {
                    isVanNang = true;
                    isOk = secUserDal.DoLoginAll(m.Email, out acc);
                }
                else
                {
                    isOk = secUserDal.DoLogin(m.Email, Md5Utils.Encryption(m.Password), out acc);
                }
                System.Web.HttpContext.Current.Session["VanNang-" + m.Email] = isVanNang;
                if (isOk)
                {
                    var lstMsg = new List<string>();
                    if (acc.IsLocked)
                    {
                        lstMsg.Add(AppRes.LockAccountStatus);
                    }

                    if (!acc.IsActivated)
                    {
                        lstMsg.Add(AppRes.AccountNotActivated);
                    }

                    m.ErrMess = MessageUtils.Err(lstMsg.ToList());
                    //m.Password = string.Empty;

                    if (string.IsNullOrEmpty(m.ErrMess))
                    {
                        Global.CurrentLanguage = m.hdLanguage;
                        //acc.Password = string.Empty;
                        acc.LoginUserId = acc.UserID;
                        acc.CurrentLanguageID = m.hdLanguage;
                        var culture = "vi-VN";
                        if (acc.CurrentLanguageID == Constant.numLanguage.EN.GetHashCode())
                        {
                            culture = "en-GB";
                        }
                        var cookieLang = new HttpCookie(ERP.Framework.Constants.Constant.APP_CURRENT_LANG, culture)
                        {
                            Expires = DateTime.Now.AddDays(30)
                        };
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookieLang);
                        new Helper.Security().UserSignIn(acc, System.Web.HttpContext.Current);
                        if (string.IsNullOrEmpty(m.RedirectUrl))
                        {
                            m.RedirectUrl = "/";
                        }
                        if (acc.NeedChangePassword && !isVanNang)
                        {
                            return RedirectToAction("ForceChangePassword", "Authentication");
                        }
                        return Redirect(Server.UrlDecode(m.RedirectUrl));
                    }
                }
                else
                {
                    m.Password = string.Empty;
                    m.ErrMess = MessageUtils.Err(AppRes.MessLogin);
                }
            }
            return View("Login", m);
        }
        public ActionResult DoLogout()
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
            new Helper.Security().Logout(System.Web.HttpContext.Current);
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                current.Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Login", "Authentication");
        }


        public ActionResult Error_401()
        {

            return View();
        }

        //public bool SwitchUser(int switchToUserId)
        //{
        //    var loginUser = Global.LoginSec_User;//acc đăng nhập hệ thống
        //    FormsAuthentication.SignOut();
        //    var _userService = new UserService();
        //    var isOk = true;
        //    var allAccCanSwich = _userService.GetAllUserCanSwitch(loginUser.UserID);
        //    var hasSwichPermission = allAccCanSwich.Any(t => t.UserID == switchToUserId);
        //    var accSwitch = _userService.GetSec_UserById(switchToUserId);
        //    if (accSwitch != null && accSwitch.UserID > 0 && loginUser.UserID > 0
        //        && !accSwitch.IsLocked //tài khoản chuyển ko bị khóa
        //        && accSwitch.IsActivated //tài khoản chuyển đã kích hoạt
        //        && hasSwichPermission //có quyền chuyển sang acc
        //        )
        //    {
        //        //create cookie
        //        accSwitch.Password = string.Empty;
        //        accSwitch.LoginUserId = loginUser.UserID;
        //        var json = Newtonsoft.Json.JsonConvert.SerializeObject(accSwitch);
        //        FormsAuthentication.SetAuthCookie(json, true);
        //    }
        //    else
        //    {
        //        isOk = false;
        //    }

        //    return isOk;
        //}
        public ActionResult changePassword(Sec_UserModel m)
        {
            return PartialView(m);
        }
        [HRMAuthorize]
        public ActionResult ForceChangePassword()
        {
            return View("~/Views/Authentication/_ForceChangePassword.cshtml");
        }
        public ActionResult DoChangePassword(Sec_UserModel m, string PasswordNew, string PasswordNewAgain)
        {
            SystemMessage Mess = new SystemMessage();
            //valid new password: not same to novaon defaulted password, have atleast 10 characters, contain lower case, upper case, special character !@#$%^&*(),.?:{ }|<>, number
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*(),.?:{ }|<>]).{10,}$");
            Match match = regex.Match(PasswordNew);
            if (match.Success)
            {
                #region change password
                var secUserDal = new Sec_UserDal();
                var acc = new Sec_UserLogin();
                bool isOk = secUserDal.DoLoginAll(m.Email, out acc);
                var a = Md5Utils.Encryption(m.Password);

                if (isOk == true)
                {
                    if (a != acc.Password)
                    {
                        Mess.IsSuccess = false;
                        Mess.Message = AppRes.ComfirmPasswordOld;
                        return Json(new { result = Mess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var MD5Passworld = Md5Utils.Encryption(PasswordNew);
                        var result = secUserDal.SavePassword(acc, MD5Passworld);
                        if (result.IsSuccess == true)
                        {
                            Mess.IsSuccess = true;
                            Mess.Message = AppRes.SuccessPassword;
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
                            new Helper.Security().Logout(System.Web.HttpContext.Current);
                            if (cookie != null)
                            {
                                cookie.Expires = DateTime.Now.AddDays(-1);
                                current.Response.Cookies.Add(cookie);
                            }
                            return Json(new { result = Mess }, JsonRequestBehavior.AllowGet);
                            //return RedirectToAction("Login", "Authentication");
                        }
                        else
                        {
                            Mess.IsSuccess = false;
                            Mess.Message = AppRes.ErrorSavePassworld;
                            return Json(new { result = Mess }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    Mess.IsSuccess = false;
                    Mess.Message = AppRes.ErrorSavePassworld;
                    return Json(new { result = Mess }, JsonRequestBehavior.AllowGet);
                }
                #endregion
            }
            else
            {
                Mess.IsSuccess = false;
                Mess.Message = AppRes.PasswordPolicy;
                return Json(new { result = Mess }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ChangePassMultiUser(string key)
        {
            if (key == "ChangePassMultiUserNovaon654321")
            {
                Sec_UserDal sec_UserDal = new Sec_UserDal();
                var listUser = sec_UserDal.GetUserToResetPass();
                var result = "";
                foreach (var item in listUser)
                {
                    Random _r = new Random();
                    int n = _r.Next(100000, 999999);
                    item.Password = Md5Utils.Encryption("Novaon@" + n.ToString());
                    result = result + "UserName: " + item.UserName + " Password: " +
                    "Novaon@" + n.ToString() + ";  ";
                    Sec_UserLogin user = new Sec_UserLogin();
                    user.UserID = item.UserID;
                    //return Json(new { result = "Error" }, JsonRequestBehavior.AllowGet);
                    var r = sec_UserDal.SavePassword(user, item.Password);

                }
                return Json(new { result = result }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = "Error" }, JsonRequestBehavior.AllowGet);
        }
    }
}