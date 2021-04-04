using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;
using Hrm.Framework.Context;
using System.Web.Hosting;
using System.IO;

namespace Hrm.Framework.Helper
{
    public static class UrlHelpers
    {
        public static string View(string url)
        {
            if (!string.IsNullOrEmpty(CurrentContext.Theme))
            {
                return string.Format("~/Themes/{0}/{1}", CurrentContext.Theme, url.Replace("~/", ""));
            }
            return url;
        }
        public static string Template(string url)
        {
            if (!string.IsNullOrEmpty(CurrentContext.Theme))
            {
                return string.Format("~/Themes/{0}/Views/Shared/Template/{1}", CurrentContext.Theme, url);
            }
            return string.Format("~/Views/Shared/Template/{0}", url);
        }
        public static string FloatingTemplate(string url)
        {
            if (!string.IsNullOrEmpty(CurrentContext.Theme))
            {
                return string.Format("~/Themes/{0}/Views/Shared/Template/FloatingLabel/{1}", CurrentContext.Theme, url);
            }
            return string.Format("~/Views/Shared/Template/FloatingLabel/{0}", url);
        }
        public static string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HostingEnvironment.MapPath(path);
            }

            //not hosted. For example, run in unit tests
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }
        public static string TemplateAdmin(string controller, string url)
        {
            return string.Format("~/Administration/Views/{0}/{1}", controller, url);
        }
    }
}