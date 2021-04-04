using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hrm.Repository
{
    public partial class TableConfigRepository : CommonRepository, ITableConfigRepository
    {
        public HrmResultEntity<TableConfigEntity> GetTableConfigDefault(long tableId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@TableId", tableId);
            par.Add("@DbName", dbName);
            return ListProcedure<TableConfigEntity>("System_Get_GetTableConfigDefault", par, useCache: true, dbName: dbName);
        }
        public HrmResultEntity<TableConfigEntity> GetUserTableConfig(long tableId, string dbName, long userId)
        {
            var par = new DynamicParameters();
            par.Add("@TableId", tableId);
            par.Add("@DbName", dbName);
            par.Add("@UserId", userId);
           return ListProcedure<TableConfigEntity>("System_Get_GetTableConfig", par, useCache: false, dbName: dbName);
        }
        public HrmResultEntity<TableConfigEntity> GetTableConfigDefaultByTableName(string tableName, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@TableName", tableName);
            par.Add("@DbName", dbName);
            return ListProcedure<TableConfigEntity>("System_Get_GetTableConfigDefaultByTableName", par, useCache: true, dbName: dbName);
        }
        public HrmResultEntity<TableConfigEntity> GetUserTableConfigByTableName(string tableName, string dbName, long userId)
        {
            var par = new DynamicParameters();
            par.Add("@TableName", tableName);
            par.Add("@DbName", dbName);
            par.Add("@UserId", userId);
            return ListProcedure<TableConfigEntity>("System_Get_GetTableConfigByTableName", par, useCache: false, dbName: dbName);
        }
        public HrmResultEntity<TableConfigEntity> SaveUserTableConfig(string tableName, string dbName, long userId, string queryData, string filterData, string configData, string isFilter)
        {
            var par = new DynamicParameters();
            par.Add("@TableName", tableName);
            par.Add("@DbName", dbName);
            par.Add("@UserId", userId);
            par.Add("@QueryData", queryData);
            par.Add("@FilterData", filterData);
            par.Add("@ConfigData", configData);
            par.Add("@IsFilter", isFilter);
            return ListProcedure<TableConfigEntity>("System_Update_SaveUserTableConfig", par, useCache: false, dbName: dbName);
        }

    }
}
