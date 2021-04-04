using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hrm.Repository
{
    public partial class ColumnValidationRepository : CommonRepository, IColumnValidationRepository
    {
        public HrmResultEntity<ColumnValidationEntity> GetColumnValidationByTable(string tableName, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@TableName", tableName);
            par.Add("@DbName", dbName);
            return ListProcedure<ColumnValidationEntity>("System_Full_GetColumnValidationByTable", par, dbName: dbName);
        }
    }

}
