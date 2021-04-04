using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hrm.Service
{
    public partial class TableConfigService : ITableConfigService
    {
        ITableConfigRepository _tableConfigRepository;
        private string _dbName;
        private long _userId;
        public TableConfigService(ITableConfigRepository tableConfigRepository)
        {
            this._tableConfigRepository = tableConfigRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
                this._userId = CurrentUser.UserId;
            }
        }
        public string GetTableConfig(long tableId)
        {
            var userTableConfigResponse = this._tableConfigRepository.GetUserTableConfig(tableId, _dbName, _userId);
            if (userTableConfigResponse != null && userTableConfigResponse.Results.Count > 0)
            {
                return JsonConvert.SerializeObject(userTableConfigResponse);
            }
            else
            {
                var tableConfigDefaultResponse = this._tableConfigRepository.GetTableConfigDefault(tableId, _dbName);
                return JsonConvert.SerializeObject(tableConfigDefaultResponse);
            }
        }
        public string GetTableConfigByTableName(string tableName)
        {
            var userTableConfigResponse = this._tableConfigRepository.GetUserTableConfigByTableName(tableName, _dbName, _userId);
            if (userTableConfigResponse != null && userTableConfigResponse.Results.Count > 0)
            {
                return JsonConvert.SerializeObject(userTableConfigResponse);
            }
            else
            {
                var tableConfigDefaultResponse = this._tableConfigRepository.GetTableConfigDefaultByTableName(tableName, _dbName);
                return JsonConvert.SerializeObject(tableConfigDefaultResponse);
            }
        }

        public string SaveUserTableConfig(string tableName, string queryData, string filterData, string configData, string isFilter)
        {
            var response = this._tableConfigRepository.SaveUserTableConfig(tableName, _dbName, _userId, queryData, filterData, configData, isFilter);
            return JsonConvert.SerializeObject(response);
        }
    }
}
