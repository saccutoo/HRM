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
    public partial class SalaryElementService : ISalaryElementService
    {
        ISalaryElementRepository _salaryElemenRepository;
        private string _dbName;
        public SalaryElementService(ISalaryElementRepository salaryElemenRepository)
        {
            this._salaryElemenRepository = salaryElemenRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetSalaryElement(BasicParamType param, out int totalRecord)
        {
            var response = _salaryElemenRepository.GetSalaryElement(param, out totalRecord);
            return JsonConvert.SerializeObject(response);
        }
        public string SaveSalaryElement(SalaryElementEntity data, List<LocalizedDataEntity> listData)
        {
            var response = _salaryElemenRepository.SaveSalaryElement(data, listData,_dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetSalaryElementById(long id)
        {
            var response = _salaryElemenRepository.GetSalaryElementById(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetResultSFormular(string sFormularstr,out string resultFormularstr, out float resultValue)
        {
            var response = _salaryElemenRepository.GetResultSFormular(sFormularstr, _dbName,out resultFormularstr, out resultValue);
            return JsonConvert.SerializeObject(response);
        }
        public string DeleteSalaryElement(long id)
        {
            var response = _salaryElemenRepository.DeleteSalaryElement(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetSalaryElementBySalaryTypeId(long id)
        {
            var response = _salaryElemenRepository.GetSalaryElementBySalaryTypeId(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
    }
}
