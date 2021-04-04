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
    public partial class StaffBonusDisciplineService : IStaffBonusDisciplineService
    {
        private string _dbName;
        IStaffBonusDisciplineRepository _staffBonusDiscipline;
        public StaffBonusDisciplineService(IStaffBonusDisciplineRepository staffBonusDiscipline)
        {
            this._staffBonusDiscipline = staffBonusDiscipline;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
       public string GetBonusDisciplineByStaff(BasicParamType param, long staffId, long type, out int totalRecord)
        {
            var staffResponse = this._staffBonusDiscipline.GetBonusDisciplineByStaff(param, staffId, type, out totalRecord);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffBonusDisciplineById(BasicParamType param, long id)
        {
            var staffResponse = this._staffBonusDiscipline.GetStaffBonusDisciplineById(param, id);
            return JsonConvert.SerializeObject(staffResponse);
        }
    }
}
