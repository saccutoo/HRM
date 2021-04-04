using Hrm.Repository.Entity;
using System.Collections.Generic;
namespace Hrm.Repository
{
    public partial interface IMenuRepository
    {
        HrmResultEntity<MenuEntity> GetMenu(string dbName);
        HrmResultEntity<MenuEntity> GetMenuByParentId(long parentId, string dbName);
        HrmResultEntity<MenuEntity> GetMenuForSearch(string dbName);
        HrmResultEntity<MenuEntity> GetSettingMenu(string menuName, string dbName);

    }
}
