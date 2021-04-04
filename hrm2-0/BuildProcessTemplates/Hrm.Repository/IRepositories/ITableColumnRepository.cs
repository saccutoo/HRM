using Hrm.Common;
using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository
{
    public partial interface ITableColumnRepository
    {
        HrmResultEntity<TableColumnEntity> GetTableColumn(string tableName, string dbName, bool isUpdatingField, long columnId, int groupId);
        HrmResultEntity<BaseEntity> ExcuteSqlString(string sqlData, string dbName);
    }
}
