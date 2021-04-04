using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Hrm.Repository.Type;

namespace Hrm.Service
{
    public partial class StaffRoleService : IStaffRoleService
    {
        private string _dbName;
        IStaffRoleRepository _staffRoleRepository;
        public StaffRoleService(IStaffRoleRepository staffRoleRepository)
        {
            this._staffRoleRepository = staffRoleRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }

        string IStaffRoleService.GetRoleByStaff(BasicParamType param, long staffId, out int totalRecord)
        {
            var staffResponse = this._staffRoleRepository.GetRoleByStaff(param, staffId, out totalRecord);
            return JsonConvert.SerializeObject(staffResponse);
        }
    }
}
