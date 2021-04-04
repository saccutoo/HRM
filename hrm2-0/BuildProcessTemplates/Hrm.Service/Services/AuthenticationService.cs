using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;

namespace Hrm.Service
{
    public partial class AuthenticationService : IAuthenticationService
    {
        IAuthenticationRepository _authenticationRepository;
        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            this._authenticationRepository = authenticationRepository;
        }
        public string UserAuthentication(string userName, string password,string dbName)
        {
            var response = this._authenticationRepository.UserAuthentication(userName, password, dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetCustomer(string dbName)
        {
            var response = this._authenticationRepository.GetCustomer(dbName);
            return JsonConvert.SerializeObject(response);
        }
        public bool ControlPermission(string action, string target)
        {
            return _authenticationRepository.ControlPermission(action, target);
        }
    }
}
