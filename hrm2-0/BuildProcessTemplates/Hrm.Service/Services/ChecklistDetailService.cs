using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial class ChecklistDetailService : IChecklistDetailService
    {
        IChecklistDetailRepository _checklistDetailRepository;
        private string _dbName;
        private long _userId;
        public ChecklistDetailService(IChecklistDetailRepository checklistDetailRepository)
        {
            this._checklistDetailRepository = checklistDetailRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
                this._userId = CurrentUser.UserId;
            }
        }
        public string GetChecklistDetailByChecklistId(long checklistId)
        {
            var response = this._checklistDetailRepository.GetChecklistDetailByChecklistId(checklistId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetChecklistStaffDetail(long checklistId)
        {
            var response = this._checklistDetailRepository.GetChecklistStaffDetail(_userId,checklistId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetChecklistDetailByStaffId(long staffId)
        {
            var response = this._checklistDetailRepository.GetChecklistDetailByStaffId(staffId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string SendStaffChecklist(long staffId, long checklistId)
        {
            var response = this._checklistDetailRepository.SendStaffChecklist(_userId, staffId, checklistId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
    }
}
