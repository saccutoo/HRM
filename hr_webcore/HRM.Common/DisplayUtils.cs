using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace HRM.Common
{
    public class DisplayUtils
    {
        public static string DisplayMoney(decimal? input)
        {
            if (input == null)
            {
                return SystemMessageConst.systemmessage.DataIsEmpty;
            }
            else
            {
                var result = String.Format("{0:0,0.##}", input);
                return result == "00" ? "0" : result;
            }
        }

        public static string DisplayMoney_Double(double? input)
        {
            if (input == null)
            {
                return SystemMessageConst.systemmessage.DataIsEmpty;
            }
            else
            {
                var result = String.Format("{0:0,0.##}", input);
                return result == "00" ? "0" : result;
            }
        }

        public static string Display_DateTime(DateTime? input)
        {
            if (input == null)
            {
                return SystemMessageConst.systemmessage.DataIsEmpty;
            }
            else
            {
                return input.Value.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }

        public static bool SetSelectedItem(string itemSelected, string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(itemSelected))
            {
                return false;
            }
            string[] lstSelected = input.Split(' ');
            foreach (var item in lstSelected)
            {
                if (itemSelected == item)
                {
                    return true;
                }
            }
            return false;
        }
        

      
//        public static string GetLanguageValues()
//        {
//            string language = HttpContext.Current.Request.Cookies["Language"] != null
//                ? HttpContext.Current.Request.Cookies["Language"].Value
//                : null;
//            return language;
//        }

        public static double GetRateUsd()
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("http://www.vietcombank.com.vn/exchangerates/ExrateXML.aspx");

                XmlNodeList noXml = xml.SelectNodes("/ExrateList/Exrate");
                double sell = 0;
                foreach (XmlNode item in noXml)
                {
                    if (item.Attributes[0].Value == "USD")
                    {
                        double.TryParse(item.Attributes[3].Value, out sell);
                    }
                }
                return sell;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static string DisplayMoney_Usd(decimal money)
        {
            double _money = 0;
            double.TryParse(money.ToString(), out _money);
            double rate = GetRateUsd();
            return DisplayMoney_Double(_money / rate);
        }

//        public static string ReturnMessageBylanguage(string contentVn, string contentEn)
//        {
//            if (GetLanguageValues() == "en")
//            {
//                return contentEn;
//            }
//            return contentVn;
//        }
//
//        public static string DisplayMoneyByLanguage(decimal moneyValues)
//        {
//            if (GetLanguageValues() == "en")
//            {
//                return DisplayMoney_Usd(moneyValues) + " $";
////            return moneyValues.ToString("C3", CultureInfo.CreateSpecificCulture("vi-VN"));
//            }
//            return DisplayMoney(moneyValues) + " VND";
//        }

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

      
      
        public static string RandomPass(string contents, int length)
        {
            var chars = contents;
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
    }
}