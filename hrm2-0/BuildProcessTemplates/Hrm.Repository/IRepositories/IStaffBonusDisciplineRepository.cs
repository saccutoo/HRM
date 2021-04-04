using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository
{
    public partial interface IStaffBonusDisciplineRepository
    {
        HrmResultEntity<StaffBonusDisciplineEntity> GetBonusDisciplineByStaff(BasicParamType param, long staffId, long type, out int totalRecord);
        HrmResultEntity<StaffBonusDisciplineEntity> GetStaffBonusDisciplineById(BasicParamType param, long id);
    }
}

