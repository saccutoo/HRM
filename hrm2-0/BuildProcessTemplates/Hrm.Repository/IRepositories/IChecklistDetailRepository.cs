using Hrm.Repository.Entity;
using System.Collections.Generic;
namespace Hrm.Repository
{
    public partial interface IChecklistDetailRepository
    {
        HrmResultEntity<ChecklistDetailEntity> GetChecklistDetailByChecklistId(long checklistId, string dbName);
        HrmResultEntity<ChecklistDetailEntity> GetChecklistStaffDetail(long staffId, long checklistId, string dbName);
        HrmResultEntity<ChecklistDetailEntity> GetChecklistDetailByStaffId(long staffId, string dbName);
        HrmResultEntity<ChecklistDetailEntity> SendStaffChecklist(long userId, long staffId, long checklistId, string dbName);
    }
}
