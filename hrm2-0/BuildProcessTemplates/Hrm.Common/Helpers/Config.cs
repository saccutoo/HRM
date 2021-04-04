using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hrm.Common.Helpers
{
    public class Config
    {
        public static string Key = "!@#456NovaonHrm";
        public static string GetConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        public static string GetConfigByKey(string key)
        {
            var sconnectionString = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(sconnectionString))
            {
                var listKey = sconnectionString.Split(';');
                var password = listKey.Where(x => x.ToLower().Contains("password")).FirstOrDefault();
                if (!string.IsNullOrEmpty(password) && password.Contains("="))
                {
                    var encryptedPass = password.Split('=')[1];
                    sconnectionString = sconnectionString.Replace(encryptedPass, DecryptWithHash(encryptedPass, Key));
                }
            }
            return sconnectionString;
        }
        public static string GetConfigByKeyAndDb(string key, string dbName)
        {
            var sconnectionString = GetConfigByKey(key);
            if (!string.IsNullOrEmpty(sconnectionString))
            {
                var listKey = sconnectionString.Split(';');
                var catalog = listKey.Where(x => x.ToLower().Contains("initial catalog")).FirstOrDefault();
                if (!string.IsNullOrEmpty(catalog) && catalog.Contains("="))
                {
                    var dbNameConfig = catalog.Split('=')[1];
                    if (dbName == Constant.MasterDbName)
                        sconnectionString = sconnectionString.Replace(dbNameConfig, dbName);
                    else
                    sconnectionString = sconnectionString.Replace(dbNameConfig, "Hrm_" + dbName);
                }
            }
            return sconnectionString;
        }
        public static string DecryptWithHash(string encoded, string key)
        {
            MACTripleDES des = new MACTripleDES();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));

            string decoded = HttpUtility.UrlDecode(encoded);
            // in the act of url encoding and decoding, plus (valid base64 value) gets replaced with space (invalid base64 value). this reverses that.
            decoded = decoded.Replace(" ", "+");
            string value = Encoding.UTF8.GetString(Convert.FromBase64String(decoded.Split('-')[1]));
            string savedHash = Encoding.UTF8.GetString(Convert.FromBase64String(decoded.Split('-')[0]));
            string calculatedHash = Encoding.UTF8.GetString(des.ComputeHash(Encoding.UTF8.GetBytes(value)));
            if (savedHash != calculatedHash) return null;
            return value;
        }
    }
}
