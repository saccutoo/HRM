
using Hrm.Repository.Entity;

namespace Hrm.Service
{
    public partial interface ICustomerService : IBaseService
    {
        string GetAllDataCustomer();
        string SaveDataCustomer(CustomerEntity data);
        string SaveDataUserByDbName(CustomerEntity data);
    }
}
