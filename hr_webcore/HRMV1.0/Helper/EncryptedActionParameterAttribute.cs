using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EncryptedActionParameterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            Dictionary<string, object> decryptedParameters = new Dictionary<string, object>();
            if (HttpContext.Current.Request.QueryString.Get("q") != null)
            {
                string encryptedQueryString = HttpContext.Current.Request.QueryString.Get("q");
                string decrptedString = Decrypt(encryptedQueryString.ToString());
                string[] paramsArrs = decrptedString.Split('&');

                for (int i = 0; i < paramsArrs.Length; i++)
                {
                    string[] paramArr = paramsArrs[i].Split('=');
                    decryptedParameters.Add(paramArr[0], paramArr[1]);
                }
            }
            var allParams = filterContext.ActionDescriptor.GetParameters(); // 1 bien
            var dicParamsType = new Dictionary<string, Type>();
            foreach(var p in allParams)
            {
                dicParamsType.Add(p.ParameterName, p.ParameterType);
            }

            // map biến decrypted trên url với các tham số trong Action
            foreach (var decryptedParam in decryptedParameters)
            {
                // nếu biến decrypted trùng với 1 tham số Action
                if (dicParamsType.Keys.Any(t => t.Equals(decryptedParam.Key)))
                {
                    var typeOfParamsOrg = dicParamsType[decryptedParam.Key];
                    //Process Nullable Type
                    var typeOfParams = Nullable.GetUnderlyingType(typeOfParamsOrg) ?? typeOfParamsOrg;
                    var paramObjValue = decryptedParameters[decryptedParam.Key];
                    if (paramObjValue != null)
                    {
                        filterContext.ActionParameters[decryptedParam.Key] = Convert.ChangeType(paramObjValue, typeOfParams);
                    }
                }
            }
            
            base.OnActionExecuting(filterContext);

        }

        private string Decrypt(string encryptedText)
        {
            //string key = ConstKey.EncryptingKey;
            //byte[] DecryptKey = { };
            //byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            //byte[] inputByte = new byte[encryptedText.Length];

            //DecryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            //DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //inputByte = Convert.FromBase64String(encryptedText);
            //MemoryStream ms = new MemoryStream();
            //CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write);
            //cs.Write(inputByte, 0, inputByte.Length);
            //cs.FlushFinalBlock();
            //System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            //return encoding.GetString(ms.ToArray());
            return HRM.Utils.Encrypt.DecryptWithHash(encryptedText);
        }
    }
}