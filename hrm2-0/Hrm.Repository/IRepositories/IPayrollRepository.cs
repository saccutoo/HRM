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
    public partial interface IPayrollRepository
    {
        HrmResultEntity<PayrollEntity> GetPayroll(BasicParamType param, out int totalRecord);
        HrmResultEntity<PayrollEntity> GetPayrollById(long id, BasicParamType param);
        HrmResultEntity<PayrollEntity> SavePayroll(PayrollEntity payroll, BasicParamType param);
        HrmResultEntity<PayrollEntity> DeletePayrollById(long id, string dbName);
    }
}
