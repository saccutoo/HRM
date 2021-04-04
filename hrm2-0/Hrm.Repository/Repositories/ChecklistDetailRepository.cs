using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hrm.Repository
{
    public partial class ChecklistDetailRepository : CommonRepository, IChecklistDetailRepository
    {
        public HrmResultEntity<ChecklistDetailEntity> GetChecklistDetailByChecklistId(long checklistId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@ChecklistId", checklistId);
            par.Add("@DbName", dbName);
            var result = ListProcedure<ChecklistDetailEntity>("Checklist_Get_GetChecklistDetailByChecklistId", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<ChecklistDetailEntity> GetChecklistStaffDetail(long staffId,long checklistId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@ChecklistId", checklistId);
            par.Add("@DbName", dbName);
            var result = ListProcedure<ChecklistDetailEntity>("Checklist_Get_GetChecklistStaffDetail", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<ChecklistDetailEntity> GetChecklistDetailByStaffId(long staffId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@DbName", dbName);
            var result = ListProcedure<ChecklistDetailEntity>("Checklist_Get_GetChecklistDetailByStaffId", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<ChecklistDetailEntity> SendStaffChecklist(long userId,long staffId,long checklistId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@UserId", userId);
            par.Add("@ChecklistId", checklistId);
            par.Add("@DbName", dbName);
            var result = ListProcedure<ChecklistDetailEntity>("Staff_Update_SendStaffChecklist", par, dbName: dbName);
            return result;
        }
    }

}
