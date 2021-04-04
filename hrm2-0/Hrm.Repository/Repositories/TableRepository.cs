using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hrm.Repository
{
    public partial class TableRepository : CommonRepository, ITableRepository
    {
        public HrmResultEntity<TableEntity> GetTable(string tableName,string dbName)
        {
          var par = new DynamicParameters();
            par.Add("@TableName", tableName);
            par.Add("@DbName", dbName);
            return ListProcedure<TableEntity>("System_Get_GetTable", par, useCache: true, dbName: dbName);
        }
        public HrmResultEntity<TableColumnEntity> GetColumnByTableName(string tableName, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@TableName", tableName);
            par.Add("@DbName", dbName);
            return ListProcedure<TableColumnEntity>("System_Get_GetColumnByTableName", par, useCache: true, dbName: dbName);
        }
    }
}
