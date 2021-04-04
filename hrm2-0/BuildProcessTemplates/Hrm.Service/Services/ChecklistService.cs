using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;
using System.Collections.Generic;
using Hrm.Repository.Type;
namespace Hrm.Service
{
    public partial class ChecklistService : IChecklistService
    {
        IChecklistRepository _checklistRepository;
        private string _dbName;
        public ChecklistService(IChecklistRepository checklistRepository)
        {
            this._checklistRepository = checklistRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetChecklistById(long checklistId)
        {
            var response = this._checklistRepository.GetChecklistById(checklistId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetChecklist()
        {
            var response = this._checklistRepository.GetChecklist(_dbName);
            if (response != null)
                return JsonConvert.SerializeObject(response);
            return JsonConvert.SerializeObject(new List<ChecklistEntity>());
        }
        public string SaveChecklist(ChecklistEntity checklist, List<Hrm.Repository.Type.ChecklistDetailType> checklistdetail)
        {
            var response = this._checklistRepository.SaveChecklist(checklist, checklistdetail, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string DeleteChecklist(long Id)
        {
            var response = this._checklistRepository.DeleteChecklist(Id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string SearchChecklist(string searchKey, long languageId)
        {
            var response = this._checklistRepository.SearchChecklist(searchKey, languageId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
    }
}
