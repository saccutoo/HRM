using Hrm.Repository;
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
using Hrm.Common;
using System.Web;

namespace Hrm.Repository
{
    public class WorkingDaySupplementConfigurationRepository : CommonRepository, IWorkingDaySupplementConfigurationRepository
    {
        public HrmResultEntity<WorkingdaySupplementConfigurationEntity> DeleteSupplementConfiguration(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdaySupplementConfigurationEntity>("WorkingdaySupplementConfiguration_Del_DeleteSupplementConfiguration", par, dbName: dbName);
            return result;
        }

        public HrmResultEntity<WorkingdaySupplementConfigurationEntity> GetSupplementConfigurationByRoleId(BasicParamType param, out int totalRecord)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<WorkingdaySupplementConfigurationEntity>("Workingday_Get_GetSupplementConfigurationByRoleId", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-Workingday_Get_GetSupplementConfigurationByRoleId-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;
            return result;
        }

        public HrmResultEntity<WorkingdaySupplementConfigurationEntity> GetSupplementConfigurationById(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdaySupplementConfigurationEntity>("WorkingdaySupplementConfiguration_Get_GetSupplementConfigurationById", par, dbName: dbName);
            return result;
        }

        public HrmResultEntity<WorkingdaySupplementConfigurationEntity> SaveSupplementConfiguration(WorkingdaySupplementConfigurationEntity entity, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", entity.Id);
            par.Add("@RequesterId", entity.RequesterId);
            par.Add("@RequesterTypeId", 270);
            par.Add("@CurrentRequestStatusId", entity.PrevStatus);
            par.Add("@NextRequestStatusId", entity.NextStatus);
            par.Add("@RequestActionId", entity.Action);
            par.Add("@IsLastStep", entity.IsLastStep);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdaySupplementConfigurationEntity>("WorkingdaySupplementConfiguration_Update_SaveSupplementConfiguration", par, dbName: dbName);
            return result;
        }
        
    }
}
