using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Hrm.Common.Helpers;
using System.Web;

namespace Hrm.Repository
{
    public partial class OrganizationRepository : CommonRepository, IOrganizationRepository
    {
        public HrmResultEntity<OrganizationEntity> GetOrganization(BasicParamType param, out int totalRecord)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<OrganizationEntity>("Organization_Get_GetOrganization", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-Organization_Get_GetOrganization-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }

        public HrmResultEntity<OrganizationEntity> SaveOrganization(OrganizationEntity data, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@Id", data.Id);
            par.Add("@OrganizationName", data.OrganizationName);
            par.Add("@OrganizationCode", data.OrganizationCode);
            par.Add("@OrderNo", data.OrderNo);
            par.Add("@ParentId", data.ParentId);
            par.Add("@Status", data.Status);
            par.Add("@OrganizationType", data.OrganizationType);
            par.Add("@Email", data.Email);
            par.Add("@Phone", data.Phone);
            par.Add("@Branch", data.Branch);
            par.Add("@CurrencyTypeId", data.CurrencyTypeId);
            par.Add("@CreatedBy", data.CreatedBy);
            par.Add("@UpdatedBy", data.UpdatedBy);
            par.Add("@DbName", param.DbName);
            return ListProcedure<OrganizationEntity>("Organization_Update_SaveOrganization", par);
        }
        public HrmResultEntity<OrganizationEntity> GetOrganizationById(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            par.Add("@UserId", CurrentUser.UserId);
            par.Add("@RoleId", CurrentUser.RoleId);
            return ListProcedure<OrganizationEntity>("Organization_Get_GetOrganizationById", par, dbName: dbName);
        }
        public HrmResultEntity<bool> savePersonnelTransfer(WorkingProcessEntity data, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", data.StaffId);
            par.Add("@OrganizationId", data.OrganizationId);
            par.Add("@Note", data.Note);
            par.Add("@DbName", param.DbName);
            return Procedure("Organization_Update_SavePersonnelTransfer", par);
        }
        public HrmResultEntity<OrganizationEntity> SaveMergerOrganization(OrganizationEntity data, List<ListDataSelectedIdType> listData, BasicParamType param,string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@ListData", listData.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@ToOrganizationaID", data.Id);
            par.Add("@DecisionNo", data.DecisionNo);
            par.Add("@WorkingStatus",MasterDataId.WorkingStatusActive);
            par.Add("@DecisionDate", data.DecisionDate);
            par.Add("@StartDate", data.StartDate);
            par.Add("@EndDate",data.EndDate);
            par.Add("@Note", data.Note);
            par.Add("@OrganizationName", data.OrganizationName);
            par.Add("@OrganizationCode", data.OrganizationCode);
            par.Add("@OrderNo", data.OrderNo);
            par.Add("@ParentId",data.ParentId);
            par.Add("@Status", data.Status);
            par.Add("@OrganizationType", data.OrganizationType);
            par.Add("@Email", data.Email);
            par.Add("@Phone", data.Phone);
            par.Add("@Branch", data.Branch);
            par.Add("@CurrencyTypeId", data.CurrencyTypeId);
            par.Add("@DbName", dbName);
            return ListProcedure<OrganizationEntity>("Organization_Update_Merger", par);
        }
        public HrmResultEntity<OrganizationEntity> GetParentOrganization(BasicParamType param, long id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            return ListProcedure<OrganizationEntity>("Organization_Get_GetParentOrganization", par, dbName: param.DbName);
        }
        public HrmResultEntity<OrganizationEntity> GetAllOrganizationForDropDown(BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            return ListProcedure<OrganizationEntity>("Organization_Get_GetAllOrganizationForDropDown", par, dbName: param.DbName);
        }

        public HrmResultEntity<OrganizationEntity> SearchOrganization(long languageId,string searchKey, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@LanguageId", languageId);
            par.Add("@SearchKey", searchKey);
            par.Add("@DbName", dbName);
            return ListProcedure<OrganizationEntity>("Organization_Get_SearchOrganization", par, dbName:dbName);
        }

        public HrmResultEntity<OrganizationEntity> DeleteOrganization(long id,long userId,long roleId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            par.Add("@UserID", userId);
            par.Add("@RoleID", roleId);
            par.Add("@Result", 0, DbType.Int32, ParameterDirection.InputOutput);
            return ListProcedure<OrganizationEntity>("Organization_Del_DeleteOrganization", par, dbName: dbName);
        }
        public HrmResultEntity<OrganizationEntity> GetAllOrganization(string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            return ListProcedure<OrganizationEntity>("Organization_Get_GetAllOrganization", par, dbName: dbName);
        }
        public HrmResultEntity<OrganizationEntity> CheckOrganizationType(List<ListDataSelectedIdType> listData,string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@listData", listData.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@DbName", dbName);
            return ListProcedure<OrganizationEntity>("Organization_Get_CheckOrganizationType", par, dbName: dbName);
        }
        public HrmResultEntity<OrganizationEntity> GetOrganizationByParentId(long parentId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@ParentId", parentId);
            par.Add("@DbName", dbName);
            return ListProcedure<OrganizationEntity>("Organization_Get_GetOrganizationByParentId", par, dbName: dbName);
        }
    }
}
