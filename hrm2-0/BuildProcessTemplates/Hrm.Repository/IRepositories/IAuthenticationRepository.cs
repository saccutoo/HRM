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
    public partial interface IAuthenticationRepository
    {
        HrmResultEntity<UserEntity> UserAuthentication(string userName, string password,string dbName);
        HrmResultEntity<CustomerEntity> GetCustomer(string dbName);
        bool ControlPermission(string action, string target);
    }
}
