using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Common.Helpers;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Hrm.Repository
{
    public partial class MasterDataRepository : CommonRepository, IMasterDataRepository
    {
        //ham mau cua Xoai
        public HrmResultEntity<MasterDataEntity> GetAllMasterData(int pageNumber, int pageSize, int groupId, string filter, int languageCode, string dbName, out int total)
        {
            var par = new DynamicParameters();
            par.Add("@FilterField", filter);
            par.Add("@OrderBy", string.Empty);
            par.Add("@PageNumber", pageNumber);
            par.Add("@PageSize", pageSize);
            par.Add("@GroupId", groupId);
            par.Add("@LanguageCode", languageCode);
            par.Add("@DBName", dbName);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<MasterDataEntity>("System_Get_MasterData", par, dbName: dbName/*,useCache:true,dbName:dbName*/);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(dbName + "-System_Get_MasterData-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                total = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                total = 0;
            return result;
        }
        public HrmResultEntity<MasterDataEntity> GetAllMasterDataByGroupId(long groupId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@GroupId", groupId);
            par.Add("@DbName", dbName);
            return ListProcedure<MasterDataEntity>("System_Get_GetMasterDataByGroupId", par, dbName: dbName);
        }
        public HrmResultEntity<MasterDataEntity> GetAllMasterDataByName(string name, long languageId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Name", name);
            par.Add("@LanguageId", languageId);
            par.Add("@DbName", dbName);
            return ListProcedure<MasterDataEntity>("System_Get_GetMasterDataByName", par, dbName: dbName);
        }
        public HrmResultEntity<MasterDataEntity> SaveMasterData(MasterDataEntity data)
        {
            var par = new DynamicParameters();
            par.Add("@Id", data.Id);
            par.Add("@GroupId", data.GroupId);
            par.Add("@Name", data.Name);
            par.Add("@Value", data.Value);
            par.Add("@Description", data.Description);
            par.Add("@OrderNo", data.OrderNo);
            par.Add("@LanguageId", data.LanguageId);
            par.Add("@IsActive", data.IsActive);
            par.Add("@IsDeleted", data.IsDeleted);
            par.Add("@DbName", CurrentUser.DbName);
            par.Add("@Color", data.Color);
            return ListProcedure<MasterDataEntity>("System_Update_SaveMasterData", par);
        }
        public HrmResultEntity<bool> SaveListMasterData(MasterDataEntity data, List<LocalizedDataEntity> listData)
        {
            var par = new DynamicParameters();
            par.Add("@Id", data.Id);
            par.Add("@GroupId", data.GroupId);
            par.Add("@Name", data.Name);
            par.Add("@Value", data.Value);
            par.Add("@Description", data.Description);
            par.Add("@OrderNo", data.OrderNo);
            par.Add("@LanguageId", data.LanguageId);
            par.Add("@IsActive", data.IsActive);
            par.Add("@IsDeleted", data.IsDeleted);
            par.Add("@DbName", CurrentUser.DbName);
            par.Add("@Color", data.Color);
            par.Add("@DataType", data.DataType);
            par.Add("@CreatedBy", data.CreatedBy);
            par.Add("@Code", data.Code);
            par.Add("@LocalizedMasterData", listData.ConvertToUserDefinedDataTable(), DbType.Object);
            return Procedure("System_Update_SaveListMasterData", par);
        }
        public HrmResultEntity<MasterDataEntity> GetMasterDataColor(string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            return ListProcedure<MasterDataEntity>("System_Get_GetMasterDataColor", par, dbName: dbName);
        }
        public HrmResultEntity<MasterDataEntity> GetAllMasterDataByGroupName(List<StringType> listName, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@ListName", listName.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@DbName", dbName);
            return ListProcedure<MasterDataEntity>("System_Get_GetAllMasterDataByGroupName", par, dbName: dbName);
        }
        public HrmResultEntity<MasterDataEntity> GetAllMasterDataByListGroupId(List<LongType> listId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@ListId", listId.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@DbName", dbName);
            return ListProcedure<MasterDataEntity>("System_Get_GetAllMasterDataByListGroupId", par, dbName: dbName);
        }
        public HrmResultEntity<MasterDataEntity> GetMasterDataById(long id,string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            return ListProcedure<MasterDataEntity>("System_Get_GetMasterDataById", par, dbName: dbName);
        }
        public HrmResultEntity<MasterDataEntity> DeleteMasterData(long id,string dataType,string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DataType", dataType);
            par.Add("@DbName", dbName);
            return ListProcedure<MasterDataEntity>("System_Del_DeleteMasterData", par);
        }
        public HrmResultEntity<MasterDataEntity> SearchMasterData(string searchKey, bool isCategory, long languageId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@IsCategory", isCategory);
            par.Add("@LanguageId", languageId);
            par.Add("@SearchKey", searchKey);
            par.Add("@DbName", dbName);
            return ListProcedure<MasterDataEntity>("System_Get_SearchMasterData", par);
        }
    }
}
