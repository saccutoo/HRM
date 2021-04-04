using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Hrm.Service
{
    public partial class AnnualLeaveService : IAnnualLeaveService
    {
        IAnnualLeaveRepository _annualLeaveRepository;
        private string _dbName;
        public AnnualLeaveService(IAnnualLeaveRepository annualLeaveRepository)
        {
            this._annualLeaveRepository = annualLeaveRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetAnnualLeave()
        {
            var response = _annualLeaveRepository.GetAnnualLeave(_dbName);
            return JsonConvert.SerializeObject(response);
        }
    }
}
