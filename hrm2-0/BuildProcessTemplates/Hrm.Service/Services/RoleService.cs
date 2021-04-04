using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;

namespace Hrm.Service
{
    public partial class RoleService : IRoleService
    {
        IRoleRepository _roleRepository;
        private string _dbName;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
            _dbName = CurrentUser.DbName;
        }
        public string GetRole()
        {
            var response = _roleRepository.GetRole(_dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetRoleById(long roleId)
        {
            var response = _roleRepository.GetRoleById(roleId,_dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetAllRole()
        {
            var response = _roleRepository.GetAllRole(_dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string SearchRole(string searchKey,long languageId)
        {
            var response = _roleRepository.SearchRole(languageId, searchKey, _dbName);
            return JsonConvert.SerializeObject(response);
        }
    }
}
