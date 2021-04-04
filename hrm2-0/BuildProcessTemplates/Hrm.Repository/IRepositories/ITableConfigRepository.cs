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
    public partial interface ITableConfigRepository
    {
        HrmResultEntity<TableConfigEntity> GetTableConfigDefault(long tableId, string dbName);
        HrmResultEntity<TableConfigEntity> GetUserTableConfig(long tableId, string dbName, long userId);
        HrmResultEntity<TableConfigEntity> GetTableConfigDefaultByTableName(string tableName, string dbName);
        HrmResultEntity<TableConfigEntity> GetUserTableConfigByTableName(string tableName, string dbName, long userId);
        HrmResultEntity<TableConfigEntity> SaveUserTableConfig(string tableName, string dbName, long userId, string queryData, string filterData, string configData, string isFilter);
    }
}
