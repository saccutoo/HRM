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
    public partial class SalaryTypeService : ISalaryTypeService
    {
        ISalaryTypeRepository _salaryTypeRepository;
        private string _dbName;
        public SalaryTypeService(ISalaryTypeRepository salaryTypeRepository)
        {
            this._salaryTypeRepository = salaryTypeRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetSalaryType(BasicParamType param, out int totalRecord)
        {
            var response = _salaryTypeRepository.GetSalaryType(param,out totalRecord);
            return JsonConvert.SerializeObject(response);
        }
        public string SaveSalaryType(SalaryTypeEntity data, List<SalaryElementType> listData, List<SalarytypeMapperType> listSalarytypeMapper)
        {
            var response = _salaryTypeRepository.SaveSalaryType(data, listData, listSalarytypeMapper,_dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetSalaryTypeById(long id)
        {
            var response = _salaryTypeRepository.GetSalaryTypeById(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetSalarytypeMapperBySalaryTypeId(long id)
        {
            var response = _salaryTypeRepository.GetSalarytypeMapperBySalaryTypeId(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string DeleteSalaryType(long id)
        {
            var response = _salaryTypeRepository.DeleteSalaryType(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }

    }
}
