using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Hrm.Service
{
    public partial class MenuService : IMenuService
    {
        IMenuRepository _menuRepository;
        private string _dbName;
        public MenuService(IMenuRepository menuRepository)
        {
            this._menuRepository = menuRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetMenu()
        {
            var response = this._menuRepository.GetMenu(_dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetMenuByParentId(long parentId)
        {
            var response = this._menuRepository.GetMenuByParentId(parentId,_dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetMenuForSearch()
        {
            var response = this._menuRepository.GetMenuForSearch(_dbName);
            return JsonConvert.SerializeObject(response);
        }

        public string GetSettingMenu(string menuName)
        {
            var response = this._menuRepository.GetSettingMenu(menuName, _dbName);
            return JsonConvert.SerializeObject(response);
        }
    }
}
