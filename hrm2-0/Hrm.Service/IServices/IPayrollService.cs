using System;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial interface IPayrollService : IBaseService
    {
        string GetPayroll(BasicParamType param, out int totalRecord);
        string GetPayrollById(long id, BasicParamType param);
        string SavePayroll(PayrollEntity payroll, BasicParamType param);
        string DeletePayrollById(long id, string dbName);
    }
}
