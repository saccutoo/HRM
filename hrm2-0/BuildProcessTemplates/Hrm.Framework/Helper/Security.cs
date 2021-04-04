using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;

namespace Hrm.Framework.Helper
{
    public static class Security
    {
        public static string SetAuthCookie(HttpContext httpContext, string authenticationTicket, string cookieName, string key, DateTime Expiration)
        {
            var encryptedTicket = Encrypt(authenticationTicket, key);
            var cookie = new HttpCookie(cookieName, encryptedTicket)
            {
                HttpOnly = true,
                Expires = Expiration
            };
            httpContext.Response.Cookies.Add(cookie);
            return encryptedTicket;
        }
        public static void UserSignIn(UserModel accountInfo, HttpContext curentHttpContext, string dbName)
        {
            var token = SetAuthCookie(curentHttpContext, JsonConvert.SerializeObject(accountInfo), dbName, dbName + "_keyauthen", DateTime.Now.AddDays(15));
        }
        public static void Logout(HttpContext httpContext,string dbName)
        {
            var cookie = new HttpCookie(dbName);
            DateTime nowDateTime = DateTime.Now;
            cookie.Expires = nowDateTime.AddDays(-1);
            httpContext.Response.Cookies.Add(cookie);
            httpContext.Request.Cookies.Remove(dbName);
            FormsAuthentication.SignOut();
        }
        public static string Encrypt(string toEncrypt, string key, bool useHashing = true)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string toDecrypt, string key, bool useHashing = true)
        {
            var rt = "";
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                rt = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                //return "";
            }
            return rt;
        }

        public static string ConvertKey(string toDecrypt, string key)
        {
            var encryptedTicket = Decrypt(toDecrypt, key + "_keyauthen");
            return encryptedTicket;
        }
        public static string EncryptKey(string EncryptKey)
        {
            using (MD5 md5hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(EncryptKey));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }        
    }
}