using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hrm.Repository
{
    public partial class MenuRepository : CommonRepository, IMenuRepository
    {
        public HrmResultEntity<MenuEntity> GetMenu(string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            return ListProcedure<MenuEntity>("System_Get_GetMenu", par, dbName: dbName);
        }
        public HrmResultEntity<MenuEntity> GetMenuByParentId(long parentId,string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@ParentId", parentId);
            par.Add("@DbName", dbName);
            return ListProcedure<MenuEntity>("System_Get_GetMenuByParentId", par, dbName: dbName);
        }
        public HrmResultEntity<MenuEntity> GetMenuForSearch(string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            return ListProcedure<MenuEntity>("System_Get_GetMenuForSearch", par, dbName: dbName);
        }

        public HrmResultEntity<MenuEntity> GetSettingMenu(string menuName, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@MenuName", menuName);
            par.Add("@DbName", dbName);
            return ListProcedure<MenuEntity>("System_Get_GetSettingMenu", par, dbName: dbName);
        }
    }

}
