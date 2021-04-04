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
    public partial class SalaryRepository : CommonRepository, ISalaryRepository
    {
        public HrmResultEntity<SalaryEntity> GetSalary(BasicParamType param,int month,int year,long staffId, out string outTotalJson, out int totalRecord)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@Month", month);
            par.Add("@Year", year);
            par.Add("@StaffId", staffId);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            par.Add("@OutTotalJson", "", DbType.String, ParameterDirection.InputOutput);

            var result = ListProcedure<SalaryEntity>("Salary_Get_GetSalary", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-Salary_Get_GetSalary-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
                outTotalJson = par.GetDataOutput<string>("@OutTotalJson");
            }
            else
            {
                totalRecord = 0;
                outTotalJson = string.Empty;
            }
            return result;
        }

        public HrmResultEntity<SalaryEntity> GetSalary(BasicParamType param, int payrollId, int month, int year, long staffId, out string outTotalJson, out int totalRecord)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@PayrollId", payrollId);
            par.Add("@Month", month);
            par.Add("@Year", year);
            par.Add("@StaffId", staffId);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            par.Add("@OutTotalJson", "", DbType.String, ParameterDirection.InputOutput);

            var result = ListProcedure<SalaryEntity>("Salary_Get_GetSalary_1", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-Salary_Get_GetSalary_1-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
                outTotalJson = par.GetDataOutput<string>("@OutTotalJson");
            }
            else
                totalRecord = 0;
            outTotalJson = string.Empty;
            return result;
        }
    }
}
