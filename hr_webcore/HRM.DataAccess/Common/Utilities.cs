using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;

namespace HRM.Common
{
    public static class Utilities
    {
        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string GenerateRandomString(int length)
        {
            var result = new StringBuilder();
            ////const string src = "abcdefghiklmnopqrstuvwxyz1234567890-_";
            //const string src = "1234567890";
            //var seed = GetRandomSeed();
            //var rnd = new Random(seed);
            //length.Times(() => result.Append(src[rnd.Next(src.Length - 1)]));
            return result.ToString();
        }

        public static DateTime ParseDate(string date, string fomat)
        {
            System.Globalization.DateTimeFormatInfo dateFormatProvider = new System.Globalization.DateTimeFormatInfo();
            dateFormatProvider.ShortDatePattern = fomat;

            return DateTime.Parse(date, dateFormatProvider);
        }

        /// <summary>
        /// Gets the random seed.
        /// </summary>
        /// <returns></returns>
        public static int GetRandomSeed()
        {
            var rng = new RNGCryptoServiceProvider();
            var randomBytes = new byte[4];
            rng.GetBytes(randomBytes);
            return (randomBytes[0] & 0x7f) << 24 |
                   randomBytes[1] << 16 |
                   randomBytes[2] << 8 |
                   randomBytes[3];
        }

        /// <summary>
        /// Generates some the random bytes.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static byte[] GenerateRandomBytes(int length)
        {
            var result = new byte[length];
            RandomNumberGenerator.Create().GetBytes(result);
            return result;
        }

        /// <summary>
        /// Hash the input password
        /// </summary>
        /// <param name="pwd">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        public static byte[] GetInputPasswordHash(string pwd, byte[] salt)
        {
            var inputPwdBytes = Encoding.UTF8.GetBytes(pwd);
            var inputPwdHasher = new Rfc2898DeriveBytes(inputPwdBytes, salt, Constants.PasswordDerivationIteration);
            return inputPwdHasher.GetBytes(Constants.PasswordBytesLength);
        }

        ///<summary>
        /// Generate notify number
        ///</summary>
        ///<param name="date">The date</param>
        ///<returns></returns>
        public static string GenerateNotifyNumber(DateTime date)
        {
            return string.Format("{0:MMddHHmmssfff/yyyy}/TB-BKAVTVAN", date);
        }

        /// <authors> 
        /// - [huynd@bkav.com] - [20151015]
        /// </ authors > 
        /// <summary>
        ///  <para>Đăng ký sử dụng d</para>
        /// </summary>
        /// <remarks>
        /// Client gửi dữ liệu thông tin người dùng theo cấu trúc của class: CustomerInfo dạng chuỗi xml
        /// </remarks>
        /// <param name="YourClassObject">[xml thông tin của người dùng]</param>
        /// <exception cref="InvalidCastException">[Chú thích lỗi...]</exception>
        /// <returns>
        ///  Trả về license key miễn phí cho KH
        /// </returns>
        public static string CreateXML(Object YourClassObject)
        {
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
                                                      // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(YourClassObject.GetType());
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, YourClassObject);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }
        public static string CreateXMLUtf8(Object obj)
        {
            using (var sw = new Utf8StringWriter())
            {
                using (var xw = XmlWriter.Create(sw, new XmlWriterSettings { Indent = true }))
                {
                    xw.WriteStartDocument(true); // that bool parameter is called "standalone"
                    var namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("inv", "");
                    var xmlSerializer = new XmlSerializer(obj.GetType());
                    xmlSerializer.Serialize(xw, obj, namespaces);
                    return sw.ToString();
                }
            }
        }
        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }
        /// <authors> 
        /// - [ThinhTD@bkav.com] - [20161216]
        /// </ authors > 
        /// <summary>
        ///  <para>Đăng ký sử dụng d</para>
        /// </summary>
        /// <remarks>
        /// Parse một file XML sang một đối tượng
        /// </remarks>
        /// <param name="XmlFilename">[Đường dẫn đến File XML]</param>
        /// <exception cref="errMsg">[Chú thích lỗi...]</exception>
        /// <returns>
        ///  Trả về đối tượng T
        /// </returns>
        public static T DeserializeXMLFileToObject<T>(string XmlFilePath, ref string errMsg)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(XmlFilePath)) return default(T);

            try
            {
                StreamReader xmlStream = new StreamReader(XmlFilePath);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(xmlStream);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return returnObject;
        }

        /// <authors> 
        /// - [ThinhTD@bkav.com] - [20161216]
        /// </ authors > 
        /// <summary>
        ///  <para>Đăng ký sử dụng d</para>
        /// </summary>
        /// <remarks>
        /// Parse một nội dung XML sang đối tượng
        /// </remarks>
        /// <param name="xml">[Nội dung XML]</param>
        /// <exception cref="errMsg">[Chú thích lỗi...]</exception>
        /// <returns>
        ///  Trả về đối tượng T
        /// </returns>
        public static T DeserializeXmlContentToObject<T>(String xml, ref string errMsg)
        {
            T returnedXmlClass = default(T);
            try
            {
                using (TextReader reader = new StringReader(xml))
                {
                    try
                    {
                        returnedXmlClass = (T)new XmlSerializer(typeof(T)).Deserialize(reader);
                    }
                    catch (InvalidOperationException)
                    {
                        //String passed is not XML, simply return defaultXmlClass
                        errMsg = "File không đúng định dạng XML";
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return returnedXmlClass;
        }

        public static string GetAppConfig(string keyName)
        {
            return ConfigurationManager.AppSettings[keyName];
        }
    }
}
