namespace Hrm.Service
{
    public partial interface IRoleService : IBaseService
    {
        string GetRole();
        string GetRoleById(long roleId);
        string GetAllRole();
        string SearchRole(string searchKey, long languageId);

    }
}
