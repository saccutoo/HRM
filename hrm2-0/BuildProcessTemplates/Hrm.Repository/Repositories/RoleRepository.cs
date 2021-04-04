using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using System;
using System.Linq;

namespace Hrm.Repository
{
    public partial class RoleRepository : CommonRepository, IRoleRepository
    {
        public HrmResultEntity<RoleEntity> GetRole(string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            return ListProcedure<RoleEntity>("Role_Get_GetRole", par,dbName: dbName);
        }
        public HrmResultEntity<RoleEntity> GetRoleById(long roleId,string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", roleId);
            par.Add("@DbName", dbName);
            return ListProcedure<RoleEntity>("Role_Get_GetRoleById", par, dbName: dbName);
        }
        public HrmResultEntity<RoleEntity> GetAllRole(string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            return ListProcedure<RoleEntity>("Role_Get_GetAllRole", par, dbName: dbName);
        }
        public HrmResultEntity<RoleEntity> SearchRole(long languageId,string searchKey, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@LanguageId", languageId);
            par.Add("@SearchKey", searchKey);
            par.Add("@DbName", dbName);
            return ListProcedure<RoleEntity>("Role_Get_SearchRole", par, dbName: dbName);
        }
    }
   
}
