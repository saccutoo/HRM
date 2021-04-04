using System;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial interface IChecklistService : IBaseService
    {
        string GetChecklistById(long checklistId);
        string GetChecklist();
        string SaveChecklist(ChecklistEntity checklist, List<ChecklistDetailType> checklistdetail);
        string DeleteChecklist(long Id);
        string SearchChecklist(string searchKey, long languageId);
    }
}
