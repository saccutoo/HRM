using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using System;
using System.Linq;

namespace Hrm.Repository
{
    public partial class AuthenticationRepository : CommonRepository, IAuthenticationRepository
    {
        public HrmResultEntity<UserEntity> UserAuthentication(string userName, string password,string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@UserName", userName);
            par.Add("@Password", password);
            par.Add("@DbName", dbName);
            var result = ListProcedure<UserEntity>("System_Full_Authentication", par,useCache:false,dbName: dbName);
            return result;
        }
        public HrmResultEntity<CustomerEntity> GetCustomer(string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            var result = ListProcedure<CustomerEntity>("System_Get_GetCustomer", par,useCache:true);
            return result;
        }
        public bool ControlPermission(string action, string target)
        {
            return CheckPermissionBySp(target, action, CurrentUser.UserId, CurrentUser.DbName);
        }
    }
   
}
