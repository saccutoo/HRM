using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using Hrm.Common.Dapper;
using Hrm.Common.Helpers;
using System.Data;
using System.Web;
using Hrm.Common;
using Hrm.Repository.IRepositories;

namespace Hrm.Repository
{
    public class WorkingDaySupplementConfigurationExceptionRepository : CommonRepository, IWorkingDaySupplementConfigurationExceptionRepository
    {
        public HrmResultEntity<WorkingdaySupplementConfigurationExceptionEntity> GetSupplementConfigurationException(BasicParamType param, out int totalRecord)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<WorkingdaySupplementConfigurationExceptionEntity>("WorkingdaySupplementConfiguration_Get_GetSupplementConfigurationException", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-WorkingdaySupplementConfiguration_Get_GetSupplementConfigurationException-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;
            return result;
        }
        #region Get all users
        public HrmResultEntity<StaffEntity> GetApprovedSaff(string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            return ListProcedure<StaffEntity>("Staff_Get_GetApprovedSaff", par, dbName: dbName);
        }
        #endregion
        #region Load approved by staff following by GetApprovedSaff
        public HrmResultEntity<StaffEntity> GetStaff(string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            return ListProcedure<StaffEntity>("Staff_Get_GetApprovedSaff", par, dbName: dbName);
        }
        #endregion
        public HrmResultEntity<WorkingdaySupplementConfigurationExceptionEntity> SaveSupplementConfigurationException(WorkingdaySupplementConfigurationExceptionEntity entity, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", entity.Id);
            par.Add("@ListStaffId", entity.ListStaffId);
            par.Add("@OrganizationId", entity.OrganizationId);
            par.Add("@PreApprovalStatus", entity.PreApprovalStatus);
            par.Add("@ApprovedBy", entity.ApprovedBy);
            par.Add("@Note", entity.Note);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdaySupplementConfigurationExceptionEntity>("WorkingdaySupplementConfiguration_Update_SaveSupplementConfigurationException", par, dbName: dbName);
            return result;
        }

        public HrmResultEntity<StaffEntity> GetApprovedBySaff(string dbName, string listApprovedSaffs)
        {
            var par = new DynamicParameters();
            par.Add("@ListApprovedSaffs", listApprovedSaffs);
            par.Add("@DbName", dbName);
            var result = ListProcedure<StaffEntity>("Staff_Get_GetApprovedBySaff", par, dbName: dbName);
            return result;
        }
    }
}
