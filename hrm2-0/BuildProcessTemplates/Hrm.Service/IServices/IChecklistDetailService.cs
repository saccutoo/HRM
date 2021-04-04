using System;
using Hrm.Repository.Entity;

namespace Hrm.Service
{
    public partial interface IChecklistDetailService : IBaseService
    {
        string GetChecklistDetailByChecklistId(long checklistId);
        string GetChecklistStaffDetail(long checklistId);
        string GetChecklistDetailByStaffId(long staffId);
        string SendStaffChecklist(long staffId, long checklistId);
    }
}
