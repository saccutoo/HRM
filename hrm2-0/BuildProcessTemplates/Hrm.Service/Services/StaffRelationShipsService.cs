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
    public partial class StaffRelationShipsService : IStaffRelationShipsService
    {
        private string _dbName;
        IStaffRelationShipsRepository _staffRelationShipsRepository;
        public StaffRelationShipsService(IStaffRelationShipsRepository staffRelationShipsRepository)
        {
            this._staffRelationShipsRepository = staffRelationShipsRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
            public  string GetRelationShipsByStaff(BasicParamType param,long staffId, out int totalRecord)
        {
            var staffResponse = this._staffRelationShipsRepository.GetRelationShipsByStaff(param, staffId, out totalRecord);
            return JsonConvert.SerializeObject(staffResponse);

        }
    }
}
