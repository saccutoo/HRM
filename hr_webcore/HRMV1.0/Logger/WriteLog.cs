using Core.Web.Enums;
using HRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using static HRM.Constants.Constant;

namespace HRM.Logger
{
    public class WriteLog : ActionFilterAttribute
    {
        //public int UserId { get; set; } = Global.CurrentUser.UserID;
        public EAction Action { get; set; }
        public string LogStoreProcedure { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext.Current.Session["Controller"] = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            HttpContext.Current.Session["LogStoreProcedure"] = LogStoreProcedure;
            HttpContext.Current.Session["UserAction"] = Action;
            //HttpContext.Current.Session["UserId"] = UserId;
            //var host = Dns.GetHostEntry(Dns.GetHostName());
            //foreach (var ip in host.AddressList)
            //{
            //    if (ip.AddressFamily == AddressFamily.InterNetwork)
            //    {
            //        HttpContext.Current.Session["UserIp"] = ip.ToString();
            //    }
            //}
            HttpContext.Current.Session["UserIp"] = GetVisitorIPAddress();

            base.OnActionExecuting(filterContext);
        }
        private void AccessDeniedReturn()
        {
            HttpContext.Current.Response.Redirect("/AccessDenied/Index");
        }
        public static string GetVisitorIPAddress(bool GetLan = false)
        {
            string visitorIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (String.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
            {
                GetLan = true;
                visitorIPAddress = string.Empty;
            }

            if (GetLan && string.IsNullOrEmpty(visitorIPAddress))
            {
                //This is for Local(LAN) Connected ID Address
                string stringHostName = Dns.GetHostName();
                //Get Ip Host Entry
                IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
                //Get Ip Address From The Ip Host Entry Address List
                IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                try
                {
                    visitorIPAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
                }
                catch
                {
                    try
                    {
                        visitorIPAddress = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        try
                        {
                            arrIpAddress = Dns.GetHostAddresses(stringHostName);
                            visitorIPAddress = arrIpAddress[0].ToString();
                        }
                        catch
                        {
                            visitorIPAddress = "127.0.0.1";
                        }
                    }
                }

            }


            return visitorIPAddress;
        }
    }
}