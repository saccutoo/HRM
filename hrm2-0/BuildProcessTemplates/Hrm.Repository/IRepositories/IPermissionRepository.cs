using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;
namespace Hrm.Repository
{
    public partial interface IPermissionRepository
    {
        HrmResultEntity<MenuPermissionEntity> GetPermissionByStaffId(long staffId, string dbName);
        HrmResultEntity<bool> SavePermission(StaffRolePermissionEntity staffRolePermisson, List<MenuPermissionType> menuPermissions, string dbName);
        HrmResultEntity<bool> DeletePermissionByDataId(long dataId, string dataTypeName, string dbName);
        HrmResultEntity<MenuPermissionEntity> GetPermissionByRoleId(long roleId, string dbName);

    }
}
