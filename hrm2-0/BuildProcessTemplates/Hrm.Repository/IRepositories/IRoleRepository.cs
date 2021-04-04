using Hrm.Common;
using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository
{
    public partial interface IRoleRepository
    {
        HrmResultEntity<RoleEntity> GetRole(string dbName);
        HrmResultEntity<RoleEntity> GetRoleById(long roleId, string dbName);
        HrmResultEntity<RoleEntity> GetAllRole(string dbName);
        HrmResultEntity<RoleEntity> SearchRole(long languageId,string searchKey, string dbName);

    }
}
