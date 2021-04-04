using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Type;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Service
{
    public partial class ConfigInsuranceDetailService : IConfigInsuranceDetailService
    {
        IConfigInsuranceDetailRepository _configInsuranceDetailRepository;
        private string _dbName;
        public ConfigInsuranceDetailService(IConfigInsuranceDetailRepository configInsuranceDetailRepository)
        {
            _configInsuranceDetailRepository = configInsuranceDetailRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetCurrentConfigInsuranceDetail(BasicParamType param, long insuranceId, out int totalRecord)
        {
            var response = this._configInsuranceDetailRepository.GetCurrentConfigInsuranceDetail(param, insuranceId, out totalRecord);
            return JsonConvert.SerializeObject(response);
        }

    }
}
