using System;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Web.Caching;

namespace HRM.DataAccess.Commons
{
    public class CacheUtils
    {
         public static string StrCn = ERP.Framework.Common.Utils.GetSetting<string>("NovaConnection");


        private const string DatabaseName = "ErpCacheName";

        public static string RenderKeyCache(params object[] Params)
        {
            try
            {
                StackTrace st = new StackTrace();
                var type = st.GetFrame(1).GetMethod().DeclaringType;
                var fullName_space = type.FullName;
                var assemblyName = st.GetFrame(1).GetMethod().DeclaringType.Assembly.GetName().Name + ".dll";
                var method = st.GetFrame(1).GetMethod().Name;
                var strParam = string.Empty;
                foreach (var param in Params)
                {
                    strParam += param + "*";
                }
                return string.Format("{0}#{1}#{2}#{3}", assemblyName, fullName_space, method, strParam);
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Lỗi" + ex.Message);
                return ex.Message;
                   
            }
           
        }
        public static T SqlCacheDependency_Get<T>(string keyCache)
        {
            try
            {
                var objData = HttpRuntime.Cache.Get(keyCache);
                if (objData != null)
                {
                    return (T)objData;
                }
                return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        public static void SqlCacheDependency_Insert(string[] tableName, string keyCache, object data)
        {
            try
            {
                var aggCacheDep = new AggregateCacheDependency();
                var sqlDepGroup = new SqlCacheDependency[tableName.Length];
                for (var i = 0; i < tableName.Length; i++)
                {
                    sqlDepGroup[i] = new SqlCacheDependency(DatabaseName, tableName[i]);
                }
                aggCacheDep.Add(sqlDepGroup);
                if (data != null)
                    HttpRuntime.Cache.Insert(keyCache, data, aggCacheDep);
            }
            catch (Exception ex)
            {
                var strErr = ex.Message;
                var exErr = string.Format("is not enabled for SQL cache notification.", tableName, DatabaseName);
                if (strErr.Contains(exErr))
                {
                    try
                    {
                        SqlCacheDependencyAdmin.EnableNotifications(StrCn);
                        SqlCacheDependencyAdmin.EnableTableForNotifications(StrCn, tableName);
                        var aggCacheDep = new AggregateCacheDependency();
                        var sqlDepGroup = new SqlCacheDependency[tableName.Length];
                        for (int i = 0; i < tableName.Length; i++)
                        {
                            sqlDepGroup[i] = new SqlCacheDependency(DatabaseName, tableName[i]);

                        }
                        aggCacheDep.Add(sqlDepGroup);
                        if (data != null)
                            HttpRuntime.Cache.Insert(keyCache, data, aggCacheDep);
                    }
                    catch (Exception ex1)
                    {
                    }
                }
            }
        }
    }
}