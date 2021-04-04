using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using System.Data;
using Hrm.Common.Helpers;
using Hrm.Repository.Type;
using System.Collections.Generic;

namespace Hrm.Repository
{
    public partial class ChecklistRepository : CommonRepository, IChecklistRepository
    {
        public HrmResultEntity<ChecklistEntity> GetChecklistById(long checklistId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", checklistId);
            par.Add("@DbName", dbName);
            var result = ListProcedure<ChecklistEntity>("Checklist_Get_GetChecklistById", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<ChecklistEntity> GetChecklist(string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            return ListProcedure<ChecklistEntity>("Checklist_Get_GetChecklist", par, dbName: dbName);
           
        }
        public HrmResultEntity<bool> SaveChecklist(ChecklistEntity checklist,List<ChecklistDetailType> checklistdetail,string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", checklist.Id);
            par.Add("@Name", checklist.Name);
            par.Add("@Note", checklist.Note);
            par.Add("@CreatedBy", checklist.CreatedBy);
            par.Add("@UpdatedBy", checklist.UpdatedBy);
            par.Add("@DbName", dbName);
            par.Add("@Checklistdetail", checklistdetail.ConvertToUserDefinedDataTable(), DbType.Object);
            return Procedure("Checklist_Update_SaveFullChecklist", par);
        }
        public HrmResultEntity<ChecklistEntity> DeleteChecklist(long Id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id",Id);
            par.Add("@DBName", dbName);
            return ListProcedure<ChecklistEntity>("Checklist_Del_DeleteChecklist", par);
        }
        public HrmResultEntity<ChecklistEntity> SearchChecklist(string searchKey,long languageId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@LanguageId", languageId);
            par.Add("@SearchKey", searchKey);
            par.Add("@DBName", dbName);
            return ListProcedure<ChecklistEntity>("Checklist_Get_SearchChecklist", par, dbName: dbName);
        }
    }

}
