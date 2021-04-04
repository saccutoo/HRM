using System;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial interface IPermissionService : IBaseService
    {
        string GetPermissionByStaffId(long staffId);
        string SavePermission(StaffRolePermissionEntity staffRolePermisson, List<MenuPermissionType> menuPermissions);
        string DeletePermissionByDataId(long dataId,string dataTypeName);
        string GetPermissionByRoleId(long roleId);

    }
}
