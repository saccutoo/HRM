namespace Hrm.Service
{
    public partial interface IAuthenticationService : IBaseService
    {
        string UserAuthentication(string userName, string password,string dbName);
        string GetCustomer(string dbName);
        bool ControlPermission(string action, string target);
    }
}
