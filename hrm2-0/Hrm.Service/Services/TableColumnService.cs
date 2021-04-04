using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Hrm.Service
{
    public partial class TableColumnService : ITableColumnService
    {
        ITableColumnRepository _tableColumnRepository;
        private string _dbName;
        public TableColumnService(ITableColumnRepository tableColumnRepository)
        {
            this._tableColumnRepository = tableColumnRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetTableColumn(string tableName, bool isUpdatingField = false, long columnId = 0, int groupId = 0)
        {
            var response = _tableColumnRepository.GetTableColumn(tableName, _dbName, isUpdatingField, columnId, groupId);
            return JsonConvert.SerializeObject(response);
        }
        public string ExcuteSqlString(string sqlData)
        {
            var response = _tableColumnRepository.ExcuteSqlString(sqlData, _dbName);
            return JsonConvert.SerializeObject(response);
        }
    }
}
