using Hrm.Common;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository
{
    public partial interface IMasterDataRepository
    {
        HrmResultEntity<MasterDataEntity> GetAllMasterData(int pageNumber, int pageSize, int groupId, string filter, int languageCode,string dbName,out int total);
        HrmResultEntity<MasterDataEntity> GetAllMasterDataByGroupId(long groupId, string dbName);
        HrmResultEntity<MasterDataEntity> SaveMasterData(MasterDataEntity data);
        HrmResultEntity<MasterDataEntity> GetAllMasterDataByName(string name, long groupId, string dbName);
        //List<MasterDataEntity> GetMasterData(long languageId, string dbName);
        HrmResultEntity<bool> SaveListMasterData(MasterDataEntity data,List<LocalizedDataEntity> listData);
        HrmResultEntity<MasterDataEntity> GetMasterDataColor(string dbName);
        HrmResultEntity<MasterDataEntity> GetAllMasterDataByGroupName(List<StringType> listName, string dbName);
        HrmResultEntity<MasterDataEntity> GetAllMasterDataByListGroupId(List<LongType> listId, string dbName);
        HrmResultEntity<MasterDataEntity> GetMasterDataById(long id, string dbName);
        HrmResultEntity<MasterDataEntity> DeleteMasterData(long id,string dataType, string dbName);
        HrmResultEntity<MasterDataEntity> SearchMasterData(string searchKey, bool isCategory, long languageId, string dbName);
    }
}
