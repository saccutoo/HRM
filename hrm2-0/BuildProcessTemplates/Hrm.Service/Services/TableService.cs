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
    public partial class TableService : ITableService
    {
        ITableRepository _tableRepository;
        private string _dbName;
        public TableService(ITableRepository tableRepository)
        {
            this._tableRepository = tableRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetTable(string tableName)
        {
            var response = _tableRepository.GetTable(tableName, _dbName);
            return JsonConvert.SerializeObject(response);
        }
    }
}
