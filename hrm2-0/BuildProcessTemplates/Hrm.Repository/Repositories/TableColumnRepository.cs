using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hrm.Repository
{
    public partial class TableColumnRepository : CommonRepository, ITableColumnRepository
    {
        public HrmResultEntity<TableColumnEntity> GetTableColumn(string tableName, string dbName, bool isUpdatingField, long columnId, int groupId)
        {
          var par = new DynamicParameters();
            par.Add("@TableName", tableName);
            par.Add("@IsUpdatingField", isUpdatingField);
            par.Add("@ColumnId", columnId);
            par.Add("@GroupId", groupId);
            par.Add("@DbName", dbName);
            return ListProcedure<TableColumnEntity>("System_Get_GetTableColumn", par, useCache: true, dbName: dbName);
        }
        public HrmResultEntity<BaseEntity> ExcuteSqlString(string sqlData, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@SqlData", sqlData);
            par.Add("@DbName", dbName);
            return ListProcedure<BaseEntity>("System_Get_ExcuteSqlString", par, useCache: false, dbName: dbName);
        }

    }
}
