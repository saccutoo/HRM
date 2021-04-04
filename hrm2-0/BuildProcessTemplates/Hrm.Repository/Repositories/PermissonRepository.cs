using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Hrm.Common.Helpers;
using System.Web;

namespace Hrm.Repository
{
    public partial class PermissonRepository : CommonRepository, IPermissionRepository
    {
        public HrmResultEntity<MenuPermissionEntity> GetPermissionByStaffId(long staffId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@DbName", dbName);
            return ListProcedure<MenuPermissionEntity>("Permission_Get_GetPermissionByStaffId", par, dbName: dbName);
        }
        public HrmResultEntity<bool> SavePermission(StaffRolePermissionEntity staffRolePermisson,List<MenuPermissionType> menuPermissions, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DataId", staffRolePermisson.DataId);
            par.Add("@DataTypeName", staffRolePermisson.DataTypeName);
            par.Add("@Name", staffRolePermisson.Name);
            par.Add("@Description", staffRolePermisson.Description);
            par.Add("@DbName", dbName);
            par.Add("@MenuPermission", menuPermissions.ConvertToUserDefinedDataTable(),DbType.Object);
            return Procedure("Permission_Update_SavePermission", par);
        }
        public HrmResultEntity<bool> DeletePermissionByDataId(long dataId, string dataTypeName, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DataId", dataId);
            par.Add("@DataTypeName", dataTypeName);
            par.Add("@dbName", dbName);
            return Procedure("Permission_Del_DeletePermissionByDataId", par);
        }
        public HrmResultEntity<MenuPermissionEntity> GetPermissionByRoleId(long roleId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@RoleId", roleId);
            par.Add("@DbName", dbName);
            return ListProcedure<MenuPermissionEntity>("Permission_Get_GetPermissionByRoleId", par, dbName: dbName);
        }

    }

}
