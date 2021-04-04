using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Hrm.Common.Helpers;
using Hrm.Common;
using System.Web;

namespace Hrm.Repository
{
    public class PayrollRepository : CommonRepository, IPayrollRepository
    {
        public HrmResultEntity<PayrollEntity> GetPayroll(BasicParamType param, out int totalRecord)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<PayrollEntity>("Payroll_Get_GetPayroll", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-Payroll_Get_GetPayroll-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }

        public HrmResultEntity<PayrollEntity> GetPayrollById(long id, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<PayrollEntity>("Payroll_Get_GetPayrollById", par, dbName: param.DbName);
            return result;
        }

        public HrmResultEntity<PayrollEntity> SavePayroll(PayrollEntity payroll, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@Id", payroll.Id);
            par.Add("@SalaryTypeId", payroll.SalaryTypeId);
            par.Add("@Name", payroll.Name);
            par.Add("@Description", payroll.Description);
            par.Add("@MonthYear", payroll.MonthYear);
            par.Add("@Status", payroll.Status);
            par.Add("@PaymentDate", payroll.PaymentDate);
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<PayrollEntity>("Payroll_Update_SavePayroll", par, useCache: false);
            return result;
        }

        public HrmResultEntity<PayrollEntity> DeletePayrollById(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("DbName", dbName);
            var result = ListProcedure<PayrollEntity>("Payroll_Del_DeletePayrollById", par, dbName: dbName);
            return result;
        }
    }
}
