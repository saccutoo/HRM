using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using Hrm.Service;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Hrm.Service
{
    public partial class SalaryService : ISalaryService
    {
        ISalaryRepository _salaryRepository;
        private string _dbName;
        public SalaryService(ISalaryRepository salaryRepository)
        {
            this._salaryRepository = salaryRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetSalary(BasicParamType param, int month, int year, long staffId, out string outTotalJson, out int totalRecord)
        {
            var response = _salaryRepository.GetSalary(param, month, year, staffId, out outTotalJson, out totalRecord);
            return JsonConvert.SerializeObject(response);
        }
    }
}
