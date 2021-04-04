using Hrm.Common;
using Hrm.Repository;
using Hrm.Service;

namespace Hrm.Service
{
    public partial class BaseService : IBaseService
    {
        IAuthenticationRepository _authenticationRepository;
        string _dbName;
        public BaseService(IAuthenticationRepository authenticationRepository)
        {
            this._authenticationRepository = authenticationRepository;
            this._dbName = CurrentUser.DbName;
        }        
    }
}
