using System;
using Hrm.Repository.Entity;

namespace Hrm.Service
{
    public partial interface IMenuService : IBaseService
    {
        string GetMenu();
        string GetMenuByParentId(long parentId);
        string GetMenuForSearch();
        string GetSettingMenu(string menuName);
    }
}
