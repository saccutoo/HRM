using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using Hrm.Service;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial class PermissionService : IPermissionService
    {
        IPermissionRepository _permissionRepository;
        private string _dbName;
        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetPermissionByStaffId(long staffId)
        {
            var response = _permissionRepository.GetPermissionByStaffId(staffId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string SavePermission(StaffRolePermissionEntity staffRolePermisson, List<MenuPermissionType> menuPermissions)
        {
            var response = this._permissionRepository.SavePermission(staffRolePermisson, menuPermissions, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string DeletePermissionByDataId(long dataId, string dataTypeName)
        {
            var response = this._permissionRepository.DeletePermissionByDataId(dataId, dataTypeName, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetPermissionByRoleId(long roleId)
        {
            var response = _permissionRepository.GetPermissionByRoleId(roleId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
    }
}
