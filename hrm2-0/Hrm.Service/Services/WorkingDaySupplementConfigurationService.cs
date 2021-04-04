using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hrm.Repository;
using System.Threading.Tasks;
using Hrm.Repository.Type;
using Hrm.Common;
using Hrm.Repository.Entity;
using Newtonsoft.Json;

namespace Hrm.Service
{
    public class WorkingDaySupplementConfigurationService : IWorkingDaySupplementConfigurationService
    {
        private string _dbName;
        private long _languageId;
        IWorkingDaySupplementConfigurationRepository _workingDaySupplementConfigurationRepository;
        public WorkingDaySupplementConfigurationService(IWorkingDaySupplementConfigurationRepository workingDaySupplementConfigurationRepository)
        {
            this._workingDaySupplementConfigurationRepository = workingDaySupplementConfigurationRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
            _languageId = CurrentUser.LanguageId;
        }

        public string DeleteSupplementConfiguration(long id)
        {
            var response = this._workingDaySupplementConfigurationRepository.DeleteSupplementConfiguration(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }

        public string GetSupplementConfigurationByRoleId(BasicParamType param, out int totalRecord)
        {
            var staffResponse = this._workingDaySupplementConfigurationRepository.GetSupplementConfigurationByRoleId(param, out totalRecord);
            return JsonConvert.SerializeObject(staffResponse);
        }

        public string GetSupplementConfigurationById(long id)
        {
            var response = this._workingDaySupplementConfigurationRepository.GetSupplementConfigurationById(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }

        public string SaveSupplementConfiguration(WorkingdaySupplementConfigurationEntity entity)
        {
            var response = this._workingDaySupplementConfigurationRepository.SaveSupplementConfiguration(entity, _dbName);
            return JsonConvert.SerializeObject(response);
        }
    }
}
