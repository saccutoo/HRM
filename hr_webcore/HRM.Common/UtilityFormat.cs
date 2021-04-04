using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Common
{
    class UtilityFormat
    {
        public static string FormatProfileCategory(string profileCategoryName)
        {
            var arr = profileCategoryName.Split('(');
            if (arr.Count() > 1)
            {
                string name1 = arr[0];
                string name2 = arr[1];
                string name3 = name2.Substring(0, 1).ToUpper();
                string name4 = name2.Substring(1);
                name1 = name1 + "<br/>" + "(" + name3 + name4;
                return name1;
            }
            else
            {
                return profileCategoryName;
            }
        }

        public static string SubContent(string str, int leng)
        {
            if (str.Length <= leng)
            {
                return str;
            }
            else
            {
                return str.Substring(0, leng - 1) + "...";
            }
        }

        public static string SubToolTipContent(string str, int leng)
        {
            if (str.Length <= leng)
            {
                return "";
            }
            else
            {
                return str;
            }
        }

        public static string FormatDateToUpdateDB(DateTime dt)
        {
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
        }

        public static string FormatDateToStringShort(DateTime dt)
        {
            if (dt.Year == DateTime.Now.Year)
                return string.Format("{0:dd/MM}", dt);
            else
                return string.Format("{0:dd/MM/yyyy}", dt);
        }

        public static string FormatDateToString(DateTime dt)
        {
            return string.Format("{0:dd/MM/yyyy}", dt);
        }

        public static string FormatHSName(string itemName)
        {
            var arr = itemName.Split('(');
            if (arr.Count() > 1)
            {
                string name1 = arr[0];
                string name2 = arr[1];
                name2 = name2.Replace(")", "");
                return name2 + " - " + name1;
            }
            else
            {
                return itemName;
            }
        }

        public static string FormatDateTimeToString(DateTime dt)
        {
            if (dt.Year == DateTime.Now.Year)
                return string.Format("{0:dd/MM HH:mm}", dt);
            else
                return string.Format("{0:dd/MM/yyyy HH:mm}", dt);
        }

        public static string FormatDateTimeToStringFullMinute(DateTime dt)
        {
            return string.Format("{0:dd/MM/yyyy HH:mm}", dt);
        }

        public static string FormatDateTimeToString(DateTime dt, int type)
        {
            if (type == 1)
            {
                return string.Format("{0:dd/MM/yyyy HH:mm tt}", dt);
            }
            else if (type == 2)
            {
                return string.Format("{0:dd/MM/yyyy - HH:mm}", dt);
            }
            else if (type == 3)
            {
                return string.Format("{0:dd/MM/yyyy - HH:mm}", dt);
            }
            else
            {
                return string.Format("{0:dd/MM/yyyy}", dt);
            }
        }

        public static string FormatMoney(object money)
        {
            try
            {
                if (money is DBNull)
                    return "Chưa có";
                else
                {
                    if (money.ToString().Length > 0)
                    {
                        CultureInfo cul = CultureInfo.GetCultureInfo("en-US");   // try with "en-US"
                        return Convert.ToDouble(money).ToString("#,###", cul.NumberFormat);//.Replace('.', ',');
                    }
                    else
                        return "Chưa có";
                }
            }
            catch
            {
                throw;
            }
        }

        public static double FormatStringToMoney(string sMoney)
        {
            try
            {
                sMoney = sMoney.Replace(",", "");
                return double.Parse(sMoney);
            }
            catch
            {
                throw;
            }
        }
    }
}
