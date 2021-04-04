using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using Hrm.Service;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Hrm.Service
{
    public partial class MasterDataService : IMasterDataService
    {
        IMasterDataRepository _masterDataRepository;
        private string _dbName;
        public MasterDataService(IMasterDataRepository masterDataRepository)
        {
            this._masterDataRepository = masterDataRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetAllMasterData(int pageNumber, int pageSize, int groupId, string filter, int languageCode,out int total)
        {
            var masterDataResponse = this._masterDataRepository.GetAllMasterData(pageNumber, pageSize, groupId, filter, languageCode, _dbName, out total);
            return JsonConvert.SerializeObject(masterDataResponse);
        }

        public string GetAllMasterDataByGroupId(long groupId)
        {
            var result = new List<MasterDataEntity>();
            var masterDataResponse = this._masterDataRepository.GetAllMasterDataByGroupId(groupId, _dbName);
            return JsonConvert.SerializeObject(masterDataResponse);
        }
        public string GetAllMasterDataByName(string name ,long languageId)
        {
            var result = new List<MasterDataEntity>();
            var masterDataResponse = this._masterDataRepository.GetAllMasterDataByName(name, languageId, _dbName);
            return JsonConvert.SerializeObject(masterDataResponse);
        }
        public string SaveMasterData(MasterDataEntity data)
        {
            var response = this._masterDataRepository.SaveMasterData(data);
            return JsonConvert.SerializeObject(response);
        }
        public string SaveListMasterData(MasterDataEntity data,List<LocalizedDataEntity> listData)
        {
            var response = this._masterDataRepository.SaveListMasterData(data, listData);
            return JsonConvert.SerializeObject(response);
        }
        public string GetMasterDataColor(string key)
        {
            var masterDataResponse = this._masterDataRepository.GetMasterDataColor(_dbName);
            if (masterDataResponse != null && masterDataResponse.Results.Count()>0)
            {
                var resource = masterDataResponse.Results.ToList().FirstOrDefault(x => x.Id.ToString() == key);
                if (resource != null) return resource.Color;
            }
            return Color.Black;
        }
        public string GetAllMasterDataByGroupName(List<StringType> listName)
        {
            var response = this._masterDataRepository.GetAllMasterDataByGroupName(listName, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetAllMasterDataByListGroupId(List<LongType> listId)
        {
            var response = this._masterDataRepository.GetAllMasterDataByListGroupId(listId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetMasterDataById(long id)
        {
            var response = this._masterDataRepository.GetMasterDataById(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string DeleteMasterData(long id,string dataType)
        {
            var response = this._masterDataRepository.DeleteMasterData(id, dataType, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string SearchMasterData(string searchKey, bool isCategory, long languageId)
        {
            var response = this._masterDataRepository.SearchMasterData(searchKey, isCategory, languageId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
    }
}
