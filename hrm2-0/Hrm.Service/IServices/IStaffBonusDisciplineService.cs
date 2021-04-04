using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Service
{
    public partial interface IStaffBonusDisciplineService : IBaseService
    {
        string GetBonusDisciplineByStaff(BasicParamType param, long staffId, long type, out int totalRecord);
        string GetStaffBonusDisciplineById(BasicParamType param, long id);
    }
}
