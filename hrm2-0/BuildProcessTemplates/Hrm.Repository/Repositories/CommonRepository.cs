using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Common.Helpers;
using Hrm.Repository.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Hrm.Repository
{
    public partial class CommonRepository
    {
        protected readonly IDbConnection _db;
        public string connectionString = Config.GetConfigByKey("Hrm");
        public CommonRepository()
        {
            _db = new SqlConnection(connectionString);
        }
        public void Dispose()
        {
            _db.Dispose();
        }
        public HrmResultEntity<T> ListProcedure<T>(string procedure, object oParams = null, bool? useCache = null, string dbName = Constant.MasterDbName)
        {
            if (!CheckPermission(procedure, CurrentUser.UserId, dbName))
            {
                return new HrmResultEntity<T>
                {
                    StatusCode = 401,
                    Success = true,
                    Results = new List<T>()
                };
            }
            if (useCache == null)
            {
                var cache = System.Configuration.ConfigurationManager.AppSettings["UseCache"];
                if (cache != null) useCache = bool.Parse(cache);
            }
            var clientDbName = string.Empty;
            var message = string.Empty;
            var cacheParam = string.Empty;

            if (oParams != null)
            {
                var listParam = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(oParams)).ListParameters;
                if (listParam != null)
                {
                    message = JsonConvert.SerializeObject(listParam);
                    cacheParam = message;
                }
                else
                {
                    message = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(oParams)));
                }
            }
            //LoggerHelper.WriteLog(message, procedure);
            if (!string.IsNullOrEmpty(dbName))
            {
                //sql dependency
                clientDbName = Config.GetConfigByKeyAndDb(Constant.ConnectionStringKey, dbName);
                SqlDependency.Start(clientDbName);
            }
            if (oParams == null) oParams = new DynamicParameters();
            using (_db)
            {
                if (_db.State == ConnectionState.Closed)
                {
                    _db.ConnectionString = connectionString;
                    _db.Open();
                }
                var cacheValueNameOutput = dbName + "-" + procedure + "-" + cacheParam + "-" + CurrentUser.UserId.ToString() + "-output";
                if (useCache.HasValue)
                    //&& !string.IsNullOrEmpty(dbName))
                {
                    IEnumerable<T> result = null;
                    var cacheValueName = dbName + "-" + procedure + "-" + cacheParam + "-" + CurrentUser.UserId.ToString();                    
                    result = HttpRuntime.Cache.Get(cacheValueName) as IEnumerable<T>;
                    if (result == null)
                    {
                        result = _db.Query<T>(procedure, oParams, commandTimeout: 600000, commandType: System.Data.CommandType.StoredProcedure);
                        using (var connection = new SqlConnection(clientDbName))
                        {
                            connection.Open();
                            var command = new SqlCommand("Dependency_" + procedure, connection);
                            command.CommandType = CommandType.StoredProcedure;
                            var dependency = new SqlCacheDependency(command);
                            HttpRuntime.Cache.Insert(cacheValueName, result, dependency);
                            HttpRuntime.Cache.Insert(cacheValueNameOutput, oParams);
                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (Exception)
                            {
                                HttpRuntime.Cache.Remove(cacheValueName);
                                return new HrmResultEntity<T>
                                {
                                    StatusCode = 201,
                                    Success = false,
                                    Results = result.ToList()
                                };
                            }
                        }
                    }
                    return new HrmResultEntity<T>
                    {
                        StatusCode = 200,
                        Success = true,
                        Results = result.ToList()
                    };
                }
                else
                {
                    var result = _db.Query<T>(procedure, oParams, commandTimeout: 600000, commandType: System.Data.CommandType.StoredProcedure);
                    HttpRuntime.Cache.Insert(cacheValueNameOutput, oParams);
                    return new HrmResultEntity<T>
                    {
                        StatusCode = 200,
                        Success = true,
                        Results = result.ToList()
                    };
                }
            }
        }
        public HrmResultEntity<bool> Procedure(string procedure, object oParams = null)
        {
            try
            {
                using (_db)
                {
                    if (_db.State == ConnectionState.Closed)
                    {
                        _db.ConnectionString = connectionString;
                        _db.Open();
                    }
                    _db.Execute(procedure, oParams, commandTimeout: 600000, commandType: CommandType.StoredProcedure);
                }
                return new HrmResultEntity<bool>
                {
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception)
            {
                return new HrmResultEntity<bool>
                {
                    StatusCode = 201,
                    Success = false
                };
            }
        }
        #region Utilities
        public bool CheckPermissionBySp(string menuName, string action, long staffId, string dbName)
        {
            var result = GetAllPermission(staffId, dbName);
            var permission = result.Where(x => x.MenuName == menuName && x.PermissionName == action).FirstOrDefault();
            if (permission != null || CurrentUser.UserName.ToLower() == "admin")
            {
                return true;
            }
            return false;
        }
        public List<RoleMenuPermissionEntity> GetAllPermission(long staffId, string dbName)
        {
            var cacheValueName = dbName + "-Permission-" + staffId.ToString();
            var result = HttpRuntime.Cache.Get(cacheValueName) as List<RoleMenuPermissionEntity>;
            if (result == null)
            {
                var par = new DynamicParameters();
                par.Add("@StaffId", staffId);
                par.Add("@DbName", dbName);
                if (par == null) par = new DynamicParameters();
                using (_db)
                {
                    if (_db.ConnectionString==string.Empty || _db.ConnectionString==null)
                    {
                        _db.ConnectionString = connectionString;
                    }
                    result = _db.Query<RoleMenuPermissionEntity>("System_Full_GetPermission", par, commandTimeout: 600000, commandType: System.Data.CommandType.StoredProcedure).ToList();
                }
                var clientDbName = string.Empty;
                if (!string.IsNullOrEmpty(dbName))
                {
                    //sql dependency
                    clientDbName = Config.GetConfigByKeyAndDb(Constant.ConnectionStringKey, dbName);
                    SqlDependency.Start(clientDbName);

                }
                if (!string.IsNullOrEmpty(clientDbName))
                {
                    using (var connection = new SqlConnection(clientDbName))
                    {
                        connection.Open();
                        var command = new SqlCommand("Dependency_System_Full_GetPermission", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        var dependency = new SqlCacheDependency(command);
                        HttpRuntime.Cache.Insert(cacheValueName, result, dependency);
                        command.ExecuteNonQuery();
                    }
                }
            }
            return result;
        }
        private bool CheckPermission(string proc, long staffId, string dbName)
        {
            if (proc.Contains('_'))
            {
                var arr = proc.Split('_');
                if (arr.Count() > 2)
                {
                    var menuName = arr[0];
                    var action = arr[1];
                    if (action.ToLower() == "get")
                    {
                        action = "View";
                    }
                    if (menuName.ToLower() == "system" || action.ToLower() == "full" || dbName == Constant.MasterDbName) return true;
                    var result = CheckPermissionBySp(menuName, action, staffId, dbName);
                    return result;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public string GetKeyFromParam(object oParams)
        {
            var message = string.Empty;
            if (oParams != null)
            {
                var listParam = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(oParams)).ListParameters;
                if (listParam != null)
                {
                    message = JsonConvert.SerializeObject(listParam);
                }
                else
                {
                    listParam = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(oParams)));
                }
            }
            return message;
        }
        #endregion
    }
}
