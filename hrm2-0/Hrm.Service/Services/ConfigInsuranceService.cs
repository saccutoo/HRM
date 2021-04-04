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
    public partial class ConfigInsuranceService : IConfigInsuranceService
    {
        IConfigInsuranceRepository _configInsuranceRepository;
        private string _dbName;
        public ConfigInsuranceService(IConfigInsuranceRepository configInsuranceRepository)
        {
            _configInsuranceRepository = configInsuranceRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetCurrentConfigInsurance(BasicParamType param)
        {
            var response = this._configInsuranceRepository.GetCurrentConfigInsurance(param);
            return JsonConvert.SerializeObject(response);
        }

        string IConfigInsuranceService.GetCurrentConfigInsurance(BasicParamType param)
        {
            throw new NotImplementedException();
        }

        string IConfigInsuranceService.GetCurrentConfigInsuranceDetailByInsurance(BasicParamType param, int insuranceId)
        {
            throw new NotImplementedException();
        }
        //public string GetCurrentConfigInsuranceDetailByInsurance(BasicParamType param, int insuranceId);
        //{
        //    var response = this._configInsuranceRepository.GetCurrentConfigInsuranceDetailByInsurance(param, insuranceId);
        //    return JsonConvert.SerializeObject(response);
        //}
    }
}
