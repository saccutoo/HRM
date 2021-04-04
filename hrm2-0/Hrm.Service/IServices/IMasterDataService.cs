using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial interface IMasterDataService : IBaseService
    {
        string GetAllMasterData(int pageNumber, int pageSize, int groupId, string filter, int languageCode,out int total);
        string GetAllMasterDataByGroupId(long groupId);
        string SaveMasterData(MasterDataEntity data);
        string GetAllMasterDataByName(string name,long languageId);
        //string GetMasterData(string key);
        string SaveListMasterData(MasterDataEntity data, List<LocalizedDataEntity> listData);
        //string GetColorMasterData(string key);
        string GetMasterDataColor(string key);
        string GetAllMasterDataByGroupName(List<StringType> listName);
        string GetAllMasterDataByListGroupId(List<LongType> listId);
        string GetMasterDataById(long id);
        string DeleteMasterData(long id,string dataType);
        string SearchMasterData(string searchKey, bool isCategory, long languageId);
    }
}
