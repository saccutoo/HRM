using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using System;
using System.Linq;

namespace Hrm.Repository
{
   public class CustomerRepository : CommonRepository, ICustomerRepository
    {
        public HrmResultEntity<CustomerEntity> GetAllDataCustomer()
        {
           
            var result = ListProcedure<CustomerEntity>("System_Get_GetAllDataCustomer");
            return result;
        }
        public HrmResultEntity<CustomerEntity> SaveDataCustomer(CustomerEntity data)
        {
            var par = new DynamicParameters();
            par.Add("@Name", data.Name);
            par.Add("@Phone", data.Phone);
            par.Add("@Email", data.Email);
            par.Add("@DbName", data.DbName);
            return ListProcedure<CustomerEntity>("Systerm_Update_SaveDataCustomer", par);
        }
        public HrmResultEntity<CustomerEntity> SaveDataUserByDbName(CustomerEntity data)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", data.DbName);
            par.Add("@UserName", data.UserName);
            par.Add("@Password", data.Password);
            return ListProcedure<CustomerEntity>("Data_User__Update_SaveDataUser", par);
        }
    }
}
