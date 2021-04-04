using Hrm.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Repository.Type;
using Hrm.Repository.IRepositories;
using Newtonsoft.Json;
using Hrm.Common;
using Hrm.Repository.Entity;

namespace Hrm.Service.Services
{
    public class WorkingDaySupplementConfigurationExceptionService : IWorkingDaySupplementConfigurationExceptionService
    {
        private string _dbName;
        private long _languageId;
        IWorkingDaySupplementConfigurationExceptionRepository _workingDaySupplementConfigurationExceptionRepository;
        public WorkingDaySupplementConfigurationExceptionService(IWorkingDaySupplementConfigurationExceptionRepository workingDaySupplementConfigurationExceptionRepository)
        {
            this._workingDaySupplementConfigurationExceptionRepository = workingDaySupplementConfigurationExceptionRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
            this._languageId = CurrentUser.LanguageId;
        }

        public string GetApprovedBySaff(string listApprovedSaffs)
        {
            var result = this._workingDaySupplementConfigurationExceptionRepository.GetApprovedBySaff(_dbName, listApprovedSaffs);
            return JsonConvert.SerializeObject(result);
        }

        public string GetApprovedSaff()
        {
            var result = this._workingDaySupplementConfigurationExceptionRepository.GetApprovedSaff(_dbName);
            return JsonConvert.SerializeObject(result);
        }

        public string GetSupplementConfigurationException(BasicParamType param, out int totalRecord)
        {
            var result = this._workingDaySupplementConfigurationExceptionRepository.GetSupplementConfigurationException(param, out totalRecord);
            return JsonConvert.SerializeObject(result);
        }

        public string SaveSupplementConfigurationException(WorkingdaySupplementConfigurationExceptionEntity entity)
        {
            var result = this._workingDaySupplementConfigurationExceptionRepository.SaveSupplementConfigurationException(entity, _dbName);
            return JsonConvert.SerializeObject(result);
        }
    }
}
