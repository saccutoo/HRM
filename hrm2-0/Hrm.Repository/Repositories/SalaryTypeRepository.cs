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
    public partial class SalaryTypeRepository : CommonRepository, ISalaryTypeRepository
    {
        public HrmResultEntity<SalaryTypeEntity> GetSalaryType(BasicParamType param, out int totalRecord)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<SalaryTypeEntity>("SalaryType_Get_GetSalaryType", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-SalaryType_Get_GetSalaryType-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;
            return result;
        }

        public HrmResultEntity<SalaryTypeEntity> SaveSalaryType(SalaryTypeEntity data, List<SalaryElementType> listData, List<SalarytypeMapperType> listSalarytypeMapper, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", data.Id);
            par.Add("@Name", data.Name);
            par.Add("@StatusId", data.StatusId);
            par.Add("@Description", data.Description);
            par.Add("@IsAutoLock", data.IsAutoLock);
            par.Add("@DayLock", data.DayLock);
            par.Add("@MaximumDay", data.MaximumDay);         
            par.Add("@CreatedBy", data.CreatedBy);
            par.Add("@UpdatedBy", data.UpdatedBy);
            par.Add("@SalaryElementType", listData.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@ListSalarytypeMapper", listSalarytypeMapper.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@DbName", dbName);
            return ListProcedure<SalaryTypeEntity>("SalaryType_Update_SaveSalaryType", par, dbName: dbName);
        }
        public HrmResultEntity<SalaryTypeEntity> GetSalaryTypeById(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);         
            par.Add("@DbName", dbName);
            return ListProcedure<SalaryTypeEntity>("SalaryType_Get_GetSalaryTypeById", par, dbName: dbName);
        }
        public HrmResultEntity<SalarytypeMapperEntity> GetSalarytypeMapperBySalaryTypeId(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            return ListProcedure<SalarytypeMapperEntity>("SalaryType_Get_GetSalarytypeMapperBySalaryTypeId", par, dbName: dbName);
        }
        public HrmResultEntity<SalaryTypeEntity> DeleteSalaryType(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            return ListProcedure<SalaryTypeEntity>("SalaryType_Del_DeleteSalaryType", par, dbName: dbName);
        }
    }
}
