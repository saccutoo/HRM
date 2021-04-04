using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using HRM.DataAccess.DAL;
using System.Net.Http;
using HRM.DataAccess.Entity;
using HRM.Constants;

namespace HRM.Common
{
    public class Global
    {
        public static Sec_User CurrentUser
        {
            get
            {
                if (!HttpContext.Current.Request.IsAuthenticated)
                {
                    return new Sec_User();
                }
                var json = HttpContext.Current.User.Identity.Name;
         //       var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Sec_User>(json);
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Sec_User>(json);
                return obj;
            }
        }

        public static Sec_User LoginSec_User
        {
            get
            {
                var currentAcc = CurrentUser;
                if (currentAcc != null && currentAcc.UserID > 0 && currentAcc.LoginUserId > 0)
                {
                    var Sec_UserService = new Sec_UserDal();
                    var loginUser = Sec_UserService.GetSec_UserById(currentAcc.LoginUserId);
                    return loginUser != null && loginUser.UserID > 0 ? loginUser : null;
                }
                return null;
            }
        }

        #region Đa ngôn ngữ
        private static readonly string _langCookieName = "CurrentLanguage";
        public static int CurrentLanguage
        {
            get
            {
                var c = HttpContext.Current.Request.Cookies[_langCookieName];
                if (c == null || string.IsNullOrEmpty(c.Value))
                {
                    return (int)Constant.numLanguage.VN; //ELanguage.VN;
                }

                var language = ERP.Framework.Utilities.ConvertUtil.ToInt32(c.Value,
                    Constant.numLanguage.VN.GetHashCode());
                return language;
            }
            set
            {
                var c = new HttpCookie(_langCookieName)
                {
                    Value = value.ToString(),
                    Expires = DateTime.Now.AddYears(10)
                };
                HttpContext.Current.Response.Cookies.Add(c);
            }
        }
        #endregion

        public enum EUserStatus
        {
            [Description("Trạng Thái 1,Status 1")]
            Status1 = 1,
            [Description("Trạng Thái 2,Status 2")]
            Status2 = 2,
            [Description("Trạng Thái 3,Status 3")]
            Status3 = 4
        };
    }
}