using Hrm.Repository.Entity;
using System.Collections.Generic;
using Hrm.Repository.Type;
namespace Hrm.Repository
{
    public partial interface IChecklistRepository
    {

        HrmResultEntity<ChecklistEntity> GetChecklistById(long checklistId, string dbName);
        HrmResultEntity<ChecklistEntity> GetChecklist(string dbName);
        HrmResultEntity<bool> SaveChecklist(ChecklistEntity checklist, List<ChecklistDetailType> checklistdetail, string dbName);
        HrmResultEntity<ChecklistEntity> DeleteChecklist(long Id, string dbName);
        HrmResultEntity<ChecklistEntity> SearchChecklist(string searchKey, long languageId, string dbName);
    }
}
