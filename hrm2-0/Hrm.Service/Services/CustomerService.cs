using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;

namespace Hrm.Service
{
    public partial class CustomerService : ICustomerService
    {
        ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        public string GetAllDataCustomer()
        {
            var response = this._customerRepository.GetAllDataCustomer();
            return JsonConvert.SerializeObject(response);
        }
        public string SaveDataCustomer(CustomerEntity data )
        {
            var response = this._customerRepository.SaveDataCustomer(data);
            return JsonConvert.SerializeObject(response);
        }
        public string SaveDataUserByDbName(CustomerEntity data)
        {
            var response = this._customerRepository.SaveDataUserByDbName(data);
            return JsonConvert.SerializeObject(response);
        }
    }
}