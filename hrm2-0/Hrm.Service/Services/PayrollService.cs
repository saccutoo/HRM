using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Service
{
    public partial class PayrollService : IPayrollService
    {
        IPayrollRepository _payrollRepository;
        private string _dbName;
        public PayrollService(IPayrollRepository payrollRepository)
        {
            this._payrollRepository = payrollRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }

        public string GetPayroll(BasicParamType param, out int totalRecord)
        {
            var response = this._payrollRepository.GetPayroll(param, out totalRecord);
            return JsonConvert.SerializeObject(response);
        }

        public string GetPayrollById(long id, BasicParamType param)
        {
            var response = this._payrollRepository.GetPayrollById(id, param);
            return JsonConvert.SerializeObject(response);
        }

        public string SavePayroll(PayrollEntity payroll, BasicParamType param)
        {
            var response = this._payrollRepository.SavePayroll(payroll, param);
            return JsonConvert.SerializeObject(response);
        }

        public string DeletePayrollById(long id, string dbName)
        {
            var response = this._payrollRepository.DeletePayrollById(id, dbName);
            return JsonConvert.SerializeObject(response);
        }
    }
}
