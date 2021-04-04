﻿using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository
{
    public partial interface IStaffRelationShipsRepository
    {
        HrmResultEntity<StaffRelationshipsEntity> GetRelationShipsByStaff(BasicParamType param, long staffId, out int totalRecord);
    }
}

