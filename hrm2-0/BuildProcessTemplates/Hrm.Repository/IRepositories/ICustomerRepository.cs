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
    public partial interface ICustomerRepository
    {
        HrmResultEntity<CustomerEntity> GetAllDataCustomer();
        HrmResultEntity<CustomerEntity> SaveDataCustomer(CustomerEntity data);
        HrmResultEntity<CustomerEntity> SaveDataUserByDbName(CustomerEntity data);
    }
}
