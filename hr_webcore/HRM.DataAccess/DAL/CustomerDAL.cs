using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.DAL
{
    public class CustomerDAL: BaseDal<ADOProvider>
    {
        public List<Customer> GetCustomerContractByUserAndKey(int userId, string key)
        {
            var param = new DynamicParameters();
            param.Add("@UserID", userId);
            param.Add("@Key", key);
            var list = UnitOfWork.Procedure<Customer>("Customer_Gets_ByUserID_and_key", param).ToList();
            return list;
        }
    }
}
